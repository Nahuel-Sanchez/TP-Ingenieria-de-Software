using DAL_08YS.Interfaces_Repositories;
using MPP_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.SQL
{
    public class SqlRolRepository : Connection_08YS , IRolRepository_08YS
    {
        public SqlRolRepository(IDbFactory_08YS factory) : base(factory) { }

        public List<Rol_08YS> GetAll()
        {
            DataSet ds = GetDataSet("sp_GetAllRolesConEstructura", storedProcedure: true);

            // RS3-RS4-RS5 → reconstruye el árbol de familias igual que SqlFamiliaRepository
            var familias = ds.Tables[3].AsEnumerable()
                .ToDictionary(
                    r => Convert.ToInt32(r["FamiliaID"]),
                    r => AccessMapper_08YS.FamiliaFromRow(r));

            foreach (DataRow row in ds.Tables[5].Rows)
            {
                int fid = Convert.ToInt32(row["FamiliaID"]);
                if (familias.TryGetValue(fid, out var f))
                    f.Agregar(AccessMapper_08YS.PermisoFromRow(row));
            }
            foreach (DataRow row in ds.Tables[4].Rows)
            {
                int hijoID = Convert.ToInt32(row["FamiliaID"]);
                int padreID = Convert.ToInt32(row["FamiliaPadreID"]);
                if (familias.TryGetValue(padreID, out var padre) &&
                    familias.TryGetValue(hijoID, out var hijo))
                    padre.Agregar(hijo);
            }

            // RS0 → roles
            var roles = ds.Tables[0].AsEnumerable()
                .ToDictionary(
                    r => Convert.ToInt32(r["RolID"]),
                    r => new Rol_08YS { RolID = Convert.ToInt32(r["RolID"]), Nombre = r["Nombre"].ToString() });

            // RS1 → familias completas al rol
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                int rolId = Convert.ToInt32(row["RolID"]);
                int familiaId = Convert.ToInt32(row["FamiliaID"]);
                if (roles.TryGetValue(rolId, out var rol) &&
                    familias.TryGetValue(familiaId, out var familia))
                    rol.Agregar(familia);
            }

            // RS2 → permisos directos al rol
            foreach (DataRow row in ds.Tables[2].Rows)
            {
                int rolId = Convert.ToInt32(row["RolID"]);
                if (roles.TryGetValue(rolId, out var rol))
                    rol.Agregar(AccessMapper_08YS.PermisoFromRow(row));
            }

            return roles.Values.ToList();
        }

        public void Create(string nombre, List<AccessComponent> componentes)
        {
            var familiasIds = componentes.OfType<Familia_08YS>().Select(f => f.FamiliaID).ToList();
            var permisosIds = componentes.OfType<Permiso_08YS>().Select(p => p.PermisoID).ToList();

            var outputParam = ParamOutput("@NuevoRolID");

            ExecuteNonQuery("sp_CreateRol",
                new IDbDataParameter[]
                {
                Param("@Nombre",   nombre),
                ParamTVP("@Familias", AccessMapper_08YS.ToIdTable(familiasIds)),
                ParamTVP("@Permisos", AccessMapper_08YS.ToIdTable(permisosIds)),
                outputParam
                },
                storedProcedure: true);
        }

        public void Modify(int rolId, string nombre, List<AccessComponent> componentes)
        {
            var familiasIds = componentes.OfType<Familia_08YS>().Select(f => f.FamiliaID).ToList();
            var permisosIds = componentes.OfType<Permiso_08YS>().Select(p => p.PermisoID).ToList();

            ExecuteNonQuery("sp_ModifyRol",
                new IDbDataParameter[]
                {
                    Param("@RolID",   rolId),
                    Param("@Nombre",  nombre),
                    ParamTVP("@Familias", AccessMapper_08YS.ToIdTable(familiasIds)),
                    ParamTVP("@Permisos", AccessMapper_08YS.ToIdTable(permisosIds))
                },
                storedProcedure: true);
        }

        public bool IsInUse(int rolId)
            => ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Users WHERE Rol = @id",
                new[] { Param("@id", rolId) }) > 0;

        public void Delete(int rolId)
            => ExecuteNonQuery("sp_DeleteRol",
                new[] { Param("@RolID", rolId) },
                storedProcedure: true);
    }
}
