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

        // CRUD
        public List<Habitacion_68SA> GetListado(HabitacionFiltro_68SA filtro)
        {
            filtro = filtro ?? new HabitacionFiltro_68SA();
            var habitaciones = _habitacionRepo.GetListado(filtro);

            // Mismo criterio que GetAll(tipo, estado): el estado se compara contra EstadoVisual
            return filtro.Estado.HasValue
                ? habitaciones.Where(h => h.EstadoVisual == filtro.Estado.Value).ToList()
                : habitaciones;
        }

        public int Crear(Habitacion_68SA habitacion)
        {
            Validar(habitacion);

            if (_habitacionRepo.ExisteNumero(habitacion.NroHabitacion))
                throw new HabitacionNumeroDuplicadoException_68SA();

            habitacion.Id = _habitacionRepo.Crear(habitacion);
            _bitacoraBll.RegistrarEvento(Evento.HabitacionCreada, targetUsername: habitacion.Id.ToString());
            return habitacion.Id;
        }

        public void Modificar(Habitacion_68SA habitacion)
        {
            Validar(habitacion);

            var actual = _habitacionRepo.GetById(habitacion.Id);
            if (actual == null)
                throw new HabitacionNoEncontradaException_68SA();

            if (_habitacionRepo.ExisteNumero(habitacion.NroHabitacion, habitacion.Id))
                throw new HabitacionNumeroDuplicadoException_68SA();

            // Cambiar el tipo con reservas Confirmada/En curso las dejaría con tarifa y capacidad de otro tipo
            if (actual.Tipo.Id != habitacion.Tipo.Id && _habitacionRepo.TieneReservasActivas(habitacion.Id))
                throw new HabitacionConReservasActivasException_68SA();

            _habitacionRepo.Modificar(habitacion);
            _bitacoraBll.RegistrarEvento(Evento.HabitacionModificada, targetUsername: habitacion.Id.ToString());
        }

        public void Eliminar(int habitacionId)
        {
            if (_habitacionRepo.GetById(habitacionId) == null)
                throw new HabitacionNoEncontradaException_68SA();

            if (!_habitacionRepo.Eliminar(habitacionId))
                throw new HabitacionConReservasException_68SA();

            _bitacoraBll.RegistrarEvento(Evento.HabitacionEliminada, targetUsername: habitacionId.ToString());
        }

        private static void Validar(Habitacion_68SA habitacion)
        {
            if (habitacion == null) throw new ArgumentNullException(nameof(habitacion));

            habitacion.NroHabitacion = habitacion.NroHabitacion?.Trim();
            if (string.IsNullOrEmpty(habitacion.NroHabitacion))
                throw new DatosInvalidosException_68SA("El número de habitación es obligatorio.");
            if (habitacion.NroHabitacion.Length > 10)
                throw new DatosInvalidosException_68SA("El número de habitación no puede superar los 10 caracteres.");
            if (habitacion.Piso == null || habitacion.Piso.PisoId <= 0)
                throw new DatosInvalidosException_68SA("Debe seleccionar un piso.");
            if (habitacion.Tipo == null || habitacion.Tipo.Id <= 0)
                throw new DatosInvalidosException_68SA("Debe seleccionar un tipo de habitación.");
        }
    }
}