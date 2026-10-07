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
using Service_08YS.Entities.Acceso;

using GUI_08YS.UserControls;
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
        VerEstadia,
        CheckOut,
        Servicio,
        CambioHabitacion,
        Renovar,
        MarcarDisponible,
        PonerEnMantenimiento
    }

    public partial class FormControlHabitaciones_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private readonly Action<Form> _openChildForm;
        private readonly ModoHabitaciones _modo;
        private readonly DateTime? _fechaIngreso;
        private readonly DateTime? _fechaEgreso;
        // Nueva reserva: huéspedes elegidos en el paso anterior (0 = sin indicar)
        private readonly int _adultos, _ninos;

        private readonly HabitacionBLL_68SA _habitacionBLL = BLLFactory_08YS.CreateHabitacionBLL();
        private readonly TipoHabitacionBLL_68SA _tipoBLL = BLLFactory_08YS.CreateTipoHabitacionBLL();
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private readonly List<TileEstadoHabitacion_68SA> _tiles = new List<TileEstadoHabitacion_68SA>();

        private int? _filtroTipoId;
        private EstadoHabitacion? _filtroEstado;
        private bool _armando; // evita recargar mientras se arman los filtros

        private FontAwesome.Sharp.IconButton _btnCambiarFechas;

        // Acción del popup -> permiso requerido. Las acciones que no figuran acá no requieren un permiso propio.
        private static readonly Dictionary<AccionHabitacion, Permisos> _mapaPermisosAcciones =
            new Dictionary<AccionHabitacion, Permisos>
            {
                { AccionHabitacion.Reservar,             Permisos.RegistrarReserva        },
                { AccionHabitacion.CheckInDirecto,       Permisos.RegistrarCheckIn        },
                { AccionHabitacion.CheckIn,              Permisos.RegistrarCheckIn        },
                { AccionHabitacion.CheckOut,             Permisos.RegistrarCheckOut       },
                { AccionHabitacion.Renovar,              Permisos.ExtenderEstadia         },
                { AccionHabitacion.Servicio,             Permisos.RegistrarRoomService    },
                { AccionHabitacion.VerEstadia,           Permisos.VerReservas             },
                { AccionHabitacion.MarcarDisponible,     Permisos.CambiarEstadoHabitacion },
                { AccionHabitacion.PonerEnMantenimiento, Permisos.CambiarEstadoHabitacion },
            };

        #endregion

        #region Constructor

        public FormControlHabitaciones_68SA(Action<Form> openChildForm, ModoHabitaciones modo,
                                             DateTime? fechaIngreso = null, DateTime? fechaEgreso = null,
                                             int? tipoInicial = null, int adultos = 0, int ninos = 0)
        {
            _openChildForm = openChildForm;
            _modo = modo;
            _fechaIngreso = fechaIngreso;
            _fechaEgreso = fechaEgreso;
            _adultos = Math.Max(0, adultos);
            _ninos = Math.Max(0, ninos);
            if (modo == ModoHabitaciones.Reserva) _filtroTipoId = tipoInicial;

            InitializeComponent();

            // Los pisos van uno debajo del otro (nunca en una columna nueva a la derecha): solo se desplaza en vertical
            // y las tarjetas de cada piso pasan a la fila siguiente cuando no entran a lo ancho.
            flpHabitaciones.WrapContents = false;
            LayoutAdaptable_68SA.OcultarScrollHorizontal(flpHabitaciones);
            LayoutAdaptable_68SA.HabilitarDobleBuffer(flpHabitaciones);
            flpHabitaciones.Layout += (s, e) => AjustarAnchoPisos();

            // En modo reserva solo se listan las disponibles para el rango: los indicadores por estado no aplican
            flpResumen.Visible = _modo == ModoHabitaciones.Gestion;
            btnActualizar.Click += (s, e) => CargarHabitaciones();
            if (_modo == ModoHabitaciones.Reserva) CrearBotonCambiarFechas();

            AplicarTextos();
            CargarChipsTipo();
            if (_modo == ModoHabitaciones.Gestion)
                CrearIndicadores();

            CargarHabitaciones();
        }

        #endregion

        #region filtros: indicadores por estado y chips por tipo

        /// <summary>Un indicador por estado con su cantidad; al hacer clic filtra por ese estado.</summary>
        private void CrearIndicadores()
        {
            flpResumen.SuspendLayout();
            foreach (var tile in _tiles) tile.Dispose();
            flpResumen.Controls.Clear();
            _tiles.Clear();

            AgregarIndicador(TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_tileTodas"), null, Color.Goldenrod);
            foreach (EstadoHabitacion estado in Enum.GetValues(typeof(EstadoHabitacion)))
                AgregarIndicador(TextoEstado(estado), estado, EstiloEstadoHabitacion_68SA.Get(estado).Acento);

            foreach (var tile in _tiles)
                tile.Activo = Equals(tile.Valor, _filtroEstado);
            flpResumen.ResumeLayout(true);
        }

        private void AgregarIndicador(string titulo, EstadoHabitacion? estado, Color acento)
        {
            var tile = new TileEstadoHabitacion_68SA { Titulo = titulo, Valor = estado, Acento = acento };
            tile.Seleccionado += (s, e) =>
            {
                _filtroEstado = estado;
                foreach (var otro in _tiles) otro.Activo = ReferenceEquals(otro, tile);
                CargarHabitaciones();
            };
            _tiles.Add(tile);
            flpResumen.Controls.Add(tile);
        }

        private void ActualizarIndicadores(List<Habitacion_68SA> habitaciones)
        {
            foreach (var tile in _tiles)
                tile.Cantidad = tile.Valor is EstadoHabitacion estado
                    ? habitaciones.Count(h => h.EstadoVisual == estado)
                    : habitaciones.Count;
        }

        private void CargarChipsTipo()
        {
            _armando = true;
            try
            {
                flpChipsTipo.Controls.Clear();

                var chipTodos = CrearChip(TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_chipTodos"), () => _filtroTipoId = null);
                chipTodos.Checked = !_filtroTipoId.HasValue;
                flpChipsTipo.Controls.Add(chipTodos);

                foreach (var tipo in _tipoBLL.GetAll())
                {
                    int id = tipo.Id;
                    var chip = CrearChip(tipo.Nombre, () => _filtroTipoId = id);
                    chip.Checked = _filtroTipoId == id;
                    flpChipsTipo.Controls.Add(chip);
                }
            }
            finally
            {
                _armando = false;
            }
        }

        private RadioButton CrearChip(string texto, Action alSeleccionar)
        {
            var chip = new RadioButton
            {
                Appearance = Appearance.Button,
                AutoSize = true,
                Text = texto,
                Padding = new Padding(12, 4, 12, 4),
                Margin = new Padding(0, 9, 8, 6),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(12, 22, 56),
                ForeColor = Color.FromArgb(200, 200, 210)
            };
            chip.FlatAppearance.BorderColor = Color.FromArgb(45, 60, 100);
            chip.FlatAppearance.CheckedBackColor = Color.Goldenrod;
            chip.FlatAppearance.MouseOverBackColor = Color.FromArgb(18, 32, 76);
            chip.CheckedChanged += (s, e) =>
            {
                chip.BackColor = chip.Checked ? Color.Goldenrod : Color.FromArgb(12, 22, 56);
                chip.ForeColor = chip.Checked ? Color.FromArgb(10, 15, 35) : Color.FromArgb(200, 200, 210);

                if (!chip.Checked || _armando) return;
                alSeleccionar();
                CargarHabitaciones();
            };
            return chip;
        }
        #endregion

        #region habitaciones

        private void CargarHabitaciones()
        {
            List<Habitacion_68SA> habitaciones = _modo == ModoHabitaciones.Gestion
                ? _habitacionBLL.GetAll(_filtroTipoId)
                : _habitacionBLL.GetDisponibles(_fechaIngreso.Value, _fechaEgreso.Value, _filtroTipoId,
                                                 _adultos + _ninos > 0 ? _adultos + _ninos : (int?)null);

            Dictionary<int, Reserva_68SA> ocupacion = new Dictionary<int, Reserva_68SA>();
            if (_modo == ModoHabitaciones.Gestion)
            {
                ActualizarIndicadores(habitaciones);
                if (_filtroEstado.HasValue)
                    habitaciones = habitaciones.Where(h => h.EstadoVisual == _filtroEstado.Value).ToList();
                ocupacion = ReservasDeHoy();
            }

            flpHabitaciones.SuspendLayout();
            foreach (Control anterior in flpHabitaciones.Controls.Cast<Control>().ToList())
                anterior.Dispose();
            flpHabitaciones.Controls.Clear();

            if (habitaciones.Count == 0)
                flpHabitaciones.Controls.Add(new Label
                {
                    AutoSize = true,
                    Text = TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_txtSinHabitaciones"),
                    ForeColor = Color.FromArgb(160, 165, 180),
                    Font = new Font("Segoe UI", 10.5F),
                    Margin = new Padding(4, 24, 4, 4)
                });

            foreach (var grupoPiso in habitaciones.GroupBy(h => h.Piso.Numero).OrderBy(g => g.Key))
            {
                var piso = grupoPiso.First().Piso;
                flpHabitaciones.Controls.Add(CrearEncabezadoPiso(piso, grupoPiso.Count()));

                var flpCardsPiso = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Margin = new Padding(0, 0, 0, 6),
                    Tag = MarcaFilaPiso
                };

                foreach (var habitacion in grupoPiso.OrderBy(h => h.NroHabitacion))
                {
                    var card = new UC_HabitacionCard_68SA();
                    ocupacion.TryGetValue(habitacion.Id, out var reservaActual);
                    card.Cargar(habitacion, forzarDisponible: _modo == ModoHabitaciones.Reserva, reservaActual: reservaActual);
                    card.HabitacionSeleccionada += Card_HabitacionSeleccionada;
                    flpCardsPiso.Controls.Add(card);
                }

                flpHabitaciones.Controls.Add(flpCardsPiso);
            }

            AjustarAnchoPisos();
            flpHabitaciones.ResumeLayout(true);
        }

        /// <summary>Título de cada piso: "PISO 2 · Segundo piso" y la cantidad de habitaciones, con una línea debajo.</summary>
        private Control CrearEncabezadoPiso(Piso_68SA piso, int cantidad)
        {
            var t = TraductorManager_08YS.Instance;
            var encabezado = new Panel { Height = 40, Margin = new Padding(0, 14, 0, 4), Tag = MarcaFilaPiso, BackColor = flpHabitaciones.BackColor };

            string nombre = string.IsNullOrWhiteSpace(piso.Nombre) ? "" : "  ·  " + piso.Nombre;
            encabezado.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                Text = $"{t.GetTexto("ControlHabitaciones_piso")} {piso.Numero}{nombre}",
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(2, 0, 0, 0)
            });
            encabezado.Controls.Add(new Label
            {
                Dock = DockStyle.Right,
                Width = 220,
                Text = string.Format(t.GetTexto("ControlHabitaciones_txtCantidadPiso"), cantidad),
                ForeColor = Color.FromArgb(160, 165, 180),
                Font = new Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleRight
            });
            encabezado.Controls.Add(new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = Color.FromArgb(45, 60, 100) });
            return encabezado;
        }

        /// <summary>
        /// Quién está en cada habitación hoy (para mostrarlo en la tarjeta): la estadía en curso o, si todavía no
        /// llegó, la reserva confirmada que ingresa hoy. Si falla la consulta, las tarjetas se muestran igual.
        /// </summary>
        private Dictionary<int, Reserva_68SA> ReservasDeHoy()
        {
            try
            {
                DateTime hoy = DateTime.Today;
                return _reservaBLL.GetEnRangoVisible(hoy.AddDays(-1), hoy.AddDays(1))
                    .Where(r => r.Estado == EstadoReserva.EnCurso
                             || (r.Estado == EstadoReserva.Confirmada && r.FechaIngreso.Date <= hoy && r.FechaEgreso.Date > hoy))
                    .GroupBy(r => r.Habitacion.Id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(r => r.Estado == EstadoReserva.EnCurso ? 0 : 1).First());
            }
            catch (Exception)
            {
                return new Dictionary<int, Reserva_68SA>();
            }
        }

        // Marca (no texto) para que TraducirControles no la tome como clave de traducción
        private static readonly object MarcaFilaPiso = new object();
        private bool _ajustandoPisos;

        /// <summary>
        /// Cada fila de tarjetas ocupa exactamente el ancho visible: con un tope de ancho, el FlowLayoutPanel
        /// autoajustable pasa las tarjetas a la fila siguiente en lugar de crecer hacia la derecha.
        /// </summary>
        private void AjustarAnchoPisos()
        {
            if (_ajustandoPisos) return;
            _ajustandoPisos = true;
            try
            {
                int ancho = Math.Max(200, flpHabitaciones.ClientSize.Width - flpHabitaciones.Padding.Horizontal - 4);
                foreach (Control fila in flpHabitaciones.Controls)
                {
                    if (!ReferenceEquals(fila.Tag, MarcaFilaPiso)) continue;
                    var tamano = new Size(ancho, 0);
                    if (fila.MaximumSize != tamano) fila.MaximumSize = tamano;
                    if (fila.MinimumSize != tamano) fila.MinimumSize = tamano;
                }
            }
            finally
            {
                _ajustandoPisos = false;
            }
        }

        private void Card_HabitacionSeleccionada(object sender, Habitacion_68SA habitacion)
        {
            if (_modo == ModoHabitaciones.Reserva)
            {
                _openChildForm(new FormRegistrarReserva_68SA(_openChildForm, habitacion, _modo, _fechaIngreso, _fechaEgreso, _adultos, _ninos));
                return;
            }

            MostrarPopupAcciones(habitacion);
        }

        #endregion

        #region Idioma

        private void AplicarTextos()
        {
            var t = TraductorManager_08YS.Instance;
            if (_modo == ModoHabitaciones.Reserva)
            {
                lblTitulo.Text = t.GetTexto("ControlHabitaciones_tituloReserva");
                int noches = (_fechaEgreso.Value.Date - _fechaIngreso.Value.Date).Days;
                lblSubtitulo.Text = string.Format(t.GetTexto("ControlHabitaciones_txtRangoFechas"), _fechaIngreso, _fechaEgreso)
                                    + "  ·  " + string.Format(t.GetTexto("ControlHabitaciones_txtNoches"), noches)
                                    + (_adultos + _ninos > 0 ? "  ·  " + string.Format(t.GetTexto("ControlHabitaciones_txtHuespedes"), _adultos + _ninos) : string.Empty);
                if (_btnCambiarFechas != null) _btnCambiarFechas.Text = " " + t.GetTexto("ControlHabitaciones_btnCambiarFechas");
                Text = $"{lblTitulo.Text} — {lblSubtitulo.Text}";
            }
            else
            {
                lblTitulo.Text = t.GetTexto("ControlHabitaciones_tituloGestion");
                string hoy = DateTime.Today.ToString(t.GetTexto("ControlHabitaciones_formatoHoy"), OpcionEnum_68SA.CulturaActual());
                lblSubtitulo.Text = hoy.Length > 0 ? char.ToUpper(hoy[0]) + hoy.Substring(1) : hoy;
                Text = lblTitulo.Text;
            }
        }

        public void UpdateIdioma()
        {
            TraducirControles(this);
            AplicarTextos();

            // Los chips, los indicadores y las tarjetas se generan en runtime (no pasan por el mecanismo de Tag),
            // así que hay que reconstruirlos para que reflejen el idioma nuevo (se conservan los filtros elegidos).
            CargarChipsTipo();
            if (_modo == ModoHabitaciones.Gestion)
                CrearIndicadores();
            CargarHabitaciones();
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

        #region Métodos auxiliares

        /// <summary>Nueva reserva: vuelve al paso de fechas y huéspedes con lo que se había elegido.</summary>
        private void CrearBotonCambiarFechas()
        {
            _btnCambiarFechas = new FontAwesome.Sharp.IconButton
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = btnActualizar.BackColor,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = btnActualizar.Font,
                ForeColor = btnActualizar.ForeColor,
                IconChar = FontAwesome.Sharp.IconChar.CalendarDays,
                IconColor = btnActualizar.IconColor,
                IconFont = FontAwesome.Sharp.IconFont.Auto,
                IconSize = 22,
                ImageAlign = ContentAlignment.MiddleLeft,
                Name = "btnCambiarFechas",
                Padding = new Padding(12, 0, 0, 0),
                Size = new Size(210, btnActualizar.Height),
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            _btnCambiarFechas.FlatAppearance.BorderColor = btnActualizar.FlatAppearance.BorderColor;
            _btnCambiarFechas.Location = new Point(btnActualizar.Left - _btnCambiarFechas.Width - 12, btnActualizar.Top);
            _btnCambiarFechas.Click += (s, e) => _openChildForm(new FormDisponibilidad(_openChildForm, _fechaIngreso, _fechaEgreso,
                                                                                     _adultos, _ninos, _filtroTipoId));
            btnActualizar.Parent.Controls.Add(_btnCambiarFechas);
            _btnCambiarFechas.BringToFront();
        }

        private void MostrarPopupAcciones(Habitacion_68SA habitacion)
        {
            var acciones = new List<(string Texto, IconChar Icono, AccionHabitacion Accion)>();

            switch (habitacion.EstadoVisual)
            {
                case EstadoHabitacion.Disponible:
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accReservar"), IconChar.CalendarPlus, AccionHabitacion.Reservar));
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accCheckInDirecto"), IconChar.DoorOpen, AccionHabitacion.CheckInDirecto));
                    break;
                case EstadoHabitacion.Reservada:
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accCheckIn"), IconChar.CalendarCheck, AccionHabitacion.CheckIn));
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accVerReserva"), IconChar.Eye, AccionHabitacion.VerReserva));
                    break;
                case EstadoHabitacion.Ocupada:
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accVerEstadia"), IconChar.Eye, AccionHabitacion.VerEstadia));
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accCheckOut"), IconChar.CalendarTimes, AccionHabitacion.CheckOut));
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accServicio"), IconChar.Bell, AccionHabitacion.Servicio));
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accCambioHabitacion"), IconChar.Retweet, AccionHabitacion.CambioHabitacion));
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accRenovarEstadia"), IconChar.ArrowRotateRight, AccionHabitacion.Renovar));
                    break;
                case EstadoHabitacion.EnLimpieza:
                case EstadoHabitacion.FueraDeServicio:
                    acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accMarcarDisponible"), IconChar.CircleCheck, AccionHabitacion.MarcarDisponible));
                    break;
            }

            if (habitacion.EstadoVisual != EstadoHabitacion.FueraDeServicio)
                acciones.Add((TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_accPonerEnMantenimiento"), IconChar.Wrench, AccionHabitacion.PonerEnMantenimiento));

            // Solo se ofrecen las acciones para las que el usuario tiene permiso
            acciones.RemoveAll(a => _mapaPermisosAcciones.TryGetValue(a.Accion, out var permiso)
                                    && !SessionManager_08YS.Instance.HasPermission(permiso));

            // El cambio de habitación sirve con cualquiera de sus dos permisos (upgrade o reubicación)
            if (!SessionManager_08YS.Instance.HasPermission(Permisos.RegistrarUpgradeHabitacion)
                && !SessionManager_08YS.Instance.HasPermission(Permisos.ReubicarHuesped))
                acciones.RemoveAll(a => a.Accion == AccionHabitacion.CambioHabitacion);

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
                    _openChildForm(new FormRegistrarReserva_68SA(_openChildForm, habitacion, _modo, _fechaIngreso, _fechaEgreso, _adultos, _ninos));
                    break;

                case AccionHabitacion.CheckIn:
                    var reservaDeHoy = BLLFactory_08YS.CreateReservaBLL().GetConfirmadaHoyPorHabitacion(habitacion.Id);
                    if (reservaDeHoy == null)
                    {
                        MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_msgSinReservaHoy"),
                            TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_tituloSinReserva"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    _openChildForm(new FormCheckIn_68SA(_openChildForm, reservaDeHoy, _modo, _fechaIngreso, _fechaEgreso));
                    break;

                case AccionHabitacion.VerReserva:
                    var reservaParaVer = BLLFactory_08YS.CreateReservaBLL().GetConfirmadaHoyPorHabitacion(habitacion.Id);
                    if (reservaParaVer == null)
                    {
                        MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_msgSinReservaHoy"),
                            TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_tituloSinReserva"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    using (var popupModificar = new FormModificarReserva_68SA(reservaParaVer))
                    {
                        if (popupModificar.ShowDialog(this) == DialogResult.OK)
                            CargarHabitaciones();
                    }
                    break;

                case AccionHabitacion.CheckOut:
                    var reservaEnCurso = BLLFactory_08YS.CreateReservaBLL().GetEnCursoPorHabitacion(habitacion.Id);
                    if (reservaEnCurso == null)
                    {
                        MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_msgSinReservaEnCurso"),
                            TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_tituloSinReserva"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    _openChildForm(new FormCheckOut_68SA(_openChildForm, reservaEnCurso, _modo, _fechaIngreso, _fechaEgreso));
                    break;

                case AccionHabitacion.VerEstadia:
                    var reservaDetalle = ObtenerReservaEnCurso(habitacion);
                    if (reservaDetalle == null) break;
                    using (var popupDetalle = new FormDetalleEstadia_68SA(reservaDetalle.Id))
                        popupDetalle.ShowDialog(this);
                    break;

                case AccionHabitacion.Servicio:
                    var reservaServicio = ObtenerReservaEnCurso(habitacion);
                    if (reservaServicio == null) break;
                    using (var popupServicio = new FormPedidoRoomService_68SA(reservaServicio))
                        popupServicio.ShowDialog(this);
                    break;

                case AccionHabitacion.CambioHabitacion:
                    var reservaCambio = ObtenerReservaEnCurso(habitacion);
                    if (reservaCambio == null) break;
                    using (var popupCambio = new FormCambioHabitacion_68SA(reservaCambio))
                    {
                        if (popupCambio.ShowDialog(this) == DialogResult.OK)
                            CargarHabitaciones();
                    }
                    break;

                case AccionHabitacion.PonerEnMantenimiento when habitacion.EstadoVisual == EstadoHabitacion.Ocupada
                                                             || habitacion.EstadoVisual == EstadoHabitacion.Reservada:
                    PonerEnMantenimientoConReserva(habitacion);
                    break;

                case AccionHabitacion.PonerEnMantenimiento:
                    _habitacionBLL.CambiarEstado(habitacion.Id, EstadoHabitacion.FueraDeServicio);
                    CargarHabitaciones();
                    break;

                case AccionHabitacion.Renovar:
                    var reservaParaRenovar = BLLFactory_08YS.CreateReservaBLL().GetEnCursoPorHabitacion(habitacion.Id);
                    if (reservaParaRenovar == null)
                    {
                        MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_msgSinReservaEnCurso"),
                            TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_tituloSinReserva"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    using (var popupRenovar = new FormRenovarEstadia_68SA(reservaParaRenovar, habitacion))
                    {
                        if (popupRenovar.ShowDialog(this) == DialogResult.OK)
                            CargarHabitaciones();
                    }
                    break;

                default:
                    MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_msgAccionPendiente"), accion),
                        TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_tituloPendiente"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private Reserva_68SA ObtenerReservaEnCurso(Habitacion_68SA habitacion)
        {
            var reserva = BLLFactory_08YS.CreateReservaBLL().GetEnCursoPorHabitacion(habitacion.Id);
            if (reserva == null)
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_msgSinReservaEnCurso"),
                    TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_tituloSinReserva"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return reserva;
        }

        /// <summary>
        /// PN2: una habitación no puede quedar fuera de servicio con un huésped adentro (Ocupada) ni con una reserva
        /// que ingresa hoy (Reservada). Primero se reubica la estadía / la reserva (cambio administrativo, sin cargo)
        /// y recién después la habitación pasa a mantenimiento.
        /// </summary>
        private void PonerEnMantenimientoConReserva(Habitacion_68SA habitacion)
        {
            var t = TraductorManager_08YS.Instance;
            bool ocupada = habitacion.EstadoVisual == EstadoHabitacion.Ocupada;
            Reserva_68SA reserva;
            if (ocupada)
                reserva = ObtenerReservaEnCurso(habitacion);
            else
            {
                reserva = BLLFactory_08YS.CreateReservaBLL().GetConfirmadaHoyPorHabitacion(habitacion.Id);
                if (reserva == null)
                    MessageBox.Show(t.GetTexto("ControlHabitaciones_msgSinReservaHoy"),
                        t.GetTexto("ControlHabitaciones_tituloSinReserva"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (reserva == null) return;

            if (!BLLFactory_08YS.CreateCambioHabitacionBLL().PuedeRegistrar(MotivoCambioHabitacion.Administrativo))
            {
                MessageBox.Show(t.GetTexto("ControlHabitaciones_msgMantenimientoSinReubicar"),
                    t.GetTexto("ControlHabitaciones_tituloReservaActiva"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                string.Format(t.GetTexto(ocupada ? "ControlHabitaciones_msgMantenimientoOcupada" : "ControlHabitaciones_msgMantenimientoReservada"),
                    habitacion.NroHabitacion, $"{reserva.Titular.Nombre} {reserva.Titular.Apellido}"),
                t.GetTexto("ControlHabitaciones_tituloReservaActiva"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes) return;

            string motivo = string.Format(t.GetTexto("ControlHabitaciones_txtMotivoMantenimiento"), habitacion.NroHabitacion);
            using (var popup = new FormCambioHabitacion_68SA(reserva, MotivoCambioHabitacion.Administrativo, motivo))
            {
                if (popup.ShowDialog(this) != DialogResult.OK)
                {
                    CargarHabitaciones();
                    return;
                }
            }

            // La estadía / reserva ya está en otra habitación: esta pasa a mantenimiento
            _habitacionBLL.CambiarEstado(habitacion.Id, EstadoHabitacion.FueraDeServicio);
            CargarHabitaciones();
        }

        private static string TextoEstado(EstadoHabitacion estado)
        {
            switch (estado)
            {
                case EstadoHabitacion.Disponible: return TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_estadoDisponible");
                case EstadoHabitacion.Ocupada: return TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_estadoOcupada");
                case EstadoHabitacion.Reservada: return TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_estadoReservada");
                case EstadoHabitacion.EnLimpieza: return TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_estadoLimpieza");
                case EstadoHabitacion.FueraDeServicio: return TraductorManager_08YS.Instance.GetTexto("ControlHabitaciones_estadoFueraServicio");
                default: return estado.ToString();
            }
        }

        #endregion

        #region Sobrescrituras

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