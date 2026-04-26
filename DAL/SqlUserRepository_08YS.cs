using Service_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS
{
    public class SqlUserRepository_08YS : Connection_08YS, IUserRepository_08YS
    {
        public User GetByUsername(string username)
        {
            using (SqlConnection conn = GetSQLConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Username = reader["Username"].ToString(),
                            DNI = Convert.ToInt32(reader["DNI"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Hash = reader["Hash"].ToString(),
                            Salt = reader["Salt"].ToString(),
                            Email = reader["Email"].ToString(),
                            Celular = reader["Celular"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Bloqueado = Convert.ToBoolean(reader["Bloqueado"])
                        };
                    }
                }
            }

            return null;
        }

        public void BloquearUsuario(string username)
        {
            using (SqlConnection conn = GetSQLConnection())
            using (SqlCommand cmd = new SqlCommand("UPDATE Users SET Bloqueado = 1 WHERE Username = @Username", conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }



    }
}
