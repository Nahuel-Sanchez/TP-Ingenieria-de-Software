namespace GUI_08YS.Recepcionista
{
    partial class FormPisos_68SA
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
            this.dgvPisos = new System.Windows.Forms.DataGridView();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.txtNumeroFiltro = new CustomControls.IconPlaceholderTextBox();
            this.txtNombreFiltro = new CustomControls.IconPlaceholderTextBox();
            this.btnFiltrar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnNuevo = new FontAwesome.Sharp.IconButton();
            this.pnlGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPisos)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlGrilla.Controls.Add(this.dgvPisos);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Location = new System.Drawing.Point(0, 142);
            this.pnlGrilla.Name = "pnlGrilla";
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(24, 0, 24, 16);
            this.pnlGrilla.Size = new System.Drawing.Size(1000, 458);
            this.pnlGrilla.TabIndex = 0;
            // 
            // dgvPisos
            // 
            this.dgvPisos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPisos.Location = new System.Drawing.Point(24, 0);
            this.dgvPisos.Name = "dgvPisos";
            this.dgvPisos.Size = new System.Drawing.Size(952, 442);
            this.dgvPisos.TabIndex = 0;
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.Controls.Add(this.btnFiltrar);
            this.pnlFiltros.Controls.Add(this.txtNombreFiltro);
            this.pnlFiltros.Controls.Add(this.txtNumeroFiltro);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 72);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(1000, 70);
            this.pnlFiltros.TabIndex = 1;
            // 
            // txtNumeroFiltro
            // 
            this.txtNumeroFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNumeroFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNumeroFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNumeroFiltro.BorderWidth = 2;
            this.txtNumeroFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumeroFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumeroFiltro.IconChar = FontAwesome.Sharp.IconChar.Hashtag;
            this.txtNumeroFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNumeroFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNumeroFiltro.IconPadding = 4;
            this.txtNumeroFiltro.IconSize = 20;
            this.txtNumeroFiltro.Location = new System.Drawing.Point(24, 8);
            this.txtNumeroFiltro.MaxLength = 10;
            this.txtNumeroFiltro.Name = "txtNumeroFiltro";
            this.txtNumeroFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNumeroFiltro.PlaceholderText = "Número";
            this.txtNumeroFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumeroFiltro.Size = new System.Drawing.Size(170, 49);
            this.txtNumeroFiltro.TabIndex = 0;
            // 
            // txtNombreFiltro
            // 
            this.txtNombreFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNombreFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.BorderWidth = 2;
            this.txtNombreFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombreFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombreFiltro.IconChar = FontAwesome.Sharp.IconChar.Building;
            this.txtNombreFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombreFiltro.IconPadding = 4;
            this.txtNombreFiltro.IconSize = 20;
            this.txtNombreFiltro.Location = new System.Drawing.Point(206, 8);
            this.txtNombreFiltro.MaxLength = 50;
            this.txtNombreFiltro.Name = "txtNombreFiltro";
            this.txtNombreFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombreFiltro.PlaceholderText = "Nombre";
            this.txtNombreFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombreFiltro.Size = new System.Drawing.Size(300, 49);
            this.txtNombreFiltro.TabIndex = 1;
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
            this.btnFiltrar.Location = new System.Drawing.Point(518, 8);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(140, 49);
            this.btnFiltrar.TabIndex = 2;
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
            this.btnLimpiar.Location = new System.Drawing.Point(670, 8);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(140, 49);
            this.btnLimpiar.TabIndex = 3;
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
            this.lblTitulo.Tag = "Pisos_titulo";
            this.lblTitulo.Text = "Pisos";
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
            this.btnNuevo.Tag = "Pisos_btnNuevo";
            this.btnNuevo.Text = "Nuevo piso";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevo.UseVisualStyleBackColor = false;
            // 
            // FormPisos_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPisos_68SA";
            this.Text = "Pisos";
            this.pnlGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPisos)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlEncabezado.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvPisos;
        private System.Windows.Forms.Panel pnlFiltros;
        private CustomControls.IconPlaceholderTextBox txtNumeroFiltro;
        private CustomControls.IconPlaceholderTextBox txtNombreFiltro;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnNuevo;
    }
}