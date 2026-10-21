using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlBitacoraReservaRepository_68SA : Connection_08YS, IBitacoraReservaRepository_68SA
    {
        #region Campos

        // Cada registro junto con los datos del registro anterior de la misma reserva (LAG), para mostrar
        // qué cambió. Los filtros se aplican después, así el "anterior" existe aunque quede fuera del filtro.
        private const string Historia = @"
            ;WITH Historia AS (
                SELECT b.BitacoraID, b.ReservaID, b.Operacion, b.CambioID, b.Fecha, b.Hora, b.UsuarioDB, b.UsuarioApp, b.Act,
                       b.RollbackDeID,
                       b.HuespedTitularID, hu.Nombre AS NombreTitular, hu.Apellido AS ApellidoTitular,
                       b.HabitacionID, h.NroHabitacion, t.Nombre AS TipoHabitacion,
                       b.FechaIngreso, b.FechaEgreso, b.CheckIn, b.CheckOut, b.Estado,
                       b.CantidadAdultos, b.CantidadNinos, b.TarifaNoche, b.MontoTotal, b.MontoOriginal,
                       b.UsuarioRegistroDNI, u.Username AS UsuarioRegistro, b.FechaRegistro,

                       LAG(b.BitacoraID)         OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_BitacoraID,
                       LAG(b.HuespedTitularID)   OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_HuespedTitularID,
                       LAG(hu.Nombre)            OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_NombreTitular,
                       LAG(hu.Apellido)          OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_ApellidoTitular,
                       LAG(b.HabitacionID)       OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_HabitacionID,
                       LAG(h.NroHabitacion)      OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_NroHabitacion,
                       LAG(t.Nombre)             OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_TipoHabitacion,
                       LAG(b.FechaIngreso)       OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_FechaIngreso,
                       LAG(b.FechaEgreso)        OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_FechaEgreso,
                       LAG(b.CheckIn)            OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_CheckIn,
                       LAG(b.CheckOut)           OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_CheckOut,
                       LAG(b.Estado)             OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_Estado,
                       LAG(b.CantidadAdultos)    OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_CantidadAdultos,
                       LAG(b.CantidadNinos)      OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_CantidadNinos,
                       LAG(b.TarifaNoche)        OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_TarifaNoche,
                       LAG(b.MontoTotal)         OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_MontoTotal,
                       LAG(b.MontoOriginal)      OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_MontoOriginal,
                       LAG(b.UsuarioRegistroDNI) OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_UsuarioRegistroDNI,
                       LAG(u.Username)           OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_UsuarioRegistro,
                       LAG(b.FechaRegistro)      OVER (PARTITION BY b.ReservaID ORDER BY b.BitacoraID) AS Ant_FechaRegistro
                FROM Bitacora_Reservas b
                LEFT JOIN Huespedes hu       ON hu.HuespedID = b.HuespedTitularID
                LEFT JOIN Habitaciones h     ON h.HabitacionID = b.HabitacionID
                LEFT JOIN TiposHabitacion t  ON t.TipoHabitacionID = h.TipoHabitacionID
                LEFT JOIN Users u            ON u.DNI = b.UsuarioRegistroDNI
            )
            SELECT * FROM Historia";

        #endregion

        #region Constructor

        public SqlBitacoraReservaRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        #endregion

        #region Métodos públicos

        public List<BitacoraReserva_68SA> Filtrar(BitacoraReservaFiltro_68SA filtro)
        {
            filtro = filtro ?? new BitacoraReservaFiltro_68SA();
            var where = new List<string>();
            var parametros = new List<IDbDataParameter>();

            if (filtro.Codigo.HasValue)
            {
                where.Add("ReservaID = @Codigo");
                parametros.Add(Param("@Codigo", filtro.Codigo.Value));
            }
            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                // "Nombre": del huésped titular o número / tipo de la habitación
                where.Add(@"(NombreTitular LIKE @Nombre OR ApellidoTitular LIKE @Nombre
                             OR (NombreTitular + ' ' + ApellidoTitular) LIKE @Nombre
                             OR NroHabitacion LIKE @Nombre OR TipoHabitacion LIKE @Nombre)");
                parametros.Add(Param("@Nombre", $"%{filtro.Nombre.Trim()}%"));
            }
            if (filtro.Operacion.HasValue)
            {
                where.Add("Operacion = @Operacion");
                parametros.Add(Param("@Operacion", BitacoraReserva_68SA.TextoBase(filtro.Operacion.Value)));
            }
            if (filtro.FechaDesde.HasValue)
            {
                where.Add("Fecha >= @FechaDesde");
                parametros.Add(Param("@FechaDesde", filtro.FechaDesde.Value.Date));
            }
            if (filtro.FechaHasta.HasValue)
            {
                where.Add("Fecha <= @FechaHasta");
                parametros.Add(Param("@FechaHasta", filtro.FechaHasta.Value.Date));
            }
            if (filtro.SoloActivos)
                where.Add("Act = 1");

            string query = Historia
                + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "")
                + " ORDER BY BitacoraID DESC";

            return BitacoraReservaMapper_68SA.FromDataTable(GetDataTable(query, parametros.ToArray()));
        }

        public ResultadoRollbackReserva Rollback(int bitacoraId)
        {
            var resultado = ParamOutput("@Resultado");
            ExecuteNonQuery("sp_RollbackReserva", new[] { Param("@BitacoraID", bitacoraId), resultado },
                storedProcedure: true, operacionReserva: "Rollback");
            return (ResultadoRollbackReserva)Convert.ToInt32(resultado.Value);
        }

        #endregion
    }
}
