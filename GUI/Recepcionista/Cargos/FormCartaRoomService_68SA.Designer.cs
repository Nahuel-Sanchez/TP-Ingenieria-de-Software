namespace GUI_08YS.Recepcionista
{
    partial class FormCartaRoomService_68SA
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
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.pnlFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.txtNombreFiltro = new CustomControls.IconPlaceholderTextBox();
            this.chkInactivos = new GUI_08YS.UserControls.CheckBoxHotel_68SA();
            this.btnFiltrar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnNuevo = new FontAwesome.Sharp.IconButton();
            this.pnlGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.Controls.Add(this.dgvItems);
            this.pnlGrilla.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(24, 0, 24, 16);
            this.pnlGrilla.Name = "pnlGrilla";
            // 
            // dgvItems
            // 
            this.dgvItems.BackgroundColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.Name = "dgvItems";
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.Controls.Add(this.txtNombreFiltro);
            this.pnlFiltros.Controls.Add(this.chkInactivos);
            this.pnlFiltros.Controls.Add(this.btnFiltrar);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.AutoSize = true;
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(24, 8, 12, 0);
            this.pnlFiltros.Size = new System.Drawing.Size(1100, 70);
            this.pnlFiltros.Name = "pnlFiltros";
            // 
            // txtNombreFiltro
            // 
            this.txtNombreFiltro.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNombreFiltro.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.BorderWidth = 2;
            this.txtNombreFiltro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreFiltro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombreFiltro.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.txtNombreFiltro.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombreFiltro.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombreFiltro.IconPadding = 4;
            this.txtNombreFiltro.IconSize = 20;
            this.txtNombreFiltro.MaxLength = 100;
            this.txtNombreFiltro.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombreFiltro.PlaceholderText = "Nombre o descripción";
            this.txtNombreFiltro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombreFiltro.Size = new System.Drawing.Size(300, 49);
            this.txtNombreFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.txtNombreFiltro.Name = "txtNombreFiltro";
            // 
            // chkInactivos
            // 
            this.chkInactivos.AutoSize = true;
            this.chkInactivos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInactivos.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.chkInactivos.Margin = new System.Windows.Forms.Padding(4, 15, 16, 8);
            this.chkInactivos.Tag = "CartaRS_chkInactivos";
            this.chkInactivos.Text = "Mostrar dados de baja";
            this.chkInactivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkInactivos.Name = "chkInactivos";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnFiltrar.IconChar = FontAwesome.Sharp.IconChar.Filter;
            this.btnFiltrar.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnFiltrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFiltrar.IconSize = 22;
            this.btnFiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnFiltrar.Size = new System.Drawing.Size(140, 49);
            this.btnFiltrar.Tag = "Crud_btnFiltrar";
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnFiltrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnFiltrar.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.btnFiltrar.Name = "btnFiltrar";
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
            this.btnLimpiar.Size = new System.Drawing.Size(140, 49);
            this.btnLimpiar.Tag = "Crud_btnLimpiar";
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.btnLimpiar.Name = "btnLimpiar";
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.btnNuevo);
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 12, 24, 8);
            this.pnlEncabezado.Size = new System.Drawing.Size(1100, 72);
            this.pnlEncabezado.Name = "pnlEncabezado";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Tag = "CartaRS_titulo";
            this.lblTitulo.Text = "Carta de servicio a la habitación";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Name = "lblTitulo";
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Goldenrod;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnNuevo.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnNuevo.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNuevo.IconSize = 22;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNuevo.Size = new System.Drawing.Size(190, 52);
            this.btnNuevo.Tag = "CartaRS_btnNuevo";
            this.btnNuevo.Text = "Nuevo ítem";
            this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnNuevo.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNuevo.Name = "btnNuevo";
            // 
            // FormCartaRoomService_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text = "Carta de servicio a la habitación";
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FormCartaRoomService_68SA";
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlGrilla.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.FlowLayoutPanel pnlFiltros;
        private CustomControls.IconPlaceholderTextBox txtNombreFiltro;
        private GUI_08YS.UserControls.CheckBoxHotel_68SA chkInactivos;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnNuevo;
    }
}
