using BE_08YS;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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

    /// <summary>
    /// Calendario de reservas tipo "línea de tiempo por habitación" (como el resource timeline de FullCalendar):
    /// una fila por habitación, agrupadas por piso, y una columna por día.
    ///
    /// Cada día se divide por hora: la barra de una reserva empieza en la hora de check-in del día de ingreso
    /// y termina en la hora de check-out del día de egreso (o en la hora real, si ya se hizo el check-in / check-out).
    /// Así, cuando una reserva termina y otra empieza el mismo día, las dos barras comparten la celda sin
    /// superponerse: la que sale ocupa la mañana (hasta las 10:00) y la que entra, la tarde (desde las 14:00).
    ///
    /// El encabezado de días y la columna de habitaciones quedan fijos al desplazarse; el ancho de los días se
    /// adapta al espacio disponible y solo aparece la barra horizontal si ni con el ancho mínimo entran.
    /// </summary>
    public partial class UC_CalendarioReservas_68SA : UserControl
    {
        #region Medidas y colores

        private const int AnchoEtiqueta = 176;
        private const int AltoFilaMes = 22;
        private const int AltoFilaDia = 36;
        private const int AltoFilaOcupacion = 22;
        private const int AltoEncabezado = AltoFilaMes + AltoFilaDia + AltoFilaOcupacion;
        private const int AltoFilaHabitacion = 38;
        private const int AltoFilaPiso = 26;
        private const int AltoPie = 32;
        private const int AnchoMinimoDiaSemana = 96;
        private const int AnchoMinimoDiaMes = 36;

        private static readonly Color ColorFondo = Color.FromArgb(5, 10, 40);
        private static readonly Color ColorPanel = Color.FromArgb(10, 18, 50);
        private static readonly Color ColorFilaAlterna = Color.FromArgb(8, 15, 46);
        private static readonly Color ColorPiso = Color.FromArgb(16, 27, 68);
        private static readonly Color ColorLinea = Color.FromArgb(35, 48, 85);
        private static readonly Color ColorLineaSuave = Color.FromArgb(22, 32, 66);
        private static readonly Color ColorTexto = Color.WhiteSmoke;
        private static readonly Color ColorTextoSuave = Color.FromArgb(160, 165, 180);
        private static readonly Color ColorFinDeSemana = Color.FromArgb(12, 20, 55);
        private static readonly Color ColorHoy = Color.FromArgb(40, 218, 165, 32);
        private static readonly Color ColorAhora = Color.FromArgb(235, 90, 90);

        #endregion

        #region Estado

        private sealed class Fila
        {
            public bool EsPiso;
            public Piso_68SA Piso;
            public int CantidadHabitaciones;
            public Habitacion_68SA Habitacion;
            public int Y;       // posición dentro del contenido (sin desplazamiento)
            public int Alto;
        }

        private sealed class Barra
        {
            public Reserva_68SA Reserva;
            public RectangleF Rect;   // coordenadas del contenido (sin desplazamiento)
        }

        private List<Habitacion_68SA> _habitaciones = new List<Habitacion_68SA>();
        private List<Reserva_68SA> _reservas = new List<Reserva_68SA>();
        private readonly List<Fila> _filas = new List<Fila>();
        private readonly List<Barra> _barras = new List<Barra>();
        private DateTime _fechaInicio = DateTime.Today;
        private DateTime _fechaFin = DateTime.Today.AddDays(7);
        private ModoCalendarioReservas _modo = ModoCalendarioReservas.Semana;
        private TimeSpan _horaCheckIn = TimeSpan.FromHours(14);
        private TimeSpan _horaCheckOut = TimeSpan.FromHours(10);

        private float _anchoDia = AnchoMinimoDiaSemana;
        // Estado propio de las barras: ScrollBar.Visible devuelve false mientras la pestaña está oculta
        private bool _conScrollH;
        private bool _conScrollV;
        private int _altoContenido;
        private readonly HScrollBar _scrollH = new HScrollBar();
        private readonly VScrollBar _scrollV = new VScrollBar();
        private readonly ToolTip _tooltip = new ToolTip { InitialDelay = 250, ReshowDelay = 100, AutoPopDelay = 15000 };
        private readonly Timer _timerAhora = new Timer { Interval = 60000 };

        private Barra _barraResaltada;
        private (Habitacion_68SA Habitacion, DateTime Dia)? _celdaResaltada;
        private string _textoTooltip;

        #endregion

        /// <summary>Clic sobre una reserva.</summary>
        public event EventHandler<Reserva_68SA> ReservaClickeada;

        /// <summary>Doble clic sobre un día libre de una habitación (desde hoy en adelante).</summary>
        public event EventHandler<NuevaReservaEventArgs_68SA> NuevaReservaSolicitada;

        /// <summary>Si es false no se ofrece crear reservas con doble clic (sin permiso).</summary>
        public bool PermiteCrearReservas { get; set; } = true;

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        public UC_CalendarioReservas_68SA()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);

            _scrollH.Visible = false;
            _scrollV.Visible = false;
            _scrollH.TabStop = false; // si toman el foco, el cursor de la barra parpadea
            _scrollV.TabStop = false;
            _scrollH.Scroll += (s, e) => Invalidate();
            _scrollV.Scroll += (s, e) => Invalidate();
            _scrollH.ValueChanged += (s, e) => Invalidate();
            _scrollV.ValueChanged += (s, e) => Invalidate();
            Controls.Add(_scrollH);
            Controls.Add(_scrollV);

            // La línea de "ahora" avanza sola
            _timerAhora.Tick += (s, e) => Invalidate();
            _timerAhora.Start();
            Disposed += (s, e) => { _timerAhora.Dispose(); _tooltip.Dispose(); };
        }

        #region Carga y medidas

        public void Cargar(List<Habitacion_68SA> habitaciones, List<Reserva_68SA> reservas,
                           DateTime fechaInicio, DateTime fechaFin, ModoCalendarioReservas modo)
            => Cargar(habitaciones, reservas, fechaInicio, fechaFin, modo, TimeSpan.FromHours(14), TimeSpan.FromHours(10));

        public void Cargar(List<Habitacion_68SA> habitaciones, List<Reserva_68SA> reservas,
                           DateTime fechaInicio, DateTime fechaFin, ModoCalendarioReservas modo,
                           TimeSpan horaCheckIn, TimeSpan horaCheckOut)
        {
            _habitaciones = (habitaciones ?? new List<Habitacion_68SA>())
                .OrderBy(h => h.Piso?.Numero ?? 0)
                .ThenBy(h => h.NroHabitacion.Length).ThenBy(h => h.NroHabitacion, StringComparer.OrdinalIgnoreCase)
                .ToList();
            _reservas = (reservas ?? new List<Reserva_68SA>()).Where(r => r.Estado != EstadoReserva.Cancelada).ToList();
            _fechaInicio = fechaInicio.Date;
            _fechaFin = fechaFin.Date > _fechaInicio ? fechaFin.Date : _fechaInicio.AddDays(1);
            _modo = modo;
            _horaCheckIn = horaCheckIn;
            _horaCheckOut = horaCheckOut;

            ArmarFilas();
            _scrollH.Value = 0;
            RecalcularMedidas();

            // Al abrir una semana o mes que contiene hoy, se centra la vista en hoy
            if (_conScrollH && DateTime.Today >= _fechaInicio && DateTime.Today < _fechaFin)
            {
                int objetivo = (int)(((DateTime.Today - _fechaInicio).TotalDays) * _anchoDia) - AnchoAreaDias / 3;
                FijarValor(_scrollH, objetivo);
            }

            _barraResaltada = null;
            _celdaResaltada = null;
            Invalidate();
        }

        private void ArmarFilas()
        {
            _filas.Clear();
            int y = 0;
            foreach (var grupo in _habitaciones.GroupBy(h => h.Piso?.Numero ?? 0))
            {
                var piso = grupo.First().Piso ?? new Piso_68SA { Numero = grupo.Key };
                _filas.Add(new Fila { EsPiso = true, Piso = piso, CantidadHabitaciones = grupo.Count(), Y = y, Alto = AltoFilaPiso });
                y += AltoFilaPiso;
                foreach (var habitacion in grupo)
                {
                    _filas.Add(new Fila { Habitacion = habitacion, Y = y, Alto = AltoFilaHabitacion });
                    y += AltoFilaHabitacion;
                }
            }
            _altoContenido = y;
        }

        private int CantidadDias => Math.Max(1, (int)(_fechaFin - _fechaInicio).TotalDays);
        private int AnchoAreaDias => Math.Max(0, ClientSize.Width - AnchoEtiqueta - (_conScrollV ? _scrollV.Width : 0));
        private int AltoAreaFilas => Math.Max(0, ClientSize.Height - AltoEncabezado - AltoPie - (_conScrollH ? _scrollH.Height : 0));

        /// <summary>
        /// Ancho de cada día según el espacio disponible y visibilidad de las barras de desplazamiento.
        /// Se resuelve dos veces porque mostrar una barra achica el espacio de la otra dirección.
        /// </summary>
        private void RecalcularMedidas()
        {
            int minimo = _modo == ModoCalendarioReservas.Semana ? AnchoMinimoDiaSemana : AnchoMinimoDiaMes;

            for (int pasada = 0; pasada < 2; pasada++)
            {
                _anchoDia = Math.Max(minimo, AnchoAreaDias / (float)CantidadDias);
                int anchoContenido = (int)Math.Ceiling(_anchoDia * CantidadDias);
                _conScrollH = anchoContenido > AnchoAreaDias + 1;
                _conScrollV = _altoContenido > AltoAreaFilas;
            }
            _scrollH.Visible = _conScrollH;
            _scrollV.Visible = _conScrollV;

            int anchoTotal = (int)Math.Ceiling(_anchoDia * CantidadDias);
            ConfigurarScroll(_scrollH, _conScrollH, anchoTotal, AnchoAreaDias, (int)_anchoDia);
            ConfigurarScroll(_scrollV, _conScrollV, _altoContenido, AltoAreaFilas, AltoFilaHabitacion);

            _scrollV.SetBounds(ClientSize.Width - _scrollV.Width, AltoEncabezado, _scrollV.Width, AltoAreaFilas);
            _scrollH.SetBounds(AnchoEtiqueta, AltoEncabezado + AltoAreaFilas, AnchoAreaDias, _scrollH.Height);
        }

        private static void ConfigurarScroll(ScrollBar barra, bool activa, int contenido, int visible, int paso)
        {
            if (!activa || visible <= 0)
            {
                barra.Value = 0;
                return;
            }
            barra.Minimum = 0;
            barra.LargeChange = Math.Max(1, visible);
            barra.SmallChange = Math.Max(1, paso);
            barra.Maximum = Math.Max(0, contenido - 1);
            FijarValor(barra, barra.Value);
        }

        private static void FijarValor(ScrollBar barra, int valor)
        {
            int maximo = Math.Max(0, barra.Maximum - barra.LargeChange + 1);
            barra.Value = Math.Max(0, Math.Min(maximo, valor));
        }

        private int OffsetX => _conScrollH ? _scrollH.Value : 0;
        private int OffsetY => _conScrollV ? _scrollV.Value : 0;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecalcularMedidas();
            Invalidate();
        }

        #endregion

        #region Conversión tiempo ⇄ posición

        private float XDe(DateTime momento) => (float)((momento - _fechaInicio).TotalDays * _anchoDia);

        /// <summary>Inicio y fin de la barra: hora real si ya ocurrió, si no la hora configurada del hotel.</summary>
        private (DateTime Inicio, DateTime Fin) Intervalo(Reserva_68SA reserva)
        {
            DateTime inicio = reserva.CheckIn ?? reserva.FechaIngreso.Date + _horaCheckIn;
            DateTime fin = reserva.Estado == EstadoReserva.Finalizada && reserva.CheckOut.HasValue
                ? reserva.CheckOut.Value
                : reserva.FechaEgreso.Date + _horaCheckOut;
            if (fin < inicio.AddHours(2)) fin = inicio.AddHours(2);
            return (inicio, fin);
        }

        #endregion

        #region Pintado

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(ColorFondo);

            if (_habitaciones.Count == 0)
            {
                using (var brush = new SolidBrush(ColorTextoSuave))
                    g.DrawString(T("Reservas_calSinHabitaciones"), Font, brush, 20, 20);
                return;
            }

            var areaFilas = new Rectangle(AnchoEtiqueta, AltoEncabezado, AnchoAreaDias, AltoAreaFilas);
            var areaEncabezado = new Rectangle(AnchoEtiqueta, 0, AnchoAreaDias, AltoEncabezado);
            var areaEtiquetas = new Rectangle(0, AltoEncabezado, AnchoEtiqueta, AltoAreaFilas);

            using (var fuente = new Fuentes())
            {
                var estado = g.Save();
                g.SetClip(areaFilas);
                g.TranslateTransform(AnchoEtiqueta - OffsetX, AltoEncabezado - OffsetY);
                DibujarGrilla(g);
                DibujarBarras(g, fuente);
                DibujarAhora(g, _altoContenido);
                g.Restore(estado);

                estado = g.Save();
                g.SetClip(areaEncabezado);
                g.TranslateTransform(AnchoEtiqueta - OffsetX, 0);
                DibujarEncabezado(g, fuente);
                g.Restore(estado);

                estado = g.Save();
                g.SetClip(areaEtiquetas);
                g.TranslateTransform(0, AltoEncabezado - OffsetY);
                DibujarEtiquetas(g, fuente);
                g.Restore(estado);

                DibujarEsquina(g, fuente);
                DibujarPie(g, fuente);
            }
        }

        private sealed class Fuentes : IDisposable
        {
            public readonly Font Normal = new Font("Segoe UI", 8.5F);
            public readonly Font Chica = new Font("Segoe UI", 7.5F);
            public readonly Font Negrita = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            public readonly Font Habitacion = new Font("Segoe UI", 10F, FontStyle.Bold);
            public readonly Font Piso = new Font("Segoe UI", 8F, FontStyle.Bold);
            public readonly Font DiaNumero = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            public readonly Font Barra = new Font("Segoe UI", 8F, FontStyle.Bold);

            public void Dispose()
            {
                foreach (var f in new[] { Normal, Chica, Negrita, Habitacion, Piso, DiaNumero, Barra }) f.Dispose();
            }
        }

        private void DibujarGrilla(Graphics g)
        {
            int dias = CantidadDias;
            float ancho = _anchoDia * dias;

            using (var alterna = new SolidBrush(ColorFilaAlterna))
            using (var piso = new SolidBrush(ColorPiso))
            using (var finde = new SolidBrush(ColorFinDeSemana))
            using (var hoy = new SolidBrush(ColorHoy))
            using (var linea = new Pen(ColorLinea))
            using (var lineaSuave = new Pen(ColorLineaSuave))
            using (var rayado = new HatchBrush(HatchStyle.BackwardDiagonal, Color.FromArgb(70, 150, 155, 165), Color.Transparent))
            {
                // Fines de semana y hoy
                for (int i = 0; i < dias; i++)
                {
                    DateTime dia = _fechaInicio.AddDays(i);
                    float x = i * _anchoDia;
                    if (dia.DayOfWeek == DayOfWeek.Saturday || dia.DayOfWeek == DayOfWeek.Sunday)
                        g.FillRectangle(finde, x, 0, _anchoDia, _altoContenido);
                }

                int filaHabitacion = 0;
                foreach (var fila in _filas)
                {
                    if (fila.EsPiso)
                    {
                        g.FillRectangle(piso, 0, fila.Y, ancho, fila.Alto);
                        filaHabitacion = 0;
                    }
                    else
                    {
                        if (filaHabitacion++ % 2 == 1)
                            g.FillRectangle(alterna, 0, fila.Y, ancho, fila.Alto);

                        // Habitación fuera de servicio: rayada desde hoy en adelante
                        if (fila.Habitacion.Estado == EstadoHabitacion.FueraDeServicio)
                        {
                            float desde = Math.Max(0, XDe(DateTime.Today));
                            if (desde < ancho)
                                g.FillRectangle(rayado, desde, fila.Y + 1, ancho - desde, fila.Alto - 2);
                        }
                    }
                    g.DrawLine(lineaSuave, 0, fila.Y + fila.Alto - 1, ancho, fila.Y + fila.Alto - 1);
                }

                if (DateTime.Today >= _fechaInicio && DateTime.Today < _fechaFin)
                    g.FillRectangle(hoy, XDe(DateTime.Today), 0, _anchoDia, _altoContenido);

                for (int i = 0; i <= dias; i++)
                {
                    float x = i * _anchoDia;
                    g.DrawLine(linea, x, 0, x, _altoContenido);
                }
            }

            // Resaltado de la celda libre bajo el mouse (doble clic para reservar)
            if (_celdaResaltada.HasValue)
            {
                var fila = _filas.FirstOrDefault(f => !f.EsPiso && f.Habitacion.Id == _celdaResaltada.Value.Habitacion.Id);
                if (fila != null)
                {
                    float x = XDe(_celdaResaltada.Value.Dia);
                    using (var brush = new SolidBrush(Color.FromArgb(35, 218, 165, 32)))
                    using (var pen = new Pen(Color.FromArgb(150, 218, 165, 32)) { DashStyle = DashStyle.Dash })
                    {
                        var rect = new RectangleF(x + 2, fila.Y + 3, _anchoDia - 4, fila.Alto - 6);
                        g.FillRectangle(brush, rect);
                        g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                        if (_anchoDia >= 30)
                        {
                            using (var mas = new SolidBrush(Color.Goldenrod))
                            using (var centrado = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                            using (var fuente = new Font("Segoe UI", 11F, FontStyle.Bold))
                                g.DrawString("+", fuente, mas, rect, centrado);
                        }
                    }
                }
            }
        }

        private void DibujarBarras(Graphics g, Fuentes fuente)
        {
            _barras.Clear();
            float limite = _anchoDia * CantidadDias;

            using (var texto = new SolidBrush(Color.FromArgb(10, 15, 35)))
            using (var formato = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap
            })
            {
                foreach (var reserva in _reservas)
                {
                    var fila = _filas.FirstOrDefault(f => !f.EsPiso && f.Habitacion.Id == reserva.Habitacion?.Id);
                    if (fila == null) continue;

                    var (inicio, fin) = Intervalo(reserva);
                    float x1 = XDe(inicio);
                    float x2 = XDe(fin);
                    if (x2 <= 0 || x1 >= limite) continue;

                    var rect = new RectangleF(x1 + 1, fila.Y + 5, Math.Max(6, x2 - x1 - 2), fila.Alto - 10);
                    _barras.Add(new Barra { Reserva = reserva, Rect = rect });

                    Color color = ColorEstado(reserva.Estado);
                    bool resaltada = _barraResaltada != null && _barraResaltada.Reserva.Id == reserva.Id;
                    if (resaltada) color = Aclarar(color, 0.25f);

                    using (var path = RectanguloRedondeado(rect, 6))
                    using (var relleno = new SolidBrush(color))
                    using (var acento = new SolidBrush(Oscurecer(color, 0.35f)))
                    {
                        g.FillPath(relleno, path);

                        // Marca de inicio (entrada) a la izquierda: se ve dónde arranca aunque haya otra barra al lado
                        var estado = g.Save();
                        g.SetClip(path, CombineMode.Intersect);
                        g.FillRectangle(acento, rect.X, rect.Y, 4, rect.Height);
                        g.Restore(estado);

                        if (resaltada)
                            using (var borde = new Pen(Color.White, 1.5f))
                                g.DrawPath(borde, path);
                    }

                    // El texto arranca en la parte visible de la barra (si empezó antes del período)
                    float xTexto = Math.Max(rect.X, OffsetX) + 8;
                    var zonaTexto = new RectangleF(xTexto, rect.Y, rect.Right - xTexto - 4, rect.Height);
                    if (zonaTexto.Width > 14)
                        g.DrawString(TextoBarra(reserva, zonaTexto.Width), fuente.Barra, texto, zonaTexto, formato);
                }
            }
        }

        private string TextoBarra(Reserva_68SA r, float ancho)
        {
            string apellido = r.Titular?.Apellido ?? "";
            if (ancho < 90) return apellido;
            string pax = r.CantidadNinos > 0 ? $"{r.CantidadAdultos}+{r.CantidadNinos}" : r.CantidadAdultos.ToString();
            if (ancho < 170) return $"{apellido} · {pax}";
            return $"{apellido}, {r.Titular?.Nombre} · {pax} · #{r.Id}";
        }

        private void DibujarAhora(Graphics g, int alto)
        {
            DateTime ahora = DateTime.Now;
            if (ahora < _fechaInicio || ahora >= _fechaFin) return;
            float x = XDe(ahora);
            using (var pen = new Pen(ColorAhora, 2f))
                g.DrawLine(pen, x, 0, x, alto);
        }

        private void DibujarEncabezado(Graphics g, Fuentes fuente)
        {
            var cultura = OpcionEnum_68SA.CulturaActual();
            int dias = CantidadDias;
            float ancho = _anchoDia * dias;

            using (var fondo = new SolidBrush(ColorPanel))
            using (var linea = new Pen(ColorLinea))
            using (var textoSuave = new SolidBrush(ColorTextoSuave))
            using (var textoClaro = new SolidBrush(ColorTexto))
            using (var dorado = new SolidBrush(Color.Goldenrod))
            using (var oscuro = new SolidBrush(Color.FromArgb(10, 15, 35)))
            using (var centrado = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap })
            using (var izquierda = new StringFormat { LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
            {
                g.FillRectangle(fondo, 0, 0, Math.Max(ancho, AnchoAreaDias + OffsetX), AltoEncabezado);

                // Fila de meses (un bloque por mes; el nombre queda visible aunque el bloque empiece antes)
                int i = 0;
                while (i < dias)
                {
                    DateTime primero = _fechaInicio.AddDays(i);
                    int j = i;
                    while (j < dias && _fechaInicio.AddDays(j).Month == primero.Month) j++;
                    float x1 = i * _anchoDia, x2 = j * _anchoDia;
                    float xTexto = Math.Max(x1, OffsetX) + 8;
                    string mes = cultura.TextInfo.ToTitleCase(primero.ToString("MMMM yyyy", cultura));
                    if (x2 - xTexto - 4 > 20)
                        g.DrawString(mes, fuente.Negrita, dorado, new RectangleF(xTexto, 0, x2 - xTexto - 4, AltoFilaMes), izquierda);
                    g.DrawLine(linea, x2, 0, x2, AltoFilaMes);
                    i = j;
                }
                g.DrawLine(linea, 0, AltoFilaMes, ancho, AltoFilaMes);

                // Fila de días
                int yDia = AltoFilaMes;
                for (int d = 0; d < dias; d++)
                {
                    DateTime dia = _fechaInicio.AddDays(d);
                    float x = d * _anchoDia;
                    bool esHoy = dia == DateTime.Today;
                    string nombre = dia.ToString("ddd", cultura).TrimEnd('.');
                    if (_modo == ModoCalendarioReservas.Mes && _anchoDia < 50) nombre = nombre.Substring(0, 1).ToUpper();

                    var zonaNombre = new RectangleF(x, yDia + 1, _anchoDia, 14);
                    var zonaNumero = new RectangleF(x, yDia + 14, _anchoDia, AltoFilaDia - 15);

                    if (esHoy)
                    {
                        float lado = Math.Min(24, Math.Min(_anchoDia - 4, zonaNumero.Height));
                        g.FillEllipse(dorado, x + (_anchoDia - lado) / 2, zonaNumero.Y + (zonaNumero.Height - lado) / 2, lado, lado);
                    }
                    g.DrawString(nombre, fuente.Chica, esHoy ? dorado : textoSuave, zonaNombre, centrado);
                    g.DrawString(dia.Day.ToString(), fuente.DiaNumero, esHoy ? oscuro : textoClaro, zonaNumero, centrado);
                    g.DrawLine(linea, x, yDia, x, AltoEncabezado);
                }
                g.DrawLine(linea, 0, AltoFilaMes + AltoFilaDia, ancho, AltoFilaMes + AltoFilaDia);

                // Fila de ocupación: habitaciones ocupadas esa noche / habitaciones en servicio
                int enServicio = _habitaciones.Count(h => h.Estado != EstadoHabitacion.FueraDeServicio);
                int yOcup = AltoFilaMes + AltoFilaDia;
                for (int d = 0; d < dias; d++)
                {
                    DateTime noche = _fechaInicio.AddDays(d);
                    int ocupadas = _reservas.Count(r => r.FechaIngreso.Date <= noche && r.FechaEgreso.Date > noche);
                    float porcentaje = enServicio > 0 ? Math.Min(1f, ocupadas / (float)enServicio) : 0;
                    float x = d * _anchoDia;

                    using (var calor = new SolidBrush(Color.FromArgb((int)(25 + 120 * porcentaje), 218, 165, 32)))
                        g.FillRectangle(calor, x + 1, yOcup + 1, _anchoDia - 1, AltoFilaOcupacion - 2);

                    string texto = _anchoDia >= 60 ? $"{ocupadas}/{enServicio}" : ocupadas.ToString();
                    g.DrawString(texto, fuente.Chica, textoClaro, new RectangleF(x, yOcup, _anchoDia, AltoFilaOcupacion), centrado);
                    g.DrawLine(linea, x, yOcup, x, AltoEncabezado);
                }
                g.DrawLine(linea, 0, AltoEncabezado - 1, ancho, AltoEncabezado - 1);
            }

            DibujarAhora(g, AltoEncabezado);
        }

        private void DibujarEtiquetas(Graphics g, Fuentes fuente)
        {
            using (var fondo = new SolidBrush(ColorPanel))
            using (var piso = new SolidBrush(ColorPiso))
            using (var linea = new Pen(ColorLinea))
            using (var lineaSuave = new Pen(ColorLineaSuave))
            using (var textoSuave = new SolidBrush(ColorTextoSuave))
            using (var textoClaro = new SolidBrush(ColorTexto))
            using (var dorado = new SolidBrush(Color.Goldenrod))
            using (var recorte = new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
            {
                g.FillRectangle(fondo, 0, 0, AnchoEtiqueta, Math.Max(_altoContenido, AltoAreaFilas + OffsetY));

                foreach (var fila in _filas)
                {
                    if (fila.EsPiso)
                    {
                        // Fila propia para el piso: nunca comparte renglón con una habitación
                        g.FillRectangle(piso, 0, fila.Y, AnchoEtiqueta, fila.Alto);
                        string nombrePiso = string.IsNullOrWhiteSpace(fila.Piso.Nombre)
                            ? $"{T("ControlHabitaciones_piso").ToUpper()} {fila.Piso.Numero}"
                            : $"{T("ControlHabitaciones_piso").ToUpper()} {fila.Piso.Numero} · {fila.Piso.Nombre}";
                        g.DrawString(nombrePiso, fuente.Piso, dorado, new RectangleF(10, fila.Y, AnchoEtiqueta - 16, fila.Alto), recorte);
                    }
                    else
                    {
                        var habitacion = fila.Habitacion;
                        var estilo = EstiloEstadoHabitacion_68SA.Get(habitacion.EstadoVisual);

                        using (var punto = new SolidBrush(estilo.Acento))
                            g.FillEllipse(punto, 12, fila.Y + (fila.Alto - 9) / 2f, 9, 9);

                        g.DrawString(habitacion.NroHabitacion, fuente.Habitacion, textoClaro,
                            new RectangleF(28, fila.Y + 2, 60, 20), recorte);
                        g.DrawString(habitacion.Tipo?.Nombre ?? "", fuente.Chica, textoSuave,
                            new RectangleF(28, fila.Y + 19, AnchoEtiqueta - 34, 16), recorte);
                    }
                    g.DrawLine(lineaSuave, 0, fila.Y + fila.Alto - 1, AnchoEtiqueta, fila.Y + fila.Alto - 1);
                }
                g.DrawLine(linea, AnchoEtiqueta - 1, 0, AnchoEtiqueta - 1, Math.Max(_altoContenido, AltoAreaFilas + OffsetY));
            }
        }

        private void DibujarEsquina(Graphics g, Fuentes fuente)
        {
            using (var fondo = new SolidBrush(ColorPanel))
            using (var linea = new Pen(ColorLinea))
            using (var dorado = new SolidBrush(Color.Goldenrod))
            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
            {
                g.FillRectangle(fondo, 0, 0, AnchoEtiqueta, AltoEncabezado);
                g.DrawString(T("Calendario_habitaciones").ToUpper(), fuente.Piso, dorado, 12, 6);
                g.DrawString($"{_habitaciones.Count} {T("Calendario_habitacionesCantidad")}", fuente.Chica, suave, 12, 24);
                g.DrawString(T("Calendario_ocupacion"), fuente.Chica, suave,
                    new RectangleF(0, AltoFilaMes + AltoFilaDia, AnchoEtiqueta - 8, AltoFilaOcupacion), derecha);
                g.DrawLine(linea, AnchoEtiqueta - 1, 0, AnchoEtiqueta - 1, AltoEncabezado);
                g.DrawLine(linea, 0, AltoEncabezado - 1, AnchoEtiqueta, AltoEncabezado - 1);
            }
        }

        private void DibujarPie(Graphics g, Fuentes fuente)
        {
            int y = ClientSize.Height - AltoPie;
            using (var fondo = new SolidBrush(ColorPanel))
            using (var linea = new Pen(ColorLinea))
            using (var suave = new SolidBrush(ColorTextoSuave))
            using (var formato = new StringFormat { LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
            {
                g.FillRectangle(fondo, 0, y, ClientSize.Width, AltoPie);
                g.DrawLine(linea, 0, y, ClientSize.Width, y);

                float x = 12;
                foreach (var estado in new[] { EstadoReserva.Confirmada, EstadoReserva.EnCurso, EstadoReserva.Finalizada })
                {
                    using (var chip = new SolidBrush(ColorEstado(estado)))
                    using (var path = RectanguloRedondeado(new RectangleF(x, y + 10, 22, 12), 4))
                        g.FillPath(chip, path);
                    string texto = OpcionEnum_68SA.Texto(estado);
                    g.DrawString(texto, fuente.Normal, suave, new RectangleF(x + 28, y, 140, AltoPie), formato);
                    x += 28 + g.MeasureString(texto, fuente.Normal).Width + 18;
                }

                using (var ahora = new Pen(ColorAhora, 2f))
                    g.DrawLine(ahora, x, y + 9, x, y + 23);
                g.DrawString(T("Calendario_ahora"), fuente.Normal, suave, new RectangleF(x + 8, y, 90, AltoPie), formato);
                x += 8 + g.MeasureString(T("Calendario_ahora"), fuente.Normal).Width + 18;

                string ayuda = string.Format(T("Calendario_ayudaHorarios"),
                    DateTime.Today.Add(_horaCheckIn).ToString("HH:mm"), DateTime.Today.Add(_horaCheckOut).ToString("HH:mm"));
                if (PermiteCrearReservas) ayuda += "   ·   " + T("Calendario_ayudaDobleClic");
                g.DrawString(ayuda, fuente.Chica, suave, new RectangleF(x, y, Math.Max(0, ClientSize.Width - x - 10), AltoPie), formato);
            }
        }

        #endregion

        #region Mouse

        private Point AContenido(Point p) => new Point(p.X - AnchoEtiqueta + OffsetX, p.Y - AltoEncabezado + OffsetY);

        private bool EnAreaFilas(Point p) =>
            p.X >= AnchoEtiqueta && p.X < AnchoEtiqueta + AnchoAreaDias && p.Y >= AltoEncabezado && p.Y < AltoEncabezado + AltoAreaFilas;

        private Barra BarraEn(Point p)
        {
            if (!EnAreaFilas(p)) return null;
            var c = AContenido(p);
            // Última dibujada = la que queda arriba
            for (int i = _barras.Count - 1; i >= 0; i--)
                if (_barras[i].Rect.Contains(c)) return _barras[i];
            return null;
        }

        private (Habitacion_68SA Habitacion, DateTime Dia)? CeldaEn(Point p)
        {
            if (!EnAreaFilas(p)) return null;
            var c = AContenido(p);
            var fila = _filas.FirstOrDefault(f => !f.EsPiso && c.Y >= f.Y && c.Y < f.Y + f.Alto);
            if (fila == null) return null;
            int indiceDia = (int)Math.Floor(c.X / _anchoDia);
            if (indiceDia < 0 || indiceDia >= CantidadDias) return null;
            return (fila.Habitacion, _fechaInicio.AddDays(indiceDia));
        }

        /// <summary>Un día se puede reservar si es de hoy en adelante, la habitación está en servicio y esa noche está libre.</summary>
        private bool EsCeldaLibre(Habitacion_68SA habitacion, DateTime dia)
        {
            if (!PermiteCrearReservas || dia < DateTime.Today) return false;
            if (habitacion.Estado == EstadoHabitacion.FueraDeServicio) return false;
            return !_reservas.Any(r => r.Habitacion?.Id == habitacion.Id
                                       && (r.Estado == EstadoReserva.Confirmada || r.Estado == EstadoReserva.EnCurso)
                                       && r.FechaIngreso.Date <= dia && r.FechaEgreso.Date > dia);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var barra = BarraEn(e.Location);
            (Habitacion_68SA Habitacion, DateTime Dia)? celda = null;
            if (barra == null)
            {
                var candidata = CeldaEn(e.Location);
                if (candidata.HasValue && EsCeldaLibre(candidata.Value.Habitacion, candidata.Value.Dia)) celda = candidata;
            }

            bool cambioBarra = !ReferenceEquals(barra, _barraResaltada);
            bool cambioCelda = !Equals(celda, _celdaResaltada);
            if (!cambioBarra && !cambioCelda) return;

            _barraResaltada = barra;
            _celdaResaltada = celda;
            Cursor = barra != null || celda != null ? Cursors.Hand : Cursors.Default;

            string texto = barra != null ? TextoTooltip(barra.Reserva)
                         : celda.HasValue ? string.Format(T("Calendario_tooltipLibre"), celda.Value.Habitacion.NroHabitacion, celda.Value.Dia.ToString("dd/MM/yyyy"))
                         : null;
            if (texto != _textoTooltip)
            {
                _textoTooltip = texto;
                if (texto == null) _tooltip.Hide(this);
                else _tooltip.Show(texto, this, e.X + 16, e.Y + 18, _tooltip.AutoPopDelay);
            }
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_barraResaltada == null && _celdaResaltada == null) return;
            _barraResaltada = null;
            _celdaResaltada = null;
            _textoTooltip = null;
            _tooltip.Hide(this);
            Cursor = Cursors.Default;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Focus();
            if (e.Button != MouseButtons.Left) return;
            var barra = BarraEn(e.Location);
            if (barra != null)
            {
                _tooltip.Hide(this);
                ReservaClickeada?.Invoke(this, barra.Reserva);
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Button != MouseButtons.Left || BarraEn(e.Location) != null) return;
            var celda = CeldaEn(e.Location);
            if (celda.HasValue && EsCeldaLibre(celda.Value.Habitacion, celda.Value.Dia))
            {
                _tooltip.Hide(this);
                NuevaReservaSolicitada?.Invoke(this, new NuevaReservaEventArgs_68SA(celda.Value.Habitacion, celda.Value.Dia));
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            bool horizontal = (ModifierKeys & Keys.Shift) == Keys.Shift || !_conScrollV;
            var barra = horizontal ? (ScrollBar)_scrollH : _scrollV;
            if (horizontal ? !_conScrollH : !_conScrollV) return;
            int paso = horizontal ? (int)_anchoDia : AltoFilaHabitacion * 2;
            FijarValor(barra, barra.Value - Math.Sign(e.Delta) * paso);
        }

        private string TextoTooltip(Reserva_68SA r)
        {
            var cultura = OpcionEnum_68SA.CulturaActual();
            var (inicio, fin) = Intervalo(r);
            string huespedes = string.Format(T("Calendario_tooltipHuespedes"), r.CantidadAdultos, r.CantidadNinos);
            string lineas =
                $"{T("Calendario_tooltipReserva")} #{r.Id:000000} · {OpcionEnum_68SA.Texto(r.Estado)}\n" +
                $"{r.Titular?.Apellido}, {r.Titular?.Nombre}  ({r.Titular?.Documento})\n" +
                $"{T("Calendario_tooltipHabitacion")} {r.Habitacion?.NroHabitacion} · {r.Habitacion?.Tipo?.Nombre}\n" +
                $"{T("Calendario_tooltipIngreso")}: {inicio.ToString("ddd dd/MM HH:mm", cultura)}" +
                (r.CheckIn.HasValue ? $" ({T("Calendario_tooltipReal")})" : "") + "\n" +
                $"{T("Calendario_tooltipEgreso")}: {fin.ToString("ddd dd/MM HH:mm", cultura)}" +
                (r.Estado == EstadoReserva.Finalizada && r.CheckOut.HasValue ? $" ({T("Calendario_tooltipReal")})" : "") + "\n" +
                $"{r.Noches} {T(r.Noches == 1 ? "RegistrarReserva_txtNoche" : "RegistrarReserva_txtNoches")} · {huespedes}\n" +
                $"{T("Calendario_tooltipTotal")}: ${r.MontoTotal.ToString("N2", cultura)}\n" +
                T("Calendario_tooltipClic");
            return lineas;
        }

        #endregion

        #region Utilidades de dibujo

        private static Color ColorEstado(EstadoReserva estado)
        {
            switch (estado)
            {
                case EstadoReserva.Confirmada: return Color.FromArgb(90, 155, 235);
                case EstadoReserva.EnCurso: return Color.FromArgb(76, 217, 100);
                case EstadoReserva.Finalizada: return Color.FromArgb(135, 142, 162);
                default: return Color.FromArgb(160, 165, 180);
            }
        }

        private static Color Aclarar(Color c, float f) =>
            Color.FromArgb(c.A, (int)(c.R + (255 - c.R) * f), (int)(c.G + (255 - c.G) * f), (int)(c.B + (255 - c.B) * f));

        private static Color Oscurecer(Color c, float f) =>
            Color.FromArgb(c.A, (int)(c.R * (1 - f)), (int)(c.G * (1 - f)), (int)(c.B * (1 - f)));

        private static GraphicsPath RectanguloRedondeado(RectangleF rect, float radio)
        {
            var path = new GraphicsPath();
            float d = Math.Min(radio * 2, Math.Min(rect.Width, rect.Height));
            if (d <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion
    }

    public class NuevaReservaEventArgs_68SA : EventArgs
    {
        public Habitacion_68SA Habitacion { get; }
        public DateTime Fecha { get; }

        public NuevaReservaEventArgs_68SA(Habitacion_68SA habitacion, DateTime fecha)
        {
            Habitacion = habitacion;
            Fecha = fecha;
        }
    }
}
