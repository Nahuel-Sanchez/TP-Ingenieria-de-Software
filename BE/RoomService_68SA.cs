using System;
using System.Collections.Generic;
using System.Linq;

namespace BE_08YS
{
    /// <summary>Ítem de la carta de servicio a la habitación. Se da de baja lógicamente (Activo = false).</summary>
    public class ItemRoomService_68SA
    {
        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool Activo { get; set; } = true;

        #endregion
    }

    public enum EstadoPedidoRoomService
    {
        Pendiente = 0,
        Entregado = 1
    }

    /// <summary>Renglón de un pedido. El precio queda fijado al momento de pedir.</summary>
    public class DetallePedidoRoomService_68SA
    {
        #region Propiedades

        public int ItemId { get; set; }
        public string NombreItem { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;

        #endregion
    }

    /// <summary>
    /// Pedido de servicio a la habitación. Cada pedido genera un cargo (CargoId) en la cuenta de la reserva;
    /// el huésped que lo recibe es el titular de esa reserva.
    /// </summary>
    public class PedidoRoomService_68SA
    {
        #region Propiedades

        public int Id { get; set; }
        public int CargoId { get; set; }
        public int ReservaId { get; set; }
        public string NroHabitacion { get; set; }
        public string Huesped { get; set; }          // titular de la reserva (solo para mostrar)
        public string Descripcion { get; set; }
        public DateTime FechaHoraPedido { get; set; }
        public decimal Monto { get; set; }
        public EstadoPedidoRoomService Estado { get; set; }
        public DateTime? FechaHoraEntrega { get; set; }
        public string UsuarioRegistroNombre { get; set; }

        public List<DetallePedidoRoomService_68SA> Detalle { get; set; } = new List<DetallePedidoRoomService_68SA>();

        #endregion

        #region Métodos públicos

        /// <summary>Minutos que lleva esperando (pendiente) o que tardó en entregarse.</summary>
        public int MinutosEspera(DateTime ahora)
            => (int)Math.Max(0, ((FechaHoraEntrega ?? ahora) - FechaHoraPedido).TotalMinutes);

        #endregion
    }

    public class PedidoRoomServiceFiltro_68SA
    {
        #region Propiedades

        public EstadoPedidoRoomService? Estado { get; set; }
        public int? ReservaId { get; set; }
        public string NroHabitacion { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        /// <summary>Solo los entregados desde esta fecha (por hora de entrega).</summary>
        public DateTime? EntregadosDesde { get; set; }

        #endregion
    }

    /// <summary>Resultado de sp_RegistrarPedidoRoomService.</summary>
    public enum ResultadoPedidoRoomService
    {
        Ok = 0,
        ReservaNoEnCurso = 1,
        ItemsInvalidos = 2
    }

    /// <summary>Ítems y cantidades que se le piden a la cocina (lo que el SP recibe como parámetro tabla).</summary>
    public class SolicitudPedidoRoomService_68SA
    {
        #region Propiedades

        public int ReservaId { get; set; }
        public Dictionary<int, int> CantidadesPorItem { get; } = new Dictionary<int, int>();

        public int TotalUnidades => CantidadesPorItem.Values.Sum();

        #endregion
    }
}
