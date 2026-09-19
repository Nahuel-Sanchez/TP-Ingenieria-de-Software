using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.habitacion;
using Service_08YS.Entities.Bitacora;
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
        private readonly BitacoraBLL_08YS _bitacoraBll;

        public HabitacionBLL_68SA(IHabitacionRepository_68SA habitacionRepo, BitacoraBLL_08YS bitacoraBll)
        {
            _habitacionRepo = habitacionRepo;
            _bitacoraBll = bitacoraBll;
        }

        // Modo Gestión
        public List<Habitacion_68SA> GetAll(int? idTipoHabitacion = null, EstadoHabitacion? estado = null)
        {
            var habitaciones = _habitacionRepo.GetAll(idTipoHabitacion);
            return estado.HasValue
                ? habitaciones.Where(h => h.EstadoVisual == estado.Value).ToList()
                : habitaciones;
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

            // FueraDeServicio ("Poner en Mantenimiento") es la transición operativamente más
            // relevante -> evento propio con criticidad Alto. El resto queda como evento genérico.
            var evento = nuevoEstado == EstadoHabitacion.FueraDeServicio
                ? Evento.HabitacionPuestaEnMantenimiento
                : Evento.HabitacionEstadoCambiado;

            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(evento, targetUsername: habitacionId.ToString());
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