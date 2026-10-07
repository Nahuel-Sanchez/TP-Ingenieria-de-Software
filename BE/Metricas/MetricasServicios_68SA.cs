using System;
using System.Collections.Generic;

namespace BE_08YS.Metricas
{
    /// <summary>Indicadores de los servicios adicionales (PN2) de un período.</summary>
    public class ResumenServicios_68SA
    {
        #region Propiedades

        public decimal MontoRoomService { get; set; }
        public decimal MontoUpgrades { get; set; }
        public int Pedidos { get; set; }
        public int PedidosPendientes { get; set; }
        public int Upgrades { get; set; }
        public int Reubicaciones { get; set; }
        public double? MinutosEntregaPromedio { get; set; }
        /// <summary>Estadías (en curso o finalizadas) que se superponen con el período.</summary>
        public int EstadiasPeriodo { get; set; }
        /// <summary>De esas estadías, cuántas tuvieron algún servicio en el período.</summary>
        public int EstadiasConConsumo { get; set; }

        public decimal Total => MontoRoomService + MontoUpgrades;
        public decimal TicketPromedioPedido => Pedidos > 0 ? Math.Round(MontoRoomService / Pedidos, 2) : 0;
        public decimal PorcentajeEstadiasConConsumo => EstadiasPeriodo > 0 ? Math.Round((decimal)EstadiasConConsumo / EstadiasPeriodo * 100, 1) : 0;

        #endregion
    }

    /// <summary>Cómo se agrupan los puntos de una serie por fecha en los gráficos.</summary>
    public enum AgrupacionSerie { Dia, Semana, Mes }

    /// <summary>Valor de una serie en una fecha (hasta dos series apiladas: Valor y Valor2).</summary>
    public class PuntoSerie_68SA
    {
        #region Propiedades

        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
        public decimal Valor2 { get; set; }

        #endregion
    }

    /// <summary>Ocupación prevista de una noche: habitaciones con estadía o reserva sobre las operativas.</summary>
    public class OcupacionDia_68SA
    {
        #region Propiedades

        public DateTime Fecha { get; set; }
        public int Ocupadas { get; set; }
        public int Total { get; set; }
        public decimal Porcentaje => Total > 0 ? Math.Round((decimal)Ocupadas / Total * 100, 1) : 0;

        #endregion
    }

    /// <summary>Movimiento de recepción del día.</summary>
    public class MovimientoHoy_68SA
    {
        #region Propiedades

        public int Llegadas { get; set; }
        public int LlegadasPendientes { get; set; }
        public int Salidas { get; set; }
        public int SalidasPendientes { get; set; }
        public int EstadiasEnCurso { get; set; }
        public int HuespedesAlojados { get; set; }
        /// <summary>Tarifa promedio por noche de las estadías en curso (ADR).</summary>
        public decimal TarifaPromedio { get; set; }

        #endregion
    }

    /// <summary>Indicadores hoteleros de un período: noches vendidas, tarifa promedio (ADR), RevPAR y perfil de las reservas.</summary>
    public class IndicadoresPeriodo_68SA
    {
        #region Propiedades

        public int NochesVendidas { get; set; }
        public decimal IngresoAlojamiento { get; set; }
        public int Dias { get; set; }
        public int HabitacionesOperativas { get; set; }
        /// <summary>Reservas (no anuladas) con ingreso dentro del período.</summary>
        public int Reservas { get; set; }
        public double? AnticipacionPromedioDias { get; set; }
        public double? EstadiaPromedioNoches { get; set; }
        public double? PersonasPorReserva { get; set; }
        public double? PorcentajeConNinos { get; set; }

        public int NochesDisponibles => Dias * HabitacionesOperativas;
        /// <summary>Tarifa promedio por noche vendida (ADR).</summary>
        public decimal TarifaPromedio => NochesVendidas > 0 ? Math.Round(IngresoAlojamiento / NochesVendidas, 2) : 0;
        /// <summary>Ingreso por habitación disponible (RevPAR).</summary>
        public decimal RevPar => NochesDisponibles > 0 ? Math.Round(IngresoAlojamiento / NochesDisponibles, 2) : 0;
        public decimal OcupacionPorcentaje => NochesDisponibles > 0 ? Math.Round((decimal)NochesVendidas / NochesDisponibles * 100, 1) : 0;

        #endregion
    }

    /// <summary>Valor genérico con etiqueta (para gráficos y donuts): monto, segundo valor opcional y cantidad.</summary>
    public class ValorEtiqueta_68SA
    {
        #region Propiedades

        public string Etiqueta { get; set; }
        public decimal Valor { get; set; }
        public decimal Valor2 { get; set; }
        public int Cantidad { get; set; }

        #endregion
    }

    /// <summary>Huésped titular con lo que gastó en las reservas del período.</summary>
    public class HuespedDestacado_68SA
    {
        #region Propiedades

        public string Nombre { get; set; }
        public int Estadias { get; set; }
        public int Noches { get; set; }
        public decimal MontoAlojamiento { get; set; }
        public decimal MontoServicios { get; set; }
        public decimal Total => MontoAlojamiento + MontoServicios;

        #endregion
    }

    /// <summary>Pedido de room service que todavía no se entregó.</summary>
    public class PedidoPendiente_68SA
    {
        #region Propiedades

        public int PedidoID { get; set; }
        public string NroHabitacion { get; set; }
        public int Unidades { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaHora { get; set; }

        #endregion
    }

    /// <summary>Cambio de habitación (upgrade o reubicación administrativa).</summary>
    public class CambioHabitacionItem_68SA
    {
        #region Propiedades

        public DateTime FechaCambio { get; set; }
        public int ReservaID { get; set; }
        public string HabitacionAnterior { get; set; }
        public string HabitacionNueva { get; set; }
        public string TipoNuevo { get; set; }
        /// <summary>0 = upgrade, 1 = cambio administrativo.</summary>
        public int Motivo { get; set; }
        public decimal Monto { get; set; }

        #endregion
    }
}
