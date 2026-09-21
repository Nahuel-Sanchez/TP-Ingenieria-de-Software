namespace GUI_08YS.Recepcionista
{
    partial class FormRegistrarHuesped_68SA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBorde = new System.Windows.Forms.Panel();
            this.btnRegistrar = new FontAwesome.Sharp.IconButton();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.pnlLinea1 = new System.Windows.Forms.Panel();
            this.lblEtiquetaEmail = new System.Windows.Forms.Label();
            this.txtEmail = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaNacionalidad = new System.Windows.Forms.Label();
            this.txtNacionalidad = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaTipoDocumento = new System.Windows.Forms.Label();
            this.cmbTipoDocumento = new CustomControls.IconComboBox();
            this.lblEtiquetaApellido = new System.Windows.Forms.Label();
            this.txtApellido = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaNombre = new System.Windows.Forms.Label();
            this.txtNombre = new CustomControls.IconPlaceholderTextBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnCerrar = new FontAwesome.Sharp.IconButton();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.iconTitulo = new FontAwesome.Sharp.IconPictureBox();
            this.pnlBorde.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTitulo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBorde
            // 
            this.pnlBorde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.pnlBorde.Controls.Add(this.btnRegistrar);
            this.pnlBorde.Controls.Add(this.btnCancelar);
            this.pnlBorde.Controls.Add(this.pnlLinea1);
            this.pnlBorde.Controls.Add(this.lblEtiquetaEmail);
            this.pnlBorde.Controls.Add(this.txtEmail);
            this.pnlBorde.Controls.Add(this.lblEtiquetaTelefono);
            this.pnlBorde.Controls.Add(this.txtTelefono);
            this.pnlBorde.Controls.Add(this.lblEtiquetaFechaNacimiento);
            this.pnlBorde.Controls.Add(this.dtpFechaNacimiento);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNacionalidad);
            this.pnlBorde.Controls.Add(this.txtNacionalidad);
            this.pnlBorde.Controls.Add(this.lblEtiquetaDocumento);
            this.pnlBorde.Controls.Add(this.txtDocumento);
            this.pnlBorde.Controls.Add(this.lblEtiquetaTipoDocumento);
            this.pnlBorde.Controls.Add(this.cmbTipoDocumento);
            this.pnlBorde.Controls.Add(this.lblEtiquetaApellido);
            this.pnlBorde.Controls.Add(this.txtApellido);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNombre);
            this.pnlBorde.Controls.Add(this.txtNombre);
            this.pnlBorde.Controls.Add(this.pnlHeader);
            this.pnlBorde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorde.Location = new System.Drawing.Point(3, 3);
            this.pnlBorde.Name = "pnlBorde";
            this.pnlBorde.Size = new System.Drawing.Size(709, 534);
            this.pnlBorde.TabIndex = 0;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnRegistrar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnRegistrar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnRegistrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRegistrar.IconSize = 22;
            this.btnRegistrar.Location = new System.Drawing.Point(366, 448);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(183, 49);
            this.btnRegistrar.TabIndex = 21;
            this.btnRegistrar.Tag = "RegistrarHuesped_btnRegistrar";
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.btnCancelar.IconColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancelar.IconSize = 22;
            this.btnCancelar.Location = new System.Drawing.Point(160, 448);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(183, 49);
            this.btnCancelar.TabIndex = 20;
            this.btnCancelar.Tag = "RegistrarHuesped_btnCancelar";
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // pnlLinea1
            // 
            this.pnlLinea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(75)))), ((int)(((byte)(110)))));
            this.pnlLinea1.Location = new System.Drawing.Point(23, 429);
            this.pnlLinea1.Name = "pnlLinea1";
            this.pnlLinea1.Size = new System.Drawing.Size(663, 1);
            this.pnlLinea1.TabIndex = 19;
            // 
            // lblEtiquetaEmail
            // 
            this.lblEtiquetaEmail.AutoSize = true;
            this.lblEtiquetaEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaEmail.Location = new System.Drawing.Point(366, 346);
            this.lblEtiquetaEmail.Name = "lblEtiquetaEmail";
            this.lblEtiquetaEmail.Size = new System.Drawing.Size(41, 16);
            this.lblEtiquetaEmail.TabIndex = 17;
            this.lblEtiquetaEmail.Tag = "RegistrarHuesped_lblEmail";
            this.lblEtiquetaEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtEmail.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtEmail.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtEmail.BorderWidth = 2;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEmail.IconChar = FontAwesome.Sharp.IconChar.Envelope;
            this.txtEmail.IconColor = System.Drawing.Color.Goldenrod;
            this.txtEmail.IconColorRight = System.Drawing.Color.DimGray;
            this.txtEmail.IconPadding = 4;
            this.txtEmail.IconSize = 20;
            this.txtEmail.Location = new System.Drawing.Point(366, 365);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtEmail.PlaceholderText = "Email de contacto";
            this.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmail.Size = new System.Drawing.Size(320, 49);
            this.txtEmail.TabIndex = 18;
            // 
            // lblEtiquetaTelefono
            // 
            this.lblEtiquetaTelefono.AutoSize = true;
            this.lblEtiquetaTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaTelefono.Location = new System.Drawing.Point(23, 346);
            this.lblEtiquetaTelefono.Name = "lblEtiquetaTelefono";
            this.lblEtiquetaTelefono.Size = new System.Drawing.Size(61, 16);
            this.lblEtiquetaTelefono.TabIndex = 15;
            this.lblEtiquetaTelefono.Tag = "RegistrarHuesped_lblTelefono";
            this.lblEtiquetaTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtTelefono.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtTelefono.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtTelefono.BorderWidth = 2;
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTelefono.IconChar = FontAwesome.Sharp.IconChar.Phone;
            this.txtTelefono.IconColor = System.Drawing.Color.Goldenrod;
            this.txtTelefono.IconColorRight = System.Drawing.Color.DimGray;
            this.txtTelefono.IconPadding = 4;
            this.txtTelefono.IconSize = 20;
            this.txtTelefono.Location = new System.Drawing.Point(23, 365);
            this.txtTelefono.MaxLength = 30;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtTelefono.PlaceholderText = "Teléfono de contacto";
            this.txtTelefono.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTelefono.Size = new System.Drawing.Size(320, 49);
            this.txtTelefono.TabIndex = 16;
            // 
            // lblEtiquetaFechaNacimiento
            // 
            this.lblEtiquetaFechaNacimiento.AutoSize = true;
            this.lblEtiquetaFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaFechaNacimiento.Location = new System.Drawing.Point(366, 260);
            this.lblEtiquetaFechaNacimiento.Name = "lblEtiquetaFechaNacimiento";
            this.lblEtiquetaFechaNacimiento.Size = new System.Drawing.Size(135, 16);
            this.lblEtiquetaFechaNacimiento.TabIndex = 13;
            this.lblEtiquetaFechaNacimiento.Tag = "RegistrarHuesped_lblFechaNacimiento";
            this.lblEtiquetaFechaNacimiento.Text = "Fecha de Nacimiento";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaNacimiento.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaNacimiento.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaNacimiento.BorderWidth = 2;
            this.dtpFechaNacimiento.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaNacimiento.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaNacimiento.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.dtpFechaNacimiento.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpFechaNacimiento.CalendarTrailingForeColor = System.Drawing.Color.Silver;
            this.dtpFechaNacimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaNacimiento.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaNacimiento.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpFechaNacimiento.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaNacimiento.IconSize = 19;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(366, 279);
            this.dtpFechaNacimiento.MaxDate = new System.DateTime(2026, 9, 13, 0, 0, 0, 0);
            this.dtpFechaNacimiento.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(320, 49);
            this.dtpFechaNacimiento.TabIndex = 14;
            this.dtpFechaNacimiento.TimeValue = System.TimeSpan.Parse("00:00:00");
            this.dtpFechaNacimiento.Value = new System.DateTime(2026, 9, 13, 0, 0, 0, 0);
            // 
            // lblEtiquetaNacionalidad
            // 
            this.lblEtiquetaNacionalidad.AutoSize = true;
            this.lblEtiquetaNacionalidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaNacionalidad.Location = new System.Drawing.Point(23, 260);
            this.lblEtiquetaNacionalidad.Name = "lblEtiquetaNacionalidad";
            this.lblEtiquetaNacionalidad.Size = new System.Drawing.Size(88, 16);
            this.lblEtiquetaNacionalidad.TabIndex = 11;
            this.lblEtiquetaNacionalidad.Tag = "RegistrarHuesped_lblNacionalidad";
            this.lblEtiquetaNacionalidad.Text = "Nacionalidad";
            // 
            // txtNacionalidad
            // 
            this.txtNacionalidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtNacionalidad.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNacionalidad.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNacionalidad.BorderWidth = 2;
            this.txtNacionalidad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNacionalidad.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNacionalidad.IconChar = FontAwesome.Sharp.IconChar.Globe;
            this.txtNacionalidad.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNacionalidad.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNacionalidad.IconPadding = 4;
            this.txtNacionalidad.IconSize = 22;
            this.txtNacionalidad.Location = new System.Drawing.Point(23, 279);
            this.txtNacionalidad.MaxLength = 50;
            this.txtNacionalidad.Name = "txtNacionalidad";
            this.txtNacionalidad.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNacionalidad.PlaceholderText = "Nacionalidad";
            this.txtNacionalidad.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNacionalidad.Size = new System.Drawing.Size(320, 49);
            this.txtNacionalidad.TabIndex = 12;
            // 
            // lblEtiquetaDocumento
            // 
            this.lblEtiquetaDocumento.AutoSize = true;
            this.lblEtiquetaDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaDocumento.Location = new System.Drawing.Point(366, 175);
            this.lblEtiquetaDocumento.Name = "lblEtiquetaDocumento";
            this.lblEtiquetaDocumento.Size = new System.Drawing.Size(93, 16);
            this.lblEtiquetaDocumento.TabIndex = 9;
            this.lblEtiquetaDocumento.Tag = "RegistrarHuesped_lblNroDocumento";
            this.lblEtiquetaDocumento.Text = "N° Documento";
            // 
            // txtDocumento
            // 
            this.txtDocumento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtDocumento.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtDocumento.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtDocumento.BorderWidth = 2;
            this.txtDocumento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocumento.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDocumento.IconChar = FontAwesome.Sharp.IconChar.Hashtag;
            this.txtDocumento.IconColor = System.Drawing.Color.Goldenrod;
            this.txtDocumento.IconColorRight = System.Drawing.Color.DimGray;
            this.txtDocumento.IconPadding = 4;
            this.txtDocumento.IconSize = 22;
            this.txtDocumento.Location = new System.Drawing.Point(366, 194);
            this.txtDocumento.MaxLength = 20;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtDocumento.PlaceholderText = "N° Documento";
            this.txtDocumento.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDocumento.Size = new System.Drawing.Size(320, 49);
            this.txtDocumento.TabIndex = 10;
            // 
            // lblEtiquetaTipoDocumento
            // 
            this.lblEtiquetaTipoDocumento.AutoSize = true;
            this.lblEtiquetaTipoDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaTipoDocumento.Location = new System.Drawing.Point(23, 175);
            this.lblEtiquetaTipoDocumento.Name = "lblEtiquetaTipoDocumento";
            this.lblEtiquetaTipoDocumento.Size = new System.Drawing.Size(126, 16);
            this.lblEtiquetaTipoDocumento.TabIndex = 7;
            this.lblEtiquetaTipoDocumento.Tag = "RegistrarHuesped_lblTipoDocumento";
            this.lblEtiquetaTipoDocumento.Text = "Tipo de Documento";
            // 
            // cmbTipoDocumento
            // 
            this.cmbTipoDocumento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.cmbTipoDocumento.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.BorderWidth = 2;
            this.cmbTipoDocumento.Cursor = System.Windows.Forms.Cursors.PanSouth;
            this.cmbTipoDocumento.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.cmbTipoDocumento.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocumento.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(100)))));
            this.cmbTipoDocumento.DropDownHighlightForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocumento.DropDownItemHeight = 28;
            this.cmbTipoDocumento.DropDownMaxHeight = 140;
            this.cmbTipoDocumento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoDocumento.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocumento.IconChar = FontAwesome.Sharp.IconChar.IdCard;
            this.cmbTipoDocumento.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.IconSize = 24;
            this.cmbTipoDocumento.Location = new System.Drawing.Point(23, 194);
            this.cmbTipoDocumento.Name = "cmbTipoDocumento";
            this.cmbTipoDocumento.SelectedItem = null;
            this.cmbTipoDocumento.SelectedValue = null;
            this.cmbTipoDocumento.Size = new System.Drawing.Size(320, 49);
            this.cmbTipoDocumento.TabIndex = 8;
            // 
            // lblEtiquetaApellido
            // 
            this.lblEtiquetaApellido.AutoSize = true;
            this.lblEtiquetaApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaApellido.Location = new System.Drawing.Point(366, 90);
            this.lblEtiquetaApellido.Name = "lblEtiquetaApellido";
            this.lblEtiquetaApellido.Size = new System.Drawing.Size(57, 16);
            this.lblEtiquetaApellido.TabIndex = 5;
            this.lblEtiquetaApellido.Tag = "RegistrarHuesped_lblApellido";
            this.lblEtiquetaApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            this.txtApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtApellido.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtApellido.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtApellido.BorderWidth = 2;
            this.txtApellido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellido.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtApellido.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtApellido.IconColor = System.Drawing.Color.Goldenrod;
            this.txtApellido.IconColorRight = System.Drawing.Color.DimGray;
            this.txtApellido.IconPadding = 4;
            this.txtApellido.IconSize = 22;
            this.txtApellido.Location = new System.Drawing.Point(366, 109);
            this.txtApellido.MaxLength = 60;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtApellido.PlaceholderText = "Apellido del huésped";
            this.txtApellido.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtApellido.Size = new System.Drawing.Size(320, 49);
            this.txtApellido.TabIndex = 6;
            // 
            // lblEtiquetaNombre
            // 
            this.lblEtiquetaNombre.AutoSize = true;
            this.lblEtiquetaNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaNombre.Location = new System.Drawing.Point(23, 90);
            this.lblEtiquetaNombre.Name = "lblEtiquetaNombre";
            this.lblEtiquetaNombre.Size = new System.Drawing.Size(56, 16);
            this.lblEtiquetaNombre.TabIndex = 3;
            this.lblEtiquetaNombre.Tag = "RegistrarHuesped_lblNombre";
            this.lblEtiquetaNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtNombre.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderWidth = 2;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombre.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtNombre.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombre.IconPadding = 4;
            this.txtNombre.IconSize = 22;
            this.txtNombre.Location = new System.Drawing.Point(23, 109);
            this.txtNombre.MaxLength = 60;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombre.PlaceholderText = "Nombre del huésped";
            this.txtNombre.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombre.Size = new System.Drawing.Size(320, 49);
            this.txtNombre.TabIndex = 4;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlHeader.Controls.Add(this.btnCerrar);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.iconTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(709, 75);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCerrar.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.btnCerrar.IconColor = System.Drawing.Color.Gold;
            this.btnCerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCerrar.IconSize = 28;
            this.btnCerrar.Location = new System.Drawing.Point(651, 17);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(41, 38);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Location = new System.Drawing.Point(75, 23);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(205, 30);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Tag = "RegistrarHuesped_lblTitulo";
            this.lblTitulo.Text = "Registrar Huésped";
            // 
            // iconTitulo
            // 
            this.iconTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.iconTitulo.ForeColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.iconTitulo.IconColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconTitulo.IconSize = 41;
            this.iconTitulo.Location = new System.Drawing.Point(21, 17);
            this.iconTitulo.Name = "iconTitulo";
            this.iconTitulo.Size = new System.Drawing.Size(43, 41);
            this.iconTitulo.TabIndex = 0;
            this.iconTitulo.TabStop = false;
            // 
            // FormRegistrarHuesped_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(715, 540);
            this.Controls.Add(this.pnlBorde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRegistrarHuesped_68SA";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar Huésped";
            this.pnlBorde.ResumeLayout(false);
            this.pnlBorde.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTitulo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBorde;
        private System.Windows.Forms.Panel pnlHeader;
        private FontAwesome.Sharp.IconPictureBox iconTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnCerrar;
        private System.Windows.Forms.Label lblEtiquetaNombre;
        private CustomControls.IconPlaceholderTextBox txtNombre;
        private System.Windows.Forms.Label lblEtiquetaApellido;
        private CustomControls.IconPlaceholderTextBox txtApellido;
        private System.Windows.Forms.Label lblEtiquetaTipoDocumento;
        private CustomControls.IconComboBox cmbTipoDocumento;
        private System.Windows.Forms.Label lblEtiquetaDocumento;
        private CustomControls.IconPlaceholderTextBox txtDocumento;
        private System.Windows.Forms.Label lblEtiquetaNacionalidad;
        private CustomControls.IconPlaceholderTextBox txtNacionalidad;
        private System.Windows.Forms.Label lblEtiquetaFechaNacimiento;
        private CustomControls.IconDateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblEtiquetaTelefono;
        private CustomControls.IconPlaceholderTextBox txtTelefono;
        private System.Windows.Forms.Label lblEtiquetaEmail;
        private CustomControls.IconPlaceholderTextBox txtEmail;
        private System.Windows.Forms.Panel pnlLinea1;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnRegistrar;
    }
}