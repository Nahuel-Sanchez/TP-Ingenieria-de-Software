using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using CustomControls;
using FontAwesome.Sharp;
using GUI_08YS.Recepcionista.Dashboard;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using static GUI_08YS.Recepcionista.UiCargos_68SA;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Reporte de servicios consumidos por huésped (RFN2): filtros por período, tipo, estado y huésped;
    /// indicadores del período; detalle agrupado por huésped con su total acumulado; composición por tipo
    /// y productos más pedidos. Desde acá se abre la vista previa para imprimir o guardar en PDF.
    /// </summary>
    public partial class FormReporteServicios_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private static readonly Color Tarjeta = Color.FromArgb(10, 18, 50);
        private static readonly Color FondoGrupo = Color.FromArgb(20, 34, 78);
        private static readonly Color Pista = Color.FromArgb(30, 42, 80);
        private static readonly Font FuenteNegrita = new Font("Segoe UI", 9F, FontStyle.Bold);

        private readonly ReporteServiciosBLL_68SA _bll = BLLFactory_08YS.CreateReporteServiciosBLL();
        private readonly Dictionary<string, TarjetaKpi> _kpis = new Dictionary<string, TarjetaKpi>();
        private ReporteServicios_68SA _reporte;
        private Periodo _periodo = Periodo.EsteMes;

        private const string ColHabitacion = "colHabitacion", ColTipo = "colTipo", ColDescripcion = "colDescripcion",
                             ColDetalle = "colDetalle", ColFecha = "colFecha", ColEstado = "colEstado", ColMonto = "colMonto";

        #endregion

        #region Constructor

        /// <param name="filtroInicial">Período con el que se abre (por ejemplo, el elegido en el Dashboard).</param>
        public FormReporteServicios_68SA(FiltroServicios_68SA filtroInicial = null)
        {
            InitializeComponent();

            ConfigurarGrilla();
            CrearKpis();

            btnEsteMes.Click += (s, e) => AplicarPeriodo(Periodo.EsteMes);
            btn30Dias.Click += (s, e) => AplicarPeriodo(Periodo.Ultimos30);
            btnEsteAnio.Click += (s, e) => AplicarPeriodo(Periodo.EsteAnio);
            btnHistorico.Click += (s, e) => AplicarPeriodo(Periodo.Historico);
            btnAplicar.Click += (s, e) => { if (_periodo != Periodo.Historico) MarcarPeriodo(Periodo.Personalizado); Generar(); };
            btnLimpiar.Click += (s, e) => Limpiar();
            btnImprimir.Click += (s, e) => Imprimir();
            txtHuesped.InnerTextBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                Generar();
            };

            pnlLeyenda.Paint += PintarLeyenda;
            pnlTopProductos.Paint += PintarTopProductos;
            pnlTopProductos.Resize += (s, e) => pnlTopProductos.Invalidate();
            LayoutAdaptable_68SA.HabilitarDobleBuffer(pnlLeyenda);
            LayoutAdaptable_68SA.HabilitarDobleBuffer(pnlTopProductos);

            AplicarTextos();

            if (filtroInicial != null && !filtroInicial.EsHistorico)
            {
                dtpDesde.Value = filtroInicial.Desde ?? DateTime.Today.AddDays(-30);
                dtpHasta.Value = filtroInicial.Hasta ?? DateTime.Today;
                MarcarPeriodo(Periodo.Personalizado);
            }
            else if (filtroInicial != null)
                MarcarPeriodo(Periodo.Historico);
            else
                FijarFechas(Periodo.EsteMes);

            Shown += (s, e) => Generar();
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            AplicarTextos();
            if (_reporte != null) Mostrar();
        }

        private void AplicarTextos()
        {
            Traducir(this);
            txtHuesped.PlaceholderText = T("RepServ_phHuesped");
            OpcionEnum_68SA.Cargar(cmbTipo, typeof(TipoCargo), T("RepServ_txtTodos"));
            OpcionEnum_68SA.Cargar(cmbEstado, typeof(EstadoServicio), T("RepServ_txtTodos"));
            if (cmbTipo.SelectedIndex < 0) cmbTipo.SelectedIndex = 0;
            if (cmbEstado.SelectedIndex < 0) cmbEstado.SelectedIndex = 0;
            foreach (var kpi in _kpis.Values) kpi.Titulo.Text = T(kpi.ClaveTitulo).ToUpper();
            if (dgvServicios.Columns.Count > 0) TitulosColumnas();
            EstiloPeriodo();
        }

        #endregion

        #region Filtros

        private void AplicarPeriodo(Periodo periodo)
        {
            FijarFechas(periodo);
            Generar();
        }

        private void FijarFechas(Periodo periodo)
        {
            DateTime hoy = DateTime.Today;
            switch (periodo)
            {
                case Periodo.EsteMes: dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1); dtpHasta.Value = hoy; break;
                case Periodo.Ultimos30: dtpDesde.Value = hoy.AddDays(-30); dtpHasta.Value = hoy; break;
                case Periodo.EsteAnio: dtpDesde.Value = new DateTime(hoy.Year, 1, 1); dtpHasta.Value = hoy; break;
            }
            MarcarPeriodo(periodo);
        }

        private void MarcarPeriodo(Periodo periodo)
        {
            _periodo = periodo;
            dtpDesde.Enabled = dtpHasta.Enabled = periodo != Periodo.Historico;
            EstiloPeriodo();
        }

        private void EstiloPeriodo()
        {
            var botones = new (IconButton Boton, Periodo Periodo)[]
            {
                (btnEsteMes, Periodo.EsteMes), (btn30Dias, Periodo.Ultimos30), (btnEsteAnio, Periodo.EsteAnio), (btnHistorico, Periodo.Historico)
            };
            foreach (var (boton, periodo) in botones)
            {
                bool activo = periodo == _periodo;
                boton.BackColor = activo ? Color.Goldenrod : FondoCampo;
                boton.ForeColor = activo ? Color.FromArgb(10, 15, 35) : Color.Goldenrod;
                boton.IconColor = activo ? Color.FromArgb(10, 15, 35) : Color.Gold;
            }
        }

        private void Limpiar()
        {
            txtHuesped.Text = string.Empty;
            cmbTipo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            AplicarPeriodo(Periodo.EsteMes);
        }

        private FiltroServicios_68SA ArmarFiltro()
        {
            bool historico = _periodo == Periodo.Historico;
            return new FiltroServicios_68SA
            {
                Desde = historico ? null : dtpDesde.Value?.Date,
                Hasta = historico ? null : dtpHasta.Value?.Date,
                Tipo = OpcionEnum_68SA.Seleccionado<TipoCargo>(cmbTipo),
                Estado = OpcionEnum_68SA.Seleccionado<EstadoServicio>(cmbEstado),
                Huesped = string.IsNullOrWhiteSpace(txtHuesped.Text) ? null : txtHuesped.Text.Trim()
            };
        }

        #endregion

        #region Generar y mostrar

        private void Generar()
        {
            var filtro = ArmarFiltro();
            if (_periodo != Periodo.Historico && (!filtro.Desde.HasValue || !filtro.Hasta.HasValue))
            {
                Aviso(this, T("Dashboard_msgFaltaPeriodo"), T("Dashboard_tituloFaltaPeriodo"));
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                _reporte = _bll.Generar(filtro);
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                Aviso(this, Describir(ex));
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            Mostrar();
        }

        private void Mostrar()
        {
            var r = _reporte;
            int pedidos = r.Cantidad(TipoCargo.RoomService);
            int upgrades = r.Cantidad(TipoCargo.Upgrade);

            Kpi("total", Moneda(r.Total), string.Format(T("RepServ_kpiTotalDetalle"), r.Servicios.Count, r.CantidadEstadias));
            Kpi("roomservice", Moneda(r.Monto(TipoCargo.RoomService)), string.Format(T("RepServ_kpiRoomServiceDetalle"), pedidos, r.Pendientes));
            Kpi("upgrade", Moneda(r.Monto(TipoCargo.Upgrade)), string.Format(T("RepServ_kpiUpgradeDetalle"), upgrades));
            Kpi("huespedes", r.CantidadHuespedes.ToString(), string.Format(T("RepServ_kpiHuespedesDetalle"), Moneda(r.PromedioPorHuesped)));
            Kpi("participacion", r.Participacion.ToString("0.#") + "%", string.Format(T("RepServ_kpiParticipacionDetalle"), Moneda(r.AlojamientoPeriodo)));
            Kpi("entrega", r.MinutosEntregaPromedio.HasValue ? string.Format(T("RepServ_txtMinutos"), Math.Round(r.MinutosEntregaPromedio.Value)) : "—",
                T(r.MinutosEntregaPromedio.HasValue ? "RepServ_kpiEntregaDetalle" : "RepServ_kpiEntregaSinDatos"));

            LlenarGrilla();

            lblDetalleResumen.Text = string.Format(T("RepServ_txtResumen"), r.CantidadHuespedes, r.Servicios.Count, Moneda(r.Total));
            var mayor = r.MayorConsumo;
            lblMayorConsumo.Text = mayor == null ? string.Empty
                : string.Format(T("RepServ_txtMayorConsumo"), mayor.NombreCompleto, Moneda(mayor.Total));

            var segmentos = new List<SegmentoDonut_68SA>
            {
                new SegmentoDonut_68SA { Etiqueta = OpcionEnum_68SA.Texto(TipoCargo.RoomService), Valor = r.Monto(TipoCargo.RoomService), Color = ReporteServiciosTextos_68SA.ColorTipo(TipoCargo.RoomService) },
                new SegmentoDonut_68SA { Etiqueta = OpcionEnum_68SA.Texto(TipoCargo.Upgrade), Valor = r.Monto(TipoCargo.Upgrade), Color = ReporteServiciosTextos_68SA.ColorTipo(TipoCargo.Upgrade) }
            };
            ucDonut.Cargar(segmentos, r.Servicios.Count.ToString(), T("RepServ_txtServicios"));
            pnlLeyenda.Invalidate();
            pnlTopProductos.Invalidate();

            btnImprimir.Enabled = !r.Vacio;
        }

        private void Kpi(string clave, string valor, string detalle)
        {
            _kpis[clave].Valor.Text = valor;
            _kpis[clave].Detalle.Text = detalle;
        }

        #endregion

        #region Indicadores

        private void CrearKpis()
        {
            var definiciones = new (string Clave, string Titulo, IconChar Icono, Color Color)[]
            {
                ("total",         "RepServ_kpiTotal",         IconChar.Receipt,          Color.Goldenrod),
                ("roomservice",   "RepServ_kpiRoomService",   IconChar.BellConcierge,    ReporteServiciosTextos_68SA.ColorTipo(TipoCargo.RoomService)),
                ("upgrade",       "RepServ_kpiUpgrade",       IconChar.ArrowUpRightDots, ReporteServiciosTextos_68SA.ColorTipo(TipoCargo.Upgrade)),
                ("huespedes",     "RepServ_kpiHuespedes",     IconChar.UsersLine,        Color.FromArgb(90, 155, 235)),
                ("participacion", "RepServ_kpiParticipacion", IconChar.ChartPie,         Color.FromArgb(235, 140, 60)),
                ("entrega",       "RepServ_kpiEntrega",       IconChar.Stopwatch,        Color.FromArgb(80, 200, 200)),
            };

            tlpKpis.SuspendLayout();
            for (int i = 0; i < definiciones.Length; i++)
            {
                var (clave, titulo, icono, acento) = definiciones[i];
                var panel = new RoundedPanel_68SA
                {
                    BackColor = Tarjeta,
                    CornerRadius = 12,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(i == 0 ? 0 : 6, 0, i == definiciones.Length - 1 ? 0 : 6, 0)
                };

                var barra = new Panel { BackColor = acento, Dock = DockStyle.Left, Width = 4 };
                var icon = new IconPictureBox
                {
                    BackColor = Tarjeta, IconChar = icono, IconColor = acento, IconSize = 24, Size = new Size(26, 26),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                var lblTitulo = new Label
                {
                    AutoSize = false, AutoEllipsis = true, Location = new Point(16, 12), Height = 18,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Segoe UI", 7.75F, FontStyle.Bold), ForeColor = Color.FromArgb(160, 165, 180)
                };
                var lblValor = new Label
                {
                    AutoSize = false, AutoEllipsis = true, Location = new Point(14, 32), Height = 34,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.WhiteSmoke, Text = "—"
                };
                var lblDetalle = new Label
                {
                    AutoSize = false, AutoEllipsis = true, Location = new Point(16, 70), Height = 18,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Segoe UI", 8.25F), ForeColor = Color.FromArgb(150, 156, 175)
                };

                panel.Controls.Add(icon);
                panel.Controls.Add(lblTitulo);
                panel.Controls.Add(lblValor);
                panel.Controls.Add(lblDetalle);
                panel.Controls.Add(barra);
                panel.Resize += (s, e) =>
                {
                    icon.Location = new Point(panel.ClientSize.Width - icon.Width - 14, 12);
                    int ancho = Math.Max(40, panel.ClientSize.Width - 30);
                    lblTitulo.Width = ancho - icon.Width - 6;
                    lblValor.Width = ancho;
                    lblDetalle.Width = ancho;
                };
                tlpKpis.Controls.Add(panel, i, 0);

                _kpis[clave] = new TarjetaKpi { Titulo = lblTitulo, Valor = lblValor, Detalle = lblDetalle, ClaveTitulo = titulo };
            }
            tlpKpis.ResumeLayout(true);
        }

        #endregion

        #region Grilla agrupada por huésped

        private void ConfigurarGrilla()
        {
            CrudEstilo_68SA.AplicarGrilla(dgvServicios, 36);
            dgvServicios.AutoGenerateColumns = false;
            dgvServicios.BackgroundColor = Tarjeta;
            dgvServicios.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvServicios.ShowCellToolTips = true;

            void Columna(string nombre, float peso, DataGridViewContentAlignment alineacion = DataGridViewContentAlignment.MiddleLeft)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = nombre,
                    FillWeight = peso,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                col.DefaultCellStyle.Alignment = alineacion;
                col.HeaderCell.Style.Alignment = alineacion;
                dgvServicios.Columns.Add(col);
            }

            Columna(ColHabitacion, 9, DataGridViewContentAlignment.MiddleCenter);
            Columna(ColTipo, 13);
            Columna(ColDescripcion, 20);
            Columna(ColDetalle, 32);
            Columna(ColFecha, 13);
            Columna(ColEstado, 11);
            Columna(ColMonto, 12, DataGridViewContentAlignment.MiddleRight);
            TitulosColumnas();

            dgvServicios.RowPrePaint += Dgv_RowPrePaint;
            dgvServicios.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvServicios.Rows[e.RowIndex].Tag is ServicioConsumido_68SA servicio)
                    using (var detalle = new FormDetalleEstadia_68SA(servicio.ReservaId))
                        detalle.ShowDialog(this);
            };
            // Los renglones de huésped son títulos: no se seleccionan
            dgvServicios.SelectionChanged += (s, e) =>
            {
                if (dgvServicios.CurrentRow?.Tag is HuespedServicios_68SA)
                    dgvServicios.CurrentRow.Selected = false;
            };
        }

        private void TitulosColumnas()
        {
            dgvServicios.Columns[ColHabitacion].HeaderText = T("RepServ_colHabitacion");
            dgvServicios.Columns[ColTipo].HeaderText = T("RepServ_colTipo");
            dgvServicios.Columns[ColDescripcion].HeaderText = T("RepServ_colDescripcion");
            dgvServicios.Columns[ColDetalle].HeaderText = T("RepServ_colDetalle");
            dgvServicios.Columns[ColFecha].HeaderText = T("RepServ_colFecha");
            dgvServicios.Columns[ColEstado].HeaderText = T("RepServ_colEstado");
            dgvServicios.Columns[ColMonto].HeaderText = T("RepServ_colMonto");
        }

        private void LlenarGrilla()
        {
            dgvServicios.SuspendLayout();
            dgvServicios.Rows.Clear();

            foreach (var huesped in _reporte.Huespedes)
            {
                int iGrupo = dgvServicios.Rows.Add();
                var grupo = dgvServicios.Rows[iGrupo];
                grupo.Tag = huesped;
                grupo.Height = 44;

                foreach (var servicio in huesped.Servicios)
                {
                    int i = dgvServicios.Rows.Add(
                        servicio.NroHabitacion,
                        OpcionEnum_68SA.Texto(servicio.Tipo),
                        ReporteServiciosTextos_68SA.Descripcion(servicio),
                        ReporteServiciosTextos_68SA.Detalle(servicio, Moneda),
                        servicio.Fecha.ToString("dd/MM/yyyy HH:mm"),
                        OpcionEnum_68SA.Texto(servicio.Estado),
                        Moneda(servicio.Monto));
                    var fila = dgvServicios.Rows[i];
                    fila.Tag = servicio;
                    fila.Cells[ColTipo].Style.ForeColor = ReporteServiciosTextos_68SA.ColorTipo(servicio.Tipo);
                    fila.Cells[ColTipo].Style.Font = FuenteNegrita;
                    fila.Cells[ColEstado].Style.ForeColor = ReporteServiciosTextos_68SA.ColorEstado(servicio.Estado);
                    fila.Cells[ColEstado].Style.Font = FuenteNegrita;
                    fila.Cells[ColDetalle].ToolTipText = ReporteServiciosTextos_68SA.Detalle(servicio, Moneda);
                    fila.Cells[ColDescripcion].ToolTipText = string.IsNullOrWhiteSpace(servicio.Usuario)
                        ? servicio.Descripcion
                        : string.Format(T("RepServ_txtRegistradoPor"), servicio.Descripcion, servicio.Usuario);
                }
            }

            dgvServicios.ResumeLayout();
            dgvServicios.ClearSelection();
            lblVacio.Visible = _reporte.Vacio;
            dgvServicios.Visible = !_reporte.Vacio;
        }

        /// <summary>Renglón del huésped: nombre, documento e ID a la izquierda y el total acumulado a la derecha.</summary>
        private void Dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (!(dgvServicios.Rows[e.RowIndex].Tag is HuespedServicios_68SA h)) return;

            var g = e.Graphics;
            var r = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgvServicios.Columns.GetColumnsWidth(DataGridViewElementStates.Visible), e.RowBounds.Height);
            using (var fondo = new SolidBrush(FondoGrupo))
                g.FillRectangle(fondo, r);
            using (var barra = new SolidBrush(Color.Goldenrod))
                g.FillRectangle(barra, r.Left, r.Top + 6, 4, r.Height - 12);

            using (var fNombre = new Font("Segoe UI", 10F, FontStyle.Bold))
            using (var fDato = new Font("Segoe UI", 8.75F))
            using (var fTotal = new Font("Segoe UI", 10F, FontStyle.Bold))
            {
                string total = string.Format(T("RepServ_txtTotalHuesped"), Moneda(h.Total));
                Size medidaTotal = TextRenderer.MeasureText(total, fTotal);
                var zonaTotal = new Rectangle(r.Right - medidaTotal.Width - 16, r.Top, medidaTotal.Width + 4, r.Height);
                TextRenderer.DrawText(g, total, fTotal, zonaTotal, Color.Goldenrod, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

                string nombre = h.NombreCompleto;
                Size medidaNombre = TextRenderer.MeasureText(nombre, fNombre);
                int x = r.Left + 16;
                int limite = zonaTotal.Left - 12;
                TextRenderer.DrawText(g, nombre, fNombre, new Rectangle(x, r.Top, Math.Max(0, limite - x), r.Height), Color.WhiteSmoke,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);

                x += medidaNombre.Width + 10;
                string datos = string.Format(T("RepServ_txtDatosHuesped"), h.Documento, h.HuespedId, h.Cantidad, h.Estadias);
                if (x < limite)
                    TextRenderer.DrawText(g, datos, fDato, new Rectangle(x, r.Top, limite - x, r.Height), Color.FromArgb(160, 165, 180),
                        TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            }

            e.Handled = true;
        }

        #endregion

        #region Panel lateral

        private void PintarLeyenda(object sender, PaintEventArgs e)
        {
            if (_reporte == null) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            decimal total = _reporte.Total;
            int y = 4;
            foreach (var tipo in new[] { TipoCargo.RoomService, TipoCargo.Upgrade })
            {
                decimal monto = _reporte.Monto(tipo);
                decimal porcentaje = total > 0 ? monto / total * 100 : 0;
                using (var punto = new SolidBrush(ReporteServiciosTextos_68SA.ColorTipo(tipo)))
                    g.FillEllipse(punto, 0, y + 5, 10, 10);
                using (var f1 = new Font("Segoe UI", 9F, FontStyle.Bold))
                using (var f2 = new Font("Segoe UI", 12F, FontStyle.Bold))
                using (var f3 = new Font("Segoe UI", 8.25F))
                {
                    int ancho = pnlLeyenda.ClientSize.Width - 18;
                    TextRenderer.DrawText(g, OpcionEnum_68SA.Texto(tipo), f1, new Rectangle(18, y, ancho, 20), Color.WhiteSmoke, TextFormatFlags.EndEllipsis);
                    TextRenderer.DrawText(g, Moneda(monto), f2, new Rectangle(18, y + 20, ancho, 24), Color.Goldenrod, TextFormatFlags.EndEllipsis);
                    TextRenderer.DrawText(g, string.Format(T("RepServ_txtLeyenda"), _reporte.Cantidad(tipo), porcentaje.ToString("0.#")), f3,
                        new Rectangle(18, y + 46, ancho, 18), Color.FromArgb(160, 165, 180), TextFormatFlags.EndEllipsis);
                }
                y += 78;
            }
        }

        /// <summary>Barras horizontales con las unidades pedidas de cada producto (los 6 primeros).</summary>
        private void PintarTopProductos(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var productos = _reporte?.Productos.Take(6).ToList() ?? new List<ProductoConsumido_68SA>();
            int ancho = pnlTopProductos.ClientSize.Width;

            if (productos.Count == 0)
            {
                using (var f = new Font("Segoe UI", 9F))
                    TextRenderer.DrawText(g, T("RepServ_txtSinProductos"), f, new Rectangle(0, 0, ancho, 40), Color.FromArgb(160, 165, 180),
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                return;
            }

            int maximo = Math.Max(1, productos.Max(p => p.Cantidad));
            int alto = Math.Max(30, Math.Min(36, pnlTopProductos.ClientSize.Height / productos.Count));
            using (var fNombre = new Font("Segoe UI", 9F))
            using (var fCant = new Font("Segoe UI", 8.75F, FontStyle.Bold))
            using (var pista = new SolidBrush(Pista))
            using (var relleno = new LinearGradientBrush(new Rectangle(0, 0, Math.Max(1, ancho), 6), Color.FromArgb(184, 134, 11), Color.Gold, LinearGradientMode.Horizontal))
            {
                for (int i = 0; i < productos.Count; i++)
                {
                    var p = productos[i];
                    int y = i * alto;
                    string cantidad = string.Format(T("RepServ_txtUnidades"), p.Cantidad);
                    Size medida = TextRenderer.MeasureText(cantidad, fCant);
                    TextRenderer.DrawText(g, p.Nombre, fNombre, new Rectangle(0, y, ancho - medida.Width - 8, 18), Color.WhiteSmoke,
                        TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                    TextRenderer.DrawText(g, cantidad, fCant, new Rectangle(ancho - medida.Width, y, medida.Width, 18), Color.Goldenrod,
                        TextFormatFlags.Right);

                    var barra = new Rectangle(0, y + 20, ancho - 2, 6);
                    RellenarRedondeado(g, pista, barra);
                    int largo = Math.Max(6, (int)((ancho - 2) * (p.Cantidad / (double)maximo)));
                    RellenarRedondeado(g, relleno, new Rectangle(0, y + 20, largo, 6));
                }
            }
        }

        private static void RellenarRedondeado(Graphics g, Brush pincel, Rectangle r)
        {
            using (var camino = new GraphicsPath())
            {
                int d = r.Height;
                camino.AddArc(r.X, r.Y, d, d, 90, 180);
                camino.AddArc(r.Right - d, r.Y, d, d, 270, 180);
                camino.CloseFigure();
                g.FillPath(pincel, camino);
            }
        }

        #endregion

        #region Métodos auxiliares

        private void Imprimir()
        {
            if (_reporte == null || _reporte.Vacio) return;
            new ReporteServiciosImpresor_68SA(_reporte).Imprimir();
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

        #region Tipos anidados

        private enum Periodo { EsteMes, Ultimos30, EsteAnio, Historico, Personalizado }

        private class TarjetaKpi
        {
            public Label Titulo, Valor, Detalle;
            public string ClaveTitulo;
        }

        #endregion
    }
}
