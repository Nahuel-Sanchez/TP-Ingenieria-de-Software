using BE_08YS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    public interface IReservaRepository_68SA
    {
        #region Consultas

        List<Reserva_68SA> GetAll(ReservaFiltro_68SA filtro);

        Reserva_68SA GetById(int reservaId);
        Reserva_68SA GetPorDocumentoTitularYEstado(string documento, EstadoReserva estado);
        Reserva_68SA GetConfirmadaHoyPorHabitacion(int habitacionId);
        List<Reserva_68SA> GetEnRangoVisible(DateTime desde, DateTime hasta);
        List<Huesped_68SA> GetAcompanantes(int reservaId);
        Reserva_68SA GetEnCursoPorHabitacion(int habitacionId);

        #endregion

        #region Operaciones

        // Devuelve el ReservaID nuevo, o -1 si la habitación dejó de estar disponible
        int Crear(Reserva_68SA reserva);

        // Devuelven false si la reserva no estaba en el estado correcto para la operación
        bool RegistrarCheckIn(int reservaId, DateTime fechaHora);
        bool RegistrarCheckOut(int reservaId, DateTime fechaHora);
        bool Cancelar(int reservaId);

        void AgregarAcompanante(int reservaId, int huespedId);

        int ProcesarNoShows();

        bool ExtenderEstadia(int reservaId, DateTime nuevaFechaEgreso);

        bool Modificar(int reservaId, DateTime fechaIngreso, DateTime fechaEgreso, int adultos, int ninos);

        #endregion
    }
}
