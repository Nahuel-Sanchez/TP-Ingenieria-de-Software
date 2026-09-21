namespace GUI_08YS.Recepcionista
{
    partial class FormEditarPiso_68SA
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
            this.txtNombre = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaNombre = new System.Windows.Forms.Label();
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
            this.pnlBorde.Controls.Add(this.txtNombre);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNombre);
            this.pnlBorde.Controls.Add(this.txtNumero);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNumero);
            this.pnlBorde.Controls.Add(this.pnlFooter);
            this.pnlBorde.Controls.Add(this.pnlHeader);
            this.pnlBorde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorde.Location = new System.Drawing.Point(3, 3);
            this.pnlBorde.Name = "pnlBorde";
            this.pnlBorde.Size = new System.Drawing.Size(474, 324);
            this.pnlBorde.TabIndex = 0;
            // 
            // lblEtiquetaNumero
            // 
            this.lblEtiquetaNumero.AutoSize = true;
            this.lblEtiquetaNumero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEtiquetaNumero.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNumero.Location = new System.Drawing.Point(24, 88);
            this.lblEtiquetaNumero.Name = "lblEtiquetaNumero";
            this.lblEtiquetaNumero.Size = new System.Drawing.Size(103, 19);
            this.lblEtiquetaNumero.TabIndex = 0;
            this.lblEtiquetaNumero.Tag = "Pisos_lblNumero";
            this.lblEtiquetaNumero.Text = "Número de piso";
            // 
            // txtNumero
            // 
            this.txtNumero.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNumero.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNumero.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNumero.BorderWidth = 2;
            this.txtNumero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumero.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumero.IconChar = FontAwesome.Sharp.IconChar.Hashtag;
            this.txtNumero.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNumero.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNumero.IconPadding = 4;
            this.txtNumero.IconSize = 20;
            this.txtNumero.Location = new System.Drawing.Point(24, 110);
            this.txtNumero.MaxLength = 10;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNumero.PlaceholderText = "Número de piso";
            this.txtNumero.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumero.Size = new System.Drawing.Size(426, 49);
            this.txtNumero.TabIndex = 1;
            // 
            // lblEtiquetaNombre
            // 
            this.lblEtiquetaNombre.AutoSize = true;
            this.lblEtiquetaNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEtiquetaNombre.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNombre.Location = new System.Drawing.Point(24, 172);
            this.lblEtiquetaNombre.Name = "lblEtiquetaNombre";
            this.lblEtiquetaNombre.Size = new System.Drawing.Size(107, 19);
            this.lblEtiquetaNombre.TabIndex = 2;
            this.lblEtiquetaNombre.Tag = "Pisos_lblNombre";
            this.lblEtiquetaNombre.Text = "Nombre del piso";
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNombre.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderWidth = 2;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombre.IconChar = FontAwesome.Sharp.IconChar.Building;
            this.txtNombre.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombre.IconPadding = 4;
            this.txtNombre.IconSize = 20;
            this.txtNombre.Location = new System.Drawing.Point(24, 194);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombre.PlaceholderText = "Nombre del piso";
            this.txtNombre.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombre.Size = new System.Drawing.Size(426, 49);
            this.txtNombre.TabIndex = 3;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlFooter.Controls.Add(this.btnGuardar);
            this.pnlFooter.Controls.Add(this.btnCancelar);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 260);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(474, 64);
            this.pnlFooter.TabIndex = 4;
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
            this.pnlHeader.TabIndex = 5;
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
            this.lblTitulo.Size = new System.Drawing.Size(101, 25);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Nuevo piso";
            // 
            // iconTitulo
            // 
            this.iconTitulo.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.iconTitulo.ForeColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.Building;
            this.iconTitulo.IconColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconTitulo.IconSize = 38;
            this.iconTitulo.Location = new System.Drawing.Point(18, 16);
            this.iconTitulo.Name = "iconTitulo";
            this.iconTitulo.Size = new System.Drawing.Size(38, 38);
            this.iconTitulo.TabIndex = 0;
            this.iconTitulo.TabStop = false;
            // 
            // FormEditarPiso_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(480, 330);
            this.Controls.Add(this.pnlBorde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditarPiso_68SA";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Piso";
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
        private CustomControls.IconPlaceholderTextBox txtNombre;
        private System.Windows.Forms.Label lblEtiquetaNombre;
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