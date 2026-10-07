using BE_08YS;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;

namespace GUI_08YS.Reportes
{
    /// <summary>Textos del análisis de demanda (se usan en el reporte impreso y en el resumen de pantalla).</summary>
    public static class AnalisisDemandaTextos_68SA
    {
        #region Métodos públicos

        public static string Porcentaje(decimal valor, CultureInfo cultura) => valor.ToString("0.#", cultura) + "%";

        public static string Sugerencia(DiaProyectado_68SA dia, CultureInfo cultura)
        {
            string variacion = Math.Abs(dia.VariacionPrecio * 100m).ToString("0", cultura);
            string texto;
            switch (dia.Sugerencia)
            {
                case SugerenciaPrecio.AumentoFuerte: texto = string.Format(T("Analisis_sugAumento"), variacion); break;
                case SugerenciaPrecio.Aumento: texto = string.Format(T("Analisis_sugAumento"), variacion); break;
                case SugerenciaPrecio.Promocion: texto = string.Format(T("Analisis_sugPromocion"), variacion); break;
                case SugerenciaPrecio.DescuentoFuerte: texto = string.Format(T("Analisis_sugDescuento"), variacion); break;
                default: texto = T("Analisis_sugMantener"); break;
            }
            if (dia.EstadiaMinima) texto += " · " + T("Analisis_sugEstadiaMinima");
            return texto;
        }

        public static Color ColorSugerencia(SugerenciaPrecio sugerencia)
        {
            switch (sugerencia)
            {
                case SugerenciaPrecio.AumentoFuerte: return Color.FromArgb(176, 42, 42);
                case SugerenciaPrecio.Aumento: return Color.FromArgb(200, 110, 20);
                case SugerenciaPrecio.Promocion: return Color.FromArgb(30, 110, 180);
                case SugerenciaPrecio.DescuentoFuerte: return Color.FromArgb(20, 80, 160);
                default: return Color.FromArgb(40, 120, 70);
            }
        }

        /// <summary>Color según la ocupación: azul (baja), verde (normal), naranja (alta), rojo (saturación).</summary>
        public static Color ColorOcupacion(decimal porcentaje)
        {
            if (porcentaje >= BLL_08YS.Negocio.AnalisisDemandaBLL_68SA.UmbralSaturacion) return Color.FromArgb(192, 57, 43);
            if (porcentaje >= BLL_08YS.Negocio.AnalisisDemandaBLL_68SA.UmbralAlto) return Color.FromArgb(230, 126, 34);
            if (porcentaje >= BLL_08YS.Negocio.AnalisisDemandaBLL_68SA.UmbralBajo) return Color.FromArgb(39, 174, 96);
            return Color.FromArgb(52, 120, 200);
        }

        public static Color ColorTemporada(TemporadaDemanda temporada)
        {
            switch (temporada)
            {
                case TemporadaDemanda.Alta: return Color.FromArgb(214, 158, 26);
                case TemporadaDemanda.Media: return Color.FromArgb(90, 140, 210);
                default: return Color.FromArgb(140, 150, 172);
            }
        }

        public static string Alerta(AlertaDemanda_68SA alerta, CultureInfo cultura)
        {
            string desde = alerta.Desde.ToString("ddd dd/MM", cultura);
            string hasta = alerta.Hasta.ToString("ddd dd/MM", cultura);
            string porcentaje = Porcentaje(alerta.Porcentaje, cultura);
            switch (alerta.Tipo)
            {
                case TipoAlertaDemanda.BajaOcupacion:
                    return string.Format(T("Analisis_alertaBaja"), desde, hasta, porcentaje, alerta.Dias);
                case TipoAlertaDemanda.Saturacion:
                    return alerta.Dias == 1
                        ? string.Format(T("Analisis_alertaSaturacionDia"), desde, porcentaje)
                        : string.Format(T("Analisis_alertaSaturacion"), desde, hasta, porcentaje, alerta.Dias);
                default:
                    return string.Format(T("Analisis_alertaFeriado"), T(alerta.Detalle), desde, porcentaje);
            }
        }

        public static Color ColorAlerta(TipoAlertaDemanda tipo)
        {
            switch (tipo)
            {
                case TipoAlertaDemanda.BajaOcupacion: return Color.FromArgb(52, 120, 200);
                case TipoAlertaDemanda.Saturacion: return Color.FromArgb(192, 57, 43);
                default: return Color.FromArgb(214, 158, 26);
            }
        }

        public static string Oferta(OfertaPersonalizada_68SA oferta, CultureInfo cultura)
        {
            string mes = cultura.DateTimeFormat.GetMonthName(Math.Max(1, Math.Min(12, oferta.MesPreferido)));
            switch (oferta.Oferta)
            {
                case TipoOfertaHuesped.UpgradeCortesia: return string.Format(T("Analisis_ofertaUpgrade"), oferta.TipoSuperior ?? "-");
                case TipoOfertaHuesped.PaqueteServicios: return T("Analisis_ofertaPaquete");
                case TipoOfertaHuesped.PreventaTemporada: return string.Format(T("Analisis_ofertaPreventa"), mes);
                default: return string.Format(T("Analisis_ofertaFidelidad"), mes);
            }
        }

        public static string Moneda(decimal valor, CultureInfo cultura) => "$" + valor.ToString("N0", cultura);

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        #endregion
    }

    /// <summary>
    /// Reporte impreso del análisis predictivo de ocupación y demanda: resumen, gráfico de la proyección,
    /// alertas, tabla día por día, demanda por tipo, estacionalidad, comportamiento de las reservas,
    /// ingresos y consumo, ofertas personalizadas y una nota sobre cómo se calcula.
    /// Las secciones se reparten solas en las páginas (si una no entra, pasa a la siguiente; las tablas
    /// largas siguen en la página siguiente con el encabezado repetido).
    /// </summary>
    public class AnalisisDemandaImpresor_68SA : IReporteImprimible_68SA
    {
        #region Campos

        private readonly AnalisisDemanda_68SA _a;
        private readonly string _usuario;
        private readonly CultureInfo _cultura = OpcionEnum_68SA.CulturaActual();

        private List<List<Elemento>> _paginas;
        private int _pagina;
        private Font _fTitulo, _fSubtitulo, _fInfo, _fEtiqueta, _fValor, _fSeccion, _fCabecera, _fCuerpo, _fCuerpoNegrita, _fChico;

        #endregion

        #region Propiedades

        public bool TieneDatos => _a.Dias.Count > 0 && _a.Capacidad > 0;

        #endregion

        #region Constructor

        public AnalisisDemandaImpresor_68SA(AnalisisDemanda_68SA analisis)
        {
            _a = analisis ?? new AnalisisDemanda_68SA();
            var actual = SessionManager_08YS.Instance.Current;
            _usuario = actual == null ? "-" : $"{actual.Nombre} {actual.Apellido}".Trim();
        }

        #endregion

        #region Estilo

        private static readonly Color ColorMarca = Color.FromArgb(10, 18, 50);
        private static readonly Color ColorDorado = Color.FromArgb(184, 134, 11);
        private static readonly Color ColorFilaAlterna = Color.FromArgb(244, 246, 250);
        private static readonly Color ColorBorde = Color.FromArgb(210, 214, 222);
        private static readonly Color ColorTextoSuave = Color.FromArgb(95, 100, 115);
        private static readonly Color ColorTexto = Color.FromArgb(25, 28, 38);
        private static readonly Color ColorReservado = Color.FromArgb(22, 40, 98);
        private static readonly Color ColorPorEntrar = Color.FromArgb(226, 186, 92);

        private const int AltoEncabezadoCompleto = 78;
        private const int AltoEncabezadoCorto = 34;
        private const int AltoPie = 30;
        private const int Separacion = 12;
        private const int AltoTitulo = 30;
        private const int AltoKpis = 64;
        private const int AltoCabecera = 24;
        private const int AltoFila = 20;

        #endregion

        #region Ciclo de impresión

        private void IniciarImpresion()
        {
            _paginas = null;
            _pagina = 0;
            LiberarFuentes();
            _fTitulo = new Font("Segoe UI", 15F, FontStyle.Bold);
            _fSubtitulo = new Font("Segoe UI", 10F, FontStyle.Bold);
            _fInfo = new Font("Segoe UI", 8F);
            _fEtiqueta = new Font("Segoe UI", 6.5F, FontStyle.Bold);
            _fValor = new Font("Segoe UI", 12F, FontStyle.Bold);
            _fSeccion = new Font("Segoe UI", 11F, FontStyle.Bold);
            _fCabecera = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            _fCuerpo = new Font("Segoe UI", 7.5F);
            _fCuerpoNegrita = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            _fChico = new Font("Segoe UI", 6.5F);
        }

        private void LiberarFuentes()
        {
            foreach (var f in new[] { _fTitulo, _fSubtitulo, _fInfo, _fEtiqueta, _fValor, _fSeccion, _fCabecera, _fCuerpo, _fCuerpoNegrita, _fChico })
                f?.Dispose();
            _fTitulo = _fSubtitulo = _fInfo = _fEtiqueta = _fValor = _fSeccion = _fCabecera = _fCuerpo = _fCuerpoNegrita = _fChico = null;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Rectangle area = e.MarginBounds;

            if (_paginas == null) _paginas = Paginar(area);
            if (_pagina >= _paginas.Count) { e.HasMorePages = false; return; }

            int y = _pagina == 0 ? DibujarEncabezadoCompleto(g, area) : DibujarEncabezadoCorto(g, area);
            foreach (var elemento in _paginas[_pagina])
            {
                elemento.Dibujar(g, new Rectangle(area.Left, y, area.Width, elemento.Alto));
                y += elemento.Alto;
            }

            _pagina++;
            DibujarPie(g, area);
            e.HasMorePages = _pagina < _paginas.Count;
        }

        /// <summary>Reparte los elementos en páginas: si uno no entra, pasa a la siguiente.</summary>
        private List<List<Elemento>> Paginar(Rectangle area)
        {
            var elementos = ArmarElementos(area.Width);
            var paginas = new List<List<Elemento>> { new List<Elemento>() };
            int limite = area.Bottom - AltoPie;
            int inicioPrimera = area.Top + AltoEncabezadoCompleto + Separacion;
            int inicioResto = area.Top + AltoEncabezadoCorto + Separacion;
            int y = inicioPrimera;

            for (int i = 0; i < elementos.Count; i++)
            {
                var el = elementos[i];
                int necesario = el.Alto;
                // Un título se lleva consigo al menos el elemento siguiente
                for (int j = i; j < elementos.Count - 1 && elementos[j].UnirConSiguiente; j++)
                    necesario += elementos[j + 1].Alto;

                var actual = paginas[paginas.Count - 1];
                if (y + necesario > limite && actual.Count > 0)
                {
                    actual = new List<Elemento>();
                    paginas.Add(actual);
                    y = inicioResto;
                    if (el.Cabecera != null)
                    {
                        actual.Add(el.Cabecera);
                        y += el.Cabecera.Alto;
                    }
                }
                actual.Add(el);
                y += el.Alto;
                if (paginas.Count > 500) break;   // resguardo
            }
            return paginas;
        }

        #endregion

        #region Armado de las secciones

        private List<Elemento> ArmarElementos(int ancho)
        {
            var lista = new List<Elemento>();

            // 1. Indicadores principales
            lista.Add(Kpis(new[]
            {
                (T("Analisis_kpiOcupacion"), Pct(_a.OcupacionProyectada),
                    string.Format(T("Analisis_kpiOcupacionDetalle"), Pct(_a.OcupacionHistorica)), AnalisisDemandaTextos_68SA.ColorOcupacion(_a.OcupacionProyectada)),
                (T("Analisis_kpiBaja"), _a.DiasBajaOcupacion.ToString(),
                    string.Format(T("Analisis_kpiDiasDe"), _a.Dias.Count), Color.FromArgb(52, 120, 200)),
                (T("Analisis_kpiSaturacion"), _a.DiasSaturados.ToString(),
                    string.Format(T("Analisis_kpiDiasDe"), _a.Dias.Count), Color.FromArgb(192, 57, 43)),
                (T("Analisis_kpiTipo"), _a.TipoMasDemandado ?? "-", T("Analisis_kpiTipoDetalle"), ColorDorado),
                (T("Analisis_kpiTemporada"), OpcionEnum_68SA.Texto(_a.TemporadaPredominante),
                    string.Format(T("Analisis_kpiCancelacion"), Pct(_a.TasaCancelacionUsada)), AnalisisDemandaTextos_68SA.ColorTemporada(_a.TemporadaPredominante)),
                (T("Analisis_kpiIngresos"), Moneda(_a.Ingresos.ProyeccionAlojamiento + _a.Ingresos.ProyeccionServicios),
                    string.Format(T("Analisis_kpiIngresosDetalle"), Moneda(_a.Ingresos.ProyeccionServicios)), Color.FromArgb(39, 174, 96)),
            }));
            lista.Add(Espacio());

            // 2. Proyección
            lista.Add(Titulo(T("Analisis_secProyeccion"), T("Analisis_secProyeccionDesc")));
            lista.Add(new Elemento { Alto = 236, Dibujar = DibujarGraficoProyeccion });
            lista.Add(Espacio());

            // 3. Alertas y recomendaciones
            lista.Add(Titulo(T("Analisis_secAlertas"), null));
            lista.Add(new Elemento { Alto = 52, Dibujar = DibujarResumenRecomendaciones });
            if (_a.Alertas.Count == 0)
                lista.Add(Linea(T("Analisis_sinAlertas"), ColorTextoSuave, Color.FromArgb(39, 174, 96)));
            foreach (var alerta in _a.Alertas)
                lista.Add(Linea(AnalisisDemandaTextos_68SA.Alerta(alerta, _cultura), ColorTexto, AnalisisDemandaTextos_68SA.ColorAlerta(alerta.Tipo)));
            lista.Add(Espacio());

            // 4. Tabla día por día
            lista.Add(Titulo(T("Analisis_secDiaria"), T("Analisis_secDiariaDesc")));
            AgregarTablaDiaria(lista);
            lista.Add(Espacio());

            // 5. Demanda por tipo de habitación
            lista.Add(Titulo(T("Analisis_secTipos"), T("Analisis_secTiposDesc")));
            AgregarTablaTipos(lista);
            lista.Add(Espacio());

            // 6. Estacionalidad
            lista.Add(Titulo(T("Analisis_secEstacionalidad"), T("Analisis_secEstacionalidadDesc")));
            lista.Add(new Elemento { Alto = 190, Dibujar = DibujarEstacionalidad });
            lista.Add(Espacio());

            // 7. Comportamiento de las reservas
            var c = _a.Comportamiento;
            lista.Add(Titulo(T("Analisis_secComportamiento"), T("Analisis_secComportamientoDesc")));
            lista.Add(Kpis(new[]
            {
                (T("Analisis_kpiReservas"), c.Reservas.ToString("N0", _cultura), string.Format(T("Analisis_kpiCanceladas"), c.Canceladas, Pct(c.TasaCancelacion)), ColorMarca),
                (T("Analisis_kpiAnticipacion"), string.Format(T("Analisis_dias"), c.AnticipacionPromedio.ToString("0.#", _cultura)),
                    string.Format(T("Analisis_kpiMediana"), c.AnticipacionMediana), ColorDorado),
                (T("Analisis_kpiDuracion"), string.Format(T("Analisis_noches"), c.DuracionPromedio.ToString("0.#", _cultura)),
                    string.Format(T("Analisis_kpiPersonas"), c.PersonasPromedio.ToString("0.#", _cultura)), Color.FromArgb(90, 140, 210)),
                (T("Analisis_kpiFinDeSemana"), Pct(c.PorcentajeFinDeSemana), T("Analisis_kpiFinDeSemanaDetalle"), Color.FromArgb(39, 174, 96)),
            }));
            lista.Add(new Elemento { Alto = 132, Dibujar = DibujarComportamiento });
            lista.Add(Espacio());

            // 8. Ingresos y consumo
            var ing = _a.Ingresos;
            lista.Add(Titulo(T("Analisis_secIngresos"), T("Analisis_secIngresosDesc")));
            lista.Add(Kpis(new[]
            {
                (T("Analisis_kpiAlojamiento"), Moneda(ing.Alojamiento), string.Format(T("Analisis_noches"), ing.NochesVendidas.ToString("N0", _cultura)), ColorMarca),
                (T("Analisis_kpiRoomService"), Moneda(ing.RoomService), string.Format(T("Analisis_kpiPorNoche"), Moneda(ing.ServiciosPorNoche)), ColorDorado),
                (T("Analisis_kpiUpgrades"), Moneda(ing.Upgrades), Pct(ing.Total > 0 ? Math.Round((ing.RoomService + ing.Upgrades) * 100m / ing.Total, 1) : 0m) + " " + T("Analisis_kpiDelTotal"), Color.FromArgb(90, 140, 210)),
                (T("Analisis_kpiADR"), Moneda(ing.ADR), T("Analisis_kpiADRDetalle"), Color.FromArgb(39, 174, 96)),
                (T("Analisis_kpiRevPAR"), Moneda(ing.RevPAR), T("Analisis_kpiRevPARDetalle"), Color.FromArgb(230, 126, 34)),
            }));
            lista.Add(new Elemento { Alto = 170, Dibujar = DibujarConsumo });
            lista.Add(Espacio());

            // 9. Ofertas personalizadas
            lista.Add(Titulo(T("Analisis_secOfertas"), T("Analisis_secOfertasDesc")));
            AgregarTablaOfertas(lista);
            lista.Add(Espacio());

            // 10. Cómo se calcula
            lista.Add(Titulo(T("Analisis_secMetodo"), null));
            lista.Add(new Elemento { Alto = 86, Dibujar = DibujarNotaMetodo });
            return lista;
        }

        private Elemento Espacio() => new Elemento { Alto = Separacion, Dibujar = (g, r) => { } };

        private Elemento Titulo(string titulo, string descripcion) => new Elemento
        {
            Alto = AltoTitulo,
            UnirConSiguiente = true,
            Dibujar = (g, r) =>
            {
                using (var acento = new SolidBrush(ColorDorado))
                    g.FillRectangle(acento, r.Left, r.Top + 5, 4, 16);
                using (var marca = new SolidBrush(ColorMarca))
                    g.DrawString(titulo, _fSeccion, marca, r.Left + 10, r.Top + 2);
                if (!string.IsNullOrEmpty(descripcion))
                {
                    float anchoTitulo = g.MeasureString(titulo, _fSeccion).Width;
                    using (var suave = new SolidBrush(ColorTextoSuave))
                    using (var formato = Recorte())
                        g.DrawString(descripcion, _fInfo, suave,
                            new RectangleF(r.Left + 18 + anchoTitulo, r.Top + 6, r.Width - anchoTitulo - 18, 16), formato);
                }
            }
        };

        private Elemento Linea(string texto, Color colorTexto, Color colorPunto) => new Elemento
        {
            Alto = 20,
            Dibujar = (g, r) =>
            {
                using (var punto = new SolidBrush(colorPunto))
                    g.FillEllipse(punto, r.Left + 6, r.Top + 6, 8, 8);
                using (var pincel = new SolidBrush(colorTexto))
                using (var formato = Recorte())
                    g.DrawString(texto, _fCuerpo, pincel, new RectangleF(r.Left + 22, r.Top + 3, r.Width - 24, 16), formato);
            }
        };

        private Elemento Kpis((string Etiqueta, string Valor, string Detalle, Color Acento)[] tarjetas) => new Elemento
        {
            Alto = AltoKpis + 4,
            Dibujar = (g, r) =>
            {
                const int hueco = 8;
                float ancho = (r.Width - hueco * (tarjetas.Length - 1)) / (float)tarjetas.Length;
                using (var borde = new Pen(ColorBorde))
                using (var suave = new SolidBrush(ColorTextoSuave))
                using (var texto = new SolidBrush(ColorTexto))
                using (var formato = Recorte())
                {
                    for (int i = 0; i < tarjetas.Length; i++)
                    {
                        var caja = new RectangleF(r.Left + i * (ancho + hueco), r.Top, ancho, AltoKpis);
                        g.DrawRectangle(borde, caja.X, caja.Y, caja.Width, caja.Height);
                        using (var acento = new SolidBrush(tarjetas[i].Acento))
                            g.FillRectangle(acento, caja.X, caja.Y, 3, caja.Height);
                        g.DrawString(tarjetas[i].Etiqueta.ToUpper(_cultura), _fEtiqueta, suave, new RectangleF(caja.X + 10, caja.Y + 6, caja.Width - 14, 12), formato);
                        g.DrawString(tarjetas[i].Valor, _fValor, texto, new RectangleF(caja.X + 10, caja.Y + 19, caja.Width - 14, 24), formato);
                        g.DrawString(tarjetas[i].Detalle, _fInfo, suave, new RectangleF(caja.X + 10, caja.Y + 44, caja.Width - 14, 14), formato);
                    }
                }
            }
        };

        #endregion

        #region Tablas

        private sealed class Columna
        {
            public string Clave;
            public float Peso;
            public StringAlignment Alineacion;
            public Columna(string clave, float peso, StringAlignment alineacion) { Clave = clave; Peso = peso; Alineacion = alineacion; }
        }

        /// <summary>Celda de una fila: texto, color y, opcionalmente, una barrita de porcentaje.</summary>
        private sealed class Celda
        {
            public string Texto;
            public Color Color;
            public bool Negrita;
            public decimal? Barra;       // 0-100: dibuja una barra de fondo
            public Color ColorBarra;
        }

        private Elemento CabeceraTabla(Columna[] columnas) => new Elemento
        {
            Alto = AltoCabecera,
            UnirConSiguiente = true,   // el encabezado nunca queda solo al pie de una página
            Dibujar = (g, r) =>
            {
                float[] anchos = Anchos(columnas, r.Width);
                using (var fondo = new SolidBrush(ColorMarca))
                    g.FillRectangle(fondo, r.Left, r.Top, r.Width, AltoCabecera);
                float x = r.Left;
                for (int i = 0; i < columnas.Length; i++)
                {
                    DibujarTexto(g, T(columnas[i].Clave), _fCabecera, Color.Goldenrod, new RectangleF(x, r.Top, anchos[i], AltoCabecera), columnas[i].Alineacion);
                    x += anchos[i];
                }
            }
        };

        private Elemento FilaTabla(Columna[] columnas, Celda[] celdas, bool alterna, Elemento cabecera, Color? fondoFila = null) => new Elemento
        {
            Alto = AltoFila,
            Cabecera = cabecera,
            Dibujar = (g, r) =>
            {
                float[] anchos = Anchos(columnas, r.Width);
                Color? fondo = fondoFila ?? (alterna ? ColorFilaAlterna : (Color?)null);
                if (fondo.HasValue)
                    using (var pincel = new SolidBrush(fondo.Value))
                        g.FillRectangle(pincel, r.Left, r.Top, r.Width, AltoFila);
                using (var borde = new Pen(ColorBorde))
                    g.DrawLine(borde, r.Left, r.Top + AltoFila, r.Right, r.Top + AltoFila);

                float x = r.Left;
                for (int i = 0; i < columnas.Length && i < celdas.Length; i++)
                {
                    var celda = celdas[i];
                    var rect = new RectangleF(x, r.Top, anchos[i], AltoFila);
                    if (celda.Barra.HasValue)
                    {
                        float largo = (anchos[i] - 12) * (float)Math.Max(0m, Math.Min(100m, celda.Barra.Value)) / 100f;
                        using (var barra = new SolidBrush(Color.FromArgb(70, celda.ColorBarra)))
                            g.FillRectangle(barra, x + 6, r.Top + 4, largo, AltoFila - 8);
                    }
                    DibujarTexto(g, celda.Texto, celda.Negrita ? _fCuerpoNegrita : _fCuerpo, celda.Color, rect, columnas[i].Alineacion);
                    x += anchos[i];
                }
            }
        };

        private Celda C(string texto, Color? color = null, bool negrita = false)
            => new Celda { Texto = texto ?? "-", Color = color ?? ColorTexto, Negrita = negrita };

        private void AgregarTablaDiaria(List<Elemento> lista)
        {
            var columnas = new[]
            {
                new Columna("Analisis_colFecha", 10.5f, StringAlignment.Near),
                new Columna("Analisis_colTemporada", 6.5f, StringAlignment.Near),
                new Columna("Analisis_colReservadas", 8f, StringAlignment.Center),
                new Columna("Analisis_colEstimada", 9.5f, StringAlignment.Center),
                new Columna("Analisis_colTipo", 12f, StringAlignment.Near),
                new Columna("Analisis_colHistorial", 7.5f, StringAlignment.Center),
                new Columna("Analisis_colConsumo", 8f, StringAlignment.Far),
                new Columna("Analisis_colSugerencia", 19.5f, StringAlignment.Near),
            };
            var cabecera = CabeceraTabla(columnas);
            lista.Add(cabecera);

            for (int i = 0; i < _a.Dias.Count; i++)
            {
                var d = _a.Dias[i];
                string fecha = d.Fecha.ToString("ddd dd/MM", _cultura);
                if (d.Feriado != null) fecha += " " + T("Analisis_marcaFeriado");
                else if (d.VisperaFeriado) fecha += " " + T("Analisis_marcaVispera");
                Color colorOcupacion = AnalisisDemandaTextos_68SA.ColorOcupacion(d.Porcentaje);

                var celdas = new[]
                {
                    C(fecha, d.FinDeSemana || d.Feriado != null ? ColorMarca : ColorTexto, d.FinDeSemana || d.Feriado != null),
                    C(OpcionEnum_68SA.Texto(d.Temporada), AnalisisDemandaTextos_68SA.ColorTemporada(d.Temporada), true),
                    C($"{d.Reservadas} · {Pct(d.PorcentajeReservado)}", ColorTextoSuave),
                    new Celda { Texto = Pct(d.Porcentaje), Color = colorOcupacion, Negrita = true, Barra = d.Porcentaje, ColorBarra = colorOcupacion },
                    C(d.TipoMasDemandado == null ? "-" : $"{d.TipoMasDemandado} ({Pct(d.PorcentajeTipoMasDemandado)})"),
                    C(d.PorcentajeAnioAnterior.HasValue ? Pct(d.PorcentajeAnioAnterior.Value) : "-", ColorTextoSuave),
                    C(Moneda(d.ConsumoEsperado), ColorTextoSuave),
                    C(AnalisisDemandaTextos_68SA.Sugerencia(d, _cultura), AnalisisDemandaTextos_68SA.ColorSugerencia(d.Sugerencia), true),
                };
                Color? fondo = d.Feriado != null ? Color.FromArgb(253, 246, 228) : (Color?)null;
                lista.Add(FilaTabla(columnas, celdas, i % 2 == 1, cabecera, fondo));
            }

            lista.Add(new Elemento
            {
                Alto = 18,
                Dibujar = (g, r) =>
                {
                    using (var suave = new SolidBrush(ColorTextoSuave))
                        g.DrawString(T("Analisis_referenciasTabla"), _fChico, suave, r.Left, r.Top + 4);
                }
            });
        }

        private void AgregarTablaTipos(List<Elemento> lista)
        {
            var columnas = new[]
            {
                new Columna("Analisis_colTipoHab", 13f, StringAlignment.Near),
                new Columna("Analisis_colHabitaciones", 5.5f, StringAlignment.Center),
                new Columna("Analisis_colReservasHist", 6.5f, StringAlignment.Center),
                new Columna("Analisis_colOcupHist", 10f, StringAlignment.Center),
                new Columna("Analisis_colOcupProy", 10f, StringAlignment.Center),
                new Columna("Analisis_colDiasDemandado", 7f, StringAlignment.Center),
                new Columna("Analisis_colTarifa", 8f, StringAlignment.Far),
                new Columna("Analisis_colTarifaSugerida", 9f, StringAlignment.Far),
                new Columna("Analisis_colIngresosHist", 10f, StringAlignment.Far),
            };
            var cabecera = CabeceraTabla(columnas);
            lista.Add(cabecera);
            int i = 0;
            foreach (var t in _a.Tipos)
            {
                Color colorProy = AnalisisDemandaTextos_68SA.ColorOcupacion(t.OcupacionProyectada);
                decimal variacion = t.Tarifa > 0 ? (t.TarifaSugerida - t.Tarifa) / t.Tarifa * 100m : 0m;
                string sugerida = Moneda(t.TarifaSugerida) + (variacion == 0 ? "" : $" ({(variacion > 0 ? "+" : "")}{variacion.ToString("0", _cultura)}%)");
                lista.Add(FilaTabla(columnas, new[]
                {
                    C(t.Nombre, ColorTexto, true),
                    C(t.Habitaciones.ToString()),
                    C(t.Reservas.ToString("N0", _cultura)),
                    new Celda { Texto = Pct(t.OcupacionHistorica), Color = ColorTexto, Barra = t.OcupacionHistorica, ColorBarra = ColorMarca },
                    new Celda { Texto = Pct(t.OcupacionProyectada), Color = colorProy, Negrita = true, Barra = t.OcupacionProyectada, ColorBarra = colorProy },
                    C(t.DiasMasDemandado.ToString()),
                    C(Moneda(t.Tarifa), ColorTextoSuave),
                    C(sugerida, variacion > 0 ? Color.FromArgb(176, 42, 42) : variacion < 0 ? Color.FromArgb(20, 80, 160) : ColorTexto, true),
                    C(Moneda(t.Ingresos)),
                }, i++ % 2 == 1, cabecera));
            }
        }

        private void AgregarTablaOfertas(List<Elemento> lista)
        {
            if (_a.Ofertas.Count == 0)
            {
                lista.Add(Linea(T("Analisis_sinOfertas"), ColorTextoSuave, ColorTextoSuave));
                return;
            }
            var columnas = new[]
            {
                new Columna("Analisis_colHuesped", 13f, StringAlignment.Near),
                new Columna("Analisis_colDocumento", 7f, StringAlignment.Near),
                new Columna("Analisis_colEstadias", 5f, StringAlignment.Center),
                new Columna("Analisis_colTipoPreferido", 10f, StringAlignment.Near),
                new Columna("Analisis_colMesPreferido", 10f, StringAlignment.Near),
                new Columna("Analisis_colGastoServicios", 8f, StringAlignment.Far),
                new Columna("Analisis_colGastoTotal", 8f, StringAlignment.Far),
                new Columna("Analisis_colPropuesta", 25f, StringAlignment.Near),
            };
            var cabecera = CabeceraTabla(columnas);
            lista.Add(cabecera);
            int i = 0;
            foreach (var o in _a.Ofertas)
            {
                string mes = _cultura.DateTimeFormat.GetMonthName(Math.Max(1, Math.Min(12, o.MesPreferido)));
                mes = mes.Length > 0 ? char.ToUpper(mes[0], _cultura) + mes.Substring(1) : mes;
                lista.Add(FilaTabla(columnas, new[]
                {
                    C(o.Huesped, ColorTexto, true),
                    C(o.Documento, ColorTextoSuave),
                    C(o.Estadias.ToString()),
                    C(o.TipoPreferido),
                    C($"{mes} ({OpcionEnum_68SA.Texto(o.TemporadaPreferida).ToLower(_cultura)})", AnalisisDemandaTextos_68SA.ColorTemporada(o.TemporadaPreferida)),
                    C(Moneda(o.GastoServiciosPorEstadia)),
                    C(Moneda(o.GastoTotal), ColorTextoSuave),
                    C(AnalisisDemandaTextos_68SA.Oferta(o, _cultura), ColorDorado, true),
                }, i++ % 2 == 1, cabecera));
            }
        }

        private static float[] Anchos(Columna[] columnas, int anchoTotal)
        {
            float peso = columnas.Sum(c => c.Peso);
            return columnas.Select(c => anchoTotal * c.Peso / peso).ToArray();
        }

        #endregion

        #region Gráficos

        /// <summary>Barras por día: reservado (azul) + lo que se espera que entre (dorado), con las líneas del 40% y 90%.</summary>
        private void DibujarGraficoProyeccion(Graphics g, Rectangle r)
        {
            var dias = _a.Dias;
            if (dias.Count == 0) return;

            var leyenda = new Rectangle(r.Left, r.Top, r.Width, 18);
            DibujarLeyenda(g, leyenda, new[]
            {
                (T("Analisis_leyReservado"), ColorReservado),
                (T("Analisis_leyPorEntrar"), ColorPorEntrar),
                (T("Analisis_leyUmbralBajo"), Color.FromArgb(52, 120, 200)),
                (T("Analisis_leyUmbralSaturacion"), Color.FromArgb(192, 57, 43)),
                (T("Analisis_leyFinDeSemana"), Color.FromArgb(232, 236, 244)),
            });

            var zona = new Rectangle(r.Left + 34, r.Top + 26, r.Width - 40, r.Height - 26 - 30);
            using (var ejes = new Pen(ColorBorde))
            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
            {
                for (int p = 0; p <= 100; p += 25)
                {
                    float y = zona.Bottom - zona.Height * p / 100f;
                    g.DrawLine(ejes, zona.Left, y, zona.Right, y);
                    g.DrawString(p + "%", _fChico, suave, new RectangleF(r.Left, y - 7, 30, 14), derecha);
                }
            }

            float paso = zona.Width / (float)dias.Count;
            float anchoBarra = Math.Max(2f, paso * 0.68f);
            int cadaCuanto = paso >= 22 ? 1 : paso >= 11 ? 2 : paso >= 6 ? 4 : 7;

            using (var reservado = new SolidBrush(ColorReservado))
            using (var porEntrar = new SolidBrush(ColorPorEntrar))
            using (var finde = new SolidBrush(Color.FromArgb(232, 236, 244)))
            using (var feriado = new SolidBrush(ColorDorado))
            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var centro = new StringFormat { Alignment = StringAlignment.Center })
            {
                for (int i = 0; i < dias.Count; i++)
                {
                    var d = dias[i];
                    float x = zona.Left + i * paso;
                    if (d.FinDeSemana)
                        g.FillRectangle(finde, x, zona.Top, paso, zona.Height);

                    float xb = x + (paso - anchoBarra) / 2;
                    float altoReservado = d.Capacidad > 0 ? zona.Height * (float)(d.ReservadasNetas / d.Capacidad) : 0;
                    float altoPorEntrar = d.Capacidad > 0 ? zona.Height * (float)(d.ReservasPorEntrar / d.Capacidad) : 0;
                    altoReservado = Math.Min(zona.Height, altoReservado);
                    altoPorEntrar = Math.Min(zona.Height - altoReservado, altoPorEntrar);
                    g.FillRectangle(reservado, xb, zona.Bottom - altoReservado, anchoBarra, altoReservado);
                    g.FillRectangle(porEntrar, xb, zona.Bottom - altoReservado - altoPorEntrar, anchoBarra, altoPorEntrar);

                    if (d.Feriado != null)
                    {
                        float cy = zona.Bottom - altoReservado - altoPorEntrar - 8;
                        g.FillPolygon(feriado, new[] { new PointF(xb + anchoBarra / 2, cy - 4), new PointF(xb + anchoBarra / 2 + 4, cy + 3), new PointF(xb + anchoBarra / 2 - 4, cy + 3) });
                    }

                    if (i % cadaCuanto == 0 || d.Fecha.Day == 1)
                    {
                        string etiqueta = d.Fecha.Day == 1 || i == 0 ? d.Fecha.ToString("dd/MM", _cultura) : d.Fecha.Day.ToString();
                        g.DrawString(etiqueta, _fChico, suave, new RectangleF(x - 12, zona.Bottom + 3, paso + 24, 12), centro);
                    }
                }
            }

            // Umbrales
            using (var bajo = new Pen(Color.FromArgb(52, 120, 200), 1.4f) { DashStyle = DashStyle.Dash })
            using (var alto = new Pen(Color.FromArgb(192, 57, 43), 1.4f) { DashStyle = DashStyle.Dash })
            {
                float yBajo = zona.Bottom - zona.Height * (float)BLL_08YS.Negocio.AnalisisDemandaBLL_68SA.UmbralBajo / 100f;
                float yAlto = zona.Bottom - zona.Height * (float)BLL_08YS.Negocio.AnalisisDemandaBLL_68SA.UmbralSaturacion / 100f;
                g.DrawLine(bajo, zona.Left, yBajo, zona.Right, yBajo);
                g.DrawLine(alto, zona.Left, yAlto, zona.Right, yAlto);
            }

            using (var suave = new SolidBrush(ColorTextoSuave))
                g.DrawString(string.Format(T("Analisis_notaGrafico"), _a.Capacidad), _fChico, suave, zona.Left, zona.Bottom + 16);
        }

        private void DibujarLeyenda(Graphics g, Rectangle r, (string Texto, Color Color)[] items)
        {
            float x = r.Left;
            using (var texto = new SolidBrush(ColorTextoSuave))
            {
                foreach (var item in items)
                {
                    using (var color = new SolidBrush(item.Color))
                        g.FillRectangle(color, x, r.Top + 4, 10, 10);
                    using (var borde = new Pen(ColorBorde))
                        g.DrawRectangle(borde, x, r.Top + 4, 10, 10);
                    g.DrawString(item.Texto, _fChico, texto, x + 14, r.Top + 3);
                    x += 14 + g.MeasureString(item.Texto, _fChico).Width + 16;
                }
            }
        }

        /// <summary>Dos gráficos: ocupación por mes (color = temporada) y por día de la semana.</summary>
        private void DibujarEstacionalidad(Graphics g, Rectangle r)
        {
            int hueco = 24;
            var izquierda = new Rectangle(r.Left, r.Top, (int)(r.Width * 0.62) - hueco / 2, r.Height);
            var derecha = new Rectangle(izquierda.Right + hueco, r.Top, r.Right - izquierda.Right - hueco, r.Height);

            var meses = _a.Meses.Select(m =>
            {
                string nombre = _cultura.DateTimeFormat.GetAbbreviatedMonthName(m.Mes).TrimEnd('.');
                return (Etiqueta: nombre.Length > 0 ? char.ToUpper(nombre[0], _cultura) + nombre.Substring(1) : nombre,
                        Valor: m.Ocupacion, Color: m.ConHistoria ? AnalisisDemandaTextos_68SA.ColorTemporada(m.Temporada) : Color.FromArgb(215, 219, 228),
                        Texto: m.ConHistoria ? Pct(m.Ocupacion) : T("Analisis_sinHistoria"));
            }).ToList();
            DibujarSubtitulo(g, izquierda, T("Analisis_graficoMeses"));
            DibujarBarras(g, new Rectangle(izquierda.Left, izquierda.Top + 20, izquierda.Width, izquierda.Height - 20), meses, 100m);
            var ley = new Rectangle(izquierda.Left + 160, izquierda.Top, izquierda.Width - 160, 16);
            DibujarLeyenda(g, ley, new[]
            {
                (OpcionEnum_68SA.Texto(TemporadaDemanda.Alta), AnalisisDemandaTextos_68SA.ColorTemporada(TemporadaDemanda.Alta)),
                (OpcionEnum_68SA.Texto(TemporadaDemanda.Media), AnalisisDemandaTextos_68SA.ColorTemporada(TemporadaDemanda.Media)),
                (OpcionEnum_68SA.Texto(TemporadaDemanda.Baja), AnalisisDemandaTextos_68SA.ColorTemporada(TemporadaDemanda.Baja)),
            });

            var nombresDias = Enumerable.Range(0, 7).Select(i =>
            {
                var dia = (DayOfWeek)((i + 1) % 7);   // 0 = lunes
                string nombre = _cultura.DateTimeFormat.GetAbbreviatedDayName(dia).TrimEnd('.');
                return nombre.Length > 0 ? char.ToUpper(nombre[0], _cultura) + nombre.Substring(1) : nombre;
            }).ToArray();
            var dias = Enumerable.Range(0, 7).Select(i =>
                (Etiqueta: nombresDias[i], Valor: _a.OcupacionPorDiaSemana[i],
                 Color: i >= 4 && i <= 5 ? ColorDorado : ColorReservado, Texto: Pct(_a.OcupacionPorDiaSemana[i]))).ToList();
            DibujarSubtitulo(g, derecha, T("Analisis_graficoDiasSemana"));
            DibujarBarras(g, new Rectangle(derecha.Left, derecha.Top + 20, derecha.Width, derecha.Height - 20), dias, 100m);
        }

        private void DibujarSubtitulo(Graphics g, Rectangle r, string texto)
        {
            using (var marca = new SolidBrush(ColorMarca))
                g.DrawString(texto, _fCuerpoNegrita, marca, r.Left, r.Top);
        }

        /// <summary>Barras verticales simples con el valor arriba y la etiqueta abajo.</summary>
        private void DibujarBarras(Graphics g, Rectangle r, List<(string Etiqueta, decimal Valor, Color Color, string Texto)> barras, decimal maximo)
        {
            if (barras.Count == 0) return;
            decimal tope = Math.Max(maximo, barras.Max(b => b.Valor));
            if (tope <= 0) tope = 1;
            var zona = new Rectangle(r.Left, r.Top + 14, r.Width, r.Height - 14 - 16);
            float paso = zona.Width / (float)barras.Count;
            float ancho = Math.Max(4f, paso * 0.6f);

            using (var eje = new Pen(ColorBorde))
                g.DrawLine(eje, zona.Left, zona.Bottom, zona.Right, zona.Bottom);
            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var texto = new SolidBrush(ColorTexto))
            using (var centro = new StringFormat { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
            {
                for (int i = 0; i < barras.Count; i++)
                {
                    var b = barras[i];
                    float x = zona.Left + i * paso;
                    float alto = zona.Height * (float)(Math.Max(0m, b.Valor) / tope);
                    using (var pincel = new SolidBrush(b.Color))
                        g.FillRectangle(pincel, x + (paso - ancho) / 2, zona.Bottom - alto, ancho, alto);
                    g.DrawString(b.Texto, _fChico, texto, new RectangleF(x - 4, zona.Bottom - alto - 13, paso + 8, 12), centro);
                    g.DrawString(b.Etiqueta, _fChico, suave, new RectangleF(x - 4, zona.Bottom + 3, paso + 8, 12), centro);
                }
            }
        }

        /// <summary>Barras horizontales (anticipación y llegadas).</summary>
        private void DibujarBarrasHorizontales(Graphics g, Rectangle r, string titulo, (string Etiqueta, int Valor)[] filas, Color color)
        {
            DibujarSubtitulo(g, r, titulo);
            int total = Math.Max(1, filas.Sum(f => f.Valor));
            int maximo = Math.Max(1, filas.Max(f => f.Valor));
            int y = r.Top + 22;
            int alto = Math.Max(12, Math.Min(18, (r.Height - 24) / Math.Max(1, filas.Length) - 4));
            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var texto = new SolidBrush(ColorTexto))
            using (var fondo = new SolidBrush(ColorFilaAlterna))
            using (var pincel = new SolidBrush(color))
            using (var formato = Recorte())
            {
                foreach (var f in filas)
                {
                    g.DrawString(f.Etiqueta, _fChico, suave, new RectangleF(r.Left, y + 2, 96, alto), formato);
                    var barra = new RectangleF(r.Left + 100, y, r.Width - 100 - 70, alto);
                    g.FillRectangle(fondo, barra);
                    g.FillRectangle(pincel, barra.X, barra.Y, barra.Width * f.Valor / maximo, barra.Height);
                    g.DrawString($"{f.Valor} ({(f.Valor * 100.0 / total).ToString("0", _cultura)}%)", _fChico, texto, barra.Right + 6, y + 2);
                    y += alto + 4;
                }
            }
        }

        private void DibujarComportamiento(Graphics g, Rectangle r)
        {
            var c = _a.Comportamiento;
            int hueco = 24;
            var izquierda = new Rectangle(r.Left, r.Top + 8, (r.Width - hueco) / 2, r.Height - 8);
            var derecha = new Rectangle(izquierda.Right + hueco, r.Top + 8, r.Right - izquierda.Right - hueco, r.Height - 8);

            DibujarBarrasHorizontales(g, izquierda, T("Analisis_graficoAnticipacion"), new[]
            {
                (T("Analisis_tramo0"), c.AnticipacionTramos[0]),
                (T("Analisis_tramo1"), c.AnticipacionTramos[1]),
                (T("Analisis_tramo2"), c.AnticipacionTramos[2]),
                (T("Analisis_tramo3"), c.AnticipacionTramos[3]),
            }, ColorDorado);

            var filas = Enumerable.Range(0, 7).Select(i =>
            {
                string nombre = _cultura.DateTimeFormat.GetDayName((DayOfWeek)((i + 1) % 7));
                return (nombre.Length > 0 ? char.ToUpper(nombre[0], _cultura) + nombre.Substring(1) : nombre, c.LlegadasPorDia[i]);
            }).ToArray();
            DibujarBarrasHorizontales(g, derecha, T("Analisis_graficoLlegadas"), filas, ColorReservado);
        }

        private void DibujarConsumo(Graphics g, Rectangle r)
        {
            int hueco = 24;
            var izquierda = new Rectangle(r.Left, r.Top + 8, (int)(r.Width * 0.58) - hueco / 2, r.Height - 8);
            var derecha = new Rectangle(izquierda.Right + hueco, r.Top + 8, r.Right - izquierda.Right - hueco, r.Height - 8);

            // Tendencia: consumo de servicios por noche ocupada, mes a mes (últimos 12)
            var meses = _a.ConsumoPorMes.Skip(Math.Max(0, _a.ConsumoPorMes.Count - 12)).Select(m =>
            {
                string nombre = m.Mes.ToString("MMM yy", _cultura).Replace(".", "");
                return (Etiqueta: nombre, Valor: m.PorNoche, Color: ColorDorado, Texto: Moneda(m.PorNoche));
            }).ToList();
            DibujarSubtitulo(g, izquierda, T("Analisis_graficoConsumo"));
            if (meses.Count == 0 || meses.All(m => m.Valor == 0))
                using (var suave = new SolidBrush(ColorTextoSuave))
                    g.DrawString(T("Analisis_sinConsumo"), _fCuerpo, suave, izquierda.Left, izquierda.Top + 30);
            else
                DibujarBarras(g, new Rectangle(izquierda.Left, izquierda.Top + 20, izquierda.Width, izquierda.Height - 20), meses, 0m);

            // Productos más pedidos
            if (_a.Productos.Count == 0)
            {
                DibujarSubtitulo(g, derecha, T("Analisis_graficoProductos"));
                using (var suave = new SolidBrush(ColorTextoSuave))
                    g.DrawString(T("Analisis_sinConsumo"), _fCuerpo, suave, derecha.Left, derecha.Top + 30);
                return;
            }
            DibujarBarrasHorizontales(g, derecha, T("Analisis_graficoProductos"),
                _a.Productos.Take(6).Select(p => (p.Nombre, p.Cantidad)).ToArray(), Color.FromArgb(90, 140, 210));
        }

        private void DibujarResumenRecomendaciones(Graphics g, Rectangle r)
        {
            int subir = _a.Dias.Count(d => d.Sugerencia == SugerenciaPrecio.Aumento || d.Sugerencia == SugerenciaPrecio.AumentoFuerte);
            int bajar = _a.Dias.Count(d => d.Sugerencia == SugerenciaPrecio.DescuentoFuerte);
            int promo = _a.Dias.Count(d => d.Sugerencia == SugerenciaPrecio.Promocion);
            int mantener = _a.Dias.Count(d => d.Sugerencia == SugerenciaPrecio.Mantener);
            var cajas = new[]
            {
                (T("Analisis_recSubir"), subir, AnalisisDemandaTextos_68SA.ColorSugerencia(SugerenciaPrecio.Aumento)),
                (T("Analisis_recMantener"), mantener, AnalisisDemandaTextos_68SA.ColorSugerencia(SugerenciaPrecio.Mantener)),
                (T("Analisis_recPromo"), promo, AnalisisDemandaTextos_68SA.ColorSugerencia(SugerenciaPrecio.Promocion)),
                (T("Analisis_recDescuento"), bajar, AnalisisDemandaTextos_68SA.ColorSugerencia(SugerenciaPrecio.DescuentoFuerte)),
            };
            const int hueco = 8;
            float ancho = (r.Width - hueco * (cajas.Length - 1)) / (float)cajas.Length;
            using (var texto = new SolidBrush(ColorTexto))
            using (var formato = Recorte())
            {
                for (int i = 0; i < cajas.Length; i++)
                {
                    var caja = new RectangleF(r.Left + i * (ancho + hueco), r.Top, ancho, 42);
                    using (var fondo = new SolidBrush(Color.FromArgb(22, cajas[i].Item3)))
                        g.FillRectangle(fondo, caja);
                    using (var acento = new SolidBrush(cajas[i].Item3))
                    {
                        g.FillRectangle(acento, caja.X, caja.Y, 3, caja.Height);
                        g.DrawString(cajas[i].Item2.ToString(), _fValor, acento, caja.X + 10, caja.Y + 8);
                    }
                    float anchoNumero = g.MeasureString(cajas[i].Item2.ToString(), _fValor).Width;
                    g.DrawString(cajas[i].Item1, _fCuerpo, texto, new RectangleF(caja.X + 14 + anchoNumero, caja.Y + 14, caja.Width - anchoNumero - 18, 16), formato);
                }
            }
        }

        private void DibujarNotaMetodo(Graphics g, Rectangle r)
        {
            string texto = string.Format(T("Analisis_notaMetodo"), _a.NochesHistoria, Pct(_a.TasaCancelacionUsada));
            using (var fondo = new SolidBrush(ColorFilaAlterna))
                g.FillRectangle(fondo, r.Left, r.Top, r.Width, r.Height - 6);
            using (var suave = new SolidBrush(ColorTextoSuave))
                g.DrawString(texto, _fInfo, suave, new RectangleF(r.Left + 10, r.Top + 6, r.Width - 20, r.Height - 16));
        }

        #endregion

        #region Encabezado y pie

        private int DibujarEncabezadoCompleto(Graphics g, Rectangle area)
        {
            var banda = new Rectangle(area.Left, area.Top, area.Width, AltoEncabezadoCompleto);
            using (var fondo = new SolidBrush(ColorMarca))
                g.FillRectangle(fondo, banda);
            using (var linea = new SolidBrush(ColorDorado))
                g.FillRectangle(linea, banda.Left, banda.Bottom - 4, banda.Width, 4);

            g.DrawString(T("Reporte_hotel"), _fTitulo, Brushes.White, banda.Left + 16, banda.Top + 10);
            using (var dorado = new SolidBrush(Color.Goldenrod))
                g.DrawString(T("Analisis_titulo"), _fSubtitulo, dorado, banda.Left + 18, banda.Top + 40);

            var f = _a.Filtro ?? new FiltroAnalisisDemanda_68SA();
            string proyeccion = string.Format(T("Analisis_infoProyeccion"), f.Desde.ToString("dd/MM/yyyy"), f.Hasta.ToString("dd/MM/yyyy"), _a.Dias.Count);
            string historia = f.HistorialCompleto
                ? string.Format(T("Analisis_infoHistoriaCompleta"), _a.NochesHistoria, _a.TipoAnalizado ?? T("Analisis_todosLosTipos"), _a.Capacidad)
                : string.Format(T("Analisis_infoHistoria"), f.MesesHistorial, _a.NochesHistoria,
                                            _a.TipoAnalizado ?? T("Analisis_todosLosTipos"), _a.Capacidad);
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            using (var claro = new SolidBrush(Color.FromArgb(200, 205, 220)))
            {
                var zona = new RectangleF(banda.Left, banda.Top + 12, banda.Width - 16, 18);
                g.DrawString(proyeccion, _fSubtitulo, Brushes.White, zona, derecha);
                zona.Y += 22;
                g.DrawString(historia, _fInfo, claro, zona, derecha);
                zona.Y += 16;
                g.DrawString(T("Analisis_infoCriterio"), _fInfo, claro, zona, derecha);
            }
            return banda.Bottom + Separacion;
        }

        private int DibujarEncabezadoCorto(Graphics g, Rectangle area)
        {
            using (var marca = new SolidBrush(ColorMarca))
                g.DrawString($"{T("Reporte_hotel")} — {T("Analisis_titulo")} {T("Reporte_continuacion")}", _fSubtitulo, marca, area.Left, area.Top + 4);
            using (var linea = new Pen(ColorDorado, 2))
                g.DrawLine(linea, area.Left, area.Top + AltoEncabezadoCorto - 6, area.Right, area.Top + AltoEncabezadoCorto - 6);
            return area.Top + AltoEncabezadoCorto + Separacion;
        }

        private void DibujarPie(Graphics g, Rectangle area)
        {
            int y = area.Bottom - AltoPie + 10;
            using (var linea = new Pen(ColorBorde))
                g.DrawLine(linea, area.Left, y, area.Right, y);
            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            {
                g.DrawString(string.Format(T("Reporte_generado"), _a.Generado.ToString("dd/MM/yyyy HH:mm"), _usuario), _fInfo, suave, area.Left, y + 4);
                g.DrawString(string.Format(T("Reporte_pagina"), _pagina, _paginas?.Count ?? _pagina), _fInfo, suave,
                    new RectangleF(area.Left, y + 4, area.Width, 16), derecha);
            }
        }

        #endregion

        #region Auxiliares

        private string Pct(decimal valor) => AnalisisDemandaTextos_68SA.Porcentaje(valor, _cultura);
        private string Moneda(decimal valor) => AnalisisDemandaTextos_68SA.Moneda(valor, _cultura);

        private static StringFormat Recorte()
            => new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };

        private static void DibujarTexto(Graphics g, string texto, Font fuente, Color color, RectangleF celda, StringAlignment alineacion)
        {
            using (var pincel = new SolidBrush(color))
            using (var formato = new StringFormat
            {
                Alignment = alineacion,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap
            })
            {
                g.DrawString(texto ?? string.Empty, fuente, pincel, new RectangleF(celda.X + 6, celda.Y, Math.Max(0, celda.Width - 12), celda.Height), formato);
            }
        }

        #endregion

        #region Métodos públicos

        public PrintDocument CrearDocumento()
        {
            var documento = new PrintDocument { DocumentName = T("Analisis_titulo") };
            documento.DefaultPageSettings.Landscape = true;
            documento.DefaultPageSettings.Margins = new Margins(40, 40, 36, 36);
            documento.BeginPrint += (s, e) => IniciarImpresion();
            documento.EndPrint += (s, e) => LiberarFuentes();
            documento.PrintPage += ImprimirPagina;
            return documento;
        }

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        #endregion

        #region Tipos anidados

        /// <summary>Una porción del reporte con alto fijo; las filas de tabla recuerdan su encabezado.</summary>
        private sealed class Elemento
        {
            public int Alto;
            public Action<Graphics, Rectangle> Dibujar;
            /// <summary>No se separa del elemento siguiente (títulos de sección).</summary>
            public bool UnirConSiguiente;
            /// <summary>Encabezado de tabla que se repite si la fila pasa a otra página.</summary>
            public Elemento Cabecera;
        }

        #endregion
    }
}
