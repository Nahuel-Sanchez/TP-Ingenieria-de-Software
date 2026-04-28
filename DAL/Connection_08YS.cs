using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS
{
    public abstract class Connection_08YS
    {
        private readonly IDbFactory_08YS _factory;

        protected Connection_08YS(IDbFactory_08YS factory)
        {
            _factory = factory;
        }

        // Helper para crear parametros de los repositorios concretos sin necesitar una referencia directa al factory
        protected IDbDataParameter Param(string name, object value)
            => _factory.CreateParameter(name, value);

        public DataTable Leer(string query, IDbDataParameter[] parameters = null, bool storedProcedure = false)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            using (IDbCommand cmd = _factory.CreateCommand(query, conn))
            {
                if (storedProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                // El factory llenara la tabla en funcion de la BD.
                return _factory.FillDataTable(cmd);
            }
        }

        public bool Escribir(string query, IDbDataParameter[] parameters = null, bool storedProcedure = false)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            using (IDbCommand cmd = _factory.CreateCommand(query, conn))
            {
                if (storedProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
