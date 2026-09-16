namespace GUI_08YS.Recepcionista
{
    partial class FormReservas_68SA
    {
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
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.btnCrearReserva = new FontAwesome.Sharp.IconButton();
            this.lblEncabezado = new System.Windows.Forms.Label();
            this.iconEncabezado = new FontAwesome.Sharp.IconPictureBox();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.btnTabLista = new FontAwesome.Sharp.IconButton();
            this.btnTabCalendario = new FontAwesome.Sharp.IconButton();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlCalendario = new System.Windows.Forms.Panel();
            this.lblCalendarioPlaceholder = new System.Windows.Forms.Label();
            this.pnlLista = new System.Windows.Forms.Panel();
            this.flpFilasReservas = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlEncabezadoTabla = new System.Windows.Forms.Panel();
            this.lblColAccion = new System.Windows.Forms.Label();
            this.lblColEstado = new System.Windows.Forms.Label();
            this.lblColSaldo = new System.Windows.Forms.Label();
            this.lblColAdelanto = new System.Windows.Forms.Label();
            this.lblColCosto = new System.Windows.Forms.Label();
            this.lblColSalida = new System.Windows.Forms.Label();
            this.lblColEntrada = new System.Windows.Forms.Label();
            this.lblColRegistro = new System.Windows.Forms.Label();
            this.lblColHab = new System.Windows.Forms.Label();
            this.lblColHuesped = new System.Windows.Forms.Label();
            this.lblColCod = new System.Windows.Forms.Label();
            this.pnlTarjetas = new System.Windows.Forms.Panel();
            this.pnlTarjetaCanceladas = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblCantCanceladas = new System.Windows.Forms.Label();
            this.lblMontoCanceladas = new System.Windows.Forms.Label();
            this.lblTituloCanceladas = new System.Windows.Forms.Label();
            this.iconCanceladas = new FontAwesome.Sharp.IconPictureBox();
            this.pnlTarjetaOcupadas = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblCantOcupadas = new System.Windows.Forms.Label();
            this.lblMontoOcupadas = new System.Windows.Forms.Label();
            this.lblTituloOcupadas = new System.Windows.Forms.Label();
            this.iconOcupadas = new FontAwesome.Sharp.IconPictureBox();
            this.pnlTarjetaPendientes = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblCantPendientes = new System.Windows.Forms.Label();
            this.lblMontoPendientes = new System.Windows.Forms.Label();
            this.lblTituloPendientes = new System.Windows.Forms.Label();
            this.iconPendientes = new FontAwesome.Sharp.IconPictureBox();
            this.pnlTarjetaVigentes = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblCantVigentes = new System.Windows.Forms.Label();
            this.lblMontoVigentes = new System.Windows.Forms.Label();
            this.lblTituloVigentes = new System.Windows.Forms.Label();
            this.iconVigentes = new FontAwesome.Sharp.IconPictureBox();
            this.pnlFiltros = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.btnLimpiarFiltros = new FontAwesome.Sharp.IconButton();
            this.cmbEstadoFiltro = new CustomControls.IconComboBox();
            this.lblEtiquetaEstadoFiltro = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaFechaHasta = new System.Windows.Forms.Label();
            this.dtpFechaDesde = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaFechaDesde = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).BeginInit();
            this.pnlTabs.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlCalendario.SuspendLayout();
            this.pnlLista.SuspendLayout();
            this.pnlEncabezadoTabla.SuspendLayout();
            this.pnlTarjetas.SuspendLayout();
            this.pnlTarjetaCanceladas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconCanceladas)).BeginInit();
            this.pnlTarjetaOcupadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconOcupadas)).BeginInit();
            this.pnlTarjetaPendientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPendientes)).BeginInit();
            this.pnlTarjetaVigentes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconVigentes)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlEncabezado.Controls.Add(this.btnCrearReserva);
            this.pnlEncabezado.Controls.Add(this.lblEncabezado);
            this.pnlEncabezado.Controls.Add(this.iconEncabezado);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1280, 76);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // btnCrearReserva
            // 
            this.btnCrearReserva.BackColor = System.Drawing.Color.Goldenrod;
            this.btnCrearReserva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearReserva.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearReserva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnCrearReserva.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnCrearReserva.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnCrearReserva.IconSize = 22;
            this.btnCrearReserva.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCrearReserva.Location = new System.Drawing.Point(1080, 20);
            this.btnCrearReserva.Name = "btnCrearReserva";
            this.btnCrearReserva.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnCrearReserva.Size = new System.Drawing.Size(180, 38);
            this.btnCrearReserva.TabIndex = 2;
            this.btnCrearReserva.Text = "Crear Reserva";
            this.btnCrearReserva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCrearReserva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCrearReserva.UseVisualStyleBackColor = false;
            // 
            // lblEncabezado
            // 
            this.lblEncabezado.AutoSize = true;
            this.lblEncabezado.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezado.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblEncabezado.Location = new System.Drawing.Point(72, 24);
            this.lblEncabezado.Name = "lblEncabezado";
            this.lblEncabezado.Size = new System.Drawing.Size(120, 30);
            this.lblEncabezado.TabIndex = 1;
            this.lblEncabezado.Text = "Reservas";
            // 
            // iconEncabezado
            // 
            this.iconEncabezado.IconChar = FontAwesome.Sharp.IconChar.CalendarDays;
            this.iconEncabezado.IconColor = System.Drawing.Color.Gold;
            this.iconEncabezado.IconSize = 40;
            this.iconEncabezado.Location = new System.Drawing.Point(20, 18);
            this.iconEncabezado.Name = "iconEncabezado";
            this.iconEncabezado.Size = new System.Drawing.Size(44, 44);
            this.iconEncabezado.TabIndex = 0;
            this.iconEncabezado.TabStop = false;
            // 
            // pnlTabs
            // 
            this.pnlTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.pnlTabs.Controls.Add(this.btnTabLista);
            this.pnlTabs.Controls.Add(this.btnTabCalendario);
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTabs.Location = new System.Drawing.Point(0, 76);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(1280, 50);
            this.pnlTabs.TabIndex = 1;
            // 
            // btnTabCalendario
            // 
            this.btnTabCalendario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.btnTabCalendario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabCalendario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTabCalendario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.btnTabCalendario.IconChar = FontAwesome.Sharp.IconChar.Calendar;
            this.btnTabCalendario.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.btnTabCalendario.IconSize = 20;
            this.btnTabCalendario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabCalendario.Location = new System.Drawing.Point(20, 6);
            this.btnTabCalendario.Name = "btnTabCalendario";
            this.btnTabCalendario.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnTabCalendario.Size = new System.Drawing.Size(160, 38);
            this.btnTabCalendario.TabIndex = 0;
            this.btnTabCalendario.Text = "Calendario";
            this.btnTabCalendario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabCalendario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTabCalendario.UseVisualStyleBackColor = false;
            // 
            // btnTabLista
            // 
            this.btnTabLista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.btnTabLista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabLista.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTabLista.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnTabLista.IconChar = FontAwesome.Sharp.IconChar.List;
            this.btnTabLista.IconColor = System.Drawing.Color.Goldenrod;
            this.btnTabLista.IconSize = 20;
            this.btnTabLista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabLista.Location = new System.Drawing.Point(190, 6);
            this.btnTabLista.Name = "btnTabLista";
            this.btnTabLista.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnTabLista.Size = new System.Drawing.Size(200, 38);
            this.btnTabLista.TabIndex = 1;
            this.btnTabLista.Text = "Lista de Reservas";
            this.btnTabLista.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabLista.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTabLista.UseVisualStyleBackColor = false;
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.pnlContenido.Controls.Add(this.pnlCalendario);
            this.pnlContenido.Controls.Add(this.pnlLista);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 126);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1280, 594);
            this.pnlContenido.TabIndex = 2;
            // 
            // pnlCalendario
            // 
            this.pnlCalendario.Controls.Add(this.lblCalendarioPlaceholder);
            this.pnlCalendario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCalendario.Location = new System.Drawing.Point(0, 0);
            this.pnlCalendario.Name = "pnlCalendario";
            this.pnlCalendario.Size = new System.Drawing.Size(1280, 594);
            this.pnlCalendario.TabIndex = 1;
            this.pnlCalendario.Visible = false;
            // 
            // lblCalendarioPlaceholder
            // 
            this.lblCalendarioPlaceholder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalendarioPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblCalendarioPlaceholder.Location = new System.Drawing.Point(20, 20);
            this.lblCalendarioPlaceholder.Name = "lblCalendarioPlaceholder";
            this.lblCalendarioPlaceholder.Size = new System.Drawing.Size(600, 40);
            this.lblCalendarioPlaceholder.TabIndex = 0;
            this.lblCalendarioPlaceholder.Text = "Vista de calendario — próximamente.";
            // 
            // pnlLista
            // 
            this.pnlLista.AutoScroll = true;
            this.pnlLista.Controls.Add(this.flpFilasReservas);
            this.pnlLista.Controls.Add(this.pnlEncabezadoTabla);
            this.pnlLista.Controls.Add(this.pnlTarjetas);
            this.pnlLista.Controls.Add(this.pnlFiltros);
            this.pnlLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLista.Location = new System.Drawing.Point(0, 0);
            this.pnlLista.Name = "pnlLista";
            this.pnlLista.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlLista.Size = new System.Drawing.Size(1280, 594);
            this.pnlLista.TabIndex = 0;
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlFiltros.Controls.Add(this.btnLimpiarFiltros);
            this.pnlFiltros.Controls.Add(this.cmbEstadoFiltro);
            this.pnlFiltros.Controls.Add(this.lblEtiquetaEstadoFiltro);
            this.pnlFiltros.Controls.Add(this.dtpFechaHasta);
            this.pnlFiltros.Controls.Add(this.lblEtiquetaFechaHasta);
            this.pnlFiltros.Controls.Add(this.dtpFechaDesde);
            this.pnlFiltros.Controls.Add(this.lblEtiquetaFechaDesde);
            this.pnlFiltros.CornerRadius = 12;
            this.pnlFiltros.Location = new System.Drawing.Point(20, 16);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(1240, 110);
            this.pnlFiltros.TabIndex = 0;
            // 
            // lblEtiquetaFechaDesde
            // 
            this.lblEtiquetaFechaDesde.AutoSize = true;
            this.lblEtiquetaFechaDesde.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaFechaDesde.Location = new System.Drawing.Point(16, 14);
            this.lblEtiquetaFechaDesde.Name = "lblEtiquetaFechaDesde";
            this.lblEtiquetaFechaDesde.Size = new System.Drawing.Size(120, 15);
            this.lblEtiquetaFechaDesde.TabIndex = 0;
            this.lblEtiquetaFechaDesde.Text = "Fecha Inicio (Desde)";
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaDesde.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaDesde.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaDesde.BorderWidth = 2;
            this.dtpFechaDesde.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaDesde.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaDesde.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaDesde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaDesde.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaDesde.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpFechaDesde.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaDesde.IconSize = 22;
            this.dtpFechaDesde.Location = new System.Drawing.Point(16, 32);
            this.dtpFechaDesde.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaDesde.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(260, 40);
            this.dtpFechaDesde.TabIndex = 1;
            this.dtpFechaDesde.Value = null;
            // 
            // lblEtiquetaFechaHasta
            // 
            this.lblEtiquetaFechaHasta.AutoSize = true;
            this.lblEtiquetaFechaHasta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaFechaHasta.Location = new System.Drawing.Point(300, 14);
            this.lblEtiquetaFechaHasta.Name = "lblEtiquetaFechaHasta";
            this.lblEtiquetaFechaHasta.Size = new System.Drawing.Size(110, 15);
            this.lblEtiquetaFechaHasta.TabIndex = 2;
            this.lblEtiquetaFechaHasta.Text = "Fecha Fin (Hasta)";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaHasta.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaHasta.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaHasta.BorderWidth = 2;
            this.dtpFechaHasta.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaHasta.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaHasta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaHasta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaHasta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaHasta.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpFechaHasta.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaHasta.IconSize = 22;
            this.dtpFechaHasta.Location = new System.Drawing.Point(300, 32);
            this.dtpFechaHasta.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaHasta.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(260, 40);
            this.dtpFechaHasta.TabIndex = 3;
            this.dtpFechaHasta.Value = null;
            // 
            // lblEtiquetaEstadoFiltro
            // 
            this.lblEtiquetaEstadoFiltro.AutoSize = true;
            this.lblEtiquetaEstadoFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaEstadoFiltro.Location = new System.Drawing.Point(584, 14);
            this.lblEtiquetaEstadoFiltro.Name = "lblEtiquetaEstadoFiltro";
            this.lblEtiquetaEstadoFiltro.Size = new System.Drawing.Size(50, 15);
            this.lblEtiquetaEstadoFiltro.TabIndex = 4;
            this.lblEtiquetaEstadoFiltro.Text = "Estado";
            // 
            // cmbEstadoFiltro
            // 
            this.cmbEstadoFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.cmbEstadoFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.BorderWidth = 2;
            this.cmbEstadoFiltro.Cursor = System.Windows.Forms.Cursors.PanSouth;
            this.cmbEstadoFiltro.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.cmbEstadoFiltro.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbEstadoFiltro.DropDownHighlightBackColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.DropDownHighlightForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.cmbEstadoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEstadoFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbEstadoFiltro.IconChar = FontAwesome.Sharp.IconChar.Filter;
            this.cmbEstadoFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.IconSize = 22;
            this.cmbEstadoFiltro.Location = new System.Drawing.Point(584, 32);
            this.cmbEstadoFiltro.Name = "cmbEstadoFiltro";
            this.cmbEstadoFiltro.Size = new System.Drawing.Size(260, 40);
            this.cmbEstadoFiltro.TabIndex = 5;
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarFiltros.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarFiltros.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiarFiltros.IconChar = FontAwesome.Sharp.IconChar.ArrowRotateLeft;
            this.btnLimpiarFiltros.IconColor = System.Drawing.Color.Gold;
            this.btnLimpiarFiltros.IconSize = 20;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(864, 32);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(180, 40);
            this.btnLimpiarFiltros.TabIndex = 6;
            this.btnLimpiarFiltros.Text = "Limpiar Filtros";
            this.btnLimpiarFiltros.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLimpiarFiltros.UseVisualStyleBackColor = false;
            // 
            // pnlTarjetas
            // 
            this.pnlTarjetas.Controls.Add(this.pnlTarjetaCanceladas);
            this.pnlTarjetas.Controls.Add(this.pnlTarjetaOcupadas);
            this.pnlTarjetas.Controls.Add(this.pnlTarjetaPendientes);
            this.pnlTarjetas.Controls.Add(this.pnlTarjetaVigentes);
            this.pnlTarjetas.Location = new System.Drawing.Point(20, 138);
            this.pnlTarjetas.Name = "pnlTarjetas";
            this.pnlTarjetas.Size = new System.Drawing.Size(1240, 110);
            this.pnlTarjetas.TabIndex = 1;
            // 
            // pnlTarjetaVigentes
            // 
            this.pnlTarjetaVigentes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlTarjetaVigentes.Controls.Add(this.iconVigentes);
            this.pnlTarjetaVigentes.Controls.Add(this.lblTituloVigentes);
            this.pnlTarjetaVigentes.Controls.Add(this.lblMontoVigentes);
            this.pnlTarjetaVigentes.Controls.Add(this.lblCantVigentes);
            this.pnlTarjetaVigentes.CornerRadius = 12;
            this.pnlTarjetaVigentes.Location = new System.Drawing.Point(0, 0);
            this.pnlTarjetaVigentes.Name = "pnlTarjetaVigentes";
            this.pnlTarjetaVigentes.Size = new System.Drawing.Size(295, 110);
            this.pnlTarjetaVigentes.TabIndex = 0;
            // 
            // lblTituloVigentes
            // 
            this.lblTituloVigentes.AutoSize = true;
            this.lblTituloVigentes.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloVigentes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloVigentes.Location = new System.Drawing.Point(16, 14);
            this.lblTituloVigentes.Name = "lblTituloVigentes";
            this.lblTituloVigentes.Size = new System.Drawing.Size(150, 13);
            this.lblTituloVigentes.TabIndex = 0;
            this.lblTituloVigentes.Text = "VALOR RESERVAS VIGENTES";
            // 
            // lblMontoVigentes
            // 
            this.lblMontoVigentes.AutoSize = true;
            this.lblMontoVigentes.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoVigentes.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblMontoVigentes.Location = new System.Drawing.Point(16, 34);
            this.lblMontoVigentes.Name = "lblMontoVigentes";
            this.lblMontoVigentes.Size = new System.Drawing.Size(80, 30);
            this.lblMontoVigentes.TabIndex = 1;
            this.lblMontoVigentes.Text = "$0";
            // 
            // lblCantVigentes
            // 
            this.lblCantVigentes.AutoSize = true;
            this.lblCantVigentes.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCantVigentes.Location = new System.Drawing.Point(16, 76);
            this.lblCantVigentes.Name = "lblCantVigentes";
            this.lblCantVigentes.Size = new System.Drawing.Size(90, 15);
            this.lblCantVigentes.TabIndex = 2;
            this.lblCantVigentes.Text = "Cantidad: 0";
            // 
            // iconVigentes
            // 
            this.iconVigentes.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this.iconVigentes.IconColor = System.Drawing.Color.Gold;
            this.iconVigentes.IconSize = 32;
            this.iconVigentes.Location = new System.Drawing.Point(240, 38);
            this.iconVigentes.Name = "iconVigentes";
            this.iconVigentes.Size = new System.Drawing.Size(40, 40);
            this.iconVigentes.TabIndex = 3;
            this.iconVigentes.TabStop = false;
            // 
            // pnlTarjetaPendientes
            // 
            this.pnlTarjetaPendientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlTarjetaPendientes.Controls.Add(this.iconPendientes);
            this.pnlTarjetaPendientes.Controls.Add(this.lblTituloPendientes);
            this.pnlTarjetaPendientes.Controls.Add(this.lblMontoPendientes);
            this.pnlTarjetaPendientes.Controls.Add(this.lblCantPendientes);
            this.pnlTarjetaPendientes.CornerRadius = 12;
            this.pnlTarjetaPendientes.Location = new System.Drawing.Point(315, 0);
            this.pnlTarjetaPendientes.Name = "pnlTarjetaPendientes";
            this.pnlTarjetaPendientes.Size = new System.Drawing.Size(295, 110);
            this.pnlTarjetaPendientes.TabIndex = 1;
            // 
            // lblTituloPendientes
            // 
            this.lblTituloPendientes.AutoSize = true;
            this.lblTituloPendientes.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloPendientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloPendientes.Location = new System.Drawing.Point(16, 14);
            this.lblTituloPendientes.Name = "lblTituloPendientes";
            this.lblTituloPendientes.Size = new System.Drawing.Size(140, 13);
            this.lblTituloPendientes.TabIndex = 0;
            this.lblTituloPendientes.Text = "RESERVAS PENDIENTES";
            // 
            // lblMontoPendientes
            // 
            this.lblMontoPendientes.AutoSize = true;
            this.lblMontoPendientes.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoPendientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(155)))), ((int)(((byte)(235)))));
            this.lblMontoPendientes.Location = new System.Drawing.Point(16, 34);
            this.lblMontoPendientes.Name = "lblMontoPendientes";
            this.lblMontoPendientes.Size = new System.Drawing.Size(80, 30);
            this.lblMontoPendientes.TabIndex = 1;
            this.lblMontoPendientes.Text = "$0";
            // 
            // lblCantPendientes
            // 
            this.lblCantPendientes.AutoSize = true;
            this.lblCantPendientes.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCantPendientes.Location = new System.Drawing.Point(16, 76);
            this.lblCantPendientes.Name = "lblCantPendientes";
            this.lblCantPendientes.Size = new System.Drawing.Size(90, 15);
            this.lblCantPendientes.TabIndex = 2;
            this.lblCantPendientes.Text = "Cantidad: 0";
            // 
            // iconPendientes
            // 
            this.iconPendientes.IconChar = FontAwesome.Sharp.IconChar.Clock;
            this.iconPendientes.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(155)))), ((int)(((byte)(235)))));
            this.iconPendientes.IconSize = 32;
            this.iconPendientes.Location = new System.Drawing.Point(240, 38);
            this.iconPendientes.Name = "iconPendientes";
            this.iconPendientes.Size = new System.Drawing.Size(40, 40);
            this.iconPendientes.TabIndex = 3;
            this.iconPendientes.TabStop = false;
            // 
            // pnlTarjetaOcupadas
            // 
            this.pnlTarjetaOcupadas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlTarjetaOcupadas.Controls.Add(this.iconOcupadas);
            this.pnlTarjetaOcupadas.Controls.Add(this.lblTituloOcupadas);
            this.pnlTarjetaOcupadas.Controls.Add(this.lblMontoOcupadas);
            this.pnlTarjetaOcupadas.Controls.Add(this.lblCantOcupadas);
            this.pnlTarjetaOcupadas.CornerRadius = 12;
            this.pnlTarjetaOcupadas.Location = new System.Drawing.Point(630, 0);
            this.pnlTarjetaOcupadas.Name = "pnlTarjetaOcupadas";
            this.pnlTarjetaOcupadas.Size = new System.Drawing.Size(295, 110);
            this.pnlTarjetaOcupadas.TabIndex = 2;
            // 
            // lblTituloOcupadas
            // 
            this.lblTituloOcupadas.AutoSize = true;
            this.lblTituloOcupadas.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloOcupadas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloOcupadas.Location = new System.Drawing.Point(16, 14);
            this.lblTituloOcupadas.Name = "lblTituloOcupadas";
            this.lblTituloOcupadas.Size = new System.Drawing.Size(120, 13);
            this.lblTituloOcupadas.TabIndex = 0;
            this.lblTituloOcupadas.Text = "RESERVAS OCUPADAS";
            // 
            // lblMontoOcupadas
            // 
            this.lblMontoOcupadas.AutoSize = true;
            this.lblMontoOcupadas.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoOcupadas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(217)))), ((int)(((byte)(100)))));
            this.lblMontoOcupadas.Location = new System.Drawing.Point(16, 34);
            this.lblMontoOcupadas.Name = "lblMontoOcupadas";
            this.lblMontoOcupadas.Size = new System.Drawing.Size(80, 30);
            this.lblMontoOcupadas.TabIndex = 1;
            this.lblMontoOcupadas.Text = "$0";
            // 
            // lblCantOcupadas
            // 
            this.lblCantOcupadas.AutoSize = true;
            this.lblCantOcupadas.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCantOcupadas.Location = new System.Drawing.Point(16, 76);
            this.lblCantOcupadas.Name = "lblCantOcupadas";
            this.lblCantOcupadas.Size = new System.Drawing.Size(90, 15);
            this.lblCantOcupadas.TabIndex = 2;
            this.lblCantOcupadas.Text = "Cantidad: 0";
            // 
            // iconOcupadas
            // 
            this.iconOcupadas.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.iconOcupadas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(217)))), ((int)(((byte)(100)))));
            this.iconOcupadas.IconSize = 32;
            this.iconOcupadas.Location = new System.Drawing.Point(240, 38);
            this.iconOcupadas.Name = "iconOcupadas";
            this.iconOcupadas.Size = new System.Drawing.Size(40, 40);
            this.iconOcupadas.TabIndex = 3;
            this.iconOcupadas.TabStop = false;
            // 
            // pnlTarjetaCanceladas
            // 
            this.pnlTarjetaCanceladas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlTarjetaCanceladas.Controls.Add(this.iconCanceladas);
            this.pnlTarjetaCanceladas.Controls.Add(this.lblTituloCanceladas);
            this.pnlTarjetaCanceladas.Controls.Add(this.lblMontoCanceladas);
            this.pnlTarjetaCanceladas.Controls.Add(this.lblCantCanceladas);
            this.pnlTarjetaCanceladas.CornerRadius = 12;
            this.pnlTarjetaCanceladas.Location = new System.Drawing.Point(945, 0);
            this.pnlTarjetaCanceladas.Name = "pnlTarjetaCanceladas";
            this.pnlTarjetaCanceladas.Size = new System.Drawing.Size(295, 110);
            this.pnlTarjetaCanceladas.TabIndex = 3;
            // 
            // lblTituloCanceladas
            // 
            this.lblTituloCanceladas.AutoSize = true;
            this.lblTituloCanceladas.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloCanceladas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblTituloCanceladas.Location = new System.Drawing.Point(16, 14);
            this.lblTituloCanceladas.Name = "lblTituloCanceladas";
            this.lblTituloCanceladas.Size = new System.Drawing.Size(130, 13);
            this.lblTituloCanceladas.TabIndex = 0;
            this.lblTituloCanceladas.Text = "RESERVAS CANCELADAS";
            // 
            // lblMontoCanceladas
            // 
            this.lblMontoCanceladas.AutoSize = true;
            this.lblMontoCanceladas.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoCanceladas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblMontoCanceladas.Location = new System.Drawing.Point(16, 34);
            this.lblMontoCanceladas.Name = "lblMontoCanceladas";
            this.lblMontoCanceladas.Size = new System.Drawing.Size(80, 30);
            this.lblMontoCanceladas.TabIndex = 1;
            this.lblMontoCanceladas.Text = "$0";
            // 
            // lblCantCanceladas
            // 
            this.lblCantCanceladas.AutoSize = true;
            this.lblCantCanceladas.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCantCanceladas.Location = new System.Drawing.Point(16, 76);
            this.lblCantCanceladas.Name = "lblCantCanceladas";
            this.lblCantCanceladas.Size = new System.Drawing.Size(90, 15);
            this.lblCantCanceladas.TabIndex = 2;
            this.lblCantCanceladas.Text = "Cantidad: 0";
            // 
            // iconCanceladas
            // 
            this.iconCanceladas.IconChar = FontAwesome.Sharp.IconChar.CircleXmark;
            this.iconCanceladas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.iconCanceladas.IconSize = 32;
            this.iconCanceladas.Location = new System.Drawing.Point(240, 38);
            this.iconCanceladas.Name = "iconCanceladas";
            this.iconCanceladas.Size = new System.Drawing.Size(40, 40);
            this.iconCanceladas.TabIndex = 3;
            this.iconCanceladas.TabStop = false;
            // 
            // pnlEncabezadoTabla
            // 
            this.pnlEncabezadoTabla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlEncabezadoTabla.Controls.Add(this.lblColAccion);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColEstado);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColSaldo);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColAdelanto);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColCosto);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColSalida);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColEntrada);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColRegistro);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColHab);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColHuesped);
            this.pnlEncabezadoTabla.Controls.Add(this.lblColCod);
            this.pnlEncabezadoTabla.Location = new System.Drawing.Point(20, 260);
            this.pnlEncabezadoTabla.Name = "pnlEncabezadoTabla";
            this.pnlEncabezadoTabla.Size = new System.Drawing.Size(1240, 36);
            this.pnlEncabezadoTabla.TabIndex = 2;
            // 
            // lblColCod
            // 
            this.lblColCod.AutoSize = true;
            this.lblColCod.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColCod.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColCod.Location = new System.Drawing.Point(10, 10);
            this.lblColCod.Name = "lblColCod";
            this.lblColCod.Size = new System.Drawing.Size(30, 15);
            this.lblColCod.TabIndex = 0;
            this.lblColCod.Text = "COD.";
            // 
            // lblColHuesped
            // 
            this.lblColHuesped.AutoSize = true;
            this.lblColHuesped.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColHuesped.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColHuesped.Location = new System.Drawing.Point(70, 10);
            this.lblColHuesped.Name = "lblColHuesped";
            this.lblColHuesped.Size = new System.Drawing.Size(60, 15);
            this.lblColHuesped.TabIndex = 1;
            this.lblColHuesped.Text = "HUÉSPED";
            // 
            // lblColHab
            // 
            this.lblColHab.AutoSize = true;
            this.lblColHab.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColHab.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColHab.Location = new System.Drawing.Point(250, 10);
            this.lblColHab.Name = "lblColHab";
            this.lblColHab.Size = new System.Drawing.Size(35, 15);
            this.lblColHab.TabIndex = 2;
            this.lblColHab.Text = "HAB.";
            // 
            // lblColRegistro
            // 
            this.lblColRegistro.AutoSize = true;
            this.lblColRegistro.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColRegistro.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColRegistro.Location = new System.Drawing.Point(320, 10);
            this.lblColRegistro.Name = "lblColRegistro";
            this.lblColRegistro.Size = new System.Drawing.Size(60, 15);
            this.lblColRegistro.TabIndex = 3;
            this.lblColRegistro.Text = "REGISTRO";
            // 
            // lblColEntrada
            // 
            this.lblColEntrada.AutoSize = true;
            this.lblColEntrada.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColEntrada.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColEntrada.Location = new System.Drawing.Point(480, 10);
            this.lblColEntrada.Name = "lblColEntrada";
            this.lblColEntrada.Size = new System.Drawing.Size(90, 15);
            this.lblColEntrada.TabIndex = 4;
            this.lblColEntrada.Text = "FECHA ENTRADA";
            // 
            // lblColSalida
            // 
            this.lblColSalida.AutoSize = true;
            this.lblColSalida.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColSalida.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColSalida.Location = new System.Drawing.Point(600, 10);
            this.lblColSalida.Name = "lblColSalida";
            this.lblColSalida.Size = new System.Drawing.Size(80, 15);
            this.lblColSalida.TabIndex = 5;
            this.lblColSalida.Text = "FECHA SALIDA";
            // 
            // lblColCosto
            // 
            this.lblColCosto.AutoSize = true;
            this.lblColCosto.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColCosto.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColCosto.Location = new System.Drawing.Point(720, 10);
            this.lblColCosto.Name = "lblColCosto";
            this.lblColCosto.Size = new System.Drawing.Size(45, 15);
            this.lblColCosto.TabIndex = 6;
            this.lblColCosto.Text = "COSTO";
            // 
            // lblColAdelanto
            // 
            this.lblColAdelanto.AutoSize = true;
            this.lblColAdelanto.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColAdelanto.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColAdelanto.Location = new System.Drawing.Point(820, 10);
            this.lblColAdelanto.Name = "lblColAdelanto";
            this.lblColAdelanto.Size = new System.Drawing.Size(60, 15);
            this.lblColAdelanto.TabIndex = 7;
            this.lblColAdelanto.Text = "ADELANTO";
            // 
            // lblColSaldo
            // 
            this.lblColSaldo.AutoSize = true;
            this.lblColSaldo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColSaldo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColSaldo.Location = new System.Drawing.Point(920, 10);
            this.lblColSaldo.Name = "lblColSaldo";
            this.lblColSaldo.Size = new System.Drawing.Size(45, 15);
            this.lblColSaldo.TabIndex = 8;
            this.lblColSaldo.Text = "SALDO";
            // 
            // lblColEstado
            // 
            this.lblColEstado.AutoSize = true;
            this.lblColEstado.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColEstado.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColEstado.Location = new System.Drawing.Point(1020, 10);
            this.lblColEstado.Name = "lblColEstado";
            this.lblColEstado.Size = new System.Drawing.Size(50, 15);
            this.lblColEstado.TabIndex = 9;
            this.lblColEstado.Text = "ESTADO";
            // 
            // lblColAccion
            // 
            this.lblColAccion.AutoSize = true;
            this.lblColAccion.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColAccion.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblColAccion.Location = new System.Drawing.Point(1130, 10);
            this.lblColAccion.Name = "lblColAccion";
            this.lblColAccion.Size = new System.Drawing.Size(50, 15);
            this.lblColAccion.TabIndex = 10;
            this.lblColAccion.Text = "ACCIÓN";
            // 
            // flpFilasReservas
            // 
            this.flpFilasReservas.AutoScroll = true;
            this.flpFilasReservas.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFilasReservas.Location = new System.Drawing.Point(20, 300);
            this.flpFilasReservas.Name = "flpFilasReservas";
            this.flpFilasReservas.Size = new System.Drawing.Size(1240, 400);
            this.flpFilasReservas.TabIndex = 3;
            this.flpFilasReservas.WrapContents = false;
            // 
            // FormReservas_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlTabs);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormReservas_68SA";
            this.Text = "FormReservas_68SA";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).EndInit();
            this.pnlTabs.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlCalendario.ResumeLayout(false);
            this.pnlCalendario.PerformLayout();
            this.pnlLista.ResumeLayout(false);
            this.pnlEncabezadoTabla.ResumeLayout(false);
            this.pnlEncabezadoTabla.PerformLayout();
            this.pnlTarjetas.ResumeLayout(false);
            this.pnlTarjetaCanceladas.ResumeLayout(false);
            this.pnlTarjetaCanceladas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconCanceladas)).EndInit();
            this.pnlTarjetaOcupadas.ResumeLayout(false);
            this.pnlTarjetaOcupadas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconOcupadas)).EndInit();
            this.pnlTarjetaPendientes.ResumeLayout(false);
            this.pnlTarjetaPendientes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPendientes)).EndInit();
            this.pnlTarjetaVigentes.ResumeLayout(false);
            this.pnlTarjetaVigentes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconVigentes)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private FontAwesome.Sharp.IconPictureBox iconEncabezado;
        private System.Windows.Forms.Label lblEncabezado;
        private FontAwesome.Sharp.IconButton btnCrearReserva;
        private System.Windows.Forms.Panel pnlTabs;
        private FontAwesome.Sharp.IconButton btnTabCalendario;
        private FontAwesome.Sharp.IconButton btnTabLista;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlCalendario;
        private System.Windows.Forms.Label lblCalendarioPlaceholder;
        private System.Windows.Forms.Panel pnlLista;
        private UserControls.RoundedPanel_68SA pnlFiltros;
        private System.Windows.Forms.Label lblEtiquetaFechaDesde;
        private CustomControls.IconDateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Label lblEtiquetaFechaHasta;
        private CustomControls.IconDateTimePicker dtpFechaHasta;
        private System.Windows.Forms.Label lblEtiquetaEstadoFiltro;
        private CustomControls.IconComboBox cmbEstadoFiltro;
        private FontAwesome.Sharp.IconButton btnLimpiarFiltros;
        private System.Windows.Forms.Panel pnlTarjetas;
        private UserControls.RoundedPanel_68SA pnlTarjetaVigentes;
        private System.Windows.Forms.Label lblTituloVigentes;
        private System.Windows.Forms.Label lblMontoVigentes;
        private System.Windows.Forms.Label lblCantVigentes;
        private FontAwesome.Sharp.IconPictureBox iconVigentes;
        private UserControls.RoundedPanel_68SA pnlTarjetaPendientes;
        private System.Windows.Forms.Label lblTituloPendientes;
        private System.Windows.Forms.Label lblMontoPendientes;
        private System.Windows.Forms.Label lblCantPendientes;
        private FontAwesome.Sharp.IconPictureBox iconPendientes;
        private UserControls.RoundedPanel_68SA pnlTarjetaOcupadas;
        private System.Windows.Forms.Label lblTituloOcupadas;
        private System.Windows.Forms.Label lblMontoOcupadas;
        private System.Windows.Forms.Label lblCantOcupadas;
        private FontAwesome.Sharp.IconPictureBox iconOcupadas;
        private UserControls.RoundedPanel_68SA pnlTarjetaCanceladas;
        private System.Windows.Forms.Label lblTituloCanceladas;
        private System.Windows.Forms.Label lblMontoCanceladas;
        private System.Windows.Forms.Label lblCantCanceladas;
        private FontAwesome.Sharp.IconPictureBox iconCanceladas;
        private System.Windows.Forms.Panel pnlEncabezadoTabla;
        private System.Windows.Forms.Label lblColCod;
        private System.Windows.Forms.Label lblColHuesped;
        private System.Windows.Forms.Label lblColHab;
        private System.Windows.Forms.Label lblColRegistro;
        private System.Windows.Forms.Label lblColEntrada;
        private System.Windows.Forms.Label lblColSalida;
        private System.Windows.Forms.Label lblColCosto;
        private System.Windows.Forms.Label lblColAdelanto;
        private System.Windows.Forms.Label lblColSaldo;
        private System.Windows.Forms.Label lblColEstado;
        private System.Windows.Forms.Label lblColAccion;
        private System.Windows.Forms.FlowLayoutPanel flpFilasReservas;
    }
}