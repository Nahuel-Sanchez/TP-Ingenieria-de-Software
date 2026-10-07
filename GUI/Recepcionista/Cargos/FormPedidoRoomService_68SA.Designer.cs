namespace GUI_08YS.Recepcionista
{
    partial class FormPedidoRoomService_68SA
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
            this.tlpEstadia = new System.Windows.Forms.TableLayoutPanel();
            this.lblEtiquetaHabitacion = new System.Windows.Forms.Label();
            this.lblEtiquetaTitular = new System.Windows.Forms.Label();
            this.lblEtiquetaEgreso = new System.Windows.Forms.Label();
            this.lblEtiquetaCargosCuenta = new System.Windows.Forms.Label();
            this.lblHabitacion = new System.Windows.Forms.Label();
            this.lblTitular = new System.Windows.Forms.Label();
            this.lblEgreso = new System.Windows.Forms.Label();
            this.lblCargosCuenta = new System.Windows.Forms.Label();
            this.pnlLinea = new System.Windows.Forms.Panel();
            this.txtBuscar = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaPedido = new System.Windows.Forms.Label();
            this.dgvCarta = new System.Windows.Forms.DataGridView();
            this.dgvPedido = new System.Windows.Forms.DataGridView();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.tlpTotal = new System.Windows.Forms.TableLayoutPanel();
            this.lblEtiquetaTotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblNota = new System.Windows.Forms.Label();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnCerrar = new FontAwesome.Sharp.IconButton();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.iconTitulo = new FontAwesome.Sharp.IconPictureBox();
            this.pnlBorde.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.tlpContenido.SuspendLayout();
            this.tlpEstadia.SuspendLayout();
            this.pnlLinea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).BeginInit();
            this.tlpTotal.SuspendLayout();
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
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(20, 14, 20, 10);
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlContenido.Name = "pnlContenido";
            // 
            // tlpContenido
            // 
            this.tlpContenido.Controls.Add(this.tlpEstadia, 0, 0);
            this.tlpContenido.Controls.Add(this.pnlLinea, 0, 1);
            this.tlpContenido.Controls.Add(this.txtBuscar, 0, 2);
            this.tlpContenido.Controls.Add(this.lblEtiquetaPedido, 1, 2);
            this.tlpContenido.Controls.Add(this.dgvCarta, 0, 3);
            this.tlpContenido.Controls.Add(this.dgvPedido, 1, 3);
            this.tlpContenido.Controls.Add(this.lblAyuda, 0, 4);
            this.tlpContenido.Controls.Add(this.tlpTotal, 1, 4);
            this.tlpContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57F));
            this.tlpContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.SetColumnSpan(this.tlpEstadia, 2);
            this.tlpContenido.SetColumnSpan(this.pnlLinea, 2);
            this.tlpContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContenido.ColumnCount = 2;
            this.tlpContenido.RowCount = 5;
            this.tlpContenido.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tlpContenido.Name = "tlpContenido";
            // 
            // tlpEstadia
            // 
            this.tlpEstadia.Controls.Add(this.lblEtiquetaHabitacion, 0, 0);
            this.tlpEstadia.Controls.Add(this.lblEtiquetaTitular, 1, 0);
            this.tlpEstadia.Controls.Add(this.lblEtiquetaEgreso, 2, 0);
            this.tlpEstadia.Controls.Add(this.lblEtiquetaCargosCuenta, 3, 0);
            this.tlpEstadia.Controls.Add(this.lblHabitacion, 0, 1);
            this.tlpEstadia.Controls.Add(this.lblTitular, 1, 1);
            this.tlpEstadia.Controls.Add(this.lblEgreso, 2, 1);
            this.tlpEstadia.Controls.Add(this.lblCargosCuenta, 3, 1);
            this.tlpEstadia.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpEstadia.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpEstadia.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpEstadia.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpEstadia.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpEstadia.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpEstadia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEstadia.AutoSize = true;
            this.tlpEstadia.ColumnCount = 4;
            this.tlpEstadia.RowCount = 2;
            this.tlpEstadia.Margin = new System.Windows.Forms.Padding(0);
            this.tlpEstadia.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tlpEstadia.Name = "tlpEstadia";
            // 
            // lblEtiquetaHabitacion
            // 
            this.lblEtiquetaHabitacion.AutoSize = true;
            this.lblEtiquetaHabitacion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaHabitacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaHabitacion.Tag = "PedidoRS_lblHabitacion";
            this.lblEtiquetaHabitacion.Text = "Habitación";
            this.lblEtiquetaHabitacion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaHabitacion.Name = "lblEtiquetaHabitacion";
            // 
            // lblEtiquetaTitular
            // 
            this.lblEtiquetaTitular.AutoSize = true;
            this.lblEtiquetaTitular.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTitular.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTitular.Tag = "PedidoRS_lblTitular";
            this.lblEtiquetaTitular.Text = "Titular";
            this.lblEtiquetaTitular.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaTitular.Name = "lblEtiquetaTitular";
            // 
            // lblEtiquetaEgreso
            // 
            this.lblEtiquetaEgreso.AutoSize = true;
            this.lblEtiquetaEgreso.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaEgreso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaEgreso.Tag = "PedidoRS_lblEgreso";
            this.lblEtiquetaEgreso.Text = "Egreso";
            this.lblEtiquetaEgreso.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaEgreso.Name = "lblEtiquetaEgreso";
            // 
            // lblEtiquetaCargosCuenta
            // 
            this.lblEtiquetaCargosCuenta.AutoSize = true;
            this.lblEtiquetaCargosCuenta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaCargosCuenta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaCargosCuenta.Tag = "PedidoRS_lblCargosCuenta";
            this.lblEtiquetaCargosCuenta.Text = "Cargos en la cuenta";
            this.lblEtiquetaCargosCuenta.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaCargosCuenta.Name = "lblEtiquetaCargosCuenta";
            // 
            // lblHabitacion
            // 
            this.lblHabitacion.AutoSize = false;
            this.lblHabitacion.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblHabitacion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHabitacion.Text = "-";
            this.lblHabitacion.AutoEllipsis = true;
            this.lblHabitacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHabitacion.Size = new System.Drawing.Size(200, 26);
            this.lblHabitacion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblHabitacion.Name = "lblHabitacion";
            // 
            // lblTitular
            // 
            this.lblTitular.AutoSize = false;
            this.lblTitular.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitular.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitular.Text = "-";
            this.lblTitular.AutoEllipsis = true;
            this.lblTitular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitular.Size = new System.Drawing.Size(200, 26);
            this.lblTitular.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTitular.Name = "lblTitular";
            // 
            // lblEgreso
            // 
            this.lblEgreso.AutoSize = false;
            this.lblEgreso.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblEgreso.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEgreso.Text = "-";
            this.lblEgreso.AutoEllipsis = true;
            this.lblEgreso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEgreso.Size = new System.Drawing.Size(200, 26);
            this.lblEgreso.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblEgreso.Name = "lblEgreso";
            // 
            // lblCargosCuenta
            // 
            this.lblCargosCuenta.AutoSize = false;
            this.lblCargosCuenta.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblCargosCuenta.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCargosCuenta.Text = "-";
            this.lblCargosCuenta.AutoEllipsis = true;
            this.lblCargosCuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCargosCuenta.Size = new System.Drawing.Size(200, 26);
            this.lblCargosCuenta.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblCargosCuenta.Name = "lblCargosCuenta";
            // 
            // pnlLinea
            // 
            this.pnlLinea.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLinea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLinea.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlLinea.Size = new System.Drawing.Size(100, 2);
            this.pnlLinea.Name = "pnlLinea";
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtBuscar.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtBuscar.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtBuscar.BorderWidth = 2;
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.txtBuscar.IconColor = System.Drawing.Color.Goldenrod;
            this.txtBuscar.IconColorRight = System.Drawing.Color.DimGray;
            this.txtBuscar.IconPadding = 4;
            this.txtBuscar.IconSize = 20;
            this.txtBuscar.MaxLength = 100;
            this.txtBuscar.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtBuscar.PlaceholderText = "Buscar en la carta";
            this.txtBuscar.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBuscar.Size = new System.Drawing.Size(520, 49);
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.txtBuscar.Name = "txtBuscar";
            // 
            // lblEtiquetaPedido
            // 
            this.lblEtiquetaPedido.AutoSize = false;
            this.lblEtiquetaPedido.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblEtiquetaPedido.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaPedido.Text = "Pedido";
            this.lblEtiquetaPedido.Tag = "PedidoRS_lblPedido";
            this.lblEtiquetaPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEtiquetaPedido.Size = new System.Drawing.Size(300, 49);
            this.lblEtiquetaPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEtiquetaPedido.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblEtiquetaPedido.Name = "lblEtiquetaPedido";
            // 
            // dgvCarta
            // 
            this.dgvCarta.BackgroundColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.dgvCarta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCarta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCarta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCarta.Margin = new System.Windows.Forms.Padding(0, 0, 12, 8);
            this.dgvCarta.Name = "dgvCarta";
            // 
            // dgvPedido
            // 
            this.dgvPedido.BackgroundColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.dgvPedido.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPedido.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.dgvPedido.Name = "dgvPedido";
            // 
            // lblAyuda
            // 
            this.lblAyuda.AutoSize = false;
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.Tag = "PedidoRS_lblAyuda";
            this.lblAyuda.Text = "Doble clic sobre un ítem (o el botón +) para sumarlo al pedido.";
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAyuda.Size = new System.Drawing.Size(400, 54);
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblAyuda.Name = "lblAyuda";
            // 
            // tlpTotal
            // 
            this.tlpTotal.Controls.Add(this.lblEtiquetaTotal, 0, 0);
            this.tlpTotal.Controls.Add(this.lblTotal, 1, 0);
            this.tlpTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTotal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTotal.AutoSize = true;
            this.tlpTotal.ColumnCount = 2;
            this.tlpTotal.RowCount = 1;
            this.tlpTotal.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.tlpTotal.Padding = new System.Windows.Forms.Padding(14, 6, 14, 6);
            this.tlpTotal.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTotal.Name = "tlpTotal";
            // 
            // lblEtiquetaTotal
            // 
            this.lblEtiquetaTotal.AutoSize = false;
            this.lblEtiquetaTotal.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTotal.Tag = "PedidoRS_lblTotal";
            this.lblEtiquetaTotal.Text = "Total del pedido";
            this.lblEtiquetaTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEtiquetaTotal.Size = new System.Drawing.Size(150, 40);
            this.lblEtiquetaTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEtiquetaTotal.Name = "lblEtiquetaTotal";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = false;
            this.lblTotal.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Text = "$0,00";
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Size = new System.Drawing.Size(150, 40);
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.Name = "lblTotal";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.lblNota);
            this.pnlFooter.Controls.Add(this.btnConfirmar);
            this.pnlFooter.Controls.Add(this.btnCancelar);
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Size = new System.Drawing.Size(994, 64);
            this.pnlFooter.Name = "pnlFooter";
            // 
            // lblNota
            // 
            this.lblNota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNota.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblNota.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNota.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.lblNota.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNota.Tag = "PedidoRS_lblNota";
            this.lblNota.Text = "El pedido se carga a la cuenta de la habitación y se cobra en el check-out.";
            this.lblNota.Name = "lblNota";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnConfirmar.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnConfirmar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConfirmar.IconSize = 22;
            this.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnConfirmar.Size = new System.Drawing.Size(220, 64);
            this.btnConfirmar.Tag = "PedidoRS_btnConfirmar";
            this.btnConfirmar.Text = "Registrar pedido";
            this.btnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnConfirmar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirmar.Name = "btnConfirmar";
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
            this.btnCancelar.Tag = "PedidoRS_btnCancelar";
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
            this.pnlHeader.Size = new System.Drawing.Size(994, 70);
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
            this.btnCerrar.Location = new System.Drawing.Point(944, 17);
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
            this.lblTitulo.Tag = "PedidoRS_lblTitulo";
            this.lblTitulo.Text = "Servicio a la habitación";
            this.lblTitulo.Name = "lblTitulo";
            // 
            // iconTitulo
            // 
            this.iconTitulo.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.BellConcierge;
            this.iconTitulo.IconColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconSize = 34;
            this.iconTitulo.Location = new System.Drawing.Point(18, 16);
            this.iconTitulo.Size = new System.Drawing.Size(38, 38);
            this.iconTitulo.TabStop = false;
            this.iconTitulo.ForeColor = System.Drawing.Color.Gold;
            this.iconTitulo.Name = "iconTitulo";
            // 
            // FormPedidoRoomService_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Servicio a la habitación";
            this.CancelButton = this.btnCancelar;
            this.Controls.Add(this.pnlBorde);
            this.Name = "FormPedidoRoomService_68SA";
            ((System.ComponentModel.ISupportInitialize)(this.iconTitulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarta)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.tlpTotal.ResumeLayout(false);
            this.tlpTotal.PerformLayout();
            this.pnlLinea.ResumeLayout(false);
            this.tlpEstadia.ResumeLayout(false);
            this.tlpEstadia.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tlpEstadia;
        private System.Windows.Forms.Label lblEtiquetaHabitacion;
        private System.Windows.Forms.Label lblEtiquetaTitular;
        private System.Windows.Forms.Label lblEtiquetaEgreso;
        private System.Windows.Forms.Label lblEtiquetaCargosCuenta;
        private System.Windows.Forms.Label lblHabitacion;
        private System.Windows.Forms.Label lblTitular;
        private System.Windows.Forms.Label lblEgreso;
        private System.Windows.Forms.Label lblCargosCuenta;
        private System.Windows.Forms.Panel pnlLinea;
        private CustomControls.IconPlaceholderTextBox txtBuscar;
        private System.Windows.Forms.Label lblEtiquetaPedido;
        private System.Windows.Forms.DataGridView dgvCarta;
        private System.Windows.Forms.DataGridView dgvPedido;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.TableLayoutPanel tlpTotal;
        private System.Windows.Forms.Label lblEtiquetaTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblNota;
        private FontAwesome.Sharp.IconButton btnConfirmar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private System.Windows.Forms.Panel pnlHeader;
        private FontAwesome.Sharp.IconButton btnCerrar;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconPictureBox iconTitulo;
    }
}
