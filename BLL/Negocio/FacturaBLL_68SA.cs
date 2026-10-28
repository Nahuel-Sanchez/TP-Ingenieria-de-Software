using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using Service_08YS.Entities.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_08YS;
using Service_08YS.Entities.Acceso;

namespace BLL_08YS.Negocio
{
    public class FacturaBLL_68SA
    {
        #region Campos

        private readonly IFacturaRepository_68SA _facturaRepo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        #endregion

        #region Constructor

        public FacturaBLL_68SA(IFacturaRepository_68SA facturaRepo, BitacoraBLL_08YS bitacoraBll)
        {
            _facturaRepo = facturaRepo;
            _bitacoraBll = bitacoraBll;
        }

        #endregion

        #region Consultas

        public List<Factura_68SA> GetByReserva(int reservaId)
        {
            return _facturaRepo.GetByReserva(reservaId);
        }

        public decimal GetTotalPagado(int reservaId)
        {
            return _facturaRepo.GetTotalPagado(reservaId);
        }

        #endregion

        #region Operaciones

        public int Registrar(Factura_68SA factura)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.Cobrar);

            if (factura.Monto <= 0)
                throw new ArgumentException("El monto de la factura debe ser mayor a cero.");

            factura.FechaEmision = DateTime.Now;
            int nuevoId = _facturaRepo.Registrar(factura);

            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.FacturaRegistrada, targetUsername: factura.ReservaId.ToString());

            return nuevoId;
        }

        #endregion
    }
}