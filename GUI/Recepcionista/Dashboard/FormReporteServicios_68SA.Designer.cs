namespace GUI_08YS.Recepcionista
{
    partial class FormReporteServicios_68SA
    {
        /// <summary>Variable del diseñador necesaria.</summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.pnlCuerpo = new System.Windows.Forms.Panel();
            this.tlpCuerpo = new System.Windows.Forms.TableLayoutPanel();
            this.pnlGrillaCard = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblVacio = new System.Windows.Forms.Label();
            this.dgvServicios = new System.Windows.Forms.DataGridView();
            this.pnlGrillaEncabezado = new System.Windows.Forms.Panel();
            this.lblDetalleResumen = new System.Windows.Forms.Label();
            this.lblDetalleTitulo = new System.Windows.Forms.Label();
            this.pnlLateral = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblMayorConsumo = new System.Windows.Forms.Label();
            this.pnlTopProductos = new System.Windows.Forms.Panel();
            this.lblTopTitulo = new System.Windows.Forms.Label();
            this.pnlLeyenda = new System.Windows.Forms.Panel();
            this.ucDonut = new GUI_08YS.Recepcionista.Dashboard.UC_DonutChart_68SA();
            this.lblComposicionTitulo = new System.Windows.Forms.Label();
            this.tlpKpis = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEsteMes = new FontAwesome.Sharp.IconButton();
            this.btn30Dias = new FontAwesome.Sharp.IconButton();
            this.btnEsteAnio = new FontAwesome.Sharp.IconButton();
            this.btnHistorico = new FontAwesome.Sharp.IconButton();
            this.pnlCampoDesde = new System.Windows.Forms.Panel();
            this.dtpDesde = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaDesde = new System.Windows.Forms.Label();
            this.pnlCampoHasta = new System.Windows.Forms.Panel();
            this.dtpHasta = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaHasta = new System.Windows.Forms.Label();
            this.pnlCampoTipo = new System.Windows.Forms.Panel();
            this.cmbTipo = new CustomControls.IconComboBox();
            this.lblEtiquetaTipo = new System.Windows.Forms.Label();
            this.pnlCampoEstado = new System.Windows.Forms.Panel();
            this.cmbEstado = new CustomControls.IconComboBox();
            this.lblEtiquetaEstado = new System.Windows.Forms.Label();
            this.pnlCampoHuesped = new System.Windows.Forms.Panel();
            this.txtHuesped = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaHuesped = new System.Windows.Forms.Label();
            this.btnAplicar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnImprimir = new FontAwesome.Sharp.IconButton();
            this.pnlCuerpo.SuspendLayout();
            this.tlpCuerpo.SuspendLayout();
            this.pnlGrillaCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicios)).BeginInit();
            this.pnlGrillaEncabezado.SuspendLayout();
            this.pnlLateral.SuspendLayout();
            this.pnlTopProductos.SuspendLayout();
            this.pnlLeyenda.SuspendLayout();
            this.tlpKpis.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.pnlCampoDesde.SuspendLayout();
            this.pnlCampoHasta.SuspendLayout();
            this.pnlCampoTipo.SuspendLayout();
            this.pnlCampoEstado.SuspendLayout();
            this.pnlCampoHuesped.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCuerpo
            // 
            this.pnlCuerpo.Controls.Add(this.tlpCuerpo);
            this.pnlCuerpo.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCuerpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCuerpo.Padding = new System.Windows.Forms.Padding(24, 0, 24, 16);
            this.pnlCuerpo.Name = "pnlCuerpo";
            // 
            // tlpCuerpo
            // 
            this.tlpCuerpo.Controls.Add(this.pnlGrillaCard, 0, 0);
            this.tlpCuerpo.Controls.Add(this.pnlLateral, 1, 0);
            this.tlpCuerpo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCuerpo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 390F));
            this.tlpCuerpo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCuerpo.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tlpCuerpo.ColumnCount = 2;
            this.tlpCuerpo.RowCount = 1;
            this.tlpCuerpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCuerpo.Size = new System.Drawing.Size(1422, 560);
            this.tlpCuerpo.Name = "tlpCuerpo";
            // 
            // pnlGrillaCard
            // 
            this.pnlGrillaCard.Controls.Add(this.lblVacio);
            this.pnlGrillaCard.Controls.Add(this.dgvServicios);
            this.pnlGrillaCard.Controls.Add(this.pnlGrillaEncabezado);
            this.pnlGrillaCard.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlGrillaCard.CornerRadius = 14;
            this.pnlGrillaCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrillaCard.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.pnlGrillaCard.Padding = new System.Windows.Forms.Padding(16, 10, 16, 14);
            this.pnlGrillaCard.Name = "pnlGrillaCard";
            // 
            // lblVacio
            // 
            this.lblVacio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVacio.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblVacio.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVacio.Tag = "RepServ_txtVacio";
            this.lblVacio.Text = "No hay servicios para los filtros elegidos.";
            this.lblVacio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVacio.Visible = false;
            this.lblVacio.Name = "lblVacio";
            // 
            // dgvServicios
            // 
            this.dgvServicios.BackgroundColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.dgvServicios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvServicios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServicios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvServicios.Name = "dgvServicios";
            // 
            // pnlGrillaEncabezado
            // 
            this.pnlGrillaEncabezado.Controls.Add(this.lblDetalleResumen);
            this.pnlGrillaEncabezado.Controls.Add(this.lblDetalleTitulo);
            this.pnlGrillaEncabezado.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlGrillaEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGrillaEncabezado.Size = new System.Drawing.Size(986, 40);
            this.pnlGrillaEncabezado.Name = "pnlGrillaEncabezado";
            // 
            // lblDetalleResumen
            // 
            this.lblDetalleResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDetalleResumen.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblDetalleResumen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalleResumen.Text = "-";
            this.lblDetalleResumen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDetalleResumen.Name = "lblDetalleResumen";
            // 
            // lblDetalleTitulo
            // 
            this.lblDetalleTitulo.AutoSize = true;
            this.lblDetalleTitulo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblDetalleTitulo.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblDetalleTitulo.Tag = "RepServ_lblDetalleTitulo";
            this.lblDetalleTitulo.Text = "Detalle por huésped";
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            // 
            // pnlLateral
            // 
            this.pnlLateral.Controls.Add(this.lblMayorConsumo);
            this.pnlLateral.Controls.Add(this.pnlTopProductos);
            this.pnlLateral.Controls.Add(this.lblTopTitulo);
            this.pnlLateral.Controls.Add(this.pnlLeyenda);
            this.pnlLateral.Controls.Add(this.ucDonut);
            this.pnlLateral.Controls.Add(this.lblComposicionTitulo);
            this.pnlLateral.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlLateral.CornerRadius = 14;
            this.pnlLateral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLateral.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLateral.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.pnlLateral.Name = "pnlLateral";
            // 
            // lblMayorConsumo
            // 
            this.lblMayorConsumo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblMayorConsumo.ForeColor = System.Drawing.Color.FromArgb(200, 205, 220);
            this.lblMayorConsumo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMayorConsumo.Location = new System.Drawing.Point(18, 492);
            this.lblMayorConsumo.Size = new System.Drawing.Size(354, 44);
            this.lblMayorConsumo.Text = "-";
            this.lblMayorConsumo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMayorConsumo.Name = "lblMayorConsumo";
            // 
            // pnlTopProductos
            // 
            this.pnlTopProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlTopProductos.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlTopProductos.Location = new System.Drawing.Point(18, 270);
            this.pnlTopProductos.Size = new System.Drawing.Size(354, 214);
            this.pnlTopProductos.Name = "pnlTopProductos";
            // 
            // lblTopTitulo
            // 
            this.lblTopTitulo.AutoSize = true;
            this.lblTopTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTopTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTopTitulo.Location = new System.Drawing.Point(16, 240);
            this.lblTopTitulo.Tag = "RepServ_lblTopTitulo";
            this.lblTopTitulo.Text = "Productos más pedidos";
            this.lblTopTitulo.Name = "lblTopTitulo";
            // 
            // pnlLeyenda
            // 
            this.pnlLeyenda.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlLeyenda.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlLeyenda.Location = new System.Drawing.Point(196, 54);
            this.pnlLeyenda.Size = new System.Drawing.Size(176, 160);
            this.pnlLeyenda.Name = "pnlLeyenda";
            // 
            // ucDonut
            // 
            this.ucDonut.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.ucDonut.Location = new System.Drawing.Point(14, 44);
            this.ucDonut.Size = new System.Drawing.Size(176, 176);
            this.ucDonut.Name = "ucDonut";
            // 
            // lblComposicionTitulo
            // 
            this.lblComposicionTitulo.AutoSize = true;
            this.lblComposicionTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComposicionTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblComposicionTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblComposicionTitulo.Tag = "RepServ_lblComposicion";
            this.lblComposicionTitulo.Text = "Composición de los servicios";
            this.lblComposicionTitulo.Name = "lblComposicionTitulo";
            // 
            // tlpKpis
            // 
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tlpKpis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpKpis.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tlpKpis.ColumnCount = 6;
            this.tlpKpis.RowCount = 1;
            this.tlpKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpKpis.Padding = new System.Windows.Forms.Padding(24, 2, 24, 14);
            this.tlpKpis.Size = new System.Drawing.Size(1470, 122);
            this.tlpKpis.Name = "tlpKpis";
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.Controls.Add(this.btnEsteMes);
            this.pnlFiltros.Controls.Add(this.btn30Dias);
            this.pnlFiltros.Controls.Add(this.btnEsteAnio);
            this.pnlFiltros.Controls.Add(this.btnHistorico);
            this.pnlFiltros.Controls.Add(this.pnlCampoDesde);
            this.pnlFiltros.Controls.Add(this.pnlCampoHasta);
            this.pnlFiltros.Controls.Add(this.pnlCampoTipo);
            this.pnlFiltros.Controls.Add(this.pnlCampoEstado);
            this.pnlFiltros.Controls.Add(this.pnlCampoHuesped);
            this.pnlFiltros.Controls.Add(this.btnAplicar);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.SetFlowBreak(this.btnHistorico, true);
            this.pnlFiltros.AutoSize = true;
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(24, 4, 12, 6);
            this.pnlFiltros.Size = new System.Drawing.Size(1470, 130);
            this.pnlFiltros.Name = "pnlFiltros";
            // 
            // btnEsteMes
            // 
            this.btnEsteMes.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnEsteMes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEsteMes.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEsteMes.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnEsteMes.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.btnEsteMes.IconColor = System.Drawing.Color.Gold;
            this.btnEsteMes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEsteMes.IconSize = 18;
            this.btnEsteMes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEsteMes.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnEsteMes.Size = new System.Drawing.Size(160, 38);
            this.btnEsteMes.Tag = "RepServ_btnEsteMes";
            this.btnEsteMes.Text = "Este mes";
            this.btnEsteMes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEsteMes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEsteMes.UseVisualStyleBackColor = false;
            this.btnEsteMes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEsteMes.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnEsteMes.Margin = new System.Windows.Forms.Padding(0, 4, 8, 6);
            this.btnEsteMes.Name = "btnEsteMes";
            // 
            // btn30Dias
            // 
            this.btn30Dias.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btn30Dias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn30Dias.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn30Dias.ForeColor = System.Drawing.Color.Goldenrod;
            this.btn30Dias.IconChar = FontAwesome.Sharp.IconChar.CalendarWeek;
            this.btn30Dias.IconColor = System.Drawing.Color.Gold;
            this.btn30Dias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn30Dias.IconSize = 18;
            this.btn30Dias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn30Dias.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btn30Dias.Size = new System.Drawing.Size(160, 38);
            this.btn30Dias.Tag = "RepServ_btn30Dias";
            this.btn30Dias.Text = "Últimos 30 días";
            this.btn30Dias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn30Dias.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn30Dias.UseVisualStyleBackColor = false;
            this.btn30Dias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn30Dias.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btn30Dias.Margin = new System.Windows.Forms.Padding(0, 4, 8, 6);
            this.btn30Dias.Name = "btn30Dias";
            // 
            // btnEsteAnio
            // 
            this.btnEsteAnio.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnEsteAnio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEsteAnio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEsteAnio.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnEsteAnio.IconChar = FontAwesome.Sharp.IconChar.CalendarDays;
            this.btnEsteAnio.IconColor = System.Drawing.Color.Gold;
            this.btnEsteAnio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEsteAnio.IconSize = 18;
            this.btnEsteAnio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEsteAnio.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnEsteAnio.Size = new System.Drawing.Size(160, 38);
            this.btnEsteAnio.Tag = "RepServ_btnEsteAnio";
            this.btnEsteAnio.Text = "Este año";
            this.btnEsteAnio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEsteAnio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEsteAnio.UseVisualStyleBackColor = false;
            this.btnEsteAnio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEsteAnio.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnEsteAnio.Margin = new System.Windows.Forms.Padding(0, 4, 8, 6);
            this.btnEsteAnio.Name = "btnEsteAnio";
            // 
            // btnHistorico
            // 
            this.btnHistorico.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnHistorico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorico.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorico.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnHistorico.IconChar = FontAwesome.Sharp.IconChar.Infinity;
            this.btnHistorico.IconColor = System.Drawing.Color.Gold;
            this.btnHistorico.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHistorico.IconSize = 18;
            this.btnHistorico.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistorico.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnHistorico.Size = new System.Drawing.Size(160, 38);
            this.btnHistorico.Tag = "RepServ_btnHistorico";
            this.btnHistorico.Text = "Histórico";
            this.btnHistorico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnHistorico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHistorico.UseVisualStyleBackColor = false;
            this.btnHistorico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorico.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnHistorico.Margin = new System.Windows.Forms.Padding(0, 4, 20, 6);
            this.btnHistorico.Name = "btnHistorico";
            // 
            // pnlCampoDesde
            // 
            this.pnlCampoDesde.Controls.Add(this.dtpDesde);
            this.pnlCampoDesde.Controls.Add(this.lblEtiquetaDesde);
            this.pnlCampoDesde.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoDesde.Margin = new System.Windows.Forms.Padding(0, 0, 12, 6);
            this.pnlCampoDesde.Size = new System.Drawing.Size(190, 68);
            this.pnlCampoDesde.Name = "pnlCampoDesde";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(0, 24);
            this.dtpDesde.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpDesde.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpDesde.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpDesde.BorderWidth = 2;
            this.dtpDesde.CalendarBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpDesde.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpDesde.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpDesde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpDesde.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpDesde.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpDesde.IconSize = 18;
            this.dtpDesde.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDesde.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDesde.Size = new System.Drawing.Size(190, 40);
            this.dtpDesde.Value = null;
            this.dtpDesde.Name = "dtpDesde";
            // 
            // lblEtiquetaDesde
            // 
            this.lblEtiquetaDesde.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaDesde.AutoSize = true;
            this.lblEtiquetaDesde.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaDesde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaDesde.Tag = "RepServ_lblDesde";
            this.lblEtiquetaDesde.Text = "Desde";
            this.lblEtiquetaDesde.Name = "lblEtiquetaDesde";
            // 
            // pnlCampoHasta
            // 
            this.pnlCampoHasta.Controls.Add(this.dtpHasta);
            this.pnlCampoHasta.Controls.Add(this.lblEtiquetaHasta);
            this.pnlCampoHasta.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoHasta.Margin = new System.Windows.Forms.Padding(0, 0, 12, 6);
            this.pnlCampoHasta.Size = new System.Drawing.Size(190, 68);
            this.pnlCampoHasta.Name = "pnlCampoHasta";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(0, 24);
            this.dtpHasta.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpHasta.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpHasta.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpHasta.BorderWidth = 2;
            this.dtpHasta.CalendarBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpHasta.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpHasta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpHasta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpHasta.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpHasta.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpHasta.IconSize = 18;
            this.dtpHasta.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpHasta.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpHasta.Size = new System.Drawing.Size(190, 40);
            this.dtpHasta.Value = null;
            this.dtpHasta.Name = "dtpHasta";
            // 
            // lblEtiquetaHasta
            // 
            this.lblEtiquetaHasta.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaHasta.AutoSize = true;
            this.lblEtiquetaHasta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaHasta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaHasta.Tag = "RepServ_lblHasta";
            this.lblEtiquetaHasta.Text = "Hasta";
            this.lblEtiquetaHasta.Name = "lblEtiquetaHasta";
            // 
            // pnlCampoTipo
            // 
            this.pnlCampoTipo.Controls.Add(this.cmbTipo);
            this.pnlCampoTipo.Controls.Add(this.lblEtiquetaTipo);
            this.pnlCampoTipo.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoTipo.Margin = new System.Windows.Forms.Padding(0, 0, 12, 6);
            this.pnlCampoTipo.Size = new System.Drawing.Size(210, 68);
            this.pnlCampoTipo.Name = "pnlCampoTipo";
            // 
            // cmbTipo
            // 
            this.cmbTipo.Location = new System.Drawing.Point(0, 24);
            this.cmbTipo.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipo.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.BorderWidth = 2;
            this.cmbTipo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbTipo.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipo.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipo.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.cmbTipo.DropDownHighlightForeColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.DropDownItemHeight = 32;
            this.cmbTipo.DropDownMaxHeight = 200;
            this.cmbTipo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipo.IconChar = FontAwesome.Sharp.IconChar.LayerGroup;
            this.cmbTipo.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.IconSize = 20;
            this.cmbTipo.SelectedItem = null;
            this.cmbTipo.SelectedValue = null;
            this.cmbTipo.Size = new System.Drawing.Size(210, 40);
            this.cmbTipo.Name = "cmbTipo";
            // 
            // lblEtiquetaTipo
            // 
            this.lblEtiquetaTipo.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaTipo.AutoSize = true;
            this.lblEtiquetaTipo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTipo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTipo.Tag = "RepServ_lblTipo";
            this.lblEtiquetaTipo.Text = "Tipo de servicio";
            this.lblEtiquetaTipo.Name = "lblEtiquetaTipo";
            // 
            // pnlCampoEstado
            // 
            this.pnlCampoEstado.Controls.Add(this.cmbEstado);
            this.pnlCampoEstado.Controls.Add(this.lblEtiquetaEstado);
            this.pnlCampoEstado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoEstado.Margin = new System.Windows.Forms.Padding(0, 0, 12, 6);
            this.pnlCampoEstado.Size = new System.Drawing.Size(200, 68);
            this.pnlCampoEstado.Name = "pnlCampoEstado";
            // 
            // cmbEstado
            // 
            this.cmbEstado.Location = new System.Drawing.Point(0, 24);
            this.cmbEstado.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbEstado.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbEstado.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbEstado.BorderWidth = 2;
            this.cmbEstado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbEstado.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbEstado.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbEstado.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbEstado.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.cmbEstado.DropDownHighlightForeColor = System.Drawing.Color.Goldenrod;
            this.cmbEstado.DropDownItemHeight = 32;
            this.cmbEstado.DropDownMaxHeight = 200;
            this.cmbEstado.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEstado.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbEstado.IconChar = FontAwesome.Sharp.IconChar.CircleHalfStroke;
            this.cmbEstado.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbEstado.IconSize = 20;
            this.cmbEstado.SelectedItem = null;
            this.cmbEstado.SelectedValue = null;
            this.cmbEstado.Size = new System.Drawing.Size(200, 40);
            this.cmbEstado.Name = "cmbEstado";
            // 
            // lblEtiquetaEstado
            // 
            this.lblEtiquetaEstado.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaEstado.AutoSize = true;
            this.lblEtiquetaEstado.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaEstado.Tag = "RepServ_lblEstado";
            this.lblEtiquetaEstado.Text = "Estado";
            this.lblEtiquetaEstado.Name = "lblEtiquetaEstado";
            // 
            // pnlCampoHuesped
            // 
            this.pnlCampoHuesped.Controls.Add(this.txtHuesped);
            this.pnlCampoHuesped.Controls.Add(this.lblEtiquetaHuesped);
            this.pnlCampoHuesped.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoHuesped.Margin = new System.Windows.Forms.Padding(0, 0, 12, 6);
            this.pnlCampoHuesped.Size = new System.Drawing.Size(270, 68);
            this.pnlCampoHuesped.Name = "pnlCampoHuesped";
            // 
            // txtHuesped
            // 
            this.txtHuesped.Location = new System.Drawing.Point(0, 24);
            this.txtHuesped.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtHuesped.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtHuesped.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtHuesped.BorderWidth = 2;
            this.txtHuesped.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHuesped.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtHuesped.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.txtHuesped.IconColor = System.Drawing.Color.Goldenrod;
            this.txtHuesped.IconColorRight = System.Drawing.Color.DimGray;
            this.txtHuesped.IconPadding = 4;
            this.txtHuesped.IconSize = 20;
            this.txtHuesped.MaxLength = 100;
            this.txtHuesped.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtHuesped.PlaceholderText = "DNI, nombre o apellido";
            this.txtHuesped.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtHuesped.Size = new System.Drawing.Size(270, 40);
            this.txtHuesped.Name = "txtHuesped";
            // 
            // lblEtiquetaHuesped
            // 
            this.lblEtiquetaHuesped.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaHuesped.AutoSize = true;
            this.lblEtiquetaHuesped.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaHuesped.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaHuesped.Tag = "RepServ_lblHuesped";
            this.lblEtiquetaHuesped.Text = "Huésped";
            this.lblEtiquetaHuesped.Name = "lblEtiquetaHuesped";
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnAplicar.IconChar = FontAwesome.Sharp.IconChar.Filter;
            this.btnAplicar.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnAplicar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAplicar.IconSize = 22;
            this.btnAplicar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAplicar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnAplicar.Size = new System.Drawing.Size(140, 40);
            this.btnAplicar.Tag = "RepServ_btnAplicar";
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAplicar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnAplicar.Margin = new System.Windows.Forms.Padding(0, 24, 12, 6);
            this.btnAplicar.Name = "btnAplicar";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.btnLimpiar.IconColor = System.Drawing.Color.Gold;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 22;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnLimpiar.Size = new System.Drawing.Size(140, 40);
            this.btnLimpiar.Tag = "Crud_btnLimpiar";
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(0, 24, 12, 6);
            this.btnLimpiar.Name = "btnLimpiar";
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.btnImprimir);
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 12, 24, 4);
            this.pnlEncabezado.Size = new System.Drawing.Size(1470, 84);
            this.pnlEncabezado.Name = "pnlEncabezado";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.Tag = "RepServ_lblSubtitulo";
            this.lblSubtitulo.Text = "Room service y upgrades cargados a la cuenta de cada estadía.";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSubtitulo.Name = "lblSubtitulo";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Size = new System.Drawing.Size(1100, 38);
            this.lblTitulo.Tag = "RepServ_titulo";
            this.lblTitulo.Text = "Servicios consumidos por huésped";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Name = "lblTitulo";
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Goldenrod;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnImprimir.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnImprimir.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnImprimir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnImprimir.IconSize = 22;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnImprimir.Size = new System.Drawing.Size(270, 52);
            this.btnImprimir.Tag = "RepServ_btnImprimir";
            this.btnImprimir.Text = "Vista previa e impresión";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnImprimir.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImprimir.Name = "btnImprimir";
            // 
            // FormReporteServicios_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1470, 920);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text = "Servicios consumidos por huésped";
            this.Controls.Add(this.pnlCuerpo);
            this.Controls.Add(this.tlpKpis);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FormReporteServicios_68SA";
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicios)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlCampoHuesped.ResumeLayout(false);
            this.pnlCampoHuesped.PerformLayout();
            this.pnlCampoEstado.ResumeLayout(false);
            this.pnlCampoEstado.PerformLayout();
            this.pnlCampoTipo.ResumeLayout(false);
            this.pnlCampoTipo.PerformLayout();
            this.pnlCampoHasta.ResumeLayout(false);
            this.pnlCampoHasta.PerformLayout();
            this.pnlCampoDesde.ResumeLayout(false);
            this.pnlCampoDesde.PerformLayout();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.tlpKpis.ResumeLayout(false);
            this.tlpKpis.PerformLayout();
            this.pnlLeyenda.ResumeLayout(false);
            this.pnlTopProductos.ResumeLayout(false);
            this.pnlLateral.ResumeLayout(false);
            this.pnlLateral.PerformLayout();
            this.pnlGrillaEncabezado.ResumeLayout(false);
            this.pnlGrillaEncabezado.PerformLayout();
            this.pnlGrillaCard.ResumeLayout(false);
            this.tlpCuerpo.ResumeLayout(false);
            this.tlpCuerpo.PerformLayout();
            this.pnlCuerpo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCuerpo;
        private System.Windows.Forms.TableLayoutPanel tlpCuerpo;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlGrillaCard;
        private System.Windows.Forms.Label lblVacio;
        private System.Windows.Forms.DataGridView dgvServicios;
        private System.Windows.Forms.Panel pnlGrillaEncabezado;
        private System.Windows.Forms.Label lblDetalleResumen;
        private System.Windows.Forms.Label lblDetalleTitulo;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlLateral;
        private System.Windows.Forms.Label lblMayorConsumo;
        private System.Windows.Forms.Panel pnlTopProductos;
        private System.Windows.Forms.Label lblTopTitulo;
        private System.Windows.Forms.Panel pnlLeyenda;
        private GUI_08YS.Recepcionista.Dashboard.UC_DonutChart_68SA ucDonut;
        private System.Windows.Forms.Label lblComposicionTitulo;
        private System.Windows.Forms.TableLayoutPanel tlpKpis;
        private System.Windows.Forms.FlowLayoutPanel pnlFiltros;
        private FontAwesome.Sharp.IconButton btnEsteMes;
        private FontAwesome.Sharp.IconButton btn30Dias;
        private FontAwesome.Sharp.IconButton btnEsteAnio;
        private FontAwesome.Sharp.IconButton btnHistorico;
        private System.Windows.Forms.Panel pnlCampoDesde;
        private CustomControls.IconDateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblEtiquetaDesde;
        private System.Windows.Forms.Panel pnlCampoHasta;
        private CustomControls.IconDateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblEtiquetaHasta;
        private System.Windows.Forms.Panel pnlCampoTipo;
        private CustomControls.IconComboBox cmbTipo;
        private System.Windows.Forms.Label lblEtiquetaTipo;
        private System.Windows.Forms.Panel pnlCampoEstado;
        private CustomControls.IconComboBox cmbEstado;
        private System.Windows.Forms.Label lblEtiquetaEstado;
        private System.Windows.Forms.Panel pnlCampoHuesped;
        private CustomControls.IconPlaceholderTextBox txtHuesped;
        private System.Windows.Forms.Label lblEtiquetaHuesped;
        private FontAwesome.Sharp.IconButton btnAplicar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnImprimir;
    }
}
