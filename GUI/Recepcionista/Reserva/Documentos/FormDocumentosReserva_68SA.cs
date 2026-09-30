using BE_08YS;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Visor del comprobante de reserva y de la factura: vista previa fiel a lo que se imprime,
    /// guardar como PDF, imprimir y enviar por mail (en segundo plano).
    /// Al registrar una reserva se abre con el mensaje de éxito y el mail se envía solo.
    /// </summary>
    public class FormDocumentosReserva_68SA : Form
    {
        private static readonly Color ColorFondo = Color.FromArgb(5, 10, 40);
        private static readonly Color ColorPanel = Color.FromArgb(10, 18, 50);
        private static readonly Color ColorLienzo = Color.FromArgb(24, 32, 66);
        private static readonly Color ColorExito = Color.FromArgb(76, 217, 100);
        private static readonly Color ColorError = Color.FromArgb(235, 90, 90);
        private static readonly Color ColorAviso = Color.FromArgb(230, 175, 46);
        private static readonly Color ColorSuave = Color.FromArgb(160, 165, 180);

        private readonly DatosDocumentoReserva_68SA _datos;
        private readonly List<DocumentoReserva_68SA> _documentos;
        private readonly bool _esNuevaReserva;
        private DocumentoReserva_68SA _actual;
        private bool _enviando;

        private readonly Panel _pnlBanner = new Panel();
        private readonly IconPictureBox _iconoBanner = new IconPictureBox();
        private readonly Label _lblTitulo = new Label();
        private readonly Label _lblEstadoCorreo = new Label();
        private readonly FlowLayoutPanel _pnlPestanas = new FlowLayoutPanel();
        private readonly Panel _pnlVista = new Panel();
        private readonly Panel _lienzo = new Panel();
        private readonly FlowLayoutPanel _pnlAcciones = new FlowLayoutPanel();
        private readonly IconButton _btnGuardarPdf = new IconButton();
        private readonly IconButton _btnImprimir = new IconButton();
        private readonly IconButton _btnEnviar = new IconButton();
        private readonly IconButton _btnCerrar = new IconButton();
        private readonly List<IconButton> _pestanas = new List<IconButton>();
        private float _zoom = 1f;          // píxeles de pantalla por centésima de pulgada del documento
        private Bitmap _render;            // documento ya dibujado al tamaño del lienzo

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        /// <param name="indiceInicial">0 = comprobante, 1 = factura.</param>
        /// <param name="esNuevaReserva">Muestra el mensaje de reserva registrada y envía el mail automáticamente.</param>
        public FormDocumentosReserva_68SA(DatosDocumentoReserva_68SA datos, int indiceInicial = 0, bool esNuevaReserva = false)
        {
            _datos = datos ?? throw new ArgumentNullException(nameof(datos));
            _esNuevaReserva = esNuevaReserva;
            _documentos = new List<DocumentoReserva_68SA>
            {
                new ComprobanteReserva_68SA(datos),
                new FacturaReserva_68SA(datos)
            };

            ArmarPantalla();
            MostrarDocumento(Math.Max(0, Math.Min(indiceInicial, _documentos.Count - 1)));
            ActualizarBanner();

            Shown += (s, e) =>
            {
                if (_esNuevaReserva) EnviarPorCorreo(automatico: true);
            };
        }

        #region Armado de la pantalla

        private void ArmarPantalla()
        {
            SuspendLayout();

            Text = string.Format(T("Documento_tituloVentana"), _datos.Reserva.Id.ToString("000000"));
            BackColor = ColorFondo;
            ForeColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 9F);
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            MinimizeBox = false;
            KeyPreview = true;
            AutoScaleMode = AutoScaleMode.None;
            MinimumSize = new Size(720, 560);
            var trabajo = Screen.FromPoint(Cursor.Position).WorkingArea;
            Size = new Size(Math.Min(980, trabajo.Width - 60), Math.Min(960, trabajo.Height - 40));
            try { Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch { ShowIcon = false; }
            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };

            // ---- Mensaje superior ----
            _pnlBanner.Dock = DockStyle.Top;
            _pnlBanner.Height = 66;
            _pnlBanner.Padding = new Padding(20, 10, 20, 10);
            _pnlBanner.BackColor = ColorPanel;

            _iconoBanner.BackColor = Color.Transparent;
            _iconoBanner.IconSize = 34;
            _iconoBanner.Size = new Size(38, 38);
            _iconoBanner.Location = new Point(20, 14);

            _lblTitulo.AutoSize = false;
            _lblTitulo.AutoEllipsis = true;
            _lblTitulo.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            _lblTitulo.Location = new Point(70, 10);
            _lblTitulo.Size = new Size(600, 24);
            _lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            _lblEstadoCorreo.AutoSize = false;
            _lblEstadoCorreo.AutoEllipsis = true;
            _lblEstadoCorreo.Font = new Font("Segoe UI", 9F);
            _lblEstadoCorreo.ForeColor = ColorSuave;
            _lblEstadoCorreo.Location = new Point(70, 36);
            _lblEstadoCorreo.Size = new Size(600, 20);
            _lblEstadoCorreo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            _pnlBanner.Controls.Add(_iconoBanner);
            _pnlBanner.Controls.Add(_lblTitulo);
            _pnlBanner.Controls.Add(_lblEstadoCorreo);
            _pnlBanner.Resize += (s, e) =>
            {
                _lblTitulo.Width = Math.Max(100, _pnlBanner.ClientSize.Width - _lblTitulo.Left - 20);
                _lblEstadoCorreo.Width = _lblTitulo.Width;
            };

            // ---- Pestañas (comprobante / factura) ----
            _pnlPestanas.Dock = DockStyle.Top;
            _pnlPestanas.Height = 50;
            _pnlPestanas.Padding = new Padding(16, 6, 16, 0);
            _pnlPestanas.BackColor = ColorFondo;
            _pnlPestanas.WrapContents = false;
            for (int i = 0; i < _documentos.Count; i++)
            {
                int indice = i;
                var pestana = new IconButton
                {
                    Text = _documentos[i].Titulo,
                    IconChar = i == 0 ? IconChar.Ticket : IconChar.FileInvoiceDollar,
                    IconSize = 20,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F),
                    AutoSize = true,                       // el ancho se ajusta al texto (y a la escala de Windows)
                    MinimumSize = new Size(180, 40),
                    Margin = new Padding(0, 0, 8, 0),
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Padding = new Padding(12, 0, 0, 0),
                    UseVisualStyleBackColor = false
                };
                pestana.FlatAppearance.BorderSize = 0;
                pestana.Click += (s, e) => MostrarDocumento(indice);
                _pestanas.Add(pestana);
                _pnlPestanas.Controls.Add(pestana);
            }

            // ---- Vista previa ----
            _pnlVista.Dock = DockStyle.Fill;
            _pnlVista.AutoScroll = true;
            _pnlVista.BackColor = ColorLienzo;
            LayoutAdaptable_68SA.HabilitarDobleBuffer(_pnlVista);
            LayoutAdaptable_68SA.HabilitarDobleBuffer(_lienzo);
            _lienzo.BackColor = ColorLienzo;
            _lienzo.Paint += (s, e) => PintarLienzo(e.Graphics);
            Disposed += (s, e) => _render?.Dispose();
            _pnlVista.Controls.Add(_lienzo);
            _pnlVista.Resize += (s, e) => AjustarLienzo();

            // ---- Acciones ----
            _pnlAcciones.Dock = DockStyle.Bottom;
            _pnlAcciones.Height = 72;
            _pnlAcciones.FlowDirection = FlowDirection.RightToLeft;
            _pnlAcciones.Padding = new Padding(16, 11, 16, 0);
            _pnlAcciones.BackColor = ColorPanel;
            _pnlAcciones.WrapContents = false;

            CrudEstilo_68SA.EstiloBoton(_btnCerrar, IconChar.Xmark, false, 140);
            CrudEstilo_68SA.EstiloBoton(_btnImprimir, IconChar.Print, false, 150);
            CrudEstilo_68SA.EstiloBoton(_btnEnviar, IconChar.PaperPlane, false, 190);
            CrudEstilo_68SA.EstiloBoton(_btnGuardarPdf, IconChar.FilePdf, true, 180);
            foreach (var boton in new[] { _btnCerrar, _btnImprimir, _btnEnviar, _btnGuardarPdf })
            {
                boton.Margin = new Padding(10, 0, 0, 0);
                boton.Padding = new Padding(10, 0, 0, 0);
                _pnlAcciones.Controls.Add(boton);
            }
            _btnCerrar.Text = T("Documento_btnCerrar");
            _btnImprimir.Text = T("Documento_btnImprimir");
            _btnEnviar.Text = T("Documento_btnEnviar");
            _btnGuardarPdf.Text = T("Documento_btnGuardarPdf");

            bool puedeImprimir = SessionManager_08YS.Instance.HasPermission(Permisos.ImprimirComprobante)
                              || SessionManager_08YS.Instance.HasPermission(Permisos.RegistrarReserva);
            _btnImprimir.Visible = puedeImprimir;
            _btnGuardarPdf.Visible = puedeImprimir;

            _btnCerrar.Click += (s, e) => Close();
            _btnImprimir.Click += (s, e) => _actual?.Imprimir(this);
            _btnGuardarPdf.Click += async (s, e) => await GuardarPdfAsync();
            _btnEnviar.Click += (s, e) => EnviarPorCorreo(automatico: false);

            Controls.Add(_pnlVista);
            Controls.Add(_pnlPestanas);
            Controls.Add(_pnlBanner);
            Controls.Add(_pnlAcciones);

            ResumeLayout(true);
        }

        private void MostrarDocumento(int indice)
        {
            _actual = _documentos[indice];
            for (int i = 0; i < _pestanas.Count; i++)
            {
                bool activa = i == indice;
                _pestanas[i].BackColor = activa ? ColorPanel : ColorFondo;
                _pestanas[i].ForeColor = activa ? Color.Goldenrod : ColorSuave;
                _pestanas[i].IconColor = _pestanas[i].ForeColor;
                _pestanas[i].Font = new Font("Segoe UI", 10F, activa ? FontStyle.Bold : FontStyle.Regular);
            }
            _pnlVista.AutoScrollPosition = Point.Empty;
            DescartarRender();
            AjustarLienzo();
            _lienzo.Invalidate();
        }

        private void DescartarRender()
        {
            _render?.Dispose();
            _render = null;
        }

        /// <summary>
        /// El documento se dibuja en una imagen cuya resolución coincide con el zoom: así las medidas
        /// (en centésimas de pulgada) y las fuentes (en puntos) escalan juntas, sea cual sea la escala de
        /// Windows (100%, 125%, 150%...). Después la imagen se copia al lienzo píxel a píxel.
        /// </summary>
        private void PintarLienzo(Graphics g)
        {
            if (_actual == null || _lienzo.Width <= 0 || _lienzo.Height <= 0) return;

            if (_render == null || _render.Size != _lienzo.Size)
            {
                DescartarRender();
                var imagen = new Bitmap(_lienzo.Width, _lienzo.Height);
                float dpi = _zoom * 100f;
                imagen.SetResolution(dpi, dpi);
                using (var gi = Graphics.FromImage(imagen))
                {
                    gi.Clear(ColorLienzo);
                    _actual.DibujarEnPantalla(gi, 1f, ColorLienzo);
                }
                _render = imagen;
            }

            g.DrawImage(_render, new Rectangle(0, 0, _render.Width, _render.Height),
                new Rectangle(0, 0, _render.Width, _render.Height), GraphicsUnit.Pixel);
        }

        /// <summary>El documento se muestra al ancho disponible de la ventana (con un tope) y centrado.</summary>
        private void AjustarLienzo()
        {
            if (_actual == null) return;

            var tamano = _actual.TamanoVista;
            int disponible = _pnlVista.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 40;
            // Tope: 1,5 px por centésima de pulgada (el comprobante no se agranda de más en pantallas anchas)
            _zoom = Math.Max(0.4f, Math.Min(1.5f, disponible / tamano.Width));

            var nuevo = new Size((int)Math.Floor(tamano.Width * _zoom), (int)Math.Floor(tamano.Height * _zoom));
            int x = Math.Max(20, (_pnlVista.ClientSize.Width - nuevo.Width) / 2);
            _lienzo.Bounds = new Rectangle(x + _pnlVista.AutoScrollPosition.X, 20 + _pnlVista.AutoScrollPosition.Y, nuevo.Width, nuevo.Height);
            _pnlVista.AutoScrollMinSize = new Size(nuevo.Width + 40, nuevo.Height + 40);
            _lienzo.Invalidate();
        }

        #endregion

        #region Banner y correo

        private void ActualizarBanner()
        {
            var r = _datos.Reserva;
            if (_esNuevaReserva)
            {
                _iconoBanner.IconChar = IconChar.CircleCheck;
                _iconoBanner.IconColor = ColorExito;
                _lblTitulo.ForeColor = ColorExito;
                _lblTitulo.Text = string.Format(T("Documento_bannerNueva"), r.Id.ToString("000000"), r.Habitacion?.NroHabitacion);
                _pnlBanner.BackColor = Color.FromArgb(14, 44, 40);
            }
            else
            {
                _iconoBanner.IconChar = IconChar.FileInvoice;
                _iconoBanner.IconColor = Color.Goldenrod;
                _lblTitulo.ForeColor = Color.WhiteSmoke;
                _lblTitulo.Text = string.Format(T("Documento_bannerConsulta"), r.Id.ToString("000000"),
                    OpcionEnum_68SA.Texto(r.Estado), $"{_datos.Titular?.Nombre} {_datos.Titular?.Apellido}");
            }

            string motivo = EnvioDocumentosReserva_68SA.MotivoNoEnvio(_datos);
            _btnEnviar.Enabled = motivo == null;
            MostrarEstadoCorreo(motivo ?? string.Format(T("Correo_disponible"), _datos.Email), motivo == null ? ColorSuave : ColorAviso);
        }

        private void MostrarEstadoCorreo(string texto, Color color)
        {
            _lblEstadoCorreo.Text = texto;
            _lblEstadoCorreo.ForeColor = color;
        }

        private void EnviarPorCorreo(bool automatico)
        {
            if (_enviando) return;
            string motivo = EnvioDocumentosReserva_68SA.MotivoNoEnvio(_datos);
            if (motivo != null)
            {
                MostrarEstadoCorreo(motivo, ColorAviso);
                if (!automatico)
                    MessageBox.Show(this, motivo, T("Correo_tituloNoEnviado"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _enviando = true;
            _btnEnviar.Enabled = false;
            MostrarEstadoCorreo(string.Format(T("Correo_enviando"), _datos.Email), ColorSuave);

            // El envío corre en otro hilo: se puede seguir usando la pantalla (o cerrarla) mientras tanto
            EnvioDocumentosReserva_68SA.Enviar(_datos, _documentos, (enviado, detalle) =>
            {
                _enviando = false;
                if (IsDisposed) return;
                _btnEnviar.Enabled = true;
                if (enviado)
                    MostrarEstadoCorreo(string.Format(T("Correo_enviado"), _datos.Email)
                        + (detalle == null ? "" : "  ·  " + string.Format(T("Correo_avisoAdjuntos"), detalle)),
                        detalle == null ? ColorExito : ColorAviso);
                else
                    MostrarEstadoCorreo(string.Format(T("Correo_error"), detalle), ColorError);
            });
        }

        #endregion

        #region PDF

        private async Task GuardarPdfAsync()
        {
            if (_actual == null) return;
            if (!DocumentoReserva_68SA.HayImpresoraPdf())
            {
                MessageBox.Show(this, string.Format(T("Documento_errorSinImpresoraPdf"), DocumentoReserva_68SA.ImpresoraPdf),
                    T("Documento_tituloPdf"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ruta;
            using (var dialogo = new SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = _actual.NombreArchivo,
                DefaultExt = "pdf",
                AddExtension = true,
                OverwritePrompt = true
            })
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                ruta = dialogo.FileName;
            }

            var documento = _actual;
            _btnGuardarPdf.Enabled = false;
            UseWaitCursor = true;
            try
            {
                // La generación espera a que termine la cola de impresión: se hace fuera del hilo de la pantalla
                string error = await Task.Run(() => documento.GuardarPdf(ruta));
                if (IsDisposed) return;

                if (error != null)
                {
                    MessageBox.Show(this, error, T("Documento_tituloPdf"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show(this, string.Format(T("Documento_pdfGuardado"), ruta), T("Documento_tituloPdf"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try { Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true }); } catch { }
                }
            }
            finally
            {
                if (!IsDisposed)
                {
                    UseWaitCursor = false;
                    _btnGuardarPdf.Enabled = true;
                }
            }
        }

        #endregion

        /// <summary>
        /// Abre el visor para una reserva existente (desde la lista o el calendario).
        /// </summary>
        public static void Mostrar(IWin32Window propietario, int reservaId, int indiceInicial = 0)
        {
            DatosDocumentoReserva_68SA datos;
            try
            {
                datos = DatosDocumentoReserva_68SA.Cargar(reservaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(propietario, string.Format(T("Reservas_msgErrorImprimir"), ex.Message), T("Comun_error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var visor = new FormDocumentosReserva_68SA(datos, indiceInicial))
                visor.ShowDialog(propietario);
        }
    }
}
