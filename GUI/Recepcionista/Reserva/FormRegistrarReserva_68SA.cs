using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using Service_08YS;
using System;
using System.Linq;
using System.Windows.Forms;
using Service_08YS.Entities.Acceso;
using System.Collections.Generic;
using System.Drawing;
using GUI_08YS.UserControls;
namespace GUI_08YS.Recepcionista
{
    public partial class FormRegistrarReserva_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private static readonly Dictionary<string, Permisos> _mapaPermisos =
            new Dictionary<string, Permisos>
            {
                { nameof(btnConfirmar),    Permisos.RegistrarReserva },
                { nameof(btnHuespedNuevo), Permisos.CrearHuesped },
            };

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

        // Huéspedes elegidos en el paso de fechas de una nueva reserva (0 = sin indicar)
        private readonly int _adultosOrigen, _ninosOrigen;

        #endregion

        #region Constructor

        public FormRegistrarReserva_68SA(Action<Form> openChildForm, Habitacion_68SA habitacion, ModoHabitaciones modoOrigen,
                                  DateTime? fechaIngreso = null, DateTime? fechaEgreso = null, int adultos = 0, int ninos = 0)
        {
            _adultosOrigen = Math.Max(0, adultos);
            _ninosOrigen = Math.Max(0, ninos);
            _openChildForm = openChildForm;
            _habitacion = habitacion;
            _modoOrigen = modoOrigen;
            _fechaIngresoOrigen = fechaIngreso;
            _fechaEgresoOrigen = fechaEgreso;

            InitializeComponent();

            PermissionFilter_08YS.Aplicar(this, _mapaPermisos);
            // Confirmar incluye el cobro (CUN-03): hacen falta ambos permisos
            btnConfirmar.Visible = SessionManager_08YS.Instance.HasPermission(Permisos.RegistrarReserva)
                                && SessionManager_08YS.Instance.HasPermission(Permisos.Cobrar);

            // Las tarjetas reparten sus campos en dos columnas que se estiran con el ancho de la pantalla
            LayoutAdaptable_68SA.RepartirColumnas(pnlResumen,
                new Control[] { lblEtiquetaFechaIngreso, dtpFechaIngreso, lblEtiquetaAdultos, nudAdultos },
                new Control[] { lblEtiquetaFechaEgreso, dtpFechaEgreso, lblEtiquetaNinos, nudNinos });
            LayoutAdaptable_68SA.RepartirColumnas(pnlIzquierda,
                new Control[] { lblEtiquetaNombre, txtNombre, lblEtiquetaFechaNacimiento, dtpFechaNacimiento, lblEtiquetaTelefono, txtTelefono },
                new Control[] { lblEtiquetaApellido, txtApellido, lblEtiquetaNacionalidad, txtNacionalidad, lblEtiquetaEmail, txtEmail });
            LayoutAdaptable_68SA.RepartirColumnas(pnlPago,
                new Control[] { lblEtiquetaMetodoPago, cmbMetodoPago, lblEtiquetaNumeroTarjeta, txtNumeroTarjeta },
                new Control[] { lblEtiquetaMontoRecibido, nudMontoRecibido, lblMarcaTarjetaDetectada, lblVueltoEtiqueta, lblVuelto,
                                lblEtiquetaTitularTarjeta, txtTitularTarjeta, lblEtiquetaCvv, txtCvv });
            LayoutAdaptable_68SA.RepartirColumnas(pnlDetalles,
                new Control[] { lblDetEtiquetaPiso, lblDetPiso }, new Control[] { lblDetEtiquetaCapacidad, lblDetCapacidad },
                new Control[] { lblDetEtiquetaPrecio, lblDetPrecio });

            // Tarjetas en dos columnas o apiladas según el ancho; barra vertical solo si no entran a lo alto
            LayoutAdaptable_68SA.OcultarScrollHorizontal(pnlCuerpo);
            pnlCuerpo.Layout += (s, e) => AcomodarTarjetas();
            pnlIzquierda.Layout += (s, e) => AcomodarFilaDocumento();
            pnlEncabezado.Resize += (s, e) => pasosReserva.Width = Math.Max(300, Math.Min(520, pnlEncabezado.Width * 40 / 100));
            pasosReserva.PasoActual = 2;
            txtDni.InnerTextBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                BtnConsultarDocumento_Click(s, EventArgs.Empty);
            };

            lblDetTipo.Text = habitacion.Tipo.Nombre;
            lblDetDescripcion.Text = string.IsNullOrEmpty(habitacion.Tipo.Descripcion) ? "-" : habitacion.Tipo.Descripcion;
            lblDetPiso.Text = habitacion.Piso.Numero.ToString();
            lblDetPrecio.Text = $"${habitacion.Tipo.TarifaNoche:N2}";
            lblDetCapacidad.Text = habitacion.Tipo.Capacidad.ToString();

            lblResHabitacion.Text = habitacion.NroHabitacion;

            nudAdultos.Maximum = habitacion.Tipo.Capacidad;
            nudNinos.Maximum = habitacion.Tipo.Capacidad;
            if (_adultosOrigen > 0)
                nudAdultos.Value = Math.Max(nudAdultos.Minimum, Math.Min(nudAdultos.Maximum, _adultosOrigen));
            if (_ninosOrigen > 0)
                nudNinos.Value = Math.Max(nudNinos.Minimum, Math.Min(nudNinos.Maximum, _ninosOrigen));

            OpcionEnum_68SA.Cargar(cmbTipoDocumento, typeof(TipoDocumento));
            cmbTipoDocumento.SelectedIndex = 0;

            OpcionEnum_68SA.Cargar(cmbMetodoPago, typeof(MetodoPago));
            cmbMetodoPago.SelectedIndex = 0;
            TraducirPlaceholders();
            int anioActual = DateTime.Today.Year % 100;
            nudAnioVencimiento.Minimum = anioActual;
            nudAnioVencimiento.Maximum = anioActual + 20;
            nudAnioVencimiento.Value = anioActual;

            txtNumeroTarjeta.TextChanged += (s, e) => ActualizarMarcaDetectada();
            cmbMetodoPago.SelectedIndexChanged += (s, e) => ActualizarVisibilidadVuelto();
            ActualizarVisibilidadVuelto();

            toolTip1.SetToolTip(btnHuespedNuevo, TraductorManager_08YS.Instance.GetTexto("Comun_tooltipRegistrarHuespedNuevo"));

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
            nudMontoRecibido.ValueChanged += (s, e) => ActualizarVuelto();
            btnConfirmar.Click += BtnConfirmar_Click;
            btnCancelar.Click += (s, e) => Volver();
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            TraducirControles(this);
            TraducirPlaceholders();
            OpcionEnum_68SA.Cargar(cmbTipoDocumento, typeof(TipoDocumento));
            OpcionEnum_68SA.Cargar(cmbMetodoPago, typeof(MetodoPago));
            ActualizarMarcaDetectada();
            ValidarComposicion();
            if (dtpFechaIngreso.Value.HasValue && dtpFechaEgreso.Value.HasValue)
            {
                int noches = (dtpFechaEgreso.Value.Value.Date - dtpFechaIngreso.Value.Value.Date).Days;
                lblResNoches.Text = $"{noches} {TraductorManager_08YS.Instance.GetTexto(noches == 1 ? "RegistrarReserva_txtNoche" : "RegistrarReserva_txtNoches")}";
            }
            toolTip1.SetToolTip(btnHuespedNuevo, TraductorManager_08YS.Instance.GetTexto("Comun_tooltipRegistrarHuespedNuevo"));
            pasosReserva.Pasos = new[] { TraductorManager_08YS.Instance.GetTexto("Disponibilidad_paso1"),
                                         TraductorManager_08YS.Instance.GetTexto("Disponibilidad_paso2"),
                                         TraductorManager_08YS.Instance.GetTexto("Disponibilidad_paso3") };
            ActualizarSubtitulo();
            ActualizarAvisoConfirmar();
            AcomodarFilaDocumento();   // el ancho de "Consultar" depende del idioma
        }

        private void TraducirPlaceholders()
        {
            var t = TraductorManager_08YS.Instance;
            string seCompleta = t.GetTexto("RegistrarReserva_phSeCompletaAlConsultar");
            txtDni.PlaceholderText = t.GetTexto("RegistrarReserva_lblEtiquetaDni");
            txtNombre.PlaceholderText = seCompleta;
            txtApellido.PlaceholderText = seCompleta;
            txtTelefono.PlaceholderText = seCompleta;
            txtNacionalidad.PlaceholderText = seCompleta;
            txtEmail.PlaceholderText = seCompleta;
            txtTitularTarjeta.PlaceholderText = t.GetTexto("RegistrarReserva_phTitularTarjeta");
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

        #region Carga de datos

        /// <summary>Encabezado: habitación, fechas y noches de la reserva que se está registrando.</summary>
        private void ActualizarSubtitulo()
        {
            if (!dtpFechaIngreso.Value.HasValue || !dtpFechaEgreso.Value.HasValue) return;
            var cultura = OpcionEnum_68SA.CulturaActual();
            DateTime ingreso = dtpFechaIngreso.Value.Value.Date, egreso = dtpFechaEgreso.Value.Value.Date;
            lblSubtitulo.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_txtSubtitulo"),
                _habitacion.NroHabitacion, _habitacion.Tipo.Nombre,
                ingreso.ToString("ddd d MMM", cultura), egreso.ToString("ddd d MMM yyyy", cultura), (egreso - ingreso).Days);
        }

        private void ActualizarAvisoConfirmar()
        {
            lblAvisoConfirmar.Text = _huespedSeleccionado == null
                ? TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_avisoFaltaTitular")
                : string.Empty;
        }

        private void ActualizarVisibilidadVuelto()
        {
            bool esEfectivo = OpcionEnum_68SA.Seleccionado<MetodoPago>(cmbMetodoPago) == MetodoPago.Efectivo;

            lblEtiquetaMontoRecibido.Visible = esEfectivo;
            nudMontoRecibido.Visible = esEfectivo;
            lblVueltoEtiqueta.Visible = esEfectivo;
            lblVuelto.Visible = esEfectivo;

            bool esTarjeta = !esEfectivo;
            lblEtiquetaNumeroTarjeta.Visible = esTarjeta;
            txtNumeroTarjeta.Visible = esTarjeta;
            lblEtiquetaTitularTarjeta.Visible = esTarjeta;
            txtTitularTarjeta.Visible = esTarjeta;
            lblEtiquetaVencimiento.Visible = esTarjeta;
            nudMesVencimiento.Visible = esTarjeta;
            lblBarraVencimiento.Visible = esTarjeta;
            nudAnioVencimiento.Visible = esTarjeta;
            lblEtiquetaCvv.Visible = esTarjeta;
            txtCvv.Visible = esTarjeta;
            lblMarcaTarjetaDetectada.Visible = esTarjeta;

            pnlPago.Height = esTarjeta ? 316 : 222;
        }

        private void ActualizarMarcaDetectada()
        {
            string marca = TarjetaValidador_68SA.DetectarMarca(txtNumeroTarjeta.RealText);
            if (marca == "Desconocida")
                marca = TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_txtMarcaDesconocida");
            lblMarcaTarjetaDetectada.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_txtMarcaDetectada"), marca);
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
            ActualizarAvisoConfirmar();
        }

        private void ActualizarVuelto()
        {
            decimal vuelto = nudMontoRecibido.Value - _montoTotal;
            lblVuelto.Text = $"${Math.Max(0, vuelto):N2}";
        }

        #endregion

        #region Eventos de controles

        private void BtnConsultarDocumento_Click(object sender, EventArgs e)
        {
            string dni = txtDni.RealText.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_msgIngreseDni"), TraductorManager_08YS.Instance.GetTexto("Comun_msgDatosIncompletos"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var huesped = _huespedBLL.GetByDocumento(dni);
            if (huesped == null)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Comun_msgHuespedNoEncontrado"),
                    TraductorManager_08YS.Instance.GetTexto("Comun_tituloHuespedNoEncontrado"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CargarHuesped(huesped);
        }

        private void BtnHuespedNuevo_Click(object sender, EventArgs e)
        {
            using (var formNuevo = new FormRegistrarHuesped_68SA(txtDni.RealText.Trim()))
            {
                if (formNuevo.ShowDialog(this) == DialogResult.OK)
                    CargarHuesped(formNuevo.HuespedCreado);
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            // Se valida el cobro antes de crear la reserva: si no, quedaría una reserva sin su pago
            SessionManager_08YS.Instance.ValidatePermission(Permisos.RegistrarReserva);
            SessionManager_08YS.Instance.ValidatePermission(Permisos.Cobrar);

            if (_huespedSeleccionado == null)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_msgFaltaTitular"), TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_tituloFaltaTitular"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((int)nudAdultos.Value + (int)nudNinos.Value > _habitacion.Tipo.Capacidad)
            {
                MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_msgCapacidadExcedida"), _habitacion.Tipo.Capacidad),
                    TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_tituloCapacidadExcedida"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var metodoSeleccionado = OpcionEnum_68SA.Seleccionado<MetodoPago>(cmbMetodoPago) ?? MetodoPago.Efectivo;
            if (metodoSeleccionado == MetodoPago.Efectivo)
            {
                if (nudMontoRecibido.Value < _montoTotal)
                {
                    MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("Comun_msgPagoInsuficiente"), nudMontoRecibido.Value, _montoTotal),
                        TraductorManager_08YS.Instance.GetTexto("Comun_tituloPagoInsuficiente"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else // Tarjeta
            {
                if (!TarjetaValidador_68SA.EsNumeroValido(txtNumeroTarjeta.RealText))
                {
                    MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_msgTarjetaNumeroInvalido"),
                        TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_tituloTarjetaInvalida"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTitularTarjeta.RealText))
                {
                    MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_msgTarjetaFaltaTitular"),
                        TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_tituloTarjetaInvalida"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!TarjetaValidador_68SA.EsVencimientoValido((int)nudMesVencimiento.Value, (int)nudAnioVencimiento.Value, out string errorVencimiento))
                {
                    string clave = errorVencimiento == "vencExpirado" ? "RegistrarReserva_msgTarjetaVencida" : "RegistrarReserva_msgTarjetaVencimientoInvalido";
                    MessageBox.Show(TraductorManager_08YS.Instance.GetTexto(clave),
                        TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_tituloTarjetaInvalida"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string marcaDetectada = TarjetaValidador_68SA.DetectarMarca(txtNumeroTarjeta.RealText);
                if (!TarjetaValidador_68SA.EsCvvValido(txtCvv.RealText, marcaDetectada))
                {
                    MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_msgTarjetaCvvInvalido"),
                            TarjetaValidador_68SA.LongitudCvvEsperada(marcaDetectada)),
                        TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_tituloTarjetaInvalida"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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

                // Se registra lo que se cobra (el total), no lo que entregó el huésped: el vuelto no es un ingreso
                decimal montoACobrar = _montoTotal;

                _pagoBLL.Registrar(new Pago_68SA
                {
                    ReservaId = reservaId,
                    Monto = montoACobrar,
                    MetodoPago = metodoSeleccionado
                });

                MostrarComprobante(reservaId, reserva.Titular ?? _huespedSeleccionado);
                Volver();
            }
            catch (TitularMenorDeEdadException_68SA)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Comun_excTitularMenorDeEdad"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloNoSePudoConfirmar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HabitacionNoDisponibleException_68SA)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Comun_excHabitacionNoDisponible"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloNoSePudoConfirmar"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Volver();
            }
            catch (RangoFechasInvalidoException_68SA)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Comun_excRangoFechasInvalido"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloNoSePudoConfirmar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Métodos auxiliares

        private const int SeparacionTarjetas = 16;
        private const int AnchoDosColumnas = 1100;    // por debajo, las tarjetas van una debajo de la otra
        private const int AltoMinimoResumen = 470;    // fechas, huéspedes, advertencia y total sin superponerse
        private bool _acomodandoTarjetas;

        /// <summary>
        /// Ubica las cuatro tarjetas. Con espacio: titular y pago a la izquierda (58%), habitación y estadía
        /// a la derecha. En pantallas angostas: una debajo de la otra en el orden en que se completan
        /// (titular, habitación, estadía, pago). Si no entran a lo alto aparece solo la barra vertical.
        /// </summary>
        private void AcomodarTarjetas()
        {
            if (_acomodandoTarjetas) return;
            _acomodandoTarjetas = true;
            Size cliente = pnlCuerpo.ClientSize;
            pnlCuerpo.SuspendLayout();
            try
            {
                int ancho = pnlCuerpo.ClientSize.Width;
                if (ancho <= 0) return;
                int y0 = pnlCuerpo.AutoScrollPosition.Y + 10;   // negativo cuando el cuerpo está desplazado
                int alto;

                if (ancho >= AnchoDosColumnas)
                {
                    int izquierda = (ancho - SeparacionTarjetas) * 58 / 100;
                    int derecha = ancho - SeparacionTarjetas - izquierda;
                    int altoIzquierda = pnlIzquierda.Height + SeparacionTarjetas + pnlPago.Height;
                    int altoDerecha = Math.Max(pnlDetalles.Height + SeparacionTarjetas + AltoMinimoResumen,
                                               Math.Max(altoIzquierda, pnlCuerpo.ClientSize.Height - 16));

                    pnlIzquierda.Bounds = new Rectangle(0, y0, izquierda, pnlIzquierda.Height);
                    pnlPago.Bounds = new Rectangle(0, y0 + pnlIzquierda.Height + SeparacionTarjetas, izquierda, pnlPago.Height);
                    int x = izquierda + SeparacionTarjetas;
                    pnlDetalles.Bounds = new Rectangle(x, y0, derecha, pnlDetalles.Height);
                    pnlResumen.Bounds = new Rectangle(x, y0 + pnlDetalles.Height + SeparacionTarjetas, derecha,
                                                      altoDerecha - pnlDetalles.Height - SeparacionTarjetas);
                    alto = 10 + Math.Max(altoIzquierda, altoDerecha) + 6;
                }
                else
                {
                    int y = y0;
                    foreach (var (tarjeta, altoTarjeta) in new (Control, int)[]
                    {
                        (pnlIzquierda, pnlIzquierda.Height), (pnlDetalles, pnlDetalles.Height),
                        (pnlResumen, AltoMinimoResumen), (pnlPago, pnlPago.Height)
                    })
                    {
                        tarjeta.Bounds = new Rectangle(0, y, ancho, altoTarjeta);
                        y += altoTarjeta + SeparacionTarjetas;
                    }
                    alto = y - y0 - SeparacionTarjetas + 16;
                }

                var minimo = alto > pnlCuerpo.ClientSize.Height ? new Size(0, alto) : Size.Empty;
                if (pnlCuerpo.AutoScrollMinSize != minimo) pnlCuerpo.AutoScrollMinSize = minimo;
            }
            finally
            {
                pnlCuerpo.ResumeLayout(false);
                _acomodandoTarjetas = false;
            }
            // Si apareció o desapareció la barra vertical cambió el ancho útil: se reparte de nuevo
            if (pnlCuerpo.ClientSize != cliente) AcomodarTarjetas();
        }

        /// <summary>
        /// Fila del documento: tipo, número, "Consultar" y "Huésped nuevo" se reparten el ancho de la tarjeta.
        /// Si el número quedaría muy angosto, "Consultar" muestra solo la lupa (el texto queda en el tooltip).
        /// </summary>
        private void AcomodarFilaDocumento()
        {
            int x = 24, disponible = pnlIzquierda.ClientSize.Width - 48;
            if (disponible <= 0) return;

            string textoConsultar = TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_btnConsultarDocumento");
            int anchoCombo = Math.Max(130, Math.Min(190, disponible * 28 / 100));
            // Sin permiso para registrar huéspedes el botón está oculto: no se le reserva lugar
            bool conNuevo = btnHuespedNuevo.Visible || !pnlIzquierda.Visible;
            int anchoNuevo = conNuevo ? btnHuespedNuevo.Width + 8 : 0;
            int anchoConTexto = TextRenderer.MeasureText(textoConsultar, btnConsultarDocumento.Font).Width + btnConsultarDocumento.IconSize + 44;
            int anchoDni = disponible - anchoCombo - 14 - 12 - anchoConTexto - anchoNuevo;
            bool conTexto = anchoDni >= 150;
            int anchoConsultar = conTexto ? anchoConTexto : 50;
            anchoDni = Math.Max(60, disponible - anchoCombo - 14 - 12 - anchoConsultar - anchoNuevo);

            btnConsultarDocumento.Text = conTexto ? textoConsultar : string.Empty;
            btnConsultarDocumento.Padding = conTexto ? new Padding(12, 0, 0, 0) : Padding.Empty;
            btnConsultarDocumento.ImageAlign = conTexto ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleCenter;
            btnConsultarDocumento.TextImageRelation = conTexto ? TextImageRelation.ImageBeforeText : TextImageRelation.Overlay;
            toolTip1.SetToolTip(btnConsultarDocumento, conTexto ? null : textoConsultar);

            cmbTipoDocumento.Bounds = new Rectangle(x, cmbTipoDocumento.Top, anchoCombo, cmbTipoDocumento.Height);
            x += anchoCombo + 14;
            lblEtiquetaDni.Left = x;
            txtDni.Bounds = new Rectangle(x, txtDni.Top, anchoDni, txtDni.Height);
            x += anchoDni + 12;
            btnConsultarDocumento.Bounds = new Rectangle(x, btnConsultarDocumento.Top, anchoConsultar, btnConsultarDocumento.Height);
            x += anchoConsultar + 8;
            btnHuespedNuevo.Left = x;
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

            lblResNoches.Text = $"{noches} {(noches == 1 ? TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_txtNoche") : TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_txtNoches"))}";
            lblResMontoTotal.Text = $"${_montoTotal:N2}";
            ActualizarSubtitulo();

            nudMontoRecibido.Value = _montoTotal;
            ActualizarVuelto();
        }

        private void ValidarComposicion()
        {
            int total = (int)nudAdultos.Value + (int)nudNinos.Value;
            int capacidad = _habitacion.Tipo.Capacidad;

            lblComposicionAdvertencia.Visible = total > capacidad;
            if (total > capacidad)
                lblComposicionAdvertencia.Text = string.Format(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_msgCapacidadSuperada"), capacidad);
        }

        /// <summary>
        /// Muestra el comprobante y la factura de la reserva recién registrada; el mail al huésped
        /// se envía solo, en segundo plano, mientras se ve el comprobante.
        /// </summary>
        private void MostrarComprobante(int reservaId, Huesped_68SA titular)
        {
            try
            {
                var datos = DatosDocumentoReserva_68SA.Cargar(reservaId, titular);
                using (var visor = new FormDocumentosReserva_68SA(datos, 0, esNuevaReserva: true))
                    visor.ShowDialog(this);
            }
            catch (Exception)
            {
                // La reserva y el pago ya quedaron registrados: si no se pudo armar el comprobante, se avisa como antes
                MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_msgReservaConfirmada"), reservaId, _habitacion.NroHabitacion),
                    TraductorManager_08YS.Instance.GetTexto("RegistrarReserva_tituloReservaConfirmada"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Volver()
            => _openChildForm(new FormControlHabitaciones_68SA(_openChildForm, _modoOrigen, _fechaIngresoOrigen, _fechaEgresoOrigen,
                                                               null,
                                                               _adultosOrigen, _ninosOrigen));

        #endregion

        #region Sobrescrituras

        // Pinta el formulario completo en memoria y lo vuelca de una vez: sin parpadeo ni franjas
        // blancas mientras los controles se acomodan al abrir o redimensionar la ventana.
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
