namespace GUI_08YS.Recepcionista
{
    partial class FormHabitaciones_68SA
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
            this.dgvHabitaciones = new System.Windows.Forms.DataGridView();

            this.flpFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.txtNumeroFiltro = new CustomControls.IconPlaceholderTextBox();
            this.cmbPisoFiltro = new CustomControls.IconComboBox();
            this.cmbTipoFiltro = new CustomControls.IconComboBox();
            this.cmbEstadoFiltro = new CustomControls.IconComboBox();

            this.pnlBotones = new System.Windows.Forms.Panel();
            this.flpBotonesDerecha = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFiltrar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();

            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnNueva = new FontAwesome.Sharp.IconButton();

            this.pnlGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHabitaciones)).BeginInit();
            this.flpFiltros.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.flpBotonesDerecha.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlGrilla
            //
            this.pnlGrilla.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlGrilla.Controls.Add(this.dgvHabitaciones);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Name = "pnlGrilla";
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(24, 0, 24, 16);
            this.pnlGrilla.TabIndex = 0;
            //
            // dgvHabitaciones
            //
            this.dgvHabitaciones.ColumnHeadersHeight = 29;
            this.dgvHabitaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHabitaciones.Name = "dgvHabitaciones";
            this.dgvHabitaciones.RowHeadersWidth = 51;
            this.dgvHabitaciones.TabIndex = 0;
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
            this.flpFiltros.Controls.Add(this.txtNumeroFiltro);
            this.flpFiltros.Controls.Add(this.cmbPisoFiltro);
            this.flpFiltros.Controls.Add(this.cmbTipoFiltro);
            this.flpFiltros.Controls.Add(this.cmbEstadoFiltro);
            this.flpFiltros.Name = "flpFiltros";
            this.flpFiltros.TabIndex = 1;
            //
            // txtNumeroFiltro
            //
            this.txtNumeroFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNumeroFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNumeroFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNumeroFiltro.BorderWidth = 2;
            this.txtNumeroFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumeroFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumeroFiltro.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.txtNumeroFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNumeroFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNumeroFiltro.IconPadding = 4;
            this.txtNumeroFiltro.IconSize = 20;
            this.txtNumeroFiltro.MaxLength = 10;
            this.txtNumeroFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.txtNumeroFiltro.MinimumSize = new System.Drawing.Size(200, 49);
            this.txtNumeroFiltro.Name = "txtNumeroFiltro";
            this.txtNumeroFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNumeroFiltro.PlaceholderText = "Habitación";
            this.txtNumeroFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumeroFiltro.Size = new System.Drawing.Size(200, 49);
            this.txtNumeroFiltro.TabIndex = 0;
            //
            // cmbPisoFiltro
            //
            this.cmbPisoFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbPisoFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbPisoFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbPisoFiltro.BorderWidth = 2;
            this.cmbPisoFiltro.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbPisoFiltro.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbPisoFiltro.DropDownForeColor = System.Drawing.Color.WhiteSmoke;
            this.cmbPisoFiltro.DropDownHighlightBackColor = System.Drawing.Color.Goldenrod;
            this.cmbPisoFiltro.DropDownHighlightForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.cmbPisoFiltro.DropDownMaxHeight = 250;
            this.cmbPisoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPisoFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbPisoFiltro.IconChar = FontAwesome.Sharp.IconChar.Building;
            this.cmbPisoFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbPisoFiltro.IconPadding = 4;
            this.cmbPisoFiltro.IconSize = 20;
            this.cmbPisoFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.cmbPisoFiltro.MinimumSize = new System.Drawing.Size(240, 49);
            this.cmbPisoFiltro.Name = "cmbPisoFiltro";
            this.cmbPisoFiltro.Size = new System.Drawing.Size(240, 49);
            this.cmbPisoFiltro.TabIndex = 1;
            //
            // cmbTipoFiltro
            //
            this.cmbTipoFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipoFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoFiltro.BorderWidth = 2;
            this.cmbTipoFiltro.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipoFiltro.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoFiltro.DropDownForeColor = System.Drawing.Color.WhiteSmoke;
            this.cmbTipoFiltro.DropDownHighlightBackColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoFiltro.DropDownHighlightForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.cmbTipoFiltro.DropDownMaxHeight = 250;
            this.cmbTipoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoFiltro.IconChar = FontAwesome.Sharp.IconChar.Bed;
            this.cmbTipoFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoFiltro.IconPadding = 4;
            this.cmbTipoFiltro.IconSize = 20;
            this.cmbTipoFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.cmbTipoFiltro.MinimumSize = new System.Drawing.Size(240, 49);
            this.cmbTipoFiltro.Name = "cmbTipoFiltro";
            this.cmbTipoFiltro.Size = new System.Drawing.Size(240, 49);
            this.cmbTipoFiltro.TabIndex = 2;
            //
            // cmbEstadoFiltro
            //
            this.cmbEstadoFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbEstadoFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.BorderWidth = 2;
            this.cmbEstadoFiltro.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbEstadoFiltro.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.DropDownForeColor = System.Drawing.Color.WhiteSmoke;
            this.cmbEstadoFiltro.DropDownHighlightBackColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.DropDownHighlightForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.cmbEstadoFiltro.DropDownMaxHeight = 250;
            this.cmbEstadoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEstadoFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbEstadoFiltro.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this.cmbEstadoFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbEstadoFiltro.IconPadding = 4;
            this.cmbEstadoFiltro.IconSize = 20;
            this.cmbEstadoFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.cmbEstadoFiltro.MinimumSize = new System.Drawing.Size(220, 49);
            this.cmbEstadoFiltro.Name = "cmbEstadoFiltro";
            this.cmbEstadoFiltro.Size = new System.Drawing.Size(220, 49);
            this.cmbEstadoFiltro.TabIndex = 3;
            //
            // pnlBotones
            //
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlBotones.Controls.Add(this.flpBotonesDerecha);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBotones.Height = 64;
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.TabIndex = 2;
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
            this.flpBotonesDerecha.TabIndex = 0;
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
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.btnNueva);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Height = 72;
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 12, 24, 8);
            this.pnlEncabezado.TabIndex = 3;
            //
            // lblTitulo
            //
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Tag = "Habitaciones_titulo";
            this.lblTitulo.Text = "Administrar habitaciones";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnNueva
            //
            this.btnNueva.BackColor = System.Drawing.Color.Goldenrod;
            this.btnNueva.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNueva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNueva.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNueva.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnNueva.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnNueva.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnNueva.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNueva.IconSize = 22;
            this.btnNueva.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNueva.MinimumSize = new System.Drawing.Size(190, 52);
            this.btnNueva.Name = "btnNueva";
            this.btnNueva.Size = new System.Drawing.Size(190, 52);
            this.btnNueva.TabIndex = 1;
            this.btnNueva.Tag = "Habitaciones_btnNueva";
            this.btnNueva.Text = "Nueva habitación";
            this.btnNueva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNueva.UseVisualStyleBackColor = false;
            //
            // FormHabitaciones_68SA
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1470, 920);
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.flpFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHabitaciones_68SA";
            this.Text = "Administrar habitaciones";
            this.pnlGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHabitaciones)).EndInit();
            this.flpFiltros.ResumeLayout(false);
            this.flpFiltros.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.pnlBotones.PerformLayout();
            this.flpBotonesDerecha.ResumeLayout(false);
            this.pnlEncabezado.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvHabitaciones;
        private System.Windows.Forms.FlowLayoutPanel flpFiltros;
        private CustomControls.IconPlaceholderTextBox txtNumeroFiltro;
        private CustomControls.IconComboBox cmbPisoFiltro;
        private CustomControls.IconComboBox cmbTipoFiltro;
        private CustomControls.IconComboBox cmbEstadoFiltro;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.FlowLayoutPanel flpBotonesDerecha;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnNueva;
    }
}