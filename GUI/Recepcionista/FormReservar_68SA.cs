using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using Service_08YS;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormReservar_68SA : Form, IIdiomaObserver_08YS
    {
        private readonly Action<Form> _openChildForm;
        private readonly Habitacion_68SA _habitacion;
        private readonly ModoHabitaciones _modoOrigen;
        private readonly DateTime? _fechaIngresoOrigen;
        private readonly DateTime? _fechaEgresoOrigen;
        private decimal _montoTotal;

        private readonly HuespedBLL_68SA _huespedBLL = BLLFactory_08YS.CreateHuespedBLL();
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private readonly PagoBLL_68SA _pagoBLL = BLLFactory_08YS.CreatePagoBLL();

        private Huesped_68SA _huespedSeleccionado;

        public FormReservar_68SA(Action<Form> openChildForm, Habitacion_68SA habitacion, ModoHabitaciones modoOrigen,
                                  DateTime? fechaIngreso = null, DateTime? fechaEgreso = null)
        {
            _openChildForm = openChildForm;
            _habitacion = habitacion;
            _modoOrigen = modoOrigen;
            _fechaIngresoOrigen = fechaIngreso;
            _fechaEgresoOrigen = fechaEgreso;

            InitializeComponent();

            lblDetTipo.Text = habitacion.Tipo.Nombre;
            lblDetDescripcion.Text = string.IsNullOrEmpty(habitacion.Tipo.Descripcion) ? "-" : habitacion.Tipo.Descripcion;
            lblDetPiso.Text = habitacion.Piso.Numero.ToString();
            lblDetPrecio.Text = $"${habitacion.Tipo.TarifaNoche:N2}";
            lblDetCapacidad.Text = habitacion.Tipo.Capacidad.ToString();

            lblResHabitacion.Text = habitacion.NroHabitacion;

            nudAdultos.Maximum = habitacion.Tipo.Capacidad;
            nudNinos.Maximum = habitacion.Tipo.Capacidad;

            cmbTipoDocumento.Items.AddRange(Enum.GetValues(typeof(TipoDocumento)).Cast<object>().ToArray());
            cmbTipoDocumento.SelectedIndex = 0;

            cmbMetodoPago.Items.AddRange(Enum.GetValues(typeof(MetodoPago)).Cast<object>().ToArray());
            cmbMetodoPago.SelectedIndex = 0;

            toolTip1.SetToolTip(btnHuespedNuevo, "Registrar huésped nuevo");

            dtpFechaIngreso.MinDate = DateTime.Today;
            dtpFechaIngreso.Value = fechaIngreso ?? DateTime.Today;
            dtpFechaEgreso.Value = fechaEgreso ?? dtpFechaIngreso.Value.Value.AddDays(1);

            dtpFechaIngreso.ValueChanged += (s, e) => RecalcularResumen();
            dtpFechaEgreso.ValueChanged += (s, e) => RecalcularResumen();
            nudAdultos.ValueChanged += (s, e) => ValidarComposicion();
            nudNinos.ValueChanged += (s, e) => ValidarComposicion();
            RecalcularResumen();
            ValidarComposicion();

            btnConsultarDocumento.Click += BtnConsultarDocumento_Click;
            btnHuespedNuevo.Click += BtnHuespedNuevo_Click;
            nudMontoRecibido.ValueChanged += (s, e) => ActualizarVuelto();
            btnConfirmar.Click += BtnConfirmar_Click;
            btnCancelar.Click += (s, e) => Volver();
        }

        private void RecalcularResumen()
        {
            if (!dtpFechaIngreso.Value.HasValue || !dtpFechaEgreso.Value.HasValue) return;

            DateTime ingreso = dtpFechaIngreso.Value.Value.Date;
            DateTime egreso = dtpFechaEgreso.Value.Value.Date;

            if (egreso <= ingreso)
            {
                dtpFechaEgreso.Value = ingreso.AddDays(1);
                return; // dispara este mismo método de nuevo vía el ValueChanged de dtpFechaEgreso
            }

            int noches = (egreso - ingreso).Days;
            _montoTotal = _habitacion.Tipo.TarifaNoche * noches;

            lblResNoches.Text = $"{noches} {(noches == 1 ? "noche" : "noches")}";
            lblResMontoTotal.Text = $"${_montoTotal:N2}";

            nudMontoRecibido.Value = _montoTotal;
            ActualizarVuelto();
        }

        private void ValidarComposicion()
        {
            int total = (int)nudAdultos.Value + (int)nudNinos.Value;
            int capacidad = _habitacion.Tipo.Capacidad;

            lblComposicionAdvertencia.Visible = total > capacidad;
            if (total > capacidad)
                lblComposicionAdvertencia.Text = $"La suma de adultos y niños supera la capacidad máxima ({capacidad}).";
        }

        private void BtnConsultarDocumento_Click(object sender, EventArgs e)
        {
            string dni = txtDni.RealText.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Ingresá un DNI para buscar.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var huesped = _huespedBLL.GetByDocumento(dni);
            if (huesped == null)
            {
                MessageBox.Show("No se encontró un huésped con ese documento. Usá el botón de al lado para registrarlo.",
                    "Huésped no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CargarHuesped(huesped);
        }

        private void BtnHuespedNuevo_Click(object sender, EventArgs e)
        {
            // TODO: cuando exista FormRegistrarHuesped_68SA, reemplazar esto por su apertura.
            // Sugerencia: que sea un ShowDialog() (no _openChildForm) para no perder el estado
            // de esta reserva en curso, y que devuelva el Huesped_68SA creado, algo así:
            //
            // using (var formNuevo = new FormRegistrarHuesped_68SA(txtDni.RealText.Trim()))
            // {
            //     if (formNuevo.ShowDialog(this) == DialogResult.OK)
            //         CargarHuesped(formNuevo.HuespedCreado);
            // }
            MessageBox.Show("Registrar huésped nuevo — formulario todavía no implementado.", "Pendiente",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarHuesped(Huesped_68SA huesped)
        {
            _huespedSeleccionado = huesped;

            txtDni.Text = huesped.Documento;
            txtNombre.Text = huesped.Nombre;
            txtApellido.Text = huesped.Apellido;
            cmbTipoDocumento.SelectedItem = huesped.TipoDocumento;
            dtpFechaNacimiento.Value = huesped.FechaNacimiento;
            txtNacionalidad.Text = huesped.Nacionalidad;
            txtEmail.Text = huesped.Email;
            txtTelefono.Text = huesped.Telefono;
        }

        private void ActualizarVuelto()
        {
            decimal vuelto = nudMontoRecibido.Value - _montoTotal;
            lblVuelto.Text = $"${Math.Max(0, vuelto):N2}";
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (_huespedSeleccionado == null)
            {
                MessageBox.Show("Consultá el documento del titular antes de confirmar.", "Falta el titular",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((int)nudAdultos.Value + (int)nudNinos.Value > _habitacion.Tipo.Capacidad)
            {
                MessageBox.Show($"La composición supera la capacidad de la habitación (máx. {_habitacion.Tipo.Capacidad}).",
                    "Capacidad excedida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudMontoRecibido.Value < _montoTotal)
            {
                MessageBox.Show($"El monto recibido (${nudMontoRecibido.Value:N2}) es menor al total (${_montoTotal:N2}).",
                    "Pago insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var reserva = new Reserva_68SA
            {
                Titular = _huespedSeleccionado,
                Habitacion = _habitacion,
                FechaIngreso = dtpFechaIngreso.Value.Value.Date,
                FechaEgreso = dtpFechaEgreso.Value.Value.Date,
                CantidadAdultos = (int)nudAdultos.Value,
                CantidadNinos = (int)nudNinos.Value,
                TarifaNoche = _habitacion.Tipo.TarifaNoche
            };

            try
            {
                int reservaId = _reservaBLL.Crear(reserva);

                _pagoBLL.Registrar(new Pago_68SA
                {
                    ReservaId = reservaId,
                    Monto = nudMontoRecibido.Value,
                    MetodoPago = (MetodoPago)cmbMetodoPago.SelectedItem
                });

                MessageBox.Show($"Reserva #{reservaId} confirmada para la habitación {_habitacion.NroHabitacion}.",
                    "Reserva confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Volver();
            }
            catch (TitularMenorDeEdadException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo confirmar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HabitacionNoDisponibleException_68SA ex)
            {
                MessageBox.Show(ex.Message + " Volvé a la lista para elegir otra.", "No se pudo confirmar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Volver();
            }
            catch (RangoFechasInvalidoException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo confirmar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Volver()
        {
            _openChildForm(new FormControlHabitaciones_68SA(_openChildForm, _modoOrigen, _fechaIngresoOrigen, _fechaEgresoOrigen));
        }

        public void UpdateIdioma()
        {
            // Pendiente junto con el resto de las traducciones de estos forms.
        }

        private void pnlColReserva_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblResEtiquetaMontoTotal_Click(object sender, EventArgs e)
        {

        }

        private void lblResMontoTotal_Click(object sender, EventArgs e)
        {

        }

        private void pnlResumen_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}