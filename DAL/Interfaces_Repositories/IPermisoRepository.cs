using MPP_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories
{
    public interface IPermisoRepository
    {
        List<Permiso> GetAll();

        void Create(Permiso permiso);

        bool IsInUse(int permisoId);

        void Delete(int permisoId);
    }
}
