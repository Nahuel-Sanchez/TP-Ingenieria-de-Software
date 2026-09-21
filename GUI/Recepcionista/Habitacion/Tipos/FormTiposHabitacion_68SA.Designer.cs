namespace GUI_08YS.Recepcionista
{
    partial class FormTiposHabitacion_68SA
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
            this.dgvTipos = new System.Windows.Forms.DataGridView();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.txtNombreFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtDescripcionFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtCapacidadFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtTarifaDesdeFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtTarifaHastaFiltro = new CustomControls.IconPlaceholderTextBox();
            this.btnFiltrar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnNuevo = new FontAwesome.Sharp.IconButton();
            this.pnlGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTipos)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlGrilla.Controls.Add(this.dgvTipos);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Location = new System.Drawing.Point(0, 208);
            this.pnlGrilla.Name = "pnlGrilla";
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(24, 0, 24, 16);
            this.pnlGrilla.Size = new System.Drawing.Size(1000, 392);
            this.pnlGrilla.TabIndex = 0;
            // 
            // dgvTipos
            // 
            this.dgvTipos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTipos.Location = new System.Drawing.Point(24, 0);
            this.dgvTipos.Name = "dgvTipos";
            this.dgvTipos.Size = new System.Drawing.Size(952, 376);
            this.dgvTipos.TabIndex = 0;
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.Controls.Add(this.btnFiltrar);
            this.pnlFiltros.Controls.Add(this.txtTarifaHastaFiltro);
            this.pnlFiltros.Controls.Add(this.txtTarifaDesdeFiltro);
            this.pnlFiltros.Controls.Add(this.txtCapacidadFiltro);
            this.pnlFiltros.Controls.Add(this.txtDescripcionFiltro);
            this.pnlFiltros.Controls.Add(this.txtNombreFiltro);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 72);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(1000, 136);
            this.pnlFiltros.TabIndex = 1;
            // 
            // txtNombreFiltro
            // 
            this.txtNombreFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNombreFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.BorderWidth = 2;
            this.txtNombreFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombreFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombreFiltro.IconChar = FontAwesome.Sharp.IconChar.Bed;
            this.txtNombreFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombreFiltro.IconPadding = 4;
            this.txtNombreFiltro.IconSize = 20;
            this.txtNombreFiltro.Location = new System.Drawing.Point(24, 8);
            this.txtNombreFiltro.MaxLength = 50;
            this.txtNombreFiltro.Name = "txtNombreFiltro";
            this.txtNombreFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombreFiltro.PlaceholderText = "Nombre";
            this.txtNombreFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombreFiltro.Size = new System.Drawing.Size(240, 49);
            this.txtNombreFiltro.TabIndex = 0;
            // 
            // txtDescripcionFiltro
            // 
            this.txtDescripcionFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtDescripcionFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtDescripcionFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtDescripcionFiltro.BorderWidth = 2;
            this.txtDescripcionFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescripcionFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescripcionFiltro.IconChar = FontAwesome.Sharp.IconChar.AlignLeft;
            this.txtDescripcionFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtDescripcionFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtDescripcionFiltro.IconPadding = 4;
            this.txtDescripcionFiltro.IconSize = 20;
            this.txtDescripcionFiltro.Location = new System.Drawing.Point(276, 8);
            this.txtDescripcionFiltro.MaxLength = 200;
            this.txtDescripcionFiltro.Name = "txtDescripcionFiltro";
            this.txtDescripcionFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtDescripcionFiltro.PlaceholderText = "Descripción";
            this.txtDescripcionFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescripcionFiltro.Size = new System.Drawing.Size(300, 49);
            this.txtDescripcionFiltro.TabIndex = 1;
            // 
            // txtCapacidadFiltro
            // 
            this.txtCapacidadFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtCapacidadFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtCapacidadFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtCapacidadFiltro.BorderWidth = 2;
            this.txtCapacidadFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCapacidadFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCapacidadFiltro.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.txtCapacidadFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtCapacidadFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtCapacidadFiltro.IconPadding = 4;
            this.txtCapacidadFiltro.IconSize = 20;
            this.txtCapacidadFiltro.Location = new System.Drawing.Point(588, 8);
            this.txtCapacidadFiltro.MaxLength = 3;
            this.txtCapacidadFiltro.Name = "txtCapacidadFiltro";
            this.txtCapacidadFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtCapacidadFiltro.PlaceholderText = "Capacidad";
            this.txtCapacidadFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCapacidadFiltro.Size = new System.Drawing.Size(160, 49);
            this.txtCapacidadFiltro.TabIndex = 2;
            // 
            // txtTarifaDesdeFiltro
            // 
            this.txtTarifaDesdeFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtTarifaDesdeFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtTarifaDesdeFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtTarifaDesdeFiltro.BorderWidth = 2;
            this.txtTarifaDesdeFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTarifaDesdeFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTarifaDesdeFiltro.IconChar = FontAwesome.Sharp.IconChar.DollarSign;
            this.txtTarifaDesdeFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtTarifaDesdeFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtTarifaDesdeFiltro.IconPadding = 4;
            this.txtTarifaDesdeFiltro.IconSize = 20;
            this.txtTarifaDesdeFiltro.Location = new System.Drawing.Point(24, 72);
            this.txtTarifaDesdeFiltro.MaxLength = 12;
            this.txtTarifaDesdeFiltro.Name = "txtTarifaDesdeFiltro";
            this.txtTarifaDesdeFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtTarifaDesdeFiltro.PlaceholderText = "Tarifa desde";
            this.txtTarifaDesdeFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTarifaDesdeFiltro.Size = new System.Drawing.Size(240, 49);
            this.txtTarifaDesdeFiltro.TabIndex = 3;
            // 
            // txtTarifaHastaFiltro
            // 
            this.txtTarifaHastaFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtTarifaHastaFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtTarifaHastaFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtTarifaHastaFiltro.BorderWidth = 2;
            this.txtTarifaHastaFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTarifaHastaFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTarifaHastaFiltro.IconChar = FontAwesome.Sharp.IconChar.DollarSign;
            this.txtTarifaHastaFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtTarifaHastaFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtTarifaHastaFiltro.IconPadding = 4;
            this.txtTarifaHastaFiltro.IconSize = 20;
            this.txtTarifaHastaFiltro.Location = new System.Drawing.Point(276, 72);
            this.txtTarifaHastaFiltro.MaxLength = 12;
            this.txtTarifaHastaFiltro.Name = "txtTarifaHastaFiltro";
            this.txtTarifaHastaFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtTarifaHastaFiltro.PlaceholderText = "Tarifa hasta";
            this.txtTarifaHastaFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTarifaHastaFiltro.Size = new System.Drawing.Size(240, 49);
            this.txtTarifaHastaFiltro.TabIndex = 4;
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
            this.btnFiltrar.Location = new System.Drawing.Point(528, 72);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(140, 49);
            this.btnFiltrar.TabIndex = 5;
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
            this.btnLimpiar.Location = new System.Drawing.Point(680, 72);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(140, 49);
            this.btnLimpiar.TabIndex = 6;
            this.btnLimpiar.Tag = "Crud_btnLimpiar";
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.btnNuevo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 12, 24, 8);
            this.pnlEncabezado.Size = new System.Drawing.Size(1000, 72);
            this.pnlEncabezado.TabIndex = 2;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Location = new System.Drawing.Point(24, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(762, 52);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Tag = "Tipos_titulo";
            this.lblTitulo.Text = "Tipos de habitación";
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
            this.btnNuevo.Location = new System.Drawing.Point(786, 12);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(190, 52);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Tag = "Tipos_btnNuevo";
            this.btnNuevo.Text = "Nuevo tipo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevo.UseVisualStyleBackColor = false;
            // 
            // FormTiposHabitacion_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTiposHabitacion_68SA";
            this.Text = "Tipos de habitación";
            this.pnlGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTipos)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlEncabezado.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvTipos;
        private System.Windows.Forms.Panel pnlFiltros;
        private CustomControls.IconPlaceholderTextBox txtNombreFiltro;
        private CustomControls.IconPlaceholderTextBox txtDescripcionFiltro;
        private CustomControls.IconPlaceholderTextBox txtCapacidadFiltro;
        private CustomControls.IconPlaceholderTextBox txtTarifaDesdeFiltro;
        private CustomControls.IconPlaceholderTextBox txtTarifaHastaFiltro;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnNuevo;
    }
}