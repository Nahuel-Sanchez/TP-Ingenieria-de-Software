using BE_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public enum ModoCalendarioReservas
    {
        Semana,
        Mes
    }

    public partial class UC_CalendarioReservas_68SA : UserControl
    {
        private const int AnchoColumnaEtiqueta = 180;
        private const int AltoEncabezado = 40;
        private const int AltoFila = 44;

        private List<Habitacion_68SA> _habitaciones = new List<Habitacion_68SA>();
        private List<Reserva_68SA> _reservas = new List<Reserva_68SA>();
        private DateTime _fechaInicio = DateTime.Today;
        private DateTime _fechaFin = DateTime.Today.AddDays(7);
        private ModoCalendarioReservas _modo = ModoCalendarioReservas.Semana;

        private readonly List<(Rectangle Rect, Reserva_68SA Reserva)> _barrasVisibles = new List<(Rectangle, Reserva_68SA)>();

        public event EventHandler<Reserva_68SA> ReservaClickeada;

        public UC_CalendarioReservas_68SA()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            AutoScroll = true;
        }

        public void Cargar(List<Habitacion_68SA> habitaciones, List<Reserva_68SA> reservas,
                            DateTime fechaInicio, DateTime fechaFin, ModoCalendarioReservas modo)
        {
            _habitaciones = habitaciones.OrderBy(h => h.Piso.Numero).ThenBy(h => h.NroHabitacion).ToList();
            _reservas = reservas;
            _fechaInicio = fechaInicio.Date;
            _fechaFin = fechaFin.Date;
            _modo = modo;

            int cantidadDias = Math.Max(1, (int)(_fechaFin - _fechaInicio).TotalDays);
            int anchoColumnaDia = _modo == ModoCalendarioReservas.Semana ? 120 : 42;

            AutoScrollMinSize = new Size(
                AnchoColumnaEtiqueta + cantidadDias * anchoColumnaDia,
                AltoEncabezado + _habitaciones.Count * AltoFila);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            _barrasVisibles.Clear();

            if (_habitaciones.Count == 0)
            {
                g.ResetTransform();
                using (var brush = new SolidBrush(Color.FromArgb(160, 165, 180)))
                    g.DrawString("No hay habitaciones para mostrar.", Font, brush, 20, 20);
                return;
            }

            int cantidadDias = Math.Max(1, (int)(_fechaFin - _fechaInicio).TotalDays);
            int anchoColumnaDia = _modo == ModoCalendarioReservas.Semana ? 120 : 42;
            int altoTotal = AltoEncabezado + _habitaciones.Count * AltoFila;
            int anchoTotal = AnchoColumnaEtiqueta + cantidadDias * anchoColumnaDia;

            using (var brushFondo = new SolidBrush(Color.FromArgb(5, 10, 40)))
                g.FillRectangle(brushFondo, 0, 0, anchoTotal, altoTotal);

            using (var brushEncabezado = new SolidBrush(Color.FromArgb(10, 18, 50)))
                g.FillRectangle(brushEncabezado, 0, 0, anchoTotal, AltoEncabezado);

            using (var penLinea = new Pen(Color.FromArgb(35, 48, 85)))
            using (var fontDia = new Font("Segoe UI", 8F))
            using (var fontDiaBold = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (var brushTextoDia = new SolidBrush(Color.FromArgb(200, 200, 210)))
            using (var brushHoy = new SolidBrush(Color.FromArgb(30, 218, 165, 32)))
            {
                var culturaEs = CultureInfo.GetCultureInfo("es-ES");

                for (int i = 0; i < cantidadDias; i++)
                {
                    DateTime dia = _fechaInicio.AddDays(i);
                    int x = AnchoColumnaEtiqueta + i * anchoColumnaDia;

                    if (dia.Date == DateTime.Today)
                        g.FillRectangle(brushHoy, x, 0, anchoColumnaDia, altoTotal);

                    string texto = _modo == ModoCalendarioReservas.Semana
                        ? dia.ToString("ddd dd", culturaEs)
                        : dia.Day.ToString();

                    var tamano = g.MeasureString(texto, fontDia);
                    g.DrawString(texto, dia.Date == DateTime.Today ? fontDiaBold : fontDia, brushTextoDia,
                        x + (anchoColumnaDia - tamano.Width) / 2, (AltoEncabezado - tamano.Height) / 2);

                    g.DrawLine(penLinea, x, 0, x, altoTotal);
                }
                g.DrawLine(penLinea, AnchoColumnaEtiqueta, 0, AnchoColumnaEtiqueta, altoTotal);
                g.DrawLine(penLinea, 0, AltoEncabezado, anchoTotal, AltoEncabezado);

                using (var fontEtiqueta = new Font("Segoe UI", 9F, FontStyle.Bold))
                using (var brushEtiqueta = new SolidBrush(Color.WhiteSmoke))
                using (var brushEtiquetaPiso = new SolidBrush(Color.FromArgb(160, 165, 180)))
                using (var brushAlt = new SolidBrush(Color.FromArgb(13, 22, 58)))
                {
                    int? pisoAnterior = null;

                    for (int fila = 0; fila < _habitaciones.Count; fila++)
                    {
                        var habitacion = _habitaciones[fila];
                        int y = AltoEncabezado + fila * AltoFila;

                        if (fila % 2 == 1)
                            g.FillRectangle(brushAlt, 0, y, anchoTotal, AltoFila);

                        if (habitacion.Piso.Numero != pisoAnterior)
                        {
                            g.DrawString($"PISO {habitacion.Piso.Numero}", fontDiaBold, brushEtiquetaPiso, 8, y + 4);
                            pisoAnterior = habitacion.Piso.Numero;
                        }
                        else
                        {
                            g.DrawString(habitacion.NroHabitacion, fontEtiqueta, brushEtiqueta, 16, y + (AltoFila - fontEtiqueta.Height) / 2);
                        }

                        g.DrawLine(penLinea, 0, y + AltoFila, anchoTotal, y + AltoFila);
                    }
                }
            }

            using (var fontBarra = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (var brushTextoBarra = new SolidBrush(Color.FromArgb(10, 15, 35)))
            {
                foreach (var reserva in _reservas)
                {
                    int fila = _habitaciones.FindIndex(h => h.Id == reserva.Habitacion.Id);
                    if (fila < 0) continue;

                    DateTime inicioVisible = reserva.FechaIngreso < _fechaInicio ? _fechaInicio : reserva.FechaIngreso;
                    DateTime finVisible = reserva.FechaEgreso > _fechaFin ? _fechaFin : reserva.FechaEgreso;
                    if (finVisible <= inicioVisible) continue;

                    int colInicio = (int)(inicioVisible - _fechaInicio).TotalDays;
                    int colFin = (int)(finVisible - _fechaInicio).TotalDays;

                    int x = AnchoColumnaEtiqueta + colInicio * anchoColumnaDia + 2;
                    int y = AltoEncabezado + fila * AltoFila + 4;
                    int ancho = Math.Max(anchoColumnaDia - 4, (colFin - colInicio) * anchoColumnaDia - 4);
                    int alto = AltoFila - 8;

                    var rect = new Rectangle(x, y, ancho, alto);
                    Color color = ColorPorEstado(reserva.Estado);

                    using (var brush = new SolidBrush(color))
                    using (var path = RectanguloRedondeado(rect, 6))
                        g.FillPath(brush, path);

                    string texto = $"{reserva.Titular.Apellido} — Hab. {reserva.Habitacion.NroHabitacion}";
                    var recorte = g.ClipBounds;
                    g.SetClip(rect);
                    g.DrawString(texto, fontBarra, brushTextoBarra, rect.X + 6, rect.Y + (alto - fontBarra.Height) / 2);
                    g.SetClip(recorte);

                    _barrasVisibles.Add((rect, reserva));
                }
            }

            g.ResetTransform();
        }

        private static Color ColorPorEstado(EstadoReserva estado)
        {
            switch (estado)
            {
                case EstadoReserva.Confirmada: return Color.FromArgb(90, 155, 235);
                case EstadoReserva.EnCurso: return Color.FromArgb(76, 217, 100);
                case EstadoReserva.Finalizada: return Color.FromArgb(160, 165, 180);
                default: return Color.FromArgb(160, 165, 180);
            }
        }

        private static GraphicsPath RectanguloRedondeado(Rectangle rect, int radio)
        {
            var path = new GraphicsPath();
            int d = radio * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            var punto = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
            foreach (var (rect, reserva) in _barrasVisibles)
            {
                if (rect.Contains(punto))
                {
                    ReservaClickeada?.Invoke(this, reserva);
                    return;
                }
            }
        }
    }
}