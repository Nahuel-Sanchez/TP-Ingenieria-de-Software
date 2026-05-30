using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_08YS
{
    public static class AccessMapper_08YS
    {
        // TVP builder
        public static DataTable ToIdTable(IEnumerable<int> ids)
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            foreach (var id in ids)
                dt.Rows.Add(id);
            return dt;
        }

        // Permiso
        public static Permiso PermisoFromRow(DataRow row) => new Permiso
        {
            PermisoID = Convert.ToInt32(row["PermisoID"]),
            Nombre = row["Nombre"].ToString(),
            Descripcion = row["Descripcion"] == DBNull.Value ? null : row["Descripcion"].ToString()
        };

        public static List<Permiso> PermisosFromTable(DataTable dt)
            => dt.AsEnumerable().Select(PermisoFromRow).ToList();

        // Familia 
        public static Familia FamiliaFromRow(DataRow row) => new Familia
        {
            FamiliaID = Convert.ToInt32(row["FamiliaID"]),
            Nombre = row["Nombre"].ToString()
        };
    }
}

