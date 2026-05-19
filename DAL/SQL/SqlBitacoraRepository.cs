using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_08YS;
using MPP_08YS;
using DAL_08YS.Repositories_Interfaces;

namespace DAL_08YS
{
    public class SqlBitacoraRepository : Connection_08YS, IBitacoraRepository_08YS
    {
        public SqlBitacoraRepository(IDbFactory_08YS factory) : base(factory) { }

        private IDbDataParameter[] ToParameters(BitacoraEvento_08YS bitacora)
        {
            return new []
            {
                Param("@login", bitacora.Login),
                Param("@fecha_hora", bitacora.FechaHora),
                Param("@modulo", (int)bitacora.Modulo),
                Param("@evento", bitacora.Evento),
                Param("@criticidad", (int)bitacora.Criticidad)
            };
        }

        public void RegistrarEvento(BitacoraEvento_08YS evento)
        {
            ExecuteNonQuery(
                "INSERT INTO Bitacora (Login_08YS, FechaHora, Modulo, Evento, Criticidad) " +
                "VALUES (@login, @fecha_hora, @modulo, @evento, @criticidad)",
                ToParameters(evento));
        }

        public List<BitacoraEvento_08YS> GetAll()
        {
            DataTable dt = Leer("SELECT * FROM Bitacora ORDER BY FechaHora DESC");
            return BitacoraMapper_08YS.FromDataTable(dt);
        }

        public List<BitacoraEvento_08YS> Filtrar(BitacoraFiltro_08YS filtro)
        {
            StringBuilder query = new StringBuilder(
                "SELECT * FROM Bitacora WHERE 1=1");

            List<IDbDataParameter> parametros = new List<IDbDataParameter>();

            if (!string.IsNullOrWhiteSpace(filtro.Username))
            {
                query.Append(" AND Login_08YS LIKE @login");

                parametros.Add(
                    Param("@login", $"%{filtro.Username}%"));
            }

            if (filtro.FechaDesde.HasValue && filtro.FechaHasta.HasValue)
            {
                query.Append(" AND FechaHora >= @desde AND FechaHora <= @hasta");

                parametros.Add(
                    Param("@desde", filtro.FechaDesde.Value));

                parametros.Add(
                    Param("@hasta", filtro.FechaHasta.Value));
            }

            if (filtro.Modulo.HasValue)
            {
                query.Append(" AND Modulo = @modulo");

                parametros.Add(
                    Param("@modulo", (int)filtro.Modulo.Value));
            }

            if (filtro.Evento.HasValue)
            {
                query.Append(" AND Evento = @evento");

                parametros.Add(
                    Param("@evento", (int)filtro.Evento.Value));
            }

            if (filtro.Criticidad.HasValue)
            {
                query.Append(" AND Criticidad = @criticidad");

                parametros.Add(
                    Param("@criticidad", (int)filtro.Criticidad.Value));
            }

            query.Append(" ORDER BY FechaHora DESC");

            DataTable dt = Leer( query.ToString(), parametros.ToArray());

            return BitacoraMapper_08YS.FromDataTable(dt);
        }
    }
}
