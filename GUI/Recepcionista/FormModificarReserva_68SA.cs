using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using System;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormModificarReserva_68SA : Form
    {
        private readonly Reserva_68SA _reserva;
        private readonly Habitacion_68SA _habitacion;
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();

        public FormModificarReserva_68SA(Reserva_68SA reserva)
        {
            _reserva = reserva;
            _habitacion = BLLFactory_08YS.CreateHabitacionBLL().GetById(reserva.Habitacion.Id);

            InitializeComponent();

            lblHabitacion.Text = _habitacion.NroHabitacion;
            lblTitular.Text = $"{reserva.Titular.Nombre} {reserva.Titular.Apellido} (DNI {reserva.Titular.Documento})";
            lblTipo.Text = $"{_habitacion.Tipo.Nombre} — Capacidad: {_habitacion.Tipo.Capacidad}";

            dtpFechaIngreso.MinDate = DateTime.Today;
            dtpFechaIngreso.Value = reserva.FechaIngreso;
            dtpFechaEgreso.MinDate = reserva.FechaIngreso.AddDays(1);
            dtpFechaEgreso.Value = reserva.FechaEgreso;

            nudAdultos.Value = reserva.CantidadAdultos;
            nudNinos.Value = reserva.CantidadNinos;

            dtpFechaIngreso.ValueChanged += (s, e) => { SincronizarMinimoEgreso(); ActualizarResumen(); };
            dtpFechaEgreso.ValueChanged += (s, e) => ActualizarResumen();
            nudAdultos.ValueChanged += (s, e) => ActualizarResumen();
            nudNinos.ValueChanged += (s, e) => ActualizarResumen();

            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            ActualizarResumen();
        }

        private void SincronizarMinimoEgreso()
        {
            if (!dtpFechaIngreso.Value.HasValue) return;

            DateTime minimoEgreso = dtpFechaIngreso.Value.Value.AddDays(1);
            dtpFechaEgreso.MinDate = minimoEgreso;
            if (dtpFechaEgreso.Value.HasValue && dtpFechaEgreso.Value.Value < minimoEgreso)
                dtpFechaEgreso.Value = minimoEgreso;
        }

        private void ActualizarResumen()
        {
            if (!dtpFechaIngreso.Value.HasValue || !dtpFechaEgreso.Value.HasValue) return;

            int noches = (dtpFechaEgreso.Value.Value.Date - dtpFechaIngreso.Value.Value.Date).Days;
            lblNoches.Text = noches.ToString();
            lblMontoTotal.Text = $"${noches * _reserva.TarifaNoche:N2}";

            int composicion = (int)nudAdultos.Value + (int)nudNinos.Value;
            lblAdvertenciaCapacidad.Visible = composicion > _habitacion.Tipo.Capacidad;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!dtpFechaIngreso.Value.HasValue || !dtpFechaEgreso.Value.HasValue) return;

            try
            {
                _reservaBLL.Modificar(_reserva.Id, dtpFechaIngreso.Value.Value.Date, dtpFechaEgreso.Value.Value.Date,
                    (int)nudAdultos.Value, (int)nudNinos.Value);

                MessageBox.Show("Reserva modificada correctamente.", "Cambios guardados",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (CapacidadExcedidaException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HabitacionNoDisponibleException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (EstadoReservaInvalidoException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (RangoFechasInvalidoException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}