using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Reportes
{
    /// <summary>Un reporte que se puede mostrar en la vista previa e imprimir.</summary>
    public interface IReporteImprimible_68SA
    {
        bool TieneDatos { get; }
        /// <summary>Documento nuevo (cada vista previa o impresión arranca de cero).</summary>
        PrintDocument CrearDocumento();
    }

    /// <summary>
    /// Vista previa de un reporte dentro del formulario: las páginas una debajo de la otra sobre fondo
    /// oscuro, navegación por páginas, zoom (también con Ctrl + rueda), "Ajustar al ancho",
    /// "Imprimir" (con el diálogo para elegir impresora) y "Guardar PDF" (con Microsoft Print to PDF).
    /// </summary>
    public class VisorReporte_68SA : UserControl
    {
        #region Campos

        private static readonly Color FondoBarra = Color.FromArgb(10, 18, 50);
        private static readonly Color FondoLienzo = Color.FromArgb(16, 24, 56);
        private static readonly Color TextoSuave = Color.FromArgb(170, 176, 196);
        private const string ImpresoraPdf = "Microsoft Print to PDF";

        private readonly Panel _barra;
        private readonly Lienzo _lienzo;
        private readonly Label _lblPagina, _lblZoom;
        private readonly IconButton _btnAnterior, _btnSiguiente, _btnAlejar, _btnAcercar, _btnAjustar, _btnImprimir, _btnPdf;

        private readonly FlowLayoutPanel _izquierda;
        private readonly ToolTip _tips = new ToolTip();
        private readonly Dictionary<IconButton, string> _textos = new Dictionary<IconButton, string>();

        private IReporteImprimible_68SA _reporte;
        private string _nombreArchivo = "Reporte";

        #endregion

        #region Propiedades

        public bool TieneReporte => _reporte != null;

        #endregion

        #region Constructor

        public VisorReporte_68SA()
        {
            BackColor = FondoLienzo;
            DoubleBuffered = true;

            _lienzo = new Lienzo { Dock = DockStyle.Fill };
            _lienzo.PaginaCambiada += (s, e) => ActualizarBarra();
            Controls.Add(_lienzo);

            _barra = new Panel { Dock = DockStyle.Top, Height = 54, BackColor = FondoBarra, Padding = new Padding(12, 9, 12, 9) };
            Controls.Add(_barra);

            // Izquierda: páginas y zoom
            var izquierda = _izquierda = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = FondoBarra,
                WrapContents = false,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            _barra.Controls.Add(izquierda);

            // Derecha: Guardar PDF e Imprimir (el último que se agrega queda contra el borde)
            _btnPdf = Boton(IconChar.FilePdf, false, 150);
            _btnPdf.Name = "btnPdf";
            _btnPdf.Dock = DockStyle.Right;
            _btnPdf.Click += (s, e) => GuardarPdf();
            _barra.Controls.Add(_btnPdf);
            _barra.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 10, BackColor = FondoBarra });
            _btnImprimir = Boton(IconChar.Print, true, 130);
            _btnImprimir.Name = "btnImprimir";
            _btnImprimir.Dock = DockStyle.Right;
            _btnImprimir.Click += (s, e) => Imprimir();
            _barra.Controls.Add(_btnImprimir);

            _btnAnterior = Boton(IconChar.ChevronUp, false, 36);
            _btnAnterior.Name = "btnPaginas";
            _btnAnterior.Click += (s, e) => _lienzo.IrAPagina(_lienzo.PaginaActual - 1);
            _lblPagina = Etiqueta(128);
            _btnSiguiente = Boton(IconChar.ChevronDown, false, 36);
            _btnSiguiente.Name = "btnPaginas";
            _btnSiguiente.Click += (s, e) => _lienzo.IrAPagina(_lienzo.PaginaActual + 1);
            var separador = new Panel { Width = 1, Height = 26, BackColor = Color.FromArgb(50, 62, 100), Margin = new Padding(14, 5, 14, 0) };
            _btnAlejar = Boton(IconChar.MagnifyingGlassMinus, false, 36);
            _btnAlejar.Name = "btnZoom";
            _btnAlejar.Click += (s, e) => _lienzo.CambiarZoom(-0.1f);
            _lblZoom = Etiqueta(58);
            _btnAcercar = Boton(IconChar.MagnifyingGlassPlus, false, 36);
            _btnAcercar.Name = "btnZoom";
            _btnAcercar.Click += (s, e) => _lienzo.CambiarZoom(+0.1f);
            _btnAjustar = Boton(IconChar.ArrowsLeftRightToLine, false, 140);
            _btnAjustar.Name = "btnAjustar";
            _btnAjustar.Margin = new Padding(10, 0, 0, 0);
            _btnAjustar.Click += (s, e) => _lienzo.AjustarAlAncho();
            _lienzo.ZoomCambiado += (s, e) => ActualizarBarra();

            izquierda.Controls.AddRange(new Control[] { _btnAnterior, _lblPagina, _btnSiguiente, separador, _btnAlejar, _lblZoom, _btnAcercar, _btnAjustar });
            _barra.Resize += (s, e) => AcomodarBarra();
            Disposed += (s, e) => _tips.Dispose();

            UpdateIdioma();
            ActualizarBarra();
        }

        #endregion

        #region Construcción

        private IconButton Boton(IconChar icono, bool principal, int ancho)
        {
            Color frente = principal ? Color.FromArgb(10, 15, 35) : Color.Goldenrod;
            var boton = new IconButton
            {
                BackColor = principal ? Color.Goldenrod : Color.FromArgb(16, 26, 62),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = frente,
                IconChar = icono,
                IconColor = frente,
                IconFont = IconFont.Auto,
                IconSize = 18,
                ImageAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 4, 0),
                Size = new Size(ancho, 36),
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            boton.FlatAppearance.BorderColor = principal ? Color.Goldenrod : Color.FromArgb(60, 72, 110);
            boton.FlatAppearance.MouseOverBackColor = principal ? Color.Gold : Color.FromArgb(28, 42, 92);
            return boton;
        }

        private Label Etiqueta(int ancho) => new Label
        {
            AutoSize = false,
            Size = new Size(ancho, 36),
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            ForeColor = Color.WhiteSmoke,
            TextAlign = ContentAlignment.MiddleCenter,
            Margin = Padding.Empty
        };

        #endregion

        #region Imprimir / PDF

        private void Imprimir()
        {
            if (_reporte == null) return;
            using (var documento = _reporte.CrearDocumento())
            using (var dialogo = new PrintDialog { Document = documento, UseEXDialog = true })
            {
                if (dialogo.ShowDialog(FindForm()) != DialogResult.OK) return;
                documento.Print();
            }
        }

        private void GuardarPdf()
        {
            if (_reporte == null) return;
            bool hayPdf = PrinterSettings.InstalledPrinters.Cast<string>()
                .Any(nombre => string.Equals(nombre, ImpresoraPdf, StringComparison.OrdinalIgnoreCase));
            if (!hayPdf)
            {
                MessageBox.Show(T("Reportes_msgSinPdf"), T("Reportes_tituloPdf"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ruta;
            using (var dialogo = new SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = NombreValido(_nombreArchivo) + ".pdf",
                OverwritePrompt = true,
                Title = T("Reportes_tituloPdf")
            })
            {
                if (dialogo.ShowDialog(FindForm()) != DialogResult.OK) return;
                ruta = dialogo.FileName;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                using (var documento = _reporte.CrearDocumento())
                {
                    bool apaisado = documento.DefaultPageSettings.Landscape;
                    var margenes = documento.DefaultPageSettings.Margins;
                    documento.PrinterSettings.PrinterName = ImpresoraPdf;
                    documento.PrinterSettings.PrintToFile = true;
                    documento.PrinterSettings.PrintFileName = ruta;
                    documento.DefaultPageSettings.Landscape = apaisado;
                    documento.DefaultPageSettings.Margins = margenes;
                    documento.PrintController = new StandardPrintController();   // sin la ventanita de "Imprimiendo..."
                    documento.Print();
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(string.Format(T("Reportes_msgErrorPdf"), ex.Message), T("Reportes_tituloPdf"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            if (MessageBox.Show(string.Format(T("Reportes_msgPdfGuardado"), ruta), T("Reportes_tituloPdf"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && File.Exists(ruta))
            {
                try { Process.Start(ruta); }
                catch (Exception) { /* sin visor de PDF asociado: el archivo quedó guardado igual */ }
            }
        }

        private static string NombreValido(string nombre)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                nombre = nombre.Replace(c, '_');
            return nombre;
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            _textos[_btnImprimir] = T("Reportes_btnImprimir");
            _textos[_btnPdf] = T("Reportes_btnPdf");
            _textos[_btnAjustar] = T("Reportes_btnAjustar");
            foreach (var par in _textos)
                _tips.SetToolTip(par.Key, par.Value);
            AcomodarBarra();
            _lienzo.TextoVacio = T("Reportes_vistaVacia");
            _lienzo.TextoAyuda = T("Reportes_vistaVaciaAyuda");
            ActualizarBarra();
        }

        #endregion

        #region Carga de datos

        private void ActualizarBarra()
        {
            int total = _lienzo.CantidadPaginas;
            bool hay = total > 0;
            _lblPagina.Text = hay ? string.Format(T("Reportes_pagina"), _lienzo.PaginaActual + 1, total) : "—";
            _lblZoom.Text = hay ? $"{Math.Round(_lienzo.Zoom * 100)}%" : "—";
            _btnAnterior.Enabled = hay && _lienzo.PaginaActual > 0;
            _btnSiguiente.Enabled = hay && _lienzo.PaginaActual < total - 1;
            _btnAlejar.Enabled = _btnAcercar.Enabled = _btnAjustar.Enabled = hay;
            _btnImprimir.Enabled = _btnPdf.Enabled = hay && _reporte != null;
        }

        #endregion

        #region Métodos públicos

        /// <summary>Muestra el reporte en la vista previa.</summary>
        public void Mostrar(IReporteImprimible_68SA reporte, string nombreArchivo)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                using (var documento = reporte.CrearDocumento())
                {
                    var controlador = new PreviewPrintController { UseAntiAlias = true };
                    documento.PrintController = controlador;
                    documento.Print();
                    _lienzo.Cargar(controlador.GetPreviewPageInfo());
                }
                _reporte = reporte;
                _nombreArchivo = string.IsNullOrWhiteSpace(nombreArchivo) ? "Reporte" : nombreArchivo;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            ActualizarBarra();
        }

        /// <summary>Vuelve al estado vacío (por ejemplo, al cambiar de pestaña o de filtros).</summary>
        public void Limpiar()
        {
            _reporte = null;
            _lienzo.Cargar(null);
            ActualizarBarra();
        }

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        /// <summary>
        /// Cada botón toma el ancho de su texto (en cualquier idioma) más el ícono y los márgenes.
        /// Si la barra es angosta y no entra todo, primero "Ajustar ancho" y después Imprimir / Guardar PDF
        /// quedan solo con el ícono (el texto se ve en el tooltip): nunca se tapan entre sí.
        /// </summary>
        private void AcomodarBarra()
        {
            if (_izquierda == null || !_textos.ContainsKey(_btnAjustar)) return;
            int disponible = _barra.ClientSize.Width - _barra.Padding.Horizontal;
            int fijoIzquierda = _izquierda.Controls.Cast<Control>()
                .Where(c => !ReferenceEquals(c, _btnAjustar))
                .Sum(c => c.Width + c.Margin.Horizontal);
            int separadorDerecha = 10;

            int Ancho(IconButton boton, bool conTexto) =>
                conTexto ? TextRenderer.MeasureText(" " + _textos[boton], boton.Font).Width + boton.IconSize + 34 : 40;

            // 0 = todo con texto, 1 = "Ajustar" solo ícono, 2 = los tres solo ícono
            int modo = 0;
            for (; modo < 2; modo++)
            {
                int necesario = fijoIzquierda + Ancho(_btnAjustar, modo == 0) + _btnAjustar.Margin.Horizontal
                              + Ancho(_btnImprimir, modo < 2) + separadorDerecha + Ancho(_btnPdf, modo < 2);
                if (necesario <= disponible) break;
            }

            Poner(_btnAjustar, modo == 0);
            Poner(_btnImprimir, modo < 2);
            Poner(_btnPdf, modo < 2);

            void Poner(IconButton boton, bool conTexto)
            {
                boton.Text = conTexto ? " " + _textos[boton] : string.Empty;
                boton.TextImageRelation = conTexto ? TextImageRelation.ImageBeforeText : TextImageRelation.Overlay;
                int ancho = Ancho(boton, conTexto);
                if (boton.Width != ancho) boton.Width = ancho;
            }
        }

        #endregion

        #region Tipos anidados

        /// <summary>Área donde se dibujan las páginas.</summary>
        private sealed class Lienzo : Panel
        {
            private const int Separacion = 26;
            private PreviewPageInfo[] _paginas = new PreviewPageInfo[0];
            private readonly List<Rectangle> _rects = new List<Rectangle>();
            private float _zoom = 1f;
            private bool _ajustarAlAncho = true;
            private int _paginaActual;

            public event EventHandler PaginaCambiada;
            public event EventHandler ZoomCambiado;

            public string TextoVacio { get; set; }
            public string TextoAyuda { get; set; }
            public int CantidadPaginas => _paginas.Length;
            public int PaginaActual => _paginaActual;
            public float Zoom => _zoom;

            public Lienzo()
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                         | ControlStyles.ResizeRedraw, true);
                BackColor = FondoLienzo;
                AutoScroll = true;
            }

            public void Cargar(PreviewPageInfo[] paginas)
            {
                foreach (var p in _paginas) p.Image?.Dispose();
                _paginas = paginas ?? new PreviewPageInfo[0];
                _paginaActual = 0;
                _ajustarAlAncho = true;
                AutoScrollPosition = Point.Empty;
                Acomodar();
                PaginaCambiada?.Invoke(this, EventArgs.Empty);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    foreach (var p in _paginas) p.Image?.Dispose();
                base.Dispose(disposing);
            }

            // Tamaño de la página en pantalla: el papel viene en centésimas de pulgada
            private Size TamanoPagina(PreviewPageInfo pagina)
                => new Size((int)(pagina.PhysicalSize.Width * 0.96f * _zoom), (int)(pagina.PhysicalSize.Height * 0.96f * _zoom));

            public void AjustarAlAncho()
            {
                _ajustarAlAncho = true;
                Acomodar();
            }

            public void CambiarZoom(float delta)
            {
                if (_paginas.Length == 0) return;
                _ajustarAlAncho = false;
                float anterior = _zoom;
                _zoom = Math.Max(0.3f, Math.Min(3f, (float)Math.Round(_zoom + delta, 2)));
                if (Math.Abs(anterior - _zoom) < 0.001f) return;
                int pagina = _paginaActual;
                Acomodar();
                IrAPagina(pagina);
            }

            private void Acomodar()
            {
                _rects.Clear();
                if (_paginas.Length == 0)
                {
                    AutoScrollMinSize = Size.Empty;
                    Invalidate();
                    ZoomCambiado?.Invoke(this, EventArgs.Empty);
                    return;
                }

                if (_ajustarAlAncho)
                {
                    float anchoPapel = _paginas.Max(p => p.PhysicalSize.Width) * 0.96f;
                    // Ancho sin depender de si la barra vertical está visible (si no, el zoom oscila)
                    int ancho = ClientSize.Width + (VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0);
                    int disponible = ancho - 2 * Separacion - SystemInformation.VerticalScrollBarWidth;
                    if (anchoPapel > 0 && disponible > 100)
                        _zoom = Math.Max(0.3f, Math.Min(1.6f, disponible / anchoPapel));
                }

                int y = Separacion, anchoMax = 0;
                foreach (var pagina in _paginas)
                {
                    var tam = TamanoPagina(pagina);
                    _rects.Add(new Rectangle(0, y, tam.Width, tam.Height));
                    y += tam.Height + Separacion;
                    anchoMax = Math.Max(anchoMax, tam.Width);
                }
                AutoScrollMinSize = new Size(anchoMax + 2 * Separacion, y);
                Invalidate();
                ZoomCambiado?.Invoke(this, EventArgs.Empty);
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                if (_ajustarAlAncho && _paginas.Length > 0) Acomodar();
            }

            public void IrAPagina(int indice)
            {
                if (_rects.Count == 0) return;
                indice = Math.Max(0, Math.Min(_rects.Count - 1, indice));
                AutoScrollPosition = new Point(-AutoScrollPosition.X, _rects[indice].Top - Separacion / 2);
                _paginaActual = indice;
                Invalidate();
                PaginaCambiada?.Invoke(this, EventArgs.Empty);
            }

            protected override void OnScroll(ScrollEventArgs se)
            {
                base.OnScroll(se);
                ActualizarPaginaActual();
                Invalidate();
            }

            protected override void OnMouseWheel(MouseEventArgs e)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    CambiarZoom(e.Delta > 0 ? 0.1f : -0.1f);
                    return;
                }
                base.OnMouseWheel(e);
                ActualizarPaginaActual();
                Invalidate();
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                if (!Focused) Focus();   // con un clic, la rueda del mouse desplaza las páginas
            }

            private void ActualizarPaginaActual()
            {
                if (_rects.Count == 0) return;
                int referencia = -AutoScrollPosition.Y + ClientSize.Height / 3;
                int pagina = 0;
                for (int i = 0; i < _rects.Count; i++)
                    if (_rects[i].Top - Separacion <= referencia) pagina = i;
                if (pagina == _paginaActual) return;
                _paginaActual = pagina;
                PaginaCambiada?.Invoke(this, EventArgs.Empty);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.Clear(BackColor);

                if (_paginas.Length == 0)
                {
                    DibujarVacio(g);
                    return;
                }

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                int anchoVisible = Math.Max(AutoScrollMinSize.Width, ClientSize.Width);
                g.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

                for (int i = 0; i < _paginas.Length; i++)
                {
                    var r = _rects[i];
                    r.X = (anchoVisible - r.Width) / 2;
                    var visible = new Rectangle(-AutoScrollPosition.X, -AutoScrollPosition.Y, ClientSize.Width, ClientSize.Height);
                    if (!visible.IntersectsWith(Rectangle.Inflate(r, 8, 8))) continue;

                    using (var sombra = new SolidBrush(Color.FromArgb(90, 0, 0, 0)))
                        g.FillRectangle(sombra, r.X + 5, r.Y + 6, r.Width, r.Height);
                    g.FillRectangle(Brushes.White, r);
                    g.DrawImage(_paginas[i].Image, r);
                    if (i == _paginaActual && _paginas.Length > 1)
                        using (var borde = new Pen(Color.FromArgb(160, 218, 165, 32), 2f))
                            g.DrawRectangle(borde, r.X - 2, r.Y - 2, r.Width + 3, r.Height + 3);
                }
            }

            private void DibujarVacio(Graphics g)
            {
                var centro = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
                var icono = IconCache.Get(IconChar.FileLines, IconFont.Auto, 64, Color.FromArgb(70, 82, 120));
                if (icono != null)
                    g.DrawImage(icono, centro.X - 32, centro.Y - 90, 64, 64);
                using (var titulo = new Font("Segoe UI", 13f, FontStyle.Bold))
                    TextRenderer.DrawText(g, TextoVacio ?? string.Empty, titulo,
                        new Rectangle(20, centro.Y - 10, ClientSize.Width - 40, 30), Color.FromArgb(200, 205, 220),
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
                using (var ayuda = new Font("Segoe UI", 10f))
                    TextRenderer.DrawText(g, TextoAyuda ?? string.Empty, ayuda,
                        new Rectangle(40, centro.Y + 22, ClientSize.Width - 80, 48), TextoSuave,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.Top | TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);
            }
        }

        #endregion
    }
}
