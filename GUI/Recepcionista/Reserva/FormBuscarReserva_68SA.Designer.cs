namespace GUI_08YS.Recepcionista
{
    partial class FormBuscarReserva_68SA
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

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.pnlBorde = new System.Windows.Forms.Panel();
            this.btnSeleccionar = new FontAwesome.Sharp.IconButton();
            this.pnlLinea2 = new System.Windows.Forms.Panel();
            this.lblResultado = new System.Windows.Forms.Label();
            this.pnlLinea1 = new System.Windows.Forms.Panel();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.txtDocumento = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaDocumento = new System.Windows.Forms.Label();
            this.cmbTipoDocumento = new CustomControls.IconComboBox();
            this.lblEtiquetaTipoDocumento = new System.Windows.Forms.Label();
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
            this.pnlBorde.Controls.Add(this.btnSeleccionar);
            this.pnlBorde.Controls.Add(this.pnlLinea2);
            this.pnlBorde.Controls.Add(this.lblResultado);
            this.pnlBorde.Controls.Add(this.pnlLinea1);
            this.pnlBorde.Controls.Add(this.btnBuscar);
            this.pnlBorde.Controls.Add(this.txtDocumento);
            this.pnlBorde.Controls.Add(this.lblEtiquetaDocumento);
            this.pnlBorde.Controls.Add(this.cmbTipoDocumento);
            this.pnlBorde.Controls.Add(this.lblEtiquetaTipoDocumento);
            this.pnlBorde.Controls.Add(this.pnlHeader);
            this.pnlBorde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorde.Location = new System.Drawing.Point(3, 3);
            this.pnlBorde.Name = "pnlBorde";
            this.pnlBorde.Size = new System.Drawing.Size(634, 453);
            this.pnlBorde.TabIndex = 0;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSeleccionar.Enabled = false;
            this.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnSeleccionar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnSeleccionar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnSeleccionar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSeleccionar.IconSize = 24;
            this.btnSeleccionar.Location = new System.Drawing.Point(183, 305);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Tag = "BuscarReserva_btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(251, 49);
            this.btnSeleccionar.TabIndex = 9;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSeleccionar.UseVisualStyleBackColor = false;
            // 
            // pnlLinea2
            // 
            this.pnlLinea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(75)))), ((int)(((byte)(110)))));
            this.pnlLinea2.Location = new System.Drawing.Point(23, 288);
            this.pnlLinea2.Name = "pnlLinea2";
            this.pnlLinea2.Size = new System.Drawing.Size(549, 1);
            this.pnlLinea2.TabIndex = 8;
            // 
            // lblResultado
            // 
            this.lblResultado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblResultado.Location = new System.Drawing.Point(23, 183);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Tag = "BuscarReserva_lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(549, 85);
            this.lblResultado.TabIndex = 7;
            this.lblResultado.Text = "Ingresá el documento del titular y presioná Buscar.";
            // 
            // pnlLinea1
            // 
            this.pnlLinea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(75)))), ((int)(((byte)(110)))));
            this.pnlLinea1.Location = new System.Drawing.Point(23, 166);
            this.pnlLinea1.Name = "pnlLinea1";
            this.pnlLinea1.Size = new System.Drawing.Size(549, 1);
            this.pnlLinea1.TabIndex = 6;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.IconSize = 24;
            this.btnBuscar.Location = new System.Drawing.Point(439, 109);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Tag = "BuscarReserva_btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(133, 43);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnBuscar.UseVisualStyleBackColor = false;
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
            this.txtDocumento.IconSize = 24;
            this.txtDocumento.Location = new System.Drawing.Point(197, 109);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtDocumento.PlaceholderText = "N° Documento del titular";
            this.txtDocumento.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDocumento.Size = new System.Drawing.Size(229, 43);
            this.txtDocumento.TabIndex = 4;
            // 
            // lblEtiquetaDocumento
            // 
            this.lblEtiquetaDocumento.AutoSize = true;
            this.lblEtiquetaDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaDocumento.Location = new System.Drawing.Point(197, 90);
            this.lblEtiquetaDocumento.Name = "lblEtiquetaDocumento";
            this.lblEtiquetaDocumento.Tag = "BuscarReserva_lblEtiquetaDocumento";
            this.lblEtiquetaDocumento.Size = new System.Drawing.Size(93, 16);
            this.lblEtiquetaDocumento.TabIndex = 3;
            this.lblEtiquetaDocumento.Text = "N° Documento";
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
            this.cmbTipoDocumento.DropDownHighlightBackColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.DropDownHighlightForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.cmbTipoDocumento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoDocumento.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocumento.IconChar = FontAwesome.Sharp.IconChar.IdCard;
            this.cmbTipoDocumento.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.IconSize = 24;
            this.cmbTipoDocumento.Location = new System.Drawing.Point(23, 109);
            this.cmbTipoDocumento.Name = "cmbTipoDocumento";
            this.cmbTipoDocumento.SelectedItem = null;
            this.cmbTipoDocumento.SelectedValue = null;
            this.cmbTipoDocumento.Size = new System.Drawing.Size(160, 43);
            this.cmbTipoDocumento.TabIndex = 2;
            // 
            // lblEtiquetaTipoDocumento
            // 
            this.lblEtiquetaTipoDocumento.AutoSize = true;
            this.lblEtiquetaTipoDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaTipoDocumento.Location = new System.Drawing.Point(23, 90);
            this.lblEtiquetaTipoDocumento.Name = "lblEtiquetaTipoDocumento";
            this.lblEtiquetaTipoDocumento.Tag = "BuscarReserva_lblEtiquetaTipoDocumento";
            this.lblEtiquetaTipoDocumento.Size = new System.Drawing.Size(126, 16);
            this.lblEtiquetaTipoDocumento.TabIndex = 1;
            this.lblEtiquetaTipoDocumento.Text = "Tipo de Documento";
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
            this.pnlHeader.Size = new System.Drawing.Size(634, 75);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.btnCerrar.IconColor = System.Drawing.Color.Gold;
            this.btnCerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCerrar.IconSize = 28;
            this.btnCerrar.Location = new System.Drawing.Point(576, 17);
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
            this.lblTitulo.Size = new System.Drawing.Size(165, 30);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Buscar reserva";
            // 
            // iconTitulo
            // 
            this.iconTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.iconTitulo.ForeColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.iconTitulo.IconColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconTitulo.IconSize = 41;
            this.iconTitulo.Location = new System.Drawing.Point(21, 17);
            this.iconTitulo.Name = "iconTitulo";
            this.iconTitulo.Size = new System.Drawing.Size(43, 41);
            this.iconTitulo.TabIndex = 0;
            this.iconTitulo.TabStop = false;
            // 
            // FormBuscarReserva_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(640, 459);
            this.Controls.Add(this.pnlBorde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBuscarReserva_68SA";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buscar reserva";
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
        private System.Windows.Forms.Label lblEtiquetaTipoDocumento;
        private CustomControls.IconComboBox cmbTipoDocumento;
        private System.Windows.Forms.Label lblEtiquetaDocumento;
        private CustomControls.IconPlaceholderTextBox txtDocumento;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.Panel pnlLinea1;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Panel pnlLinea2;
        private FontAwesome.Sharp.IconButton btnSeleccionar;
    }
}