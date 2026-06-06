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
    public class SqlFamiliaRepository_08YS : Connection_08YS , IFamiliaRepository_08YS
    {
        public SqlFamiliaRepository_08YS(IDbFactory_08YS factory) : base(factory) { }

        public List<Familia_08YS> GetAll()
        {
            DataSet ds = GetDataSet("sp_GetAllFamilias", storedProcedure: true);

            // RS0 → diccionario de familias por ID
            var familias = ds.Tables[0].AsEnumerable()
                .ToDictionary(
                    r => Convert.ToInt32(r["FamiliaID"]),
                    r => new Familia_08YS
                    {
                        FamiliaID = Convert.ToInt32(r["FamiliaID"]),
                        Nombre = r["Nombre"].ToString()
                    });

            // RS2 → permisos directos: se agregan antes que las subfamilias
            // para que ObtenerPermisos() los encuentre al reconstruir
            foreach (DataRow row in ds.Tables[2].Rows)
            {
                int fid = Convert.ToInt32(row["FamiliaID"]);
                if (familias.TryGetValue(fid, out var familia))
                    familia.Agregar(new Permiso_08YS
                    {
                        PermisoID = Convert.ToInt32(row["PermisoID"]),
                        Nombre = row["PermisoNombre"].ToString(),
                        Descripcion = row["Descripcion"] == DBNull.Value
                            ? null
                            : row["Descripcion"].ToString()
                    });
            }

            // RS1 → subfamilias: los hijos se agregan a sus padres
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                int hijoID = Convert.ToInt32(row["FamiliaID"]);
                int padreID = Convert.ToInt32(row["FamiliaPadreID"]);
                if (familias.TryGetValue(padreID, out var padre) &&
                    familias.TryGetValue(hijoID, out var hijo))
                    padre.Agregar(hijo);
            }

            // Solo retorna raíces (familias que no son hijas de nadie)
            var hijosIds = ds.Tables[1].AsEnumerable()
                .Select(r => Convert.ToInt32(r["FamiliaID"]))
                .ToHashSet();

            return familias.Values
                .Where(f => !hijosIds.Contains(f.FamiliaID))
                .ToList();
        }

        public void Create(string nombre, List<AccessComponent_08YS> componentes)
        {
            var permisosIds = componentes.OfType<Permiso_08YS>().Select(p => p.PermisoID).ToList();
            var subFamiliasIds = componentes.OfType<Familia_08YS>().Select(f => f.FamiliaID).ToList();

            var outputParam = ParamOutput("@NuevoFamiliaID");

            ExecuteNonQuery("sp_CreateFamilia",
                new IDbDataParameter[]
                {
                    Param("@Nombre",      nombre),
                    ParamTVP("@Permisos",    AccessMapper_08YS.ToIdTable(permisosIds)),
                    ParamTVP("@SubFamilias", AccessMapper_08YS.ToIdTable(subFamiliasIds)),
                    outputParam
                },
                storedProcedure: true);
            // outputParam.Value contiene el ID generado por SCOPE_IDENTITY()
        }

        public void Modify(int familiaId, string nombre, List<AccessComponent_08YS> componentes)
        {
            var permisosIds = componentes.OfType<Permiso_08YS>().Select(p => p.PermisoID).ToList();
            var subFamiliasIds = componentes.OfType<Familia_08YS>().Select(f => f.FamiliaID).ToList();

            ExecuteNonQuery("sp_ModifyFamilia",
                new IDbDataParameter[]
                {
                    Param("@FamiliaID",   familiaId),
                    Param("@Nombre",      nombre),
                    ParamTVP("@Permisos",    AccessMapper_08YS.ToIdTable(permisosIds)),
                    ParamTVP("@SubFamilias", AccessMapper_08YS.ToIdTable(subFamiliasIds))
                },
                storedProcedure: true);
        }

        public bool IsInUse(int familiaId)
            => ExecuteScalar<int>(
                "SELECT COUNT(1) FROM RolFamilia WHERE FamiliaID = @id",
                new[] { Param("@id", familiaId) }) > 0
            || ExecuteScalar<int>(
                "SELECT COUNT(1) FROM FamiliaIntegrada WHERE FamiliaID = @id",
                new[] { Param("@id", familiaId) }) > 0;

        public void Delete(int familiaId)
            => ExecuteNonQuery("sp_DeleteFamilia",
                new[] { Param("@FamiliaID", familiaId) },
                storedProcedure: true);
    }
}
