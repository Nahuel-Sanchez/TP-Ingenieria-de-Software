using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using FontAwesome.Sharp;
using Service_08YS;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormCheckOut_68SA : Form, IIdiomaObserver_08YS
    {
        private readonly Action<Form> _openChildForm;
        private readonly ModoHabitaciones _modoOrigen;
        private readonly DateTime? _fechaIngresoOrigen;
        private readonly DateTime? _fechaEgresoOrigen;

        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private readonly PagoBLL_68SA _pagoBLL = BLLFactory_08YS.CreatePagoBLL();

        private Reserva_68SA _reserva;
        private decimal _saldoPendiente;

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

            cmbMetodoPagoFinal.Items.AddRange(Enum.GetValues(typeof(MetodoPago)).Cast<object>().ToArray());
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

        private void BtnBuscarReserva_Click(object sender, EventArgs e)
        {
            using (var popup = new FormBuscarReserva_68SA(EstadoReserva.EnCurso, "Buscar reserva para check-out", IconChar.DoorClosed))
            {
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    flpContenido.Controls.Remove(pnlAccionBuscar);
                    CargarReserva(popup.ReservaSeleccionada);
                }
            }
        }

        private void CargarReserva(Reserva_68SA reserva)
        {
            _reserva = reserva;

            lblResHabitacion.Text = reserva.Habitacion.NroHabitacion;
            lblResTitular.Text = $"{reserva.Titular.Nombre} {reserva.Titular.Apellido} (DNI {reserva.Titular.Documento})";
            lblResFechas.Text = $"{reserva.FechaIngreso:dd/MM/yyyy} → {reserva.FechaEgreso:dd/MM/yyyy}";
            lblResComposicion.Text = $"{reserva.CantidadAdultos} adulto(s), {reserva.CantidadNinos} niño(s)";

            var habitacion = BLLFactory_08YS.CreateHabitacionBLL().GetById(reserva.Habitacion.Id);
            lblResTipo.Text = habitacion.Tipo.Nombre;

            if (!flpContenido.Controls.Contains(pnlReserva))
                flpContenido.Controls.Add(pnlReserva);

            decimal totalPagado = _pagoBLL.GetTotalPagado(reserva.Id);
            _saldoPendiente = Math.Max(0, reserva.MontoTotal - totalPagado);

            lblMontoTotal.Text = $"${reserva.MontoTotal:N2}";
            lblTotalPagado.Text = $"${totalPagado:N2}";
            lblSaldo.Text = $"${_saldoPendiente:N2}";
            lblSaldo.ForeColor = _saldoPendiente > 0
                ? System.Drawing.Color.FromArgb(235, 90, 90)
                : System.Drawing.Color.FromArgb(76, 217, 100);

            bool haySaldo = _saldoPendiente > 0;
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
                pnlCuenta.Height = 330;
            }
            else
            {
                lblEtiquetaVueltoFinal.Visible = false;
                lblVueltoFinal.Visible = false;
                pnlCuenta.Height = 150;
            }

            if (!flpContenido.Controls.Contains(pnlCuenta))
                flpContenido.Controls.Add(pnlCuenta);
        }

        private void ActualizarVueltoFinal()
        {
            decimal vuelto = nudMontoRecibidoFinal.Value - _saldoPendiente;
            lblVueltoFinal.Text = $"${Math.Max(0, vuelto):N2}";
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (_reserva == null)
            {
                MessageBox.Show("Buscá y encontrá una reserva antes de confirmar el check-out.", "Falta la reserva",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_saldoPendiente > 0 && nudMontoRecibidoFinal.Value < _saldoPendiente)
            {
                MessageBox.Show($"El monto recibido (${nudMontoRecibidoFinal.Value:N2}) es menor al saldo pendiente (${_saldoPendiente:N2}).",
                    "Pago insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_saldoPendiente > 0)
                {
                    _pagoBLL.Registrar(new Pago_68SA
                    {
                        ReservaId = _reserva.Id,
                        Monto = nudMontoRecibidoFinal.Value,
                        MetodoPago = (MetodoPago)cmbMetodoPagoFinal.SelectedItem
                    });
                }

                _reservaBLL.RegistrarCheckOut(_reserva.Id);
                MessageBox.Show($"Check-out registrado para la habitación {_reserva.Habitacion.NroHabitacion}.",
                    "Check-out confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Volver();
            }
            catch (EstadoReservaInvalidoException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo confirmar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Volver()
        {
            _openChildForm(new FormControlHabitaciones_68SA(_openChildForm, _modoOrigen, _fechaIngresoOrigen, _fechaEgresoOrigen));
        }

        private void ActualizarVisibilidadVueltoFinal()
        {
            bool esEfectivo = cmbMetodoPagoFinal.SelectedItem is MetodoPago metodo && metodo == MetodoPago.Efectivo;
            lblEtiquetaVueltoFinal.Visible = esEfectivo;
            lblVueltoFinal.Visible = esEfectivo;
        }

        public void UpdateIdioma()
        {
            // Pendiente junto con el resto de las traducciones de estos forms.
        }
    }
}