using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    public class PisoBLL_68SA
    {
        private readonly IPisoRepository_68SA _pisoRepo;

        public PisoBLL_68SA(IPisoRepository_68SA pisoRepo)
        {
            _pisoRepo = pisoRepo;
        }

        public List<Piso_68SA> GetAll()
        {
            return _pisoRepo.GetAll();
        }
    }
}
