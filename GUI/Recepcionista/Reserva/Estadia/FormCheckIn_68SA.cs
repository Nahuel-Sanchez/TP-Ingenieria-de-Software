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
            using (var popup = new FormBuscarReserva_68SA(EstadoReserva.Confirmada, TraductorManager_08YS.Instance.GetTexto("CheckIn_tituloBuscarReserva"), IconChar.DoorOpen))
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
            lblResTitular.Text = $"{reserva.Titular.Nombre} {reserva.Titular.Apellido} ({TraductorManager_08YS.Instance.GetTexto("Comun_abrevDni")} {reserva.Titular.Documento})";
            lblResFechas.Text = $"{reserva.FechaIngreso:dd/MM/yyyy} → {reserva.FechaEgreso:dd/MM/yyyy}";
            lblResComposicion.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("Comun_txtAdultosNinos"), reserva.CantidadAdultos, reserva.CantidadNinos);

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
                    PlaceholderText = TraductorManager_08YS.Instance.GetTexto("Comun_placeholderDni"),
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
                    Text = TraductorManager_08YS.Instance.GetTexto("CheckIn_txtSinBuscar")
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
                toolTip1.SetToolTip(btnNuevo, TraductorManager_08YS.Instance.GetTexto("Comun_tooltipRegistrarHuespedNuevo"));
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
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("CheckIn_msgIngreseDniAcompanante"), TraductorManager_08YS.Instance.GetTexto("Comun_msgDatosIncompletos"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dni == _reserva.Titular.Documento)
            {
                fila.HuespedEncontrado = null;
                fila.Resultado.ForeColor = Color.FromArgb(235, 90, 90);
                fila.Resultado.Text = TraductorManager_08YS.Instance.GetTexto("CheckIn_msgDocumentoEsDelTitular");
                return;
            }

            bool yaCargado = _filasAcompanantes.Any(f => f != fila && f.HuespedEncontrado != null && f.HuespedEncontrado.Documento == dni);
            if (yaCargado)
            {
                fila.HuespedEncontrado = null;
                fila.Resultado.ForeColor = Color.FromArgb(235, 90, 90);
                fila.Resultado.Text = TraductorManager_08YS.Instance.GetTexto("CheckIn_msgDocumentoYaCargado");
                return;
            }

            var huesped = _huespedBLL.GetByDocumento(dni);
            if (huesped == null)
            {
                fila.HuespedEncontrado = null;
                fila.Resultado.ForeColor = Color.FromArgb(235, 90, 90);
                fila.Resultado.Text = TraductorManager_08YS.Instance.GetTexto("CheckIn_msgAcompananteNoEncontrado");
                return;
            }

            fila.HuespedEncontrado = huesped;
            fila.Resultado.ForeColor = Color.FromArgb(76, 217, 100);
            fila.Resultado.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("Comun_txtEdadAnios"), huesped.Nombre, huesped.Apellido, huesped.Edad);
        }

        private void AbrirRegistrarHuesped(FilaAcompanante fila)
        {
            using (var formNuevo = new FormRegistrarHuesped_68SA(fila.Dni.RealText.Trim()))
            {
                if (formNuevo.ShowDialog(this) != DialogResult.OK) return;

                var huesped = formNuevo.HuespedCreado;

                if (huesped.Documento == _reserva.Titular.Documento)
                {
                    fila.HuespedEncontrado = null;
                    fila.Resultado.ForeColor = Color.FromArgb(235, 90, 90);
                    fila.Resultado.Text = TraductorManager_08YS.Instance.GetTexto("CheckIn_msgDocumentoEsDelTitular");
                    return;
                }

                bool yaCargado = _filasAcompanantes.Any(f => f != fila && f.HuespedEncontrado != null && f.HuespedEncontrado.Documento == huesped.Documento);
                if (yaCargado)
                {
                    fila.HuespedEncontrado = null;
                    fila.Resultado.ForeColor = Color.FromArgb(235, 90, 90);
                    fila.Resultado.Text = TraductorManager_08YS.Instance.GetTexto("CheckIn_msgDocumentoYaCargado");
                    return;
                }

                fila.Dni.Text = huesped.Documento;
                fila.HuespedEncontrado = huesped;
                fila.Resultado.ForeColor = Color.FromArgb(76, 217, 100);
                fila.Resultado.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("Comun_txtEdadAnios"), huesped.Nombre, huesped.Apellido, huesped.Edad);
            }
        }

        // ---------- Confirmar ----------

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (_reserva == null)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("CheckIn_msgFaltaReserva"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloFaltaReserva"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var acompanantes = new List<Huesped_68SA>();
            foreach (var fila in _filasAcompanantes)
            {
                if (fila.HuespedEncontrado == null)
                {
                    MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("CheckIn_msgFaltanAcompanantes"), TraductorManager_08YS.Instance.GetTexto("CheckIn_tituloFaltanAcompanantes"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                acompanantes.Add(fila.HuespedEncontrado);
            }

            int adultosIngresados = 1 + acompanantes.Count(h => HuespedBLL_68SA.EsMayorDeEdad(h)); // el titular siempre es adulto
            int ninosIngresados = acompanantes.Count(h => !HuespedBLL_68SA.EsMayorDeEdad(h));

            if (adultosIngresados != _reserva.CantidadAdultos || ninosIngresados != _reserva.CantidadNinos)
            {
                MessageBox.Show(
                    string.Format(TraductorManager_08YS.Instance.GetTexto("CheckIn_msgComposicionNoCoincide"),
                        _reserva.CantidadAdultos, _reserva.CantidadNinos, adultosIngresados, ninosIngresados),
                    TraductorManager_08YS.Instance.GetTexto("CheckIn_tituloComposicionNoCoincide"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _reservaBLL.RegistrarCheckIn(_reserva.Id, acompanantes);
                MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("CheckIn_msgCheckInRegistrado"), _reserva.Habitacion.NroHabitacion),
                    TraductorManager_08YS.Instance.GetTexto("CheckIn_tituloCheckInConfirmado"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Volver();
            }
            catch (EstadoReservaInvalidoException_68SA)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Comun_excEstadoReservaInvalido"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloNoSePudoConfirmar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (AcompananteDuplicadoException_68SA)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Comun_excAcompananteDuplicado"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloNoSePudoConfirmar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (TitularEntreAcompanantesException_68SA)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Comun_excTitularEntreAcompanantes"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloNoSePudoConfirmar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Volver()
        {
            _openChildForm(new FormControlHabitaciones_68SA(_openChildForm, _modoOrigen, _fechaIngresoOrigen, _fechaEgresoOrigen));
        }

        public void UpdateIdioma()
        {
            TraducirControles(this);
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
    }
}