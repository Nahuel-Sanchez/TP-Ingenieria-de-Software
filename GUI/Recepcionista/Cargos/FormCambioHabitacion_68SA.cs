using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using GUI_08YS.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static GUI_08YS.Recepcionista.UiCargos_68SA;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Cambio de habitación de una reserva (PN2): estadía en curso, o reserva confirmada sin check-in (solo reubicación).
    /// - Upgrade pedido por el huésped: solo habitaciones de tarifa mayor; muestra el cargo antes de confirmar.
    /// - Reubicación por el hotel: cualquier habitación libre, sin cargo, con el motivo obligatorio.
    /// La pantalla solo arma la solicitud y muestra la cotización: las reglas están en CambioHabitacionBLL_68SA.
    /// </summary>
    public partial class FormCambioHabitacion_68SA : Form
    {
        #region Campos

        private readonly CambioHabitacionBLL_68SA _cambioBLL = BLLFactory_08YS.CreateCambioHabitacionBLL();
        private readonly Reserva_68SA _reserva;
        private readonly MotivoCambioHabitacion? _motivoFijo;

        private MotivoCambioHabitacion _motivo;
        private decimal _tarifaVigente;
        private int _nochesRestantes;
        private List<Habitacion_68SA> _opciones = new List<Habitacion_68SA>();
        private CotizacionCambioHabitacion_68SA _cotizacion;

        #endregion

        #region Propiedades

        /// <summary>Lo que devolvió la base al confirmar (monto cobrado, id del cambio).</summary>
        public ResultadoRegistroCambio_68SA Resultado { get; private set; }
        public Habitacion_68SA HabitacionNueva { get; private set; }

        #endregion

        #region Constructor

        /// <param name="reserva">Estadía en curso (con habitación, titular y fechas).</param>
        /// <param name="motivoFijo">Si se indica, no se puede elegir otro motivo (por ejemplo, reubicar por mantenimiento).</param>
        /// <param name="observacionSugerida">Texto inicial del motivo / observación.</param>
        public FormCambioHabitacion_68SA(Reserva_68SA reserva, MotivoCambioHabitacion? motivoFijo = null, string observacionSugerida = null)
        {
            _reserva = reserva ?? throw new ArgumentNullException(nameof(reserva));
            _motivoFijo = motivoFijo;

            InitializeComponent();
            Traducir(this);

            ConfigurarGrilla();
            EstiloChip(rbUpgrade);
            EstiloChip(rbAdministrativo);
            LayoutAdaptable_68SA.HabilitarDobleBuffer(dgvOpciones);

            if (!string.IsNullOrWhiteSpace(observacionSugerida))
                txtObservacion.Text = observacionSugerida;

            CargarEstadia();

            rbUpgrade.CheckedChanged += (s, e) => { if (rbUpgrade.Checked) CambiarMotivo(MotivoCambioHabitacion.Upgrade); };
            rbAdministrativo.CheckedChanged += (s, e) => { if (rbAdministrativo.Checked) CambiarMotivo(MotivoCambioHabitacion.Administrativo); };
            dgvOpciones.SelectionChanged += (s, e) => ActualizarCotizacion();
            dgvOpciones.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Confirmar(); };
            btnConfirmar.Click += (s, e) => Confirmar();

            // En Load (antes de que el formulario tome el foco): un RadioButton se marca solo al recibir el foco
            Load += (s, e) => SeleccionarMotivoInicial();
        }

        #endregion

        #region Carga

        private void CargarEstadia()
        {
            _tarifaVigente = _cambioBLL.GetTarifaVigente(_reserva.Id);
            _nochesRestantes = CambioHabitacionBLL_68SA.CalcularNochesRestantes(_reserva, DateTime.Today);

            string tipo = _reserva.Habitacion.Tipo?.Nombre;
            lblHabitacion.Text = string.IsNullOrEmpty(tipo) ? _reserva.Habitacion.NroHabitacion : $"{_reserva.Habitacion.NroHabitacion} · {tipo}";
            lblTitular.Text = $"{_reserva.Titular.Nombre} {_reserva.Titular.Apellido}";
            lblEgreso.Text = _reserva.FechaEgreso.ToString("dd/MM/yyyy");
            lblNoches.Text = _nochesRestantes.ToString();
            lblTarifaVigente.Text = Moneda(_tarifaVigente);
        }

        private void SeleccionarMotivoInicial()
        {
            // El upgrade es solo con la estadía en curso; antes del check-in solo se puede reubicar
            bool puedeUpgrade = _cambioBLL.PuedeRegistrar(MotivoCambioHabitacion.Upgrade)
                                && _cambioBLL.AdmiteEstado(MotivoCambioHabitacion.Upgrade, _reserva.Estado);
            bool puedeReubicar = _cambioBLL.PuedeRegistrar(MotivoCambioHabitacion.Administrativo)
                                 && _cambioBLL.AdmiteEstado(MotivoCambioHabitacion.Administrativo, _reserva.Estado);

            rbUpgrade.Visible = puedeUpgrade && (_motivoFijo == null || _motivoFijo == MotivoCambioHabitacion.Upgrade);
            rbAdministrativo.Visible = puedeReubicar && (_motivoFijo == null || _motivoFijo == MotivoCambioHabitacion.Administrativo);

            if (!rbUpgrade.Visible && !rbAdministrativo.Visible)
            {
                // Si tiene permiso para algún motivo, lo que lo impide es el estado de la reserva (no el permiso)
                bool tienePermiso = _cambioBLL.PuedeRegistrar(MotivoCambioHabitacion.Upgrade)
                                    || _cambioBLL.PuedeRegistrar(MotivoCambioHabitacion.Administrativo);
                Aviso(this, Describir(tienePermiso ? (Exception)new EstadoReservaInvalidoException_68SA() : new UnauthorizedAccessException()));
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            // El upgrade es el caso principal: se elige primero si está disponible
            if (rbUpgrade.Visible) rbUpgrade.Checked = true;
            else rbAdministrativo.Checked = true;
        }

        /// <summary>La reserva todavía no hizo el check-in: se cambia la habitación de toda la estadía.</summary>
        private bool EsSinCheckIn => _reserva.Estado == EstadoReserva.Confirmada;

        private void CambiarMotivo(MotivoCambioHabitacion motivo)
        {
            _motivo = motivo;
            bool esUpgrade = motivo == MotivoCambioHabitacion.Upgrade;

            txtObservacion.PlaceholderText = T(esUpgrade ? "CambioHab_phObservacionUpgrade" : "CambioHab_phObservacionAdmin");
            lblEtiquetaOpciones.Text = T(esUpgrade ? "CambioHab_lblOpcionesUpgrade"
                                         : EsSinCheckIn ? "CambioHab_lblOpcionesConfirmada" : "CambioHab_lblOpciones");
            iconTitulo.IconChar = esUpgrade ? FontAwesome.Sharp.IconChar.ArrowUpRightDots : FontAwesome.Sharp.IconChar.Retweet;
            CargarOpciones();
        }

        private void CargarOpciones()
        {
            try
            {
                _opciones = _cambioBLL.GetOpciones(_reserva.Id, _motivo);
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                _opciones = new List<Habitacion_68SA>();
                MostrarNota(Describir(ex), ColorError);
            }

            dgvOpciones.DataSource = null;
            dgvOpciones.DataSource = _opciones.Select(h => new FilaOpcion_68SA
            {
                Id = h.Id,
                Habitacion = h.NroHabitacion,
                Piso = string.IsNullOrWhiteSpace(h.Piso?.Nombre) ? h.Piso?.Numero.ToString() : h.Piso.Nombre,
                Tipo = h.Tipo.Nombre,
                Capacidad = h.Tipo.Capacidad,
                Tarifa = h.Tipo.TarifaNoche,
                Diferencia = h.Tipo.TarifaNoche - _tarifaVigente
            }).ToList();
            AplicarColumnas();

            if (_opciones.Count == 0)
            {
                LimpiarCotizacion();
                MostrarNota(T(_motivo == MotivoCambioHabitacion.Upgrade ? "CambioHab_msgSinOpcionesUpgrade" : "CambioHab_msgSinOpciones"), ColorAviso);
                btnConfirmar.Enabled = false;
                return;
            }

            dgvOpciones.ClearSelection();
            dgvOpciones.Rows[0].Selected = true;
            ActualizarCotizacion();
        }

        #endregion

        #region Grilla y cotización

        private sealed class FilaOpcion_68SA
        {
            public int Id { get; set; }
            public string Habitacion { get; set; }
            public string Piso { get; set; }
            public string Tipo { get; set; }
            public int Capacidad { get; set; }
            public decimal Tarifa { get; set; }
            public decimal Diferencia { get; set; }
        }

        private void ConfigurarGrilla()
        {
            CrudEstilo_68SA.AplicarGrilla(dgvOpciones, 38);
            dgvOpciones.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || dgvOpciones.Columns[e.ColumnIndex].Name != nameof(FilaOpcion_68SA.Diferencia) || !(e.Value is decimal dif))
                    return;
                e.Value = dif == 0 ? T("CambioHab_txtMismaTarifa") : (dif > 0 ? "+" : "−") + Moneda(Math.Abs(dif));
                e.CellStyle.ForeColor = dif > 0 ? Color.Goldenrod : TextoSuave;
                e.FormattingApplied = true;
            };
        }

        private void AplicarColumnas()
        {
            var columnas = dgvOpciones.Columns;
            if (columnas.Count == 0) return;
            columnas[nameof(FilaOpcion_68SA.Id)].Visible = false;

            void Col(string nombre, string clave, float peso, string formato = null, bool derecha = false)
            {
                var c = columnas[nombre];
                c.HeaderText = T(clave);
                c.FillWeight = peso;
                if (formato != null) c.DefaultCellStyle.Format = formato;
                if (derecha) c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            Col(nameof(FilaOpcion_68SA.Habitacion), "CambioHab_colHabitacion", 14);
            Col(nameof(FilaOpcion_68SA.Piso), "CambioHab_colPiso", 16);
            Col(nameof(FilaOpcion_68SA.Tipo), "CambioHab_colTipo", 24);
            Col(nameof(FilaOpcion_68SA.Capacidad), "CambioHab_colCapacidad", 12);
            Col(nameof(FilaOpcion_68SA.Tarifa), "CambioHab_colTarifa", 16, "$#,##0.00", true);
            Col(nameof(FilaOpcion_68SA.Diferencia), "CambioHab_colDiferencia", 18, null, true);
        }

        private Habitacion_68SA OpcionSeleccionada()
        {
            if (dgvOpciones.CurrentRow?.DataBoundItem is FilaOpcion_68SA fila)
                return _opciones.FirstOrDefault(h => h.Id == fila.Id);
            return null;
        }

        private void ActualizarCotizacion()
        {
            var habitacion = OpcionSeleccionada();
            _cotizacion = null;
            if (habitacion == null)
            {
                LimpiarCotizacion();
                btnConfirmar.Enabled = false;
                return;
            }

            lblTarifaNueva.Text = Moneda(habitacion.Tipo.TarifaNoche);
            try
            {
                _cotizacion = _cambioBLL.Cotizar(_reserva, habitacion, _motivo);
                bool cobra = _cotizacion.Monto > 0;
                lblDiferencia.Text = cobra ? "+" + Moneda(_cotizacion.DiferenciaPorNoche) : "—";
                lblNochesCobro.Text = cobra ? _cotizacion.NochesRestantes.ToString() : "—";
                lblCargo.Text = cobra ? Moneda(_cotizacion.Monto) : T("CambioHab_txtSinCargo");
                lblCargo.ForeColor = cobra ? Color.Goldenrod : ColorExito;
                MostrarNota(T(cobra ? "CambioHab_lblNotaUpgrade" : EsSinCheckIn ? "CambioHab_lblNotaConfirmada" : "CambioHab_lblNotaAdmin"), TextoSuave);
                btnConfirmar.Enabled = true;
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                lblDiferencia.Text = lblNochesCobro.Text = "—";
                lblCargo.Text = "—";
                lblCargo.ForeColor = ColorError;
                MostrarNota(Describir(ex), ColorError);
                btnConfirmar.Enabled = false;
            }
        }

        private void LimpiarCotizacion()
        {
            lblTarifaNueva.Text = lblDiferencia.Text = lblNochesCobro.Text = lblCargo.Text = "—";
            lblCargo.ForeColor = Color.Goldenrod;
        }

        private void MostrarNota(string texto, Color color)
        {
            lblNota.Text = texto;
            lblNota.ForeColor = color;
        }

        #endregion

        #region Métodos auxiliares

        private void Confirmar()
        {
            var habitacion = OpcionSeleccionada();
            if (habitacion == null || _cotizacion == null || !btnConfirmar.Enabled) return;

            string titular = $"{_reserva.Titular.Nombre} {_reserva.Titular.Apellido}";
            string pregunta = _motivo == MotivoCambioHabitacion.Upgrade
                ? string.Format(T("CambioHab_msgConfirmarUpgrade"), titular, _reserva.Habitacion.NroHabitacion, habitacion.NroHabitacion, Moneda(_cotizacion.Monto))
                : string.Format(T("CambioHab_msgConfirmarReubicacion"), titular, _reserva.Habitacion.NroHabitacion, habitacion.NroHabitacion);
            if (!UiCargos_68SA.Confirmar(this, pregunta)) return;

            try
            {
                Resultado = _cambioBLL.Registrar(new SolicitudCambioHabitacion_68SA
                {
                    ReservaId = _reserva.Id,
                    HabitacionNuevaId = habitacion.Id,
                    Motivo = _motivo,
                    Observacion = txtObservacion.RealText
                });
                HabitacionNueva = habitacion;

                Info(this, Resultado.Monto > 0
                        ? string.Format(T("CambioHab_msgUpgradeRegistrado"), habitacion.NroHabitacion, Moneda(Resultado.Monto))
                        : string.Format(T(EsSinCheckIn ? "CambioHab_msgReubicacionReservaRegistrada" : "CambioHab_msgReubicacionRegistrada"), habitacion.NroHabitacion),
                     T("CambioHab_tituloRegistrado"));
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                Aviso(this, Describir(ex), T("CambioHab_tituloNoSePudo"));
                // La disponibilidad pudo cambiar (otro recepcionista): se vuelve a consultar
                if (ex is HabitacionNoDisponibleException_68SA) CargarOpciones();
            }
        }

        #endregion

        #region Sobrescrituras

        // Pinta el formulario completo en memoria y lo vuelca de una vez (sin parpadeo al redimensionar columnas)
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
