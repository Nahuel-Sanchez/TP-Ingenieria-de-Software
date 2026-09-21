namespace GUI_08YS.Recepcionista
{
    partial class FormHuespedes_68SA
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlGrilla = new System.Windows.Forms.Panel();
            this.dgvHuespedes = new System.Windows.Forms.DataGridView();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.lblAvisoLimite = new System.Windows.Forms.Label();
            this.lblNacHasta = new System.Windows.Forms.Label();
            this.lblNacDesde = new System.Windows.Forms.Label();
            this.txtTelefonoFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtEmailFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtNacionalidadFiltro = new CustomControls.IconPlaceholderTextBox();
            this.cmbTipoDocFiltro = new CustomControls.IconComboBox();
            this.txtDocumentoFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtApellidoFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtNombreFiltro = new CustomControls.IconPlaceholderTextBox();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.btnFiltrar = new FontAwesome.Sharp.IconButton();
            this.btnNuevo = new FontAwesome.Sharp.IconButton();
            this.dtpNacDesde = new CustomControls.IconDateTimePicker();
            this.dtpNacHasta = new CustomControls.IconDateTimePicker();
            this.pnlGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHuespedes)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.pnlGrilla.Controls.Add(this.dgvHuespedes);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Location = new System.Drawing.Point(0, 297);
            this.pnlGrilla.Name = "pnlGrilla";
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(24, 0, 24, 16);
            this.pnlGrilla.Size = new System.Drawing.Size(1470, 623);
            this.pnlGrilla.TabIndex = 0;
            // 
            // dgvHuespedes
            // 
            this.dgvHuespedes.ColumnHeadersHeight = 29;
            this.dgvHuespedes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHuespedes.Location = new System.Drawing.Point(24, 0);
            this.dgvHuespedes.Name = "dgvHuespedes";
            this.dgvHuespedes.RowHeadersWidth = 51;
            this.dgvHuespedes.Size = new System.Drawing.Size(1422, 607);
            this.dgvHuespedes.TabIndex = 0;
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.pnlFiltros.Controls.Add(this.dtpNacHasta);
            this.pnlFiltros.Controls.Add(this.dtpNacDesde);
            this.pnlFiltros.Controls.Add(this.lblAvisoLimite);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.Controls.Add(this.btnFiltrar);
            this.pnlFiltros.Controls.Add(this.lblNacHasta);
            this.pnlFiltros.Controls.Add(this.lblNacDesde);
            this.pnlFiltros.Controls.Add(this.txtTelefonoFiltro);
            this.pnlFiltros.Controls.Add(this.txtEmailFiltro);
            this.pnlFiltros.Controls.Add(this.txtNacionalidadFiltro);
            this.pnlFiltros.Controls.Add(this.cmbTipoDocFiltro);
            this.pnlFiltros.Controls.Add(this.txtDocumentoFiltro);
            this.pnlFiltros.Controls.Add(this.txtApellidoFiltro);
            this.pnlFiltros.Controls.Add(this.txtNombreFiltro);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 72);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(1470, 225);
            this.pnlFiltros.TabIndex = 1;
            // 
            // lblAvisoLimite
            // 
            this.lblAvisoLimite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(175)))), ((int)(((byte)(46)))));
            this.lblAvisoLimite.Location = new System.Drawing.Point(24, 200);
            this.lblAvisoLimite.Name = "lblAvisoLimite";
            this.lblAvisoLimite.Size = new System.Drawing.Size(900, 20);
            this.lblAvisoLimite.TabIndex = 13;
            this.lblAvisoLimite.Visible = false;
            // 
            // lblNacHasta
            // 
            this.lblNacHasta.AutoSize = true;
            this.lblNacHasta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblNacHasta.Location = new System.Drawing.Point(256, 126);
            this.lblNacHasta.Name = "lblNacHasta";
            this.lblNacHasta.Size = new System.Drawing.Size(111, 16);
            this.lblNacHasta.TabIndex = 9;
            this.lblNacHasta.Tag = "Huespedes_lblNacHasta";
            this.lblNacHasta.Text = "Nacimiento hasta";
            // 
            // lblNacDesde
            // 
            this.lblNacDesde.AutoSize = true;
            this.lblNacDesde.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblNacDesde.Location = new System.Drawing.Point(24, 126);
            this.lblNacDesde.Name = "lblNacDesde";
            this.lblNacDesde.Size = new System.Drawing.Size(117, 16);
            this.lblNacDesde.TabIndex = 7;
            this.lblNacDesde.Tag = "Huespedes_lblNacDesde";
            this.lblNacDesde.Text = "Nacimiento desde";
            // 
            // txtTelefonoFiltro
            // 
            this.txtTelefonoFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtTelefonoFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtTelefonoFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtTelefonoFiltro.BorderWidth = 2;
            this.txtTelefonoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTelefonoFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTelefonoFiltro.IconChar = FontAwesome.Sharp.IconChar.Phone;
            this.txtTelefonoFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtTelefonoFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtTelefonoFiltro.IconPadding = 4;
            this.txtTelefonoFiltro.IconSize = 20;
            this.txtTelefonoFiltro.Location = new System.Drawing.Point(488, 68);
            this.txtTelefonoFiltro.MaxLength = 30;
            this.txtTelefonoFiltro.Name = "txtTelefonoFiltro";
            this.txtTelefonoFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtTelefonoFiltro.PlaceholderText = "Teléfono";
            this.txtTelefonoFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTelefonoFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtTelefonoFiltro.TabIndex = 6;
            // 
            // txtEmailFiltro
            // 
            this.txtEmailFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtEmailFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtEmailFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtEmailFiltro.BorderWidth = 2;
            this.txtEmailFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmailFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEmailFiltro.IconChar = FontAwesome.Sharp.IconChar.Envelope;
            this.txtEmailFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtEmailFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtEmailFiltro.IconPadding = 4;
            this.txtEmailFiltro.IconSize = 20;
            this.txtEmailFiltro.Location = new System.Drawing.Point(256, 68);
            this.txtEmailFiltro.MaxLength = 150;
            this.txtEmailFiltro.Name = "txtEmailFiltro";
            this.txtEmailFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtEmailFiltro.PlaceholderText = "Email";
            this.txtEmailFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmailFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtEmailFiltro.TabIndex = 5;
            // 
            // txtNacionalidadFiltro
            // 
            this.txtNacionalidadFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtNacionalidadFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNacionalidadFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNacionalidadFiltro.BorderWidth = 2;
            this.txtNacionalidadFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNacionalidadFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNacionalidadFiltro.IconChar = FontAwesome.Sharp.IconChar.Globe;
            this.txtNacionalidadFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNacionalidadFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNacionalidadFiltro.IconPadding = 4;
            this.txtNacionalidadFiltro.IconSize = 20;
            this.txtNacionalidadFiltro.Location = new System.Drawing.Point(24, 68);
            this.txtNacionalidadFiltro.MaxLength = 50;
            this.txtNacionalidadFiltro.Name = "txtNacionalidadFiltro";
            this.txtNacionalidadFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNacionalidadFiltro.PlaceholderText = "Nacionalidad";
            this.txtNacionalidadFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNacionalidadFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtNacionalidadFiltro.TabIndex = 4;
            // 
            // cmbTipoDocFiltro
            // 
            this.cmbTipoDocFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.cmbTipoDocFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocFiltro.BorderWidth = 2;
            this.cmbTipoDocFiltro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbTipoDocFiltro.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.cmbTipoDocFiltro.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocFiltro.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocFiltro.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(100)))));
            this.cmbTipoDocFiltro.DropDownHighlightForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocFiltro.DropDownItemHeight = 28;
            this.cmbTipoDocFiltro.DropDownMaxHeight = 140;
            this.cmbTipoDocFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoDocFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocFiltro.IconChar = FontAwesome.Sharp.IconChar.IdCard;
            this.cmbTipoDocFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocFiltro.IconPadding = 4;
            this.cmbTipoDocFiltro.IconSize = 20;
            this.cmbTipoDocFiltro.Location = new System.Drawing.Point(720, 8);
            this.cmbTipoDocFiltro.Name = "cmbTipoDocFiltro";
            this.cmbTipoDocFiltro.SelectedItem = null;
            this.cmbTipoDocFiltro.SelectedValue = null;
            this.cmbTipoDocFiltro.Size = new System.Drawing.Size(220, 49);
            this.cmbTipoDocFiltro.TabIndex = 3;
            // 
            // txtDocumentoFiltro
            // 
            this.txtDocumentoFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtDocumentoFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtDocumentoFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtDocumentoFiltro.BorderWidth = 2;
            this.txtDocumentoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDocumentoFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDocumentoFiltro.IconChar = FontAwesome.Sharp.IconChar.Hashtag;
            this.txtDocumentoFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtDocumentoFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtDocumentoFiltro.IconPadding = 4;
            this.txtDocumentoFiltro.IconSize = 20;
            this.txtDocumentoFiltro.Location = new System.Drawing.Point(488, 8);
            this.txtDocumentoFiltro.MaxLength = 20;
            this.txtDocumentoFiltro.Name = "txtDocumentoFiltro";
            this.txtDocumentoFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtDocumentoFiltro.PlaceholderText = "N° Documento";
            this.txtDocumentoFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDocumentoFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtDocumentoFiltro.TabIndex = 2;
            // 
            // txtApellidoFiltro
            // 
            this.txtApellidoFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtApellidoFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtApellidoFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtApellidoFiltro.BorderWidth = 2;
            this.txtApellidoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtApellidoFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtApellidoFiltro.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtApellidoFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtApellidoFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtApellidoFiltro.IconPadding = 4;
            this.txtApellidoFiltro.IconSize = 20;
            this.txtApellidoFiltro.Location = new System.Drawing.Point(256, 8);
            this.txtApellidoFiltro.MaxLength = 100;
            this.txtApellidoFiltro.Name = "txtApellidoFiltro";
            this.txtApellidoFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtApellidoFiltro.PlaceholderText = "Apellido";
            this.txtApellidoFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtApellidoFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtApellidoFiltro.TabIndex = 1;
            // 
            // txtNombreFiltro
            // 
            this.txtNombreFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtNombreFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.BorderWidth = 2;
            this.txtNombreFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombreFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombreFiltro.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtNombreFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombreFiltro.IconPadding = 4;
            this.txtNombreFiltro.IconSize = 20;
            this.txtNombreFiltro.Location = new System.Drawing.Point(24, 8);
            this.txtNombreFiltro.MaxLength = 100;
            this.txtNombreFiltro.Name = "txtNombreFiltro";
            this.txtNombreFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombreFiltro.PlaceholderText = "Nombre";
            this.txtNombreFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombreFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtNombreFiltro.TabIndex = 0;
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.btnNuevo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 12, 24, 8);
            this.pnlEncabezado.Size = new System.Drawing.Size(1470, 72);
            this.pnlEncabezado.TabIndex = 2;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Location = new System.Drawing.Point(24, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1232, 52);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Tag = "Huespedes_titulo";
            this.lblTitulo.Text = "Huéspedes";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.btnLimpiar.IconColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 22;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(640, 145);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(140, 49);
            this.btnLimpiar.TabIndex = 12;
            this.btnLimpiar.Tag = "Crud_btnLimpiar";
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFiltrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnFiltrar.IconChar = FontAwesome.Sharp.IconChar.Filter;
            this.btnFiltrar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnFiltrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFiltrar.IconSize = 22;
            this.btnFiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.Location = new System.Drawing.Point(488, 145);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(140, 49);
            this.btnFiltrar.TabIndex = 11;
            this.btnFiltrar.Tag = "Crud_btnFiltrar";
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltrar.UseVisualStyleBackColor = false;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Goldenrod;
            this.btnNuevo.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnNuevo.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnNuevo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNuevo.IconSize = 22;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(1256, 12);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(190, 52);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Tag = "Huespedes_btnNuevo";
            this.btnNuevo.Text = "Nuevo huésped";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevo.UseVisualStyleBackColor = false;
            // 
            // dtpNacDesde
            // 
            this.dtpNacDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpNacDesde.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpNacDesde.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpNacDesde.BorderWidth = 2;
            this.dtpNacDesde.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpNacDesde.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpNacDesde.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.dtpNacDesde.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpNacDesde.CalendarTrailingForeColor = System.Drawing.Color.Silver;
            this.dtpNacDesde.Checked = false;
            this.dtpNacDesde.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpNacDesde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNacDesde.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpNacDesde.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpNacDesde.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpNacDesde.IconSize = 22;
            this.dtpNacDesde.Location = new System.Drawing.Point(24, 145);
            this.dtpNacDesde.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNacDesde.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNacDesde.Name = "dtpNacDesde";
            this.dtpNacDesde.Size = new System.Drawing.Size(220, 49);
            this.dtpNacDesde.TabIndex = 14;
            this.dtpNacDesde.TimeValue = null;
            this.dtpNacDesde.Value = null;
            // 
            // dtpNacHasta
            // 
            this.dtpNacHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpNacHasta.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpNacHasta.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpNacHasta.BorderWidth = 2;
            this.dtpNacHasta.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpNacHasta.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpNacHasta.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.dtpNacHasta.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpNacHasta.CalendarTrailingForeColor = System.Drawing.Color.Silver;
            this.dtpNacHasta.Checked = false;
            this.dtpNacHasta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpNacHasta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNacHasta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpNacHasta.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpNacHasta.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpNacHasta.IconSize = 22;
            this.dtpNacHasta.Location = new System.Drawing.Point(256, 145);
            this.dtpNacHasta.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNacHasta.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNacHasta.Name = "dtpNacHasta";
            this.dtpNacHasta.Size = new System.Drawing.Size(220, 49);
            this.dtpNacHasta.TabIndex = 15;
            this.dtpNacHasta.TimeValue = null;
            this.dtpNacHasta.Value = null;
            // 
            // FormHuespedes_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1470, 920);
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHuespedes_68SA";
            this.Text = "Huéspedes";
            this.pnlGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHuespedes)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvHuespedes;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblAvisoLimite;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private System.Windows.Forms.Label lblNacHasta;
        private System.Windows.Forms.Label lblNacDesde;
        private CustomControls.IconPlaceholderTextBox txtTelefonoFiltro;
        private CustomControls.IconPlaceholderTextBox txtEmailFiltro;
        private CustomControls.IconPlaceholderTextBox txtNacionalidadFiltro;
        private CustomControls.IconComboBox cmbTipoDocFiltro;
        private CustomControls.IconPlaceholderTextBox txtDocumentoFiltro;
        private CustomControls.IconPlaceholderTextBox txtApellidoFiltro;
        private CustomControls.IconPlaceholderTextBox txtNombreFiltro;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnNuevo;
        private CustomControls.IconDateTimePicker dtpNacHasta;
        private CustomControls.IconDateTimePicker dtpNacDesde;
    }
}