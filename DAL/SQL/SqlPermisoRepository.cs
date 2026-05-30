using DAL_08YS.Interfaces_Repositories;
using MPP_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.SQL
{
    public class SqlPermisoRepository : Connection_08YS , IPermisoRepository
    {
        public SqlPermisoRepository(IDbFactory_08YS factory) : base(factory) { }

        public List<Permiso> GetAll()
                => AccessMapper_08YS.PermisosFromTable(
                        GetDataTable("sp_GetAllPermisos", storedProcedure: true));

        public void Create(Permiso permiso)
            => ExecuteNonQuery("sp_CreatePermiso",
                    new[]
                    {
                        Param("@Nombre",      permiso.Nombre),
                        Param("@Descripcion", (object)permiso.Descripcion ?? DBNull.Value)
                    },
                    storedProcedure: true);

        public bool IsInUse(int permisoId)
        {
            return ExecuteScalar<int>(
                        "SELECT COUNT(1) FROM FamiliaPermiso WHERE PermisoID = @id",
                        new[] { Param("@id", permisoId) }) > 0

                || ExecuteScalar<int>(
                        "SELECT COUNT(1) FROM RolPermiso WHERE PermisoID = @id",
                        new[] { Param("@id", permisoId) }) > 0;
        }

        public void Delete(int permisoId)
            => ExecuteNonQuery("sp_DeletePermiso",
                                new[] { Param("@PermisoID", permisoId) },
                                storedProcedure: true);
    }
}
