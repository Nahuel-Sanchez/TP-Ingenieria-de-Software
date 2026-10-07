using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlBitacoraCambioHabitacionRepository_68SA : Connection_08YS, IBitacoraCambioHabitacionRepository_68SA
    {
        #region Campos

        private const string Select = @"
            SELECT b.BitacoraID, b.ReservaID, r.Estado AS EstadoReserva,
                   hu.Nombre AS NombreTitular, hu.Apellido AS ApellidoTitular,
                   b.HabitacionID, b.NroHabitacion, b.TipoHabitacion, b.TarifaNoche,
                   b.HabitacionAnteriorID, ha.NroHabitacion AS NroHabitacionAnterior,
                   b.CambioID, b.MontoCargo, b.Operacion, b.Fecha, b.Hora, b.UsuarioDB, b.UsuarioApp, b.Act
            FROM Bitacora_CambiosHabitacion b
            INNER JOIN Reservas r     ON r.ReservaID = b.ReservaID
            INNER JOIN Huespedes hu   ON hu.HuespedID = r.HuespedTitularID
            LEFT  JOIN Habitaciones ha ON ha.HabitacionID = b.HabitacionAnteriorID";

        #endregion

        #region Constructor

        public SqlBitacoraCambioHabitacionRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        #endregion

        #region Métodos públicos

        public List<BitacoraCambioHabitacion_68SA> Filtrar(BitacoraCambioHabitacionFiltro_68SA filtro)
        {
            filtro = filtro ?? new BitacoraCambioHabitacionFiltro_68SA();
            var where = new List<string>();
            var parametros = new List<IDbDataParameter>();

            if (filtro.Codigo.HasValue)
            {
                where.Add("b.ReservaID = @Codigo");
                parametros.Add(Param("@Codigo", filtro.Codigo.Value));
            }
            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                // "Nombre": del huésped titular o número / tipo de la habitación
                where.Add(@"(hu.Nombre LIKE @Nombre OR hu.Apellido LIKE @Nombre
                             OR (hu.Nombre + ' ' + hu.Apellido) LIKE @Nombre
                             OR b.NroHabitacion LIKE @Nombre OR b.TipoHabitacion LIKE @Nombre)");
                parametros.Add(Param("@Nombre", $"%{filtro.Nombre.Trim()}%"));
            }
            if (filtro.FechaDesde.HasValue)
            {
                where.Add("b.Fecha >= @FechaDesde");
                parametros.Add(Param("@FechaDesde", filtro.FechaDesde.Value.Date));
            }
            if (filtro.FechaHasta.HasValue)
            {
                where.Add("b.Fecha <= @FechaHasta");
                parametros.Add(Param("@FechaHasta", filtro.FechaHasta.Value.Date));
            }
            if (filtro.SoloActivos)
                where.Add("b.Act = 1");

            string query = Select
                + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "")
                + " ORDER BY b.ReservaID DESC, b.BitacoraID DESC";

            return BitacoraCambioHabitacionMapper_68SA.FromDataTable(GetDataTable(query, parametros.ToArray()));
        }

        #endregion
    }
}
