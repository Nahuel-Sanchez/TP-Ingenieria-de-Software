namespace GUI_08YS.Recepcionista
{
    partial class FormEditarItemRoomService_68SA
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
            this.pnlBorde = new System.Windows.Forms.Panel();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.tlpContenido = new System.Windows.Forms.TableLayoutPanel();
            this.lblEtiquetaNombre = new System.Windows.Forms.Label();
            this.txtNombre = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaPrecio = new System.Windows.Forms.Label();
            this.nudPrecio = new CustomControls.IconNumericUpDown();
            this.chkActivo = new GUI_08YS.UserControls.CheckBoxHotel_68SA();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnCerrar = new FontAwesome.Sharp.IconButton();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.iconTitulo = new FontAwesome.Sharp.IconPictureBox();
            this.pnlBorde.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.tlpContenido.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTitulo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBorde
            // 
            this.pnlBorde.Controls.Add(this.pnlContenido);
            this.pnlBorde.Controls.Add(this.pnlFooter);
            this.pnlBorde.Controls.Add(this.pnlHeader);
            this.pnlBorde.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlBorde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorde.Name = "pnlBorde";
            // 
            // pnlContenido
            // 
            this.pnlContenido.Controls.Add(this.tlpContenido);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 16, 24, 8);
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlContenido.Name = "pnlContenido";
            // 
            // tlpContenido
            // 
            this.tlpContenido.Controls.Add(this.lblEtiquetaNombre, 0, 0);
            this.tlpContenido.Controls.Add(this.txtNombre, 0, 1);
            this.tlpContenido.Controls.Add(this.lblEtiquetaDescripcion, 0, 2);
            this.tlpContenido.Controls.Add(this.txtDescripcion, 0, 3);
            this.tlpContenido.Controls.Add(this.lblEtiquetaPrecio, 0, 4);
            this.tlpContenido.Controls.Add(this.nudPrecio, 0, 5);
            this.tlpContenido.Controls.Add(this.chkActivo, 1, 5);
            this.tlpContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContenido.SetColumnSpan(this.lblEtiquetaNombre, 2);
            this.tlpContenido.SetColumnSpan(this.txtNombre, 2);
            this.tlpContenido.SetColumnSpan(this.lblEtiquetaDescripcion, 2);
            this.tlpContenido.SetColumnSpan(this.txtDescripcion, 2);
            this.tlpContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContenido.ColumnCount = 2;
            this.tlpContenido.RowCount = 7;
            this.tlpContenido.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tlpContenido.Name = "tlpContenido";
            // 
            // lblEtiquetaNombre
            // 
            this.lblEtiquetaNombre.AutoSize = true;
            this.lblEtiquetaNombre.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaNombre.Tag = "ItemRS_lblNombre";
            this.lblEtiquetaNombre.Text = "Nombre";
            this.lblEtiquetaNombre.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblEtiquetaNombre.Name = "lblEtiquetaNombre";
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNombre.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.BorderWidth = 2;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombre.IconChar = FontAwesome.Sharp.IconChar.Utensils;
            this.txtNombre.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombre.IconPadding = 4;
            this.txtNombre.IconSize = 20;
            this.txtNombre.MaxLength = 100;
            this.txtNombre.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombre.PlaceholderText = "Nombre del ítem";
            this.txtNombre.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombre.Size = new System.Drawing.Size(500, 49);
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombre.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.txtNombre.Name = "txtNombre";
            // 
            // lblEtiquetaDescripcion
            // 
            this.lblEtiquetaDescripcion.AutoSize = true;
            this.lblEtiquetaDescripcion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaDescripcion.Tag = "ItemRS_lblDescripcion";
            this.lblEtiquetaDescripcion.Text = "Descripción (opcional)";
            this.lblEtiquetaDescripcion.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblEtiquetaDescripcion.Name = "lblEtiquetaDescripcion";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtDescripcion.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtDescripcion.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtDescripcion.BorderWidth = 2;
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescripcion.IconChar = FontAwesome.Sharp.IconChar.AlignLeft;
            this.txtDescripcion.IconColor = System.Drawing.Color.Goldenrod;
            this.txtDescripcion.IconColorRight = System.Drawing.Color.DimGray;
            this.txtDescripcion.IconPadding = 4;
            this.txtDescripcion.IconSize = 20;
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtDescripcion.PlaceholderText = "Descripción";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescripcion.Size = new System.Drawing.Size(500, 49);
            this.txtDescripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.txtDescripcion.Name = "txtDescripcion";
            // 
            // lblEtiquetaPrecio
            // 
            this.lblEtiquetaPrecio.AutoSize = true;
            this.lblEtiquetaPrecio.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaPrecio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaPrecio.Tag = "ItemRS_lblPrecio";
            this.lblEtiquetaPrecio.Text = "Precio unitario";
            this.lblEtiquetaPrecio.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblEtiquetaPrecio.Name = "lblEtiquetaPrecio";
            // 
            // nudPrecio
            // 
            this.nudPrecio.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudPrecio.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudPrecio.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudPrecio.BorderWidth = 2;
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrecio.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudPrecio.IconChar = FontAwesome.Sharp.IconChar.DollarSign;
            this.nudPrecio.IconColor = System.Drawing.Color.Goldenrod;
            this.nudPrecio.IconPadding = 4;
            this.nudPrecio.IconSize = 20;
            this.nudPrecio.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudPrecio.Maximum = new decimal(new int[] { 1410065407, 2, 0, 131072 });
            this.nudPrecio.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            this.nudPrecio.Size = new System.Drawing.Size(240, 49);
            this.nudPrecio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudPrecio.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.nudPrecio.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudPrecio.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudPrecio.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudPrecio.ThousandsSeparator = true;
            this.nudPrecio.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            this.nudPrecio.Name = "nudPrecio";
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActivo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.chkActivo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left));
            this.chkActivo.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.chkActivo.Tag = "ItemRS_chkActivo";
            this.chkActivo.Text = "Disponible en la carta";
            this.chkActivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkActivo.Name = "chkActivo";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnGuardar);
            this.pnlFooter.Controls.Add(this.btnCancelar);
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Size = new System.Drawing.Size(554, 64);
            this.pnlFooter.Name = "pnlFooter";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnGuardar.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 22;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnGuardar.Size = new System.Drawing.Size(180, 64);
            this.btnGuardar.Tag = "ItemRS_btnGuardar";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnGuardar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGuardar.Name = "btnGuardar";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnCancelar.IconColor = System.Drawing.Color.Gold;
            this.btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancelar.IconSize = 22;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnCancelar.Size = new System.Drawing.Size(150, 64);
            this.btnCancelar.Tag = "ItemRS_btnCancelar";
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.btnCerrar);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.iconTitulo);
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Size = new System.Drawing.Size(554, 70);
            this.pnlHeader.Name = "pnlHeader";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnCerrar.IconColor = System.Drawing.Color.Gold;
            this.btnCerrar.IconSize = 26;
            this.btnCerrar.Location = new System.Drawing.Point(504, 17);
            this.btnCerrar.Size = new System.Drawing.Size(34, 34);
            this.btnCerrar.TabStop = false;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.Name = "btnCerrar";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Location = new System.Drawing.Point(66, 22);
            this.lblTitulo.Tag = "ItemRS_tituloNuevo";
            this.lblTitulo.Text = "Ítem de la carta";
            this.lblTitulo.Name = "lblTitulo";
            // 
            // iconTitulo
            // 
            this.iconTitulo.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.Utensils;
            this.iconTitulo.IconColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconSize = 34;
            this.iconTitulo.Location = new System.Drawing.Point(18, 16);
            this.iconTitulo.Size = new System.Drawing.Size(38, 38);
            this.iconTitulo.TabStop = false;
            this.iconTitulo.ForeColor = System.Drawing.Color.Gold;
            this.iconTitulo.Name = "iconTitulo";
            // 
            // FormEditarItemRoomService_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(560, 470);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ítem de la carta";
            this.CancelButton = this.btnCancelar;
            this.Controls.Add(this.pnlBorde);
            this.Name = "FormEditarItemRoomService_68SA";
            ((System.ComponentModel.ISupportInitialize)(this.iconTitulo)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.tlpContenido.ResumeLayout(false);
            this.tlpContenido.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlBorde.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBorde;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.TableLayoutPanel tlpContenido;
        private System.Windows.Forms.Label lblEtiquetaNombre;
        private CustomControls.IconPlaceholderTextBox txtNombre;
        private System.Windows.Forms.Label lblEtiquetaDescripcion;
        private CustomControls.IconPlaceholderTextBox txtDescripcion;
        private System.Windows.Forms.Label lblEtiquetaPrecio;
        private CustomControls.IconNumericUpDown nudPrecio;
        private GUI_08YS.UserControls.CheckBoxHotel_68SA chkActivo;
        private System.Windows.Forms.Panel pnlFooter;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private System.Windows.Forms.Panel pnlHeader;
        private FontAwesome.Sharp.IconButton btnCerrar;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconPictureBox iconTitulo;
    }
}
