namespace GUI_08YS.Recepcionista
{
    partial class FormEditarTipoHabitacion_68SA
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
            this.pnlBorde = new System.Windows.Forms.Panel();
            this.nudTarifa = new CustomControls.IconNumericUpDown();
            this.lblEtiquetaTarifa = new System.Windows.Forms.Label();
            this.nudCapacidad = new CustomControls.IconNumericUpDown();
            this.lblEtiquetaCapacidad = new System.Windows.Forms.Label();
            this.txtDescripcion = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaDescripcion = new System.Windows.Forms.Label();
            this.txtNombre = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaNombre = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnCerrar = new FontAwesome.Sharp.IconButton();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.iconTitulo = new FontAwesome.Sharp.IconPictureBox();
            this.pnlBorde.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTitulo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBorde
            // 
            this.pnlBorde.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlBorde.Controls.Add(this.nudTarifa);
            this.pnlBorde.Controls.Add(this.lblEtiquetaTarifa);
            this.pnlBorde.Controls.Add(this.nudCapacidad);
            this.pnlBorde.Controls.Add(this.lblEtiquetaCapacidad);
            this.pnlBorde.Controls.Add(this.txtDescripcion);
            this.pnlBorde.Controls.Add(this.lblEtiquetaDescripcion);
            this.pnlBorde.Controls.Add(this.txtNombre);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNombre);
            this.pnlBorde.Controls.Add(this.pnlFooter);
            this.pnlBorde.Controls.Add(this.pnlHeader);
            this.pnlBorde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorde.Location = new System.Drawing.Point(3, 3);
            this.pnlBorde.Name = "pnlBorde";
            this.pnlBorde.Size = new System.Drawing.Size(474, 404);
            this.pnlBorde.TabIndex = 0;
            // 
            // lblEtiquetaNombre
            // 
            this.lblEtiquetaNombre.AutoSize = true;
            this.lblEtiquetaNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEtiquetaNombre.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNombre.Location = new System.Drawing.Point(24, 84);
            this.lblEtiquetaNombre.Name = "lblEtiquetaNombre";
            this.lblEtiquetaNombre.Size = new System.Drawing.Size(58, 19);
            this.lblEtiquetaNombre.TabIndex = 0;
            this.lblEtiquetaNombre.Tag = "Tipos_lblNombre";
            this.lblEtiquetaNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNombre.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderWidth = 2;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombre.IconChar = FontAwesome.Sharp.IconChar.Bed;
            this.txtNombre.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombre.IconPadding = 4;
            this.txtNombre.IconSize = 20;
            this.txtNombre.Location = new System.Drawing.Point(24, 106);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombre.PlaceholderText = "Nombre";
            this.txtNombre.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombre.Size = new System.Drawing.Size(426, 49);
            this.txtNombre.TabIndex = 1;
            // 
            // lblEtiquetaDescripcion
            // 
            this.lblEtiquetaDescripcion.AutoSize = true;
            this.lblEtiquetaDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEtiquetaDescripcion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaDescripcion.Location = new System.Drawing.Point(24, 166);
            this.lblEtiquetaDescripcion.Name = "lblEtiquetaDescripcion";
            this.lblEtiquetaDescripcion.Size = new System.Drawing.Size(132, 19);
            this.lblEtiquetaDescripcion.TabIndex = 2;
            this.lblEtiquetaDescripcion.Tag = "Tipos_lblDescripcion";
            this.lblEtiquetaDescripcion.Text = "Descripción (opcional)";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtDescripcion.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtDescripcion.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtDescripcion.BorderWidth = 2;
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescripcion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescripcion.IconChar = FontAwesome.Sharp.IconChar.AlignLeft;
            this.txtDescripcion.IconColor = System.Drawing.Color.Goldenrod;
            this.txtDescripcion.IconColorRight = System.Drawing.Color.DimGray;
            this.txtDescripcion.IconPadding = 4;
            this.txtDescripcion.IconSize = 20;
            this.txtDescripcion.Location = new System.Drawing.Point(24, 188);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtDescripcion.PlaceholderText = "Descripción";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescripcion.Size = new System.Drawing.Size(426, 49);
            this.txtDescripcion.TabIndex = 3;
            // 
            // lblEtiquetaCapacidad
            // 
            this.lblEtiquetaCapacidad.AutoSize = true;
            this.lblEtiquetaCapacidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEtiquetaCapacidad.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaCapacidad.Location = new System.Drawing.Point(24, 248);
            this.lblEtiquetaCapacidad.Name = "lblEtiquetaCapacidad";
            this.lblEtiquetaCapacidad.Size = new System.Drawing.Size(126, 19);
            this.lblEtiquetaCapacidad.TabIndex = 4;
            this.lblEtiquetaCapacidad.Tag = "Tipos_lblCapacidad";
            this.lblEtiquetaCapacidad.Text = "Capacidad (personas)";
            // 
            // nudCapacidad
            // 
            this.nudCapacidad.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudCapacidad.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudCapacidad.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudCapacidad.BorderWidth = 2;
            this.nudCapacidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCapacidad.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudCapacidad.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.nudCapacidad.IconColor = System.Drawing.Color.Goldenrod;
            this.nudCapacidad.IconPadding = 4;
            this.nudCapacidad.IconSize = 20;
            this.nudCapacidad.Location = new System.Drawing.Point(24, 270);
            this.nudCapacidad.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            this.nudCapacidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCapacidad.Name = "nudCapacidad";
            this.nudCapacidad.Size = new System.Drawing.Size(200, 49);
            this.nudCapacidad.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudCapacidad.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudCapacidad.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudCapacidad.TabIndex = 5;
            this.nudCapacidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblEtiquetaTarifa
            // 
            this.lblEtiquetaTarifa.AutoSize = true;
            this.lblEtiquetaTarifa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEtiquetaTarifa.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTarifa.Location = new System.Drawing.Point(236, 248);
            this.lblEtiquetaTarifa.Name = "lblEtiquetaTarifa";
            this.lblEtiquetaTarifa.Size = new System.Drawing.Size(106, 19);
            this.lblEtiquetaTarifa.TabIndex = 6;
            this.lblEtiquetaTarifa.Tag = "Tipos_lblTarifa";
            this.lblEtiquetaTarifa.Text = "Tarifa por noche";
            // 
            // nudTarifa
            // 
            this.nudTarifa.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudTarifa.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudTarifa.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudTarifa.BorderWidth = 2;
            this.nudTarifa.DecimalPlaces = 2;
            this.nudTarifa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudTarifa.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudTarifa.IconChar = FontAwesome.Sharp.IconChar.DollarSign;
            this.nudTarifa.IconColor = System.Drawing.Color.Goldenrod;
            this.nudTarifa.IconPadding = 4;
            this.nudTarifa.IconSize = 20;
            this.nudTarifa.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudTarifa.Location = new System.Drawing.Point(236, 270);
            this.nudTarifa.Maximum = new decimal(new int[] { 1410065407, 2, 0, 131072 });
            this.nudTarifa.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            this.nudTarifa.Name = "nudTarifa";
            this.nudTarifa.Size = new System.Drawing.Size(214, 49);
            this.nudTarifa.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudTarifa.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudTarifa.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudTarifa.TabIndex = 7;
            this.nudTarifa.ThousandsSeparator = true;
            this.nudTarifa.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlFooter.Controls.Add(this.btnGuardar);
            this.pnlFooter.Controls.Add(this.btnCancelar);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 340);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(474, 64);
            this.pnlFooter.TabIndex = 8;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnGuardar.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 22;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(278, 8);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(180, 48);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Tag = "Crud_btnGuardar";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnCancelar.IconColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancelar.IconSize = 22;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(126, 8);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(140, 48);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Tag = "Crud_btnCancelar";
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlHeader.Controls.Add(this.btnCerrar);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.iconTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(474, 70);
            this.pnlHeader.TabIndex = 9;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCerrar.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnCerrar.IconColor = System.Drawing.Color.Goldenrod;
            this.btnCerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCerrar.IconSize = 20;
            this.btnCerrar.Location = new System.Drawing.Point(424, 17);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(34, 34);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Location = new System.Drawing.Point(66, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(200, 25);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Nuevo tipo de habitación";
            // 
            // iconTitulo
            // 
            this.iconTitulo.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.iconTitulo.ForeColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.Bed;
            this.iconTitulo.IconColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconTitulo.IconSize = 38;
            this.iconTitulo.Location = new System.Drawing.Point(18, 16);
            this.iconTitulo.Name = "iconTitulo";
            this.iconTitulo.Size = new System.Drawing.Size(38, 38);
            this.iconTitulo.TabIndex = 0;
            this.iconTitulo.TabStop = false;
            // 
            // FormEditarTipoHabitacion_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(480, 410);
            this.Controls.Add(this.pnlBorde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditarTipoHabitacion_68SA";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tipo de habitación";
            this.pnlBorde.ResumeLayout(false);
            this.pnlBorde.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTitulo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBorde;
        private CustomControls.IconNumericUpDown nudTarifa;
        private System.Windows.Forms.Label lblEtiquetaTarifa;
        private CustomControls.IconNumericUpDown nudCapacidad;
        private System.Windows.Forms.Label lblEtiquetaCapacidad;
        private CustomControls.IconPlaceholderTextBox txtDescripcion;
        private System.Windows.Forms.Label lblEtiquetaDescripcion;
        private CustomControls.IconPlaceholderTextBox txtNombre;
        private System.Windows.Forms.Label lblEtiquetaNombre;
        private System.Windows.Forms.Panel pnlFooter;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private System.Windows.Forms.Panel pnlHeader;
        private FontAwesome.Sharp.IconButton btnCerrar;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconPictureBox iconTitulo;
    }
}