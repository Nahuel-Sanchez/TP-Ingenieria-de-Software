using Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBitacora_08YS
    {
        public void RegistrarEvento(BitacoraEvento_08YS evento)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand
                ("INSERT INTO Bitacora (Login, FechaHora, Modulo, Descripcion, Criticidad) " +
                "VALUES (@login, @fecha_hora, @modulo, @descripcion, @criticidad)", conn))
            {
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                      new SqlParameter("@login", evento.Login),
                      new SqlParameter("@fecha_hora", evento.FechaHora),
                      new SqlParameter("@modulo", evento.Modulo.ToString()),
                      new SqlParameter("@descripcion", evento.Descripcion),
                      new SqlParameter("@criticidad", evento.Criticidad.ToString()),
                };

                cmd.Parameters.AddRange(parametros.ToArray());
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error de persistencia en Bitácora", ex);

                }
            }
        }

        public List<BitacoraEvento_08YS> ObtenerTodos()
        {
            List<BitacoraEvento_08YS> lista = new List<BitacoraEvento_08YS>();
            string query = "SELECT * FROM Bitacora";

            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Creamos el objeto pasando los datos al constructor
                            lista.Add(new BitacoraEvento_08YS(
                                reader["Login"].ToString(),
                                Convert.ToDateTime(reader["FechaHora"]),
                                (Modulo)Enum.Parse(typeof(Modulo),reader["Modulo"].ToString()),
                                reader["Descripcion"].ToString(),
                                (Criticidad)Enum.Parse(typeof(Criticidad), reader["Criticidad"].ToString())
                            ));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al cargar la bitácora desde la base de datos.", ex);
                }
            }
            return lista;
        }
    }
}
