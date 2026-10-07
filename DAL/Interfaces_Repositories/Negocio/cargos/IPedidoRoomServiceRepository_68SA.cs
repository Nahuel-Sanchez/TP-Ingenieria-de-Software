using BE_08YS;
using System.Collections.Generic;

namespace DAL_08YS.Interfaces_Repositories.Negocio.cargos
{
    /// <summary>Pedidos de servicio a la habitación: alta (con su cargo), consulta y confirmación de entrega.</summary>
    public interface IPedidoRoomServiceRepository_68SA
    {
        #region Consultas

        List<PedidoRoomService_68SA> GetPedidos(PedidoRoomServiceFiltro_68SA filtro);
        List<DetallePedidoRoomService_68SA> GetDetalle(int pedidoId);

        #endregion

        #region Operaciones

        /// <summary>Registra pedido + detalle + cargo en una transacción (sp_RegistrarPedidoRoomService).</summary>
        ResultadoPedidoRoomService Registrar(SolicitudPedidoRoomService_68SA solicitud, string descripcion,
                                             int? usuarioDni, out int pedidoId);

        /// <summary>false si el pedido no existe o ya estaba entregado.</summary>
        bool ConfirmarEntrega(int pedidoId);

        #endregion
    }
}
