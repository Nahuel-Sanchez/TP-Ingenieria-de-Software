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
    /// </summary>
    public class RoomServiceBLL_68SA
    {
        #region Campos

        public const int MaximoPorItem = 20;
        private const int LargoDescripcion = 200;

        private readonly IPedidoRoomServiceRepository_68SA _pedidoRepo;
        private readonly ICatalogoRoomServiceRepository_68SA _catalogoRepo;
        private readonly IReservaRepository_68SA _reservaRepo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        #endregion

        #region Constructor

        public RoomServiceBLL_68SA(IPedidoRoomServiceRepository_68SA pedidoRepo, ICatalogoRoomServiceRepository_68SA catalogoRepo,
                                   IReservaRepository_68SA reservaRepo, BitacoraBLL_08YS bitacoraBll)
        {
            _pedidoRepo = pedidoRepo;
            _catalogoRepo = catalogoRepo;
            _reservaRepo = reservaRepo;
            _bitacoraBll = bitacoraBll;
        }

        #endregion

        #region Consultas

        /// <summary>Ítems que se pueden pedir (solo los activos).</summary>
        public List<ItemRoomService_68SA> GetCarta() => _catalogoRepo.GetItems(incluirInactivos: false);

        public static decimal CalcularTotal(IEnumerable<DetallePedidoRoomService_68SA> renglones)
            => renglones?.Sum(r => r.Subtotal) ?? 0m;

        /// <summary>Pedidos (para la recepción o para el personal de servicio).</summary>
        public List<PedidoRoomService_68SA> GetPedidos(PedidoRoomServiceFiltro_68SA filtro)
        {
            ValidarPuedeVerPedidos();
            if (filtro?.FechaDesde != null && filtro.FechaHasta != null && filtro.FechaDesde.Value.Date > filtro.FechaHasta.Value.Date)
                throw new RangoFechasInvalidoException_68SA("La fecha desde no puede ser posterior a la fecha hasta.");
            return _pedidoRepo.GetPedidos(filtro);
        }

        public List<PedidoRoomService_68SA> GetPendientes()
            => GetPedidos(new PedidoRoomServiceFiltro_68SA { Estado = EstadoPedidoRoomService.Pendiente });

        public List<PedidoRoomService_68SA> GetPorReserva(int reservaId)
            => _pedidoRepo.GetPedidos(new PedidoRoomServiceFiltro_68SA { ReservaId = reservaId });

        public List<DetallePedidoRoomService_68SA> GetDetalle(int pedidoId) => _pedidoRepo.GetDetalle(pedidoId);

        #endregion

        #region Operaciones

        /// <summary>Registra el pedido y su cargo. Devuelve el número de pedido.</summary>
        public int RegistrarPedido(SolicitudPedidoRoomService_68SA solicitud)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.RegistrarRoomService);
            if (solicitud == null || solicitud.CantidadesPorItem.Count == 0)
                throw new PedidoRoomServiceInvalidoException_68SA("Agregá al menos un ítem al pedido.");
            if (solicitud.CantidadesPorItem.Values.Any(c => c <= 0 || c > MaximoPorItem))
                throw new PedidoRoomServiceInvalidoException_68SA($"La cantidad de cada ítem debe estar entre 1 y {MaximoPorItem}.");

            var reserva = _reservaRepo.GetById(solicitud.ReservaId) ?? throw new ReservaNoEncontradaException_68SA();
            if (reserva.Estado != EstadoReserva.EnCurso)
                throw new EstadoReservaInvalidoException_68SA("Solo se puede cargar servicio a la habitación a una estadía En Curso.");

            var carta = GetCarta().ToDictionary(i => i.Id);
            if (solicitud.CantidadesPorItem.Keys.Any(id => !carta.ContainsKey(id)))
                throw new PedidoRoomServiceInvalidoException_68SA();

            string descripcion = ArmarDescripcion(solicitud, carta);
            var resultado = _pedidoRepo.Registrar(solicitud, descripcion, SessionManager_08YS.Instance.Current?.DNI, out int pedidoId);

            switch (resultado)
            {
                case ResultadoPedidoRoomService.Ok: break;
                case ResultadoPedidoRoomService.ReservaNoEnCurso:
                    throw new EstadoReservaInvalidoException_68SA("La estadía ya no está En Curso.");
                default:
                    throw new PedidoRoomServiceInvalidoException_68SA();
            }

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

        private static void ValidarPuedeVerPedidos()
        {
            var sesion = SessionManager_08YS.Instance;
            if (!sesion.HasPermission(Permisos.ConfirmarEntregaRoomService) && !sesion.HasPermission(Permisos.RegistrarRoomService))
                sesion.ValidatePermission(Permisos.ConfirmarEntregaRoomService); // lanza el UnauthorizedAccessException estándar
        }

        #endregion

        #region Métodos auxiliares

        // "Room service: 2 x Club sándwich, 1 x Agua mineral 500 ml" (entra en los 200 caracteres del cargo)
        private static string ArmarDescripcion(SolicitudPedidoRoomService_68SA solicitud, IDictionary<int, ItemRoomService_68SA> carta)
        {
            var renglones = solicitud.CantidadesPorItem
                .OrderBy(r => carta[r.Key].Nombre)
                .Select(r => $"{r.Value} x {carta[r.Key].Nombre}");
            string texto = "Room service: " + string.Join(", ", renglones);
            return texto.Length <= LargoDescripcion ? texto : texto.Substring(0, LargoDescripcion - 1) + "…";
        }

        #endregion
    }
}
