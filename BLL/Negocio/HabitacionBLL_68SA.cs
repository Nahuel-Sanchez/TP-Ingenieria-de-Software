using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.habitacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    public class HabitacionBLL_68SA
    {
        private readonly IHabitacionRepository_68SA _habitacionRepo;

        public HabitacionBLL_68SA(IHabitacionRepository_68SA habitacionRepo)
        {
            _habitacionRepo = habitacionRepo;
        }

        // Modo Gestión
        public List<Habitacion_68SA> GetAll(int? idTipoHabitacion = null, EstadoHabitacion? estado = null)
        {
            return _habitacionRepo.GetAll(idTipoHabitacion, estado);
        }

        // Modo Reserva
        public List<Habitacion_68SA> GetDisponibles(DateTime fechaIngreso, DateTime fechaEgreso,
                                                      int? idTipoHabitacion = null, int? capacidadMinima = null)
        {
            ValidarRango(fechaIngreso, fechaEgreso);
            return _habitacionRepo.GetDisponibles(fechaIngreso, fechaEgreso, idTipoHabitacion, capacidadMinima);
        }

        public Habitacion_68SA GetById(int habitacionId)
        {
            return _habitacionRepo.GetById(habitacionId);
        }

        public void CambiarEstado(int habitacionId, EstadoHabitacion nuevoEstado)
        {
            _habitacionRepo.CambiarEstado(habitacionId, nuevoEstado);
        }

        // Pública y estática para que ReservaBLL_68SA la reutilice sin duplicar la regla
        public static void ValidarRango(DateTime fechaIngreso, DateTime fechaEgreso)
        {
            if (fechaIngreso.Date < DateTime.Today)
                throw new RangoFechasInvalidoException_68SA("La fecha de ingreso no puede ser anterior a hoy.");

            if (fechaEgreso.Date <= fechaIngreso.Date)
                throw new RangoFechasInvalidoException_68SA("La fecha de egreso debe ser posterior a la fecha de ingreso.");
        }
    }
}
