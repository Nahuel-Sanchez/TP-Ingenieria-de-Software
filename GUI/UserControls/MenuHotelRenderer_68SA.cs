using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    /// <summary>
    /// Dibujo de los menús desplegables con la estética del hotel: fondo azul noche, borde dorado,
    /// renglón resaltado redondeado con una barra dorada a la izquierda y flechas de submenú doradas.
    /// No depende de los colores del sistema, así que nunca aparece el cuadro gris/blanco de Windows.
    /// </summary>
    public class MenuHotelRenderer_68SA : ToolStripRenderer
    {
        #region Campos

        public static readonly Color Fondo = Color.FromArgb(10, 18, 50);
        public static readonly Color FondoResaltado = Color.FromArgb(24, 38, 86);
        public static readonly Color Borde = Color.FromArgb(150, 115, 30);
        public static readonly Color Separador = Color.FromArgb(38, 52, 92);
        public static readonly Color Texto = Color.FromArgb(225, 228, 236);
        public static readonly Color TextoResaltado = Color.Goldenrod;
        public static readonly Color TextoDeshabilitado = Color.FromArgb(105, 112, 132);

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

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (var fondo = new SolidBrush(Fondo))
                e.Graphics.FillRectangle(fondo, e.AffectedBounds);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            var r = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            using (var borde = new Pen(Borde))
                e.Graphics.DrawRectangle(borde, r);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            // Sin columna de íconos de otro color: el margen es parte del fondo
            using (var fondo = new SolidBrush(Fondo))
                e.Graphics.FillRectangle(fondo, e.AffectedBounds);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected || !e.Item.Enabled) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(4, 1, e.Item.Width - 8, e.Item.Height - 2);
            using (var camino = Redondeado(r, 6))
            using (var fondo = new SolidBrush(FondoResaltado))
                g.FillPath(fondo, camino);
            using (var barra = new SolidBrush(Color.Goldenrod))
                g.FillRectangle(barra, r.Left, r.Top + 6, 3, r.Height - 12);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = !e.Item.Enabled ? TextoDeshabilitado
                        : e.Item.Selected ? TextoResaltado
                        : Texto;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var centro = new Point(e.ArrowRectangle.Left + e.ArrowRectangle.Width / 2, e.ArrowRectangle.Top + e.ArrowRectangle.Height / 2);
            using (var lapiz = new Pen(e.Item != null && e.Item.Selected ? Color.Gold : Color.Goldenrod, 2f))
            {
                lapiz.StartCap = lapiz.EndCap = LineCap.Round;
                g.DrawLines(lapiz, new[]
                {
                    new Point(centro.X - 2, centro.Y - 4),
                    new Point(centro.X + 2, centro.Y),
                    new Point(centro.X - 2, centro.Y + 4)
                });
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            int y = e.Item.Height / 2;
            using (var linea = new Pen(Separador))
                e.Graphics.DrawLine(linea, 12, y, e.Item.Width - 12, y);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            if (e.Image == null) return;
            var imagen = e.Image;
            var destino = e.ImageRectangle;
            // El ícono se centra verticalmente en el renglón aunque el renglón sea más alto que la imagen
            int y = (e.Item.Height - imagen.Height) / 2;
            e.Graphics.DrawImage(imagen, new Rectangle(destino.X, Math.Max(0, y), imagen.Width, imagen.Height));
        }

        #endregion
    }
}
