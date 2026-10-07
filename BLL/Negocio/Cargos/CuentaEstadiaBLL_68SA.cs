using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using System.Collections.Generic;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// Cuenta de la estadía: alojamiento (reservado + renovaciones) + cargos adicionales (upgrades y room service) - pagos.
    /// El check-out, el detalle de la estadía, la factura y el comprobante toman el saldo de acá:
    /// la regla está en un solo lugar.
    /// </summary>
    public class CuentaEstadiaBLL_68SA
    {
        #region Campos

        private readonly ICargoAdicionalRepository_68SA _cargoRepo;
        private readonly IPagoRepository_68SA _pagoRepo;
        private readonly IReservaRepository_68SA _reservaRepo;

        #endregion

        #region Constructor

        public CuentaEstadiaBLL_68SA(ICargoAdicionalRepository_68SA cargoRepo, IPagoRepository_68SA pagoRepo,
                                     IReservaRepository_68SA reservaRepo)
        {
            _cargoRepo = cargoRepo;
            _pagoRepo = pagoRepo;
            _reservaRepo = reservaRepo;
        }

        #endregion

        #region Consultas

        public List<CargoAdicional_68SA> GetCargos(int reservaId) => _cargoRepo.GetByReserva(reservaId);

        public decimal GetTotalCargos(int reservaId) => _cargoRepo.GetTotalByReserva(reservaId);

        /// <summary>
        /// Se lee la reserva completa por su id: las búsquedas rápidas (por documento) no traen el monto original,
        /// y sin él no se puede separar el recargo por renovación.
        /// </summary>
        public EstadoCuentaReserva_68SA GetEstadoCuenta(int reservaId)
        {
            var reserva = _reservaRepo.GetById(reservaId) ?? throw new ReservaNoEncontradaException_68SA();
            bool cancelada = reserva.Estado == EstadoReserva.Cancelada;

            return new EstadoCuentaReserva_68SA
            {
                ReservaId = reserva.Id,
                Alojamiento = cancelada ? 0 : reserva.MontoTotal,
                AlojamientoReservado = cancelada ? 0 : reserva.MontoOriginal,
                Cargos = _cargoRepo.GetByReserva(reserva.Id),
                TotalPagado = _pagoRepo.GetTotalPagado(reserva.Id)
            };
        }

        public EstadoCuentaReserva_68SA GetEstadoCuenta(Reserva_68SA reserva)
        {
            if (reserva == null) throw new ReservaNoEncontradaException_68SA();
            return GetEstadoCuenta(reserva.Id);
        }

        #endregion
    }
}
