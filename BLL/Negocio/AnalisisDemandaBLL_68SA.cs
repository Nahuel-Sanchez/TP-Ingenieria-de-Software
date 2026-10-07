using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// Reporte inteligente "Análisis predictivo de ocupación y demanda".
    ///
    /// Cómo se proyecta la ocupación de cada noche futura (por tipo de habitación):
    ///   1. Base histórica: ocupación promedio del mismo mes en la historia, ajustada por el día de la
    ///      semana (los viernes y sábados suelen llenarse más) y por feriado.
    ///   2. Reservas en cartera: las habitaciones ya reservadas para esa noche, descontando la tasa de
    ///      cancelación histórica (las estadías en curso cuentan enteras).
    ///   3. Reservas por entrar: la parte de la demanda habitual que todavía no reservó. Sale de la
    ///      curva de anticipación: qué porción de las noches vendidas se reservó con al menos N días.
    ///   Ocupación estimada = reservas netas + base × (1 − porción que ya suele estar reservada), con
    ///   tope en la capacidad. Así, una fecha lejana depende casi solo de la historia y una cercana,
    ///   sobre todo de lo que ya está reservado.
    ///
    /// Con el resultado se arman la temporada, las alertas de baja ocupación o saturación, las
    /// sugerencias de precio / promoción y las ofertas personalizadas para los huéspedes frecuentes.
    /// </summary>
    public class AnalisisDemandaBLL_68SA
    {
        #region Campos

        public const decimal UmbralMuyBajo = 25m;
        public const decimal UmbralBajo = 40m;
        public const decimal UmbralAlto = 75m;
        public const decimal UmbralSaturacion = 90m;

        public const int HorizonteMinimo = 7, HorizonteMaximo = 120;
        public const int HistorialMinimo = 3, HistorialMaximo = 36;

        private readonly IAnalisisDemandaRepository_68SA _repo;

        #endregion

        #region Constructor

        public AnalisisDemandaBLL_68SA(IAnalisisDemandaRepository_68SA repo)
        {
            _repo = repo;
        }

        #endregion

        #region Cálculo

        /// <summary>Arma el análisis completo a partir de los datos crudos (no accede a la base).</summary>
        public static AnalisisDemanda_68SA Calcular(DatosDemanda_68SA datos, FiltroAnalisisDemanda_68SA filtro)
        {
            var c = new Contexto(datos, filtro);
            var analisis = new AnalisisDemanda_68SA
            {
                Filtro = filtro,
                Generado = DateTime.Now,
                TipoAnalizado = filtro.TipoHabitacionId.HasValue
                    ? datos.Tipos.FirstOrDefault(t => t.Id == filtro.TipoHabitacionId.Value)?.Nombre
                    : null,
                Capacidad = c.Analizados.Sum(t => t.Habitaciones),
                NochesHistoria = c.NochesHistoria,
                TasaCancelacionUsada = Math.Round(c.TasaCancelacion * 100m, 1)
            };

            ProyectarDias(c, analisis);
            ArmarAlertas(analisis);
            ArmarTipos(c, analisis);
            ArmarEstacionalidad(c, analisis);
            ArmarComportamiento(c, analisis);
            ArmarIngresosYConsumo(c, analisis);
            ArmarOfertas(c, analisis);

            var dias = analisis.Dias;
            analisis.OcupacionHistorica = c.OcupacionHistoricaPorcentaje();
            analisis.OcupacionProyectada = dias.Count > 0 ? Math.Round(dias.Average(d => d.Porcentaje), 1) : 0m;
            analisis.DiasBajaOcupacion = dias.Count(d => d.Porcentaje < UmbralBajo);
            analisis.DiasSaturados = dias.Count(d => d.Porcentaje >= UmbralSaturacion);
            analisis.TipoMasDemandado = dias.Where(d => d.TipoMasDemandado != null)
                                            .GroupBy(d => d.TipoMasDemandado)
                                            .OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
            analisis.TemporadaPredominante = dias.Count == 0 ? TemporadaDemanda.Media
                : dias.GroupBy(d => d.Temporada).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).First().Key;
            return analisis;
        }

        /// <summary>Ocupación por noche y por tipo, estadísticas históricas y curva de anticipación.</summary>
        private sealed class Contexto
        {
            public readonly FiltroAnalisisDemanda_68SA Filtro;
            public readonly DatosDemanda_68SA Datos;
            public readonly List<TipoHabitacionDemanda_68SA> Analizados;
            public readonly HashSet<int> IdsAnalizados;
            public readonly DateTime Hoy, Inicio, Fin, HistoriaDesde, HistoriaHasta;
            public readonly int NochesHistoria;
            public readonly decimal TasaCancelacion;
            /// <summary>Habitaciones ocupadas por noche y por tipo (índice 0 = Inicio).</summary>
            public readonly Dictionary<int, int[]> Ocupacion = new Dictionary<int, int[]>();
            /// <summary>Habitaciones ya reservadas a futuro, ponderadas por la probabilidad de que se mantengan.</summary>
            public readonly Dictionary<int, decimal[]> ReservadasNetas = new Dictionary<int, decimal[]>();
            public readonly Dictionary<int, int[]> Reservadas = new Dictionary<int, int[]>();
            /// <summary>Anticipación (en días) de cada noche vendida en la historia, ordenada.</summary>
            private readonly int[] _anticipaciones;
            public readonly Dictionary<int, ReservaDemanda_68SA> ReservaPorId;

            public Contexto(DatosDemanda_68SA datos, FiltroAnalisisDemanda_68SA filtro)
            {
                Datos = datos;
                Filtro = filtro;
                Hoy = filtro.Desde.Date;
                Inicio = filtro.HistorialDesde;
                // Con toda la historia, los arreglos por noche arrancan en la primera reserva (no en 1900)
                if (filtro.HistorialCompleto)
                    Inicio = datos.Reservas.Count == 0 ? filtro.Desde.Date : Min(datos.Reservas.Min(r => r.Ingreso.Date), filtro.Desde.Date);
                Fin = filtro.Hasta;
                HistoriaHasta = Hoy.AddDays(-1);

                Analizados = datos.Tipos.Where(t => !filtro.TipoHabitacionId.HasValue || t.Id == filtro.TipoHabitacionId.Value).ToList();
                IdsAnalizados = new HashSet<int>(Analizados.Select(t => t.Id));
                ReservaPorId = datos.Reservas.GroupBy(r => r.Id).ToDictionary(g => g.Key, g => g.First());

                int total = Indice(Fin) + 1;
                foreach (var tipo in datos.Tipos)
                {
                    Ocupacion[tipo.Id] = new int[total];
                    Reservadas[tipo.Id] = new int[total];
                    ReservadasNetas[tipo.Id] = new decimal[total];
                }

                // La historia empieza con la primera reserva (si el sistema tiene menos historia que la pedida)
                var vendidas = datos.Reservas.Where(r => !r.Cancelada).ToList();
                DateTime primera = vendidas.Count == 0 ? Hoy : vendidas.Min(r => r.Ingreso.Date);
                HistoriaDesde = primera > Inicio ? primera : Inicio;
                NochesHistoria = Math.Max(0, (HistoriaHasta - HistoriaDesde).Days + 1);

                // Tasa de cancelación: reservas con llegada dentro de la historia
                var historicas = datos.Reservas.Where(r => IdsAnalizados.Contains(r.TipoHabitacionId)
                                                        && r.Ingreso.Date >= HistoriaDesde && r.Ingreso.Date <= HistoriaHasta).ToList();
                TasaCancelacion = historicas.Count == 0 ? 0m
                    : Math.Min(0.9m, historicas.Count(r => r.Cancelada) / (decimal)historicas.Count);

                var anticipaciones = new List<int>();
                foreach (var r in vendidas)
                {
                    if (!Ocupacion.ContainsKey(r.TipoHabitacionId)) continue;
                    DateTime desde = r.Ingreso.Date < Inicio ? Inicio : r.Ingreso.Date;
                    DateTime hasta = r.Egreso.Date.AddDays(-1) > Fin ? Fin : r.Egreso.Date.AddDays(-1);
                    // En curso: se queda seguro; confirmada a futuro: puede cancelarse
                    decimal peso = r.Estado == EstadoReserva.EnCurso ? 1m : 1m - TasaCancelacion;
                    for (DateTime noche = desde; noche <= hasta; noche = noche.AddDays(1))
                    {
                        int i = Indice(noche);
                        if (noche < Hoy)
                        {
                            Ocupacion[r.TipoHabitacionId][i]++;
                            if (noche >= HistoriaDesde)
                                anticipaciones.Add(Math.Max(0, (noche - r.Registro.Date).Days));
                        }
                        else if (r.Estado == EstadoReserva.Confirmada || r.Estado == EstadoReserva.EnCurso)
                        {
                            Reservadas[r.TipoHabitacionId][i]++;
                            ReservadasNetas[r.TipoHabitacionId][i] += peso;
                        }
                    }
                }
                anticipaciones.Sort();
                _anticipaciones = anticipaciones.ToArray();
            }

            public int Indice(DateTime fecha) => (fecha.Date - Inicio).Days;

            public IEnumerable<DateTime> NochesHistoricas()
            {
                for (DateTime d = HistoriaDesde; d <= HistoriaHasta; d = d.AddDays(1))
                    yield return d;
            }

            /// <summary>
            /// Porción de la demanda que ya suele estar reservada cuando faltan <paramref name="dias"/> días
            /// (reservas hechas con más anticipación que esa). Para hoy queda afuera lo que se reserva en el día.
            /// Sin historia se asume que todo está reservado (la proyección queda igual a las reservas en cartera).
            /// </summary>
            public decimal YaReservado(int dias)
            {
                if (_anticipaciones.Length == 0) return 1m;
                // primera posición con anticipación > dias
                int lo = 0, hi = _anticipaciones.Length;
                while (lo < hi)
                {
                    int mid = (lo + hi) / 2;
                    if (_anticipaciones[mid] <= dias) lo = mid + 1; else hi = mid;
                }
                return (_anticipaciones.Length - lo) / (decimal)_anticipaciones.Length;
            }

            public int CapacidadHistorica(TipoHabitacionDemanda_68SA t) => t.HabitacionesTotales > 0 ? t.HabitacionesTotales : t.Habitaciones;

            public int OcupadasHistoricas(IEnumerable<TipoHabitacionDemanda_68SA> tipos, DateTime noche)
            {
                int i = Indice(noche);
                return tipos.Sum(t => Ocupacion[t.Id][i]);
            }

            public decimal OcupacionHistoricaPorcentaje()
            {
                int capacidad = Analizados.Sum(CapacidadHistorica);
                if (capacidad == 0 || NochesHistoria == 0) return 0m;
                long ocupadas = NochesHistoricas().Sum(n => (long)OcupadasHistoricas(Analizados, n));
                return Math.Round(ocupadas * 100m / ((long)capacidad * NochesHistoria), 1);
            }

            /// <summary>Promedios históricos de un conjunto de tipos (en habitaciones ocupadas).</summary>
            public Patron PatronDe(IList<TipoHabitacionDemanda_68SA> tipos)
            {
                var p = new Patron();
                var porMes = new decimal[13]; var nochesMes = new int[13];
                var porDia = new decimal[7]; var nochesDia = new int[7];
                decimal sumaFeriados = 0; int nochesFeriado = 0;
                decimal suma = 0; int noches = 0;
                foreach (var noche in NochesHistoricas())
                {
                    int ocupadas = OcupadasHistoricas(tipos, noche);
                    suma += ocupadas; noches++;
                    porMes[noche.Month] += ocupadas; nochesMes[noche.Month]++;
                    int dia = DiaSemana(noche);
                    porDia[dia] += ocupadas; nochesDia[dia]++;
                    if (FeriadoDeLaNoche(noche) != null) { sumaFeriados += ocupadas; nochesFeriado++; }
                }
                p.Promedio = noches > 0 ? suma / noches : 0m;
                for (int m = 1; m <= 12; m++)
                {
                    p.NochesMes[m] = nochesMes[m];
                    p.PromedioMes[m] = nochesMes[m] > 0 ? porMes[m] / nochesMes[m] : p.Promedio;
                }
                for (int d = 0; d < 7; d++)
                    p.FactorDia[d] = nochesDia[d] > 0 && p.Promedio > 0 ? (porDia[d] / nochesDia[d]) / p.Promedio : 1m;
                p.FactorFeriado = nochesFeriado >= 2 && p.Promedio > 0
                    ? Math.Max(1m, Math.Min(1.6m, (sumaFeriados / nochesFeriado) / p.Promedio))
                    : 1.1m;
                return p;
            }
        }

        private sealed class Patron
        {
            public decimal Promedio;
            public readonly decimal[] PromedioMes = new decimal[13];
            public readonly int[] NochesMes = new int[13];
            public readonly decimal[] FactorDia = new decimal[7];
            public decimal FactorFeriado = 1m;

            /// <summary>Habitaciones que se esperarían ocupadas esa noche según la historia.</summary>
            public decimal Base(DateTime noche)
            {
                // Con menos de una semana de historia del mes se usa el promedio general
                decimal mes = NochesMes[noche.Month] >= 7 ? PromedioMes[noche.Month] : Promedio;
                decimal valor = mes * FactorDia[DiaSemana(noche)];
                if (FeriadoDeLaNoche(noche) != null) valor *= FactorFeriado;
                return valor;
            }

            public TemporadaDemanda Temporada(int mes)
            {
                if (NochesMes[mes] < 7 || Promedio <= 0) return TemporadaCalendario(mes);
                decimal indice = PromedioMes[mes] / Promedio;
                return indice >= 1.12m ? TemporadaDemanda.Alta : indice <= 0.88m ? TemporadaDemanda.Baja : TemporadaDemanda.Media;
            }
        }

        private static void ProyectarDias(Contexto c, AnalisisDemanda_68SA analisis)
        {
            var patronAnalizado = c.PatronDe(c.Analizados);
            var patrones = c.Datos.Tipos.ToDictionary(t => t.Id, t => c.PatronDe(new[] { t }));
            decimal serviciosPorNoche = ServiciosPorNocheOcupada(c);

            for (DateTime noche = c.Hoy; noche <= c.Fin; noche = noche.AddDays(1))
            {
                int i = c.Indice(noche);
                decimal yaReservado = c.YaReservado((noche - c.Hoy).Days);

                // Proyección de cada tipo (todos: el tipo más demandado se busca en todo el hotel)
                var porTipo = new Dictionary<int, (decimal Estimada, decimal Netas, int Reservadas, decimal PorEntrar)>();
                foreach (var tipo in c.Datos.Tipos)
                {
                    int capacidad = tipo.Habitaciones;
                    decimal netas = Math.Min(capacidad, c.ReservadasNetas[tipo.Id][i]);
                    decimal porEntrar = Math.Max(0m, patrones[tipo.Id].Base(noche) * (1m - yaReservado));
                    porEntrar = Math.Min(porEntrar, Math.Max(0m, capacidad - netas));
                    porTipo[tipo.Id] = (netas + porEntrar, netas, c.Reservadas[tipo.Id][i], porEntrar);
                }

                var dia = new DiaProyectado_68SA
                {
                    Fecha = noche,
                    Capacidad = c.Analizados.Sum(t => t.Habitaciones),
                    Reservadas = c.Analizados.Sum(t => porTipo[t.Id].Reservadas),
                    ReservadasNetas = Math.Round(c.Analizados.Sum(t => porTipo[t.Id].Netas), 2),
                    ReservasPorEntrar = Math.Round(c.Analizados.Sum(t => porTipo[t.Id].PorEntrar), 2),
                    FinDeSemana = EsFinDeSemana(noche),
                    Feriado = Feriado(noche),
                    VisperaFeriado = Feriado(noche) == null && Feriado(noche.AddDays(1)) != null,
                    Temporada = patronAnalizado.Temporada(noche.Month)
                };
                dia.OcupacionEstimada = Math.Round(dia.ReservadasNetas + dia.ReservasPorEntrar, 2);
                dia.ConsumoEsperado = Math.Round(dia.OcupacionEstimada * serviciosPorNoche, 2);

                // Tipo más demandado: el de mayor ocupación estimada (en %)
                var masDemandado = c.Datos.Tipos.Where(t => t.Habitaciones > 0)
                    .Select(t => new { Tipo = t, Porcentaje = porTipo[t.Id].Estimada / t.Habitaciones * 100m })
                    .OrderByDescending(x => x.Porcentaje).ThenByDescending(x => x.Tipo.Habitaciones)
                    .FirstOrDefault();
                if (masDemandado != null && masDemandado.Porcentaje > 0)
                {
                    dia.TipoMasDemandado = masDemandado.Tipo.Nombre;
                    dia.PorcentajeTipoMasDemandado = Math.Round(masDemandado.Porcentaje, 1);
                }

                // Mismo día de la semana, un año antes (364 días)
                DateTime anterior = noche.AddDays(-364);
                int capacidadHistorica = c.Analizados.Sum(t => c.CapacidadHistorica(t));
                if (anterior >= c.HistoriaDesde && anterior <= c.HistoriaHasta && capacidadHistorica > 0)
                    dia.PorcentajeAnioAnterior = Math.Round(c.OcupadasHistoricas(c.Analizados, anterior) * 100m / capacidadHistorica, 1);

                Sugerir(dia);
                analisis.Dias.Add(dia);
            }
        }

        /// <summary>Sugerencia de precio según la ocupación estimada.</summary>
        private static void Sugerir(DiaProyectado_68SA dia)
        {
            decimal p = dia.Porcentaje;
            bool pico = dia.FinDeSemana || dia.Feriado != null || dia.VisperaFeriado;
            if (p >= UmbralSaturacion)
            {
                dia.Sugerencia = SugerenciaPrecio.AumentoFuerte;
                dia.VariacionPrecio = 0.20m;
                dia.EstadiaMinima = pico;
            }
            else if (p >= UmbralAlto)
            {
                dia.Sugerencia = SugerenciaPrecio.Aumento;
                dia.VariacionPrecio = pico ? 0.15m : 0.10m;
            }
            else if (p >= UmbralBajo)
            {
                dia.Sugerencia = SugerenciaPrecio.Mantener;
                dia.VariacionPrecio = 0m;
            }
            else if (p >= UmbralMuyBajo)
            {
                dia.Sugerencia = SugerenciaPrecio.Promocion;
                dia.VariacionPrecio = -0.10m;
            }
            else
            {
                dia.Sugerencia = SugerenciaPrecio.DescuentoFuerte;
                dia.VariacionPrecio = -0.20m;
            }
        }

        private static decimal VariacionSegun(decimal porcentaje)
            => porcentaje >= UmbralSaturacion ? 0.20m
             : porcentaje >= UmbralAlto ? 0.10m
             : porcentaje >= UmbralBajo ? 0m
             : porcentaje >= UmbralMuyBajo ? -0.10m
             : -0.20m;

        /// <summary>Períodos seguidos de baja ocupación (3 días o más) o de saturación, y los feriados del período.</summary>
        private static void ArmarAlertas(AnalisisDemanda_68SA analisis)
        {
            var dias = analisis.Dias;
            AgregarTramos(analisis, dias, d => d.Porcentaje < UmbralBajo, 3, TipoAlertaDemanda.BajaOcupacion);
            AgregarTramos(analisis, dias, d => d.Porcentaje >= UmbralSaturacion, 1, TipoAlertaDemanda.Saturacion);

            foreach (var dia in dias.Where(d => d.Feriado != null))
                analisis.Alertas.Add(new AlertaDemanda_68SA
                {
                    Tipo = TipoAlertaDemanda.Feriado,
                    Desde = dia.Fecha,
                    Hasta = dia.Fecha,
                    Porcentaje = dia.Porcentaje,
                    Detalle = dia.Feriado
                });

            analisis.Alertas.Sort((a, b) =>
            {
                int porFecha = a.Desde.CompareTo(b.Desde);
                return porFecha != 0 ? porFecha : a.Tipo.CompareTo(b.Tipo);
            });
        }

        private static void AgregarTramos(AnalisisDemanda_68SA analisis, List<DiaProyectado_68SA> dias,
                                          Func<DiaProyectado_68SA, bool> condicion, int minimo, TipoAlertaDemanda tipo)
        {
            int i = 0;
            while (i < dias.Count)
            {
                if (!condicion(dias[i])) { i++; continue; }
                int j = i;
                while (j + 1 < dias.Count && condicion(dias[j + 1])) j++;
                if (j - i + 1 >= minimo)
                {
                    var tramo = dias.GetRange(i, j - i + 1);
                    analisis.Alertas.Add(new AlertaDemanda_68SA
                    {
                        Tipo = tipo,
                        Desde = dias[i].Fecha,
                        Hasta = dias[j].Fecha,
                        Porcentaje = Math.Round(tramo.Average(d => d.Porcentaje), 1)
                    });
                }
                i = j + 1;
            }
        }

        private static void ArmarTipos(Contexto c, AnalisisDemanda_68SA analisis)
        {
            var patrones = c.Datos.Tipos.ToDictionary(t => t.Id, t => c.PatronDe(new[] { t }));
            foreach (var tipo in c.Datos.Tipos)
            {
                var reservas = c.Datos.Reservas.Where(r => r.TipoHabitacionId == tipo.Id && !r.Cancelada
                                                        && r.Ingreso.Date >= c.HistoriaDesde && r.Ingreso.Date <= c.HistoriaHasta).ToList();
                int noches = c.NochesHistoricas().Sum(n => c.Ocupacion[tipo.Id][c.Indice(n)]);
                int capacidadHistorica = c.CapacidadHistorica(tipo);

                // Ingresos de alojamiento de las noches vendidas dentro de la historia
                decimal ingresos = 0m;
                foreach (var r in c.Datos.Reservas.Where(r => r.TipoHabitacionId == tipo.Id && !r.Cancelada))
                    ingresos += r.TarifaNoche * NochesDentro(r, c.HistoriaDesde, c.HistoriaHasta);

                // Ocupación proyectada del tipo: misma lógica que la de los días
                decimal proyectada = 0m; int dias = 0;
                for (DateTime noche = c.Hoy; noche <= c.Fin; noche = noche.AddDays(1), dias++)
                {
                    int i = c.Indice(noche);
                    decimal netas = Math.Min(tipo.Habitaciones, c.ReservadasNetas[tipo.Id][i]);
                    decimal porEntrar = Math.Max(0m, patrones[tipo.Id].Base(noche) * (1m - c.YaReservado((noche - c.Hoy).Days)));
                    decimal estimada = Math.Min(tipo.Habitaciones, netas + porEntrar);
                    proyectada += tipo.Habitaciones > 0 ? estimada / tipo.Habitaciones * 100m : 0m;
                }
                proyectada = dias > 0 ? Math.Round(proyectada / dias, 1) : 0m;

                analisis.Tipos.Add(new DemandaTipo_68SA
                {
                    Id = tipo.Id,
                    Nombre = tipo.Nombre,
                    Habitaciones = tipo.Habitaciones,
                    Tarifa = tipo.Tarifa,
                    Reservas = reservas.Count,
                    Noches = noches,
                    OcupacionHistorica = capacidadHistorica > 0 && c.NochesHistoria > 0
                        ? Math.Round(noches * 100m / (capacidadHistorica * (decimal)c.NochesHistoria), 1) : 0m,
                    OcupacionProyectada = proyectada,
                    TarifaSugerida = Math.Round(tipo.Tarifa * (1m + VariacionSegun(proyectada)), 0),
                    Ingresos = ingresos,
                    DiasMasDemandado = analisis.Dias.Count(d => d.TipoMasDemandado == tipo.Nombre)
                });
            }
            analisis.Tipos.Sort((a, b) => b.OcupacionProyectada.CompareTo(a.OcupacionProyectada));
        }

        private static void ArmarEstacionalidad(Contexto c, AnalisisDemanda_68SA analisis)
        {
            var patron = c.PatronDe(c.Analizados);
            int capacidad = c.Analizados.Sum(t => c.CapacidadHistorica(t));
            var cargosPorMes = new decimal[13];
            foreach (var cargo in CargosHistoricos(c))
                cargosPorMes[cargo.Fecha.Month] += cargo.Monto;

            var tarifas = new decimal[13]; var ocupadas = new int[13];
            foreach (var r in c.Datos.Reservas.Where(r => !r.Cancelada && c.IdsAnalizados.Contains(r.TipoHabitacionId)))
                for (DateTime n = Max(r.Ingreso.Date, c.HistoriaDesde); n < r.Egreso.Date && n <= c.HistoriaHasta; n = n.AddDays(1))
                {
                    tarifas[n.Month] += r.TarifaNoche;
                    ocupadas[n.Month]++;
                }

            for (int mes = 1; mes <= 12; mes++)
            {
                bool conHistoria = patron.NochesMes[mes] >= 7;
                analisis.Meses.Add(new MesDemanda_68SA
                {
                    Mes = mes,
                    Noches = patron.NochesMes[mes],
                    ConHistoria = conHistoria,
                    Ocupacion = conHistoria && capacidad > 0 ? Math.Round(patron.PromedioMes[mes] / capacidad * 100m, 1) : 0m,
                    TarifaPromedio = ocupadas[mes] > 0 ? Math.Round(tarifas[mes] / ocupadas[mes], 0) : 0m,
                    ConsumoPorNoche = ocupadas[mes] > 0 ? Math.Round(cargosPorMes[mes] / ocupadas[mes], 0) : 0m,
                    Temporada = patron.Temporada(mes)
                });
            }

            for (int d = 0; d < 7; d++)
                analisis.OcupacionPorDiaSemana[d] = capacidad > 0 ? Math.Round(patron.Promedio * patron.FactorDia[d] / capacidad * 100m, 1) : 0m;
        }

        private static void ArmarComportamiento(Contexto c, AnalisisDemanda_68SA analisis)
        {
            var reservas = c.Datos.Reservas.Where(r => c.IdsAnalizados.Contains(r.TipoHabitacionId)
                                                   && r.Ingreso.Date >= c.HistoriaDesde && r.Ingreso.Date <= c.HistoriaHasta).ToList();
            var vendidas = reservas.Where(r => !r.Cancelada).ToList();
            var comp = analisis.Comportamiento;
            comp.Reservas = reservas.Count;
            comp.Canceladas = reservas.Count(r => r.Cancelada);
            if (vendidas.Count == 0) return;

            var anticipaciones = vendidas.Select(r => r.Anticipacion).OrderBy(x => x).ToList();
            comp.AnticipacionPromedio = Math.Round(anticipaciones.Average(), 1);
            comp.AnticipacionMediana = anticipaciones[anticipaciones.Count / 2];
            comp.DuracionPromedio = Math.Round(vendidas.Average(r => r.Noches), 1);
            comp.PersonasPromedio = Math.Round(vendidas.Average(r => r.Personas), 1);
            foreach (int a in anticipaciones)
                comp.AnticipacionTramos[a <= 2 ? 0 : a <= 7 ? 1 : a <= 30 ? 2 : 3]++;
            foreach (var r in vendidas)
                comp.LlegadasPorDia[DiaSemana(r.Ingreso)]++;
            comp.PorcentajeFinDeSemana = Math.Round(vendidas.Count(r => EsFinDeSemana(r.Ingreso)) * 100m / vendidas.Count, 1);
        }

        private static void ArmarIngresosYConsumo(Contexto c, AnalisisDemanda_68SA analisis)
        {
            var ing = analisis.Ingresos;
            var vendidas = c.Datos.Reservas.Where(r => !r.Cancelada && c.IdsAnalizados.Contains(r.TipoHabitacionId)).ToList();
            foreach (var r in vendidas)
            {
                int noches = NochesDentro(r, c.HistoriaDesde, c.HistoriaHasta);
                ing.NochesVendidas += noches;
                ing.Alojamiento += r.TarifaNoche * noches;
            }
            ing.NochesDisponibles = c.Analizados.Sum(t => c.CapacidadHistorica(t)) * c.NochesHistoria;
            foreach (var cargo in CargosHistoricos(c))
            {
                if (cargo.Tipo == TipoCargo.RoomService) ing.RoomService += cargo.Monto;
                else ing.Upgrades += cargo.Monto;
            }

            // Proyección: noches estimadas por tipo a la tarifa actual del tipo (proporcional a la ocupación)
            decimal tarifaPromedio = c.Analizados.Sum(t => t.Habitaciones) > 0
                ? c.Analizados.Sum(t => t.Tarifa * t.Habitaciones) / c.Analizados.Sum(t => t.Habitaciones)
                : 0m;
            ing.ProyeccionAlojamiento = Math.Round(analisis.Dias.Sum(d => d.OcupacionEstimada) * tarifaPromedio, 2);
            ing.ProyeccionServicios = Math.Round(analisis.Dias.Sum(d => d.ConsumoEsperado), 2);

            // Tendencia de consumo: un renglón por mes calendario de la historia
            var meses = new SortedDictionary<DateTime, ConsumoMes_68SA>();
            ConsumoMes_68SA Mes(DateTime fecha)
            {
                var clave = new DateTime(fecha.Year, fecha.Month, 1);
                if (!meses.TryGetValue(clave, out var mes)) meses[clave] = mes = new ConsumoMes_68SA { Mes = clave };
                return mes;
            }
            foreach (var noche in c.NochesHistoricas())
                Mes(noche).NochesOcupadas += c.OcupadasHistoricas(c.Analizados, noche);
            foreach (var cargo in CargosHistoricos(c))
            {
                var mes = Mes(cargo.Fecha);
                if (cargo.Tipo == TipoCargo.RoomService) mes.RoomService += cargo.Monto;
                else mes.Upgrades += cargo.Monto;
            }
            analisis.ConsumoPorMes = meses.Values.ToList();

            analisis.Productos = c.Datos.Productos
                .Where(p => p.Fecha.Date >= c.HistoriaDesde && p.Fecha.Date <= c.HistoriaHasta
                         && c.ReservaPorId.TryGetValue(p.ReservaId, out var r) && c.IdsAnalizados.Contains(r.TipoHabitacionId))
                .GroupBy(p => p.Nombre)
                .Select(g => new ProductoConsumido_68SA { Nombre = g.Key, Cantidad = g.Sum(p => p.Cantidad), Monto = g.Sum(p => p.Monto) })
                .OrderByDescending(p => p.Cantidad).ThenByDescending(p => p.Monto)
                .Take(8).ToList();
        }

        /// <summary>Ofertas para los huéspedes que volvieron al hotel (2 estadías o más).</summary>
        private static void ArmarOfertas(Contexto c, AnalisisDemanda_68SA analisis)
        {
            var tiposPorTarifa = c.Datos.Tipos.OrderBy(t => t.Tarifa).ToList();
            var temporadaMes = analisis.Meses.ToDictionary(m => m.Mes, m => m.Temporada);
            var cargosPorReserva = c.Datos.Cargos.GroupBy(x => x.ReservaId).ToDictionary(g => g.Key, g => g.Sum(x => x.Monto));

            // Solo estadías que ya empezaron (las reservas futuras todavía no tienen consumos)
            var vendidas = c.Datos.Reservas.Where(r => !r.Cancelada && r.Ingreso.Date < c.Hoy).ToList();
            decimal serviciosPorEstadia = vendidas.Count > 0
                ? vendidas.Sum(r => cargosPorReserva.TryGetValue(r.Id, out var m) ? m : 0m) / vendidas.Count
                : 0m;

            foreach (var grupo in vendidas.GroupBy(r => r.HuespedId).Where(g => g.Count() >= 2))
            {
                var estadias = grupo.ToList();
                int tipoPreferido = estadias.GroupBy(r => r.TipoHabitacionId).OrderByDescending(g => g.Count()).First().Key;
                var tipo = tiposPorTarifa.FirstOrDefault(t => t.Id == tipoPreferido);
                var superior = tipo == null ? null : tiposPorTarifa.FirstOrDefault(t => t.Tarifa > tipo.Tarifa);
                int mesPreferido = estadias.GroupBy(r => r.Ingreso.Month).OrderByDescending(g => g.Count())
                                           .ThenBy(g => g.Key).First().Key;
                decimal servicios = estadias.Sum(r => cargosPorReserva.TryGetValue(r.Id, out var m) ? m : 0m);
                decimal serviciosEstadia = servicios / estadias.Count;
                var temporada = temporadaMes.TryGetValue(mesPreferido, out var t2) ? t2 : TemporadaCalendario(mesPreferido);

                TipoOfertaHuesped oferta;
                if (serviciosEstadia > 0 && serviciosEstadia >= serviciosPorEstadia * 1.3m) oferta = TipoOfertaHuesped.PaqueteServicios;
                else if (temporada == TemporadaDemanda.Alta) oferta = TipoOfertaHuesped.PreventaTemporada;
                else if (temporada == TemporadaDemanda.Baja) oferta = TipoOfertaHuesped.DescuentoFidelidad;
                else if (superior != null) oferta = TipoOfertaHuesped.UpgradeCortesia;
                else oferta = TipoOfertaHuesped.DescuentoFidelidad;

                var primera = estadias[0];
                analisis.Ofertas.Add(new OfertaPersonalizada_68SA
                {
                    HuespedId = grupo.Key,
                    Huesped = primera.Huesped,
                    Documento = primera.Documento,
                    Estadias = estadias.Count,
                    TipoPreferido = tipo?.Nombre,
                    TipoSuperior = superior?.Nombre,
                    MesPreferido = mesPreferido,
                    TemporadaPreferida = temporada,
                    GastoTotal = estadias.Sum(r => r.Monto) + servicios,
                    GastoServiciosPorEstadia = Math.Round(serviciosEstadia, 2),
                    Oferta = oferta
                });
            }

            analisis.Ofertas = analisis.Ofertas.OrderByDescending(o => o.Estadias).ThenByDescending(o => o.GastoTotal).Take(10).ToList();
        }

        #endregion

        #region Auxiliares

        private static IEnumerable<CargoDemanda_68SA> CargosHistoricos(Contexto c)
            => c.Datos.Cargos.Where(x => x.Fecha.Date >= c.HistoriaDesde && x.Fecha.Date <= c.HistoriaHasta
                                      && c.ReservaPorId.TryGetValue(x.ReservaId, out var r) && c.IdsAnalizados.Contains(r.TipoHabitacionId));

        /// <summary>Consumo promedio de servicios por noche ocupada en la historia (para proyectar el consumo).</summary>
        private static decimal ServiciosPorNocheOcupada(Contexto c)
        {
            long noches = c.NochesHistoricas().Sum(n => (long)c.OcupadasHistoricas(c.Analizados, n));
            return noches > 0 ? CargosHistoricos(c).Sum(x => x.Monto) / noches : 0m;
        }

        private static int NochesDentro(ReservaDemanda_68SA r, DateTime desde, DateTime hasta)
        {
            DateTime inicio = Max(r.Ingreso.Date, desde);
            DateTime fin = r.Egreso.Date.AddDays(-1) < hasta ? r.Egreso.Date.AddDays(-1) : hasta;
            return fin < inicio ? 0 : (fin - inicio).Days + 1;
        }

        private static DateTime Max(DateTime a, DateTime b) => a > b ? a : b;
        private static DateTime Min(DateTime a, DateTime b) => a < b ? a : b;

        /// <summary>0 = lunes ... 6 = domingo.</summary>
        public static int DiaSemana(DateTime fecha) => ((int)fecha.DayOfWeek + 6) % 7;

        /// <summary>Noches de fin de semana para un hotel: viernes y sábado.</summary>
        public static bool EsFinDeSemana(DateTime noche) => noche.DayOfWeek == DayOfWeek.Friday || noche.DayOfWeek == DayOfWeek.Saturday;

        /// <summary>Temporada típica de Argentina cuando no hay historia de ese mes.</summary>
        public static TemporadaDemanda TemporadaCalendario(int mes)
        {
            switch (mes)
            {
                case 1: case 2: case 7: return TemporadaDemanda.Alta;   // verano y vacaciones de invierno
                case 3: case 8: case 12: return TemporadaDemanda.Media;
                default: return TemporadaDemanda.Baja;
            }
        }

        /// <summary>
        /// Feriado que afecta a esa noche (clave de traducción) o null: el feriado mismo o la noche anterior,
        /// que es la que se llena en un fin de semana largo.
        /// </summary>
        public static string FeriadoDeLaNoche(DateTime noche)
            => Feriado(noche.Date) ?? Feriado(noche.Date.AddDays(1));

        /// <summary>Feriado nacional de Argentina en esa fecha (clave de traducción) o null. Fechas de calendario, sin traslados.</summary>
        public static string Feriado(DateTime fecha)
        {
            switch (fecha.Month * 100 + fecha.Day)
            {
                case 101: return "Analisis_feriado_anioNuevo";
                case 324: return "Analisis_feriado_memoria";
                case 402: return "Analisis_feriado_malvinas";
                case 501: return "Analisis_feriado_trabajador";
                case 525: return "Analisis_feriado_revolucion";
                case 617: return "Analisis_feriado_guemes";
                case 620: return "Analisis_feriado_belgrano";
                case 709: return "Analisis_feriado_independencia";
                case 817: return "Analisis_feriado_sanMartin";
                case 1012: return "Analisis_feriado_diversidad";
                case 1120: return "Analisis_feriado_soberania";
                case 1208: return "Analisis_feriado_inmaculada";
                case 1225: return "Analisis_feriado_navidad";
            }
            DateTime pascua = Pascua(fecha.Year);
            int dias = (fecha - pascua).Days;
            if (dias == -48 || dias == -47) return "Analisis_feriado_carnaval";
            if (dias == -3 || dias == -2) return "Analisis_feriado_semanaSanta";
            return null;
        }

        /// <summary>Domingo de Pascua (algoritmo de Gauss / Meeus para el calendario gregoriano).</summary>
        public static DateTime Pascua(int anio)
        {
            int a = anio % 19, b = anio / 100, c = anio % 100, d = b / 4, e = b % 4;
            int f = (b + 8) / 25, g = (b - f + 1) / 3, h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4, k = c % 4, l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int mes = (h + l - 7 * m + 114) / 31, dia = ((h + l - 7 * m + 114) % 31) + 1;
            return new DateTime(anio, mes, dia);
        }

        #endregion

        #region Operaciones

        public AnalisisDemanda_68SA Generar(FiltroAnalisisDemanda_68SA filtro)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.GenerarAnalisisDemanda);

            filtro = filtro ?? new FiltroAnalisisDemanda_68SA();
            if (filtro.DiasHorizonte < HorizonteMinimo || filtro.DiasHorizonte > HorizonteMaximo)
                throw new DatosInvalidosException_68SA($"El horizonte debe estar entre {HorizonteMinimo} y {HorizonteMaximo} días.");
            if (!filtro.HistorialCompleto && (filtro.MesesHistorial < HistorialMinimo || filtro.MesesHistorial > HistorialMaximo))
                throw new DatosInvalidosException_68SA($"El historial debe estar entre {HistorialMinimo} y {HistorialMaximo} meses.");
            if (filtro.Desde.Date < DateTime.Today)
                throw new RangoFechasInvalidoException_68SA("La proyección no puede empezar antes de hoy.");

            var datos = _repo.GetDatos(filtro.HistorialDesde, filtro.Hasta);
            return Calcular(datos, filtro);
        }

        #endregion

        #region Métodos públicos

        public bool PuedeGenerar() => SessionManager_08YS.Instance.HasPermission(Permisos.GenerarAnalisisDemanda);

        #endregion
    }
}
