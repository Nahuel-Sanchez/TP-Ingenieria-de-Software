using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using FontAwesome.Sharp;
using Service_08YS;
using System;
using System.Linq;
using System.Windows.Forms;
using Service_08YS.Entities.Acceso;
using System.Collections.Generic;
using GUI_08YS.UserControls;

namespace GUI_08YS.Recepcionista
{
    public partial class FormCheckOut_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private static readonly Dictionary<string, Permisos> _mapaPermisos =
            new Dictionary<string, Permisos>
            {
                { nameof(btnConfirmar), Permisos.RegistrarCheckOut },
            };

        private readonly Action<Form> _openChildForm;
        private readonly ModoHabitaciones _modoOrigen;
        private readonly DateTime? _fechaIngresoOrigen;
        private readonly DateTime? _fechaEgresoOrigen;

        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private readonly PagoBLL_68SA _pagoBLL = BLLFactory_08YS.CreatePagoBLL();
        private readonly CuentaEstadiaBLL_68SA _cuentaBLL = BLLFactory_08YS.CreateCuentaEstadiaBLL();

        private Reserva_68SA _reserva;
        private decimal _saldoPendiente;

        #endregion

        #region Constructor

        // Caso 1: sin reserva conocida
        public FormCheckOut_68SA(Action<Form> openChildForm, ModoHabitaciones modoOrigen,
                                  DateTime? fechaIngresoOrigen = null, DateTime? fechaEgresoOrigen = null)
            : this(openChildForm, null, modoOrigen, fechaIngresoOrigen, fechaEgresoOrigen)
        {
        }

        // Caso 2: ya se conoce la reserva (viene de una habitación puntual)
        public FormCheckOut_68SA(Action<Form> openChildForm, Reserva_68SA reserva, ModoHabitaciones modoOrigen,
                                  DateTime? fechaIngresoOrigen = null, DateTime? fechaEgresoOrigen = null)
        {
            _openChildForm = openChildForm;
            _modoOrigen = modoOrigen;
            _fechaIngresoOrigen = fechaIngresoOrigen;
            _fechaEgresoOrigen = fechaEgresoOrigen;

            InitializeComponent();
            // El detalle de la cuenta se abre solo si se pide
            lnkDetalleCuenta.LinkClicked += (s, e) =>
            {
                if (_reserva == null) return;
                using (var detalle = new FormDetalleEstadia_68SA(_reserva.Id))
                    detalle.ShowDialog(this);
            };

            PermissionFilter_08YS.Aplicar(this, _mapaPermisos);

            // Las tarjetas (buscar, reserva, cuenta/acompañantes) ocupan todo el ancho disponible
            LayoutAdaptable_68SA.EstirarHijosAlAncho(flpContenido);

            OpcionEnum_68SA.Cargar(cmbMetodoPagoFinal, typeof(MetodoPago));
            cmbMetodoPagoFinal.SelectedIndex = 0;
            cmbMetodoPagoFinal.SelectedIndexChanged += (s, e) => ActualizarVisibilidadVueltoFinal();
            nudMontoRecibidoFinal.ValueChanged += (s, e) => ActualizarVueltoFinal();

            btnConfirmar.Click += BtnConfirmar_Click;
            btnCancelar.Click += (s, e) => Volver();
            btnBuscarReserva.Click += BtnBuscarReserva_Click;

            if (reserva != null)
                CargarReserva(reserva);
            else
                flpContenido.Controls.Add(pnlAccionBuscar);
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            // Los paneles de reserva/cuenta se agregan a flpContenido recién al buscar: se traducen siempre
            TraducirControles(this);
            TraducirControles(pnlAccionBuscar);
            TraducirControles(pnlReserva);
            TraducirControles(pnlCuenta);
            OpcionEnum_68SA.Cargar(cmbMetodoPagoFinal, typeof(MetodoPago));
            if (_reserva != null)
                lblResComposicion.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("Comun_txtAdultosNinos"), _reserva.CantidadAdultos, _reserva.CantidadNinos);
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag != null && c.Tag is string tag && !string.IsNullOrWhiteSpace(tag))
                    c.Text = TraductorManager_08YS.Instance.GetTexto(tag);

                if (c.HasChildren)
                    TraducirControles(c);
            }
        }

        #endregion

        #region Carga de datos

        private void CargarReserva(Reserva_68SA reserva)
        {
            _reserva = reserva;

            lblResHabitacion.Text = reserva.Habitacion.NroHabitacion;
            lblResTitular.Text = $"{reserva.Titular.Nombre} {reserva.Titular.Apellido} ({TraductorManager_08YS.Instance.GetTexto("Comun_abrevDni")} {reserva.Titular.Documento})";
            lblResFechas.Text = $"{reserva.FechaIngreso:dd/MM/yyyy} → {reserva.FechaEgreso:dd/MM/yyyy}";
            lblResComposicion.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("Comun_txtAdultosNinos"), reserva.CantidadAdultos, reserva.CantidadNinos);

            var habitacion = BLLFactory_08YS.CreateHabitacionBLL().GetById(reserva.Habitacion.Id);
            lblResTipo.Text = habitacion.Tipo.Nombre;

            if (!flpContenido.Controls.Contains(pnlReserva))
                flpContenido.Controls.Add(pnlReserva);

            // PN2: el saldo incluye los cargos adicionales (upgrades y servicio a la habitación)
            var cuenta = _cuentaBLL.GetEstadoCuenta(reserva.Id);
            _saldoPendiente = cuenta.Saldo;

            lblMontoTotal.Text = $"${cuenta.AlojamientoReservado:N2}";
            lblRenovacion.Text = $"${cuenta.Renovacion:N2}";
            lblRenovacion.ForeColor = cuenta.Renovacion > 0 ? System.Drawing.Color.Goldenrod : System.Drawing.Color.WhiteSmoke;
            lblCargos.Text = $"${cuenta.TotalCargos:N2}";
            MostrarAvisoCargos(cuenta);
            lblTotalEstadia.Text = $"${cuenta.Total:N2}";
            lblTotalPagado.Text = $"${cuenta.TotalPagado:N2}";
            lblSaldo.Text = $"${_saldoPendiente:N2}";
            lblSaldo.ForeColor = _saldoPendiente > 0
                ? System.Drawing.Color.FromArgb(235, 90, 90)
                : System.Drawing.Color.FromArgb(76, 217, 100);

            bool haySaldo = _saldoPendiente > 0;

            // Con saldo pendiente el check-out incluye el cobro (CUN-03): hacen falta ambos permisos
            btnConfirmar.Visible = SessionManager_08YS.Instance.HasPermission(Permisos.RegistrarCheckOut)
                                && (!haySaldo || SessionManager_08YS.Instance.HasPermission(Permisos.Cobrar));

            lblEtiquetaMetodoPagoFinal.Visible = haySaldo;
            cmbMetodoPagoFinal.Visible = haySaldo;
            lblEtiquetaMontoRecibidoFinal.Visible = haySaldo;
            nudMontoRecibidoFinal.Visible = haySaldo;
            lblEtiquetaVueltoFinal.Visible = haySaldo;
            lblVueltoFinal.Visible = haySaldo;

            if (haySaldo)
            {
                nudMontoRecibidoFinal.Value = _saldoPendiente;
                ActualizarVueltoFinal();
                ActualizarVisibilidadVueltoFinal();
                pnlCuenta.Height = 414;
            }
            else
            {
                lblEtiquetaVueltoFinal.Visible = false;
                lblVueltoFinal.Visible = false;
                pnlCuenta.Height = 234;
            }

            if (!flpContenido.Controls.Contains(pnlCuenta))
                flpContenido.Controls.Add(pnlCuenta);
        }

        private void ActualizarVueltoFinal()
        {
            decimal vuelto = nudMontoRecibidoFinal.Value - _saldoPendiente;
            lblVueltoFinal.Text = $"${Math.Max(0, vuelto):N2}";
        }

        private void ActualizarVisibilidadVueltoFinal()
        {
            bool esEfectivo = OpcionEnum_68SA.Seleccionado<MetodoPago>(cmbMetodoPagoFinal) == MetodoPago.Efectivo;
            lblEtiquetaVueltoFinal.Visible = esEfectivo;
            lblVueltoFinal.Visible = esEfectivo;
        }

        #endregion

        #region Eventos de controles

        private void BtnBuscarReserva_Click(object sender, EventArgs e)
        {
            using (var popup = new FormBuscarReserva_68SA(EstadoReserva.EnCurso, TraductorManager_08YS.Instance.GetTexto("CheckOut_tituloBuscarReserva"), IconChar.DoorClosed))
            {
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    flpContenido.Controls.Remove(pnlAccionBuscar);
                    CargarReserva(popup.ReservaSeleccionada);
                }
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (_reserva == null)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("CheckOut_msgFaltaReserva"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloFaltaReserva"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_saldoPendiente > 0 && nudMontoRecibidoFinal.Value < _saldoPendiente)
            {
                MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("Comun_msgPagoInsuficiente"), nudMontoRecibidoFinal.Value, _saldoPendiente),
                    TraductorManager_08YS.Instance.GetTexto("Comun_tituloPagoInsuficiente"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_saldoPendiente > 0)
                {
                    _pagoBLL.Registrar(new Pago_68SA
                    {
                        ReservaId = _reserva.Id,
                        Monto = _saldoPendiente, // el vuelto entregado no es un ingreso
                        MetodoPago = OpcionEnum_68SA.Seleccionado<MetodoPago>(cmbMetodoPagoFinal) ?? MetodoPago.Efectivo
                    });
                }

                _reservaBLL.RegistrarCheckOut(_reserva.Id);
                MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("CheckOut_msgCheckOutRegistrado"), _reserva.Habitacion.NroHabitacion),
                    TraductorManager_08YS.Instance.GetTexto("CheckOut_tituloCheckOutConfirmado"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Volver();
            }
            catch (EstadoReservaInvalidoException_68SA)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Comun_excEstadoReservaInvalido"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloNoSePudoConfirmar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Métodos auxiliares

        /// <summary>
        /// Al lado del total de cargos solo se avisa lo importante (pedidos de room service sin entregar);
        /// el detalle completo de la cuenta se ve con "Ver detalle de la cuenta".
        /// </summary>
        private void MostrarAvisoCargos(EstadoCuentaReserva_68SA cuenta)
        {
            int pendientes = cuenta.Cargos.Count == 0 ? 0
                : BLLFactory_08YS.CreateRoomServiceBLL().GetPorReserva(cuenta.ReservaId)
                    .Count(p => p.Estado == EstadoPedidoRoomService.Pendiente);

            lblDetalleCargos.Text = pendientes > 0
                ? string.Format(TraductorManager_08YS.Instance.GetTexto("CheckOut_txtPedidosPendientes"), pendientes)
                : string.Empty;
            lblDetalleCargos.ForeColor = System.Drawing.Color.FromArgb(240, 180, 60);
        }

        private void Volver()
        {
            _openChildForm(new FormControlHabitaciones_68SA(_openChildForm, _modoOrigen, _fechaIngresoOrigen, _fechaEgresoOrigen));
        }

        #endregion

        #region Sobrescrituras

        // Pinta el formulario completo en memoria y lo vuelca de una vez: sin parpadeo ni franjas
        // blancas mientras los controles se acomodan al abrir o redimensionar la ventana.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion
    }
}