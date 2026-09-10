using BE_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_08YS
{
    public static class HuespedMapper_68SA
    {
        public static List<Huesped_68SA> FromDataTable(DataTable dt)
        {
            var huespedes = new List<Huesped_68SA>();
            foreach (DataRow row in dt.Rows)
                huespedes.Add(FromDataRow(row));
            return huespedes;
        }

        public static Huesped_68SA FromDataRow(DataRow row)
        {
            return new Huesped_68SA
            {
                Id = Convert.ToInt32(row["HuespedID"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Documento = row["Documento"].ToString(),
                TipoDocumento = (TipoDocumento)Convert.ToInt32(row["TipoDocumento"]),
                Nacionalidad = row["Nacionalidad"] == DBNull.Value ? null : row["Nacionalidad"].ToString(),
                FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                Email = row["Email"] == DBNull.Value ? null : row["Email"].ToString(),
                Telefono = row["Telefono"] == DBNull.Value ? null : row["Telefono"].ToString()
            };
        }
    }
}
