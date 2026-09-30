using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using System;
using System.Windows.Forms;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using System.Collections.Generic;

namespace GUI_08YS.Recepcionista
{
    public partial class FormModificarReserva_68SA : Form
    {
        private static readonly Dictionary<string, Permisos> _mapaPermisos =
            new Dictionary<string, Permisos>
            {
                { nameof(btnGuardar), Permisos.ModificarReserva },
            };

        private readonly Reserva_68SA _reserva;
        private readonly Habitacion_68SA _habitacion;
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();

        public FormModificarReserva_68SA(Reserva_68SA reserva)
        {
            _reserva = reserva;
            _habitacion = BLLFactory_08YS.CreateHabitacionBLL().GetById(reserva.Habitacion.Id);

            InitializeComponent();
            TraducirControles(this);
            Text = lblTitulo.Text;

            PermissionFilter_08YS.Aplicar(this, _mapaPermisos);

            var t = TraductorManager_08YS.Instance;
            lblHabitacion.Text = _habitacion.NroHabitacion;
            lblTitular.Text = $"{reserva.Titular.Nombre} {reserva.Titular.Apellido} ({t.GetTexto("Comun_abrevDni")} {reserva.Titular.Documento})";
            lblTipo.Text = string.Format(t.GetTexto("ModificarReserva_txtTipoCapacidad"), _habitacion.Tipo.Nombre, _habitacion.Tipo.Capacidad);

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

                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("ModificarReserva_msgGuardado"),
                    TraductorManager_08YS.Instance.GetTexto("ModificarReserva_tituloGuardado"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (CapacidadExcedidaException_68SA)
            {
                MostrarNoSePudoGuardar("Comun_excCapacidadExcedida");
            }
            catch (HabitacionNoDisponibleException_68SA)
            {
                MostrarNoSePudoGuardar("Comun_excHabitacionNoDisponible");
            }
            catch (EstadoReservaInvalidoException_68SA)
            {
                MostrarNoSePudoGuardar("Comun_excEstadoReservaInvalido");
            }
            catch (RangoFechasInvalidoException_68SA)
            {
                MostrarNoSePudoGuardar("Comun_excRangoFechasInvalido");
            }
        }

        private static void MostrarNoSePudoGuardar(string claveMensaje)
        {
            MessageBox.Show(TraductorManager_08YS.Instance.GetTexto(claveMensaje),
                TraductorManager_08YS.Instance.GetTexto("ModificarReserva_tituloNoSePudoGuardar"),
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag is string tag && !string.IsNullOrWhiteSpace(tag))
                    c.Text = TraductorManager_08YS.Instance.GetTexto(tag);

                if (c.HasChildren)
                    TraducirControles(c);
            }
        }
    }
}