namespace GUI_08YS.Recepcionista
{
    partial class FormBitacoraReservas_68SA
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
            this.pnlGrilla = new System.Windows.Forms.Panel();
            this.dgvBitacora = new System.Windows.Forms.DataGridView();
            this.pnlDetalle = new System.Windows.Forms.Panel();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.lblDetalleSub = new System.Windows.Forms.Label();
            this.lblDetalleTitulo = new System.Windows.Forms.Label();
            this.pnlSeparadorDetalle = new System.Windows.Forms.Panel();
            this.pnlPie = new System.Windows.Forms.Panel();
            this.lblSeleccion = new System.Windows.Forms.Label();
            this.btnHistorial = new FontAwesome.Sharp.IconButton();
            this.btnRollback = new FontAwesome.Sharp.IconButton();
            this.btnSalir = new FontAwesome.Sharp.IconButton();
            this.pnlFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCampoCodigo = new System.Windows.Forms.Panel();
            this.txtCodigo = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaCodigo = new System.Windows.Forms.Label();
            this.pnlCampoNombre = new System.Windows.Forms.Panel();
            this.txtNombre = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaNombre = new System.Windows.Forms.Label();
            this.pnlCampoOperacion = new System.Windows.Forms.Panel();
            this.cmbOperacion = new CustomControls.IconComboBox();
            this.lblEtiquetaOperacion = new System.Windows.Forms.Label();
            this.pnlCampoDesde = new System.Windows.Forms.Panel();
            this.dtpDesde = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaDesde = new System.Windows.Forms.Label();
            this.pnlCampoHasta = new System.Windows.Forms.Panel();
            this.dtpHasta = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaHasta = new System.Windows.Forms.Label();
            this.chkSoloActivos = new GUI_08YS.UserControls.CheckBoxHotel_68SA();
            this.btnAplicar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).BeginInit();
            this.pnlDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.pnlSeparadorDetalle.SuspendLayout();
            this.pnlPie.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.pnlCampoCodigo.SuspendLayout();
            this.pnlCampoNombre.SuspendLayout();
            this.pnlCampoOperacion.SuspendLayout();
            this.pnlCampoDesde.SuspendLayout();
            this.pnlCampoHasta.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.Controls.Add(this.dgvBitacora);
            this.pnlGrilla.Controls.Add(this.pnlDetalle);
            this.pnlGrilla.Controls.Add(this.pnlSeparadorDetalle);
            this.pnlGrilla.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(24, 0, 24, 12);
            this.pnlGrilla.Name = "pnlGrilla";
            // 
            // dgvBitacora
            // 
            this.dgvBitacora.BackgroundColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.dgvBitacora.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBitacora.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBitacora.Name = "dgvBitacora";
            // 
            // pnlDetalle
            // 
            this.pnlDetalle.Controls.Add(this.dgvDetalle);
            this.pnlDetalle.Controls.Add(this.lblDetalleSub);
            this.pnlDetalle.Controls.Add(this.lblDetalleTitulo);
            this.pnlDetalle.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlDetalle.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetalle.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlDetalle.Size = new System.Drawing.Size(420, 520);
            this.pnlDetalle.Name = "pnlDetalle";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.BackgroundColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.dgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDetalle.Name = "dgvDetalle";
            // 
            // lblDetalleSub
            // 
            this.lblDetalleSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetalleSub.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblDetalleSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalleSub.Size = new System.Drawing.Size(388, 62);
            this.lblDetalleSub.Text = "";
            this.lblDetalleSub.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblDetalleSub.UseMnemonic = false;
            this.lblDetalleSub.Name = "lblDetalleSub";
            // 
            // lblDetalleTitulo
            // 
            this.lblDetalleTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblDetalleTitulo.Size = new System.Drawing.Size(388, 30);
            this.lblDetalleTitulo.Tag = "BitacoraRes_lblDetalleTitulo";
            this.lblDetalleTitulo.Text = "Detalle del registro";
            this.lblDetalleTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            // 
            // pnlSeparadorDetalle
            // 
            this.pnlSeparadorDetalle.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlSeparadorDetalle.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSeparadorDetalle.Size = new System.Drawing.Size(16, 520);
            this.pnlSeparadorDetalle.Name = "pnlSeparadorDetalle";
            // 
            // pnlPie
            // 
            this.pnlPie.Controls.Add(this.lblSeleccion);
            this.pnlPie.Controls.Add(this.btnHistorial);
            this.pnlPie.Controls.Add(this.btnRollback);
            this.pnlPie.Controls.Add(this.btnSalir);
            this.pnlPie.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPie.Size = new System.Drawing.Size(1280, 64);
            this.pnlPie.Name = "pnlPie";
            // 
            // lblSeleccion
            // 
            this.lblSeleccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSeleccion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSeleccion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeleccion.Padding = new System.Windows.Forms.Padding(24, 0, 10, 0);
            this.lblSeleccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSeleccion.Tag = "BitacoraRes_lblSeleccion";
            this.lblSeleccion.UseMnemonic = false;
            this.lblSeleccion.Text = "Los registros los genera el sistema y no se borran ni se editan.";
            this.lblSeleccion.Name = "lblSeleccion";
            // 
            // btnHistorial
            // 
            this.btnHistorial.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorial.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnHistorial.IconChar = FontAwesome.Sharp.IconChar.ListUl;
            this.btnHistorial.IconColor = System.Drawing.Color.Gold;
            this.btnHistorial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHistorial.IconSize = 22;
            this.btnHistorial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistorial.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnHistorial.Size = new System.Drawing.Size(170, 64);
            this.btnHistorial.Tag = "BitacoraRes_btnHistorial";
            this.btnHistorial.Text = "Ver historial";
            this.btnHistorial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnHistorial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHistorial.UseVisualStyleBackColor = false;
            this.btnHistorial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorial.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnHistorial.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnHistorial.Name = "btnHistorial";
            // 
            // btnRollback
            // 
            this.btnRollback.BackColor = System.Drawing.Color.Goldenrod;
            this.btnRollback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRollback.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRollback.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnRollback.IconChar = FontAwesome.Sharp.IconChar.ClockRotateLeft;
            this.btnRollback.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnRollback.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRollback.IconSize = 22;
            this.btnRollback.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRollback.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnRollback.Size = new System.Drawing.Size(220, 64);
            this.btnRollback.Tag = "BitacoraRes_btnRollback";
            this.btnRollback.Text = "Restaurar como activo";
            this.btnRollback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRollback.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRollback.UseVisualStyleBackColor = false;
            this.btnRollback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRollback.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnRollback.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRollback.Name = "btnRollback";
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnSalir.IconChar = FontAwesome.Sharp.IconChar.RightFromBracket;
            this.btnSalir.IconColor = System.Drawing.Color.Gold;
            this.btnSalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSalir.IconSize = 22;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSalir.Size = new System.Drawing.Size(150, 64);
            this.btnSalir.Tag = "BitacoraRes_btnSalir";
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSalir.Name = "btnSalir";
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.Controls.Add(this.pnlCampoCodigo);
            this.pnlFiltros.Controls.Add(this.pnlCampoNombre);
            this.pnlFiltros.Controls.Add(this.pnlCampoOperacion);
            this.pnlFiltros.Controls.Add(this.pnlCampoDesde);
            this.pnlFiltros.Controls.Add(this.pnlCampoHasta);
            this.pnlFiltros.Controls.Add(this.chkSoloActivos);
            this.pnlFiltros.Controls.Add(this.btnAplicar);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.AutoSize = true;
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(24, 8, 12, 0);
            this.pnlFiltros.Size = new System.Drawing.Size(1280, 70);
            this.pnlFiltros.Name = "pnlFiltros";
            // 
            // pnlCampoCodigo
            // 
            this.pnlCampoCodigo.Controls.Add(this.txtCodigo);
            this.pnlCampoCodigo.Controls.Add(this.lblEtiquetaCodigo);
            this.pnlCampoCodigo.Size = new System.Drawing.Size(150, 72);
            this.pnlCampoCodigo.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoCodigo.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoCodigo.Name = "pnlCampoCodigo";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(0, 24);
            this.txtCodigo.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtCodigo.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtCodigo.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtCodigo.BorderWidth = 2;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCodigo.IconChar = FontAwesome.Sharp.IconChar.Hashtag;
            this.txtCodigo.IconColor = System.Drawing.Color.Goldenrod;
            this.txtCodigo.IconColorRight = System.Drawing.Color.DimGray;
            this.txtCodigo.IconPadding = 4;
            this.txtCodigo.IconSize = 20;
            this.txtCodigo.MaxLength = 9;
            this.txtCodigo.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtCodigo.PlaceholderText = "Código";
            this.txtCodigo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCodigo.Size = new System.Drawing.Size(150, 44);
            this.txtCodigo.Name = "txtCodigo";
            // 
            // lblEtiquetaCodigo
            // 
            this.lblEtiquetaCodigo.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaCodigo.AutoSize = true;
            this.lblEtiquetaCodigo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaCodigo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaCodigo.Tag = "BitacoraRes_lblCodigo";
            this.lblEtiquetaCodigo.Text = "Código de reserva";
            this.lblEtiquetaCodigo.Name = "lblEtiquetaCodigo";
            // 
            // pnlCampoNombre
            // 
            this.pnlCampoNombre.Controls.Add(this.txtNombre);
            this.pnlCampoNombre.Controls.Add(this.lblEtiquetaNombre);
            this.pnlCampoNombre.Size = new System.Drawing.Size(240, 72);
            this.pnlCampoNombre.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoNombre.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoNombre.Name = "pnlCampoNombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(0, 24);
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNombre.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderWidth = 2;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombre.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtNombre.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombre.IconPadding = 4;
            this.txtNombre.IconSize = 20;
            this.txtNombre.MaxLength = 100;
            this.txtNombre.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombre.PlaceholderText = "Nombre";
            this.txtNombre.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombre.Size = new System.Drawing.Size(240, 44);
            this.txtNombre.Name = "txtNombre";
            // 
            // lblEtiquetaNombre
            // 
            this.lblEtiquetaNombre.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaNombre.AutoSize = true;
            this.lblEtiquetaNombre.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaNombre.Tag = "BitacoraRes_lblNombre";
            this.lblEtiquetaNombre.Text = "Nombre (huésped o habitación)";
            this.lblEtiquetaNombre.Name = "lblEtiquetaNombre";
            // 
            // pnlCampoOperacion
            // 
            this.pnlCampoOperacion.Controls.Add(this.cmbOperacion);
            this.pnlCampoOperacion.Controls.Add(this.lblEtiquetaOperacion);
            this.pnlCampoOperacion.Size = new System.Drawing.Size(220, 72);
            this.pnlCampoOperacion.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoOperacion.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoOperacion.Name = "pnlCampoOperacion";
            // 
            // cmbOperacion
            // 
            this.cmbOperacion.Location = new System.Drawing.Point(0, 24);
            this.cmbOperacion.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbOperacion.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbOperacion.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbOperacion.BorderWidth = 2;
            this.cmbOperacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbOperacion.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbOperacion.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbOperacion.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbOperacion.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.cmbOperacion.DropDownHighlightForeColor = System.Drawing.Color.Goldenrod;
            this.cmbOperacion.DropDownItemHeight = 32;
            this.cmbOperacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOperacion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbOperacion.IconChar = FontAwesome.Sharp.IconChar.ListCheck;
            this.cmbOperacion.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbOperacion.IconSize = 20;
            this.cmbOperacion.SelectedItem = null;
            this.cmbOperacion.SelectedValue = null;
            this.cmbOperacion.Size = new System.Drawing.Size(220, 44);
            this.cmbOperacion.Name = "cmbOperacion";
            // 
            // lblEtiquetaOperacion
            // 
            this.lblEtiquetaOperacion.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaOperacion.AutoSize = true;
            this.lblEtiquetaOperacion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaOperacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaOperacion.Tag = "BitacoraRes_lblOperacion";
            this.lblEtiquetaOperacion.Text = "Operación";
            this.lblEtiquetaOperacion.Name = "lblEtiquetaOperacion";
            // 
            // pnlCampoDesde
            // 
            this.pnlCampoDesde.Controls.Add(this.dtpDesde);
            this.pnlCampoDesde.Controls.Add(this.lblEtiquetaDesde);
            this.pnlCampoDesde.Size = new System.Drawing.Size(160, 72);
            this.pnlCampoDesde.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoDesde.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
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
            this.dtpDesde.Size = new System.Drawing.Size(160, 44);
            this.dtpDesde.Value = null;
            this.dtpDesde.Name = "dtpDesde";
            // 
            // lblEtiquetaDesde
            // 
            this.lblEtiquetaDesde.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaDesde.AutoSize = true;
            this.lblEtiquetaDesde.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaDesde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaDesde.Tag = "BitacoraRes_lblFechaDesde";
            this.lblEtiquetaDesde.Text = "Fecha inicio";
            this.lblEtiquetaDesde.Name = "lblEtiquetaDesde";
            // 
            // pnlCampoHasta
            // 
            this.pnlCampoHasta.Controls.Add(this.dtpHasta);
            this.pnlCampoHasta.Controls.Add(this.lblEtiquetaHasta);
            this.pnlCampoHasta.Size = new System.Drawing.Size(160, 72);
            this.pnlCampoHasta.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoHasta.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
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
            this.dtpHasta.Size = new System.Drawing.Size(160, 44);
            this.dtpHasta.Value = null;
            this.dtpHasta.Name = "dtpHasta";
            // 
            // lblEtiquetaHasta
            // 
            this.lblEtiquetaHasta.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaHasta.AutoSize = true;
            this.lblEtiquetaHasta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaHasta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaHasta.Tag = "BitacoraRes_lblFechaHasta";
            this.lblEtiquetaHasta.Text = "Fecha fin";
            this.lblEtiquetaHasta.Name = "lblEtiquetaHasta";
            // 
            // chkSoloActivos
            // 
            this.chkSoloActivos.AutoSize = true;
            this.chkSoloActivos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSoloActivos.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.chkSoloActivos.Margin = new System.Windows.Forms.Padding(4, 36, 16, 8);
            this.chkSoloActivos.Tag = "BitacoraRes_chkSoloActivos";
            this.chkSoloActivos.Text = "Solo activos";
            this.chkSoloActivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSoloActivos.Name = "chkSoloActivos";
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
            this.btnAplicar.Size = new System.Drawing.Size(140, 44);
            this.btnAplicar.Tag = "BitacoraRes_btnAplicar";
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAplicar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnAplicar.Margin = new System.Windows.Forms.Padding(0, 24, 12, 8);
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
            this.btnLimpiar.Size = new System.Drawing.Size(140, 44);
            this.btnLimpiar.Tag = "Crud_btnLimpiar";
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(0, 24, 12, 8);
            this.btnLimpiar.Name = "btnLimpiar";
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 12, 24, 8);
            this.pnlEncabezado.Size = new System.Drawing.Size(1280, 84);
            this.pnlEncabezado.Name = "pnlEncabezado";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.Tag = "BitacoraRes_lblSubtitulo";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSubtitulo.Text = "Cada alta y cada cambio de una reserva deja una foto completa. El registro activo es el estado actual; no se borra ningún registro.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Size = new System.Drawing.Size(1232, 36);
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Tag = "BitacoraRes_titulo";
            this.lblTitulo.Text = "Bitácora de cambios de reservas";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Name = "lblTitulo";
            // 
            // FormBitacoraReservas_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text = "Bitácora de cambios de reservas";
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.pnlPie);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FormBitacoraReservas_68SA";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlCampoHasta.ResumeLayout(false);
            this.pnlCampoHasta.PerformLayout();
            this.pnlCampoDesde.ResumeLayout(false);
            this.pnlCampoDesde.PerformLayout();
            this.pnlCampoOperacion.ResumeLayout(false);
            this.pnlCampoOperacion.PerformLayout();
            this.pnlCampoNombre.ResumeLayout(false);
            this.pnlCampoNombre.PerformLayout();
            this.pnlCampoCodigo.ResumeLayout(false);
            this.pnlCampoCodigo.PerformLayout();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlPie.ResumeLayout(false);
            this.pnlSeparadorDetalle.ResumeLayout(false);
            this.pnlDetalle.ResumeLayout(false);
            this.pnlGrilla.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.Windows.Forms.Panel pnlDetalle;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Label lblDetalleSub;
        private System.Windows.Forms.Label lblDetalleTitulo;
        private System.Windows.Forms.Panel pnlSeparadorDetalle;
        private System.Windows.Forms.Panel pnlPie;
        private System.Windows.Forms.Label lblSeleccion;
        private FontAwesome.Sharp.IconButton btnHistorial;
        private FontAwesome.Sharp.IconButton btnRollback;
        private FontAwesome.Sharp.IconButton btnSalir;
        private System.Windows.Forms.FlowLayoutPanel pnlFiltros;
        private System.Windows.Forms.Panel pnlCampoCodigo;
        private CustomControls.IconPlaceholderTextBox txtCodigo;
        private System.Windows.Forms.Label lblEtiquetaCodigo;
        private System.Windows.Forms.Panel pnlCampoNombre;
        private CustomControls.IconPlaceholderTextBox txtNombre;
        private System.Windows.Forms.Label lblEtiquetaNombre;
        private System.Windows.Forms.Panel pnlCampoOperacion;
        private CustomControls.IconComboBox cmbOperacion;
        private System.Windows.Forms.Label lblEtiquetaOperacion;
        private System.Windows.Forms.Panel pnlCampoDesde;
        private CustomControls.IconDateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblEtiquetaDesde;
        private System.Windows.Forms.Panel pnlCampoHasta;
        private CustomControls.IconDateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblEtiquetaHasta;
        private GUI_08YS.UserControls.CheckBoxHotel_68SA chkSoloActivos;
        private FontAwesome.Sharp.IconButton btnAplicar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
    }
}
