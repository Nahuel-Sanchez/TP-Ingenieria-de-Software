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

        private IDbDataParameter[] ToParameters(BitacoraEvento_08YS evento)
        {
            return new []
            {
                Param("@login", evento.Login),
                Param("@fecha_hora", evento.FechaHora),
                Param("@modulo", evento.Modulo.ToString()),
                Param("@descripcion", evento.Descripcion),
                Param("@criticidad", evento.Criticidad.ToString())
            };
        }

        public void RegistrarEvento(BitacoraEvento_08YS evento)
        {
            ExecuteNonQuery(
                "INSERT INTO Bitacora (Login, FechaHora, Modulo, Descripcion, Criticidad) " +
                "VALUES (@login, @fecha_hora, @modulo, @descripcion, @criticidad)",
                ToParameters(evento));
        }

        public List<BitacoraEvento_08YS> GetAll()
        {
            DataTable dt = Leer("SELECT * FROM Bitacora");
            return BitacoraMapper_08YS.FromDataTable(dt);
        }
    }
}
