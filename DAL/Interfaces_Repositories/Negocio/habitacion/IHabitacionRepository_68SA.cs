using BE_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio.habitacion
{
    public interface IHabitacionRepository_68SA
    {
        List<Habitacion_68SA> GetAll(int? idTipoHabitacion = null);

        List<Habitacion_68SA> GetDisponibles(DateTime fechaIngreso, DateTime fechaEgreso,
                                              int? idTipoHabitacion = null, int? capacidadMinima = null);

        Habitacion_68SA GetById(int habitacionId);

        void CambiarEstado(int habitacionId, EstadoHabitacion nuevoEstado);
    }
}
