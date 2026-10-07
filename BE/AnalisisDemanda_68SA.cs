using System;
using System.Collections.Generic;

namespace BE_08YS
{
    // ==============================================================================================
    // Reporte inteligente "Análisis predictivo de ocupación y demanda".
    // Cruza reservas, estadías y facturación para proyectar la ocupación de los próximos días,
    // detectar períodos de baja ocupación o saturación y proponer precios y promociones.
    // ==============================================================================================

    public enum TemporadaDemanda { Baja = 0, Media = 1, Alta = 2 }

    public enum TipoAlertaDemanda { BajaOcupacion = 0, Saturacion = 1, Feriado = 2 }

    /// <summary>Sugerencia de precio / promoción para un día proyectado.</summary>
    public enum SugerenciaPrecio
    {
        DescuentoFuerte = 0,     // ocupación muy baja: descuento y paquete
        Promocion = 1,           // baja: promoción combinada estadía + servicios
        Mantener = 2,            // normal
        Aumento = 3,             // alta demanda
        AumentoFuerte = 4        // saturación: subir tarifa y pedir estadía mínima
    }

    /// <summary>Tipo de oferta personalizada para un huésped frecuente.</summary>
    public enum TipoOfertaHuesped
    {
        UpgradeCortesia = 0,       // siempre elige el mismo tipo: invitarlo a probar el superior
        PaqueteServicios = 1,      // consume mucho room service: paquete estadía + consumos
        DescuentoFidelidad = 2,    // viene en temporada baja: descuento para que repita
        PreventaTemporada = 3      // viene en temporada alta: reservar antes con precio asegurado
    }

    /// <summary>Parámetros del análisis.</summary>
    public class FiltroAnalisisDemanda_68SA
    {
        #region Campos

        /// <summary>Fecha desde la que se piden los datos cuando se usa toda la historia.</summary>
        public static readonly DateTime InicioHistoriaCompleta = new DateTime(1900, 1, 1);

        #endregion

        #region Propiedades

        /// <summary>Primer día proyectado (normalmente hoy).</summary>
        public DateTime Desde { get; set; } = DateTime.Today;
        /// <summary>Cantidad de días a proyectar.</summary>
        public int DiasHorizonte { get; set; } = 30;
        /// <summary>Meses de historia que se usan para aprender el comportamiento.</summary>
        public int MesesHistorial { get; set; } = 12;
        /// <summary>Usa toda la historia del sistema (MesesHistorial no se tiene en cuenta).</summary>
        public bool HistorialCompleto { get; set; }
        /// <summary>Tipo de habitación a analizar (null = todo el hotel).</summary>
        public int? TipoHabitacionId { get; set; }

        public DateTime Hasta => Desde.Date.AddDays(Math.Max(1, DiasHorizonte) - 1);
        public DateTime HistorialDesde => HistorialCompleto ? InicioHistoriaCompleta : Desde.Date.AddMonths(-Math.Max(1, MesesHistorial));

        #endregion
    }

    #region Datos de entrada (los lee la DAL)

    public class TipoHabitacionDemanda_68SA
    {
        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Tarifa { get; set; }
        public int Capacidad { get; set; }
        /// <summary>Habitaciones del tipo que se pueden vender hoy (no fuera de servicio): capacidad a futuro.</summary>
        public int Habitaciones { get; set; }
        /// <summary>Todas las habitaciones del tipo: capacidad para medir la ocupación pasada.</summary>
        public int HabitacionesTotales { get; set; }

        #endregion
    }

    public class ReservaDemanda_68SA
    {
        #region Propiedades

        public int Id { get; set; }
        public int HuespedId { get; set; }
        public string Huesped { get; set; }
        public string Documento { get; set; }
        public int TipoHabitacionId { get; set; }
        public DateTime Registro { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime Egreso { get; set; }
        public EstadoReserva Estado { get; set; }
        public decimal TarifaNoche { get; set; }
        public decimal Monto { get; set; }
        public int Personas { get; set; }

        public int Noches => Math.Max(1, (Egreso.Date - Ingreso.Date).Days);
        /// <summary>Días entre que se hizo la reserva y la llegada.</summary>
        public int Anticipacion => Math.Max(0, (Ingreso.Date - Registro.Date).Days);
        public bool Cancelada => Estado == EstadoReserva.Cancelada;

        #endregion

        #region Métodos públicos

        public bool Ocupa(DateTime noche) => !Cancelada && noche >= Ingreso.Date && noche < Egreso.Date;

        #endregion
    }

    public class CargoDemanda_68SA
    {
        #region Propiedades

        public int ReservaId { get; set; }
        public TipoCargo Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        #endregion
    }

    public class ProductoDemanda_68SA
    {
        #region Propiedades

        public int ReservaId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        #endregion
    }

    public class DatosDemanda_68SA
    {
        #region Propiedades

        public List<TipoHabitacionDemanda_68SA> Tipos { get; set; } = new List<TipoHabitacionDemanda_68SA>();
        public List<ReservaDemanda_68SA> Reservas { get; set; } = new List<ReservaDemanda_68SA>();
        public List<CargoDemanda_68SA> Cargos { get; set; } = new List<CargoDemanda_68SA>();
        public List<ProductoDemanda_68SA> Productos { get; set; } = new List<ProductoDemanda_68SA>();

        #endregion
    }

    #endregion

    #region Resultado

    /// <summary>Un día del período proyectado (renglón de la tabla del reporte).</summary>
    public class DiaProyectado_68SA
    {
        #region Propiedades

        public DateTime Fecha { get; set; }
        public int Capacidad { get; set; }
        /// <summary>Habitaciones ya reservadas para esa noche (reservas confirmadas o en curso).</summary>
        public int Reservadas { get; set; }
        /// <summary>Reservadas que se espera que se mantengan (descontando la tasa de cancelación).</summary>
        public decimal ReservadasNetas { get; set; }
        /// <summary>Reservas que se espera que todavía entren, según la anticipación habitual.</summary>
        public decimal ReservasPorEntrar { get; set; }
        public decimal OcupacionEstimada { get; set; }
        public decimal Porcentaje => Capacidad > 0 ? Math.Round(OcupacionEstimada / Capacidad * 100m, 1) : 0m;
        public decimal PorcentajeReservado => Capacidad > 0 ? Math.Round(Reservadas / (decimal)Capacidad * 100m, 1) : 0m;

        public string TipoMasDemandado { get; set; }
        public decimal PorcentajeTipoMasDemandado { get; set; }
        public TemporadaDemanda Temporada { get; set; }
        public bool FinDeSemana { get; set; }
        /// <summary>Nombre del feriado (null si no es feriado).</summary>
        public string Feriado { get; set; }
        /// <summary>Noche anterior a un feriado (fin de semana largo).</summary>
        public bool VisperaFeriado { get; set; }
        /// <summary>Ocupación real del mismo día de la semana un año antes (null si no hay historia).</summary>
        public decimal? PorcentajeAnioAnterior { get; set; }
        /// <summary>Consumo de servicios esperado (room service y upgrades) para ese día.</summary>
        public decimal ConsumoEsperado { get; set; }
        public SugerenciaPrecio Sugerencia { get; set; }
        /// <summary>Variación de tarifa sugerida (-0.15 = 15% de descuento, 0.10 = 10% de aumento).</summary>
        public decimal VariacionPrecio { get; set; }
        public bool EstadiaMinima { get; set; }

        #endregion
    }

    public class AlertaDemanda_68SA
    {
        #region Propiedades

        public TipoAlertaDemanda Tipo { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public decimal Porcentaje { get; set; }
        public string Detalle { get; set; }
        public int Dias => (Hasta.Date - Desde.Date).Days + 1;

        #endregion
    }

    public class DemandaTipo_68SA
    {
        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Habitaciones { get; set; }
        public decimal Tarifa { get; set; }
        public int Reservas { get; set; }
        public int Noches { get; set; }
        public decimal OcupacionHistorica { get; set; }
        public decimal OcupacionProyectada { get; set; }
        public decimal TarifaSugerida { get; set; }
        public decimal Ingresos { get; set; }
        /// <summary>Días del horizonte en los que es el tipo más demandado.</summary>
        public int DiasMasDemandado { get; set; }

        #endregion
    }

    /// <summary>Estacionalidad: comportamiento de un mes del año en la historia analizada.</summary>
    public class MesDemanda_68SA
    {
        #region Propiedades

        public int Mes { get; set; }
        public int Noches { get; set; }
        public decimal Ocupacion { get; set; }
        public decimal TarifaPromedio { get; set; }
        public decimal ConsumoPorNoche { get; set; }
        public TemporadaDemanda Temporada { get; set; }
        /// <summary>false si no hay historia de ese mes (la temporada sale del calendario típico).</summary>
        public bool ConHistoria { get; set; }

        #endregion
    }

    public class ComportamientoReservas_68SA
    {
        #region Propiedades

        public int Reservas { get; set; }
        public int Canceladas { get; set; }
        public decimal TasaCancelacion => Reservas > 0 ? Math.Round(Canceladas / (decimal)Reservas * 100m, 1) : 0m;
        public double AnticipacionPromedio { get; set; }
        public int AnticipacionMediana { get; set; }
        public double DuracionPromedio { get; set; }
        public double PersonasPromedio { get; set; }
        /// <summary>Reservas según anticipación: 0-2 días, 3-7, 8-30 y más de 30.</summary>
        public int[] AnticipacionTramos { get; set; } = new int[4];
        /// <summary>Llegadas por día de la semana (0 = lunes ... 6 = domingo).</summary>
        public int[] LlegadasPorDia { get; set; } = new int[7];
        public decimal PorcentajeFinDeSemana { get; set; }

        #endregion
    }

    public class IngresosDemanda_68SA
    {
        #region Propiedades

        public decimal Alojamiento { get; set; }
        public decimal RoomService { get; set; }
        public decimal Upgrades { get; set; }
        public int NochesVendidas { get; set; }
        public int NochesDisponibles { get; set; }
        public decimal Total => Alojamiento + RoomService + Upgrades;
        /// <summary>Tarifa promedio por noche vendida.</summary>
        public decimal ADR => NochesVendidas > 0 ? Math.Round(Alojamiento / NochesVendidas, 2) : 0m;
        /// <summary>Ingreso de alojamiento por habitación disponible.</summary>
        public decimal RevPAR => NochesDisponibles > 0 ? Math.Round(Alojamiento / NochesDisponibles, 2) : 0m;
        public decimal ServiciosPorNoche => NochesVendidas > 0 ? Math.Round((RoomService + Upgrades) / NochesVendidas, 2) : 0m;
        public decimal ProyeccionAlojamiento { get; set; }
        public decimal ProyeccionServicios { get; set; }

        #endregion
    }

    /// <summary>Consumo de servicios de un mes del historial (tendencia).</summary>
    public class ConsumoMes_68SA
    {
        #region Propiedades

        public DateTime Mes { get; set; }
        public int NochesOcupadas { get; set; }
        public decimal RoomService { get; set; }
        public decimal Upgrades { get; set; }
        public decimal PorNoche => NochesOcupadas > 0 ? Math.Round((RoomService + Upgrades) / NochesOcupadas, 2) : 0m;

        #endregion
    }

    public class OfertaPersonalizada_68SA
    {
        #region Propiedades

        public int HuespedId { get; set; }
        public string Huesped { get; set; }
        public string Documento { get; set; }
        public int Estadias { get; set; }
        public string TipoPreferido { get; set; }
        /// <summary>Tipo superior al preferido (para la oferta de upgrade), si existe.</summary>
        public string TipoSuperior { get; set; }
        public int MesPreferido { get; set; }
        public TemporadaDemanda TemporadaPreferida { get; set; }
        public decimal GastoTotal { get; set; }
        public decimal GastoServiciosPorEstadia { get; set; }
        public TipoOfertaHuesped Oferta { get; set; }

        #endregion
    }

    public class AnalisisDemanda_68SA
    {
        #region Propiedades

        public FiltroAnalisisDemanda_68SA Filtro { get; set; }
        public DateTime Generado { get; set; } = DateTime.Now;
        /// <summary>Nombre del tipo analizado (null = todo el hotel).</summary>
        public string TipoAnalizado { get; set; }
        public int Capacidad { get; set; }
        /// <summary>Noches de historia con datos: cuanto más, más confiable la proyección.</summary>
        public int NochesHistoria { get; set; }

        public List<DiaProyectado_68SA> Dias { get; set; } = new List<DiaProyectado_68SA>();
        public List<AlertaDemanda_68SA> Alertas { get; set; } = new List<AlertaDemanda_68SA>();
        public List<DemandaTipo_68SA> Tipos { get; set; } = new List<DemandaTipo_68SA>();
        public List<MesDemanda_68SA> Meses { get; set; } = new List<MesDemanda_68SA>();
        /// <summary>Ocupación histórica por día de la semana (0 = lunes ... 6 = domingo).</summary>
        public decimal[] OcupacionPorDiaSemana { get; set; } = new decimal[7];
        public ComportamientoReservas_68SA Comportamiento { get; set; } = new ComportamientoReservas_68SA();
        public IngresosDemanda_68SA Ingresos { get; set; } = new IngresosDemanda_68SA();
        public List<ConsumoMes_68SA> ConsumoPorMes { get; set; } = new List<ConsumoMes_68SA>();
        public List<ProductoConsumido_68SA> Productos { get; set; } = new List<ProductoConsumido_68SA>();
        public List<OfertaPersonalizada_68SA> Ofertas { get; set; } = new List<OfertaPersonalizada_68SA>();

        public decimal OcupacionHistorica { get; set; }
        public decimal OcupacionProyectada { get; set; }
        public int DiasBajaOcupacion { get; set; }
        public int DiasSaturados { get; set; }
        public string TipoMasDemandado { get; set; }
        public TemporadaDemanda TemporadaPredominante { get; set; }
        public decimal TasaCancelacionUsada { get; set; }

        #endregion
    }

    #endregion
}
