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
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    /// <summary>Textos y colores de los servicios, compartidos por la pantalla del reporte y la impresión.</summary>
    public static class ReporteServiciosTextos_68SA
    {
        #region Métodos públicos

        public static string Descripcion(ServicioConsumido_68SA s)
        {
            switch (s.Tipo)
            {
                case TipoCargo.RoomService:
                    return string.Format(T("RepServ_descPedido"), (s.PedidoId ?? 0).ToString("00000"));
                case TipoCargo.Upgrade:
                    return string.Format(T("RepServ_descUpgrade"), s.TipoHabitacionNueva);
                default:
                    return s.Descripcion;
            }
        }

        /// <summary>Cantidad / detalle: productos del pedido o habitaciones y noches del upgrade.</summary>
        public static string Detalle(ServicioConsumido_68SA s, Func<decimal, string> moneda)
        {
            switch (s.Tipo)
            {
                case TipoCargo.RoomService:
                    return string.Format(T("RepServ_detPedido"), s.Cantidad, s.Detalle);
                case TipoCargo.Upgrade:
                    return string.Format(T("RepServ_detUpgrade"), s.HabitacionAnterior, s.NroHabitacion, s.Cantidad,
                        moneda(s.DiferenciaTarifa ?? 0));
                default:
                    return s.Detalle;
            }
        }

        public static Color ColorTipo(TipoCargo tipo)
            => tipo == TipoCargo.Upgrade ? Color.FromArgb(160, 125, 235) : Color.FromArgb(76, 217, 100);

        public static Color ColorEstado(EstadoServicio estado)
        {
            switch (estado)
            {
                case EstadoServicio.Solicitado: return Color.FromArgb(240, 170, 60);
                case EstadoServicio.Entregado: return Color.FromArgb(76, 217, 100);
                default: return Color.FromArgb(90, 155, 235);
            }
        }

        // Mismos colores, más oscuros, para que se lean sobre papel blanco
        public static Color ColorTipoImpresion(TipoCargo tipo)
            => tipo == TipoCargo.Upgrade ? Color.FromArgb(105, 60, 170) : Color.FromArgb(20, 120, 55);

        public static Color ColorEstadoImpresion(EstadoServicio estado)
        {
            switch (estado)
            {
                case EstadoServicio.Solicitado: return Color.FromArgb(190, 110, 10);
                case EstadoServicio.Entregado: return Color.FromArgb(20, 130, 60);
                default: return Color.FromArgb(30, 90, 180);
            }
        }

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        #endregion
    }

    /// <summary>
    /// Reporte impreso de servicios consumidos por huésped: vista previa antes de imprimir, resumen del período,
    /// detalle agrupado por huésped (con su total acumulado), total general y pie "Página X de Y".
    /// Se imprime en cualquier impresora o en "Microsoft Print to PDF". Mismo estilo que el reporte de reservas.
    /// </summary>
    public class ReporteServiciosImpresor_68SA : GUI_08YS.Reportes.IReporteImprimible_68SA
    {
        #region Campos

        private readonly ReporteServicios_68SA _reporte;
        private readonly List<HuespedServicios_68SA> _huespedes;
        private readonly string _usuario;
        private readonly CultureInfo _cultura = OpcionEnum_68SA.CulturaActual();

        private List<List<Renglon>> _paginas;
        private int _pagina;

        private Font _fTitulo, _fSubtitulo, _fInfo, _fEtiqueta, _fValor, _fCabecera, _fCuerpo, _fCuerpoNegrita, _fGrupo;

        #endregion

        #region Propiedades

        public bool TieneDatos => !_reporte.Vacio;

        #endregion

        #region Constructor

        public ReporteServiciosImpresor_68SA(ReporteServicios_68SA reporte)
        {
            _reporte = reporte ?? new ReporteServicios_68SA();
            _huespedes = _reporte.Huespedes;
            var actual = SessionManager_08YS.Instance.Current;
            _usuario = actual == null ? "-" : $"{actual.Nombre} {actual.Apellido}".Trim();
        }

        #endregion

        #region Estilo

        private static readonly Color ColorMarca = Color.FromArgb(10, 18, 50);
        private static readonly Color ColorDorado = Color.FromArgb(184, 134, 11);
        private static readonly Color ColorGrupo = Color.FromArgb(234, 237, 244);
        private static readonly Color ColorBorde = Color.FromArgb(210, 214, 222);
        private static readonly Color ColorTextoSuave = Color.FromArgb(95, 100, 115);
        private static readonly Color ColorTexto = Color.FromArgb(25, 28, 38);

        private const int AltoCabeceraTabla = 24;
        private const int AltoGrupo = 24;
        private const int AltoSubtotal = 22;
        private const int AltoTotalGeneral = 28;
        private const int AltoFilaMinimo = 20;
        private const int AltoPie = 30;
        private const int AltoEncabezadoCompleto = 70;
        private const int AltoEncabezadoCorto = 34;
        private const int AltoResumen = 64;
        private const int AltoTopProductos = 20;
        private const int Separacion = 12;

        private static readonly (string Clave, float Peso, StringAlignment Alineacion)[] Columnas =
        {
            ("RepServ_colFecha",       10.5f, StringAlignment.Near),
            ("RepServ_colHabitacion",   7f,   StringAlignment.Center),
            ("RepServ_colTipo",        11f,   StringAlignment.Near),
            ("RepServ_colDescripcion", 17f,   StringAlignment.Near),
            ("RepServ_colDetalle",     33f,   StringAlignment.Near),
            ("RepServ_colEstado",       9.5f, StringAlignment.Near),
            ("RepServ_colMonto",       10f,   StringAlignment.Far),
        };
        private const int ColDetalle = 4;

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
            _fCabecera = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            _fCuerpo = new Font("Segoe UI", 7.5F);
            _fCuerpoNegrita = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            _fGrupo = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        }

        private void LiberarFuentes()
        {
            foreach (var f in new[] { _fTitulo, _fSubtitulo, _fInfo, _fEtiqueta, _fValor, _fCabecera, _fCuerpo, _fCuerpoNegrita, _fGrupo })
                f?.Dispose();
            _fTitulo = _fSubtitulo = _fInfo = _fEtiqueta = _fValor = _fCabecera = _fCuerpo = _fCuerpoNegrita = _fGrupo = null;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            Rectangle area = e.MarginBounds;
            if (_paginas == null) _paginas = Paginar(g, area);
            _pagina++;

            int y = _pagina == 1 ? DibujarEncabezadoCompleto(g, area) : DibujarEncabezadoCorto(g, area);
            if (_pagina == 1) y = DibujarResumen(g, area, y);

            float[] anchos = CalcularAnchos(area.Width);
            var renglones = _paginas[_pagina - 1];
            if (renglones.Count > 0)
            {
                y = DibujarCabeceraTabla(g, area.Left, y, anchos);
                foreach (var renglon in renglones)
                {
                    DibujarRenglon(g, area.Left, y, anchos, renglon);
                    y += renglon.Alto;
                }
            }

            DibujarPie(g, area);
            e.HasMorePages = _pagina < _paginas.Count;
        }

        /// <summary>
        /// Arma de antemano todas las páginas (así el pie puede decir "Página X de Y"). Un huésped nunca
        /// queda con su título solo al pie de una hoja; si sus servicios siguen en la hoja siguiente, se
        /// repite el título como "(continúa)".
        /// </summary>
        private List<List<Renglon>> Paginar(Graphics g, Rectangle area)
        {
            float[] anchos = CalcularAnchos(area.Width);
            var todos = new List<Renglon>();
            foreach (var h in _huespedes)
            {
                todos.Add(new Renglon { Tipo = TipoRenglon.Grupo, Huesped = h, Alto = AltoGrupo });
                foreach (var s in h.Servicios)
                    todos.Add(new Renglon { Tipo = TipoRenglon.Servicio, Huesped = h, Servicio = s, Alto = AltoServicio(g, s, anchos[ColDetalle]) });
                todos.Add(new Renglon { Tipo = TipoRenglon.Subtotal, Huesped = h, Alto = AltoSubtotal });
            }
            todos.Add(new Renglon { Tipo = TipoRenglon.TotalGeneral, Alto = AltoTotalGeneral });

            int limite = area.Bottom - AltoPie;
            int inicioPrimera = area.Top + AltoEncabezadoCompleto + Separacion + AltoResumen + AltoTopProductos + Separacion * 2;
            int inicioOtras = area.Top + AltoEncabezadoCorto + Separacion;

            var paginas = new List<List<Renglon>>();
            var pagina = new List<Renglon>();
            int y = inicioPrimera + AltoCabeceraTabla;

            for (int i = 0; i < todos.Count; i++)
            {
                var r = todos[i];
                // El título del huésped necesita lugar para al menos su primer servicio
                int necesario = r.Alto + (r.Tipo == TipoRenglon.Grupo && i + 1 < todos.Count ? todos[i + 1].Alto : 0);

                if (y + necesario > limite && pagina.Count > 0)
                {
                    paginas.Add(pagina);
                    pagina = new List<Renglon>();
                    y = inicioOtras + AltoCabeceraTabla;
                    if (r.Tipo == TipoRenglon.Servicio || r.Tipo == TipoRenglon.Subtotal)
                    {
                        pagina.Add(new Renglon { Tipo = TipoRenglon.Grupo, Huesped = r.Huesped, Alto = AltoGrupo, Continuacion = true });
                        y += AltoGrupo;
                    }
                }
                pagina.Add(r);
                y += r.Alto;
                if (paginas.Count > 1000) break; // resguardo ante un papel sin espacio útil
            }
            if (pagina.Count > 0) paginas.Add(pagina);
            return paginas;
        }

        private int AltoServicio(Graphics g, ServicioConsumido_68SA s, float anchoDetalle)
        {
            string detalle = ReporteServiciosTextos_68SA.Detalle(s, Moneda);
            SizeF medida = g.MeasureString(detalle, _fCuerpo, (int)Math.Max(10, anchoDetalle - 8));
            int lineas = Math.Min(3, Math.Max(1, (int)Math.Ceiling(medida.Height / _fCuerpo.GetHeight(g) - 0.05)));
            return Math.Max(AltoFilaMinimo, 8 + lineas * (int)Math.Ceiling(_fCuerpo.GetHeight(g)));
        }

        #endregion

        #region Encabezado, resumen y pie

        private int DibujarEncabezadoCompleto(Graphics g, Rectangle area)
        {
            var banda = new Rectangle(area.Left, area.Top, area.Width, AltoEncabezadoCompleto);
            using (var fondo = new SolidBrush(ColorMarca))
                g.FillRectangle(fondo, banda);
            using (var linea = new SolidBrush(ColorDorado))
                g.FillRectangle(linea, banda.Left, banda.Bottom - 4, banda.Width, 4);

            g.DrawString(T("Reporte_hotel"), _fTitulo, Brushes.White, banda.Left + 16, banda.Top + 10);
            using (var dorado = new SolidBrush(Color.Goldenrod))
                g.DrawString(T("RepServ_titulo"), _fSubtitulo, dorado, banda.Left + 18, banda.Top + 40);

            var filtro = _reporte.Filtro;
            string periodo = filtro.Desde.HasValue && filtro.Hasta.HasValue
                ? string.Format(T("Reporte_periodo"), filtro.Desde.Value.ToString("dd/MM/yyyy"), filtro.Hasta.Value.ToString("dd/MM/yyyy"))
                : T("Reporte_periodoHistorico");

            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            using (var claro = new SolidBrush(Color.FromArgb(200, 205, 220)))
            {
                var zona = new RectangleF(banda.Left, banda.Top + 12, banda.Width - 16, 18);
                g.DrawString(periodo, _fSubtitulo, Brushes.White, zona, derecha);
                zona.Y += 22;
                g.DrawString(TextoCriterio(), _fInfo, claro, zona, derecha);
            }

            return banda.Bottom + Separacion;
        }

        private string TextoCriterio()
        {
            var f = _reporte.Filtro;
            string tipo = f.Tipo.HasValue ? OpcionEnum_68SA.Texto(f.Tipo.Value) : T("RepServ_txtTodos");
            string estado = f.Estado.HasValue ? OpcionEnum_68SA.Texto(f.Estado.Value) : T("RepServ_txtTodos");
            string huesped = string.IsNullOrWhiteSpace(f.Huesped) ? T("RepServ_txtTodos") : $"\"{f.Huesped}\"";
            return string.Format(T("RepServ_criterio"), tipo, estado, huesped);
        }

        private int DibujarEncabezadoCorto(Graphics g, Rectangle area)
        {
            using (var marca = new SolidBrush(ColorMarca))
                g.DrawString($"{T("Reporte_hotel")} — {T("RepServ_titulo")} {T("Reporte_continuacion")}", _fSubtitulo, marca, area.Left, area.Top + 4);
            using (var linea = new Pen(ColorDorado, 2))
                g.DrawLine(linea, area.Left, area.Top + AltoEncabezadoCorto - 6, area.Right, area.Top + AltoEncabezadoCorto - 6);
            return area.Top + AltoEncabezadoCorto + Separacion;
        }

        private int DibujarResumen(Graphics g, Rectangle area, int y)
        {
            var r = _reporte;
            var tarjetas = new[]
            {
                (T("RepServ_kpiTotal"), Moneda(r.Total),
                    string.Format(T("RepServ_kpiTotalDetalle"), r.Servicios.Count, r.CantidadEstadias)),
                (T("RepServ_kpiRoomService"), Moneda(r.Monto(TipoCargo.RoomService)),
                    string.Format(T("RepServ_kpiRoomServiceDetalle"), r.Cantidad(TipoCargo.RoomService), r.Pendientes)),
                (T("RepServ_kpiUpgrade"), Moneda(r.Monto(TipoCargo.Upgrade)),
                    string.Format(T("RepServ_kpiUpgradeDetalle"), r.Cantidad(TipoCargo.Upgrade))),
                (T("RepServ_kpiHuespedes"), r.CantidadHuespedes.ToString("N0", _cultura),
                    string.Format(T("RepServ_kpiHuespedesDetalle"), Moneda(r.PromedioPorHuesped))),
                (T("RepServ_kpiParticipacion"), r.Participacion.ToString("0.#", _cultura) + "%",
                    string.Format(T("RepServ_kpiParticipacionDetalle"), Moneda(r.AlojamientoPeriodo))),
                (T("RepServ_kpiEntrega"),
                    r.MinutosEntregaPromedio.HasValue ? string.Format(T("RepServ_txtMinutos"), Math.Round(r.MinutosEntregaPromedio.Value)) : "—",
                    T(r.MinutosEntregaPromedio.HasValue ? "RepServ_kpiEntregaDetalle" : "RepServ_kpiEntregaSinDatos")),
            };

            const int hueco = 8;
            float ancho = (area.Width - hueco * (tarjetas.Length - 1)) / (float)tarjetas.Length;
            using (var borde = new Pen(ColorBorde))
            using (var acento = new SolidBrush(ColorDorado))
            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var texto = new SolidBrush(ColorTexto))
            using (var recorte = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
            {
                for (int i = 0; i < tarjetas.Length; i++)
                {
                    var caja = new RectangleF(area.Left + i * (ancho + hueco), y, ancho, AltoResumen);
                    g.DrawRectangle(borde, caja.X, caja.Y, caja.Width, caja.Height);
                    g.FillRectangle(acento, caja.X, caja.Y, 3, caja.Height);

                    var (etiqueta, valor, detalle) = tarjetas[i];
                    g.DrawString(etiqueta.ToUpper(), _fEtiqueta, suave, new RectangleF(caja.X + 10, caja.Y + 6, caja.Width - 14, 12), recorte);
                    g.DrawString(valor, _fValor, texto, new RectangleF(caja.X + 10, caja.Y + 19, caja.Width - 14, 24), recorte);
                    g.DrawString(detalle, _fInfo, suave, new RectangleF(caja.X + 10, caja.Y + 44, caja.Width - 14, 14), recorte);
                }

                // Productos más pedidos (si hay room service en el reporte)
                y += AltoResumen + Separacion;
                var top = r.Productos.Take(5).Select(p => string.Format(T("RepServ_txtProductoTop"), p.Nombre, p.Cantidad)).ToList();
                string linea = top.Count == 0 ? T("RepServ_txtSinProductos") : T("RepServ_lblTopTitulo") + ":  " + string.Join("   ·   ", top);
                g.DrawString(linea, _fInfo, suave, new RectangleF(area.Left, y, area.Width, AltoTopProductos), recorte);
            }

            return y + AltoTopProductos + Separacion;
        }

        private void DibujarPie(Graphics g, Rectangle area)
        {
            int y = area.Bottom - AltoPie + 10;
            using (var linea = new Pen(ColorBorde))
                g.DrawLine(linea, area.Left, y, area.Right, y);

            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            {
                g.DrawString(string.Format(T("Reporte_generado"), _reporte.Generado.ToString("dd/MM/yyyy HH:mm"), _usuario),
                    _fInfo, suave, area.Left, y + 4);
                g.DrawString(string.Format(T("Reporte_pagina"), _pagina, Math.Max(_pagina, _paginas?.Count ?? 1)),
                    _fInfo, suave, new RectangleF(area.Left, y + 4, area.Width, 16), derecha);
            }
        }

        #endregion

        #region Tabla

        private static float[] CalcularAnchos(int anchoTotal)
        {
            float pesoTotal = Columnas.Sum(c => c.Peso);
            return Columnas.Select(c => anchoTotal * c.Peso / pesoTotal).ToArray();
        }

        private int DibujarCabeceraTabla(Graphics g, int x, int y, float[] anchos)
        {
            float total = anchos.Sum();
            using (var fondo = new SolidBrush(ColorMarca))
                g.FillRectangle(fondo, x, y, total, AltoCabeceraTabla);

            float xCelda = x;
            for (int i = 0; i < Columnas.Length; i++)
            {
                DibujarTexto(g, T(Columnas[i].Clave), _fCabecera, Color.Goldenrod,
                    new RectangleF(xCelda, y, anchos[i], AltoCabeceraTabla), Columnas[i].Alineacion);
                xCelda += anchos[i];
            }
            return y + AltoCabeceraTabla;
        }

        private void DibujarRenglon(Graphics g, int x, int y, float[] anchos, Renglon renglon)
        {
            float total = anchos.Sum();
            switch (renglon.Tipo)
            {
                case TipoRenglon.Grupo:
                {
                    var h = renglon.Huesped;
                    using (var fondo = new SolidBrush(ColorGrupo))
                        g.FillRectangle(fondo, x, y + 2, total, renglon.Alto - 2);
                    using (var acento = new SolidBrush(ColorDorado))
                        g.FillRectangle(acento, x, y + 2, 3, renglon.Alto - 2);

                    string titulo = h.NombreCompleto + (renglon.Continuacion ? "  " + T("Reporte_continuacion") : string.Empty);
                    string datos = string.Format(T("RepServ_txtDatosHuesped"), h.Documento, h.HuespedId, h.Cantidad, h.Estadias);
                    SizeF medida = g.MeasureString(titulo, _fGrupo);
                    DibujarTexto(g, titulo, _fGrupo, ColorMarca, new RectangleF(x + 6, y + 2, medida.Width + 12, renglon.Alto - 2), StringAlignment.Near);
                    DibujarTexto(g, datos, _fCuerpo, ColorTextoSuave,
                        new RectangleF(x + 14 + medida.Width, y + 2, Math.Max(10, total - medida.Width - 20), renglon.Alto - 2), StringAlignment.Near);
                    break;
                }

                case TipoRenglon.Servicio:
                {
                    var s = renglon.Servicio;
                    using (var borde = new Pen(ColorBorde))
                        g.DrawLine(borde, x, y + renglon.Alto, x + total, y + renglon.Alto);

                    string[] valores =
                    {
                        s.Fecha.ToString("dd/MM/yyyy HH:mm"),
                        s.NroHabitacion ?? "-",
                        OpcionEnum_68SA.Texto(s.Tipo),
                        ReporteServiciosTextos_68SA.Descripcion(s),
                        ReporteServiciosTextos_68SA.Detalle(s, Moneda),
                        OpcionEnum_68SA.Texto(s.Estado),
                        Moneda(s.Monto)
                    };

                    float xCelda = x;
                    for (int i = 0; i < valores.Length; i++)
                    {
                        Color color = ColorTexto;
                        Font fuente = _fCuerpo;
                        if (i == 2) { color = ReporteServiciosTextos_68SA.ColorTipoImpresion(s.Tipo); fuente = _fCuerpoNegrita; }
                        else if (i == 5) { color = ReporteServiciosTextos_68SA.ColorEstadoImpresion(s.Estado); fuente = _fCuerpoNegrita; }

                        DibujarTexto(g, valores[i], fuente, color, new RectangleF(xCelda, y, anchos[i], renglon.Alto),
                            Columnas[i].Alineacion, multilinea: i == ColDetalle);
                        xCelda += anchos[i];
                    }
                    break;
                }

                case TipoRenglon.Subtotal:
                {
                    var h = renglon.Huesped;
                    float anchoEtiqueta = total - anchos[anchos.Length - 1];
                    DibujarTexto(g, string.Format(T("RepServ_txtSubtotal"), h.NombreCompleto), _fCuerpoNegrita, ColorTexto,
                        new RectangleF(x, y, anchoEtiqueta, renglon.Alto), StringAlignment.Far);
                    DibujarTexto(g, Moneda(h.Total), _fCuerpoNegrita, ColorMarca,
                        new RectangleF(x + anchoEtiqueta, y, anchos[anchos.Length - 1], renglon.Alto), StringAlignment.Far);
                    break;
                }

                case TipoRenglon.TotalGeneral:
                {
                    using (var fondo = new SolidBrush(Color.FromArgb(225, 229, 238)))
                        g.FillRectangle(fondo, x, y + 4, total, renglon.Alto - 4);
                    using (var linea = new Pen(ColorMarca, 1.5f))
                        g.DrawLine(linea, x, y + 4, x + total, y + 4);

                    float anchoEtiqueta = total - anchos[anchos.Length - 1];
                    DibujarTexto(g, string.Format(T("RepServ_txtTotalGeneral"), _reporte.Servicios.Count, _reporte.CantidadHuespedes),
                        _fGrupo, ColorTexto, new RectangleF(x, y + 4, anchoEtiqueta, renglon.Alto - 4), StringAlignment.Near);
                    DibujarTexto(g, Moneda(_reporte.Total), _fGrupo, ColorMarca,
                        new RectangleF(x + anchoEtiqueta, y + 4, anchos[anchos.Length - 1], renglon.Alto - 4), StringAlignment.Far);
                    break;
                }
            }
        }

        private static void DibujarTexto(Graphics g, string texto, Font fuente, Color color, RectangleF celda,
            StringAlignment alineacion, bool multilinea = false)
        {
            var interior = new RectangleF(celda.X + 4, celda.Y + (multilinea ? 3 : 0), Math.Max(1, celda.Width - 8),
                                          Math.Max(1, celda.Height - (multilinea ? 6 : 0)));
            using (var pincel = new SolidBrush(color))
            using (var formato = new StringFormat
            {
                Alignment = alineacion,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = multilinea ? (StringFormatFlags)0 : StringFormatFlags.NoWrap
            })
            {
                g.DrawString(texto ?? string.Empty, fuente, pincel, interior, formato);
            }
        }

        private string Moneda(decimal valor) => "$" + valor.ToString("N2", _cultura);

        #endregion

        #region Métodos públicos

        public void Imprimir()
        {
            if (_reporte.Vacio)
            {
                MessageBox.Show(T("RepServ_msgSinDatos"), T("Dashboard_tituloReporteSinDatos"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var documento = CrearDocumento())
            using (var vistaPrevia = new PrintPreviewDialog
            {
                Document = documento,
                Text = T("RepServ_tituloVentana"),
                WindowState = FormWindowState.Maximized,
                ShowIcon = false,
                UseAntiAlias = true
            })
            {
                ReemplazarBotonImprimir(vistaPrevia, documento);
                vistaPrevia.ShowDialog();
            }
        }

        /// <summary>Documento listo para la vista previa o para imprimir (cada impresión arranca de cero).</summary>
        public PrintDocument CrearDocumento()
        {
            var documento = new PrintDocument { DocumentName = T("RepServ_titulo") };
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

        /// <summary>El "Imprimir" de la vista previa abre el diálogo para elegir impresora (o "Microsoft Print to PDF").</summary>
        private static void ReemplazarBotonImprimir(PrintPreviewDialog vistaPrevia, PrintDocument documento)
        {
            var barra = vistaPrevia.Controls.OfType<ToolStrip>().FirstOrDefault();
            var original = barra?.Items.OfType<ToolStripButton>().FirstOrDefault();
            if (original == null) return;

            var imprimir = new ToolStripButton(T("Reporte_btnImprimir"), original.Image)
            {
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                ToolTipText = T("Reporte_btnImprimir")
            };
            imprimir.Click += (s, e) =>
            {
                using (var dialogo = new PrintDialog { Document = documento, UseEXDialog = true })
                {
                    if (dialogo.ShowDialog(vistaPrevia) != DialogResult.OK) return;
                    documento.Print();
                    vistaPrevia.Close();
                }
            };

            original.Visible = false;
            barra.Items.Insert(barra.Items.IndexOf(original), imprimir);
        }

        #endregion

        #region Tipos anidados

        private enum TipoRenglon { Grupo, Servicio, Subtotal, TotalGeneral }

        private class Renglon
        {
            public TipoRenglon Tipo;
            public HuespedServicios_68SA Huesped;
            public ServicioConsumido_68SA Servicio;
            public int Alto;
            public bool Continuacion;
        }

        #endregion
    }
}
