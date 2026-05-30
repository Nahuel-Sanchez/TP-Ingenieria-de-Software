using MPP_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories
{
    public interface IRolRepository_08YS
    {
        List<Rol> GetAll();

        void Create(string nombre, List<AccessComponent> componentes);

        bool IsInUse(int rolId);

        void Delete(int rolId);
    }
}
