using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using System;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormRenovarEstadia_68SA : Form
    {
        private readonly Reserva_68SA _reserva;
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();

        public FormRenovarEstadia_68SA(Reserva_68SA reserva, Habitacion_68SA habitacion)
        {
            _reserva = reserva;
            InitializeComponent();

            lblHabitacion.Text = habitacion.NroHabitacion;
            lblTitular.Text = $"{reserva.Titular.Nombre} {reserva.Titular.Apellido}";
            lblFechaActual.Text = reserva.FechaEgreso.ToString("dd/MM/yyyy");
            lblTarifa.Text = $"${reserva.TarifaNoche:N2}";

            dtpNuevaFechaEgreso.MinDate = reserva.FechaEgreso.AddDays(1);
            dtpNuevaFechaEgreso.Value = reserva.FechaEgreso.AddDays(1);
            dtpNuevaFechaEgreso.ValueChanged += (s, e) => ActualizarCosto();
            ActualizarCosto();

            btnConfirmar.Click += BtnConfirmar_Click;
            btnCancelar.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        }

        private void ActualizarCosto()
        {
            if (!dtpNuevaFechaEgreso.Value.HasValue) return;

            int noches = (dtpNuevaFechaEgreso.Value.Value.Date - _reserva.FechaEgreso.Date).Days;
            lblNochesAdicionales.Text = $"{noches} {(noches == 1 ? "noche" : "noches")}";
            lblCostoAdicional.Text = $"${noches * _reserva.TarifaNoche:N2}";
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (!dtpNuevaFechaEgreso.Value.HasValue) return;

            try
            {
                _reservaBLL.ExtenderEstadia(_reserva.Id, dtpNuevaFechaEgreso.Value.Value.Date);
                MessageBox.Show(
                    "Estadía renovada correctamente. El costo adicional queda como saldo a cobrar en el check-out.",
                    "Renovación confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (HabitacionNoDisponibleException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo renovar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (EstadoReservaInvalidoException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo renovar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (RangoFechasInvalidoException_68SA ex)
            {
                MessageBox.Show(ex.Message, "No se pudo renovar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}