using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    /// <summary>
    /// Panel con una imagen de fondo que cubre todo el panel sin deformarse (recorta lo que sobra,
    /// como "cover" en CSS) y un velo degradado encima para que el texto se lea bien.
    /// Los controles hijos con fondo transparente (logo, etiquetas) se ven sobre la imagen.
    /// </summary>
    public class PanelImagen_68SA : Panel
    {
        #region Campos

        private Image _imagen;
        private float _focoX = 0.5f, _focoY = 0.5f;
        private Color _veloArriba = Color.FromArgb(40, 5, 10, 40);
        private Color _veloAbajo = Color.FromArgb(200, 5, 10, 40);

        #endregion

        #region Propiedades

        [Category("Apariencia")]
        public Image Imagen
        {
            get => _imagen;
            set { _imagen = value; Invalidate(); }
        }

        /// <summary>Punto de la imagen que queda siempre visible al recortar (0 = izquierda/arriba, 1 = derecha/abajo).</summary>
        [Category("Apariencia"), DefaultValue(0.5f)]
        public float FocoX { get => _focoX; set { _focoX = Math.Max(0, Math.Min(1, value)); Invalidate(); } }

        [Category("Apariencia"), DefaultValue(0.5f)]
        public float FocoY { get => _focoY; set { _focoY = Math.Max(0, Math.Min(1, value)); Invalidate(); } }

        [Category("Apariencia")]
        public Color VeloArriba { get => _veloArriba; set { _veloArriba = value; Invalidate(); } }

        [Category("Apariencia")]
        public Color VeloAbajo { get => _veloAbajo; set { _veloAbajo = value; Invalidate(); } }

        #endregion

        #region Constructor

        public PanelImagen_68SA()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        #endregion

        #region Sobrescrituras

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics;
            using (var fondo = new SolidBrush(BackColor))
                g.FillRectangle(fondo, ClientRectangle);
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0) return;

            if (_imagen != null)
            {
                // Escala que cubre todo el panel y recorte alrededor del foco
                float escala = Math.Max(ClientSize.Width / (float)_imagen.Width, ClientSize.Height / (float)_imagen.Height);
                float ancho = ClientSize.Width / escala, alto = ClientSize.Height / escala;
                float x = (_imagen.Width - ancho) * _focoX, y = (_imagen.Height - alto) * _focoY;

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(_imagen, ClientRectangle, new RectangleF(x, y, ancho, alto), GraphicsUnit.Pixel);
            }

            using (var velo = new LinearGradientBrush(ClientRectangle, _veloArriba, _veloAbajo, LinearGradientMode.Vertical))
                g.FillRectangle(velo, ClientRectangle);
        }

        #endregion
    }
}
