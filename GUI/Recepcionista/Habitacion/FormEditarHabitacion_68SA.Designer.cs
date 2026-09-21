namespace GUI_08YS.Recepcionista
{
    partial class FormEditarHabitacion_68SA
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
            this.lblAvisoTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new CustomControls.IconComboBox();
            this.lblEtiquetaTipo = new System.Windows.Forms.Label();
            this.cmbPiso = new CustomControls.IconComboBox();
            this.lblEtiquetaPiso = new System.Windows.Forms.Label();
            this.txtNumero = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaNumero = new System.Windows.Forms.Label();
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
            this.pnlBorde.Controls.Add(this.lblAvisoTipo);
            this.pnlBorde.Controls.Add(this.cmbTipo);
            this.pnlBorde.Controls.Add(this.lblEtiquetaTipo);
            this.pnlBorde.Controls.Add(this.cmbPiso);
            this.pnlBorde.Controls.Add(this.lblEtiquetaPiso);
            this.pnlBorde.Controls.Add(this.txtNumero);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNumero);
            this.pnlBorde.Controls.Add(this.pnlFooter);
            this.pnlBorde.Controls.Add(this.pnlHeader);
            this.pnlBorde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorde.Location = new System.Drawing.Point(3, 3);
            this.pnlBorde.Name = "pnlBorde";
            this.pnlBorde.Size = new System.Drawing.Size(474, 424);
            this.pnlBorde.TabIndex = 0;
            // 
            // lblEtiquetaNumero
            // 
            this.lblEtiquetaNumero.AutoSize = true;
            this.lblEtiquetaNumero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEtiquetaNumero.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNumero.Location = new System.Drawing.Point(24, 84);
            this.lblEtiquetaNumero.Name = "lblEtiquetaNumero";
            this.lblEtiquetaNumero.Size = new System.Drawing.Size(140, 19);
            this.lblEtiquetaNumero.TabIndex = 0;
            this.lblEtiquetaNumero.Tag = "Habitaciones_lblNumero";
            this.lblEtiquetaNumero.Text = "Número de habitación";
            // 
            // txtNumero
            // 
            this.txtNumero.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNumero.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNumero.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNumero.BorderWidth = 2;
            this.txtNumero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumero.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumero.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.txtNumero.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNumero.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNumero.IconPadding = 4;
            this.txtNumero.IconSize = 20;
            this.txtNumero.Location = new System.Drawing.Point(24, 106);
            this.txtNumero.MaxLength = 10;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNumero.PlaceholderText = "Número de habitación";
            this.txtNumero.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumero.Size = new System.Drawing.Size(426, 49);
            this.txtNumero.TabIndex = 1;
            // 
            // lblEtiquetaPiso
            // 
            this.lblEtiquetaPiso.AutoSize = true;
            this.lblEtiquetaPiso.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEtiquetaPiso.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaPiso.Location = new System.Drawing.Point(24, 166);
            this.lblEtiquetaPiso.Name = "lblEtiquetaPiso";
            this.lblEtiquetaPiso.Size = new System.Drawing.Size(29, 19);
            this.lblEtiquetaPiso.TabIndex = 2;
            this.lblEtiquetaPiso.Tag = "Habitaciones_lblPiso";
            this.lblEtiquetaPiso.Text = "Piso";
            // 
            // cmbPiso
            // 
            this.cmbPiso.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbPiso.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbPiso.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbPiso.BorderWidth = 2;
            this.cmbPiso.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbPiso.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbPiso.DropDownForeColor = System.Drawing.Color.WhiteSmoke;
            this.cmbPiso.DropDownHighlightBackColor = System.Drawing.Color.Goldenrod;
            this.cmbPiso.DropDownHighlightForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.cmbPiso.DropDownMaxHeight = 250;
            this.cmbPiso.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPiso.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbPiso.IconChar = FontAwesome.Sharp.IconChar.Building;
            this.cmbPiso.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbPiso.IconPadding = 4;
            this.cmbPiso.IconSize = 20;
            this.cmbPiso.Location = new System.Drawing.Point(24, 188);
            this.cmbPiso.Name = "cmbPiso";
            this.cmbPiso.Size = new System.Drawing.Size(426, 49);
            this.cmbPiso.TabIndex = 3;
            // 
            // lblEtiquetaTipo
            // 
            this.lblEtiquetaTipo.AutoSize = true;
            this.lblEtiquetaTipo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEtiquetaTipo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTipo.Location = new System.Drawing.Point(24, 248);
            this.lblEtiquetaTipo.Name = "lblEtiquetaTipo";
            this.lblEtiquetaTipo.Size = new System.Drawing.Size(115, 19);
            this.lblEtiquetaTipo.TabIndex = 4;
            this.lblEtiquetaTipo.Tag = "Habitaciones_lblTipo";
            this.lblEtiquetaTipo.Text = "Tipo de habitación";
            // 
            // cmbTipo
            // 
            this.cmbTipo.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipo.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.BorderWidth = 2;
            this.cmbTipo.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipo.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.DropDownForeColor = System.Drawing.Color.WhiteSmoke;
            this.cmbTipo.DropDownHighlightBackColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.DropDownHighlightForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.cmbTipo.DropDownMaxHeight = 250;
            this.cmbTipo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipo.IconChar = FontAwesome.Sharp.IconChar.Bed;
            this.cmbTipo.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbTipo.IconPadding = 4;
            this.cmbTipo.IconSize = 20;
            this.cmbTipo.Location = new System.Drawing.Point(24, 270);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(426, 49);
            this.cmbTipo.TabIndex = 5;
            // 
            // lblAvisoTipo
            // 
            this.lblAvisoTipo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAvisoTipo.ForeColor = System.Drawing.Color.FromArgb(230, 175, 46);
            this.lblAvisoTipo.Location = new System.Drawing.Point(24, 324);
            this.lblAvisoTipo.Name = "lblAvisoTipo";
            this.lblAvisoTipo.Size = new System.Drawing.Size(426, 34);
            this.lblAvisoTipo.TabIndex = 6;
            this.lblAvisoTipo.Visible = false;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlFooter.Controls.Add(this.btnGuardar);
            this.pnlFooter.Controls.Add(this.btnCancelar);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 360);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(474, 64);
            this.pnlFooter.TabIndex = 7;
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
            this.pnlHeader.TabIndex = 8;
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
            this.lblTitulo.Size = new System.Drawing.Size(140, 25);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Nueva habitación";
            // 
            // iconTitulo
            // 
            this.iconTitulo.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.iconTitulo.ForeColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.iconTitulo.IconColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconTitulo.IconSize = 38;
            this.iconTitulo.Location = new System.Drawing.Point(18, 16);
            this.iconTitulo.Name = "iconTitulo";
            this.iconTitulo.Size = new System.Drawing.Size(38, 38);
            this.iconTitulo.TabIndex = 0;
            this.iconTitulo.TabStop = false;
            // 
            // FormEditarHabitacion_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(480, 430);
            this.Controls.Add(this.pnlBorde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditarHabitacion_68SA";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Habitación";
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
        private System.Windows.Forms.Label lblAvisoTipo;
        private CustomControls.IconComboBox cmbTipo;
        private System.Windows.Forms.Label lblEtiquetaTipo;
        private CustomControls.IconComboBox cmbPiso;
        private System.Windows.Forms.Label lblEtiquetaPiso;
        private CustomControls.IconPlaceholderTextBox txtNumero;
        private System.Windows.Forms.Label lblEtiquetaNumero;
        private System.Windows.Forms.Panel pnlFooter;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private System.Windows.Forms.Panel pnlHeader;
        private FontAwesome.Sharp.IconButton btnCerrar;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconPictureBox iconTitulo;
    }
}