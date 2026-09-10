using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.habitacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    public class TipoHabitacionBLL_68SA
    {
        private readonly ITipoHabitacionRepository_68SA _tipoRepo;

        public TipoHabitacionBLL_68SA(ITipoHabitacionRepository_68SA tipoRepo)
        {
            _tipoRepo = tipoRepo;
        }

        public List<TipoHabitacion> GetAll()
        {
            return _tipoRepo.GetAll();
        }

        public TipoHabitacion GetById(int tipoHabitacionId)
        {
            return _tipoRepo.GetById(tipoHabitacionId);
        }
    }
}
