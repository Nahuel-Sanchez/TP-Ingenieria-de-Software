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

        public decimal GetTarifaVigente(int reservaId)
        {
            return ExecuteScalar<decimal>("SELECT dbo.fn_TarifaVigenteReserva(@ReservaID)",
                new[] { Param("@ReservaID", reservaId) });
        }

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
            DataTable dt = GetDataTable(@"
                SELECT hc.CambioID, hc.ReservaID, hc.Motivo, hc.Observacion, hc.CargoID,
                       hc.HabitacionAnteriorID, ha.NroHabitacion AS NroHabitacionAnterior,
                       hc.HabitacionNuevaID, hn.NroHabitacion AS NroHabitacionNueva,
                       hc.TarifaAnterior, hc.TarifaNueva, hc.NochesRestantes, hc.FechaCambio,
                       ISNULL(c.Monto, 0) AS Monto
                FROM HistorialCambiosHabitacion hc
                INNER JOIN Habitaciones ha ON ha.HabitacionID = hc.HabitacionAnteriorID
                INNER JOIN Habitaciones hn ON hn.HabitacionID = hc.HabitacionNuevaID
                LEFT  JOIN CargosAdicionales c ON c.CargoID = hc.CargoID
                WHERE hc.ReservaID = @ReservaID
                ORDER BY hc.FechaCambio, hc.CambioID",
                new[] { Param("@ReservaID", reservaId) });

            return CambioHabitacionMapper_68SA.FromDataTable(dt);
        }

        #endregion

        #region Operaciones

        public ResultadoRegistroCambio_68SA Registrar(SolicitudCambioHabitacion_68SA solicitud, string descripcionCargo,
                                                      int? usuarioDni, string usuarioApp)
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
                    Param("@DescripcionCargo", (object)descripcionCargo ?? DBNull.Value),
                    Param("@UsuarioDNI", (object)usuarioDni ?? DBNull.Value),
                    Param("@UsuarioApp", (object)usuarioApp ?? DBNull.Value),
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
