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
    /// <summary>
    /// Reporte de reservas del Dashboard: vista previa antes de imprimir, resumen del período
    /// (cantidades, noches, facturación, cobros y saldo), tabla paginada con encabezado repetido,
    /// totales y pie "Página X de Y". Se imprime en cualquier impresora o en "Microsoft Print to PDF".
    /// </summary>
    public class ReporteReservasImpresor_68SA
    {
        #region Estilo

        private static readonly Color ColorMarca = Color.FromArgb(10, 18, 50);
        private static readonly Color ColorDorado = Color.FromArgb(184, 134, 11);
        private static readonly Color ColorFilaAlterna = Color.FromArgb(244, 246, 250);
        private static readonly Color ColorBorde = Color.FromArgb(210, 214, 222);
        private static readonly Color ColorTextoSuave = Color.FromArgb(95, 100, 115);
        private static readonly Color ColorTexto = Color.FromArgb(25, 28, 38);

        private const int AltoFila = 22;
        private const int AltoCabeceraTabla = 26;
        private const int AltoTotales = 26;
        private const int AltoPie = 30;
        private const int AltoEncabezadoCompleto = 70;
        private const int AltoEncabezadoCorto = 34;
        private const int AltoResumen = 64;
        private const int Separacion = 12;

        // Columnas: clave de traducción, peso relativo y alineación
        private static readonly (string Clave, float Peso, StringAlignment Alineacion)[] Columnas =
        {
            ("Reporte_colNumero",     4.5f, StringAlignment.Near),
            ("Reporte_colTitular",   15f,   StringAlignment.Near),
            ("Reporte_colDocumento",  8.5f, StringAlignment.Near),
            ("Reporte_colHabitacion", 4.5f, StringAlignment.Center),
            ("Reporte_colTipo",      11f,   StringAlignment.Near),
            ("Reporte_colIngreso",    7.5f, StringAlignment.Center),
            ("Reporte_colEgreso",     7.5f, StringAlignment.Center),
            ("Reporte_colNoches",     5f,   StringAlignment.Center),
            ("Reporte_colHuespedes",  5f,   StringAlignment.Center),
            ("Reporte_colEstado",     7.5f, StringAlignment.Near),
            ("Reporte_colTotal",      9f,   StringAlignment.Far),
            ("Reporte_colPagado",     9f,   StringAlignment.Far),
            ("Reporte_colSaldo",      8.5f, StringAlignment.Far),
        };

        #endregion

        private readonly List<Reserva_68SA> _reservas;
        private readonly DateTime? _desde;
        private readonly DateTime? _hasta;
        private readonly string _usuario;
        private readonly DateTime _generado = DateTime.Now;
        private readonly CultureInfo _cultura = OpcionEnum_68SA.CulturaActual();

        private int _filaActual;
        private int _pagina;
        private int _totalPaginas;
        private bool _totalesImpresos;

        private Font _fTitulo, _fSubtitulo, _fInfo, _fEtiqueta, _fValor, _fCabecera, _fCuerpo, _fCuerpoNegrita;

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        public ReporteReservasImpresor_68SA(List<Reserva_68SA> reservas, DateTime? desde, DateTime? hasta)
        {
            // Orden cronológico por ingreso (el listado llega del más nuevo al más viejo)
            _reservas = (reservas ?? new List<Reserva_68SA>())
                .OrderBy(r => r.FechaIngreso).ThenBy(r => r.Id).ToList();
            _desde = desde;
            _hasta = hasta;

            var actual = SessionManager_08YS.Instance.Current;
            _usuario = actual == null ? "-" : $"{actual.Nombre} {actual.Apellido}".Trim();
        }

        public void Imprimir()
        {
            if (_reservas.Count == 0)
            {
                MessageBox.Show(T("Dashboard_msgReporteSinDatos"), T("Dashboard_tituloReporteSinDatos"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var documento = CrearDocumento())
            using (var vistaPrevia = new PrintPreviewDialog
            {
                Document = documento,
                Text = T("Reporte_tituloVentana"),
                WindowState = FormWindowState.Maximized,
                ShowIcon = false,
                UseAntiAlias = true
            })
            {
                ReemplazarBotonImprimir(vistaPrevia, documento);
                vistaPrevia.ShowDialog();
            }
        }

        private PrintDocument CrearDocumento()
        {
            var documento = new PrintDocument { DocumentName = T("Reporte_titulo") };
            documento.DefaultPageSettings.Landscape = true;
            documento.DefaultPageSettings.Margins = new Margins(40, 40, 36, 36);
            documento.BeginPrint += (s, e) => IniciarImpresion();
            documento.EndPrint += (s, e) => LiberarFuentes();
            documento.PrintPage += ImprimirPagina;
            return documento;
        }

        /// <summary>
        /// El botón "Imprimir" de la vista previa manda directo a la impresora predeterminada;
        /// se reemplaza por uno que abre el diálogo para elegir impresora (o "Microsoft Print to PDF").
        /// </summary>
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

        #region Ciclo de impresión

        private void IniciarImpresion()
        {
            _filaActual = 0;
            _pagina = 0;
            _totalPaginas = 0;
            _totalesImpresos = false;

            LiberarFuentes();
            _fTitulo = new Font("Segoe UI", 15F, FontStyle.Bold);
            _fSubtitulo = new Font("Segoe UI", 10F, FontStyle.Bold);
            _fInfo = new Font("Segoe UI", 8F);
            _fEtiqueta = new Font("Segoe UI", 6.5F, FontStyle.Bold);
            _fValor = new Font("Segoe UI", 12F, FontStyle.Bold);
            _fCabecera = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            _fCuerpo = new Font("Segoe UI", 7.5F);
            _fCuerpoNegrita = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        }

        private void LiberarFuentes()
        {
            foreach (var f in new[] { _fTitulo, _fSubtitulo, _fInfo, _fEtiqueta, _fValor, _fCabecera, _fCuerpo, _fCuerpoNegrita })
                f?.Dispose();
            _fTitulo = _fSubtitulo = _fInfo = _fEtiqueta = _fValor = _fCabecera = _fCuerpo = _fCuerpoNegrita = null;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            Rectangle area = e.MarginBounds;
            if (_totalPaginas == 0) _totalPaginas = CalcularTotalPaginas(area);
            _pagina++;
            if (_pagina > 1000) { e.HasMorePages = false; return; } // resguardo ante un papel sin espacio útil

            int y = _pagina == 1 ? DibujarEncabezadoCompleto(g, area) : DibujarEncabezadoCorto(g, area);
            if (_pagina == 1) y = DibujarResumen(g, area, y);

            int limite = area.Bottom - AltoPie;
            float[] anchos = CalcularAnchos(area.Width);

            if (_filaActual < _reservas.Count)
            {
                y = DibujarCabeceraTabla(g, area.Left, y, anchos);
                while (_filaActual < _reservas.Count && y + AltoFila <= limite)
                {
                    DibujarFila(g, area.Left, y, anchos, _reservas[_filaActual], _filaActual % 2 == 1);
                    y += AltoFila;
                    _filaActual++;
                }
            }

            if (_filaActual >= _reservas.Count && y + AltoTotales <= limite)
            {
                DibujarTotales(g, area.Left, y, anchos);
                _totalesImpresos = true;
            }

            DibujarPie(g, area);
            e.HasMorePages = !_totalesImpresos;
        }

        /// <summary>Simula el reparto de filas para poder imprimir "Página X de Y".</summary>
        private int CalcularTotalPaginas(Rectangle area)
        {
            int limite = area.Bottom - AltoPie;
            int filas = _reservas.Count;
            int paginas = 0;
            bool totales = false;

            while (!totales)
            {
                paginas++;
                int y = area.Top + (paginas == 1 ? AltoEncabezadoCompleto + AltoResumen + Separacion * 2 : AltoEncabezadoCorto + Separacion);
                if (filas > 0)
                {
                    y += AltoCabeceraTabla;
                    int entran = Math.Max(1, (limite - y) / AltoFila);
                    int usadas = Math.Min(entran, filas);
                    filas -= usadas;
                    y += usadas * AltoFila;
                }
                if (filas == 0 && y + AltoTotales <= limite) totales = true;
                if (paginas > 1000) break; // resguardo ante una página sin espacio útil
            }
            return paginas;
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
                g.DrawString(T("Reporte_titulo"), _fSubtitulo, dorado, banda.Left + 18, banda.Top + 40);

            string periodo = _desde.HasValue && _hasta.HasValue
                ? string.Format(T("Reporte_periodo"), _desde.Value.ToString("dd/MM/yyyy"), _hasta.Value.ToString("dd/MM/yyyy"))
                : T("Reporte_periodoHistorico");

            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            using (var claro = new SolidBrush(Color.FromArgb(200, 205, 220)))
            {
                var zona = new RectangleF(banda.Left, banda.Top + 12, banda.Width - 16, 18);
                g.DrawString(periodo, _fSubtitulo, Brushes.White, zona, derecha);
                zona.Y += 22;
                g.DrawString(T("Reporte_criterio"), _fInfo, claro, zona, derecha);
            }

            return banda.Bottom + Separacion;
        }

        private int DibujarEncabezadoCorto(Graphics g, Rectangle area)
        {
            using (var marca = new SolidBrush(ColorMarca))
                g.DrawString($"{T("Reporte_hotel")} — {T("Reporte_titulo")} {T("Reporte_continuacion")}", _fSubtitulo, marca, area.Left, area.Top + 4);
            using (var linea = new Pen(ColorDorado, 2))
                g.DrawLine(linea, area.Left, area.Top + AltoEncabezadoCorto - 6, area.Right, area.Top + AltoEncabezadoCorto - 6);
            return area.Top + AltoEncabezadoCorto + Separacion;
        }

        private int DibujarResumen(Graphics g, Rectangle area, int y)
        {
            var vigentes = _reservas.Where(r => r.Estado != EstadoReserva.Cancelada).ToList();
            int pendientes = _reservas.Count(r => r.Estado == EstadoReserva.Confirmada);
            int enCurso = _reservas.Count(r => r.Estado == EstadoReserva.EnCurso);
            int finalizadas = _reservas.Count(r => r.Estado == EstadoReserva.Finalizada);
            int canceladas = _reservas.Count(r => r.Estado == EstadoReserva.Cancelada);
            int noches = vigentes.Sum(r => r.Noches);
            decimal facturado = vigentes.Sum(r => r.MontoTotal);
            decimal cobrado = _reservas.Sum(r => r.MontoPagado);
            decimal saldo = vigentes.Sum(r => Math.Max(0, r.MontoTotal - r.MontoPagado));
            decimal tasaCancelacion = _reservas.Count > 0 ? (decimal)canceladas / _reservas.Count * 100 : 0;

            var tarjetas = new[]
            {
                (T("Reporte_kpiReservas"), _reservas.Count.ToString("N0", _cultura),
                    string.Format(T("Reporte_kpiReservasDetalle"), pendientes, enCurso, finalizadas, canceladas)),
                (T("Reporte_kpiNoches"), noches.ToString("N0", _cultura),
                    string.Format(_cultura, T("Reporte_kpiNochesDetalle"), vigentes.Count > 0 ? (double)noches / vigentes.Count : 0)),
                (T("Reporte_kpiFacturacion"), Moneda(facturado), T("Reporte_kpiFacturacionDetalle")),
                (T("Reporte_kpiCobrado"), Moneda(cobrado), T("Reporte_kpiCobradoDetalle")),
                (T("Reporte_kpiSaldo"), Moneda(saldo), T("Reporte_kpiSaldoDetalle")),
                (T("Reporte_kpiCancelacion"), tasaCancelacion.ToString("0.#", _cultura) + "%",
                    string.Format(T("Reporte_kpiCancelacionDetalle"), canceladas, _reservas.Count)),
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
                    g.DrawString(etiqueta, _fEtiqueta, suave, new RectangleF(caja.X + 10, caja.Y + 6, caja.Width - 14, 12), recorte);
                    g.DrawString(valor, _fValor, texto, new RectangleF(caja.X + 10, caja.Y + 19, caja.Width - 14, 24), recorte);
                    g.DrawString(detalle, _fInfo, suave, new RectangleF(caja.X + 10, caja.Y + 44, caja.Width - 14, 14), recorte);
                }
            }

            return y + AltoResumen + Separacion;
        }

        private void DibujarPie(Graphics g, Rectangle area)
        {
            int y = area.Bottom - AltoPie + 10;
            using (var linea = new Pen(ColorBorde))
                g.DrawLine(linea, area.Left, y, area.Right, y);

            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            {
                g.DrawString(string.Format(T("Reporte_generado"), _generado.ToString("dd/MM/yyyy HH:mm"), _usuario),
                    _fInfo, suave, area.Left, y + 4);
                g.DrawString(string.Format(T("Reporte_pagina"), _pagina, Math.Max(_pagina, _totalPaginas)),
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

        private void DibujarFila(Graphics g, int x, int y, float[] anchos, Reserva_68SA r, bool alterna)
        {
            float total = anchos.Sum();
            if (alterna)
                using (var fondo = new SolidBrush(ColorFilaAlterna))
                    g.FillRectangle(fondo, x, y, total, AltoFila);
            using (var borde = new Pen(ColorBorde))
                g.DrawLine(borde, x, y + AltoFila, x + total, y + AltoFila);

            decimal saldo = r.Estado == EstadoReserva.Cancelada ? 0 : Math.Max(0, r.MontoTotal - r.MontoPagado);
            string huespedes = r.CantidadNinos > 0 ? $"{r.CantidadAdultos} + {r.CantidadNinos}" : r.CantidadAdultos.ToString();

            string[] valores =
            {
                r.Id.ToString(),
                $"{r.Titular?.Apellido}, {r.Titular?.Nombre}".Trim(' ', ','),
                r.Titular?.Documento ?? "-",
                r.Habitacion?.NroHabitacion ?? "-",
                r.Habitacion?.Tipo?.Nombre ?? "-",
                r.FechaIngreso.ToString("dd/MM/yyyy"),
                r.FechaEgreso.ToString("dd/MM/yyyy"),
                r.Noches.ToString(),
                huespedes,
                OpcionEnum_68SA.Texto(r.Estado),
                Moneda(r.MontoTotal),
                Moneda(r.MontoPagado),
                Moneda(saldo)
            };

            float xCelda = x;
            for (int i = 0; i < valores.Length; i++)
            {
                Color color = ColorTexto;
                Font fuente = _fCuerpo;
                if (i == 9) { color = ColorEstado(r.Estado); fuente = _fCuerpoNegrita; }
                else if (i == 12 && saldo > 0) { color = Color.FromArgb(190, 40, 40); fuente = _fCuerpoNegrita; }
                else if (r.Estado == EstadoReserva.Cancelada) color = ColorTextoSuave;

                DibujarTexto(g, valores[i], fuente, color, new RectangleF(xCelda, y, anchos[i], AltoFila), Columnas[i].Alineacion);
                xCelda += anchos[i];
            }
        }

        private void DibujarTotales(Graphics g, int x, int y, float[] anchos)
        {
            float total = anchos.Sum();
            using (var fondo = new SolidBrush(Color.FromArgb(232, 235, 242)))
                g.FillRectangle(fondo, x, y, total, AltoTotales);
            using (var linea = new Pen(ColorMarca, 1.5f))
                g.DrawLine(linea, x, y, x + total, y);

            decimal sumaTotal = _reservas.Sum(r => r.MontoTotal);
            decimal sumaPagado = _reservas.Sum(r => r.MontoPagado);
            decimal sumaSaldo = _reservas.Where(r => r.Estado != EstadoReserva.Cancelada).Sum(r => Math.Max(0, r.MontoTotal - r.MontoPagado));

            float anchoEtiqueta = anchos.Take(10).Sum();
            DibujarTexto(g, string.Format(T("Reporte_totales"), _reservas.Count), _fCuerpoNegrita, ColorTexto,
                new RectangleF(x, y, anchoEtiqueta, AltoTotales), StringAlignment.Near);

            float xCelda = x + anchoEtiqueta;
            decimal[] montos = { sumaTotal, sumaPagado, sumaSaldo };
            for (int i = 0; i < montos.Length; i++)
            {
                DibujarTexto(g, Moneda(montos[i]), _fCuerpoNegrita, ColorTexto,
                    new RectangleF(xCelda, y, anchos[10 + i], AltoTotales), StringAlignment.Far);
                xCelda += anchos[10 + i];
            }
        }

        private static void DibujarTexto(Graphics g, string texto, Font fuente, Color color, RectangleF celda, StringAlignment alineacion)
        {
            var interior = new RectangleF(celda.X + 4, celda.Y, Math.Max(1, celda.Width - 8), celda.Height);
            using (var pincel = new SolidBrush(color))
            using (var formato = new StringFormat
            {
                Alignment = alineacion,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap
            })
            {
                g.DrawString(texto ?? string.Empty, fuente, pincel, interior, formato);
            }
        }

        private static Color ColorEstado(EstadoReserva estado)
        {
            switch (estado)
            {
                case EstadoReserva.Confirmada: return Color.FromArgb(30, 90, 180);
                case EstadoReserva.EnCurso: return Color.FromArgb(20, 130, 60);
                case EstadoReserva.Finalizada: return Color.FromArgb(90, 95, 110);
                case EstadoReserva.Cancelada: return Color.FromArgb(190, 40, 40);
                default: return ColorTexto;
            }
        }

        private string Moneda(decimal valor) => "$" + valor.ToString("N2", _cultura);

        #endregion
    }
}
