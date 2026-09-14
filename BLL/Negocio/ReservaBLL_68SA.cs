using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL_08YS.Negocio
{
    public class ReservaBLL_68SA
    {
        private readonly IReservaRepository_68SA _reservaRepo;
        private readonly HuespedBLL_68SA _huespedBLL;

        public ReservaBLL_68SA(IReservaRepository_68SA reservaRepo, HuespedBLL_68SA huespedBLL)
        {
            _reservaRepo = reservaRepo;
            _huespedBLL = huespedBLL;
        }

        public int Crear(Reserva_68SA reserva)
        {
            HabitacionBLL_68SA.ValidarRango(reserva.FechaIngreso, reserva.FechaEgreso);

            if (!HuespedBLL_68SA.EsMayorDeEdad(reserva.Titular.FechaNacimiento))
                throw new TitularMenorDeEdadException_68SA();

            reserva.Titular = _huespedBLL.ObtenerOCrear(reserva.Titular);
            reserva.MontoTotal = reserva.TarifaNoche * reserva.Noches;

            int nuevoId = _reservaRepo.Crear(reserva);
            if (nuevoId == -1)
                throw new HabitacionNoDisponibleException_68SA();

            reserva.Id = nuevoId;
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

        public Reserva_68SA GetPorDocumentoTitularYEstado(string documento, EstadoReserva estado)
        {
            return _reservaRepo.GetPorDocumentoTitularYEstado(documento, estado);
        }

        public Reserva_68SA GetConfirmadaHoyPorHabitacion(int habitacionId)
        {
            return _reservaRepo.GetConfirmadaHoyPorHabitacion(habitacionId);
        }

        public void RegistrarCheckIn(int reservaId, List<Huesped_68SA> acompanantes)
        {
            if (acompanantes != null && acompanantes.Count > 0)
            {
                var reserva = _reservaRepo.GetById(reservaId);
                if (reserva != null && acompanantes.Any(a => a.Documento == reserva.Titular.Documento))
                    throw new TitularEntreAcompanantesException_68SA();
            }

            if (!_reservaRepo.RegistrarCheckIn(reservaId, DateTime.Now))
                throw new EstadoReservaInvalidoException_68SA("Solo se puede hacer check-in sobre una reserva Confirmada.");

            if (acompanantes == null) return;

            foreach (var acompanante in acompanantes)
            {
                var registrado = _huespedBLL.ObtenerOCrear(acompanante);
                _reservaRepo.AgregarAcompanante(reservaId, registrado.Id);
            }
        }

        public void RegistrarCheckOut(int reservaId)
        {
            if (!_reservaRepo.RegistrarCheckOut(reservaId, DateTime.Now))
                throw new EstadoReservaInvalidoException_68SA("Solo se puede hacer check-out sobre una reserva En Curso.");
        }

        public void Cancelar(int reservaId)
        {
            if (!_reservaRepo.Cancelar(reservaId))
                throw new EstadoReservaInvalidoException_68SA("Solo se puede cancelar una reserva Confirmada (sin check-in todavía).");
        }

        public List<Huesped_68SA> GetAcompanantes(int reservaId)
        {
            return _reservaRepo.GetAcompanantes(reservaId);
        }

        public int ProcesarNoShows()
        {
            return _reservaRepo.ProcesarNoShows();
        }
    }
}