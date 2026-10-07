using BE_08YS;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Tarjeta de una habitación en Control de habitaciones. Se dibuja entera (sin controles hijos):
    /// número, tipo, estado, capacidad, tarifa y, si corresponde, quién la ocupa o quién llega hoy.
    /// Se adapta al escalado de Windows (100 %, 125 %, 150 %...).
    /// </summary>
    public partial class UC_HabitacionCard_68SA : UserControl
    {
        #region Campos

        public const int AnchoBase = 252;
        public const int AltoBase = 152;

        private static readonly Color FondoTarjeta = Color.FromArgb(12, 22, 56);
        private static readonly Color FondoSobre = Color.FromArgb(18, 32, 76);
        private static readonly Color BordeTarjeta = Color.FromArgb(35, 48, 85);
        private static readonly Color TextoSuave = Color.FromArgb(160, 165, 180);
        private static readonly Color TextoTenue = Color.FromArgb(120, 128, 148);

        private EstadoHabitacion _estado;
        private Reserva_68SA _reserva;     // estadía en curso u hoy (solo para mostrar)
        private bool _sobre;
        private readonly float _escala;
        private readonly ToolTip _toolTip = new ToolTip { InitialDelay = 400 };

        #endregion

        #region Propiedades

        public Habitacion_68SA Habitacion { get; private set; }

        #endregion

        #region Eventos

        public event EventHandler<Habitacion_68SA> HabitacionSeleccionada;

        #endregion

        #region Constructor

        public UC_HabitacionCard_68SA()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw, true);
            _escala = Math.Max(1f, DeviceDpi / 96f);
            Size = new Size(E(AnchoBase), E(AltoBase));
            Cursor = Cursors.Hand;
            Disposed += (s, e) => _toolTip.Dispose();
        }

        #endregion

        #region Métodos públicos

        /// <param name="reservaActual">Estadía en curso (ocupada) o reserva que ingresa hoy (reservada), si se conoce.</param>
        public void Cargar(Habitacion_68SA habitacion, bool forzarDisponible = false, Reserva_68SA reservaActual = null)
        {
            Habitacion = habitacion;
            _estado = forzarDisponible ? EstadoHabitacion.Disponible : habitacion.EstadoVisual;
            _reserva = forzarDisponible ? null : reservaActual;
            string ocupante = _reserva?.Titular == null ? "" :
                Environment.NewLine + $"{_reserva.Titular.Nombre} {_reserva.Titular.Apellido}".Trim() +
                $"  ({_reserva.FechaIngreso:dd/MM} → {_reserva.FechaEgreso:dd/MM})";
            _toolTip.SetToolTip(this, $"{habitacion.NroHabitacion} · {habitacion.Tipo?.Nombre}" +
                                      (string.IsNullOrWhiteSpace(habitacion.Tipo?.Descripcion) ? "" : Environment.NewLine + habitacion.Tipo.Descripcion) +
                                      ocupante);
            Invalidate();
        }

        #endregion

        #region Métodos auxiliares

        private int E(float valor) => (int)Math.Round(valor * _escala);
        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        private (IconChar Icono, string Principal, string Secundario, Color Color) Pie()
        {
            string titular = NombreCorto(_reserva?.Titular);
            switch (_estado)
            {
                case EstadoHabitacion.Ocupada when titular != null:
                    return (IconChar.User, titular, DetalleSalida(), Color.WhiteSmoke);
                case EstadoHabitacion.Ocupada:
                    return (IconChar.User, T("HabitacionCard_txtOcupada"), null, TextoSuave);
                case EstadoHabitacion.Reservada when titular != null:
                    return (IconChar.PersonWalkingLuggage, titular,
                        T("HabitacionCard_txtLlegaHoyCorto") + "  ·  " + string.Format(T("HabitacionCard_txtNochesEstadia"), _reserva.Noches),
                        Color.FromArgb(120, 175, 245));
                case EstadoHabitacion.Reservada:
                    return (IconChar.CalendarCheck, T("HabitacionCard_txtReservadaHoy"), null, TextoSuave);
                case EstadoHabitacion.EnLimpieza:
                    return (IconChar.Broom, T("HabitacionCard_txtEnLimpieza"), null, Color.FromArgb(230, 175, 46));
                case EstadoHabitacion.FueraDeServicio:
                    return (IconChar.ScrewdriverWrench, T("HabitacionCard_txtFueraServicio"), null, TextoTenue);
                default:
                    return (IconChar.CircleCheck, T("HabitacionCard_txtLista"), null, Color.FromArgb(76, 217, 100));
            }
        }

        /// <summary>"Sale hoy" o "Sale el vie 03/10 · 2 noche(s) más".</summary>
        private string DetalleSalida()
        {
            DateTime egreso = _reserva.FechaEgreso.Date;
            if (egreso <= DateTime.Today) return T("HabitacionCard_txtSaleHoy");
            string dia = egreso.ToString("ddd dd/MM", OpcionEnum_68SA.CulturaActual());
            int restantes = (egreso - DateTime.Today).Days;
            return string.Format(T("HabitacionCard_txtSale"), dia) + "  ·  " + string.Format(T("HabitacionCard_txtQuedan"), restantes);
        }

        /// <summary>Primer nombre y primer apellido: "María Fernanda Gómez Ruiz" queda "María Gómez".</summary>
        private static string NombreCorto(Huesped_68SA huesped)
        {
            if (huesped == null) return null;
            string Primero(string texto) => (texto ?? string.Empty).Trim().Split(' ')[0];
            string nombre = $"{Primero(huesped.Nombre)} {Primero(huesped.Apellido)}".Trim();
            return nombre.Length == 0 ? null : nombre;
        }

        private static void DibujarIcono(Graphics g, IconChar icono, Color color, int x, int y, int tamano)
        {
            var imagen = IconCache.Get(icono, IconFont.Auto, tamano, color);
            if (imagen != null) g.DrawImage(imagen, x, y, tamano, tamano);
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
            if (Habitacion != null) HabitacionSeleccionada?.Invoke(this, Habitacion);
        }

        protected override void OnMouseEnter(EventArgs e) { _sobre = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _sobre = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Parent?.BackColor ?? BackColor);
            if (Habitacion == null) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            var (textoEstado, fondoEstado, acento) = EstiloEstadoHabitacion_68SA.Get(_estado);
            var tarjeta = new Rectangle(0, 0, Width - 1, Height - 1);

            using (var camino = Redondeado(tarjeta, E(10)))
            {
                using (var fondo = new SolidBrush(_sobre ? FondoSobre : FondoTarjeta))
                    g.FillPath(fondo, camino);

                // Franja del color del estado a la izquierda (recortada a la forma de la tarjeta)
                var recorteAnterior = g.Clip;
                g.SetClip(camino);
                using (var franja = new SolidBrush(acento))
                    g.FillRectangle(franja, 0, 0, E(5), Height);
                g.Clip = recorteAnterior;

                using (var borde = new Pen(_sobre ? Color.Goldenrod : BordeTarjeta, _sobre ? 1.6f : 1f))
                    g.DrawPath(borde, camino);
            }

            int x = E(18), derecha = Width - E(14);

            // Estado (pastilla arriba a la derecha)
            using (var fuentePastilla = new Font("Segoe UI", 7.5f, FontStyle.Bold))
            {
                Size medida = TextRenderer.MeasureText(textoEstado, fuentePastilla);
                var pastilla = new Rectangle(derecha - medida.Width - E(22), E(14), medida.Width + E(22), E(22));
                using (var camino = Redondeado(pastilla, pastilla.Height / 2))
                using (var fondo = new SolidBrush(fondoEstado))
                    g.FillPath(fondo, camino);
                using (var punto = new SolidBrush(acento))
                    g.FillEllipse(punto, pastilla.X + E(8), pastilla.Y + (pastilla.Height - E(7)) / 2, E(7), E(7));
                TextRenderer.DrawText(g, textoEstado, fuentePastilla,
                    new Rectangle(pastilla.X + E(17), pastilla.Y, pastilla.Width - E(19), pastilla.Height), acento,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPrefix);
            }

            // Número y tipo
            using (var fuenteNumero = new Font("Segoe UI", 20f, FontStyle.Bold))
                TextRenderer.DrawText(g, Habitacion.NroHabitacion, fuenteNumero, new Point(x - E(3), E(8)), Color.WhiteSmoke, TextFormatFlags.NoPrefix);
            using (var fuenteTipo = new Font("Segoe UI", 9.5f))
                TextRenderer.DrawText(g, Habitacion.Tipo?.Nombre ?? "", fuenteTipo,
                    new Rectangle(x, E(48), Width - x - E(14), E(20)), TextoSuave, TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);

            // Capacidad y tarifa
            using (var fuenteDato = new Font("Segoe UI", 9f))
            {
                int y = E(74);
                DibujarIcono(g, IconChar.UserGroup, Color.Goldenrod, x, y, E(14));
                TextRenderer.DrawText(g, (Habitacion.Tipo?.Capacidad ?? 0).ToString(), fuenteDato,
                    new Point(x + E(20), y - E(2)), Color.WhiteSmoke, TextFormatFlags.NoPrefix);

                decimal valorNoche = Habitacion.Tipo?.TarifaNoche ?? 0m;
                string tarifa = $"${valorNoche:N0} {T("HabitacionCard_txtPorNoche")}";
                TextRenderer.DrawText(g, tarifa, fuenteDato, new Rectangle(x, y - E(2), derecha - x, E(18)),
                    Color.Goldenrod, TextFormatFlags.Right | TextFormatFlags.NoPrefix);
            }

            // Separador y pie con lo que pasa en la habitación
            using (var linea = new Pen(BordeTarjeta))
                g.DrawLine(linea, x, E(100), derecha, E(100));

            // Pie en dos renglones: quién está (o el estado) y, abajo, cuándo sale / llega.
            // Así el nombre tiene todo el ancho y nunca queda cortado junto con la fecha.
            var (icono, principal, secundario, color) = Pie();
            int anchoTexto = derecha - x - E(22);
            bool dosRenglones = !string.IsNullOrEmpty(secundario);
            int yPrincipal = dosRenglones ? E(106) : E(114);
            using (var fuentePie = new Font("Segoe UI", 9.5f, _reserva != null ? FontStyle.Bold : FontStyle.Regular))
            {
                DibujarIcono(g, icono, color, x, yPrincipal + E(3), E(14));
                TextRenderer.DrawText(g, principal, fuentePie, new Rectangle(x + E(22), yPrincipal, anchoTexto, E(20)),
                    color, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            }
            if (dosRenglones)
                using (var fuenteDetalle = new Font("Segoe UI", 8.5f))
                    TextRenderer.DrawText(g, secundario, fuenteDetalle, new Rectangle(x + E(22), E(126), anchoTexto, E(18)),
                        TextoSuave, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        #endregion
    }
}
