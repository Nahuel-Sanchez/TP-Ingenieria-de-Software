namespace GUI_08YS.Recepcionista
{
    partial class FormBitacoraCambiosHabitacion_68SA
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
            this.pnlPie = new System.Windows.Forms.Panel();
            this.lblSeleccion = new System.Windows.Forms.Label();
            this.btnSalir = new FontAwesome.Sharp.IconButton();
            this.pnlFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCampoCodigo = new System.Windows.Forms.Panel();
            this.txtCodigo = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaCodigo = new System.Windows.Forms.Label();
            this.pnlCampoNombre = new System.Windows.Forms.Panel();
            this.txtNombre = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaNombre = new System.Windows.Forms.Label();
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
            this.pnlPie.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.pnlCampoCodigo.SuspendLayout();
            this.pnlCampoNombre.SuspendLayout();
            this.pnlCampoDesde.SuspendLayout();
            this.pnlCampoHasta.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.Controls.Add(this.dgvBitacora);
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
            // pnlPie
            // 
            this.pnlPie.Controls.Add(this.lblSeleccion);
            this.pnlPie.Controls.Add(this.btnSalir);
            this.pnlPie.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPie.Size = new System.Drawing.Size(1100, 64);
            this.pnlPie.Name = "pnlPie";
            // 
            // lblSeleccion
            // 
            this.lblSeleccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSeleccion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSeleccion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeleccion.Padding = new System.Windows.Forms.Padding(24, 0, 10, 0);
            this.lblSeleccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSeleccion.Tag = "BitacoraHab_lblSeleccion";
            this.lblSeleccion.Text = "Los registros los genera el sistema: la bitácora es de solo lectura.";
            this.lblSeleccion.Name = "lblSeleccion";
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
            this.btnSalir.Tag = "BitacoraHab_btnSalir";
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
            this.pnlFiltros.Controls.Add(this.pnlCampoDesde);
            this.pnlFiltros.Controls.Add(this.pnlCampoHasta);
            this.pnlFiltros.Controls.Add(this.chkSoloActivos);
            this.pnlFiltros.Controls.Add(this.btnAplicar);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.AutoSize = true;
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(24, 8, 12, 0);
            this.pnlFiltros.Size = new System.Drawing.Size(1100, 70);
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
            this.lblEtiquetaCodigo.Tag = "BitacoraHab_lblCodigo";
            this.lblEtiquetaCodigo.Text = "Código de reserva";
            this.lblEtiquetaCodigo.Name = "lblEtiquetaCodigo";
            // 
            // pnlCampoNombre
            // 
            this.pnlCampoNombre.Controls.Add(this.txtNombre);
            this.pnlCampoNombre.Controls.Add(this.lblEtiquetaNombre);
            this.pnlCampoNombre.Size = new System.Drawing.Size(260, 72);
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
            this.txtNombre.Size = new System.Drawing.Size(260, 44);
            this.txtNombre.Name = "txtNombre";
            // 
            // lblEtiquetaNombre
            // 
            this.lblEtiquetaNombre.Location = new System.Drawing.Point(0, 2);
            this.lblEtiquetaNombre.AutoSize = true;
            this.lblEtiquetaNombre.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaNombre.Tag = "BitacoraHab_lblNombre";
            this.lblEtiquetaNombre.Text = "Nombre (huésped o habitación)";
            this.lblEtiquetaNombre.Name = "lblEtiquetaNombre";
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
            this.lblEtiquetaDesde.Tag = "BitacoraHab_lblFechaDesde";
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
            this.lblEtiquetaHasta.Tag = "BitacoraHab_lblFechaHasta";
            this.lblEtiquetaHasta.Text = "Fecha fin";
            this.lblEtiquetaHasta.Name = "lblEtiquetaHasta";
            // 
            // chkSoloActivos
            // 
            this.chkSoloActivos.AutoSize = true;
            this.chkSoloActivos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSoloActivos.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.chkSoloActivos.Margin = new System.Windows.Forms.Padding(4, 36, 16, 8);
            this.chkSoloActivos.Tag = "BitacoraHab_chkSoloActivos";
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
            this.btnAplicar.Tag = "BitacoraHab_btnAplicar";
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
            this.pnlEncabezado.Size = new System.Drawing.Size(1100, 84);
            this.pnlEncabezado.Name = "pnlEncabezado";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.Tag = "BitacoraHab_lblSubtitulo";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSubtitulo.Text = "Cada habitación que tuvo cada reserva. El registro activo es la habitación actual; no se borra ningún registro.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Size = new System.Drawing.Size(1052, 36);
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Tag = "BitacoraHab_titulo";
            this.lblTitulo.Text = "Bitácora de cambios de habitación";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Name = "lblTitulo";
            // 
            // FormBitacoraCambiosHabitacion_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text = "Bitácora de cambios de habitación";
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.pnlPie);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FormBitacoraCambiosHabitacion_68SA";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlCampoHasta.ResumeLayout(false);
            this.pnlCampoHasta.PerformLayout();
            this.pnlCampoDesde.ResumeLayout(false);
            this.pnlCampoDesde.PerformLayout();
            this.pnlCampoNombre.ResumeLayout(false);
            this.pnlCampoNombre.PerformLayout();
            this.pnlCampoCodigo.ResumeLayout(false);
            this.pnlCampoCodigo.PerformLayout();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlPie.ResumeLayout(false);
            this.pnlGrilla.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.Windows.Forms.Panel pnlPie;
        private System.Windows.Forms.Label lblSeleccion;
        private FontAwesome.Sharp.IconButton btnSalir;
        private System.Windows.Forms.FlowLayoutPanel pnlFiltros;
        private System.Windows.Forms.Panel pnlCampoCodigo;
        private CustomControls.IconPlaceholderTextBox txtCodigo;
        private System.Windows.Forms.Label lblEtiquetaCodigo;
        private System.Windows.Forms.Panel pnlCampoNombre;
        private CustomControls.IconPlaceholderTextBox txtNombre;
        private System.Windows.Forms.Label lblEtiquetaNombre;
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
