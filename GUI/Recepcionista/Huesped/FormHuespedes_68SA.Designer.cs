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
            this.components = new System.ComponentModel.Container();
            this.toolTipCrud = new System.Windows.Forms.ToolTip(this.components);

            this.pnlGrilla = new System.Windows.Forms.Panel();
            this.dgvHuespedes = new System.Windows.Forms.DataGridView();

            this.flpFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.txtNombreFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtApellidoFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtDocumentoFiltro = new CustomControls.IconPlaceholderTextBox();
            this.cmbTipoDocFiltro = new CustomControls.IconComboBox();
            this.txtNacionalidadFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtEmailFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtTelefonoFiltro = new CustomControls.IconPlaceholderTextBox();
            this.tblEdad = new System.Windows.Forms.TableLayoutPanel();
            this.cmbOperadorEdad = new CustomControls.IconComboBox();
            this.nudEdadDesde = new CustomControls.IconNumericUpDown();
            this.lblGuionEdad = new System.Windows.Forms.Label();
            this.nudEdadHasta = new CustomControls.IconNumericUpDown();

            this.pnlBotones = new System.Windows.Forms.Panel();
            this.flpBotonesIzquierda = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSerializar = new FontAwesome.Sharp.IconButton();
            this.btnDeserializar = new FontAwesome.Sharp.IconButton();
            this.flpBotonesDerecha = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFiltrar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();

            this.lblAvisoLimite = new System.Windows.Forms.Label();
            this.lblOrigenXml = new System.Windows.Forms.Label();

            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnNuevo = new FontAwesome.Sharp.IconButton();

            this.pnlGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHuespedes)).BeginInit();
            this.flpFiltros.SuspendLayout();
            this.tblEdad.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.flpBotonesIzquierda.SuspendLayout();
            this.flpBotonesDerecha.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlGrilla
            //
            this.pnlGrilla.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlGrilla.Controls.Add(this.dgvHuespedes);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Name = "pnlGrilla";
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(24, 0, 24, 16);
            this.pnlGrilla.TabIndex = 0;
            //
            // dgvHuespedes
            //
            this.dgvHuespedes.ColumnHeadersHeight = 29;
            this.dgvHuespedes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHuespedes.Name = "dgvHuespedes";
            this.dgvHuespedes.RowHeadersWidth = 51;
            this.dgvHuespedes.TabIndex = 0;
            //
            // flpFiltros
            //
            this.flpFiltros.AutoSize = true;
            this.flpFiltros.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpFiltros.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.flpFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpFiltros.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpFiltros.WrapContents = true;
            this.flpFiltros.Padding = new System.Windows.Forms.Padding(24, 12, 12, 0);
            this.flpFiltros.Controls.Add(this.txtNombreFiltro);
            this.flpFiltros.Controls.Add(this.txtApellidoFiltro);
            this.flpFiltros.Controls.Add(this.txtDocumentoFiltro);
            this.flpFiltros.Controls.Add(this.cmbTipoDocFiltro);
            this.flpFiltros.Controls.Add(this.txtNacionalidadFiltro);
            this.flpFiltros.Controls.Add(this.txtTelefonoFiltro);
            this.flpFiltros.Controls.Add(this.txtEmailFiltro);
            this.flpFiltros.Controls.Add(this.tblEdad);
            this.flpFiltros.Name = "flpFiltros";
            this.flpFiltros.TabIndex = 1;
            //
            // txtNombreFiltro
            //
            this.txtNombreFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
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
            this.txtNombreFiltro.MaxLength = 100;
            this.txtNombreFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.txtNombreFiltro.MinimumSize = new System.Drawing.Size(220, 49);
            this.txtNombreFiltro.Name = "txtNombreFiltro";
            this.txtNombreFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombreFiltro.PlaceholderText = "Nombre";
            this.txtNombreFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombreFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtNombreFiltro.TabIndex = 0;
            //
            // txtApellidoFiltro
            //
            this.txtApellidoFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
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
            this.txtApellidoFiltro.MaxLength = 100;
            this.txtApellidoFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.txtApellidoFiltro.MinimumSize = new System.Drawing.Size(220, 49);
            this.txtApellidoFiltro.Name = "txtApellidoFiltro";
            this.txtApellidoFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtApellidoFiltro.PlaceholderText = "Apellido";
            this.txtApellidoFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtApellidoFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtApellidoFiltro.TabIndex = 1;
            //
            // txtDocumentoFiltro
            //
            this.txtDocumentoFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
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
            this.txtDocumentoFiltro.MaxLength = 20;
            this.txtDocumentoFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.txtDocumentoFiltro.MinimumSize = new System.Drawing.Size(220, 49);
            this.txtDocumentoFiltro.Name = "txtDocumentoFiltro";
            this.txtDocumentoFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtDocumentoFiltro.PlaceholderText = "N° Documento";
            this.txtDocumentoFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDocumentoFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtDocumentoFiltro.TabIndex = 2;
            //
            // cmbTipoDocFiltro
            //
            this.cmbTipoDocFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipoDocFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocFiltro.BorderWidth = 2;
            this.cmbTipoDocFiltro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbTipoDocFiltro.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipoDocFiltro.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocFiltro.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocFiltro.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(5, 5, 100);
            this.cmbTipoDocFiltro.DropDownHighlightForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocFiltro.DropDownItemHeight = 28;
            this.cmbTipoDocFiltro.DropDownMaxHeight = 140;
            this.cmbTipoDocFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoDocFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocFiltro.IconChar = FontAwesome.Sharp.IconChar.IdCard;
            this.cmbTipoDocFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocFiltro.IconPadding = 4;
            this.cmbTipoDocFiltro.IconSize = 20;
            this.cmbTipoDocFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.cmbTipoDocFiltro.MinimumSize = new System.Drawing.Size(220, 49);
            this.cmbTipoDocFiltro.Name = "cmbTipoDocFiltro";
            this.cmbTipoDocFiltro.SelectedItem = null;
            this.cmbTipoDocFiltro.SelectedValue = null;
            this.cmbTipoDocFiltro.Size = new System.Drawing.Size(220, 49);
            this.cmbTipoDocFiltro.TabIndex = 3;
            //
            // txtNacionalidadFiltro
            //
            this.txtNacionalidadFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
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
            this.txtNacionalidadFiltro.MaxLength = 50;
            this.txtNacionalidadFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.txtNacionalidadFiltro.MinimumSize = new System.Drawing.Size(220, 49);
            this.txtNacionalidadFiltro.Name = "txtNacionalidadFiltro";
            this.txtNacionalidadFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNacionalidadFiltro.PlaceholderText = "Nacionalidad";
            this.txtNacionalidadFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNacionalidadFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtNacionalidadFiltro.TabIndex = 4;
            //
            // txtEmailFiltro
            //
            this.txtEmailFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
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
            this.txtEmailFiltro.MaxLength = 150;
            this.txtEmailFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.txtEmailFiltro.MinimumSize = new System.Drawing.Size(220, 49);
            this.txtEmailFiltro.Name = "txtEmailFiltro";
            this.txtEmailFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtEmailFiltro.PlaceholderText = "Email";
            this.txtEmailFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmailFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtEmailFiltro.TabIndex = 5;
            //
            // txtTelefonoFiltro
            //
            this.txtTelefonoFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
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
            this.txtTelefonoFiltro.MaxLength = 30;
            this.txtTelefonoFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.txtTelefonoFiltro.MinimumSize = new System.Drawing.Size(220, 49);
            this.txtTelefonoFiltro.Name = "txtTelefonoFiltro";
            this.txtTelefonoFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtTelefonoFiltro.PlaceholderText = "Teléfono";
            this.txtTelefonoFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTelefonoFiltro.Size = new System.Drawing.Size(220, 49);
            this.txtTelefonoFiltro.TabIndex = 6;
            //
            // tblEdad
            //
            this.tblEdad.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tblEdad.ColumnCount = 4;
            this.tblEdad.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEdad.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tblEdad.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tblEdad.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tblEdad.Controls.Add(this.cmbOperadorEdad, 0, 0);
            this.tblEdad.Controls.Add(this.nudEdadDesde, 1, 0);
            this.tblEdad.Controls.Add(this.lblGuionEdad, 2, 0);
            this.tblEdad.Controls.Add(this.nudEdadHasta, 3, 0);
            this.tblEdad.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.tblEdad.MinimumSize = new System.Drawing.Size(370, 49);
            this.tblEdad.Name = "tblEdad";
            this.tblEdad.Padding = new System.Windows.Forms.Padding(0);
            this.tblEdad.RowCount = 1;
            this.tblEdad.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEdad.Size = new System.Drawing.Size(370, 49);
            this.tblEdad.TabIndex = 7;
            //
            // cmbOperadorEdad
            //
            this.cmbOperadorEdad.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbOperadorEdad.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbOperadorEdad.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbOperadorEdad.BorderWidth = 2;
            this.cmbOperadorEdad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbOperadorEdad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbOperadorEdad.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbOperadorEdad.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbOperadorEdad.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbOperadorEdad.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(5, 5, 100);
            this.cmbOperadorEdad.DropDownHighlightForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbOperadorEdad.DropDownItemHeight = 28;
            this.cmbOperadorEdad.DropDownMaxHeight = 140;
            this.cmbOperadorEdad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbOperadorEdad.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbOperadorEdad.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.cmbOperadorEdad.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbOperadorEdad.IconPadding = 4;
            this.cmbOperadorEdad.IconSize = 20;
            this.cmbOperadorEdad.Margin = new System.Windows.Forms.Padding(0);
            this.cmbOperadorEdad.MinimumSize = new System.Drawing.Size(150, 49);
            this.cmbOperadorEdad.Name = "cmbOperadorEdad";
            this.cmbOperadorEdad.SelectedItem = null;
            this.cmbOperadorEdad.SelectedValue = null;
            this.cmbOperadorEdad.Size = new System.Drawing.Size(160, 49);
            this.cmbOperadorEdad.TabIndex = 0;
            //
            // nudEdadDesde
            //
            this.nudEdadDesde.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudEdadDesde.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudEdadDesde.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudEdadDesde.BorderWidth = 2;
            this.nudEdadDesde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudEdadDesde.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudEdadDesde.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudEdadDesde.IconChar = FontAwesome.Sharp.IconChar.None;
            this.nudEdadDesde.IconColor = System.Drawing.Color.Goldenrod;
            this.nudEdadDesde.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.nudEdadDesde.Maximum = new decimal(new int[] { 150, 0, 0, 0 });
            this.nudEdadDesde.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudEdadDesde.Name = "nudEdadDesde";
            this.nudEdadDesde.Size = new System.Drawing.Size(84, 49);
            this.nudEdadDesde.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudEdadDesde.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudEdadDesde.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudEdadDesde.TabIndex = 1;
            this.nudEdadDesde.Value = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudEdadDesde.Visible = false;
            //
            // lblGuionEdad
            //
            this.lblGuionEdad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblGuionEdad.AutoSize = true;
            this.lblGuionEdad.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblGuionEdad.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblGuionEdad.Name = "lblGuionEdad";
            this.lblGuionEdad.TabIndex = 2;
            this.lblGuionEdad.Text = "—";
            this.lblGuionEdad.Visible = false;
            //
            // nudEdadHasta
            //
            this.nudEdadHasta.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudEdadHasta.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudEdadHasta.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudEdadHasta.BorderWidth = 2;
            this.nudEdadHasta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudEdadHasta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudEdadHasta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudEdadHasta.IconChar = FontAwesome.Sharp.IconChar.None;
            this.nudEdadHasta.IconColor = System.Drawing.Color.Goldenrod;
            this.nudEdadHasta.Margin = new System.Windows.Forms.Padding(0);
            this.nudEdadHasta.Maximum = new decimal(new int[] { 150, 0, 0, 0 });
            this.nudEdadHasta.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudEdadHasta.Name = "nudEdadHasta";
            this.nudEdadHasta.Size = new System.Drawing.Size(84, 49);
            this.nudEdadHasta.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudEdadHasta.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudEdadHasta.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudEdadHasta.TabIndex = 3;
            this.nudEdadHasta.Value = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudEdadHasta.Visible = false;
            //
            // pnlBotones
            //
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlBotones.Controls.Add(this.flpBotonesDerecha);
            this.pnlBotones.Controls.Add(this.flpBotonesIzquierda);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBotones.Height = 64;
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.TabIndex = 2;
            //
            // flpBotonesIzquierda
            //
            this.flpBotonesIzquierda.AutoSize = true;
            this.flpBotonesIzquierda.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpBotonesIzquierda.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpBotonesIzquierda.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpBotonesIzquierda.WrapContents = false;
            this.flpBotonesIzquierda.Padding = new System.Windows.Forms.Padding(24, 8, 0, 8);
            this.flpBotonesIzquierda.Controls.Add(this.btnSerializar);
            this.flpBotonesIzquierda.Controls.Add(this.btnDeserializar);
            this.flpBotonesIzquierda.Name = "flpBotonesIzquierda";
            this.flpBotonesIzquierda.TabIndex = 0;
            //
            // btnSerializar
            //
            this.btnSerializar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnSerializar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerializar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSerializar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnSerializar.IconChar = FontAwesome.Sharp.IconChar.FileExport;
            this.btnSerializar.IconColor = System.Drawing.Color.Goldenrod;
            this.btnSerializar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSerializar.IconSize = 22;
            this.btnSerializar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSerializar.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btnSerializar.MinimumSize = new System.Drawing.Size(150, 48);
            this.btnSerializar.Name = "btnSerializar";
            this.btnSerializar.Size = new System.Drawing.Size(150, 48);
            this.btnSerializar.TabIndex = 0;
            this.btnSerializar.Tag = "Huespedes_btnSerializar";
            this.btnSerializar.Text = "Serializar";
            this.btnSerializar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSerializar.UseVisualStyleBackColor = false;
            //
            // btnDeserializar
            //
            this.btnDeserializar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnDeserializar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeserializar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeserializar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnDeserializar.IconChar = FontAwesome.Sharp.IconChar.FileImport;
            this.btnDeserializar.IconColor = System.Drawing.Color.Goldenrod;
            this.btnDeserializar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDeserializar.IconSize = 22;
            this.btnDeserializar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeserializar.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeserializar.MinimumSize = new System.Drawing.Size(160, 48);
            this.btnDeserializar.Name = "btnDeserializar";
            this.btnDeserializar.Size = new System.Drawing.Size(160, 48);
            this.btnDeserializar.TabIndex = 1;
            this.btnDeserializar.Tag = "Huespedes_btnDeserializar";
            this.btnDeserializar.Text = "Deserializar";
            this.btnDeserializar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeserializar.UseVisualStyleBackColor = false;
            //
            // flpBotonesDerecha
            //
            this.flpBotonesDerecha.AutoSize = true;
            this.flpBotonesDerecha.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpBotonesDerecha.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpBotonesDerecha.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpBotonesDerecha.WrapContents = false;
            this.flpBotonesDerecha.Padding = new System.Windows.Forms.Padding(0, 8, 24, 8);
            this.flpBotonesDerecha.Controls.Add(this.btnFiltrar);
            this.flpBotonesDerecha.Controls.Add(this.btnLimpiar);
            this.flpBotonesDerecha.Name = "flpBotonesDerecha";
            this.flpBotonesDerecha.TabIndex = 1;
            //
            // btnFiltrar
            //
            this.btnFiltrar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFiltrar.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnFiltrar.IconChar = FontAwesome.Sharp.IconChar.Filter;
            this.btnFiltrar.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnFiltrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFiltrar.IconSize = 22;
            this.btnFiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btnFiltrar.MinimumSize = new System.Drawing.Size(140, 48);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(140, 48);
            this.btnFiltrar.TabIndex = 0;
            this.btnFiltrar.Tag = "Crud_btnFiltrar";
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltrar.UseVisualStyleBackColor = false;
            //
            // btnLimpiar
            //
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.btnLimpiar.IconColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 22;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpiar.MinimumSize = new System.Drawing.Size(140, 48);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(140, 48);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Tag = "Crud_btnLimpiar";
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            //
            // lblAvisoLimite
            //
            this.lblAvisoLimite.AutoSize = false;
            this.lblAvisoLimite.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAvisoLimite.ForeColor = System.Drawing.Color.FromArgb(230, 175, 46);
            this.lblAvisoLimite.Height = 28;
            this.lblAvisoLimite.Name = "lblAvisoLimite";
            this.lblAvisoLimite.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            this.lblAvisoLimite.TabIndex = 3;
            this.lblAvisoLimite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAvisoLimite.Visible = false;
            //
            // lblOrigenXml
            //
            this.lblOrigenXml.AutoSize = false;
            this.lblOrigenXml.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrigenXml.ForeColor = System.Drawing.Color.FromArgb(230, 175, 46);
            this.lblOrigenXml.Height = 28;
            this.lblOrigenXml.Name = "lblOrigenXml";
            this.lblOrigenXml.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            this.lblOrigenXml.TabIndex = 4;
            this.lblOrigenXml.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOrigenXml.Visible = false;
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.btnNuevo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Height = 72;
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 12, 24, 8);
            this.pnlEncabezado.TabIndex = 5;
            //
            // lblTitulo
            //
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Tag = "Huespedes_titulo";
            this.lblTitulo.Text = "Huéspedes";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnNuevo
            //
            this.btnNuevo.BackColor = System.Drawing.Color.Goldenrod;
            this.btnNuevo.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnNuevo.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnNuevo.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNuevo.IconSize = 22;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.MinimumSize = new System.Drawing.Size(190, 52);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(190, 52);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Tag = "Huespedes_btnNuevo";
            this.btnNuevo.Text = "Nuevo huésped";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevo.UseVisualStyleBackColor = false;
            //
            // FormHuespedes_68SA
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1470, 920);
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.lblOrigenXml);
            this.Controls.Add(this.lblAvisoLimite);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.flpFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHuespedes_68SA";
            this.Text = "Huéspedes";
            this.pnlGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHuespedes)).EndInit();
            this.flpFiltros.ResumeLayout(false);
            this.flpFiltros.PerformLayout();
            this.tblEdad.ResumeLayout(false);
            this.tblEdad.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.pnlBotones.PerformLayout();
            this.flpBotonesIzquierda.ResumeLayout(false);
            this.flpBotonesDerecha.ResumeLayout(false);
            this.pnlEncabezado.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvHuespedes;
        private System.Windows.Forms.FlowLayoutPanel flpFiltros;
        private CustomControls.IconPlaceholderTextBox txtNombreFiltro;
        private CustomControls.IconPlaceholderTextBox txtApellidoFiltro;
        private CustomControls.IconPlaceholderTextBox txtDocumentoFiltro;
        private CustomControls.IconComboBox cmbTipoDocFiltro;
        private CustomControls.IconPlaceholderTextBox txtNacionalidadFiltro;
        private CustomControls.IconPlaceholderTextBox txtEmailFiltro;
        private CustomControls.IconPlaceholderTextBox txtTelefonoFiltro;
        private System.Windows.Forms.TableLayoutPanel tblEdad;
        private CustomControls.IconComboBox cmbOperadorEdad;
        private CustomControls.IconNumericUpDown nudEdadDesde;
        private System.Windows.Forms.Label lblGuionEdad;
        private CustomControls.IconNumericUpDown nudEdadHasta;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.FlowLayoutPanel flpBotonesIzquierda;
        private FontAwesome.Sharp.IconButton btnSerializar;
        private FontAwesome.Sharp.IconButton btnDeserializar;
        private System.Windows.Forms.FlowLayoutPanel flpBotonesDerecha;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Label lblAvisoLimite;
        private System.Windows.Forms.Label lblOrigenXml;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnNuevo;
        private System.Windows.Forms.ToolTip toolTipCrud;
    }
}