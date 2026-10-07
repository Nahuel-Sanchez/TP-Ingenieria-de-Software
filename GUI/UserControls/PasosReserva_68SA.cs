using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    /// <summary>
    /// Indicador de pasos de un proceso (por ejemplo, una reserva: Fechas → Habitación → Huésped y pago).
    /// Los pasos ya hechos se ven con un tilde, el actual resaltado en dorado y los siguientes atenuados.
    /// </summary>
    public class PasosReserva_68SA : Control
    {
        #region Campos

        private static readonly Color Dorado = Color.Goldenrod;
        private static readonly Color Apagado = Color.FromArgb(70, 82, 120);
        private static readonly Color TextoActivo = Color.WhiteSmoke;
        private static readonly Color TextoApagado = Color.FromArgb(140, 148, 170);

        private string[] _pasos = { "1", "2", "3" };
        private int _actual;

        #endregion

        #region Propiedades

        /// <summary>Títulos de los pasos, en orden.</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] Pasos
        {
            get => _pasos;
            set { _pasos = value ?? new string[0]; Invalidate(); }
        }

        /// <summary>Paso actual, empezando en 0.</summary>
        [DefaultValue(0)]
        public int PasoActual
        {
            get => _actual;
            set { _actual = value; Invalidate(); }
        }

        #endregion

        #region Constructor

        public PasosReserva_68SA()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            Size = new Size(520, 64);
        }

        #endregion

        #region Sobrescrituras

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Parent?.BackColor ?? BackColor);
            if (_pasos.Length == 0) return;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            const int diametro = 30;
            float tramo = Width / (float)_pasos.Length;
            int cy = 6 + diametro / 2;

            // Líneas entre los círculos (dorada si el paso de la izquierda ya se hizo)
            for (int i = 0; i < _pasos.Length - 1; i++)
            {
                float x1 = tramo * i + tramo / 2 + diametro / 2 + 6;
                float x2 = tramo * (i + 1) + tramo / 2 - diametro / 2 - 6;
                using (var linea = new Pen(i < _actual ? Dorado : Apagado, 2f))
                    g.DrawLine(linea, x1, cy, x2, cy);
            }

            using (var fuenteNumero = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            using (var formatoCentro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                for (int i = 0; i < _pasos.Length; i++)
                {
                    float cx = tramo * i + tramo / 2;
                    var circulo = new RectangleF(cx - diametro / 2f, cy - diametro / 2f, diametro, diametro);
                    bool hecho = i < _actual, actual = i == _actual;

                    if (hecho || actual)
                        using (var relleno = new SolidBrush(Dorado))
                            g.FillEllipse(relleno, circulo);
                    else
                        using (var borde = new Pen(Apagado, 2f))
                            g.DrawEllipse(borde, circulo);

                    if (actual)
                        using (var halo = new Pen(Color.FromArgb(90, Dorado), 4f))
                            g.DrawEllipse(halo, RectangleF.Inflate(circulo, 4, 4));

                    if (hecho)
                    {
                        // Tilde
                        using (var tilde = new Pen(Color.FromArgb(10, 15, 35), 2.5f) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                            g.DrawLines(tilde, new[]
                            {
                                new PointF(cx - 6, cy), new PointF(cx - 1.5f, cy + 5), new PointF(cx + 7, cy - 5)
                            });
                    }
                    else
                    {
                        using (var numero = new SolidBrush(actual ? Color.FromArgb(10, 15, 35) : TextoApagado))
                            g.DrawString((i + 1).ToString(), fuenteNumero, numero, circulo, formatoCentro);
                    }

                    var zonaTexto = new Rectangle((int)(tramo * i), cy + diametro / 2 + 4, (int)tramo, Height - (cy + diametro / 2 + 4));
                    TextRenderer.DrawText(g, _pasos[i], Font, zonaTexto, hecho || actual ? TextoActivo : TextoApagado,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.Top | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                }
            }
        }

        #endregion
    }
}
