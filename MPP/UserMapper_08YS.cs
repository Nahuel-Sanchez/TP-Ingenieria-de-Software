using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_08YS;

namespace MPP_08YS
{
    public static class UserMapper_08YS
    {
        public static List<User_08YS> FromDataTable(DataTable dt)
        {
            var users = new List<User_08YS>();
            foreach (DataRow row in dt.Rows)
                users.Add(FromDataRow(row));
            return users;
        }

        public static User_08YS FromDataRow(DataRow row)
        {
            return new User_08YS
            {
                Username = row["Username"].ToString(),
                DNI = Convert.ToInt32(row["DNI"]),
                Rol = (UserRole)Convert.ToInt32(row["RolID"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Hash = row["Hash"].ToString(),
                Salt = row["Salt"].ToString(),
                Email = row["Email"].ToString(),
                Bloqueado = Convert.ToBoolean(row["Bloqueado"]),
                Activo = Convert.ToBoolean(row["Activo"]),
                Idioma = row["Idioma"].ToString() 
            };
        }
    }
}
