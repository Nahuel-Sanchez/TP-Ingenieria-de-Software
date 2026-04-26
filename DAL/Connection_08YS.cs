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
        private readonly string connectionString;
        public Connection_08YS()
        {
            connectionString = "";
        }
        protected SqlConnection GetSQLConnection()
        {
            return new SqlConnection(connectionString);
        }

        public DataTable Leer(string query, SqlParameter[] parameters = null, bool StoredProcedure = false)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (StoredProcedure) cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null) cmd.Parameters.AddRange(parameters);

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(dt);

                return dt;
            }
        }

        public bool Escribir(string query, SqlParameter[] parameters = null, bool StoredProcedure = false)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                if (StoredProcedure) cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null) cmd.Parameters.AddRange(parameters);

                connection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
