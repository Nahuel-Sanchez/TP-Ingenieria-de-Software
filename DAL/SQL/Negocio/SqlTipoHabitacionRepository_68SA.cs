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
    public class SqlTipoHabitacionRepository_68SA : Connection_08YS, ITipoHabitacionRepository_68SA
    {
        private const string BaseSelect =
            "SELECT TipoHabitacionID, Nombre, Descripcion, Capacidad, TarifaNoche FROM TiposHabitacion";

        public SqlTipoHabitacionRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public List<TipoHabitacion> GetAll()
        {
            DataTable dt = GetDataTable(BaseSelect + " ORDER BY Nombre");
            return TipoHabitacionMapper_08YS.FromDataTable(dt);
        }

        public TipoHabitacion GetById(int tipoHabitacionId)
        {
            DataTable dt = GetDataTable(
                BaseSelect + " WHERE TipoHabitacionID = @Id",
                new[] { Param("@Id", tipoHabitacionId) });

            return dt.Rows.Count > 0 ? TipoHabitacionMapper_08YS.FromDataRow(dt.Rows[0]) : null;
        }

        public List<TipoHabitacion> GetAll(TipoHabitacionFiltro_68SA filtro)
        {
            var where = new List<string>();
            var parametros = new List<IDbDataParameter>();

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                where.Add("t.Nombre LIKE @Nombre");
                parametros.Add(Param("@Nombre", $"%{filtro.Nombre.Trim()}%"));
            }
            if (!string.IsNullOrWhiteSpace(filtro.Descripcion))
            {
                where.Add("t.Descripcion LIKE @Descripcion");
                parametros.Add(Param("@Descripcion", $"%{filtro.Descripcion.Trim()}%"));
            }
            if (filtro.Capacidad.HasValue)
            {
                where.Add("t.Capacidad = @Capacidad");
                parametros.Add(Param("@Capacidad", filtro.Capacidad.Value));
            }
            if (filtro.TarifaDesde.HasValue)
            {
                where.Add("t.TarifaNoche >= @TarifaDesde");
                parametros.Add(Param("@TarifaDesde", filtro.TarifaDesde.Value));
            }
            if (filtro.TarifaHasta.HasValue)
            {
                where.Add("t.TarifaNoche <= @TarifaHasta");
                parametros.Add(Param("@TarifaHasta", filtro.TarifaHasta.Value));
            }

            // La cantidad de habitaciones viaja en la misma consulta (sin una query por fila)
            string query = @"
        SELECT t.TipoHabitacionID, t.Nombre, t.Descripcion, t.Capacidad, t.TarifaNoche,
               (SELECT COUNT(1) FROM Habitaciones h WHERE h.TipoHabitacionID = t.TipoHabitacionID) AS CantidadHabitaciones
        FROM TiposHabitacion t"
                + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "")
                + " ORDER BY t.Nombre";

            return TipoHabitacionMapper_08YS.FromDataTable(GetDataTable(query, parametros.ToArray()));
        }

        public int Crear(TipoHabitacion tipo)
        {
            return ExecuteScalar<int>(
                @"INSERT INTO TiposHabitacion (Nombre, Descripcion, Capacidad, TarifaNoche)
          VALUES (@Nombre, @Descripcion, @Capacidad, @TarifaNoche);
          SELECT CAST(SCOPE_IDENTITY() AS int);",
                new[]
                {
            Param("@Nombre", tipo.Nombre),
            Param("@Descripcion", (object)tipo.Descripcion ?? DBNull.Value),
            Param("@Capacidad", tipo.Capacidad),
            Param("@TarifaNoche", tipo.TarifaNoche)
                });
        }

        public void Modificar(TipoHabitacion tipo)
        {
            ExecuteNonQuery(
                @"UPDATE TiposHabitacion
          SET Nombre = @Nombre, Descripcion = @Descripcion, Capacidad = @Capacidad, TarifaNoche = @TarifaNoche
          WHERE TipoHabitacionID = @Id",
                new[]
                {
            Param("@Nombre", tipo.Nombre),
            Param("@Descripcion", (object)tipo.Descripcion ?? DBNull.Value),
            Param("@Capacidad", tipo.Capacidad),
            Param("@TarifaNoche", tipo.TarifaNoche),
            Param("@Id", tipo.Id)
                });
        }

        public bool Eliminar(int tipoHabitacionId)
        {
            return ExecuteNonQuery(
                @"DELETE FROM TiposHabitacion
          WHERE TipoHabitacionID = @Id
            AND NOT EXISTS (SELECT 1 FROM Habitaciones WHERE TipoHabitacionID = @Id)",
                new[] { Param("@Id", tipoHabitacionId) });
        }

        public bool ExisteNombre(string nombre, int? excluirTipoId = null)
        {
            string query = "SELECT COUNT(1) FROM TiposHabitacion WHERE Nombre = @Nombre";
            var parametros = new List<IDbDataParameter> { Param("@Nombre", nombre) };

            if (excluirTipoId.HasValue)
            {
                query += " AND TipoHabitacionID <> @Excluir";
                parametros.Add(Param("@Excluir", excluirTipoId.Value));
            }

            return ExecuteScalar<int>(query, parametros.ToArray()) > 0;
        }
    }
}
