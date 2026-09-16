using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using FontAwesome.Sharp;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public enum ModoHabitaciones
    {
        Gestion,
        Reserva
    }

    public enum AccionHabitacion
    {
        Reservar,
        CheckInDirecto,
        CheckIn,
        VerReserva,
        CheckOut,
        Servicio,
        CambioHabitacion,
        MarcarDisponible,
        PonerEnMantenimiento
    }

    public partial class FormControlHabitaciones_68SA : Form, IIdiomaObserver_08YS
    {
        private readonly Action<Form> _openChildForm;
        private readonly ModoHabitaciones _modo;
        private readonly DateTime? _fechaIngreso;
        private readonly DateTime? _fechaEgreso;

        private readonly HabitacionBLL_68SA _habitacionBLL = BLLFactory_08YS.CreateHabitacionBLL();
        private readonly TipoHabitacionBLL_68SA _tipoBLL = BLLFactory_08YS.CreateTipoHabitacionBLL();

        private int? _filtroTipoId;
        private EstadoHabitacion? _filtroEstado;

        public FormControlHabitaciones_68SA(Action<Form> openChildForm, ModoHabitaciones modo,
                                             DateTime? fechaIngreso = null, DateTime? fechaEgreso = null)
        {
            _openChildForm = openChildForm;
            _modo = modo;
            _fechaIngreso = fechaIngreso;
            _fechaEgreso = fechaEgreso;

            InitializeComponent();

            Text = _modo == ModoHabitaciones.Reserva
                ? $"Habitaciones disponibles — {fechaIngreso:dd/MM} al {fechaEgreso:dd/MM}"
                : "Control de Habitaciones";

            pnlFilaEstado.Visible = _modo == ModoHabitaciones.Gestion;

            CargarChipsTipo();
            if (_modo == ModoHabitaciones.Gestion)
                CargarChipsEstado();

            CargarHabitaciones();
        }

        #region filtros chip/radio buttons

        private void CargarChipsTipo()
        {
            flpChipsTipo.Controls.Clear();

            var chipTodos = CrearChip("Todos", null, () => _filtroTipoId = null);
            chipTodos.Checked = true;
            flpChipsTipo.Controls.Add(chipTodos);

            foreach (var tipo in _tipoBLL.GetAll())
            {
                int id = tipo.Id;
                flpChipsTipo.Controls.Add(CrearChip(tipo.Nombre, id, () => _filtroTipoId = id));
            }
        }

        private void CargarChipsEstado()
        {
            flpChipsEstado.Controls.Clear();

            var chipTodos = CrearChip("Todos", null, () => _filtroEstado = null);
            chipTodos.Checked = true;
            flpChipsEstado.Controls.Add(chipTodos);

            foreach (EstadoHabitacion estado in Enum.GetValues(typeof(EstadoHabitacion)))
            {
                var valor = estado;
                flpChipsEstado.Controls.Add(CrearChip(TextoEstado(valor), valor, () => _filtroEstado = valor));
            }
        }

        private RadioButton CrearChip(string texto, object valor, Action alSeleccionar)
        {
            var chip = new RadioButton
            {
                Appearance = Appearance.Button,
                AutoSize = true,
                Text = texto,
                // Tag = valor,  <-- eliminado, no se usaba
                Padding = new Padding(10, 4, 10, 4),
                Margin = new Padding(4, 6, 4, 6),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(15, 25, 55),
                ForeColor = Color.FromArgb(200, 200, 210)
            };
            chip.FlatAppearance.BorderColor = Color.FromArgb(60, 75, 110);
            chip.CheckedChanged += (s, e) =>
            {
                chip.BackColor = chip.Checked ? Color.Goldenrod : Color.FromArgb(15, 25, 55);
                chip.ForeColor = chip.Checked ? Color.FromArgb(10, 15, 35) : Color.FromArgb(200, 200, 210);

                if (!chip.Checked) return;
                alSeleccionar();
                CargarHabitaciones();
            };
            return chip;
        }
        #endregion

        #region habitaciones

        private void CargarHabitaciones()
        {
            flpHabitaciones.Controls.Clear();

            List<Habitacion_68SA> habitaciones = _modo == ModoHabitaciones.Gestion
                ? _habitacionBLL.GetAll(_filtroTipoId, _filtroEstado)
                : _habitacionBLL.GetDisponibles(_fechaIngreso.Value, _fechaEgreso.Value, _filtroTipoId);

            foreach (var grupoPiso in habitaciones.GroupBy(h => h.Piso.Numero).OrderBy(g => g.Key))
            {
                var lblPiso = new Label
                {
                    Text = $"PISO {grupoPiso.Key}",
                    ForeColor = Color.FromArgb(160, 165, 180),
                    Font = new Font(Font, FontStyle.Bold),
                    AutoSize = true,
                    Margin = new Padding(4, 16, 4, 4)
                };
                flpHabitaciones.Controls.Add(lblPiso);

                var flpCardsPiso = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Width = flpHabitaciones.ClientSize.Width - 24,
                    Margin = new Padding(0)
                };

                foreach (var habitacion in grupoPiso.OrderBy(h => h.NroHabitacion))
                {
                    var card = new UC_HabitacionCard_68SA { Margin = new Padding(6) };
                    card.Cargar(habitacion, forzarDisponible: _modo == ModoHabitaciones.Reserva);
                    card.HabitacionSeleccionada += Card_HabitacionSeleccionada;
                    flpCardsPiso.Controls.Add(card);
                }

                flpHabitaciones.Controls.Add(flpCardsPiso);
            }
        }

        private void Card_HabitacionSeleccionada(object sender, Habitacion_68SA habitacion)
        {
            if (_modo == ModoHabitaciones.Reserva)
            {
                _openChildForm(new FormRegistrarReserva_68SA(_openChildForm, habitacion, _modo, _fechaIngreso, _fechaEgreso));
                return;
            }

            MostrarPopupAcciones(habitacion);
        }

        #endregion

        private void MostrarPopupAcciones(Habitacion_68SA habitacion)
        {
            var acciones = new List<(string Texto, IconChar Icono, AccionHabitacion Accion)>();

            switch (habitacion.EstadoVisual)
            {
                case EstadoHabitacion.Disponible:
                    acciones.Add(("Reservar", IconChar.CalendarPlus, AccionHabitacion.Reservar));
                    acciones.Add(("Check-in directo / Walk-in", IconChar.DoorOpen, AccionHabitacion.CheckInDirecto));
                    break;
                case EstadoHabitacion.Reservada:
                    acciones.Add(("Registrar check-in", IconChar.CalendarCheck, AccionHabitacion.CheckIn));
                    acciones.Add(("Ver / Modificar reserva", IconChar.Eye, AccionHabitacion.VerReserva));
                    break;
                case EstadoHabitacion.Ocupada:
                    acciones.Add(("Registrar check-out", IconChar.CalendarTimes, AccionHabitacion.CheckOut));
                    acciones.Add(("Servicio a la habitación", IconChar.Bell, AccionHabitacion.Servicio));
                    acciones.Add(("Cambio de habitación", IconChar.Retweet, AccionHabitacion.CambioHabitacion));
                    break;
                case EstadoHabitacion.EnLimpieza:
                case EstadoHabitacion.FueraDeServicio:
                    acciones.Add(("Marcar como Disponible", IconChar.CircleCheck, AccionHabitacion.MarcarDisponible));
                    break;
            }

            if (habitacion.EstadoVisual != EstadoHabitacion.FueraDeServicio)
                acciones.Add(("Poner en Mantenimiento", IconChar.Wrench, AccionHabitacion.PonerEnMantenimiento));

            using (var popup = new FormAccionesHabitacion_68SA(habitacion, acciones))
            {
                if (popup.ShowDialog(this) == DialogResult.OK && popup.AccionSeleccionada.HasValue)
                    EjecutarAccion(popup.AccionSeleccionada.Value, habitacion);
            }
        }

        private void EjecutarAccion(AccionHabitacion accion, Habitacion_68SA habitacion)
        {
            switch (accion)
            {
                case AccionHabitacion.MarcarDisponible:
                    _habitacionBLL.CambiarEstado(habitacion.Id, EstadoHabitacion.Disponible);
                    CargarHabitaciones();
                    break;

                case AccionHabitacion.Reservar:
                    _openChildForm(new FormRegistrarReserva_68SA(_openChildForm, habitacion, _modo, _fechaIngreso, _fechaEgreso));
                    break;

                case AccionHabitacion.CheckIn:
                    var reservaDeHoy = BLLFactory_08YS.CreateReservaBLL().GetConfirmadaHoyPorHabitacion(habitacion.Id);
                    if (reservaDeHoy == null)
                    {
                        MessageBox.Show("No se encontró una reserva vigente para hoy en esta habitación.", "Sin reserva",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    _openChildForm(new FormCheckIn_68SA(_openChildForm, reservaDeHoy, _modo, _fechaIngreso, _fechaEgreso));
                    break;

                case AccionHabitacion.CheckOut:
                    var reservaEnCurso = BLLFactory_08YS.CreateReservaBLL().GetEnCursoPorHabitacion(habitacion.Id);
                    if (reservaEnCurso == null)
                    {
                        MessageBox.Show("No se encontró una reserva En Curso para esta habitación.", "Sin reserva",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    _openChildForm(new FormCheckOut_68SA(_openChildForm, reservaEnCurso, _modo, _fechaIngreso, _fechaEgreso));
                    break;

                case AccionHabitacion.PonerEnMantenimiento:
                    if (habitacion.EstadoVisual == EstadoHabitacion.Reservada || habitacion.EstadoVisual == EstadoHabitacion.Ocupada)
                    {
                        var confirmacion = MessageBox.Show(
                            "Esta habitación tiene una reserva activa. Ponerla en mantenimiento no reubica al huésped ni modifica la reserva — eso todavía hay que resolverlo a mano. ¿Poner en mantenimiento igual?",
                            "Reserva activa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (confirmacion != DialogResult.Yes) break;
                    }
                    _habitacionBLL.CambiarEstado(habitacion.Id, EstadoHabitacion.FueraDeServicio);
                    CargarHabitaciones();
                    break;

                default:
                    MessageBox.Show($"'{accion}' todavía no está implementado.", "Pendiente",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private static string TextoEstado(EstadoHabitacion estado)
        {
            switch (estado)
            {
                case EstadoHabitacion.Disponible: return "Disponible";
                case EstadoHabitacion.Ocupada: return "Ocupada";
                case EstadoHabitacion.Reservada: return "Reservada";
                case EstadoHabitacion.EnLimpieza: return "Limpieza";
                case EstadoHabitacion.FueraDeServicio: return "Fuera de Servicio";
                default: return estado.ToString();
            }
        }

        // ---------- Idioma ----------

        public void UpdateIdioma()
        {
            TraducirControles(this);
            // Los chips y las tarjetas se generan en runtime y no pasan por este mecanismo de Tag;
            // si más adelante hace falta traducirlos, hay que llamar TraductorManager_08YS.Instance.GetTexto(...)
            // directamente en CargarChipsTipo/CargarChipsEstado/TextoEstado.
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

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}