using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories
{
    public interface IFamiliaRepository_08YS
    {
        List<Familia_08YS> GetAll();

        void Create(string nombre, List<AccessComponent_08YS> componentes);

        bool IsInUse(int familiaId);

        void Delete(int familiaId);
        void Modify(int familiaId, string nombre, List<AccessComponent_08YS> componentes);
    }
}
