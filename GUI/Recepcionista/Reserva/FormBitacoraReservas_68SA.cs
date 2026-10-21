using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GUI_08YS.UserControls;
using static GUI_08YS.Recepcionista.UiCargos_68SA;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Bitácora de cambios de reservas (patrón de la cátedra): cada alta y cada cambio de una reserva
    /// (modificación, cancelación, no-show, check-in, check-out, renovación, upgrade o reubicación) deja una
    /// foto completa de la reserva. Filtros por código, nombre, operación y rango de fechas; Aplicar / Limpiar / Salir.
    /// A la derecha se ve, campo por campo, qué cambió en el registro elegido respecto del anterior.
    /// "Ver historial" muestra todos los registros de la reserva elegida y "Restaurar como activo" (rollback,
    /// solo Administrador y Supervisor) vuelve a poner como estado actual un registro anterior.
    /// Los registros los genera el trigger de Reservas y no se borran ni se editan: el rollback deja uno nuevo.
    /// </summary>
    public partial class FormBitacoraReservas_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private static readonly Color ColorCambio = Color.FromArgb(40, 52, 30);
        private static readonly Color ColorAzul = Color.FromArgb(100, 170, 255);
        private static readonly Color ColorRollback = Color.FromArgb(190, 140, 255);

        private readonly BitacoraReservaBLL_68SA _bitacoraBLL = BLLFactory_08YS.CreateBitacoraReservaBLL();
        private List<BitacoraReserva_68SA> _registros = new List<BitacoraReserva_68SA>();
        private bool _sinAplicar;   // mientras se cargan filtros por código, los controles no vuelven a consultar

        #endregion

        #region Constructor

        public FormBitacoraReservas_68SA()
        {
            InitializeComponent();

            CrudEstilo_68SA.AplicarGrilla(dgvBitacora, 40);
            dgvBitacora.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvBitacora.SelectionChanged += (s, e) => ActualizarSeleccion();
            dgvBitacora.CellFormatting += DgvBitacora_CellFormatting;

            CrudEstilo_68SA.AplicarGrilla(dgvDetalle, 32);
            dgvDetalle.BackgroundColor = FondoBarra;
            dgvDetalle.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvDetalle.CellFormatting += DgvDetalle_CellFormatting;

            btnAplicar.Click += (s, e) => Aplicar();
            btnLimpiar.Click += (s, e) => Limpiar();
            btnSalir.Click += (s, e) => Close();
            btnHistorial.Click += (s, e) => VerHistorial();
            btnRollback.Click += (s, e) => Rollback();
            chkSoloActivos.CheckedChanged += (s, e) => { if (!_sinAplicar) Aplicar(); };
            cmbOperacion.SelectedIndexChanged += (s, e) => { if (Visible && !_sinAplicar) Aplicar(); };

            // Rollback: solo Administrador y Supervisor (permiso RollbackBitacoraReservas)
            btnRollback.Visible = _bitacoraBLL.PuedeHacerRollback();
            LayoutAdaptable_68SA.SoloIconoSiNoEntra(btnHistorial);
            LayoutAdaptable_68SA.SoloIconoSiNoEntra(btnRollback);
            foreach (var txt in new[] { txtCodigo, txtNombre })
                txt.InnerTextBox.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Aplicar(); } };
            // El código de reserva es numérico
            txtCodigo.InnerTextBox.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };

            FormClosed += (s, e) => TraductorManager_08YS.Instance.Desuscribir(this);

            AplicarTextos();
            cmbOperacion.SelectedIndex = 0;
            Shown += (s, e) => Aplicar();
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            AplicarTextos();
            Mostrar();
        }

        private void AplicarTextos()
        {
            Traducir(this);
            txtCodigo.PlaceholderText = T("BitacoraRes_phCodigo");
            txtNombre.PlaceholderText = T("BitacoraRes_phNombre");
            OpcionEnum_68SA.Cargar(cmbOperacion, typeof(OperacionBitacoraReserva), T("BitacoraRes_txtTodas"));
        }

        #endregion

        #region Carga de datos

        private void Aplicar()
        {
            int? codigo = null;
            string textoCodigo = txtCodigo.RealText?.Trim();
            if (!string.IsNullOrEmpty(textoCodigo))
            {
                if (!int.TryParse(textoCodigo, out int valor) || valor <= 0)
                {
                    Aviso(this, T("BitacoraRes_msgCodigoInvalido"));
                    return;
                }
                codigo = valor;
            }

            try
            {
                _registros = _bitacoraBLL.Filtrar(new BitacoraReservaFiltro_68SA
                {
                    Codigo = codigo,
                    Nombre = string.IsNullOrWhiteSpace(txtNombre.RealText) ? null : txtNombre.RealText.Trim(),
                    Operacion = OpcionEnum_68SA.Seleccionado<OperacionBitacoraReserva>(cmbOperacion),
                    FechaDesde = dtpDesde.Value,
                    FechaHasta = dtpHasta.Value,
                    SoloActivos = chkSoloActivos.Checked
                });
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                Aviso(this, Describir(ex));
                return;
            }
            Mostrar();
        }

        /// <summary>Historial de cambios de la reserva elegida: todos sus registros, sin los demás filtros.</summary>
        private void VerHistorial(int? reservaId = null, Func<BitacoraReserva_68SA, bool> seleccionar = null)
        {
            int id = reservaId ?? RegistroSeleccionado()?.Reserva.ReservaId ?? 0;
            if (id <= 0) return;

            _sinAplicar = true;
            try
            {
                txtCodigo.Text = id.ToString();
                txtNombre.Text = string.Empty;
                dtpDesde.Value = null;
                dtpHasta.Value = null;
                cmbOperacion.SelectedIndex = 0;
                chkSoloActivos.Checked = false;
            }
            finally
            {
                _sinAplicar = false;
            }

            try
            {
                _registros = _bitacoraBLL.GetHistorial(id);
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                Aviso(this, Describir(ex));
                return;
            }
            Mostrar();
            if (seleccionar != null) Seleccionar(seleccionar);
        }

        /// <summary>
        /// Rollback: el registro anterior elegido vuelve a ser el estado ACTIVO de la reserva. La base restaura la reserva
        /// con esa foto y la bitácora suma un registro "Rollback"; el que estaba activo queda como historia.
        /// </summary>
        private void Rollback()
        {
            var registro = RegistroSeleccionado();
            if (registro == null || registro.Act) return;

            string pregunta = string.Format(T("BitacoraRes_msgConfirmarRollback"),
                registro.Reserva.ReservaId.ToString("000000"), registro.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                OpcionEnum_68SA.Texto(registro.Operacion));
            if (!Confirmar(this, pregunta, T("BitacoraRes_tituloRollback"))) return;

            try
            {
                _bitacoraBLL.Rollback(registro);
            }
            catch (RollbackReservaException_68SA ex)
            {
                Aviso(this, T("BitacoraRes_excRollback" + ex.Motivo), T("BitacoraRes_tituloRollback"));
                // La reserva pudo cambiar mientras tanto: se vuelve a mostrar su historial actualizado
                VerHistorial(registro.Reserva.ReservaId);
                return;
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                Aviso(this, Describir(ex), T("BitacoraRes_tituloRollback"));
                return;
            }

            Info(this, string.Format(T("BitacoraRes_msgRollbackOk"), registro.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                     registro.Reserva.ReservaId.ToString("000000")), T("BitacoraRes_tituloRollback"));
            // Se muestra el historial de la reserva con el registro nuevo (el activo) seleccionado
            VerHistorial(registro.Reserva.ReservaId, r => r.Act);
        }

        private void Limpiar()
        {
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            dtpDesde.Value = null;
            dtpHasta.Value = null;
            cmbOperacion.SelectedIndex = 0;     // si cambió, ya vuelve a aplicar el filtro
            if (chkSoloActivos.Checked)
                chkSoloActivos.Checked = false; // el cambio ya vuelve a aplicar el filtro
            else
                Aplicar();
        }

        #endregion

        #region Grilla de registros

        private sealed class FilaBitacora_68SA
        {
            public int Id { get; set; }
            public DateTime Fecha { get; set; }
            public string Operacion { get; set; }
            public int Reserva { get; set; }
            public string Titular { get; set; }
            public string Habitacion { get; set; }
            public string Estado { get; set; }
            public string Estadia { get; set; }
            public string Huespedes { get; set; }
            public decimal Monto { get; set; }
            public string Usuario { get; set; }
            public string Act { get; set; }
        }

        private void Mostrar()
        {
            int? seleccionado = RegistroSeleccionado()?.Id;

            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = _registros.Select(r => new FilaBitacora_68SA
            {
                Id = r.Id,
                Fecha = r.FechaHora,
                Operacion = OpcionEnum_68SA.Texto(r.Operacion),
                Reserva = r.Reserva.ReservaId,
                Titular = r.Reserva.Titular,
                Habitacion = r.Reserva.NroHabitacion,
                Estado = OpcionEnum_68SA.Texto(r.Reserva.Estado),
                Estadia = $"{r.Reserva.FechaIngreso:dd/MM/yy} → {r.Reserva.FechaEgreso:dd/MM/yy}",
                Huespedes = $"{r.Reserva.CantidadAdultos} + {r.Reserva.CantidadNinos}",
                Monto = r.Reserva.MontoTotal,
                Usuario = TextoUsuario(r),
                Act = r.Act ? "✔" : string.Empty
            }).ToList();
            AplicarColumnas();

            foreach (DataGridViewRow fila in dgvBitacora.Rows)
                if ((fila.DataBoundItem as FilaBitacora_68SA)?.Id == seleccionado)
                {
                    fila.Selected = true;
                    dgvBitacora.CurrentCell = fila.Cells[nameof(FilaBitacora_68SA.Reserva)];
                    break;
                }

            ActualizarSeleccion();
        }

        private void Seleccionar(Func<BitacoraReserva_68SA, bool> criterio)
        {
            foreach (DataGridViewRow fila in dgvBitacora.Rows)
            {
                var registro = RegistroDeFila(fila.Index);
                if (registro == null || !criterio(registro)) continue;
                dgvBitacora.ClearSelection();
                fila.Selected = true;
                dgvBitacora.CurrentCell = fila.Cells[nameof(FilaBitacora_68SA.Reserva)];
                break;
            }
            ActualizarSeleccion();
        }

        private void AplicarColumnas()
        {
            var col = dgvBitacora.Columns;
            if (col.Count == 0) return;
            col[nameof(FilaBitacora_68SA.Id)].Visible = false;

            void Col(string nombre, string clave, float peso, string formato = null, DataGridViewContentAlignment? alineacion = null)
            {
                col[nombre].HeaderText = T(clave);
                col[nombre].FillWeight = peso;
                if (formato != null) col[nombre].DefaultCellStyle.Format = formato;
                if (alineacion.HasValue) col[nombre].DefaultCellStyle.Alignment = alineacion.Value;
            }
            Col(nameof(FilaBitacora_68SA.Fecha), "BitacoraRes_colFecha", 11, "dd/MM/yyyy HH:mm");
            Col(nameof(FilaBitacora_68SA.Operacion), "BitacoraRes_colOperacion", 11);
            Col(nameof(FilaBitacora_68SA.Reserva), "BitacoraRes_colReserva", 7, "000000");
            Col(nameof(FilaBitacora_68SA.Titular), "BitacoraRes_colTitular", 13);
            Col(nameof(FilaBitacora_68SA.Habitacion), "BitacoraRes_colHabitacion", 6, null, DataGridViewContentAlignment.MiddleCenter);
            Col(nameof(FilaBitacora_68SA.Estado), "BitacoraRes_colEstado", 8);
            Col(nameof(FilaBitacora_68SA.Estadia), "BitacoraRes_colEstadia", 11);
            Col(nameof(FilaBitacora_68SA.Huespedes), "BitacoraRes_colHuespedes", 6, null, DataGridViewContentAlignment.MiddleCenter);
            Col(nameof(FilaBitacora_68SA.Monto), "BitacoraRes_colMonto", 9, "$#,##0.00", DataGridViewContentAlignment.MiddleRight);
            Col(nameof(FilaBitacora_68SA.Usuario), "BitacoraRes_colUsuario", 9);
            Col(nameof(FilaBitacora_68SA.Act), "BitacoraRes_colAct", 4, null, DataGridViewContentAlignment.MiddleCenter);
        }

        private void DgvBitacora_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var registro = RegistroDeFila(e.RowIndex);
            if (registro == null) return;

            string columna = dgvBitacora.Columns[e.ColumnIndex].Name;
            if (columna == nameof(FilaBitacora_68SA.Act))
                e.CellStyle.ForeColor = ColorExito;
            else if (columna == nameof(FilaBitacora_68SA.Operacion))
                e.CellStyle.ForeColor = ColorOperacion(registro.Operacion);
            else if (!registro.Act)
                // El registro vigente de cada reserva se destaca; los anteriores quedan más apagados
                e.CellStyle.ForeColor = TextoSuave;
        }

        private static Color ColorOperacion(OperacionBitacoraReserva operacion)
        {
            switch (operacion)
            {
                case OperacionBitacoraReserva.Alta:
                case OperacionBitacoraReserva.CheckIn:
                    return ColorExito;
                case OperacionBitacoraReserva.Cancelacion:
                case OperacionBitacoraReserva.NoShow:
                    return ColorError;
                case OperacionBitacoraReserva.Upgrade:
                case OperacionBitacoraReserva.Renovacion:
                    return Color.Goldenrod;
                case OperacionBitacoraReserva.CambioAdministrativo:
                case OperacionBitacoraReserva.CambioHabitacion:
                case OperacionBitacoraReserva.CheckOut:
                    return ColorAzul;
                case OperacionBitacoraReserva.Modificacion:
                    return ColorAviso;
                case OperacionBitacoraReserva.Rollback:
                    return ColorRollback;
                default:
                    return TextoSuave;
            }
        }

        private static string TextoUsuario(BitacoraReserva_68SA registro)
            // Solo el usuario del sistema: el login de la base (en Windows "PC\usuario") no se muestra
            => string.IsNullOrWhiteSpace(registro.UsuarioApp) ? T("BitacoraRes_txtFueraDelSistema") : registro.UsuarioApp;

        private BitacoraReserva_68SA RegistroDeFila(int fila)
        {
            if (fila < 0 || fila >= dgvBitacora.Rows.Count) return null;
            int id = (dgvBitacora.Rows[fila].DataBoundItem as FilaBitacora_68SA)?.Id ?? 0;
            return _registros.FirstOrDefault(r => r.Id == id);
        }

        private BitacoraReserva_68SA RegistroSeleccionado()
            => dgvBitacora.CurrentRow == null ? null : RegistroDeFila(dgvBitacora.CurrentRow.Index);

        private void ActualizarSeleccion()
        {
            var registro = RegistroSeleccionado();
            if (registro == null)
                lblSeleccion.Text = string.Format(T("BitacoraRes_txtTotal"), _registros.Count);
            else
                lblSeleccion.Text = string.Format(T(registro.Act ? "BitacoraRes_txtVigente" : "BitacoraRes_txtAnterior"),
                    registro.Reserva.ReservaId.ToString("000000"), _registros.Count);
            btnHistorial.Enabled = registro != null;
            btnRollback.Enabled = registro != null && !registro.Act;
            MostrarDetalle(registro);
        }

        #endregion

        #region Detalle del registro

        private sealed class FilaDetalle_68SA
        {
            public string Campo { get; set; }
            public string Anterior { get; set; }
            public string Nuevo { get; set; }
            public bool Cambio { get; set; }
        }

        /// <summary>Campo por campo: valor en el registro anterior de la reserva y en el elegido.</summary>
        private void MostrarDetalle(BitacoraReserva_68SA registro)
        {
            dgvDetalle.DataSource = null;
            if (registro == null)
            {
                lblDetalleSub.Text = T("BitacoraRes_txtDetalleVacio");
                return;
            }

            string sub = string.Format(T("BitacoraRes_txtDetalleSub"), OpcionEnum_68SA.Texto(registro.Operacion),
                registro.FechaHora.ToString("dd/MM/yyyy HH:mm"), TextoUsuario(registro));
            if (registro.CambioId.HasValue)
                sub += Environment.NewLine + string.Format(T("BitacoraRes_txtCambioHabitacion"), registro.CambioId.Value);
            if (registro.RollbackDeId.HasValue)
            {
                var restaurado = _registros.FirstOrDefault(r => r.Id == registro.RollbackDeId.Value);
                sub += Environment.NewLine + (restaurado == null
                    ? string.Format(T("BitacoraRes_txtRollbackDeId"), registro.RollbackDeId.Value)
                    : string.Format(T("BitacoraRes_txtRollbackDe"), restaurado.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                                    OpcionEnum_68SA.Texto(restaurado.Operacion)));
            }
            lblDetalleSub.Text = sub;

            var cambiados = new HashSet<CampoReserva>(registro.CamposModificados());
            dgvDetalle.DataSource = Enum.GetValues(typeof(CampoReserva)).Cast<CampoReserva>().Select(c => new FilaDetalle_68SA
            {
                Campo = NombreCampo(c),
                Anterior = registro.Anterior == null ? "—" : Formatear(registro.Anterior, c),
                Nuevo = Formatear(registro.Reserva, c),
                Cambio = cambiados.Contains(c)
            }).ToList();

            var col = dgvDetalle.Columns;
            if (col.Count == 0) return;
            col[nameof(FilaDetalle_68SA.Cambio)].Visible = false;
            col[nameof(FilaDetalle_68SA.Campo)].HeaderText = T("BitacoraRes_colCampo");
            col[nameof(FilaDetalle_68SA.Campo)].FillWeight = 34;
            col[nameof(FilaDetalle_68SA.Anterior)].HeaderText = T("BitacoraRes_colValorAnterior");
            col[nameof(FilaDetalle_68SA.Anterior)].FillWeight = 33;
            col[nameof(FilaDetalle_68SA.Nuevo)].HeaderText = T("BitacoraRes_colValorNuevo");
            col[nameof(FilaDetalle_68SA.Nuevo)].FillWeight = 33;
            dgvDetalle.ClearSelection();
        }

        private void DgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDetalle.Rows.Count) return;
            var fila = dgvDetalle.Rows[e.RowIndex].DataBoundItem as FilaDetalle_68SA;
            if (fila == null) return;

            // Los campos que cambiaron se resaltan; el resto queda apagado
            if (fila.Cambio)
            {
                e.CellStyle.BackColor = ColorCambio;
                e.CellStyle.SelectionBackColor = ColorCambio;
                e.CellStyle.ForeColor = dgvDetalle.Columns[e.ColumnIndex].Name == nameof(FilaDetalle_68SA.Nuevo)
                    ? Color.Goldenrod : Color.WhiteSmoke;
            }
            else
                e.CellStyle.ForeColor = TextoSuave;
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }

        private static string NombreCampo(CampoReserva campo) => T("BitacoraRes_campo" + campo);

        private static string Formatear(FotoReserva_68SA foto, CampoReserva campo)
        {
            switch (campo)
            {
                case CampoReserva.Titular: return foto.Titular ?? "—";
                case CampoReserva.Habitacion:
                    return string.IsNullOrEmpty(foto.TipoHabitacion) ? foto.NroHabitacion : $"{foto.NroHabitacion} · {foto.TipoHabitacion}";
                case CampoReserva.FechaIngreso: return foto.FechaIngreso.ToString("dd/MM/yyyy");
                case CampoReserva.FechaEgreso: return foto.FechaEgreso.ToString("dd/MM/yyyy");
                case CampoReserva.CheckIn: return foto.CheckIn?.ToString("dd/MM/yyyy HH:mm") ?? "—";
                case CampoReserva.CheckOut: return foto.CheckOut?.ToString("dd/MM/yyyy HH:mm") ?? "—";
                case CampoReserva.Estado: return OpcionEnum_68SA.Texto(foto.Estado);
                case CampoReserva.Adultos: return foto.CantidadAdultos.ToString();
                case CampoReserva.Ninos: return foto.CantidadNinos.ToString();
                case CampoReserva.TarifaNoche: return Moneda(foto.TarifaNoche);
                case CampoReserva.MontoTotal: return Moneda(foto.MontoTotal);
                case CampoReserva.MontoOriginal: return Moneda(foto.MontoOriginal);
                case CampoReserva.UsuarioRegistro: return foto.UsuarioRegistro ?? "—";
                case CampoReserva.FechaRegistro: return foto.FechaRegistro.ToString("dd/MM/yyyy HH:mm");
                default: return string.Empty;
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
