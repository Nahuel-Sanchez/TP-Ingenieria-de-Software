using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using CustomControls;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormReservas_68SA : Form, IIdiomaObserver_08YS
    {
        private static readonly Bitmap IconoVacio = new Bitmap(1, 1);

        private readonly Action<Form> _openChildForm;
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private readonly HabitacionBLL_68SA _habitacionBLL = BLLFactory_08YS.CreateHabitacionBLL();

        private DateTime _fechaBaseCalendario = DateTime.Today;
        private ModoCalendarioReservas _modoCalendario = ModoCalendarioReservas.Semana;

        public FormReservas_68SA(Action<Form> openChildForm)
        {
            _openChildForm = openChildForm;
            InitializeComponent();

            ConfigurarGrid();

            cmbEstadoFiltro.Items.Add("Todos");
            cmbEstadoFiltro.Items.AddRange(Enum.GetValues(typeof(EstadoReserva)).Cast<object>().ToArray());
            cmbEstadoFiltro.SelectedIndex = 0;

            cmbOperadorCosto.Items.AddRange(new object[] { "Cualquiera", "Mayor a", "Menor a", "Entre" });
            cmbOperadorCosto.SelectedIndex = 0;
            cmbOperadorCosto.SelectedIndexChanged += (s, e) => ActualizarVisibilidadCosto();

            btnTabCalendario.Click += (s, e) => CambiarTab(false);
            btnTabLista.Click += (s, e) => CambiarTab(true);
            btnCrearReserva.Click += (s, e) => _openChildForm(new FormDisponibilidad(_openChildForm));
            btnCalAnterior.Click += (s, e) =>
            {
                _fechaBaseCalendario = _modoCalendario == ModoCalendarioReservas.Semana
                    ? _fechaBaseCalendario.AddDays(-7) : _fechaBaseCalendario.AddMonths(-1);
                CargarCalendario();
            };
            btnCalSiguiente.Click += (s, e) =>
            {
                _fechaBaseCalendario = _modoCalendario == ModoCalendarioReservas.Semana
                    ? _fechaBaseCalendario.AddDays(7) : _fechaBaseCalendario.AddMonths(1);
                CargarCalendario();
            };
            btnCalHoy.Click += (s, e) => { _fechaBaseCalendario = DateTime.Today; CargarCalendario(); };
            btnCalVistaSemana.Click += (s, e) => CambiarVistaCalendario(ModoCalendarioReservas.Semana);
            btnCalVistaMes.Click += (s, e) => CambiarVistaCalendario(ModoCalendarioReservas.Mes);
            ucCalendario.ReservaClickeada += (s, reserva) =>
                MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("Reservas_msgDetallePendiente"), reserva.Id), TraductorManager_08YS.Instance.GetTexto("Comun_tituloPendiente"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnFiltrar.Click += (s, e) => CargarLista();
            btnLimpiarFiltros.Click += (s, e) => LimpiarFiltros();

            // La primera carga se dispara en Load, no acá — generar los íconos de la grilla
            // antes de que la ventana tenga su handle de Windows los deja sin pintar.
            this.Shown += (s, e) => CargarLista();
        }

        // ---------- Grid ----------

        private void ConfigurarGrid()
        {
            dgvReservas.AutoGenerateColumns = true;
            dgvReservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReservas.BackgroundColor = Color.FromArgb(5, 10, 40);
            dgvReservas.GridColor = Color.FromArgb(35, 48, 85);
            dgvReservas.BorderStyle = BorderStyle.None;
            dgvReservas.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgvReservas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvReservas.RowHeadersVisible = false;
            dgvReservas.AllowUserToAddRows = false;
            dgvReservas.AllowUserToDeleteRows = false;
            dgvReservas.AllowUserToOrderColumns = false;
            dgvReservas.ReadOnly = true;
            dgvReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservas.MultiSelect = false;
            dgvReservas.RowTemplate.Height = 48;
            dgvReservas.ColumnHeadersHeight = 36;
            dgvReservas.EnableHeadersVisualStyles = false;

            dgvReservas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 18, 50);
            dgvReservas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Goldenrod;
            dgvReservas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvReservas.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(10, 18, 50);
            dgvReservas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvReservas.DefaultCellStyle.BackColor = Color.FromArgb(10, 18, 50);
            dgvReservas.DefaultCellStyle.ForeColor = Color.WhiteSmoke;
            dgvReservas.DefaultCellStyle.SelectionBackColor = Color.Goldenrod;
            dgvReservas.DefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 15, 35);
            dgvReservas.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvReservas.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvReservas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(13, 22, 58);

            dgvReservas.CellFormatting += DgvReservas_CellFormatting;
            dgvReservas.CellClick += DgvReservas_CellClick;
        }

        private void AplicarColumnasGrid()
        {
            void Configurar(string nombre, string encabezado, int peso, string formato = null)
            {
                if (dgvReservas.Columns[nombre] == null) return;
                dgvReservas.Columns[nombre].HeaderText = encabezado;
                dgvReservas.Columns[nombre].FillWeight = peso;
                if (formato != null) dgvReservas.Columns[nombre].DefaultCellStyle.Format = formato;
            }

            Configurar("ReservaId", "Cód.", 55);
            Configurar("HuespedDisplay", "Huésped", 190);
            Configurar("Habitacion", "Hab.", 60);
            Configurar("RegistroDisplay", "Registro", 170);
            Configurar("FechaEntrada", "Entrada", 90, "dd/MM/yyyy");
            Configurar("FechaSalida", "Salida", 90, "dd/MM/yyyy");
            Configurar("Costo", "Costo", 90, "$#,##0.00");
            Configurar("Adelanto", "Adelanto", 90, "$#,##0.00");
            Configurar("Saldo", "Saldo", 90, "$#,##0.00");
            Configurar("Estado", "Estado", 90);

            foreach (var oculta in new[] { "Huesped", "Documento", "RegistradoPor", "FechaRegistro", "PuedeCancelar" })
                if (dgvReservas.Columns[oculta] != null)
                    dgvReservas.Columns[oculta].Visible = false;

            // Evita duplicar las columnas de acciones en cada refresco — CargarLista() se llama en cada Filtrar/Limpiar
            foreach (var nombre in new[] { "colEditar", "colImprimir", "colCancelar" })
                if (dgvReservas.Columns.Contains(nombre))
                    dgvReservas.Columns.Remove(nombre);

            dgvReservas.Columns.Add(CrearColumnaIcono("colEditar"));
            dgvReservas.Columns.Add(CrearColumnaIcono("colImprimir"));
            dgvReservas.Columns.Add(CrearColumnaIcono("colCancelar"));

            Image iconoEditar = IconCache.Get(IconChar.PenToSquare, IconFont.Auto, 36, Color.Goldenrod);
            Image iconoImprimir = IconCache.Get(IconChar.Print, IconFont.Auto, 36, Color.Goldenrod);
            Image iconoCancelar = IconCache.Get(IconChar.Ban, IconFont.Auto, 36, Color.FromArgb(235, 90, 90));

            foreach (DataGridViewRow fila in dgvReservas.Rows)
            {
                if (!(fila.DataBoundItem is ReservaListItem_68SA item)) continue;
                fila.Cells["colEditar"].Value = iconoEditar;
                fila.Cells["colImprimir"].Value = iconoImprimir;
                fila.Cells["colCancelar"].Value = item.PuedeCancelar ? iconoCancelar : IconoVacio;
            }

            dgvReservas.Invalidate();
        }

        private static DataGridViewImageColumn CrearColumnaIcono(string nombre)
        {
            var columna = new DataGridViewImageColumn
            {
                Name = nombre,
                HeaderText = "",
                Width = 42,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            columna.DefaultCellStyle.BackColor = Color.FromArgb(10, 18, 50);
            columna.DefaultCellStyle.SelectionBackColor = Color.FromArgb(15, 25, 55);
            columna.DefaultCellStyle.NullValue = null;
            return columna;
        }

        private void DgvReservas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvReservas.Columns[e.ColumnIndex].Name != "Estado" || e.Value == null) return;

            switch (e.Value.ToString())
            {
                case "Pendiente": e.CellStyle.ForeColor = Color.FromArgb(90, 155, 235); break;
                case "Ocupada": e.CellStyle.ForeColor = Color.FromArgb(76, 217, 100); break;
                case "Finalizada": e.CellStyle.ForeColor = Color.FromArgb(160, 165, 180); break;
                case "Cancelada": e.CellStyle.ForeColor = Color.FromArgb(235, 90, 90); break;
            }
        }

        private void DgvReservas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (!(dgvReservas.Rows[e.RowIndex].DataBoundItem is ReservaListItem_68SA item)) return;

            string columna = dgvReservas.Columns[e.ColumnIndex].Name;

            if (columna == "colEditar")
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Reservas_msgEditarPendiente"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloPendiente"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (columna == "colImprimir")
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Reservas_msgImprimirPendiente"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloPendiente"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (columna == "colCancelar" && item.PuedeCancelar)
                CancelarReserva(item.ReservaId);
        }

        // ---------- Tabs ----------

        private void CambiarTab(bool lista)
        {
            pnlLista.Visible = lista;
            pnlCalendario.Visible = !lista;

            btnTabLista.BackColor = lista ? Color.FromArgb(10, 18, 50) : Color.FromArgb(5, 10, 40);
            btnTabLista.ForeColor = lista ? Color.Goldenrod : Color.FromArgb(160, 165, 180);
            btnTabLista.IconColor = btnTabLista.ForeColor;
            btnTabLista.Font = new Font("Segoe UI", 10F, lista ? FontStyle.Bold : FontStyle.Regular);

            btnTabCalendario.BackColor = !lista ? Color.FromArgb(10, 18, 50) : Color.FromArgb(5, 10, 40);
            btnTabCalendario.ForeColor = !lista ? Color.Goldenrod : Color.FromArgb(160, 165, 180);
            btnTabCalendario.IconColor = btnTabCalendario.ForeColor;
            btnTabCalendario.Font = new Font("Segoe UI", 10F, !lista ? FontStyle.Bold : FontStyle.Regular);

            if (!lista) CargarCalendario();
        }

        // ---------- Filtros ----------

        private void ActualizarVisibilidadCosto()
        {
            string operador = cmbOperadorCosto.SelectedItem?.ToString();
            nudCostoDesde.Visible = operador != "Cualquiera";
            lblGuionCosto.Visible = operador == "Entre";
            nudCostoHasta.Visible = operador == "Entre";
        }

        private void LimpiarFiltros()
        {
            txtHuespedFiltro.Text = string.Empty;
            txtHabitacionFiltro.Text = string.Empty;
            txtRegistroFiltro.Text = string.Empty;
            cmbEstadoFiltro.SelectedIndex = 0;
            dtpFechaDesde.Value = null;
            dtpFechaHasta.Value = null;
            dtpFechaEgresoDesde.Value = null;
            dtpFechaEgresoHasta.Value = null;
            cmbOperadorCosto.SelectedIndex = 0;
            nudCostoDesde.Value = 0;
            nudCostoHasta.Value = 0;
            CargarLista();
        }

        private ReservaFiltro_68SA ConstruirFiltro()
        {
            var filtro = new ReservaFiltro_68SA
            {
                Huesped = string.IsNullOrWhiteSpace(txtHuespedFiltro.RealText) ? null : txtHuespedFiltro.RealText.Trim(),
                Habitacion = string.IsNullOrWhiteSpace(txtHabitacionFiltro.RealText) ? null : txtHabitacionFiltro.RealText.Trim(),
                Registro = string.IsNullOrWhiteSpace(txtRegistroFiltro.RealText) ? null : txtRegistroFiltro.RealText.Trim(),
                Estado = cmbEstadoFiltro.SelectedItem is EstadoReserva estado ? estado : (EstadoReserva?)null,
                FechaDesde = dtpFechaDesde.Value,
                FechaHasta = dtpFechaHasta.Value,
                FechaEgresoDesde = dtpFechaEgresoDesde.Value,
                FechaEgresoHasta = dtpFechaEgresoHasta.Value
            };

            switch (cmbOperadorCosto.SelectedItem?.ToString())
            {
                case "Mayor a":
                    filtro.CostoDesde = nudCostoDesde.Value;
                    break;
                case "Menor a":
                    filtro.CostoHasta = nudCostoDesde.Value;
                    break;
                case "Entre":
                    filtro.CostoDesde = nudCostoDesde.Value;
                    filtro.CostoHasta = nudCostoHasta.Value;
                    break;
            }

            return filtro;
        }

        // ---------- Carga ----------

        private void CargarLista()
        {
            var filtro = ConstruirFiltro();

            if (filtro.FechaDesde.HasValue && filtro.FechaHasta.HasValue && filtro.FechaDesde > filtro.FechaHasta)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Reservas_msgFiltroIngresoInvalido"), TraductorManager_08YS.Instance.GetTexto("Reservas_tituloFiltroInvalido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (filtro.FechaEgresoDesde.HasValue && filtro.FechaEgresoHasta.HasValue && filtro.FechaEgresoDesde > filtro.FechaEgresoHasta)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Reservas_msgFiltroEgresoInvalido"), TraductorManager_08YS.Instance.GetTexto("Reservas_tituloFiltroInvalido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var reservas = _reservaBLL.GetTodas(filtro);
            ActualizarTarjetas(reservas);

            var items = reservas.Select(r => new ReservaListItem_68SA
            {
                ReservaId = r.Id,
                Huesped = $"{r.Titular.Nombre} {r.Titular.Apellido}",
                Documento = r.Titular.Documento,
                Habitacion = r.Habitacion.NroHabitacion,
                RegistradoPor = r.UsuarioRegistroNombre,
                FechaRegistro = r.FechaRegistro,
                FechaEntrada = r.FechaIngreso,
                FechaSalida = r.FechaEgreso,
                Costo = r.MontoTotal,
                Adelanto = r.MontoPagado,
                Saldo = Math.Max(0, r.MontoTotal - r.MontoPagado),
                Estado = TextoEstadoReserva(r.Estado),
                PuedeCancelar = r.Estado == EstadoReserva.Confirmada
            }).ToList();

            dgvReservas.DataSource = null;
            dgvReservas.DataSource = items;
            AplicarColumnasGrid();
        }
        private void CargarCalendario()
        {
            DateTime desde, hasta;

            if (_modoCalendario == ModoCalendarioReservas.Semana)
            {
                int diasDesdeElLunes = ((int)_fechaBaseCalendario.DayOfWeek + 6) % 7;
                desde = _fechaBaseCalendario.Date.AddDays(-diasDesdeElLunes);
                hasta = desde.AddDays(7);
            }
            else
            {
                desde = new DateTime(_fechaBaseCalendario.Year, _fechaBaseCalendario.Month, 1);
                hasta = desde.AddMonths(1);
            }

            lblCalPeriodo.Text = _modoCalendario == ModoCalendarioReservas.Semana
                ? $"{desde:dd/MM} – {hasta.AddDays(-1):dd/MM/yyyy}"
                : desde.ToString("MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo("es-ES"));

            var habitaciones = _habitacionBLL.GetAll();
            var reservas = _reservaBLL.GetEnRangoVisible(desde, hasta);

            ucCalendario.Cargar(habitaciones, reservas, desde, hasta, _modoCalendario);
        }

        private void CambiarVistaCalendario(ModoCalendarioReservas modo)
        {
            _modoCalendario = modo;

            bool esSemana = modo == ModoCalendarioReservas.Semana;
            btnCalVistaSemana.BackColor = esSemana ? Color.Goldenrod : Color.FromArgb(10, 18, 50);
            btnCalVistaSemana.ForeColor = esSemana ? Color.FromArgb(10, 15, 35) : Color.Goldenrod;
            btnCalVistaSemana.Font = new Font("Segoe UI", 9.5F, esSemana ? FontStyle.Bold : FontStyle.Regular);

            btnCalVistaMes.BackColor = !esSemana ? Color.Goldenrod : Color.FromArgb(10, 18, 50);
            btnCalVistaMes.ForeColor = !esSemana ? Color.FromArgb(10, 15, 35) : Color.Goldenrod;
            btnCalVistaMes.Font = new Font("Segoe UI", 9.5F, !esSemana ? FontStyle.Bold : FontStyle.Regular);

            CargarCalendario();
        }

        private void ActualizarTarjetas(List<Reserva_68SA> reservas)
        {
            var vigentes = reservas.Where(r => r.Estado == EstadoReserva.Confirmada || r.Estado == EstadoReserva.EnCurso).ToList();
            var pendientes = reservas.Where(r => r.Estado == EstadoReserva.Confirmada).ToList();
            var ocupadas = reservas.Where(r => r.Estado == EstadoReserva.EnCurso).ToList();
            var finalizadas = reservas.Where(r => r.Estado == EstadoReserva.Finalizada).ToList();
            var canceladas = reservas.Where(r => r.Estado == EstadoReserva.Cancelada).ToList();

            lblMontoVigentes.Text = $"${vigentes.Sum(r => r.MontoTotal):N2}";
            lblCantVigentes.Text = $"{TraductorManager_08YS.Instance.GetTexto("Reservas_txtCantidad")}: {vigentes.Count}";

            lblMontoPendientes.Text = $"${pendientes.Sum(r => r.MontoTotal):N2}";
            lblCantPendientes.Text = $"{TraductorManager_08YS.Instance.GetTexto("Reservas_txtCantidad")}: {pendientes.Count}";

            lblMontoOcupadas.Text = $"${ocupadas.Sum(r => r.MontoTotal):N2}";
            lblCantOcupadas.Text = $"{TraductorManager_08YS.Instance.GetTexto("Reservas_txtCantidad")}: {ocupadas.Count}";

            lblMontoFinalizadas.Text = $"${finalizadas.Sum(r => r.MontoTotal):N2}";
            lblCantFinalizadas.Text = $"{TraductorManager_08YS.Instance.GetTexto("Reservas_txtCantidad")}: {finalizadas.Count}";

            lblMontoCanceladas.Text = $"${canceladas.Sum(r => r.MontoTotal):N2}";
            lblCantCanceladas.Text = $"{TraductorManager_08YS.Instance.GetTexto("Reservas_txtCantidad")}: {canceladas.Count}";
        }

        private void CancelarReserva(int reservaId)
        {
            var confirmacion = MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Reservas_msgConfirmarCancelar"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloConfirmar"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _reservaBLL.Cancelar(reservaId);
                CargarLista();
            }
            catch (EstadoReservaInvalidoException_68SA)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Comun_excEstadoReservaInvalido"), TraductorManager_08YS.Instance.GetTexto("Reservas_tituloNoSePudoCancelar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static string TextoEstadoReserva(EstadoReserva estado)
        {
            switch (estado)
            {
                case EstadoReserva.Confirmada: return TraductorManager_08YS.Instance.GetTexto("Reservas_estadoPendiente");
                case EstadoReserva.EnCurso: return TraductorManager_08YS.Instance.GetTexto("Reservas_estadoOcupada");
                case EstadoReserva.Finalizada: return TraductorManager_08YS.Instance.GetTexto("Reservas_estadoFinalizada");
                case EstadoReserva.Cancelada: return TraductorManager_08YS.Instance.GetTexto("Reservas_estadoCancelada");
                default: return estado.ToString();
            }
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


    public class ReservaListItem_68SA
    {
        public int ReservaId { get; set; }
        public string Huesped { get; set; }
        public string Documento { get; set; }
        public string HuespedDisplay => $"{Huesped}\n{Documento}";

        public string Habitacion { get; set; }

        public string RegistradoPor { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string RegistroDisplay => string.IsNullOrEmpty(RegistradoPor) ? "-" : $"{RegistradoPor}\n{FechaRegistro:dd/MM/yyyy HH:mm}";

        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public decimal Costo { get; set; }
        public decimal Adelanto { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
        public bool PuedeCancelar { get; set; }
    }
}