namespace GUI_08YS.Recepcionista
{
    partial class FormPedidosRoomService_68SA
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
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.pnlFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.rbPendientes = new System.Windows.Forms.RadioButton();
            this.rbEntregados = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.txtHabitacion = new CustomControls.IconPlaceholderTextBox();
            this.btnFiltrar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblResumen = new System.Windows.Forms.Label();
            this.btnActualizar = new FontAwesome.Sharp.IconButton();
            this.pnlGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.Controls.Add(this.dgvPedidos);
            this.pnlGrilla.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(24, 0, 24, 16);
            this.pnlGrilla.Name = "pnlGrilla";
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.BackgroundColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.dgvPedidos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPedidos.Name = "dgvPedidos";
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.Controls.Add(this.rbPendientes);
            this.pnlFiltros.Controls.Add(this.rbEntregados);
            this.pnlFiltros.Controls.Add(this.rbTodos);
            this.pnlFiltros.Controls.Add(this.txtHabitacion);
            this.pnlFiltros.Controls.Add(this.btnFiltrar);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.AutoSize = true;
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(24, 8, 12, 0);
            this.pnlFiltros.Size = new System.Drawing.Size(1100, 70);
            this.pnlFiltros.Name = "pnlFiltros";
            // 
            // rbPendientes
            // 
            this.rbPendientes.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPendientes.AutoSize = true;
            this.rbPendientes.BackColor = System.Drawing.Color.FromArgb(15, 25, 55);
            this.rbPendientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbPendientes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPendientes.ForeColor = System.Drawing.Color.FromArgb(200, 200, 210);
            this.rbPendientes.Margin = new System.Windows.Forms.Padding(0, 10, 8, 8);
            this.rbPendientes.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.rbPendientes.Tag = "PedidosRS_rbPendientes";
            this.rbPendientes.Text = "Pendientes";
            this.rbPendientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbPendientes.UseVisualStyleBackColor = false;
            this.rbPendientes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(60, 75, 110);
            this.rbPendientes.Name = "rbPendientes";
            // 
            // rbEntregados
            // 
            this.rbEntregados.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbEntregados.AutoSize = true;
            this.rbEntregados.BackColor = System.Drawing.Color.FromArgb(15, 25, 55);
            this.rbEntregados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbEntregados.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEntregados.ForeColor = System.Drawing.Color.FromArgb(200, 200, 210);
            this.rbEntregados.Margin = new System.Windows.Forms.Padding(0, 10, 8, 8);
            this.rbEntregados.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.rbEntregados.Tag = "PedidosRS_rbEntregados";
            this.rbEntregados.Text = "Entregados hoy";
            this.rbEntregados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbEntregados.UseVisualStyleBackColor = false;
            this.rbEntregados.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(60, 75, 110);
            this.rbEntregados.Name = "rbEntregados";
            // 
            // rbTodos
            // 
            this.rbTodos.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTodos.AutoSize = true;
            this.rbTodos.BackColor = System.Drawing.Color.FromArgb(15, 25, 55);
            this.rbTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTodos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTodos.ForeColor = System.Drawing.Color.FromArgb(200, 200, 210);
            this.rbTodos.Margin = new System.Windows.Forms.Padding(0, 10, 20, 8);
            this.rbTodos.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.rbTodos.Tag = "PedidosRS_rbTodos";
            this.rbTodos.Text = "Todos";
            this.rbTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbTodos.UseVisualStyleBackColor = false;
            this.rbTodos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(60, 75, 110);
            this.rbTodos.Name = "rbTodos";
            // 
            // txtHabitacion
            // 
            this.txtHabitacion.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtHabitacion.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtHabitacion.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtHabitacion.BorderWidth = 2;
            this.txtHabitacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHabitacion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtHabitacion.IconChar = FontAwesome.Sharp.IconChar.Hashtag;
            this.txtHabitacion.IconColor = System.Drawing.Color.Goldenrod;
            this.txtHabitacion.IconColorRight = System.Drawing.Color.DimGray;
            this.txtHabitacion.IconPadding = 4;
            this.txtHabitacion.IconSize = 20;
            this.txtHabitacion.MaxLength = 10;
            this.txtHabitacion.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtHabitacion.PlaceholderText = "Habitación";
            this.txtHabitacion.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtHabitacion.Size = new System.Drawing.Size(170, 49);
            this.txtHabitacion.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.txtHabitacion.Name = "txtHabitacion";
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
            this.pnlEncabezado.Controls.Add(this.lblResumen);
            this.pnlEncabezado.Controls.Add(this.btnActualizar);
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
            this.lblTitulo.Tag = "PedidosRS_titulo";
            this.lblTitulo.Text = "Pedidos de servicio a la habitación";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Name = "lblTitulo";
            // 
            // lblResumen
            // 
            this.lblResumen.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblResumen.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumen.Size = new System.Drawing.Size(360, 52);
            this.lblResumen.Text = "";
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblResumen.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblResumen.Name = "lblResumen";
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnActualizar.IconChar = FontAwesome.Sharp.IconChar.ArrowsRotate;
            this.btnActualizar.IconColor = System.Drawing.Color.Gold;
            this.btnActualizar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnActualizar.IconSize = 22;
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnActualizar.Size = new System.Drawing.Size(170, 52);
            this.btnActualizar.Tag = "PedidosRS_btnActualizar";
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnActualizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnActualizar.Name = "btnActualizar";
            // 
            // FormPedidosRoomService_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text = "Pedidos de servicio a la habitación";
            this.Controls.Add(this.pnlGrilla);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FormPedidosRoomService_68SA";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlGrilla.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrilla;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.FlowLayoutPanel pnlFiltros;
        private System.Windows.Forms.RadioButton rbPendientes;
        private System.Windows.Forms.RadioButton rbEntregados;
        private System.Windows.Forms.RadioButton rbTodos;
        private CustomControls.IconPlaceholderTextBox txtHabitacion;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblResumen;
        private FontAwesome.Sharp.IconButton btnActualizar;
    }
}
