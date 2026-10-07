using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    /// <summary>
    /// CheckBox dibujado con la estética del hotel (caja azul noche con borde dorado y tilde dorado),
    /// en lugar del recuadro blanco de Windows. Se usa igual que un CheckBox común (Checked, CheckedChanged, Text).
    /// </summary>
    public class CheckBoxHotel_68SA : CheckBox
    {
        #region Campos

        private const int Lado = 18;
        private const int Separacion = 8;
        private bool _sobre;

        #endregion

        #region Propiedades

        [Category("Apariencia hotel")] public Color ColorBorde { get; set; } = Color.Goldenrod;
        [Category("Apariencia hotel")] public Color ColorMarcado { get; set; } = Color.Goldenrod;
        [Category("Apariencia hotel")] public Color ColorFondoCaja { get; set; } = Color.FromArgb(5, 15, 45);
        [Category("Apariencia hotel")] public Color ColorTilde { get; set; } = Color.FromArgb(10, 15, 35);

        #endregion

        #region Constructor

        public CheckBoxHotel_68SA()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            Cursor = Cursors.Hand;
            ForeColor = Color.WhiteSmoke;
            BackColor = Color.Transparent;
        }

        #endregion

        #region Métodos auxiliares

        private static GraphicsPath Redondeado(Rectangle r, int radio)
        {
            int d = radio * 2;
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

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size texto = TextRenderer.MeasureText(Text ?? string.Empty, Font);
            return new Size(Lado + Separacion + texto.Width + Padding.Horizontal + 2, Math.Max(Lado + 6, texto.Height + 6));
        }

        protected override void OnMouseEnter(EventArgs eventargs) { _sobre = true; Invalidate(); base.OnMouseEnter(eventargs); }
        protected override void OnMouseLeave(EventArgs eventargs) { _sobre = false; Invalidate(); base.OnMouseLeave(eventargs); }
        protected override void OnCheckedChanged(EventArgs e) { Invalidate(); base.OnCheckedChanged(e); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }
        protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }
        protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Fondo: el del contenedor (el control es "transparente")
            Color fondo = BackColor == Color.Transparent && Parent != null ? Parent.BackColor : BackColor;
            e.Graphics.Clear(fondo);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var caja = new Rectangle(Padding.Left + 1, (Height - Lado) / 2, Lado - 1, Lado - 1);
            Color borde = !Enabled ? Color.FromArgb(90, 95, 110)
                        : (_sobre || Focused) ? Color.Gold : ColorBorde;

            using (var camino = Redondeado(caja, 4))
            {
                using (var relleno = new SolidBrush(Checked && Enabled ? ColorMarcado : ColorFondoCaja))
                    g.FillPath(relleno, camino);
                using (var lapiz = new Pen(borde, 1.6f))
                    g.DrawPath(lapiz, camino);
            }

            if (Checked)
            {
                using (var tilde = new Pen(Enabled ? ColorTilde : Color.FromArgb(150, 150, 160), 2.2f)
                       { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
                {
                    g.DrawLines(tilde, new[]
                    {
                        new PointF(caja.X + 4f, caja.Y + caja.Height * 0.52f),
                        new PointF(caja.X + caja.Width * 0.42f, caja.Bottom - 4.5f),
                        new PointF(caja.Right - 3.5f, caja.Y + 4.5f)
                    });
                }
            }

            var areaTexto = new Rectangle(caja.Right + Separacion, 0, Math.Max(0, Width - caja.Right - Separacion), Height);
            TextRenderer.DrawText(g, Text, Font, areaTexto, Enabled ? ForeColor : Color.FromArgb(120, 125, 140),
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        #endregion
    }
}
