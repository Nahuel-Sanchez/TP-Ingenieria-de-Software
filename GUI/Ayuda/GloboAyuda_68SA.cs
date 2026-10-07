using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_08YS.Ayuda
{
    /// <summary>
    /// Globo de ayuda que aparece junto al puntero con la explicación de un elemento de la pantalla
    /// (ayuda por posición del mouse: F1 sobre el elemento o modo "¿Qué es esto?").
    /// No le quita el foco a la pantalla; se cierra con cualquier clic o tecla.
    /// </summary>
    public class GloboAyuda_68SA : Form
    {
        #region Campos

        private static readonly Color Fondo = Color.FromArgb(10, 18, 50);
        private const int AnchoTexto = 380;

        #endregion

        #region Eventos

        public event EventHandler VerAyudaCompleta;

        #endregion

        #region Constructor

        public GloboAyuda_68SA(string titulo, string texto, string textoEnlace, bool mostrarEnlace = true)
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            BackColor = Color.Goldenrod;          // el borde dorado es el fondo que se ve por el Padding
            Padding = new Padding(1);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Font = new Font("Segoe UI", 9.5f);

            var cuerpo = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Fondo,
                ColumnCount = 2,
                Dock = DockStyle.Fill,               // queda dentro del Padding(1): 1 px de borde dorado alrededor
                Padding = new Padding(12, 10, 14, 10),
                Margin = Padding.Empty
            };
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            Controls.Add(cuerpo);

            var icono = new IconPictureBox
            {
                BackColor = Fondo, IconChar = IconChar.CircleQuestion, IconColor = Color.Goldenrod, IconFont = IconFont.Auto,
                IconSize = 22, Size = new Size(24, 24), Margin = new Padding(0, 1, 10, 0)
            };
            cuerpo.Controls.Add(icono, 0, 0);
            cuerpo.SetRowSpan(icono, 3);

            cuerpo.Controls.Add(new Label
            {
                AutoSize = true, MaximumSize = new Size(AnchoTexto, 0), UseMnemonic = false, BackColor = Fondo,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold), ForeColor = Color.Goldenrod, Text = titulo ?? string.Empty,
                Margin = new Padding(0, 2, 0, 4)
            }, 1, 0);
            cuerpo.Controls.Add(new Label
            {
                AutoSize = true, MaximumSize = new Size(AnchoTexto, 0), UseMnemonic = false, BackColor = Fondo,
                ForeColor = Color.FromArgb(228, 231, 240), Text = texto ?? string.Empty, Margin = new Padding(0, 0, 0, 2)
            }, 1, 1);

            if (mostrarEnlace)
            {
                var enlace = new LinkLabel
                {
                    AutoSize = true, BackColor = Fondo, Text = textoEnlace ?? string.Empty, UseMnemonic = false,
                    LinkColor = Color.FromArgb(127, 178, 255), ActiveLinkColor = Color.Gold, VisitedLinkColor = Color.FromArgb(127, 178, 255),
                    LinkBehavior = LinkBehavior.HoverUnderline, Margin = new Padding(0, 8, 0, 0)
                };
                enlace.LinkClicked += (s, e) => VerAyudaCompleta?.Invoke(this, EventArgs.Empty);
                cuerpo.Controls.Add(enlace, 1, 2);
            }
        }

        #endregion

        #region Métodos públicos

        /// <summary>Muestra el globo cerca del punto (en coordenadas de pantalla), sin salirse de la pantalla.</summary>
        public void MostrarEn(Point punto)
        {
            var tamaño = GetPreferredSize(Size.Empty);
            Rectangle area = Screen.FromPoint(punto).WorkingArea;
            int x = punto.X + 14, y = punto.Y + 20;
            if (x + tamaño.Width > area.Right) x = Math.Max(area.Left, area.Right - tamaño.Width - 4);
            if (y + tamaño.Height > area.Bottom) y = Math.Max(area.Top, punto.Y - tamaño.Height - 10);
            Location = new Point(x, y);
            Show();
        }

        #endregion

        #region Sobrescrituras

        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOOLWINDOW = 0x80, WS_EX_TOPMOST = 0x8, CS_DROPSHADOW = 0x20000;
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW | WS_EX_TOPMOST;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        #endregion
    }
}
