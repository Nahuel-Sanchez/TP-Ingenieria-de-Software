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
        // Devuelve el ReservaID nuevo, o -1 si la habitación dejó de estar disponible
        int Crear(Reserva_68SA reserva);

        Reserva_68SA GetById(int reservaId);
        Reserva_68SA GetConfirmadaPorDocumentoTitular(string documento);

        // Devuelven false si la reserva no estaba en el estado correcto para la operación
        bool RegistrarCheckIn(int reservaId, DateTime fechaHora);
        bool RegistrarCheckOut(int reservaId, DateTime fechaHora);
        bool Cancelar(int reservaId);

        void AgregarAcompanante(int reservaId, int huespedId);
        List<Huesped_68SA> GetAcompanantes(int reservaId);
    }
}
