using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using Service_08YS;
using Service_08YS.Entities.Acceso;
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
    /// Bitácora de cambios de habitación (patrón de la cátedra para la tabla de cambios):
    /// filtros por código (de reserva), nombre (huésped o habitación) y rango de fechas; Aplicar / Limpiar / Salir.
    /// Es de solo lectura: los registros los generan los triggers de la base y no se borran ni se editan.
    /// </summary>
    public partial class FormBitacoraCambiosHabitacion_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private readonly BitacoraCambioHabitacionBLL_68SA _bitacoraBLL = BLLFactory_08YS.CreateBitacoraCambioHabitacionBLL();
        private List<BitacoraCambioHabitacion_68SA> _registros = new List<BitacoraCambioHabitacion_68SA>();

        #endregion

        #region Constructor

        public FormBitacoraCambiosHabitacion_68SA()
        {
            InitializeComponent();

            CrudEstilo_68SA.AplicarGrilla(dgvBitacora, 40);
            dgvBitacora.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvBitacora.SelectionChanged += (s, e) => ActualizarSeleccion();
            dgvBitacora.CellFormatting += DgvBitacora_CellFormatting;

            btnAplicar.Click += (s, e) => Aplicar();
            btnLimpiar.Click += (s, e) => Limpiar();
            btnSalir.Click += (s, e) => Close();
            chkSoloActivos.CheckedChanged += (s, e) => Aplicar();
            foreach (var txt in new[] { txtCodigo, txtNombre })
                txt.InnerTextBox.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Aplicar(); } };
            // El código de reserva es numérico
            txtCodigo.InnerTextBox.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };

            FormClosed += (s, e) => TraductorManager_08YS.Instance.Desuscribir(this);

            AplicarTextos();
            Shown += (s, e) => Aplicar();
        }

        #endregion

        #region Filtros

        private void Aplicar()
        {
            int? codigo = null;
            string textoCodigo = txtCodigo.RealText?.Trim();
            if (!string.IsNullOrEmpty(textoCodigo))
            {
                if (!int.TryParse(textoCodigo, out int valor) || valor <= 0)
                {
                    Aviso(this, T("BitacoraHab_msgCodigoInvalido"));
                    return;
                }
                codigo = valor;
            }

            try
            {
                _registros = _bitacoraBLL.Filtrar(new BitacoraCambioHabitacionFiltro_68SA
                {
                    Codigo = codigo,
                    Nombre = string.IsNullOrWhiteSpace(txtNombre.RealText) ? null : txtNombre.RealText.Trim(),
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

        private void Limpiar()
        {
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            dtpDesde.Value = null;
            dtpHasta.Value = null;
            if (chkSoloActivos.Checked)
                chkSoloActivos.Checked = false; // el cambio ya vuelve a aplicar el filtro
            else
                Aplicar();
        }

        #endregion

        #region Grilla

        private sealed class FilaBitacora_68SA
        {
            public int Id { get; set; }
            public int Reserva { get; set; }
            public string Huesped { get; set; }
            public string Habitacion { get; set; }
            public string Tipo { get; set; }
            public decimal Tarifa { get; set; }
            public string Anterior { get; set; }
            public string Operacion { get; set; }
            public decimal Cargo { get; set; }
            public DateTime Fecha { get; set; }
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
                Reserva = r.ReservaId,
                Huesped = r.Huesped,
                Habitacion = r.NroHabitacion,
                Tipo = r.TipoHabitacion,
                Tarifa = r.TarifaNoche,
                Anterior = r.NroHabitacionAnterior ?? "—",
                Operacion = TextoOperacion(r.Operacion),
                Cargo = r.MontoCargo,
                Fecha = r.FechaHora,
                // Solo el usuario del sistema: el login de la base (en Windows "PC\usuario") no se muestra
                Usuario = string.IsNullOrWhiteSpace(r.UsuarioApp) ? T("BitacoraHab_txtFueraDelSistema") : r.UsuarioApp,
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

        // La operación se guarda en español en la base (la escriben los triggers): acá se muestra traducida
        private static string TextoOperacion(string operacion)
        {
            switch (operacion)
            {
                case "Alta": return T("BitacoraHab_opAlta");
                case "Upgrade": return T("BitacoraHab_opUpgrade");
                case "Cambio administrativo": return T("BitacoraHab_opAdministrativo");
                case "Cambio en BD": return T("BitacoraHab_opCambioBD");
                default: return operacion;
            }
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
            Col(nameof(FilaBitacora_68SA.Reserva), "BitacoraHab_colReserva", 7, "000000");
            Col(nameof(FilaBitacora_68SA.Huesped), "BitacoraHab_colHuesped", 15);
            Col(nameof(FilaBitacora_68SA.Habitacion), "BitacoraHab_colHabitacion", 7, null, DataGridViewContentAlignment.MiddleCenter);
            Col(nameof(FilaBitacora_68SA.Tipo), "BitacoraHab_colTipo", 11);
            Col(nameof(FilaBitacora_68SA.Tarifa), "BitacoraHab_colTarifa", 9, "$#,##0.00", DataGridViewContentAlignment.MiddleRight);
            Col(nameof(FilaBitacora_68SA.Anterior), "BitacoraHab_colAnterior", 7, null, DataGridViewContentAlignment.MiddleCenter);
            Col(nameof(FilaBitacora_68SA.Operacion), "BitacoraHab_colOperacion", 12);
            Col(nameof(FilaBitacora_68SA.Cargo), "BitacoraHab_colCargo", 9, "$#,##0.00", DataGridViewContentAlignment.MiddleRight);
            Col(nameof(FilaBitacora_68SA.Fecha), "BitacoraHab_colFecha", 11, "dd/MM/yyyy HH:mm");
            Col(nameof(FilaBitacora_68SA.Usuario), "BitacoraHab_colUsuario", 14);
            Col(nameof(FilaBitacora_68SA.Act), "BitacoraHab_colAct", 5, null, DataGridViewContentAlignment.MiddleCenter);
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
            else if (columna == nameof(FilaBitacora_68SA.Cargo) && registro.MontoCargo == 0)
            {
                e.Value = "—";
                e.FormattingApplied = true;
            }

            // El registro vigente de cada reserva se destaca; los anteriores quedan más apagados
            if (!registro.Act && columna != nameof(FilaBitacora_68SA.Operacion))
                e.CellStyle.ForeColor = TextoSuave;
        }

        private static Color ColorOperacion(string operacion)
        {
            switch (operacion)
            {
                case "Upgrade": return Color.Goldenrod;
                case "Cambio administrativo": return Color.FromArgb(100, 170, 255);
                case "Cambio en BD": return ColorError;
                default: return Color.WhiteSmoke;
            }
        }

        private BitacoraCambioHabitacion_68SA RegistroDeFila(int fila)
        {
            if (fila < 0 || fila >= dgvBitacora.Rows.Count) return null;
            int id = (dgvBitacora.Rows[fila].DataBoundItem as FilaBitacora_68SA)?.Id ?? 0;
            return _registros.FirstOrDefault(r => r.Id == id);
        }

        private BitacoraCambioHabitacion_68SA RegistroSeleccionado()
            => dgvBitacora.CurrentRow == null ? null : RegistroDeFila(dgvBitacora.CurrentRow.Index);

        private void ActualizarSeleccion()
        {
            var registro = RegistroSeleccionado();
            if (registro == null)
                lblSeleccion.Text = string.Format(T("BitacoraHab_txtTotal"), _registros.Count);
            else if (registro.Act)
                lblSeleccion.Text = string.Format(T("BitacoraHab_txtYaActivo"), registro.ReservaId.ToString("000000"), registro.NroHabitacion);
            else
                lblSeleccion.Text = string.Format(T("BitacoraHab_txtAnterior"), registro.ReservaId.ToString("000000"), registro.NroHabitacion);
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
            txtCodigo.PlaceholderText = T("BitacoraHab_phCodigo");
            txtNombre.PlaceholderText = T("BitacoraHab_phNombre");
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
