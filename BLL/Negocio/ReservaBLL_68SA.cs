using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using DAL_08YS.Interfaces_Repositories.Negocio.habitacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    public class ReservaBLL_68SA
    {
        private readonly IReservaRepository_68SA _reservaRepo;
        private readonly IHabitacionRepository_68SA _habitacionRepo;
        private readonly HuespedBLL_68SA _huespedBLL;

        public ReservaBLL_68SA(IReservaRepository_68SA reservaRepo, IHabitacionRepository_68SA habitacionRepo, HuespedBLL_68SA huespedBLL)
        {
            _reservaRepo = reservaRepo;
            _habitacionRepo = habitacionRepo;
            _huespedBLL = huespedBLL;
        }

        // PN1, pasos 2 a 10
        public int Crear(Reserva_68SA reserva)
        {
            HabitacionBLL_68SA.ValidarRango(reserva.FechaIngreso, reserva.FechaEgreso);

            if (!HuespedBLL_68SA.EsMayorDeEdad(reserva.Titular.FechaNacimiento))
                throw new TitularMenorDeEdadException_68SA();

            reserva.Titular = _huespedBLL.ObtenerOCrear(reserva.Titular);

            if (_reservaRepo.ExisteSolapamiento(reserva.Habitacion.Id, reserva.FechaIngreso, reserva.FechaEgreso))
                throw new HabitacionNoDisponibleException_68SA();

            reserva.Estado = EstadoReserva.Confirmada;
            reserva.MontoTotal = reserva.TarifaNoche * reserva.Noches;

            reserva.Id = _reservaRepo.Crear(reserva);

            // La habitación queda "Reservada" (badge del mockup) hasta que se haga el check-in
            _habitacionRepo.CambiarEstado(reserva.Habitacion.Id, EstadoHabitacion.Reservada);

            return reserva.Id;
        }

        public Reserva_68SA GetById(int reservaId)
        {
            var reserva = _reservaRepo.GetById(reservaId);
            if (reserva == null)
                throw new ReservaNoEncontradaException_68SA();

            reserva.Acompanantes = _reservaRepo.GetAcompanantes(reservaId);
            return reserva;
        }

        // PN1, check-in: búsqueda de la reserva por DNI del titular
        public Reserva_68SA GetConfirmadaPorDocumentoTitular(string documento)
        {
            return _reservaRepo.GetConfirmadaPorDocumentoTitular(documento);
        }

        // PN1, check-in
        public void RegistrarCheckIn(int reservaId, List<Huesped_68SA> acompanantes)
        {
            var reserva = _reservaRepo.GetById(reservaId);
            if (reserva == null)
                throw new ReservaNoEncontradaException_68SA();

            if (reserva.Estado != EstadoReserva.Confirmada)
                throw new EstadoReservaInvalidoException_68SA("Solo se puede hacer check-in sobre una reserva Confirmada.");

            _reservaRepo.RegistrarCheckIn(reservaId, DateTime.Now);
            _habitacionRepo.CambiarEstado(reserva.Habitacion.Id, EstadoHabitacion.Ocupada);

            if (acompanantes == null) return;

            foreach (var acompanante in acompanantes)
            {
                var registrado = _huespedBLL.ObtenerOCrear(acompanante);
                _reservaRepo.AgregarAcompanante(reservaId, registrado.Id);
            }
        }

        // PN1, check-out
        public void RegistrarCheckOut(int reservaId)
        {
            var reserva = _reservaRepo.GetById(reservaId);
            if (reserva == null)
                throw new ReservaNoEncontradaException_68SA();

            if (reserva.Estado != EstadoReserva.EnCurso)
                throw new EstadoReservaInvalidoException_68SA("Solo se puede hacer check-out sobre una reserva En Curso.");

            _reservaRepo.RegistrarCheckOut(reservaId, DateTime.Now);
            _habitacionRepo.CambiarEstado(reserva.Habitacion.Id, EstadoHabitacion.EnLimpieza);
        }

        public void Cancelar(int reservaId)
        {
            var reserva = _reservaRepo.GetById(reservaId);
            if (reserva == null)
                throw new ReservaNoEncontradaException_68SA();

            if (reserva.Estado != EstadoReserva.Confirmada)
                throw new EstadoReservaInvalidoException_68SA("Solo se puede cancelar una reserva Confirmada (sin check-in todavía).");

            _reservaRepo.Cancelar(reservaId);
            _habitacionRepo.CambiarEstado(reserva.Habitacion.Id, EstadoHabitacion.Disponible);
        }

        public List<Huesped_68SA> GetAcompanantes(int reservaId)
        {
            return _reservaRepo.GetAcompanantes(reservaId);
        }
    }
}
