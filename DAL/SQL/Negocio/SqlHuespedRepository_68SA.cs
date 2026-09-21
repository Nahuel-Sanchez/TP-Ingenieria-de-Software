using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlHuespedRepository_68SA : Connection_08YS, IHuespedRepository_68SA
    {
        private const string BaseSelect =
            "SELECT HuespedID, Nombre, Apellido, Documento, TipoDocumento, Nacionalidad, FechaNacimiento, Email, Telefono FROM Huespedes";

        public SqlHuespedRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public Huesped_68SA GetByDocumento(string documento)
        {
            DataTable dt = GetDataTable(
                BaseSelect + " WHERE Documento = @Documento",
                new[] { Param("@Documento", documento) });

            return dt.Rows.Count > 0 ? HuespedMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public bool Exists(string documento)
        {
            return ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Huespedes WHERE Documento = @Documento",
                new[] { Param("@Documento", documento) }) > 0;
        }

        public int Create(Huesped_68SA huesped)
        {
            return ExecuteScalar<int>(
                @"INSERT INTO Huespedes (Nombre, Apellido, Documento, TipoDocumento, Nacionalidad, FechaNacimiento, Email, Telefono)
                  VALUES (@Nombre, @Apellido, @Documento, @TipoDocumento, @Nacionalidad, @FechaNacimiento, @Email, @Telefono);
                  SELECT CAST(SCOPE_IDENTITY() AS int);",
                new[]
                {
                    Param("@Nombre",          huesped.Nombre),
                    Param("@Apellido",        huesped.Apellido),
                    Param("@Documento",       huesped.Documento),
                    Param("@TipoDocumento",   (int)huesped.TipoDocumento),
                    Param("@Nacionalidad",    huesped.Nacionalidad),
                    Param("@FechaNacimiento", huesped.FechaNacimiento.Date),
                    Param("@Email",           huesped.Email),
                    Param("@Telefono",        huesped.Telefono)
                });
        }

        public Huesped_68SA GetPorClaveCompleta(string documento, TipoDocumento tipoDocumento, string nacionalidad)
        {
            DataTable dt = GetDataTable(
                BaseSelect + @" WHERE Documento = @Documento AND TipoDocumento = @TipoDocumento
                        AND (Nacionalidad = @Nacionalidad OR (Nacionalidad IS NULL AND @Nacionalidad IS NULL))",
                new[]
                {
            Param("@Documento", documento),
            Param("@TipoDocumento", (int)tipoDocumento),
            Param("@Nacionalidad", (object)nacionalidad ?? DBNull.Value)
                });

            return dt.Rows.Count > 0 ? HuespedMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        // Cantidad de reservas (como titular + como acompañante) en la misma consulta, con una pasada agrupada por tabla
        private const string ListadoSelect = @"
    SELECT TOP (@Maximo)
           h.HuespedID, h.Nombre, h.Apellido, h.Documento, h.TipoDocumento, h.Nacionalidad,
           h.FechaNacimiento, h.Email, h.Telefono,
           ISNULL(rt.Cant, 0) + ISNULL(ra.Cant, 0) AS CantidadReservas
    FROM Huespedes h
    LEFT JOIN (SELECT HuespedTitularID AS HuespedID, COUNT(1) AS Cant
               FROM Reservas GROUP BY HuespedTitularID) rt ON rt.HuespedID = h.HuespedID
    LEFT JOIN (SELECT HuespedID, COUNT(1) AS Cant
               FROM Acompanantes GROUP BY HuespedID) ra ON ra.HuespedID = h.HuespedID";

        public Huesped_68SA GetById(int huespedId)
        {
            DataTable dt = GetDataTable(
                BaseSelect + " WHERE HuespedID = @Id",
                new[] { Param("@Id", huespedId) });

            return dt.Rows.Count > 0 ? HuespedMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public List<Huesped_68SA> GetListado(HuespedFiltro_68SA filtro)
        {
            var where = new List<string>();
            var parametros = new List<IDbDataParameter> { Param("@Maximo", filtro.Maximo) };

            void Like(string campo, string parametro, string valor)
            {
                if (string.IsNullOrWhiteSpace(valor)) return;
                where.Add($"{campo} LIKE {parametro}");
                parametros.Add(Param(parametro, $"%{valor.Trim()}%"));
            }

            Like("h.Nombre", "@Nombre", filtro.Nombre);
            Like("h.Apellido", "@Apellido", filtro.Apellido);
            Like("h.Documento", "@Documento", filtro.Documento);
            Like("h.Nacionalidad", "@Nacionalidad", filtro.Nacionalidad);
            Like("h.Email", "@Email", filtro.Email);
            Like("h.Telefono", "@Telefono", filtro.Telefono);

            if (filtro.TipoDocumento.HasValue)
            {
                where.Add("h.TipoDocumento = @TipoDocumento");
                parametros.Add(Param("@TipoDocumento", (int)filtro.TipoDocumento.Value));
            }
            if (filtro.NacimientoDesde.HasValue)
            {
                where.Add("h.FechaNacimiento >= @NacDesde");
                parametros.Add(Param("@NacDesde", filtro.NacimientoDesde.Value.Date));
            }
            if (filtro.NacimientoHasta.HasValue)
            {
                where.Add("h.FechaNacimiento <= @NacHasta");
                parametros.Add(Param("@NacHasta", filtro.NacimientoHasta.Value.Date));
            }

            string query = ListadoSelect
                + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "")
                + " ORDER BY h.Apellido, h.Nombre";

            return HuespedMapper_68SA.FromDataTable(GetDataTable(query, parametros.ToArray()));
        }

        public void Modificar(Huesped_68SA huesped)
        {
            ExecuteNonQuery(
                @"UPDATE Huespedes
          SET Nombre = @Nombre, Apellido = @Apellido, Documento = @Documento, TipoDocumento = @TipoDocumento,
              Nacionalidad = @Nacionalidad, FechaNacimiento = @FechaNacimiento, Email = @Email, Telefono = @Telefono
          WHERE HuespedID = @Id",
                new[]
                {
            Param("@Nombre",          huesped.Nombre),
            Param("@Apellido",        huesped.Apellido),
            Param("@Documento",       huesped.Documento),
            Param("@TipoDocumento",   (int)huesped.TipoDocumento),
            Param("@Nacionalidad",    huesped.Nacionalidad),
            Param("@FechaNacimiento", huesped.FechaNacimiento.Date),
            Param("@Email",           (object)huesped.Email ?? DBNull.Value),
            Param("@Telefono",        (object)huesped.Telefono ?? DBNull.Value),
            Param("@Id",              huesped.Id)
                });
        }

        // Un solo DELETE atómico: si figura en alguna reserva (titular o acompañante) o no existe, no afecta filas y devuelve false
        public bool Eliminar(int huespedId)
        {
            return ExecuteNonQuery(
                @"DELETE FROM Huespedes
          WHERE HuespedID = @Id
            AND NOT EXISTS (SELECT 1 FROM Reservas WHERE HuespedTitularID = @Id)
            AND NOT EXISTS (SELECT 1 FROM Acompanantes WHERE HuespedID = @Id)",
                new[] { Param("@Id", huespedId) });
        }

        public bool ExisteOtroConClave(int excluirHuespedId, string documento, TipoDocumento tipoDocumento, string nacionalidad)
        {
            return ExecuteScalar<int>(
                @"SELECT COUNT(1) FROM Huespedes
          WHERE HuespedID <> @Id AND Documento = @Documento
            AND TipoDocumento = @TipoDocumento AND Nacionalidad = @Nacionalidad",
                new[]
                {
            Param("@Id", excluirHuespedId),
            Param("@Documento", documento),
            Param("@TipoDocumento", (int)tipoDocumento),
            Param("@Nacionalidad", nacionalidad)
                }) > 0;
        }
    }
}
