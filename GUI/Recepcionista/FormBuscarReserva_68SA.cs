using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormBuscarReserva_68SA : Form
    {
        public Reserva_68SA ReservaSeleccionada { get; private set; }

        private readonly EstadoReserva _estadoBuscado;
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();

        public FormBuscarReserva_68SA(EstadoReserva estadoBuscado, string titulo, IconChar icono)
        {
            _estadoBuscado = estadoBuscado;
            InitializeComponent();

            lblTitulo.Text = titulo;
            iconTitulo.IconChar = icono;

            cmbTipoDocumento.Items.AddRange(Enum.GetValues(typeof(TipoDocumento)).Cast<object>().ToArray());
            cmbTipoDocumento.SelectedIndex = 0;

            btnBuscar.Click += BtnBuscar_Click;
            btnSeleccionar.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string documento = txtDocumento.RealText.Trim();
            if (string.IsNullOrEmpty(documento))
            {
                MessageBox.Show("Ingresá un número de documento.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var reserva = _reservaBLL.GetPorDocumentoTitularYEstado(documento, _estadoBuscado);
            ReservaSeleccionada = reserva;

            if (reserva == null)
            {
                lblResultado.ForeColor = Color.FromArgb(235, 90, 90);
                lblResultado.Text = "No se encontró una reserva con ese documento en el estado esperado.";
                btnSeleccionar.Enabled = false;
                return;
            }

            lblResultado.ForeColor = Color.FromArgb(76, 217, 100);
            lblResultado.Text = $"Habitación {reserva.Habitacion.NroHabitacion} — {reserva.Titular.Nombre} {reserva.Titular.Apellido}\n" +
                                 $"{reserva.FechaIngreso:dd/MM/yyyy} → {reserva.FechaEgreso:dd/MM/yyyy}\n" +
                                 $"{reserva.CantidadAdultos} adulto(s), {reserva.CantidadNinos} niño(s)";
            btnSeleccionar.Enabled = true;
        }
    }
}