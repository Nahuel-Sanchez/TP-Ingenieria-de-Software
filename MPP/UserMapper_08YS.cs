using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_08YS;

namespace MPP
{
    public static class UserMapper_08YS
    {
        public static List<User> FromDataTable(DataTable dt)
        {
            var users = new List<User>();
            foreach (DataRow row in dt.Rows)
                users.Add(FromDataRow(row));
            return users;
        }

        public static User FromDataRow(DataRow row)
        {
            return new User
            {
                Username = row["Username"].ToString(),
                DNI = Convert.ToInt32(row["DNI"]),
                Rol = (UserRole)Enum.Parse(typeof(UserRole), row["Rol"].ToString()),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Hash = row["Hash"].ToString(),
                Salt = row["Salt"].ToString(),
                Email = row["Email"].ToString(),
                Celular = row["Celular"] == DBNull.Value ? null : row["Celular"].ToString(),
                Direccion = row["Direccion"] == DBNull.Value ? null : row["Direccion"].ToString(),
                Bloqueado = Convert.ToBoolean(row["Bloqueado"])
            };
        }
    }
}
