using System;
using System.Collections.Generic;
using System.Linq;

namespace BE_08YS
{
    /// <summary>Estado de un servicio en el reporte de servicios consumidos.</summary>
    public enum EstadoServicio
    {
        Solicitado = 0,   // pedido cargado, todavía no entregado
        Entregado = 1,    // pedido entregado en la habitación
        Completado = 2    // servicio que se cumple al registrarse (upgrade)
    }

    /// <summary>
    /// Un servicio consumido por un huésped durante su estadía (un renglón del reporte).
    /// Sale de un cargo adicional: pedido de room service o upgrade de habitación.
    /// </summary>
    public class ServicioConsumido_68SA
    {
        #region Propiedades

        public int CargoId { get; set; }
        public int ReservaId { get; set; }
        public int HuespedId { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        /// <summary>Habitación en la que estaba el huésped al consumir (en un upgrade, la nueva).</summary>
        public string NroHabitacion { get; set; }
        public TipoCargo Tipo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        /// <summary>Unidades: productos del pedido o noches del upgrade.</summary>
        public int Cantidad { get; set; }
        /// <summary>Detalle del pedido ("2 x Club sándwich, 1 x Agua mineral"); vacío en un upgrade.</summary>
        public string Detalle { get; set; }
        public decimal Monto { get; set; }
        public EstadoServicio Estado { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int? PedidoId { get; set; }
        public string Usuario { get; set; }

        // Solo upgrade
        public string HabitacionAnterior { get; set; }
        public string TipoHabitacionNueva { get; set; }
        public decimal? DiferenciaTarifa { get; set; }

        public string NombreCompleto => $"{Apellido}, {Nombre}".Trim(' ', ',');

        /// <summary>Minutos entre el pedido y la entrega (solo room service entregado).</summary>
        public double? MinutosEntrega => FechaEntrega.HasValue ? (FechaEntrega.Value - Fecha).TotalMinutes : (double?)null;

        #endregion
    }

    /// <summary>Servicios de un huésped, con su total acumulado.</summary>
    public class HuespedServicios_68SA
    {
        #region Propiedades

        public int HuespedId { get; set; }
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }
        public List<ServicioConsumido_68SA> Servicios { get; set; } = new List<ServicioConsumido_68SA>();

        public int Cantidad => Servicios.Count;
        public decimal Total => Servicios.Sum(s => s.Monto);
        public int Estadias => Servicios.Select(s => s.ReservaId).Distinct().Count();

        #endregion
    }

    /// <summary>Producto de la carta con lo que se pidió en el período.</summary>
    public class ProductoConsumido_68SA
    {
        #region Propiedades

        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }

        #endregion
    }

    /// <summary>Filtros del reporte (todos opcionales).</summary>
    public class FiltroServicios_68SA
    {
        #region Propiedades

        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public TipoCargo? Tipo { get; set; }
        public EstadoServicio? Estado { get; set; }
        /// <summary>Documento, nombre o apellido del huésped (contiene).</summary>
        public string Huesped { get; set; }

        public bool EsHistorico => !Desde.HasValue && !Hasta.HasValue;

        #endregion
    }

    /// <summary>
    /// Reporte de servicios consumidos por huésped: los renglones, agrupados por huésped,
    /// y los indicadores del período.
    /// </summary>
    public class ReporteServicios_68SA
    {
        #region Propiedades

        public FiltroServicios_68SA Filtro { get; set; } = new FiltroServicios_68SA();
        public DateTime Generado { get; set; } = DateTime.Now;
        public List<ServicioConsumido_68SA> Servicios { get; set; } = new List<ServicioConsumido_68SA>();
        public List<ProductoConsumido_68SA> Productos { get; set; } = new List<ProductoConsumido_68SA>();
        /// <summary>Alojamiento facturado por las reservas que ingresan en el período (para la participación).</summary>
        public decimal AlojamientoPeriodo { get; set; }

        public List<HuespedServicios_68SA> Huespedes =>
            Servicios.GroupBy(s => s.HuespedId)
                     .Select(g => new HuespedServicios_68SA
                     {
                         HuespedId = g.Key,
                         Documento = g.First().Documento,
                         NombreCompleto = g.First().NombreCompleto,
                         Servicios = g.OrderBy(s => s.Fecha).ThenBy(s => s.CargoId).ToList()
                     })
                     .OrderBy(h => h.NombreCompleto, StringComparer.CurrentCultureIgnoreCase)
                     .ThenBy(h => h.HuespedId)
                     .ToList();

        public bool Vacio => Servicios.Count == 0;
        public decimal Total => Servicios.Sum(s => s.Monto);
        public int Pendientes => Servicios.Count(s => s.Estado == EstadoServicio.Solicitado);
        public int CantidadHuespedes => Servicios.Select(s => s.HuespedId).Distinct().Count();
        public int CantidadEstadias => Servicios.Select(s => s.ReservaId).Distinct().Count();
        public decimal PromedioPorHuesped => CantidadHuespedes > 0 ? Math.Round(Total / CantidadHuespedes, 2) : 0;

        /// <summary>Peso de los servicios sobre lo facturado (alojamiento + servicios), en %.</summary>
        public decimal Participacion => AlojamientoPeriodo + Total > 0 ? Math.Round(Total / (AlojamientoPeriodo + Total) * 100, 1) : 0;

        public double? MinutosEntregaPromedio
        {
            get
            {
                var tiempos = Servicios.Where(s => s.MinutosEntrega.HasValue).Select(s => s.MinutosEntrega.Value).ToList();
                return tiempos.Count > 0 ? tiempos.Average() : (double?)null;
            }
        }

        public HuespedServicios_68SA MayorConsumo => Huespedes.OrderByDescending(h => h.Total).FirstOrDefault();

        #endregion

        #region Métodos públicos

        public int Cantidad(TipoCargo tipo) => Servicios.Count(s => s.Tipo == tipo);
        public decimal Monto(TipoCargo tipo) => Servicios.Where(s => s.Tipo == tipo).Sum(s => s.Monto);

        #endregion
    }
}
