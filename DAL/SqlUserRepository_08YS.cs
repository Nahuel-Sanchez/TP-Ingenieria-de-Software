using DAL;
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
        // Recibe el factory por DI — no sabe nada de SQL Server directamente.
        public SqlUserRepository_08YS(IDbFactory_08YS factory) : base(factory) { }

        public User GetByUsername(string username)
        {
            DataTable dt = Leer(
                "SELECT * FROM Users WHERE Username = @Username",
                new[] { Param("@Username", username) }
            );

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new User
            {
                Username = row["Username"].ToString(),
                DNI = Convert.ToInt32(row["DNI"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Hash = row["Hash"].ToString(),
                Salt = row["Salt"].ToString(),
                Email = row["Email"].ToString(),
                Celular = row["Celular"].ToString(),
                Direccion = row["Direccion"].ToString(),
                Bloqueado = Convert.ToBoolean(row["Bloqueado"])
            };
        }

        public void BloquearUsuario(string username)
        {
            Escribir(
                "UPDATE Users SET Bloqueado = 1 WHERE Username = @Username",
                new[] { Param("@Username", username) }
            );
        }
    }
}
