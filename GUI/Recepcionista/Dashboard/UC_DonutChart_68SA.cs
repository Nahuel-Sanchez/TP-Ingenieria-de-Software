using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista.Dashboard
{
    public partial class UC_DonutChart_68SA : UserControl
    {
        private List<SegmentoDonut_68SA> _segmentos = new List<SegmentoDonut_68SA>();
        private string _textoCentral = "";
        private string _subtextoCentral = "";

        public UC_DonutChart_68SA()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void Cargar(List<SegmentoDonut_68SA> segmentos, string textoCentral, string subtextoCentral)
        {
            _segmentos = segmentos ?? new List<SegmentoDonut_68SA>();
            _textoCentral = textoCentral;
            _subtextoCentral = subtextoCentral;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int areaDisponible = System.Math.Min(Width, Height) - 16;
            if (areaDisponible <= 0) return;

            int grosor = System.Math.Max(14, areaDisponible / 7);
            // El trazo se dibuja centrado sobre este diámetro (grosor/2 hacia afuera y hacia adentro),
            // así que lo achicamos "grosor" respecto al área disponible para que el borde externo
            // del anillo no se salga del control.
            int diametroCentro = areaDisponible - grosor;
            if (diametroCentro <= 0) return;

            var rect = new Rectangle((Width - diametroCentro) / 2, (Height - diametroCentro) / 2, diametroCentro, diametroCentro);
            decimal total = _segmentos.Sum(s => s.Valor);

            if (total <= 0 || _segmentos.Count == 0)
            {
                using (var pen = new Pen(Color.FromArgb(35, 48, 85), grosor))
                    g.DrawEllipse(pen, rect);
            }
            else
            {
                float anguloInicio = -90f;
                foreach (var segmento in _segmentos)
                {
                    float barrido = (float)(segmento.Valor / total) * 360f;
                    if (barrido <= 0) continue;

                    using (var pen = new Pen(segmento.Color, grosor))
                        g.DrawArc(pen, rect, anguloInicio, barrido);

                    anguloInicio += barrido;
                }
            }

            int diametroInterior = diametroCentro - grosor;
            if (diametroInterior < 20) diametroInterior = 20;

            using (var brushCentral = new SolidBrush(Color.Goldenrod))
            {
                float tamanoFuente = System.Math.Max(9F, diametroInterior / 5.5F);
                Font fontCentral = new Font("Segoe UI", tamanoFuente, FontStyle.Bold);
                SizeF medida = g.MeasureString(_textoCentral, fontCentral);

                // Achica la fuente hasta que el texto entre en el hueco interior — evita que se
                // superponga con el anillo cuando el monto tiene muchos dígitos.
                while (medida.Width > diametroInterior * 0.82F && tamanoFuente > 8F)
                {
                    fontCentral.Dispose();
                    tamanoFuente -= 1F;
                    fontCentral = new Font("Segoe UI", tamanoFuente, FontStyle.Bold);
                    medida = g.MeasureString(_textoCentral, fontCentral);
                }

                g.DrawString(_textoCentral, fontCentral, brushCentral,
                    (Width - medida.Width) / 2, (Height - medida.Height) / 2 - 6);
                fontCentral.Dispose();
            }

            using (var fontSub = new Font("Segoe UI", 8F))
            using (var brushSub = new SolidBrush(Color.FromArgb(160, 165, 180)))
            {
                var tamano = g.MeasureString(_subtextoCentral, fontSub);
                g.DrawString(_subtextoCentral, fontSub, brushSub,
                    (Width - tamano.Width) / 2, Height / 2 + 14);
            }
        }
    }

    public class SegmentoDonut_68SA
    {
        public string Etiqueta { get; set; }
        public decimal Valor { get; set; }
        public Color Color { get; set; }
    }
}