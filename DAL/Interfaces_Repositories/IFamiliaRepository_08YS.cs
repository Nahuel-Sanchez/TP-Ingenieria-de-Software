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
        void Create(string nombre, List<AccessComponent> componentes);

        bool IsInUse(int familiaId);

        void Delete(int familiaId);
    }
}
