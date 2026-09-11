using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.habitacion;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlHabitacionRepository_68SA : Connection_08YS, IHabitacionRepository_68SA
    {
        // Alias usados por HabitacionMapper_08YS: HabitacionID, NroHabitacion, Estado,
        // PisoID, Numero, NombrePiso, TipoHabitacionID, NombreTipo, Descripcion, Capacidad, TarifaNoche
        private const string BaseSelect = @"
            SELECT h.HabitacionID, h.NroHabitacion, h.Estado,
                   p.PisoID, p.Numero, p.Nombre AS NombrePiso,
                   t.TipoHabitacionID, t.Nombre AS NombreTipo, t.Descripcion, t.Capacidad, t.TarifaNoche,
                   CASE WHEN rHoy.ReservaID IS NOT NULL THEN 1 ELSE 0 END AS TieneReservaHoy
            FROM Habitaciones h
            INNER JOIN Pisos p ON h.PisoID = p.PisoID
            INNER JOIN TiposHabitacion t ON h.TipoHabitacionID = t.TipoHabitacionID
            LEFT JOIN Reservas rHoy ON rHoy.HabitacionID = h.HabitacionID AND rHoy.Estado = 0 -- Confirmada
                                    AND rHoy.FechaIngreso <= CAST(GETDATE() AS DATE)
                                    AND rHoy.FechaEgreso > CAST(GETDATE() AS DATE)";

        public SqlHabitacionRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public List<Habitacion_68SA> GetAll(int? idTipoHabitacion = null)
        {
            var where = new List<string>();
            var parametros = new List<IDbDataParameter>();

            if (idTipoHabitacion.HasValue)
            {
                where.Add("h.TipoHabitacionID = @TipoHabitacionID");
                parametros.Add(Param("@TipoHabitacionID", idTipoHabitacion.Value));
            }

            string query = BaseSelect
                + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "")
                + " ORDER BY p.Numero, h.NroHabitacion";

            DataTable dt = GetDataTable(query, parametros.ToArray());
            return HabitacionMapper_68SA.FromDataTable(dt);
        }

        public List<Habitacion_68SA> GetDisponibles(DateTime fechaIngreso, DateTime fechaEgreso,
                                              int? idTipoHabitacion = null, int? capacidadMinima = null)
        {
            DataTable dt = GetDataTable("sp_GetHabitacionesDisponibles",
                new[]
                {
                    Param("@FechaIngreso", fechaIngreso.Date),
                    Param("@FechaEgreso",  fechaEgreso.Date),
                    Param("@TipoHabitacionID", (object)idTipoHabitacion ?? DBNull.Value),
                    Param("@CapacidadMinima",  (object)capacidadMinima ?? DBNull.Value)
                },
                storedProcedure: true);

            return HabitacionMapper_68SA.FromDataTable(dt);
        }

        public Habitacion_68SA GetById(int habitacionId)
        {
            DataTable dt = GetDataTable(
                BaseSelect + " WHERE h.HabitacionID = @Id",
                new[] { Param("@Id", habitacionId) });

            return dt.Rows.Count > 0 ? HabitacionMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public void CambiarEstado(int habitacionId, EstadoHabitacion nuevoEstado)
        {
            ExecuteNonQuery(
                "UPDATE Habitaciones SET Estado = @Estado WHERE HabitacionID = @Id",
                new[] { Param("@Estado", (int)nuevoEstado), Param("@Id", habitacionId) });
        }
    }
}
