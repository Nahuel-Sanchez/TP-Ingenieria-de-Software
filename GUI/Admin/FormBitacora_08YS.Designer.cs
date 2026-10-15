namespace GUI
{
    partial class FormBitacora_08YS
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
            this.dgvEventos = new System.Windows.Forms.DataGridView();
            this.pnlPie = new System.Windows.Forms.Panel();
            this.lblPie = new System.Windows.Forms.Label();
            this.btnExportar = new FontAwesome.Sharp.IconButton();
            this.pnlFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCampoUsuario = new System.Windows.Forms.Panel();
            this.txtUsername = new CustomControls.IconPlaceholderTextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.pnlCampoAfectado = new System.Windows.Forms.Panel();
            this.txtTargetUsername = new CustomControls.IconPlaceholderTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCampoModulo = new System.Windows.Forms.Panel();
            this.comboBoxModulo = new CustomControls.IconComboBox();
            this.lblModulo = new System.Windows.Forms.Label();
            this.pnlCampoEvento = new System.Windows.Forms.Panel();
            this.comboBoxEvento = new CustomControls.IconComboBox();
            this.lblEvento = new System.Windows.Forms.Label();
            this.pnlCampoCriticidad = new System.Windows.Forms.Panel();
            this.comboBoxCriticidad = new CustomControls.IconComboBox();
            this.lblCriticidad = new System.Windows.Forms.Label();
            this.pnlCampoDesde = new System.Windows.Forms.Panel();
            this.dtpDesde = new CustomControls.IconDateTimePicker();
            this.lblFechaIni = new System.Windows.Forms.Label();
            this.pnlCampoHasta = new System.Windows.Forms.Panel();
            this.dtpHasta = new CustomControls.IconDateTimePicker();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.btnFiltrar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.pnlGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.pnlPie.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.pnlCampoUsuario.SuspendLayout();
            this.pnlCampoAfectado.SuspendLayout();
            this.pnlCampoModulo.SuspendLayout();
            this.pnlCampoEvento.SuspendLayout();
            this.pnlCampoCriticidad.SuspendLayout();
            this.pnlCampoDesde.SuspendLayout();
            this.pnlCampoHasta.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.Controls.Add(this.dgvEventos);
            this.pnlGrilla.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(24, 0, 24, 12);
            this.pnlGrilla.Name = "pnlGrilla";
            // 
            // dgvEventos
            // 
            this.dgvEventos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEventos_CellFormatting);
            this.dgvEventos.BackgroundColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.dgvEventos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEventos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEventos.Name = "dgvEventos";
            // 
            // pnlPie
            // 
            this.pnlPie.Controls.Add(this.lblPie);
            this.pnlPie.Controls.Add(this.btnExportar);
            this.pnlPie.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPie.Size = new System.Drawing.Size(1280, 64);
            this.pnlPie.Name = "pnlPie";
            // 
            // lblPie
            // 
            this.lblPie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPie.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblPie.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPie.Padding = new System.Windows.Forms.Padding(24, 0, 10, 0);
            this.lblPie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPie.Tag = "Bitacora_lblPie";
            this.lblPie.UseMnemonic = false;
            this.lblPie.AutoEllipsis = true;
            this.lblPie.Text = "Exportar imprime los eventos de la grilla (podés elegir una impresora de PDF).";
            this.lblPie.Name = "lblPie";
            // 
            // btnExportar
            // 
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnExportar.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            this.btnExportar.IconColor = System.Drawing.Color.Gold;
            this.btnExportar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExportar.IconSize = 22;
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnExportar.Size = new System.Drawing.Size(170, 64);
            this.btnExportar.Tag = "Bitacora_btnExportar";
            this.btnExportar.Text = "Exportar";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnExportar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExportar.Name = "btnExportar";
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.Controls.Add(this.pnlCampoUsuario);
            this.pnlFiltros.Controls.Add(this.pnlCampoAfectado);
            this.pnlFiltros.Controls.Add(this.pnlCampoModulo);
            this.pnlFiltros.Controls.Add(this.pnlCampoEvento);
            this.pnlFiltros.Controls.Add(this.pnlCampoCriticidad);
            this.pnlFiltros.Controls.Add(this.pnlCampoDesde);
            this.pnlFiltros.Controls.Add(this.pnlCampoHasta);
            this.pnlFiltros.Controls.Add(this.btnFiltrar);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.AutoSize = true;
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(24, 8, 12, 4);
            this.pnlFiltros.Size = new System.Drawing.Size(1280, 84);
            this.pnlFiltros.Name = "pnlFiltros";
            // 
            // pnlCampoUsuario
            // 
            this.pnlCampoUsuario.Controls.Add(this.txtUsername);
            this.pnlCampoUsuario.Controls.Add(this.lblLogin);
            this.pnlCampoUsuario.Size = new System.Drawing.Size(210, 72);
            this.pnlCampoUsuario.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoUsuario.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoUsuario.Name = "pnlCampoUsuario";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(0, 24);
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtUsername.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtUsername.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtUsername.BorderWidth = 2;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtUsername.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtUsername.IconColor = System.Drawing.Color.Goldenrod;
            this.txtUsername.IconColorRight = System.Drawing.Color.DimGray;
            this.txtUsername.IconPadding = 4;
            this.txtUsername.IconSize = 20;
            this.txtUsername.MaxLength = 100;
            this.txtUsername.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtUsername.PlaceholderText = "Username";
            this.txtUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUsername.Size = new System.Drawing.Size(210, 44);
            this.txtUsername.Name = "txtUsername";
            // 
            // lblLogin
            // 
            this.lblLogin.Location = new System.Drawing.Point(0, 2);
            this.lblLogin.AutoSize = true;
            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.Tag = "Bitacora_lblUsuario";
            this.lblLogin.Text = "Usuario";
            this.lblLogin.Name = "lblLogin";
            // 
            // pnlCampoAfectado
            // 
            this.pnlCampoAfectado.Controls.Add(this.txtTargetUsername);
            this.pnlCampoAfectado.Controls.Add(this.label2);
            this.pnlCampoAfectado.Size = new System.Drawing.Size(210, 72);
            this.pnlCampoAfectado.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoAfectado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoAfectado.Name = "pnlCampoAfectado";
            // 
            // txtTargetUsername
            // 
            this.txtTargetUsername.Location = new System.Drawing.Point(0, 24);
            this.txtTargetUsername.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtTargetUsername.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtTargetUsername.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtTargetUsername.BorderWidth = 2;
            this.txtTargetUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTargetUsername.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTargetUsername.IconChar = FontAwesome.Sharp.IconChar.UserTag;
            this.txtTargetUsername.IconColor = System.Drawing.Color.Goldenrod;
            this.txtTargetUsername.IconColorRight = System.Drawing.Color.DimGray;
            this.txtTargetUsername.IconPadding = 4;
            this.txtTargetUsername.IconSize = 20;
            this.txtTargetUsername.MaxLength = 100;
            this.txtTargetUsername.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtTargetUsername.PlaceholderText = "Username";
            this.txtTargetUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTargetUsername.Size = new System.Drawing.Size(210, 44);
            this.txtTargetUsername.Name = "txtTargetUsername";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 2);
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Tag = "Bitacora_lblUsuarioAfectado";
            this.label2.Text = "Usuario afectado";
            this.label2.Name = "label2";
            // 
            // pnlCampoModulo
            // 
            this.pnlCampoModulo.Controls.Add(this.comboBoxModulo);
            this.pnlCampoModulo.Controls.Add(this.lblModulo);
            this.pnlCampoModulo.Size = new System.Drawing.Size(200, 72);
            this.pnlCampoModulo.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoModulo.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoModulo.Name = "pnlCampoModulo";
            // 
            // comboBoxModulo
            // 
            this.comboBoxModulo.SelectedIndexChanged += new System.EventHandler(this.comboBoxModulo_SelectedIndexChanged);
            this.comboBoxModulo.Location = new System.Drawing.Point(0, 24);
            this.comboBoxModulo.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.comboBoxModulo.BorderColor = System.Drawing.Color.Goldenrod;
            this.comboBoxModulo.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.comboBoxModulo.BorderWidth = 2;
            this.comboBoxModulo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxModulo.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.comboBoxModulo.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.comboBoxModulo.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxModulo.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.comboBoxModulo.DropDownHighlightForeColor = System.Drawing.Color.Goldenrod;
            this.comboBoxModulo.DropDownItemHeight = 32;
            this.comboBoxModulo.DropDownMaxHeight = 240;
            this.comboBoxModulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxModulo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxModulo.IconChar = FontAwesome.Sharp.IconChar.LayerGroup;
            this.comboBoxModulo.IconColor = System.Drawing.Color.Goldenrod;
            this.comboBoxModulo.IconSize = 20;
            this.comboBoxModulo.SelectedItem = null;
            this.comboBoxModulo.SelectedValue = null;
            this.comboBoxModulo.Size = new System.Drawing.Size(200, 44);
            this.comboBoxModulo.Name = "comboBoxModulo";
            // 
            // lblModulo
            // 
            this.lblModulo.Location = new System.Drawing.Point(0, 2);
            this.lblModulo.AutoSize = true;
            this.lblModulo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblModulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModulo.Tag = "Bitacora_lblModulo";
            this.lblModulo.Text = "Módulo";
            this.lblModulo.Name = "lblModulo";
            // 
            // pnlCampoEvento
            // 
            this.pnlCampoEvento.Controls.Add(this.comboBoxEvento);
            this.pnlCampoEvento.Controls.Add(this.lblEvento);
            this.pnlCampoEvento.Size = new System.Drawing.Size(260, 72);
            this.pnlCampoEvento.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoEvento.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoEvento.Name = "pnlCampoEvento";
            // 
            // comboBoxEvento
            // 
            this.comboBoxEvento.SelectedIndexChanged += new System.EventHandler(this.comboBoxEvento_SelectedIndexChanged);
            this.comboBoxEvento.Location = new System.Drawing.Point(0, 24);
            this.comboBoxEvento.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.comboBoxEvento.BorderColor = System.Drawing.Color.Goldenrod;
            this.comboBoxEvento.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.comboBoxEvento.BorderWidth = 2;
            this.comboBoxEvento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxEvento.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.comboBoxEvento.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.comboBoxEvento.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxEvento.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.comboBoxEvento.DropDownHighlightForeColor = System.Drawing.Color.Goldenrod;
            this.comboBoxEvento.DropDownItemHeight = 32;
            this.comboBoxEvento.DropDownMaxHeight = 240;
            this.comboBoxEvento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEvento.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxEvento.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.comboBoxEvento.IconColor = System.Drawing.Color.Goldenrod;
            this.comboBoxEvento.IconSize = 20;
            this.comboBoxEvento.SelectedItem = null;
            this.comboBoxEvento.SelectedValue = null;
            this.comboBoxEvento.Size = new System.Drawing.Size(260, 44);
            this.comboBoxEvento.Name = "comboBoxEvento";
            // 
            // lblEvento
            // 
            this.lblEvento.Location = new System.Drawing.Point(0, 2);
            this.lblEvento.AutoSize = true;
            this.lblEvento.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEvento.Tag = "Bitacora_lblEvento";
            this.lblEvento.Text = "Evento";
            this.lblEvento.Name = "lblEvento";
            // 
            // pnlCampoCriticidad
            // 
            this.pnlCampoCriticidad.Controls.Add(this.comboBoxCriticidad);
            this.pnlCampoCriticidad.Controls.Add(this.lblCriticidad);
            this.pnlCampoCriticidad.Size = new System.Drawing.Size(170, 72);
            this.pnlCampoCriticidad.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.pnlCampoCriticidad.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlCampoCriticidad.Name = "pnlCampoCriticidad";
            // 
            // comboBoxCriticidad
            // 
            this.comboBoxCriticidad.SelectedIndexChanged += new System.EventHandler(this.comboBoxCriticidad_SelectedIndexChanged);
            this.comboBoxCriticidad.Location = new System.Drawing.Point(0, 24);
            this.comboBoxCriticidad.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.comboBoxCriticidad.BorderColor = System.Drawing.Color.Goldenrod;
            this.comboBoxCriticidad.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.comboBoxCriticidad.BorderWidth = 2;
            this.comboBoxCriticidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxCriticidad.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.comboBoxCriticidad.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.comboBoxCriticidad.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxCriticidad.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.comboBoxCriticidad.DropDownHighlightForeColor = System.Drawing.Color.Goldenrod;
            this.comboBoxCriticidad.DropDownItemHeight = 32;
            this.comboBoxCriticidad.DropDownMaxHeight = 240;
            this.comboBoxCriticidad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCriticidad.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxCriticidad.IconChar = FontAwesome.Sharp.IconChar.TriangleExclamation;
            this.comboBoxCriticidad.IconColor = System.Drawing.Color.Goldenrod;
            this.comboBoxCriticidad.IconSize = 20;
            this.comboBoxCriticidad.SelectedItem = null;
            this.comboBoxCriticidad.SelectedValue = null;
            this.comboBoxCriticidad.Size = new System.Drawing.Size(170, 44);
            this.comboBoxCriticidad.Name = "comboBoxCriticidad";
            // 
            // lblCriticidad
            // 
            this.lblCriticidad.Location = new System.Drawing.Point(0, 2);
            this.lblCriticidad.AutoSize = true;
            this.lblCriticidad.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblCriticidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriticidad.Tag = "Bitacora_lblCriticidad";
            this.lblCriticidad.Text = "Criticidad";
            this.lblCriticidad.Name = "lblCriticidad";
            // 
            // pnlCampoDesde
            // 
            this.pnlCampoDesde.Controls.Add(this.dtpDesde);
            this.pnlCampoDesde.Controls.Add(this.lblFechaIni);
            this.pnlCampoDesde.Size = new System.Drawing.Size(150, 72);
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
            this.dtpDesde.Size = new System.Drawing.Size(150, 44);
            this.dtpDesde.Value = null;
            this.dtpDesde.Name = "dtpDesde";
            // 
            // lblFechaIni
            // 
            this.lblFechaIni.Location = new System.Drawing.Point(0, 2);
            this.lblFechaIni.AutoSize = true;
            this.lblFechaIni.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblFechaIni.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaIni.Tag = "Bitacora_lblDesde";
            this.lblFechaIni.Text = "Desde";
            this.lblFechaIni.Name = "lblFechaIni";
            // 
            // pnlCampoHasta
            // 
            this.pnlCampoHasta.Controls.Add(this.dtpHasta);
            this.pnlCampoHasta.Controls.Add(this.lblFechaFin);
            this.pnlCampoHasta.Size = new System.Drawing.Size(150, 72);
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
            this.dtpHasta.Size = new System.Drawing.Size(150, 44);
            this.dtpHasta.Value = null;
            this.dtpHasta.Name = "dtpHasta";
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.Location = new System.Drawing.Point(0, 2);
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblFechaFin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFin.Tag = "Bitacora_lblHasta";
            this.lblFechaFin.Text = "Hasta";
            this.lblFechaFin.Name = "lblFechaFin";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnFiltrar.IconChar = FontAwesome.Sharp.IconChar.Filter;
            this.btnFiltrar.IconColor = System.Drawing.Color.Gold;
            this.btnFiltrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFiltrar.IconSize = 22;
            this.btnFiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnFiltrar.Size = new System.Drawing.Size(140, 44);
            this.btnFiltrar.Tag = "Bitacora_btnFiltrar";
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnFiltrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnFiltrar.Margin = new System.Windows.Forms.Padding(0, 24, 12, 8);
            this.btnFiltrar.Name = "btnFiltrar";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.Gold;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 22;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnLimpiar.Size = new System.Drawing.Size(140, 44);
            this.btnLimpiar.Tag = "Bitacora_btnLimpiar";
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
            this.pnlEncabezado.Controls.Add(this.iconPictureBox1);
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 12, 24, 4);
            this.pnlEncabezado.Size = new System.Drawing.Size(1280, 80);
            this.pnlEncabezado.Name = "pnlEncabezado";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.AutoEllipsis = true;
            this.lblSubtitulo.Tag = "Bitacora_lblSubtitulo";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSubtitulo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblSubtitulo.Text = "Todo lo que se registró en el sistema: quién lo hizo, sobre quién, cuándo, en qué módulo y con qué criticidad.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Size = new System.Drawing.Size(1188, 36);
            this.lblTitulo.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Tag = "Bitacora_lblAuditoriaEventos";
            this.lblTitulo.Text = "Auditoría de eventos";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Name = "lblTitulo";
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.iconPictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconPictureBox1.ForeColor = System.Drawing.Color.Goldenrod;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.FileInvoice;
            this.iconPictureBox1.IconColor = System.Drawing.Color.Goldenrod;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 44;
            this.iconPictureBox1.Size = new System.Drawing.Size(44, 64);
            this.iconPictureBox1.TabStop = false;
            this.iconPictureBox1.Name = "iconPictureBox1";
            // 
            // FormBitacora_08YS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Text = "Auditoría de eventos";
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.pnlPie);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FormBitacora_08YS";
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlCampoHasta.ResumeLayout(false);
            this.pnlCampoHasta.PerformLayout();
            this.pnlCampoDesde.ResumeLayout(false);
            this.pnlCampoDesde.PerformLayout();
            this.pnlCampoCriticidad.ResumeLayout(false);
            this.pnlCampoCriticidad.PerformLayout();
            this.pnlCampoEvento.ResumeLayout(false);
            this.pnlCampoEvento.PerformLayout();
            this.pnlCampoModulo.ResumeLayout(false);
            this.pnlCampoModulo.PerformLayout();
            this.pnlCampoAfectado.ResumeLayout(false);
            this.pnlCampoAfectado.PerformLayout();
            this.pnlCampoUsuario.ResumeLayout(false);
            this.pnlCampoUsuario.PerformLayout();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlPie.ResumeLayout(false);
            this.pnlGrilla.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvEventos;
        private System.Windows.Forms.Panel pnlPie;
        private System.Windows.Forms.Label lblPie;
        private FontAwesome.Sharp.IconButton btnExportar;
        private System.Windows.Forms.FlowLayoutPanel pnlFiltros;
        private System.Windows.Forms.Panel pnlCampoUsuario;
        private CustomControls.IconPlaceholderTextBox txtUsername;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Panel pnlCampoAfectado;
        private CustomControls.IconPlaceholderTextBox txtTargetUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlCampoModulo;
        private CustomControls.IconComboBox comboBoxModulo;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.Panel pnlCampoEvento;
        private CustomControls.IconComboBox comboBoxEvento;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Panel pnlCampoCriticidad;
        private CustomControls.IconComboBox comboBoxCriticidad;
        private System.Windows.Forms.Label lblCriticidad;
        private System.Windows.Forms.Panel pnlCampoDesde;
        private CustomControls.IconDateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblFechaIni;
        private System.Windows.Forms.Panel pnlCampoHasta;
        private CustomControls.IconDateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblFechaFin;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
    }
}
