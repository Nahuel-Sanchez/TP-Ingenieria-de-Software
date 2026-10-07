using FontAwesome.Sharp;
using Service_08YS;
using System;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    /// <summary>
    /// Cuadro de mensaje con la estética del hotel (azul noche, borde y acentos dorados).
    /// Reemplaza al MessageBox de Windows en toda la aplicación: ver la clase MessageBox de más abajo.
    /// Mismo comportamiento: botones estándar, botón por defecto, Enter acepta, Esc cancela, sonido del sistema.
    /// </summary>
    public sealed class FormMensaje_68SA : Form
    {
        #region Campos

        private static readonly Color Fondo = Color.FromArgb(5, 10, 40);
        private static readonly Color FondoBarra = Color.FromArgb(10, 18, 50);
        private static readonly Color FondoCampo = Color.FromArgb(5, 15, 45);
        private static readonly Color TextoOscuro = Color.FromArgb(10, 15, 35);
        private const int AnchoTexto = 500;
        private const int AltoMaximoTexto = 380;

        private readonly MessageBoxIcon _icono;

        #endregion

        #region Constructor

        private FormMensaje_68SA(string texto, string titulo, MessageBoxButtons botones, MessageBoxIcon icono,
                                 MessageBoxDefaultButton botonPorDefecto)
        {
            _icono = icono;

            SuspendLayout();
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            KeyPreview = true;
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.Goldenrod;        // el padding deja ver un borde dorado de 2 px
            Padding = new Padding(2);
            Font = new Font("Segoe UI", 10F);
            Text = titulo ?? string.Empty;

            var raiz = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Fondo,
                Margin = new Padding(0),
                Padding = new Padding(0),
                MinimumSize = new Size(440, 0)
            };
            raiz.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            raiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            raiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            raiz.Controls.Add(CrearCuerpo(texto, titulo, icono), 0, 0);
            raiz.Controls.Add(CrearPie(botones, botonPorDefecto), 0, 1);
            Controls.Add(raiz);

            // Sin barra de título: se arrastra desde cualquier parte del cuerpo
            HabilitarArrastre(raiz);
            ResumeLayout(true);
        }

        #endregion

        #region Construcción

        private Control CrearCuerpo(string texto, string titulo, MessageBoxIcon icono)
        {
            var cuerpo = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Fill,
                BackColor = Fondo,
                Margin = new Padding(0),
                Padding = new Padding(24, 22, 28, 20)
            };
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            var (iconoChar, color) = EstiloIcono(icono);
            if (iconoChar != IconChar.None)
            {
                cuerpo.Controls.Add(new IconPictureBox
                {
                    IconChar = iconoChar,
                    IconColor = color,
                    IconSize = 44,
                    BackColor = Fondo,
                    ForeColor = color,
                    Size = new Size(48, 48),
                    Margin = new Padding(0, 2, 18, 0)
                }, 0, 0);
            }

            var textos = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Fondo,
                Margin = new Padding(0)
            };
            textos.Controls.Add(new Label
            {
                AutoSize = true,
                MaximumSize = new Size(AnchoTexto, 0),
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Text = string.IsNullOrWhiteSpace(titulo) ? TituloPorDefecto(icono) : titulo,
                Margin = new Padding(0, 0, 0, 10)
            });
            textos.Controls.Add(CrearTexto(texto ?? string.Empty));
            cuerpo.Controls.Add(textos, iconoChar == IconChar.None ? 0 : 1, 0);
            if (iconoChar == IconChar.None) cuerpo.SetColumnSpan(textos, 2);
            return cuerpo;
        }

        /// <summary>Texto del mensaje; si es muy largo (por ejemplo, un error técnico) va en un cuadro con desplazamiento.</summary>
        private Control CrearTexto(string texto)
        {
            var etiqueta = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(AnchoTexto, 0),
                ForeColor = Color.WhiteSmoke,
                Text = texto,
                Margin = new Padding(0),
                UseMnemonic = false
            };
            if (etiqueta.GetPreferredSize(new Size(AnchoTexto, 0)).Height <= AltoMaximoTexto)
                return etiqueta;

            etiqueta.Dispose();
            return new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BorderStyle = BorderStyle.None,
                BackColor = FondoCampo,
                ForeColor = Color.WhiteSmoke,
                Text = texto.Replace("\r\n", "\n").Replace("\n", Environment.NewLine),
                Size = new Size(AnchoTexto, AltoMaximoTexto),
                Margin = new Padding(0),
                TabStop = false
            };
        }

        private Control CrearPie(MessageBoxButtons botones, MessageBoxDefaultButton botonPorDefecto)
        {
            var pie = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Dock = DockStyle.Fill,
                BackColor = FondoBarra,
                Margin = new Padding(0),
                Padding = new Padding(16, 12, 16, 12)
            };

            var resultados = Resultados(botones);
            int indicePorDefecto = Math.Min(resultados.Length - 1,
                botonPorDefecto == MessageBoxDefaultButton.Button3 ? 2 : botonPorDefecto == MessageBoxDefaultButton.Button2 ? 1 : 0);

            // RightToLeft: se agregan del último al primero para que queden en el orden de Windows
            for (int i = resultados.Length - 1; i >= 0; i--)
            {
                var boton = CrearBoton(resultados[i], i == indicePorDefecto);
                pie.Controls.Add(boton);
                if (i == indicePorDefecto) AcceptButton = boton;
            }

            // Esc: Cancelar / No / (si solo hay Aceptar) Aceptar
            DialogResult escape = Array.IndexOf(resultados, DialogResult.Cancel) >= 0 ? DialogResult.Cancel
                                : Array.IndexOf(resultados, DialogResult.No) >= 0 ? DialogResult.No
                                : resultados.Length == 1 ? resultados[0] : DialogResult.None;
            foreach (Control c in pie.Controls)
                if (c is Button b && b.DialogResult == escape) CancelButton = b;

            Shown += (s, e) => (AcceptButton as Control)?.Focus();
            return pie;
        }

        private static IconButton CrearBoton(DialogResult resultado, bool principal)
        {
            Color frente = principal ? TextoOscuro : Color.Goldenrod;
            var fuente = new Font("Segoe UI", 10F, FontStyle.Bold);
            string texto = TextoBoton(resultado);
            // Ancho según el texto (y el escalado de Windows): "Reintentar" con ícono no entra en un ancho fijo a 150 %
            int ancho = Math.Max(124, TextRenderer.MeasureText(texto, fuente).Width + 56);
            int alto = Math.Max(40, fuente.Height + 18);
            var boton = new IconButton
            {
                Text = texto,
                DialogResult = resultado,
                AutoSize = false,
                Size = new Size(ancho, alto),
                Margin = new Padding(10, 0, 0, 0),
                FlatStyle = FlatStyle.Flat,
                BackColor = principal ? Color.Goldenrod : FondoCampo,
                ForeColor = frente,
                Font = fuente,
                IconChar = IconoBoton(resultado),
                IconColor = frente,
                IconSize = 18,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            boton.FlatAppearance.BorderColor = Color.Goldenrod;
            boton.FlatAppearance.BorderSize = 1;
            boton.FlatAppearance.MouseOverBackColor = principal ? Color.Gold : Color.FromArgb(15, 28, 70);
            return boton;
        }

        private static DialogResult[] Resultados(MessageBoxButtons botones)
        {
            switch (botones)
            {
                case MessageBoxButtons.OKCancel: return new[] { DialogResult.OK, DialogResult.Cancel };
                case MessageBoxButtons.AbortRetryIgnore: return new[] { DialogResult.Abort, DialogResult.Retry, DialogResult.Ignore };
                case MessageBoxButtons.YesNoCancel: return new[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel };
                case MessageBoxButtons.YesNo: return new[] { DialogResult.Yes, DialogResult.No };
                case MessageBoxButtons.RetryCancel: return new[] { DialogResult.Retry, DialogResult.Cancel };
                default: return new[] { DialogResult.OK };
            }
        }

        private static string TextoBoton(DialogResult resultado)
        {
            switch (resultado)
            {
                case DialogResult.Cancel: return Texto("Mensaje_btnCancelar", "Cancelar");
                case DialogResult.Yes: return Texto("Mensaje_btnSi", "Sí");
                case DialogResult.No: return Texto("Mensaje_btnNo", "No");
                case DialogResult.Abort: return Texto("Mensaje_btnAnular", "Anular");
                case DialogResult.Retry: return Texto("Mensaje_btnReintentar", "Reintentar");
                case DialogResult.Ignore: return Texto("Mensaje_btnOmitir", "Omitir");
                default: return Texto("Mensaje_btnAceptar", "Aceptar");
            }
        }

        private static IconChar IconoBoton(DialogResult resultado)
        {
            switch (resultado)
            {
                case DialogResult.OK:
                case DialogResult.Yes: return IconChar.Check;
                case DialogResult.Cancel:
                case DialogResult.No:
                case DialogResult.Abort: return IconChar.Xmark;
                case DialogResult.Retry: return IconChar.RotateRight;
                default: return IconChar.None;
            }
        }

        private static (IconChar Icono, Color Color) EstiloIcono(MessageBoxIcon icono)
        {
            switch (icono)
            {
                case MessageBoxIcon.Error: return (IconChar.CircleXmark, Color.FromArgb(235, 90, 90));
                case MessageBoxIcon.Warning: return (IconChar.TriangleExclamation, Color.FromArgb(240, 180, 60));
                case MessageBoxIcon.Question: return (IconChar.CircleQuestion, Color.Goldenrod);
                case MessageBoxIcon.Information: return (IconChar.CircleInfo, Color.FromArgb(100, 170, 255));
                default: return (IconChar.None, Color.Goldenrod);
            }
        }

        private static string TituloPorDefecto(MessageBoxIcon icono)
        {
            switch (icono)
            {
                case MessageBoxIcon.Error: return Texto("Mensaje_tituloError", "Error");
                case MessageBoxIcon.Warning: return Texto("Mensaje_tituloAtencion", "Atención");
                case MessageBoxIcon.Question: return Texto("Mensaje_tituloConfirmar", "Confirmar");
                default: return Texto("Mensaje_tituloInformacion", "Información");
            }
        }

        // El traductor puede no estar listo (por ejemplo, un error al arrancar): se usa el texto en español
        private static string Texto(string clave, string porDefecto)
        {
            try
            {
                string texto = TraductorManager_08YS.Instance.GetTexto(clave);
                return string.IsNullOrEmpty(texto) || texto == $"[{clave}]" ? porDefecto : texto;
            }
            catch
            {
                return porDefecto;
            }
        }

        #endregion

        #region Arrastre y sonido

        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private void HabilitarArrastre(Control control)
        {
            if (!(control is ButtonBase) && !(control is TextBoxBase))
                control.MouseDown += (s, e) =>
                {
                    if (e.Button != MouseButtons.Left) return;
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);
                };
            foreach (Control hijo in control.Controls)
                HabilitarArrastre(hijo);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            switch (_icono)
            {
                case MessageBoxIcon.Error: SystemSounds.Hand.Play(); break;
                case MessageBoxIcon.Warning: SystemSounds.Exclamation.Play(); break;
                case MessageBoxIcon.Question: SystemSounds.Question.Play(); break;
                case MessageBoxIcon.Information: SystemSounds.Asterisk.Play(); break;
            }
        }

        // Sombra del sistema alrededor de la ventana sin borde
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x20000; // CS_DROPSHADOW
                return cp;
            }
        }

        #endregion

        #region Métodos públicos

        /// <summary>Muestra el mensaje centrado sobre su ventana (o en la pantalla si no hay ninguna) y devuelve el botón elegido.</summary>
        public static DialogResult Mostrar(IWin32Window propietario, string texto, string titulo, MessageBoxButtons botones,
                                           MessageBoxIcon icono, MessageBoxDefaultButton botonPorDefecto)
        {
            propietario = propietario ?? Form.ActiveForm;
            // Las pantallas del menú son formularios hijos: se centra sobre la ventana principal que las contiene
            if (propietario is Control control && control.TopLevelControl != null)
                propietario = control.TopLevelControl;
            using (var mensaje = new FormMensaje_68SA(texto, titulo, botones, icono, botonPorDefecto))
            {
                if (propietario == null)
                {
                    mensaje.StartPosition = FormStartPosition.CenterScreen;
                    mensaje.TopMost = true;
                    return mensaje.ShowDialog();
                }
                mensaje.StartPosition = FormStartPosition.CenterParent;
                return mensaje.ShowDialog(propietario);
            }
        }

        #endregion
    }
}

namespace GUI_08YS
{
    /// <summary>
    /// Reemplazo del MessageBox de Windows con la estética del hotel. Como está declarado en el espacio de nombres
    /// GUI_08YS, todas las pantallas (GUI_08YS.*) lo usan en lugar de System.Windows.Forms.MessageBox sin cambiar
    /// sus llamadas: MessageBox.Show(...) sigue igual y devuelve el mismo DialogResult.
    /// </summary>
    public static class MessageBox
    {
        #region Métodos públicos

        public static DialogResult Show(string text)
            => Show(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        public static DialogResult Show(string text, string caption)
            => Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
            => Show(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            => Show(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
            => Show(null, text, caption, buttons, icon, defaultButton);

        public static DialogResult Show(IWin32Window owner, string text)
            => Show(owner, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        public static DialogResult Show(IWin32Window owner, string text, string caption)
            => Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
            => Show(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            => Show(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1);

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons,
                                        MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
            => UserControls.FormMensaje_68SA.Mostrar(owner, text, caption, buttons, icon, defaultButton);

        #endregion
    }
}

namespace GUI
{
    /// <summary>Las pantallas del espacio de nombres GUI (bitácora, usuarios) usan el mismo cuadro de mensaje.</summary>
    internal static class MessageBox
    {
        #region Métodos públicos

        public static DialogResult Show(string text) => GUI_08YS.MessageBox.Show(text);
        public static DialogResult Show(string text, string caption) => GUI_08YS.MessageBox.Show(text, caption);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
            => GUI_08YS.MessageBox.Show(text, caption, buttons);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            => GUI_08YS.MessageBox.Show(text, caption, buttons, icon);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
            => GUI_08YS.MessageBox.Show(text, caption, buttons, icon, defaultButton);
        public static DialogResult Show(IWin32Window owner, string text) => GUI_08YS.MessageBox.Show(owner, text);
        public static DialogResult Show(IWin32Window owner, string text, string caption) => GUI_08YS.MessageBox.Show(owner, text, caption);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
            => GUI_08YS.MessageBox.Show(owner, text, caption, buttons);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            => GUI_08YS.MessageBox.Show(owner, text, caption, buttons, icon);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
            => GUI_08YS.MessageBox.Show(owner, text, caption, buttons, icon, defaultButton);

        #endregion
    }
}
