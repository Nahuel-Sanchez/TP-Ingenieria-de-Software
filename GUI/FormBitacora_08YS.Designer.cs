namespace GUI
{
    partial class FormBitacora_08YS
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblCriticidad = new System.Windows.Forms.Label();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblModulo = new System.Windows.Forms.Label();
            this.lblFechaIni = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.comboBoxModulo = new System.Windows.Forms.ComboBox();
            this.comboBoxEvento = new System.Windows.Forms.ComboBox();
            this.comboBoxCriticidad = new System.Windows.Forms.ComboBox();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvEventos = new System.Windows.Forms.DataGridView();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.btnFiltrar = new FontAwesome.Sharp.IconButton();
            this.btnExportar = new FontAwesome.Sharp.IconButton();
            this.dtpHasta = new CustomControls.IconDateTimePicker();
            this.dtpDesde = new CustomControls.IconDateTimePicker();
            this.txtUsername = new CustomControls.IconPlaceholderTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCriticidad
            // 
            this.lblCriticidad.AutoSize = true;
            this.lblCriticidad.BackColor = System.Drawing.Color.Transparent;
            this.lblCriticidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriticidad.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblCriticidad.Location = new System.Drawing.Point(901, 729);
            this.lblCriticidad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCriticidad.Name = "lblCriticidad";
            this.lblCriticidad.Size = new System.Drawing.Size(85, 20);
            this.lblCriticidad.TabIndex = 43;
            this.lblCriticidad.Text = "Criticidad:";
            this.lblCriticidad.Click += new System.EventHandler(this.lblCriticidad_Click);
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFin.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblFechaFin.Location = new System.Drawing.Point(901, 663);
            this.lblFechaFin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(110, 20);
            this.lblFechaFin.TabIndex = 42;
            this.lblFechaFin.Text = "Fecha Hasta:";
            this.lblFechaFin.Click += new System.EventHandler(this.lblFechaFin_Click);
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.BackColor = System.Drawing.Color.Transparent;
            this.lblEvento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEvento.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblEvento.Location = new System.Drawing.Point(405, 730);
            this.lblEvento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(65, 20);
            this.lblEvento.TabIndex = 41;
            this.lblEvento.Text = "Evento:";
            this.lblEvento.Click += new System.EventHandler(this.lblEvento_Click);
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = true;
            this.lblModulo.BackColor = System.Drawing.Color.Transparent;
            this.lblModulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblModulo.Location = new System.Drawing.Point(68, 729);
            this.lblModulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(68, 20);
            this.lblModulo.TabIndex = 40;
            this.lblModulo.Text = "Modulo:";
            this.lblModulo.Click += new System.EventHandler(this.lblModulo_Click);
            // 
            // lblFechaIni
            // 
            this.lblFechaIni.AutoSize = true;
            this.lblFechaIni.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaIni.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblFechaIni.Location = new System.Drawing.Point(405, 665);
            this.lblFechaIni.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaIni.Name = "lblFechaIni";
            this.lblFechaIni.Size = new System.Drawing.Size(114, 20);
            this.lblFechaIni.TabIndex = 39;
            this.lblFechaIni.Text = "Fecha Desde:";
            this.lblFechaIni.Click += new System.EventHandler(this.lblFechaIni_Click);
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.BackColor = System.Drawing.Color.Transparent;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblLogin.Location = new System.Drawing.Point(68, 663);
            this.lblLogin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(91, 20);
            this.lblLogin.TabIndex = 38;
            this.lblLogin.Text = "Username:";
            this.lblLogin.Click += new System.EventHandler(this.lblLogin_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.LightYellow;
            this.lblNombre.Location = new System.Drawing.Point(237, 607);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(73, 20);
            this.lblNombre.TabIndex = 36;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.Click += new System.EventHandler(this.lblNombre_Click);
            // 
            // comboBoxModulo
            // 
            this.comboBoxModulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.comboBoxModulo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxModulo.FormattingEnabled = true;
            this.comboBoxModulo.Location = new System.Drawing.Point(184, 729);
            this.comboBoxModulo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxModulo.Name = "comboBoxModulo";
            this.comboBoxModulo.Size = new System.Drawing.Size(172, 24);
            this.comboBoxModulo.TabIndex = 50;
            this.comboBoxModulo.SelectedIndexChanged += new System.EventHandler(this.comboBoxModulo_SelectedIndexChanged);
            // 
            // comboBoxEvento
            // 
            this.comboBoxEvento.FormattingEnabled = true;
            this.comboBoxEvento.Location = new System.Drawing.Point(603, 730);
            this.comboBoxEvento.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxEvento.Name = "comboBoxEvento";
            this.comboBoxEvento.Size = new System.Drawing.Size(256, 24);
            this.comboBoxEvento.TabIndex = 51;
            this.comboBoxEvento.SelectedIndexChanged += new System.EventHandler(this.comboBoxEvento_SelectedIndexChanged);
            // 
            // comboBoxCriticidad
            // 
            this.comboBoxCriticidad.FormattingEnabled = true;
            this.comboBoxCriticidad.Location = new System.Drawing.Point(1125, 726);
            this.comboBoxCriticidad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxCriticidad.Name = "comboBoxCriticidad";
            this.comboBoxCriticidad.Size = new System.Drawing.Size(261, 24);
            this.comboBoxCriticidad.TabIndex = 52;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox1.ForeColor = System.Drawing.Color.Goldenrod;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.FileInvoice;
            this.iconPictureBox1.IconColor = System.Drawing.Color.Goldenrod;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 111;
            this.iconPictureBox1.Location = new System.Drawing.Point(48, 27);
            this.iconPictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(111, 115);
            this.iconPictureBox1.TabIndex = 53;
            this.iconPictureBox1.TabStop = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Location = new System.Drawing.Point(177, 44);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(359, 42);
            this.lblTitulo.TabIndex = 54;
            this.lblTitulo.Text = "Auditoria de eventos";
            // 
            // dgvEventos
            // 
            this.dgvEventos.AllowUserToAddRows = false;
            this.dgvEventos.AllowUserToDeleteRows = false;
            this.dgvEventos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEventos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.dgvEventos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEventos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEventos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEventos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEventos.EnableHeadersVisualStyles = false;
            this.dgvEventos.GridColor = System.Drawing.Color.Goldenrod;
            this.dgvEventos.Location = new System.Drawing.Point(13, 165);
            this.dgvEventos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvEventos.MultiSelect = false;
            this.dgvEventos.Name = "dgvEventos";
            this.dgvEventos.ReadOnly = true;
            this.dgvEventos.RowHeadersVisible = false;
            this.dgvEventos.RowHeadersWidth = 51;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(100)))));
            this.dgvEventos.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvEventos.RowTemplate.Height = 30;
            this.dgvEventos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEventos.Size = new System.Drawing.Size(1427, 420);
            this.dgvEventos.TabIndex = 55;
            this.dgvEventos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEventos_CellFormatting);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.Gold;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 30;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(748, 817);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.btnLimpiar.Size = new System.Drawing.Size(165, 60);
            this.btnLimpiar.TabIndex = 57;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.Transparent;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnFiltrar.IconChar = FontAwesome.Sharp.IconChar.Filter;
            this.btnFiltrar.IconColor = System.Drawing.Color.Gold;
            this.btnFiltrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFiltrar.IconSize = 30;
            this.btnFiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.Location = new System.Drawing.Point(995, 817);
            this.btnFiltrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.btnFiltrar.Size = new System.Drawing.Size(165, 60);
            this.btnFiltrar.TabIndex = 58;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.Transparent;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnExportar.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            this.btnExportar.IconColor = System.Drawing.Color.Gold;
            this.btnExportar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExportar.IconSize = 30;
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.Location = new System.Drawing.Point(1221, 817);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.btnExportar.Size = new System.Drawing.Size(165, 60);
            this.btnExportar.TabIndex = 59;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpHasta.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpHasta.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpHasta.BorderWidth = 2;
            this.dtpHasta.CalendarBackColor = System.Drawing.SystemColors.Window;
            this.dtpHasta.CalendarForeColor = System.Drawing.SystemColors.WindowText;
            this.dtpHasta.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.dtpHasta.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpHasta.CalendarTrailingForeColor = System.Drawing.Color.Silver;
            this.dtpHasta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpHasta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpHasta.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpHasta.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpHasta.IconSize = 19;
            this.dtpHasta.Location = new System.Drawing.Point(1125, 657);
            this.dtpHasta.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpHasta.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(261, 35);
            this.dtpHasta.TabIndex = 62;
            this.dtpHasta.Value = new System.DateTime(2026, 5, 23, 15, 11, 1, 374);
            // 
            // dtpDesde
            // 
            this.dtpDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpDesde.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpDesde.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpDesde.BorderWidth = 2;
            this.dtpDesde.CalendarBackColor = System.Drawing.SystemColors.Window;
            this.dtpDesde.CalendarForeColor = System.Drawing.SystemColors.WindowText;
            this.dtpDesde.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.dtpDesde.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpDesde.CalendarTrailingForeColor = System.Drawing.Color.Silver;
            this.dtpDesde.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpDesde.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpDesde.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpDesde.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpDesde.IconSize = 19;
            this.dtpDesde.Location = new System.Drawing.Point(603, 657);
            this.dtpDesde.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDesde.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(256, 35);
            this.dtpDesde.TabIndex = 61;
            this.dtpDesde.Value = new System.DateTime(2026, 5, 23, 15, 11, 1, 374);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.txtUsername.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtUsername.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtUsername.BorderWidth = 2;
            this.txtUsername.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtUsername.IconAlignment = CustomControls.IconTextBoxAlignment.Right;
            this.txtUsername.IconColor = System.Drawing.Color.Goldenrod;
            this.txtUsername.IconSize = 25;
            this.txtUsername.Location = new System.Drawing.Point(184, 657);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtUsername.PlaceholderText = "Ingrese un username";
            this.txtUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUsername.Size = new System.Drawing.Size(203, 35);
            this.txtUsername.TabIndex = 60;
            // 
            // FormBitacora_08YS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::GUI_08YS.Properties.Resources.ChatGPT_Image_19_may_2026__22_23_23;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1452, 914);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvEventos);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.comboBoxCriticidad);
            this.Controls.Add(this.comboBoxEvento);
            this.Controls.Add(this.comboBoxModulo);
            this.Controls.Add(this.lblCriticidad);
            this.Controls.Add(this.lblFechaFin);
            this.Controls.Add(this.lblEvento);
            this.Controls.Add(this.lblModulo);
            this.Controls.Add(this.lblFechaIni);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblNombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormBitacora_08YS";
            this.Text = " ";
            this.Load += new System.EventHandler(this.FormBitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblCriticidad;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.Label lblFechaIni;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.ComboBox comboBoxModulo;
        private System.Windows.Forms.ComboBox comboBoxEvento;
        private System.Windows.Forms.ComboBox comboBoxCriticidad;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvEventos;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private FontAwesome.Sharp.IconButton btnExportar;
        private CustomControls.IconPlaceholderTextBox txtUsername;
        private CustomControls.IconDateTimePicker dtpDesde;
        private CustomControls.IconDateTimePicker dtpHasta;
    }
}