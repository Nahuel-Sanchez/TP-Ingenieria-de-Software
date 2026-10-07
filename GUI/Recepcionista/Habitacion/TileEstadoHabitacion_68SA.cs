using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Indicador de Control de habitaciones: cantidad de habitaciones en un estado, con su color.
    /// Funciona también como filtro: al hacer clic queda seleccionado (borde dorado) y avisa con Seleccionado.
    /// </summary>
    public class TileEstadoHabitacion_68SA : Control
    {
        #region Campos

        private static readonly Color Fondo = Color.FromArgb(12, 22, 56);
        private static readonly Color FondoSobre = Color.FromArgb(18, 32, 76);
        private static readonly Color Borde = Color.FromArgb(35, 48, 85);

        private bool _sobre;
        private bool _activo;
        private int _cantidad;
        private readonly float _escala;

        private static readonly Font FuenteTitulo = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        private static readonly Font FuenteCantidad = new Font("Segoe UI", 17f, FontStyle.Bold);

        #endregion

        #region Propiedades

        /// <summary>Estado que representa (null = todas las habitaciones).</summary>
        public object Valor { get; set; }
        public Color Acento { get; set; } = Color.Goldenrod;
        public string Titulo { get => Text; set { Text = value; AjustarAncho(); Invalidate(); } }

        public int Cantidad
        {
            get => _cantidad;
            set { _cantidad = value; Invalidate(); }
        }

        public bool Activo
        {
            get => _activo;
            set { _activo = value; Invalidate(); }
        }

        #endregion

        #region Eventos

        public event EventHandler Seleccionado;

        #endregion

        #region Constructor

        public TileEstadoHabitacion_68SA()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw, true);
            _escala = Math.Max(1f, DeviceDpi / 96f);
            Size = new Size(E(164), E(66));
            Margin = new Padding(0, 0, E(12), E(8));
            Cursor = Cursors.Hand;
        }

        #endregion

        #region Métodos auxiliares

        private int E(float valor) => (int)Math.Round(valor * _escala);

        /// <summary>
        /// El ancho se adapta al título (en mayúsculas, como se dibuja) para que nunca quede cortado
        /// con "...", con un mínimo para que todos los indicadores se vean parejos.
        /// </summary>
        private void AjustarAncho()
        {
            string titulo = (Text ?? string.Empty).ToUpper();
            int ancho = TextRenderer.MeasureText(titulo, FuenteTitulo, Size.Empty,
                                                 TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine).Width;
            Width = Math.Max(E(150), ancho + E(14) + E(16));
        }

        private static GraphicsPath Redondeado(Rectangle r, int radio)
        {
            int d = Math.Max(2, radio * 2);
            var camino = new GraphicsPath();
            camino.AddArc(r.X, r.Y, d, d, 180, 90);
            camino.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            camino.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            camino.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            camino.CloseFigure();
            return camino;
        }

        #endregion

        #region Sobrescrituras

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Seleccionado?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseEnter(EventArgs e) { _sobre = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _sobre = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Parent?.BackColor ?? Color.FromArgb(5, 10, 40));
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var camino = Redondeado(rect, E(9)))
            {
                using (var fondo = new SolidBrush(_activo || _sobre ? FondoSobre : Fondo))
                    g.FillPath(fondo, camino);

                // Barra del color del estado abajo
                var recorte = g.Clip;
                g.SetClip(camino);
                using (var barra = new SolidBrush(Acento))
                    g.FillRectangle(barra, 0, Height - E(4), Width, E(4));
                g.Clip = recorte;

                using (var borde = new Pen(_activo ? Color.Goldenrod : _sobre ? Color.FromArgb(90, 100, 130) : Borde, _activo ? 2f : 1f))
                    g.DrawPath(borde, camino);
            }

            TextRenderer.DrawText(g, _cantidad.ToString(), FuenteCantidad, new Point(E(12), E(6)),
                _activo ? Color.Goldenrod : Color.WhiteSmoke, TextFormatFlags.NoPrefix);
            TextRenderer.DrawText(g, (Text ?? string.Empty).ToUpper(), FuenteTitulo,
                new Rectangle(E(14), E(40), Width - E(16), E(18)), Color.FromArgb(170, 175, 190),
                TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine);
        }

        #endregion
    }
}
