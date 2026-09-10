using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
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

        public PagoBLL_68SA(IPagoRepository_68SA pagoRepo)
        {
            _pagoRepo = pagoRepo;
        }

        public int Registrar(Pago_68SA pago)
        {
            if (pago.Monto <= 0)
                throw new ArgumentException("El monto del pago debe ser mayor a cero.");

            pago.FechaPago = DateTime.Now;
            return _pagoRepo.Registrar(pago);
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
