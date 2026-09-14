using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using CustomControls;
using FontAwesome.Sharp;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormCheckIn_68SA : Form, IIdiomaObserver_08YS
    {
        private class FilaAcompanante
        {
            public Panel Contenedor;
            public IconPlaceholderTextBox Dni;
            public Label Resultado;
            public Huesped_68SA HuespedEncontrado;
        }

        private readonly Action<Form> _openChildForm;
        private readonly ModoHabitaciones _modoOrigen;
        private readonly DateTime? _fechaIngresoOrigen;
        private readonly DateTime? _fechaEgresoOrigen;

        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private readonly HuespedBLL_68SA _huespedBLL = BLLFactory_08YS.CreateHuespedBLL();

        private readonly List<FilaAcompanante> _filasAcompanantes = new List<FilaAcompanante>();
        private Reserva_68SA _reserva;

        // Caso 1: sin reserva conocida — arranca mostrando el botón de búsqueda
        public FormCheckIn_68SA(Action<Form> openChildForm, ModoHabitaciones modoOrigen,
                                 DateTime? fechaIngresoOrigen = null, DateTime? fechaEgresoOrigen = null)
            : this(openChildForm, null, modoOrigen, fechaIngresoOrigen, fechaEgresoOrigen)
        {
        }

        // Caso 2: ya se conoce la reserva (viene de una habitación puntual)
        public FormCheckIn_68SA(Action<Form> openChildForm, Reserva_68SA reserva, ModoHabitaciones modoOrigen,
                                 DateTime? fechaIngresoOrigen = null, DateTime? fechaEgresoOrigen = null)
        {
            _openChildForm = openChildForm;
            _modoOrigen = modoOrigen;
            _fechaIngresoOrigen = fechaIngresoOrigen;
            _fechaEgresoOrigen = fechaEgresoOrigen;

            InitializeComponent();

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
            using (var popup = new FormBuscarReserva_68SA(EstadoReserva.Confirmada, "Buscar reserva para check-in", IconChar.DoorOpen))
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

            int totalHuespedes = reserva.CantidadAdultos + reserva.CantidadNinos;
            bool necesitaAcompanantes = totalHuespedes > 1;

            if (necesitaAcompanantes)
            {
                GenerarFilasAcompanantes(totalHuespedes - 1);
                if (!flpContenido.Controls.Contains(pnlAcompanantes))
                    flpContenido.Controls.Add(pnlAcompanantes);
            }
            else if (flpContenido.Controls.Contains(pnlAcompanantes))
            {
                flpContenido.Controls.Remove(pnlAcompanantes);
            }
        }

        private void GenerarFilasAcompanantes(int cantidad)
        {
            flpFilas.Controls.Clear();
            _filasAcompanantes.Clear();

            for (int i = 0; i < cantidad; i++)
            {
                var panel = new Panel { Width = 640, Height = 46, Margin = new Padding(0, 0, 0, 8) };

                var dni = new IconPlaceholderTextBox
                {
                    Location = new Point(0, 3),
                    Size = new Size(150, 40),
                    BackColor = Color.FromArgb(5, 15, 45),
                    BorderColor = Color.Goldenrod,
                    BorderFocusColor = Color.Goldenrod,
                    BorderWidth = 2,
                    ForeColor = SystemColors.ControlLightLight,
                    IconChar = IconChar.Hashtag,
                    IconColor = Color.Goldenrod,
                    IconPadding = 4,
                    IconSize = 18,
                    PlaceholderColor = Color.LightGray,
                    PlaceholderText = "DNI",
                    ScrollBars = ScrollBars.None,
                    Font = new Font("Segoe UI", 9F)
                };
                panel.Controls.Add(dni);

                var fila = new FilaAcompanante { Contenedor = panel, Dni = dni };

                var btnBuscar = new IconButton
                {
                    IconChar = IconChar.MagnifyingGlass,
                    IconColor = Color.Gold,
                    IconSize = 18,
                    BackColor = Color.FromArgb(5, 15, 45),
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(156, 3),
                    Size = new Size(40, 40),
                    UseVisualStyleBackColor = false
                };
                btnBuscar.Click += (s, e) => BuscarAcompanante(fila);
                panel.Controls.Add(btnBuscar);

                var resultado = new Label
                {
                    Location = new Point(204, 12),
                    Size = new Size(340, 24),
                    ForeColor = Color.FromArgb(160, 165, 180),
                    Font = new Font("Segoe UI", 9F),
                    Text = "Sin buscar."
                };
                panel.Controls.Add(resultado);
                fila.Resultado = resultado;

                var btnNuevo = new IconButton
                {
                    IconChar = IconChar.UserPlus,
                    IconColor = Color.Gold,
                    IconSize = 18,
                    BackColor = Color.FromArgb(5, 15, 45),
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(552, 3),
                    Size = new Size(40, 40),
                    UseVisualStyleBackColor = false
                };
                toolTip1.SetToolTip(btnNuevo, "Registrar huésped nuevo");
                btnNuevo.Click += (s, e) => AbrirRegistrarHuesped(fila);
                panel.Controls.Add(btnNuevo);

                flpFilas.Controls.Add(panel);
                _filasAcompanantes.Add(fila);
            }
        }

        private void BuscarAcompanante(FilaAcompanante fila)
        {
            string dni = fila.Dni.RealText.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Ingresá el DNI del acompañante.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dni == _reserva.Titular.Documento)
            {
                fila.HuespedEncontrado = null;
                fila.Resultado.ForeColor = Color.FromArgb(235, 90, 90);
                fila.Resultado.Text = "Ese documento es el del titular — no puede repetirse como acompañante.";
                return;
            }

            var huesped = _huespedBLL.GetByDocumento(dni);
            if (huesped == null)
            {
                fila.HuespedEncontrado = null;
                fila.Resultado.ForeColor = Color.FromArgb(235, 90, 90);
                fila.Resultado.Text = "No encontrado — registralo con el ícono de al lado.";
                return;
            }

            fila.HuespedEncontrado = huesped;
            int edad = HuespedBLL_68SA.CalcularEdad(huesped.FechaNacimiento);
            fila.Resultado.ForeColor = Color.FromArgb(76, 217, 100);
            fila.Resultado.Text = $"{huesped.Nombre} {huesped.Apellido} ({edad} años)";
        }

        private void AbrirRegistrarHuesped(FilaAcompanante fila)
        {
            // TODO: cuando exista FormRegistrarHuesped_68SA, reemplazar por su apertura (ShowDialog),
            // y al volver con OK, cargar el Huesped_68SA creado en 'fila' igual que en BuscarAcompanante:
            //
            // using (var formNuevo = new FormRegistrarHuesped_68SA(fila.Dni.RealText.Trim()))
            // {
            //     if (formNuevo.ShowDialog(this) == DialogResult.OK)
            //     {
            //         fila.HuespedEncontrado = formNuevo.HuespedCreado;
            //         fila.Dni.Text = fila.HuespedEncontrado.Documento;
            //         ... actualizar fila.Resultado igual que en BuscarAcompanante
            //     }
            // }
            MessageBox.Show("Registrar huésped nuevo — formulario todavía no implementado.", "Pendiente",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ---------- Confirmar ----------

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (_reserva == null)
            {
                MessageBox.Show("Buscá y encontrá una reserva antes de confirmar el check-in.", "Falta la reserva",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var acompanantes = new List<Huesped_68SA>();
            foreach (var fila in _filasAcompanantes)
            {
                if (fila.HuespedEncontrado == null)
                {
                    MessageBox.Show("Buscá (o registrá) a todos los acompañantes antes de confirmar.", "Faltan acompañantes",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                acompanantes.Add(fila.HuespedEncontrado);
            }

            int adultosIngresados = 1 + acompanantes.Count(h => HuespedBLL_68SA.EsMayorDeEdad(h.FechaNacimiento)); // el titular siempre es adulto
            int ninosIngresados = acompanantes.Count(h => !HuespedBLL_68SA.EsMayorDeEdad(h.FechaNacimiento));

            if (adultosIngresados != _reserva.CantidadAdultos || ninosIngresados != _reserva.CantidadNinos)
            {
                MessageBox.Show(
                    $"La reserva espera {_reserva.CantidadAdultos} adulto(s) y {_reserva.CantidadNinos} niño(s), " +
                    $"pero según las edades cargadas hay {adultosIngresados} adulto(s) y {ninosIngresados} niño(s).",
                    "La composición no coincide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _reservaBLL.RegistrarCheckIn(_reserva.Id, acompanantes);
                MessageBox.Show($"Check-in registrado para la habitación {_reserva.Habitacion.NroHabitacion}.",
                    "Check-in confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void UpdateIdioma()
        {
            // Pendiente junto con el resto de las traducciones de estos forms.
        }
    }
}