using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using Service_08YS.Entities.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// Servicio a la habitación: la recepción carga el pedido a la cuenta de una estadía en curso
    /// y el personal de servicio confirma la entrega.
    /// Solo usa su propio repositorio (pedidos): la carta la pide a CatalogoRoomServiceBLL_68SA.
    /// </summary>
    public class RoomServiceBLL_68SA
    {
        #region Campos

        public const int MaximoPorItem = 20;

        private readonly IPedidoRoomServiceRepository_68SA _pedidoRepo;
        private readonly CatalogoRoomServiceBLL_68SA _catalogoBll;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        #endregion

        #region Constructor

        public RoomServiceBLL_68SA(IPedidoRoomServiceRepository_68SA pedidoRepo, CatalogoRoomServiceBLL_68SA catalogoBll,
                                   BitacoraBLL_08YS bitacoraBll)
        {
            _pedidoRepo = pedidoRepo;
            _catalogoBll = catalogoBll;
            _bitacoraBll = bitacoraBll;
        }

        #endregion

        #region Consultas

        /// <summary>Ítems que se pueden pedir (solo los activos).</summary>
        public List<ItemRoomService_68SA> GetCarta() => _catalogoBll.GetItems(incluirInactivos: false);

        public static decimal CalcularTotal(IEnumerable<DetallePedidoRoomService_68SA> renglones)
            => renglones?.Sum(r => r.Subtotal) ?? 0m;

        /// <summary>Pedidos (para la recepción o para el personal de servicio).</summary>
        public List<PedidoRoomService_68SA> GetPedidos(PedidoRoomServiceFiltro_68SA filtro)
        {
            ValidarPuedeVerPedidos();
            if (filtro?.FechaDesde != null && filtro.FechaHasta != null && filtro.FechaDesde.Value.Date > filtro.FechaHasta.Value.Date)
                throw new RangoFechasInvalidoException_68SA("La fecha desde no puede ser posterior a la fecha hasta.");
            if (filtro?.MontoDesde < 0 || filtro?.MontoHasta < 0)
                throw new DatosInvalidosException_68SA("El monto no puede ser negativo.");
            if (filtro?.MontoDesde != null && filtro.MontoHasta != null && filtro.MontoDesde.Value > filtro.MontoHasta.Value)
                throw new DatosInvalidosException_68SA("El monto desde no puede ser mayor que el monto hasta.");
            return _pedidoRepo.GetPedidos(filtro);
        }

        public List<PedidoRoomService_68SA> GetPendientes()
            => GetPedidos(new PedidoRoomServiceFiltro_68SA { Estado = EstadoPedidoRoomService.Pendiente });

        public List<PedidoRoomService_68SA> GetPorReserva(int reservaId)
            => _pedidoRepo.GetPedidos(new PedidoRoomServiceFiltro_68SA { ReservaId = reservaId });

        public List<DetallePedidoRoomService_68SA> GetDetalle(int pedidoId) => _pedidoRepo.GetDetalle(pedidoId);

        #endregion

        #region Operaciones

        /// <summary>
        /// Registra el pedido y su cargo. Devuelve el número de pedido.
        /// Lo que depende solo de la solicitud (permiso, ítems y cantidades) se valida acá, sin ir a la base.
        /// Lo que depende del estado de la base (estadía En Curso, ítems activos en la carta) lo valida
        /// sp_RegistrarPedidoRoomService en la misma transacción en la que registra, con los datos del momento:
        /// una sola ida a la base y sin carrera entre la validación y el alta.
        /// </summary>
        public int RegistrarPedido(SolicitudPedidoRoomService_68SA solicitud)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.RegistrarRoomService);
            ValidarSolicitud(solicitud);

            var resultado = _pedidoRepo.Registrar(solicitud, SessionManager_08YS.Instance.Current?.DNI, out int pedidoId);
            LanzarSiFallo(resultado);

            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.PedidoRoomServiceRegistrado, targetUsername: solicitud.ReservaId.ToString());
            return pedidoId;
        }

        /// <summary>El personal de servicio marca el pedido como entregado (queda la hora de entrega).</summary>
        public void ConfirmarEntrega(PedidoRoomService_68SA pedido)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.ConfirmarEntregaRoomService);
            if (pedido == null) throw new ArgumentNullException(nameof(pedido));
            if (pedido.Estado != EstadoPedidoRoomService.Pendiente || !_pedidoRepo.ConfirmarEntrega(pedido.Id))
                throw new PedidoNoPendienteException_68SA();

            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.PedidoRoomServiceEntregado, targetUsername: pedido.ReservaId.ToString());
        }

        #endregion

        #region Validaciones

        private static void ValidarSolicitud(SolicitudPedidoRoomService_68SA solicitud)
        {
            if (solicitud == null || solicitud.CantidadesPorItem.Count == 0)
                throw new PedidoRoomServiceInvalidoException_68SA("Agregá al menos un ítem al pedido.");
            if (solicitud.CantidadesPorItem.Values.Any(c => c <= 0 || c > MaximoPorItem))
                throw new PedidoRoomServiceInvalidoException_68SA($"La cantidad de cada ítem debe estar entre 1 y {MaximoPorItem}.");
        }

        // Traduce el resultado del SP a la excepción de negocio que muestra la pantalla
        private static void LanzarSiFallo(ResultadoPedidoRoomService resultado)
        {
            switch (resultado)
            {
                case ResultadoPedidoRoomService.Ok: return;
                case ResultadoPedidoRoomService.ReservaNoEnCurso:
                    throw new EstadoReservaInvalidoException_68SA("La estadía ya no está En Curso.");
                default:
                    throw new PedidoRoomServiceInvalidoException_68SA();
            }
        }

        private static void ValidarPuedeVerPedidos()
        {
            var sesion = SessionManager_08YS.Instance;
            if (!sesion.HasPermission(Permisos.ConfirmarEntregaRoomService) && !sesion.HasPermission(Permisos.RegistrarRoomService))
                sesion.ValidatePermission(Permisos.ConfirmarEntregaRoomService); // lanza el UnauthorizedAccessException estándar
        }

        #endregion
    }
}
