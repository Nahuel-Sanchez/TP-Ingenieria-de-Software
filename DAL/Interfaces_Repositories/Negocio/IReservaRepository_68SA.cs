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
        int Crear(Reserva_68SA reserva);
        Reserva_68SA GetById(int reservaId);
        Reserva_68SA GetConfirmadaPorDocumentoTitular(string documento);
        bool ExisteSolapamiento(int habitacionId, DateTime fechaIngreso, DateTime fechaEgreso);

        void RegistrarCheckIn(int reservaId, DateTime fechaHora);
        void RegistrarCheckOut(int reservaId, DateTime fechaHora);
        void Cancelar(int reservaId);

        void AgregarAcompanante(int reservaId, int huespedId);
        List<Huesped_68SA> GetAcompanantes(int reservaId);
    }
}
