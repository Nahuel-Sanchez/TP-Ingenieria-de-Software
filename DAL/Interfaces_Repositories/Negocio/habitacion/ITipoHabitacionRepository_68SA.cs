using BE_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio.habitacion
{
    public interface ITipoHabitacionRepository_68SA
    {
        List<TipoHabitacion> GetAll();
        TipoHabitacion GetById(int tipoHabitacionId);
    }
}
