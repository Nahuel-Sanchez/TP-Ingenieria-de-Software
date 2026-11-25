using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlCambioHabitacionRepository_68SA : Connection_08YS, ICambioHabitacionRepository_68SA
    {
        #region Constructor

        public SqlCambioHabitacionRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        #endregion

        #region Consultas

        public List<Habitacion_68SA> GetHabitacionesParaCambio(int reservaId, bool soloUpgrade)
        {
            DataTable dt = GetDataTable("sp_GetHabitacionesParaCambio",
                new[] { Param("@ReservaID", reservaId), Param("@SoloUpgrade", soloUpgrade) },
                storedProcedure: true);

            // Mismas columnas que sp_GetHabitacionesDisponibles
            return HabitacionMapper_68SA.FromDataTable(dt);
        }

        public List<CambioHabitacion_68SA> GetByReserva(int reservaId)
        {
            // Historial de habitaciones de la reserva: cada registro dice en qué habitación está desde ese
            // momento y qué tarifa paga. La habitación y la tarifa anteriores de un cambio son las del registro
            // previo (LAG); el registro inicial (Motivo 2, la habitación con la que se reservó) no es un cambio.
            DataTable dt = GetDataTable(@"
                ;WITH hc AS (
                    SELECT h.CambioID, h.ReservaID, h.Motivo, h.Observacion, h.CargoID, h.HabitacionID, h.TarifaNoche,
                           h.NochesRestantes, h.FechaCambio,
                           LAG(h.HabitacionID) OVER (PARTITION BY h.ReservaID ORDER BY h.FechaCambio, h.CambioID) AS HabitacionAnteriorID,
                           LAG(h.TarifaNoche)  OVER (PARTITION BY h.ReservaID ORDER BY h.FechaCambio, h.CambioID) AS TarifaAnterior
                    FROM HistorialCambiosHabitacion h
                    WHERE h.ReservaID = @ReservaID
                )
                SELECT hc.CambioID, hc.ReservaID, hc.Motivo, hc.Observacion, hc.CargoID,
                       hc.HabitacionAnteriorID, ha.NroHabitacion AS NroHabitacionAnterior,
                       hc.HabitacionID AS HabitacionNuevaID, hn.NroHabitacion AS NroHabitacionNueva,
                       hc.TarifaAnterior, hc.TarifaNoche AS TarifaNueva, hc.NochesRestantes, hc.FechaCambio,
                       ISNULL(c.Monto, 0) AS Monto
                FROM hc
                INNER JOIN Habitaciones ha ON ha.HabitacionID = hc.HabitacionAnteriorID
                INNER JOIN Habitaciones hn ON hn.HabitacionID = hc.HabitacionID
                LEFT  JOIN CargosAdicionales c ON c.CargoID = hc.CargoID
                WHERE hc.Motivo <> 2
                ORDER BY hc.FechaCambio, hc.CambioID",
                new[] { Param("@ReservaID", reservaId) });

            return CambioHabitacionMapper_68SA.FromDataTable(dt);
        }

        #endregion

        #region Operaciones

        public ResultadoRegistroCambio_68SA Registrar(SolicitudCambioHabitacion_68SA solicitud)
        {
            var cambioOutput = ParamOutput("@CambioID");
            var montoOutput = ParamOutputDecimal("@Monto");
            var resultadoOutput = ParamOutput("@Resultado");

            ExecuteNonQuery("sp_RegistrarCambioHabitacion",
                new[]
                {
                    Param("@ReservaID", solicitud.ReservaId),
                    Param("@HabitacionNuevaID", solicitud.HabitacionNuevaId),
                    Param("@Motivo", (int)solicitud.Motivo),
                    Param("@Observacion", string.IsNullOrWhiteSpace(solicitud.Observacion) ? (object)DBNull.Value : solicitud.Observacion),
                    cambioOutput,
                    montoOutput,
                    resultadoOutput
                },
                storedProcedure: true);

            return new ResultadoRegistroCambio_68SA
            {
                Resultado = (ResultadoCambioHabitacion)Convert.ToInt32(resultadoOutput.Value),
                CambioId = cambioOutput.Value == DBNull.Value ? (int?)null : Convert.ToInt32(cambioOutput.Value),
                Monto = montoOutput.Value == DBNull.Value ? 0m : Convert.ToDecimal(montoOutput.Value)
            };
        }

        #endregion
    }
}
