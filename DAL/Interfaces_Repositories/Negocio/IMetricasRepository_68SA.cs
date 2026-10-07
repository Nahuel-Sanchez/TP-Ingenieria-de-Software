using BE_08YS;
using BE_08YS.Metricas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.Interfaces_Repositories.Negocio
{
    public interface IMetricasRepository_68SA
    {
        #region Consultas

        ResumenOcupacion_68SA GetResumenOcupacion();
        List<OcupacionPorTipoItem_68SA> GetOcupacionPorTipo();
        ResumenIngresos_68SA GetResumenIngresos(DateTime? desde, DateTime? hasta); 
        ResumenHuespedes_68SA GetResumenHuespedes(DateTime? desde, DateTime? hasta);

        // PN1: movimiento del día, ocupación prevista e ingresos por día
        MovimientoHoy_68SA GetMovimientoHoy();
        List<OcupacionDia_68SA> GetOcupacionPrevista(int dias);
        List<PuntoSerie_68SA> GetIngresosPorDia(DateTime? desde, DateTime? hasta);

        // PN2: servicios adicionales
        ResumenServicios_68SA GetResumenServicios(DateTime? desde, DateTime? hasta);
        List<PuntoSerie_68SA> GetServiciosPorDia(DateTime? desde, DateTime? hasta);
        int[] GetPedidosPorHora(DateTime? desde, DateTime? hasta);
        List<ProductoConsumido_68SA> GetTopProductos(DateTime? desde, DateTime? hasta, int cantidad);
        List<ValorEtiqueta_68SA> GetUpgradesPorTipo(DateTime? desde, DateTime? hasta);
        int[] GetTiemposEntrega(DateTime? desde, DateTime? hasta);
        List<PedidoPendiente_68SA> GetPedidosPendientes();
        List<CambioHabitacionItem_68SA> GetCambiosHabitacion(DateTime? desde, DateTime? hasta, int cantidad);

        // Indicadores ampliados del dashboard
        IndicadoresPeriodo_68SA GetIndicadoresPeriodo(DateTime? desde, DateTime? hasta);
        List<ValorEtiqueta_68SA> GetPagosPorMetodo(DateTime? desde, DateTime? hasta);
        int[] GetLlegadasPorDiaSemana(DateTime? desde, DateTime? hasta);
        List<PuntoSerie_68SA> GetMovimientoProximosDias(int dias);
        List<OcupacionDia_68SA> GetOcupacionMensual(int meses);
        List<ValorEtiqueta_68SA> GetHabitacionesMasOcupadas(int dias, int cantidad);
        List<ValorEtiqueta_68SA> GetRangoEtario(DateTime? desde, DateTime? hasta);
        List<ValorEtiqueta_68SA> GetComposicionGrupos(DateTime? desde, DateTime? hasta);
        List<HuespedDestacado_68SA> GetTopHuespedes(DateTime? desde, DateTime? hasta, int cantidad);

        #endregion
    }
}
