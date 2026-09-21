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
    public class SqlPisoRepository_68SA : Connection_08YS, IPisoRepository_68SA
    {
        public SqlPisoRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public List<Piso_68SA> GetAll()
        {
            DataTable dt = GetDataTable("SELECT PisoID, Numero, Nombre FROM Pisos ORDER BY Numero");
            return PisoMapper_08YS.FromDataTable(dt);
        }

        public Piso_68SA GetById(int pisoId)
        {
            DataTable dt = GetDataTable(
                "SELECT PisoID, Numero, Nombre FROM Pisos WHERE PisoID = @PisoID",
                new[] { Param("@PisoID", pisoId) });

            return dt.Rows.Count > 0 ? PisoMapper_08YS.FromDataRow(dt.Rows[0]) : null;
        }

        public List<Piso_68SA> GetAll(PisoFiltro_68SA filtro)
        {
            var where = new List<string>();
            var parametros = new List<IDbDataParameter>();

            if (!string.IsNullOrWhiteSpace(filtro.Numero))
            {
                where.Add("CAST(p.Numero AS NVARCHAR(10)) LIKE @Numero");
                parametros.Add(Param("@Numero", $"%{filtro.Numero.Trim()}%"));
            }
            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                where.Add("p.Nombre LIKE @Nombre");
                parametros.Add(Param("@Nombre", $"%{filtro.Nombre.Trim()}%"));
            }

            // La cantidad de habitaciones viaja en la misma consulta (sin una query por fila)
            string query = @"
                SELECT p.PisoID, p.Numero, p.Nombre,
                (SELECT COUNT(1) FROM Habitaciones h WHERE h.PisoID = p.PisoID) AS CantidadHabitaciones
                FROM Pisos p"
                + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "")
                + " ORDER BY p.Numero";

            return PisoMapper_08YS.FromDataTable(GetDataTable(query, parametros.ToArray()));
        }

        public int Crear(Piso_68SA piso)
        {
            return ExecuteScalar<int>(
                @"INSERT INTO Pisos (Numero, Nombre) VALUES (@Numero, @Nombre);
          SELECT CAST(SCOPE_IDENTITY() AS int);",
                new[] { Param("@Numero", piso.Numero), Param("@Nombre", piso.Nombre) });
        }

        public void Modificar(Piso_68SA piso)
        {
            ExecuteNonQuery(
                "UPDATE Pisos SET Numero = @Numero, Nombre = @Nombre WHERE PisoID = @PisoID",
                new[] { Param("@Numero", piso.Numero), Param("@Nombre", piso.Nombre), Param("@PisoID", piso.PisoId) });
        }

        // Un solo DELETE atómico: si tiene habitaciones (o no existe) no afecta filas y devuelve false
        public bool Eliminar(int pisoId)
        {
            return ExecuteNonQuery(
                @"DELETE FROM Pisos
          WHERE PisoID = @PisoID
            AND NOT EXISTS (SELECT 1 FROM Habitaciones WHERE PisoID = @PisoID)",
                new[] { Param("@PisoID", pisoId) });
        }

        public bool ExisteNumero(int numero, int? excluirPisoId = null)
        {
            string query = "SELECT COUNT(1) FROM Pisos WHERE Numero = @Numero";
            var parametros = new List<IDbDataParameter> { Param("@Numero", numero) };

            if (excluirPisoId.HasValue)
            {
                query += " AND PisoID <> @Excluir";
                parametros.Add(Param("@Excluir", excluirPisoId.Value));
            }

            return ExecuteScalar<int>(query, parametros.ToArray()) > 0;
        }
    }
}
