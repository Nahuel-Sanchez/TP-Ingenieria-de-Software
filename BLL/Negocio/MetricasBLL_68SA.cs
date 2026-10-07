using BE_08YS;
using BE_08YS.Metricas;
using DAL_08YS.Interfaces_Repositories.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_08YS.Negocio
{
    public class MetricasBLL_68SA
    {
        #region Campos

        private readonly IMetricasRepository_68SA _repo;

        #endregion

        #region Constructor

        public MetricasBLL_68SA(IMetricasRepository_68SA repo)
        {
            _repo = repo;
        }

        #endregion

        #region Consultas

        public ResumenOcupacion_68SA GetResumenOcupacion() => _repo.GetResumenOcupacion();
        public List<OcupacionPorTipoItem_68SA> GetOcupacionPorTipo() => _repo.GetOcupacionPorTipo();
        public ResumenIngresos_68SA GetResumenIngresos(DateTime? desde, DateTime? hasta) => _repo.GetResumenIngresos(desde, hasta);
        public ResumenHuespedes_68SA GetResumenHuespedes(DateTime? desde, DateTime? hasta) => _repo.GetResumenHuespedes(desde, hasta);

        public MovimientoHoy_68SA GetMovimientoHoy() => _repo.GetMovimientoHoy();
        public List<OcupacionDia_68SA> GetOcupacionPrevista(int dias = 14) => _repo.GetOcupacionPrevista(dias);
        public List<PuntoSerie_68SA> GetIngresosPorDia(DateTime? desde, DateTime? hasta) => _repo.GetIngresosPorDia(desde, hasta);

        public ResumenServicios_68SA GetResumenServicios(DateTime? desde, DateTime? hasta) => _repo.GetResumenServicios(desde, hasta);
        public List<PuntoSerie_68SA> GetServiciosPorDia(DateTime? desde, DateTime? hasta) => _repo.GetServiciosPorDia(desde, hasta);
        public int[] GetPedidosPorHora(DateTime? desde, DateTime? hasta) => _repo.GetPedidosPorHora(desde, hasta);
        public List<ProductoConsumido_68SA> GetTopProductos(DateTime? desde, DateTime? hasta, int cantidad = 8) => _repo.GetTopProductos(desde, hasta, cantidad);
        public List<ValorEtiqueta_68SA> GetUpgradesPorTipo(DateTime? desde, DateTime? hasta) => _repo.GetUpgradesPorTipo(desde, hasta);
        /// <summary>Pedidos entregados según el tiempo de entrega: hasta 15, 30, 45, 60 minutos y más de una hora.</summary>
        public int[] GetTiemposEntrega(DateTime? desde, DateTime? hasta) => _repo.GetTiemposEntrega(desde, hasta);
        public List<PedidoPendiente_68SA> GetPedidosPendientes() => _repo.GetPedidosPendientes();
        public List<CambioHabitacionItem_68SA> GetCambiosHabitacion(DateTime? desde, DateTime? hasta, int cantidad = 8) => _repo.GetCambiosHabitacion(desde, hasta, cantidad);

        public IndicadoresPeriodo_68SA GetIndicadoresPeriodo(DateTime? desde, DateTime? hasta) => _repo.GetIndicadoresPeriodo(desde, hasta);
        public List<ValorEtiqueta_68SA> GetPagosPorMetodo(DateTime? desde, DateTime? hasta) => _repo.GetPagosPorMetodo(desde, hasta);
        /// <summary>Llegadas según el día de la semana del ingreso (0 = lunes ... 6 = domingo).</summary>
        public int[] GetLlegadasPorDiaSemana(DateTime? desde, DateTime? hasta) => _repo.GetLlegadasPorDiaSemana(desde, hasta);
        /// <summary>Por día desde hoy: Valor = llegadas previstas, Valor2 = salidas previstas.</summary>
        public List<PuntoSerie_68SA> GetMovimientoProximosDias(int dias = 7) => _repo.GetMovimientoProximosDias(dias);
        /// <summary>Por mes (los últimos meses incluido el actual): noches vendidas sobre noches disponibles.</summary>
        public List<OcupacionDia_68SA> GetOcupacionMensual(int meses = 12) => _repo.GetOcupacionMensual(meses);
        public List<ValorEtiqueta_68SA> GetHabitacionesMasOcupadas(int dias = 90, int cantidad = 8) => _repo.GetHabitacionesMasOcupadas(dias, cantidad);
        public List<ValorEtiqueta_68SA> GetRangoEtario(DateTime? desde, DateTime? hasta) => _repo.GetRangoEtario(desde, hasta);
        public List<ValorEtiqueta_68SA> GetComposicionGrupos(DateTime? desde, DateTime? hasta) => _repo.GetComposicionGrupos(desde, hasta);
        public List<HuespedDestacado_68SA> GetTopHuespedes(DateTime? desde, DateTime? hasta, int cantidad = 8) => _repo.GetTopHuespedes(desde, hasta, cantidad);

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Agrupa una serie diaria para que el gráfico no tenga más de ~45 barras:
        /// hasta 45 días por día, hasta ~10 meses por semana (lunes) y después por mes.
        /// </summary>
        public static List<PuntoSerie_68SA> Agrupar(List<PuntoSerie_68SA> diaria, out AgrupacionSerie agrupacion)
        {
            agrupacion = AgrupacionSerie.Dia;
            if (diaria == null || diaria.Count == 0) return new List<PuntoSerie_68SA>();

            int dias = (diaria.Max(p => p.Fecha) - diaria.Min(p => p.Fecha)).Days + 1;
            if (dias <= 45) return diaria;

            agrupacion = dias <= 300 ? AgrupacionSerie.Semana : AgrupacionSerie.Mes;
            var modo = agrupacion;
            return diaria
                .GroupBy(p => modo == AgrupacionSerie.Semana
                    ? p.Fecha.Date.AddDays(-(((int)p.Fecha.DayOfWeek + 6) % 7))
                    : new DateTime(p.Fecha.Year, p.Fecha.Month, 1))
                .Select(g => new PuntoSerie_68SA { Fecha = g.Key, Valor = g.Sum(p => p.Valor), Valor2 = g.Sum(p => p.Valor2) })
                .OrderBy(p => p.Fecha)
                .ToList();
        }

        #endregion
    }
}
