using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using Service_08YS.Entities.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    public class PagoBLL_68SA
    {
        private readonly IPagoRepository_68SA _pagoRepo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        public PagoBLL_68SA(IPagoRepository_68SA pagoRepo, BitacoraBLL_08YS bitacoraBll)
        {
            _pagoRepo = pagoRepo;
            _bitacoraBll = bitacoraBll;
        }

        public int Registrar(Pago_68SA pago)
        {
            if (pago.Monto <= 0)
                throw new ArgumentException("El monto del pago debe ser mayor a cero.");

            pago.FechaPago = DateTime.Now;
            int nuevoId = _pagoRepo.Registrar(pago);

            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.PagoRegistrado, targetUsername: pago.ReservaId.ToString());

            return nuevoId;
        }

        public List<Pago_68SA> GetByReserva(int reservaId)
        {
            return _pagoRepo.GetByReserva(reservaId);
        }

        public decimal GetTotalPagado(int reservaId)
        {
            return _pagoRepo.GetTotalPagado(reservaId);
        }
    }
}