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
using Service_08YS.Entities.Acceso;

namespace GUI_08YS.Recepcionista
{
    public partial class FormReservas_68SA : Form, IIdiomaObserver_08YS
    {
        private static readonly Dictionary<string, Permisos> _mapaPermisos =
            new Dictionary<string, Permisos>
            {
                { nameof(btnCrearReserva), Permisos.RegistrarReserva },
            };

        private static readonly Bitmap IconoVacio = new Bitmap(1, 1);

        // Íconos de acción y colores de estado se resuelven una vez por carga y se entregan en
        // CellFormatting: la grilla no recorre filas ni guarda imágenes por celda (filas "compartidas").
        private readonly Image _iconoEditar = IconCache.Get(IconChar.PenToSquare, IconFont.Auto, 36, Color.Goldenrod);
        private readonly Image _iconoImprimir = IconCache.Get(IconChar.Print, IconFont.Auto, 36, Color.Goldenrod);
        private readonly Image _iconoCancelar = IconCache.Get(IconChar.Ban, IconFont.Auto, 36, Color.FromArgb(235, 90, 90));
        private List<ReservaListItem_68SA> _items = new List<ReservaListItem_68SA>();
        private Dictionary<string, Color> _coloresEstado = new Dictionary<string, Color>();
        private int _colEstado = -1, _colEditar = -1, _colImprimir = -1, _colCancelar = -1;

        private readonly Action<Form> _openChildForm;
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private readonly HabitacionBLL_68SA _habitacionBLL = BLLFactory_08YS.CreateHabitacionBLL();
        private readonly PagoBLL_68SA _pagoBLL = BLLFactory_08YS.CreatePagoBLL();

        // Primer día visible del calendario: < y > lo corren un día, << y >> una semana o un mes completo
        private DateTime _fechaBaseCalendario = InicioAlineado(DateTime.Today, ModoCalendarioReservas.Semana);
        private readonly ToolTip _tooltipNavegacion = new ToolTip();
        private ModoCalendarioReservas _modoCalendario = ModoCalendarioReservas.Semana;

        public FormReservas_68SA(Action<Form> openChildForm)
        {
            _openChildForm = openChildForm;
            InitializeComponent();

            PermissionFilter_08YS.Aplicar(this, _mapaPermisos);

            ConfigurarGrid();

            // Filtros adaptables: cada grupo reparte su ancho entre sus campos (se ven bien en cualquier resolución)
            LayoutAdaptable_68SA.RepartirColumnas(pnlGrupoIngreso,
                new Control[] { lblEtiquetaFechaDesde, dtpFechaDesde }, new Control[] { lblEtiquetaFechaHasta, dtpFechaHasta });
            LayoutAdaptable_68SA.RepartirColumnas(pnlGrupoEgreso,
                new Control[] { lblEtiquetaFechaEgresoDesde, dtpFechaEgresoDesde }, new Control[] { lblEtiquetaFechaEgresoHasta, dtpFechaEgresoHasta });
            LayoutAdaptable_68SA.RepartirColumnas(pnlGrupoBotones,
                new Control[] { btnLimpiarFiltros }, new Control[] { btnFiltrar });
            pnlGrupoCosto.Resize += (s, e) => AcomodarGrupoCosto();

            CargarCombosFiltro();
            cmbEstadoFiltro.SelectedIndex = 0;
            cmbOperadorCosto.SelectedIndex = 0;
            cmbOperadorCosto.SelectedIndexChanged += (s, e) => ActualizarVisibilidadCosto();

            btnTabCalendario.Click += (s, e) => CambiarTab(false);
            btnTabLista.Click += (s, e) => CambiarTab(true);
            btnCrearReserva.Click += (s, e) => _openChildForm(new FormDisponibilidad(_openChildForm));
            btnCalAnteriorPeriodo.Click += (s, e) => MoverCalendario(-1, periodoCompleto: true);
            btnCalAnterior.Click += (s, e) => MoverCalendario(-1, periodoCompleto: false);
            btnCalSiguiente.Click += (s, e) => MoverCalendario(1, periodoCompleto: false);
            btnCalSiguientePeriodo.Click += (s, e) => MoverCalendario(1, periodoCompleto: true);
            btnCalHoy.Click += (s, e) => { _fechaBaseCalendario = InicioAlineado(DateTime.Today, _modoCalendario); CargarCalendario(); };
            TraducirTooltipsCalendario();
            btnCalVistaSemana.Click += (s, e) => CambiarVistaCalendario(ModoCalendarioReservas.Semana);
            btnCalVistaMes.Click += (s, e) => CambiarVistaCalendario(ModoCalendarioReservas.Mes);
            // Calendario: clic en una reserva abre su comprobante / factura; doble clic en un día libre, reserva esa habitación
            ucCalendario.PermiteCrearReservas = SessionManager_08YS.Instance.HasPermission(Permisos.RegistrarReserva)
                                             && SessionManager_08YS.Instance.HasPermission(Permisos.Cobrar);
            ucCalendario.ReservaClickeada += (s, reserva) => FormDocumentosReserva_68SA.Mostrar(this, reserva.Id);
            ucCalendario.NuevaReservaSolicitada += (s, e) => ReservarDesdeCalendario(e.Habitacion, e.Fecha);

            btnFiltrar.Click += (s, e) => CargarLista();
            btnLimpiarFiltros.Click += (s, e) => LimpiarFiltros();

            // La primera carga se dispara en Load, no acá — generar los íconos de la grilla
            // antes de que la ventana tenga su handle de Windows los deja sin pintar.
            this.Shown += (s, e) => CargarLista();
        }

        #region Grid

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
            dgvReservas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvReservas.AllowUserToResizeRows = false;
            // Sin doble buffer la grilla se repinta celda por celda al desplazarse (se "arrastra")
            LayoutAdaptable_68SA.HabilitarDobleBuffer(dgvReservas);
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
            var t = TraductorManager_08YS.Instance;
            void Configurar(string nombre, string encabezado, int peso, string formato = null)
            {
                if (dgvReservas.Columns[nombre] == null) return;
                dgvReservas.Columns[nombre].HeaderText = encabezado;
                dgvReservas.Columns[nombre].FillWeight = peso;
                if (formato != null) dgvReservas.Columns[nombre].DefaultCellStyle.Format = formato;
            }

            Configurar("ReservaId", t.GetTexto("Reservas_colCodigo"), 55);
            Configurar("HuespedDisplay", t.GetTexto("Reservas_colHuesped"), 190);
            Configurar("Habitacion", t.GetTexto("Reservas_colHabitacion"), 60);
            Configurar("RegistroDisplay", t.GetTexto("Reservas_colRegistro"), 170);
            Configurar("FechaEntrada", t.GetTexto("Reservas_colEntrada"), 90, "dd/MM/yyyy");
            Configurar("FechaSalida", t.GetTexto("Reservas_colSalida"), 90, "dd/MM/yyyy");
            Configurar("Costo", t.GetTexto("Reservas_colCosto"), 90, "$#,##0.00");
            Configurar("Adelanto", t.GetTexto("Reservas_colAdelanto"), 90, "$#,##0.00");
            Configurar("Saldo", t.GetTexto("Reservas_colSaldo"), 90, "$#,##0.00");
            Configurar("Estado", t.GetTexto("Reservas_colEstado"), 90);

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
            dgvReservas.Columns["colEditar"].Visible = SessionManager_08YS.Instance.HasPermission(Permisos.ModificarReserva);
            dgvReservas.Columns["colImprimir"].Visible = SessionManager_08YS.Instance.HasPermission(Permisos.ImprimirComprobante);
            dgvReservas.Columns["colCancelar"].Visible = SessionManager_08YS.Instance.HasPermission(Permisos.CancelarReserva);

            _colEstado = dgvReservas.Columns["Estado"]?.Index ?? -1;
            _colEditar = dgvReservas.Columns["colEditar"].Index;
            _colImprimir = dgvReservas.Columns["colImprimir"].Index;
            _colCancelar = dgvReservas.Columns["colCancelar"].Index;
        }

        private static DataGridViewImageColumn CrearColumnaIcono(string nombre)
        {
            var columna = new DataGridViewImageColumn
            {
                Name = nombre,
                HeaderText = "",
                Width = 42,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ImageLayout = DataGridViewImageCellLayout.Normal // el ícono ya viene del tamaño justo: no se reescala en cada pintado
            };
            columna.DefaultCellStyle.BackColor = Color.FromArgb(10, 18, 50);
            columna.DefaultCellStyle.SelectionBackColor = Color.FromArgb(15, 25, 55);
            columna.DefaultCellStyle.NullValue = null;
            return columna;
        }

        private void DgvReservas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (e.ColumnIndex == _colEstado)
            {
                if (e.Value is string estado && _coloresEstado.TryGetValue(estado, out Color color))
                    e.CellStyle.ForeColor = color;
            }
            else if (e.ColumnIndex == _colEditar)
            {
                e.Value = _iconoEditar;
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == _colImprimir)
            {
                e.Value = _iconoImprimir;
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == _colCancelar)
            {
                // Se lee de la lista enlazada (no de Rows[i]) para no "descompartir" la fila
                bool puedeCancelar = e.RowIndex < _items.Count && _items[e.RowIndex].PuedeCancelar;
                e.Value = puedeCancelar ? _iconoCancelar : IconoVacio;
                e.FormattingApplied = true;
            }
        }

        private void DgvReservas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (!(dgvReservas.Rows[e.RowIndex].DataBoundItem is ReservaListItem_68SA item)) return;

            string columna = dgvReservas.Columns[e.ColumnIndex].Name;

            if (columna == "colEditar")
            {
                if (item.PuedeCancelar) // mismo criterio que Cancelar: solo Pendiente (Confirmada)
                {
                    var reserva = _reservaBLL.GetById(item.ReservaId);
                    using (var popup = new FormModificarReserva_68SA(reserva))
                    {
                        if (popup.ShowDialog(this) == DialogResult.OK)
                            CargarLista();
                    }
                }
                else
                {
                    MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("Reservas_msgSoloPendientes"), TraductorManager_08YS.Instance.GetTexto("Reservas_tituloNoDisponible"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (columna == "colImprimir")
                GenerarYAbrirFactura(item.ReservaId);
            else if (columna == "colCancelar" && item.PuedeCancelar)
                CancelarReserva(item.ReservaId);
        }

        #endregion

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

        #region Filtros

        private const int OperadorCostoCualquiera = 0;
        private const int OperadorCostoMayorA = 1;
        private const int OperadorCostoMenorA = 2;
        private const int OperadorCostoEntre = 3;

        // Combos y placeholders de los filtros en el idioma activo (conserva la selección al cambiar de idioma)
        private void CargarCombosFiltro()
        {
            var t = TraductorManager_08YS.Instance;

            OpcionEnum_68SA.Cargar(cmbEstadoFiltro, typeof(EstadoReserva), t.GetTexto("Crud_todos"));

            int? operador = ItemCombo_68SA.IdSeleccionado(cmbOperadorCosto);
            cmbOperadorCosto.Items.Clear();
            cmbOperadorCosto.Items.Add(new ItemCombo_68SA(OperadorCostoCualquiera, t.GetTexto("Reservas_costoCualquiera")));
            cmbOperadorCosto.Items.Add(new ItemCombo_68SA(OperadorCostoMayorA, t.GetTexto("Reservas_costoMayorA")));
            cmbOperadorCosto.Items.Add(new ItemCombo_68SA(OperadorCostoMenorA, t.GetTexto("Reservas_costoMenorA")));
            cmbOperadorCosto.Items.Add(new ItemCombo_68SA(OperadorCostoEntre, t.GetTexto("Reservas_costoEntre")));
            if (operador.HasValue) ItemCombo_68SA.Seleccionar(cmbOperadorCosto, operador);

            txtHuespedFiltro.PlaceholderText = t.GetTexto("Reservas_phHuespedFiltro");
            txtHabitacionFiltro.PlaceholderText = t.GetTexto("Reservas_phHabitacionFiltro");
            txtRegistroFiltro.PlaceholderText = t.GetTexto("Reservas_phRegistroFiltro");
        }

        private void ActualizarVisibilidadCosto()
        {
            int operador = ItemCombo_68SA.IdSeleccionado(cmbOperadorCosto) ?? OperadorCostoCualquiera;
            nudCostoDesde.Visible = operador != OperadorCostoCualquiera;
            lblGuionCosto.Visible = operador == OperadorCostoEntre;
            nudCostoHasta.Visible = operador == OperadorCostoEntre;
            AcomodarGrupoCosto();
        }

        // Operador (40%) + uno o dos importes que se reparten el resto del ancho del grupo
        private void AcomodarGrupoCosto()
        {
            int ancho = pnlGrupoCosto.ClientSize.Width;
            if (ancho <= 0) return;

            const int separacion = 8;
            int anchoCombo = Math.Max(90, ancho * 40 / 100);
            cmbOperadorCosto.Left = 0;
            cmbOperadorCosto.Width = anchoCombo;

            int inicio = anchoCombo + separacion;
            int resto = Math.Max(0, ancho - inicio);
            // Se decide por el operador elegido (Visible devuelve false mientras la pestaña está oculta)
            if (ItemCombo_68SA.IdSeleccionado(cmbOperadorCosto) == OperadorCostoEntre)
            {
                int anchoGuion = lblGuionCosto.Width + 6;
                int anchoImporte = Math.Max(40, (resto - anchoGuion) / 2);
                nudCostoDesde.Left = inicio;
                nudCostoDesde.Width = anchoImporte;
                lblGuionCosto.Left = nudCostoDesde.Right + 3;
                nudCostoHasta.Left = ancho - anchoImporte;
                nudCostoHasta.Width = anchoImporte;
            }
            else
            {
                nudCostoDesde.Left = inicio;
                nudCostoDesde.Width = Math.Max(40, resto);
            }
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
                Estado = OpcionEnum_68SA.Seleccionado<EstadoReserva>(cmbEstadoFiltro),
                FechaDesde = dtpFechaDesde.Value,
                FechaHasta = dtpFechaHasta.Value,
                FechaEgresoDesde = dtpFechaEgresoDesde.Value,
                FechaEgresoHasta = dtpFechaEgresoHasta.Value
            };

            switch (ItemCombo_68SA.IdSeleccionado(cmbOperadorCosto) ?? OperadorCostoCualquiera)
            {
                case OperadorCostoMayorA:
                    filtro.CostoDesde = nudCostoDesde.Value;
                    break;
                case OperadorCostoMenorA:
                    filtro.CostoHasta = nudCostoDesde.Value;
                    break;
                case OperadorCostoEntre:
                    filtro.CostoDesde = nudCostoDesde.Value;
                    filtro.CostoHasta = nudCostoHasta.Value;
                    break;
            }

            return filtro;
        }

        #endregion

        #region Carga

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

            _coloresEstado = new Dictionary<string, Color>
            {
                [TextoEstadoReserva(EstadoReserva.Confirmada)] = Color.FromArgb(90, 155, 235),
                [TextoEstadoReserva(EstadoReserva.EnCurso)] = Color.FromArgb(76, 217, 100),
                [TextoEstadoReserva(EstadoReserva.Finalizada)] = Color.FromArgb(160, 165, 180),
                [TextoEstadoReserva(EstadoReserva.Cancelada)] = Color.FromArgb(235, 90, 90)
            };

            dgvReservas.SuspendLayout();
            _items = items;
            // Las columnas se regeneran al volver a enlazar: los índices guardados dejan de valer
            _colEstado = _colEditar = _colImprimir = _colCancelar = -1;
            dgvReservas.DataSource = null;
            dgvReservas.DataSource = items;
            AplicarColumnasGrid();
            dgvReservas.ResumeLayout();
        }
        private void CargarCalendario()
        {
            DateTime desde = _fechaBaseCalendario.Date;
            DateTime hasta = _modoCalendario == ModoCalendarioReservas.Semana ? desde.AddDays(7) : desde.AddMonths(1);

            // Un mes que arranca el día 1 se titula con su nombre; si se corrió de a días, con el rango
            var cultura = OpcionEnum_68SA.CulturaActual();
            lblCalPeriodo.Text = _modoCalendario == ModoCalendarioReservas.Mes && desde.Day == 1
                ? cultura.TextInfo.ToTitleCase(desde.ToString("MMMM yyyy", cultura))
                : $"{desde:dd/MM} – {hasta.AddDays(-1):dd/MM/yyyy}";

            var habitaciones = _habitacionBLL.GetAll();
            // Un día antes: la reserva que sale el primer día visible ocupa su mañana (hasta el check-out)
            var reservas = _reservaBLL.GetEnRangoVisible(desde.AddDays(-1), hasta);

            TimeSpan horaCheckIn = TimeSpan.FromHours(14), horaCheckOut = TimeSpan.FromHours(10);
            try
            {
                var configuracion = BLLFactory_08YS.CreateConfiguracionHotelBLL().GetConfiguracion();
                if (configuracion != null)
                {
                    horaCheckIn = configuracion.HoraCheckIn;
                    horaCheckOut = configuracion.HoraCheckOut;
                }
            }
            catch { /* se usan los horarios por defecto */ }

            ucCalendario.Cargar(habitaciones, reservas, desde, hasta, _modoCalendario, horaCheckIn, horaCheckOut);
        }

        private void ReservarDesdeCalendario(Habitacion_68SA habitacion, DateTime fecha)
        {
            try
            {
                SessionManager_08YS.Instance.ValidatePermission(Permisos.RegistrarReserva);
                // Se relee la habitación completa (tipo con tarifa y capacidad) para el alta
                var completa = _habitacionBLL.GetById(habitacion.Id);
                _openChildForm(new FormRegistrarReserva_68SA(_openChildForm, completa, ModoHabitaciones.Reserva, fecha, fecha.AddDays(1)));
            }
            catch (UnauthorizedAccessException)
            {
                throw; // lo muestra el manejador global de permisos
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TraductorManager_08YS.Instance.GetTexto("Comun_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        /// <summary>
        /// Semana: arranca el lunes. Mes: arranca el día 1. (Punto de partida al tocar "Hoy" o cambiar de vista.)
        /// </summary>
        private static DateTime InicioAlineado(DateTime fecha, ModoCalendarioReservas modo)
        {
            if (modo == ModoCalendarioReservas.Mes)
                return new DateTime(fecha.Year, fecha.Month, 1);
            int diasDesdeElLunes = ((int)fecha.DayOfWeek + 6) % 7;
            return fecha.Date.AddDays(-diasDesdeElLunes);
        }

        /// <param name="sentido">-1 hacia atrás, 1 hacia adelante.</param>
        /// <param name="periodoCompleto">true: una semana o un mes (según la vista); false: un día.</param>
        private void MoverCalendario(int sentido, bool periodoCompleto)
        {
            if (!periodoCompleto)
                _fechaBaseCalendario = _fechaBaseCalendario.AddDays(sentido);
            else if (_modoCalendario == ModoCalendarioReservas.Semana)
                _fechaBaseCalendario = _fechaBaseCalendario.AddDays(7 * sentido);
            else
                _fechaBaseCalendario = _fechaBaseCalendario.AddMonths(sentido);
            CargarCalendario();
        }

        private void TraducirTooltipsCalendario()
        {
            var t = TraductorManager_08YS.Instance;
            bool semana = _modoCalendario == ModoCalendarioReservas.Semana;
            _tooltipNavegacion.SetToolTip(btnCalAnterior, t.GetTexto("Reservas_ttDiaAnterior"));
            _tooltipNavegacion.SetToolTip(btnCalSiguiente, t.GetTexto("Reservas_ttDiaSiguiente"));
            _tooltipNavegacion.SetToolTip(btnCalAnteriorPeriodo, t.GetTexto(semana ? "Reservas_ttSemanaAnterior" : "Reservas_ttMesAnterior"));
            _tooltipNavegacion.SetToolTip(btnCalSiguientePeriodo, t.GetTexto(semana ? "Reservas_ttSemanaSiguiente" : "Reservas_ttMesSiguiente"));
        }

        private void CambiarVistaCalendario(ModoCalendarioReservas modo)
        {
            _modoCalendario = modo;
            _fechaBaseCalendario = InicioAlineado(_fechaBaseCalendario, modo);
            TraducirTooltipsCalendario();

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

        private void GenerarYAbrirFactura(int reservaId)
        {
            try
            {
                SessionManager_08YS.Instance.ValidatePermission(Permisos.ImprimirComprobante);

                // Visor con la factura (y el comprobante): imprimir, guardar PDF o reenviar por mail
                FormDocumentosReserva_68SA.Mostrar(this, reservaId, indiceInicial: 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(TraductorManager_08YS.Instance.GetTexto("Reservas_msgErrorImprimir"), ex.Message), TraductorManager_08YS.Instance.GetTexto("Comun_error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Idioma
        public void UpdateIdioma()
        {
            TraducirControles(this);
            CargarCombosFiltro();
            TraducirTooltipsCalendario();

            // Si ya hay datos cargados, se recargan para traducir estados y encabezados
            if (dgvReservas.DataSource != null)
            {
                CargarLista();
                if (pnlCalendario.Visible) CargarCalendario();
            }
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