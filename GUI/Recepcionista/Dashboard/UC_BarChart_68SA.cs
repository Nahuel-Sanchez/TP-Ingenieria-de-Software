using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista.Dashboard
{
    /// <summary>Una barra del gráfico: etiqueta del eje y un valor por serie (las series se apilan).</summary>
    public class BarraGrafico_68SA
    {
        #region Propiedades

        public string Etiqueta { get; set; }
        /// <summary>Texto del encabezado del globo al pasar el mouse (si es null se usa la etiqueta).</summary>
        public string Titulo { get; set; }
        public decimal[] Valores { get; set; } = new decimal[0];
        public decimal Total => Valores?.Sum() ?? 0;

        #endregion
    }

    /// <summary>
    /// Gráfico de barras dibujado a mano con la estética del Dashboard: verticales (con series apiladas
    /// y líneas guía) u horizontales (ranking). Al pasar el mouse resalta la barra y muestra sus valores.
    /// </summary>
    public class UC_BarChart_68SA : Control
    {
        #region Campos

        private static readonly Color ColorGuia = Color.FromArgb(32, 44, 82);
        private static readonly Color ColorEje = Color.FromArgb(140, 146, 165);
        private static readonly Color ColorResaltado = Color.FromArgb(20, 32, 72);
        private static readonly Color FondoGlobo = Color.FromArgb(4, 8, 30);

        private List<BarraGrafico_68SA> _datos = new List<BarraGrafico_68SA>();
        private Color[] _colores = { Color.Goldenrod };
        private string[] _series = new string[0];
        private Func<decimal, string> _formato = v => v.ToString("N0");
        private decimal? _maximo;
        private int _sobre = -1;

        #endregion

        #region Propiedades

        /// <summary>true: barras horizontales (etiqueta a la izquierda, valor a la derecha).</summary>
        public bool Horizontal { get; set; }

        /// <summary>Texto que se muestra cuando no hay datos.</summary>
        public string TextoVacio { get; set; } = "-";

        /// <summary>Etiqueta del total en el globo cuando hay varias series.</summary>
        public string TextoTotal { get; set; } = "Total";

        #endregion

        #region Constructor

        public UC_BarChart_68SA()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.FromArgb(10, 18, 50);
            ForeColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 8.25F);
        }

        #endregion

        #region Mouse

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int indice = IndiceEn(e.Location);
            if (indice == _sobre) return;
            _sobre = indice;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_sobre < 0) return;
            _sobre = -1;
            Invalidate();
        }

        private int IndiceEn(Point p)
        {
            if (_datos.Count == 0) return -1;
            Rectangle area = AreaGrafico();
            if (!area.Contains(p)) return -1;
            if (Horizontal)
            {
                int alto = AltoRenglon(area);
                int i = (p.Y - area.Top) / alto;
                return i >= 0 && i < _datos.Count ? i : -1;
            }
            float ranura = area.Width / (float)_datos.Count;
            int indice = (int)((p.X - area.Left) / ranura);
            return indice >= 0 && indice < _datos.Count ? indice : -1;
        }

        #endregion

        #region Dibujo

        private bool HayLeyenda => !Horizontal && _series.Length > 1;

        private Rectangle AreaGrafico()
        {
            if (Horizontal)
                return new Rectangle(0, 2, Math.Max(1, Width - 2), Math.Max(1, Height - 4));

            int izquierda = AnchoEjeY() + 10;
            int arriba = HayLeyenda ? 26 : 10;
            return new Rectangle(izquierda, arriba, Math.Max(1, Width - izquierda - 8), Math.Max(1, Height - arriba - 26));
        }

        private int AnchoEjeY()
        {
            decimal maximo = MaximoEje();
            return Math.Max(TextRenderer.MeasureText(_formato(maximo), Font).Width,
                            TextRenderer.MeasureText(_formato(maximo / 2), Font).Width);
        }

        private decimal MaximoEje()
        {
            decimal maximo = _maximo ?? (_datos.Count == 0 ? 0 : _datos.Max(d => d.Total));
            return _maximo.HasValue ? maximo : Redondear(maximo);
        }

        /// <summary>Máximo "redondo" para las líneas guía (1, 2, 2.5 o 5 por potencia de 10).</summary>
        private static decimal Redondear(decimal valor)
        {
            if (valor <= 0) return 1;
            double potencia = Math.Pow(10, Math.Floor(Math.Log10((double)valor)));
            foreach (double paso in new[] { 1, 2, 2.5, 5, 10 })
                if (paso * potencia >= (double)valor)
                    return (decimal)(paso * potencia);
            return (decimal)(10 * potencia);
        }

        private int AltoRenglon(Rectangle area)
            => _datos.Count == 0 ? area.Height : Math.Max(26, Math.Min(42, area.Height / _datos.Count));

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (_datos.Count == 0 || _datos.All(d => d.Total <= 0))
            {
                TextRenderer.DrawText(g, TextoVacio, Font, ClientRectangle, ColorEje,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
                return;
            }

            if (Horizontal) PintarHorizontal(g);
            else PintarVertical(g);
        }

        private void PintarVertical(Graphics g)
        {
            Rectangle area = AreaGrafico();
            decimal maximo = Math.Max(0.0001m, MaximoEje());

            if (HayLeyenda) PintarLeyenda(g);

            // Líneas guía y valores del eje
            using (var guia = new Pen(ColorGuia))
            {
                for (int i = 0; i <= 4; i++)
                {
                    int y = area.Bottom - (int)(area.Height * i / 4f);
                    g.DrawLine(guia, area.Left, y, area.Right, y);
                    string texto = _formato(maximo * i / 4);
                    var zona = new Rectangle(0, y - 8, area.Left - 6, 16);
                    TextRenderer.DrawText(g, texto, Font, zona, ColorEje, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                }
            }

            float ranura = area.Width / (float)_datos.Count;
            float ancho = Math.Max(3, Math.Min(ranura * 0.62f, 46));
            int anchoEtiqueta = Math.Max(1, _datos.Max(d => TextRenderer.MeasureText(d.Etiqueta ?? "", Font).Width) + 8);
            int salto = Math.Max(1, (int)Math.Ceiling(anchoEtiqueta / Math.Max(1f, ranura)));

            for (int i = 0; i < _datos.Count; i++)
            {
                var d = _datos[i];
                float x = area.Left + i * ranura;

                if (i == _sobre)
                    using (var resaltado = new SolidBrush(ColorResaltado))
                        g.FillRectangle(resaltado, x + 1, area.Top, ranura - 2, area.Height);

                // Segmentos apilados de abajo hacia arriba; el último lleva las puntas redondeadas
                float yBase = area.Bottom;
                int ultimo = Array.FindLastIndex(d.Valores, v => v > 0);
                for (int s = 0; s < d.Valores.Length; s++)
                {
                    if (d.Valores[s] <= 0) continue;
                    float alto = (float)(d.Valores[s] / maximo) * area.Height;
                    var rect = new RectangleF(x + (ranura - ancho) / 2, yBase - alto, ancho, alto);
                    using (var pincel = new SolidBrush(_colores[s % _colores.Length]))
                    {
                        if (s == ultimo && rect.Height > 4) RellenarPuntaRedondeada(g, pincel, rect, Math.Min(5f, ancho / 2));
                        else g.FillRectangle(pincel, rect);
                    }
                    yBase -= alto;
                }

                if (i % salto == 0)
                    TextRenderer.DrawText(g, d.Etiqueta, Font, new Rectangle((int)(x + ranura / 2 - anchoEtiqueta), area.Bottom + 4, anchoEtiqueta * 2, 18),
                        i == _sobre ? Color.Goldenrod : ColorEje, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPrefix);
            }

            if (_sobre >= 0) PintarGlobo(g, area, _sobre, area.Left + _sobre * ranura + ranura / 2);
        }

        private void PintarHorizontal(Graphics g)
        {
            Rectangle area = AreaGrafico();
            decimal maximo = Math.Max(0.0001m, _maximo ?? _datos.Max(d => d.Total));
            int alto = AltoRenglon(area);

            using (var fNegrita = new Font(Font.FontFamily, Font.Size, FontStyle.Bold))
            using (var pista = new SolidBrush(Color.FromArgb(30, 42, 80)))
            {
                for (int i = 0; i < _datos.Count; i++)
                {
                    var d = _datos[i];
                    int y = area.Top + i * alto;
                    if (i == _sobre)
                        using (var resaltado = new SolidBrush(ColorResaltado))
                            g.FillRectangle(resaltado, area.Left, y, area.Width, alto - 2);

                    string valor = _formato(d.Total);
                    int anchoValor = TextRenderer.MeasureText(valor, fNegrita).Width;
                    TextRenderer.DrawText(g, d.Etiqueta, Font, new Rectangle(area.Left + 4, y + 2, area.Width - anchoValor - 14, 16),
                        ForeColor, TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                    TextRenderer.DrawText(g, valor, fNegrita, new Rectangle(area.Right - anchoValor - 4, y + 2, anchoValor, 16),
                        Color.Goldenrod, TextFormatFlags.Right);

                    var barra = new RectangleF(area.Left + 4, y + 20, area.Width - 8, 6);
                    RellenarCapsula(g, pista, barra);
                    float largo = Math.Max(6, (float)(d.Total / maximo) * barra.Width);
                    using (var relleno = new LinearGradientBrush(new RectangleF(barra.X, barra.Y, Math.Max(1, barra.Width), barra.Height),
                        Color.FromArgb(184, 134, 11), _colores[0], LinearGradientMode.Horizontal))
                        RellenarCapsula(g, relleno, new RectangleF(barra.X, barra.Y, largo, barra.Height));
                }
            }
        }

        private void PintarLeyenda(Graphics g)
        {
            int x = Width - 8;
            for (int s = _series.Length - 1; s >= 0; s--)
            {
                int ancho = TextRenderer.MeasureText(_series[s], Font).Width;
                x -= ancho;
                TextRenderer.DrawText(g, _series[s], Font, new Point(x, 4), ColorEje, TextFormatFlags.NoPrefix);
                x -= 16;
                using (var pincel = new SolidBrush(_colores[s % _colores.Length]))
                    g.FillEllipse(pincel, x, 8, 9, 9);
                x -= 14;
            }
        }

        private void PintarGlobo(Graphics g, Rectangle area, int indice, float centroX)
        {
            var d = _datos[indice];
            var lineas = new List<(string Texto, Color Color)> { (d.Titulo ?? d.Etiqueta, Color.Goldenrod) };
            for (int s = 0; s < d.Valores.Length; s++)
            {
                string nombre = s < _series.Length ? _series[s] + ": " : string.Empty;
                lineas.Add((nombre + _formato(d.Valores[s]), _colores[s % _colores.Length]));
            }
            if (d.Valores.Length > 1)
                lineas.Add((TextoTotal + ": " + _formato(d.Total), Color.WhiteSmoke));

            using (var fNegrita = new Font(Font.FontFamily, Font.Size, FontStyle.Bold))
            {
                int ancho = lineas.Max(l => TextRenderer.MeasureText(l.Texto, fNegrita).Width) + 20;
                int alto = lineas.Count * 18 + 10;
                float x = Math.Max(area.Left, Math.Min(centroX - ancho / 2f, Width - ancho - 2));
                var globo = new RectangleF(x, area.Top + 4, ancho, alto);
                using (var camino = Redondeado(globo, 6))
                using (var fondo = new SolidBrush(FondoGlobo))
                using (var borde = new Pen(Color.FromArgb(150, 115, 30)))
                {
                    g.FillPath(fondo, camino);
                    g.DrawPath(borde, camino);
                }
                for (int i = 0; i < lineas.Count; i++)
                    TextRenderer.DrawText(g, lineas[i].Texto, i == 0 ? fNegrita : Font,
                        new Point((int)globo.X + 10, (int)globo.Y + 6 + i * 18), lineas[i].Color, TextFormatFlags.NoPrefix);
            }
        }

        private static void RellenarPuntaRedondeada(Graphics g, Brush pincel, RectangleF r, float radio)
        {
            using (var camino = new GraphicsPath())
            {
                float d = radio * 2;
                camino.AddArc(r.X, r.Y, d, d, 180, 90);
                camino.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                camino.AddLine(r.Right, r.Y + radio, r.Right, r.Bottom);
                camino.AddLine(r.Right, r.Bottom, r.X, r.Bottom);
                camino.CloseFigure();
                g.FillPath(pincel, camino);
            }
        }

        private static void RellenarCapsula(Graphics g, Brush pincel, RectangleF r)
        {
            if (r.Width <= 0 || r.Height <= 0) return;
            using (var camino = new GraphicsPath())
            {
                float d = Math.Min(r.Height, r.Width);
                camino.AddArc(r.X, r.Y, d, d, 90, 180);
                camino.AddArc(r.Right - d, r.Y, d, d, 270, 180);
                camino.CloseFigure();
                g.FillPath(pincel, camino);
            }
        }

        private static GraphicsPath Redondeado(RectangleF r, float radio)
        {
            float d = radio * 2;
            var camino = new GraphicsPath();
            camino.AddArc(r.X, r.Y, d, d, 180, 90);
            camino.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            camino.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            camino.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            camino.CloseFigure();
            return camino;
        }

        #endregion

        #region Métodos públicos

        public void Cargar(List<BarraGrafico_68SA> datos, Color[] colores, string[] series, Func<decimal, string> formato, decimal? maximo = null)
        {
            _datos = datos ?? new List<BarraGrafico_68SA>();
            _colores = colores != null && colores.Length > 0 ? colores : new[] { Color.Goldenrod };
            _series = series ?? new string[0];
            _formato = formato ?? (v => v.ToString("N0"));
            _maximo = maximo;
            _sobre = -1;
            Invalidate();
        }

        #endregion
    }
}
