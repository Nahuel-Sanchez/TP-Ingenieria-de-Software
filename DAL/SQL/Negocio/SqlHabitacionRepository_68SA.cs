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
        private const string BaseColumnas = @"
            h.HabitacionID, h.NroHabitacion, h.Estado,
            p.PisoID, p.Numero, p.Nombre AS NombrePiso,
            t.TipoHabitacionID, t.Nombre AS NombreTipo, t.Descripcion, t.Capacidad, t.TarifaNoche,
            CASE WHEN rHoy.ReservaID IS NOT NULL THEN 1 ELSE 0 END AS TieneReservaHoy";

        private const string BaseFrom = @"
            FROM Habitaciones h
            INNER JOIN Pisos p ON h.PisoID = p.PisoID
            INNER JOIN TiposHabitacion t ON h.TipoHabitacionID = t.TipoHabitacionID
            LEFT JOIN Reservas rHoy ON rHoy.HabitacionID = h.HabitacionID AND rHoy.Estado = 0 -- Confirmada
                                    AND rHoy.FechaIngreso <= CAST(GETDATE() AS DATE)
                                    AND rHoy.FechaEgreso > CAST(GETDATE() AS DATE)";

        private const string BaseSelect = "SELECT " + BaseColumnas + BaseFrom;

        private const string ListadoSelect =
            "SELECT " + BaseColumnas + @",
           ISNULL(rc.Total, 0) AS CantidadReservas,
           ISNULL(rc.Activas, 0) AS CantidadReservasActivas" + BaseFrom + @"
           LEFT JOIN (SELECT HabitacionID, COUNT(1) AS Total, SUM(CASE WHEN Estado IN (0, 1) THEN 1 ELSE 0 END) AS Activas
           FROM Reservas GROUP BY HabitacionID) rc ON rc.HabitacionID = h.HabitacionID";

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

        public List<Habitacion_68SA> GetListado(HabitacionFiltro_68SA filtro)
        {
            var where = new List<string>();
            var parametros = new List<IDbDataParameter>();

            if (!string.IsNullOrWhiteSpace(filtro.NroHabitacion))
            {
                where.Add("h.NroHabitacion LIKE @Nro");
                parametros.Add(Param("@Nro", $"%{filtro.NroHabitacion.Trim()}%"));
            }
            if (filtro.PisoId.HasValue)
            {
                where.Add("h.PisoID = @PisoID");
                parametros.Add(Param("@PisoID", filtro.PisoId.Value));
            }
            if (filtro.TipoHabitacionId.HasValue)
            {
                where.Add("h.TipoHabitacionID = @TipoID");
                parametros.Add(Param("@TipoID", filtro.TipoHabitacionId.Value));
            }

            string query = ListadoSelect
                + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "")
                + " ORDER BY p.Numero, h.NroHabitacion";

            return HabitacionMapper_68SA.FromDataTable(GetDataTable(query, parametros.ToArray()));
        }

        public int Crear(Habitacion_68SA habitacion)
        {
            return ExecuteScalar<int>(
                @"INSERT INTO Habitaciones (NroHabitacion, PisoID, TipoHabitacionID, Estado)
          VALUES (@Nro, @PisoID, @TipoID, @Estado);
          SELECT CAST(SCOPE_IDENTITY() AS int);",
                new[]
                {
            Param("@Nro", habitacion.NroHabitacion),
            Param("@PisoID", habitacion.Piso.PisoId),
            Param("@TipoID", habitacion.Tipo.Id),
            Param("@Estado", (int)EstadoHabitacion.Disponible)
                });
        }

        // No toca Estado: lo mueven el check-in/out y las acciones de Control de habitaciones
        public void Modificar(Habitacion_68SA habitacion)
        {
            ExecuteNonQuery(
                @"UPDATE Habitaciones
          SET NroHabitacion = @Nro, PisoID = @PisoID, TipoHabitacionID = @TipoID
          WHERE HabitacionID = @Id",
                new[]
                {
            Param("@Nro", habitacion.NroHabitacion),
            Param("@PisoID", habitacion.Piso.PisoId),
            Param("@TipoID", habitacion.Tipo.Id),
            Param("@Id", habitacion.Id)
                });
        }

        // Un solo DELETE atómico: si alguna vez tuvo reservas (FK Reservas→Habitaciones) o no existe, no afecta filas y devuelve false
        public bool Eliminar(int habitacionId)
        {
            return ExecuteNonQuery(
                @"DELETE FROM Habitaciones
          WHERE HabitacionID = @Id
            AND NOT EXISTS (SELECT 1 FROM Reservas WHERE HabitacionID = @Id)",
                new[] { Param("@Id", habitacionId) });
        }

        public bool ExisteNumero(string nroHabitacion, int? excluirHabitacionId = null)
        {
            string query = "SELECT COUNT(1) FROM Habitaciones WHERE NroHabitacion = @Nro";
            var parametros = new List<IDbDataParameter> { Param("@Nro", nroHabitacion) };

            if (excluirHabitacionId.HasValue)
            {
                query += " AND HabitacionID <> @Excluir";
                parametros.Add(Param("@Excluir", excluirHabitacionId.Value));
            }

            return ExecuteScalar<int>(query, parametros.ToArray()) > 0;
        }

        public bool TieneReservasActivas(int habitacionId)
        {
            return ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Reservas WHERE HabitacionID = @Id AND Estado IN (0, 1)", // Confirmada, En curso
                new[] { Param("@Id", habitacionId) }) > 0;
        }
    }
}
