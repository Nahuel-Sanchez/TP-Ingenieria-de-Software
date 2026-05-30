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
    public class SqlFamiliaRepository : Connection_08YS , IFamiliaRepository_08YS
    {
        public SqlFamiliaRepository(IDbFactory_08YS factory) : base(factory) { }

        public void Create(string nombre, List<AccessComponent> componentes)
        {
            var permisosIds = componentes.OfType<Permiso>().Select(p => p.PermisoID).ToList();
            var subFamiliasIds = componentes.OfType<Familia>().Select(f => f.FamiliaID).ToList();

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
