namespace GUI_08YS.Recepcionista
{
    partial class FormDetalleEstadia_68SA
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
            this.lblEtiquetaReserva = new System.Windows.Forms.Label();
            this.lblEtiquetaTitular = new System.Windows.Forms.Label();
            this.lblEtiquetaHabitacion = new System.Windows.Forms.Label();
            this.lblEtiquetaFechas = new System.Windows.Forms.Label();
            this.lblEtiquetaEstado = new System.Windows.Forms.Label();
            this.lblReserva = new System.Windows.Forms.Label();
            this.lblTitular = new System.Windows.Forms.Label();
            this.lblHabitacion = new System.Windows.Forms.Label();
            this.lblFechas = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.pnlLinea = new System.Windows.Forms.Panel();
            this.lblEtiquetaMovimientos = new System.Windows.Forms.Label();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.tlpTotales = new System.Windows.Forms.TableLayoutPanel();
            this.lblEtiquetaTotAlojamiento = new System.Windows.Forms.Label();
            this.lblEtiquetaTotRenovacion = new System.Windows.Forms.Label();
            this.lblEtiquetaTotCargos = new System.Windows.Forms.Label();
            this.lblEtiquetaTotTotal = new System.Windows.Forms.Label();
            this.lblEtiquetaTotPagado = new System.Windows.Forms.Label();
            this.lblEtiquetaTotSaldo = new System.Windows.Forms.Label();
            this.lblTotAlojamiento = new System.Windows.Forms.Label();
            this.lblTotRenovacion = new System.Windows.Forms.Label();
            this.lblTotCargos = new System.Windows.Forms.Label();
            this.lblTotTotal = new System.Windows.Forms.Label();
            this.lblTotPagado = new System.Windows.Forms.Label();
            this.lblTotSaldo = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblNota = new System.Windows.Forms.Label();
            this.btnFactura = new FontAwesome.Sharp.IconButton();
            this.btnCerrarPie = new FontAwesome.Sharp.IconButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnCerrar = new FontAwesome.Sharp.IconButton();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.iconTitulo = new FontAwesome.Sharp.IconPictureBox();
            this.pnlBorde.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.tlpContenido.SuspendLayout();
            this.tlpEstadia.SuspendLayout();
            this.pnlLinea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.tlpTotales.SuspendLayout();
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
            this.tlpContenido.Controls.Add(this.lblEtiquetaMovimientos, 0, 2);
            this.tlpContenido.Controls.Add(this.dgvMovimientos, 0, 3);
            this.tlpContenido.Controls.Add(this.tlpTotales, 0, 4);
            this.tlpContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContenido.ColumnCount = 1;
            this.tlpContenido.RowCount = 5;
            this.tlpContenido.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tlpContenido.Name = "tlpContenido";
            // 
            // tlpEstadia
            // 
            this.tlpEstadia.Controls.Add(this.lblEtiquetaReserva, 0, 0);
            this.tlpEstadia.Controls.Add(this.lblEtiquetaTitular, 1, 0);
            this.tlpEstadia.Controls.Add(this.lblEtiquetaHabitacion, 2, 0);
            this.tlpEstadia.Controls.Add(this.lblEtiquetaFechas, 3, 0);
            this.tlpEstadia.Controls.Add(this.lblEtiquetaEstado, 4, 0);
            this.tlpEstadia.Controls.Add(this.lblReserva, 0, 1);
            this.tlpEstadia.Controls.Add(this.lblTitular, 1, 1);
            this.tlpEstadia.Controls.Add(this.lblHabitacion, 2, 1);
            this.tlpEstadia.Controls.Add(this.lblFechas, 3, 1);
            this.tlpEstadia.Controls.Add(this.lblEstado, 4, 1);
            this.tlpEstadia.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tlpEstadia.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tlpEstadia.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpEstadia.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tlpEstadia.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpEstadia.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpEstadia.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpEstadia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEstadia.AutoSize = true;
            this.tlpEstadia.ColumnCount = 5;
            this.tlpEstadia.RowCount = 2;
            this.tlpEstadia.Margin = new System.Windows.Forms.Padding(0);
            this.tlpEstadia.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tlpEstadia.Name = "tlpEstadia";
            // 
            // lblEtiquetaReserva
            // 
            this.lblEtiquetaReserva.AutoSize = true;
            this.lblEtiquetaReserva.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaReserva.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaReserva.Tag = "DetalleEst_lblReserva";
            this.lblEtiquetaReserva.Text = "Reserva";
            this.lblEtiquetaReserva.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaReserva.Name = "lblEtiquetaReserva";
            // 
            // lblEtiquetaTitular
            // 
            this.lblEtiquetaTitular.AutoSize = true;
            this.lblEtiquetaTitular.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTitular.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTitular.Tag = "DetalleEst_lblTitular";
            this.lblEtiquetaTitular.Text = "Titular";
            this.lblEtiquetaTitular.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaTitular.Name = "lblEtiquetaTitular";
            // 
            // lblEtiquetaHabitacion
            // 
            this.lblEtiquetaHabitacion.AutoSize = true;
            this.lblEtiquetaHabitacion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaHabitacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaHabitacion.Tag = "DetalleEst_lblHabitacion";
            this.lblEtiquetaHabitacion.Text = "Habitación";
            this.lblEtiquetaHabitacion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaHabitacion.Name = "lblEtiquetaHabitacion";
            // 
            // lblEtiquetaFechas
            // 
            this.lblEtiquetaFechas.AutoSize = true;
            this.lblEtiquetaFechas.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaFechas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaFechas.Tag = "DetalleEst_lblFechas";
            this.lblEtiquetaFechas.Text = "Estadía";
            this.lblEtiquetaFechas.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaFechas.Name = "lblEtiquetaFechas";
            // 
            // lblEtiquetaEstado
            // 
            this.lblEtiquetaEstado.AutoSize = true;
            this.lblEtiquetaEstado.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaEstado.Tag = "DetalleEst_lblEstado";
            this.lblEtiquetaEstado.Text = "Estado";
            this.lblEtiquetaEstado.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaEstado.Name = "lblEtiquetaEstado";
            // 
            // lblReserva
            // 
            this.lblReserva.AutoSize = false;
            this.lblReserva.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblReserva.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserva.Text = "-";
            this.lblReserva.AutoEllipsis = true;
            this.lblReserva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReserva.Size = new System.Drawing.Size(160, 26);
            this.lblReserva.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblReserva.Name = "lblReserva";
            // 
            // lblTitular
            // 
            this.lblTitular.AutoSize = false;
            this.lblTitular.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitular.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitular.Text = "-";
            this.lblTitular.AutoEllipsis = true;
            this.lblTitular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitular.Size = new System.Drawing.Size(160, 26);
            this.lblTitular.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTitular.Name = "lblTitular";
            // 
            // lblHabitacion
            // 
            this.lblHabitacion.AutoSize = false;
            this.lblHabitacion.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblHabitacion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHabitacion.Text = "-";
            this.lblHabitacion.AutoEllipsis = true;
            this.lblHabitacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHabitacion.Size = new System.Drawing.Size(160, 26);
            this.lblHabitacion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblHabitacion.Name = "lblHabitacion";
            // 
            // lblFechas
            // 
            this.lblFechas.AutoSize = false;
            this.lblFechas.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblFechas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechas.Text = "-";
            this.lblFechas.AutoEllipsis = true;
            this.lblFechas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechas.Size = new System.Drawing.Size(160, 26);
            this.lblFechas.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblFechas.Name = "lblFechas";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = false;
            this.lblEstado.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Text = "-";
            this.lblEstado.AutoEllipsis = true;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Size = new System.Drawing.Size(160, 26);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblEstado.Name = "lblEstado";
            // 
            // pnlLinea
            // 
            this.pnlLinea.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLinea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLinea.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlLinea.Size = new System.Drawing.Size(100, 2);
            this.pnlLinea.Name = "pnlLinea";
            // 
            // lblEtiquetaMovimientos
            // 
            this.lblEtiquetaMovimientos.AutoSize = true;
            this.lblEtiquetaMovimientos.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaMovimientos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaMovimientos.Tag = "DetalleEst_lblMovimientos";
            this.lblEtiquetaMovimientos.Text = "Movimientos de la cuenta";
            this.lblEtiquetaMovimientos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblEtiquetaMovimientos.Name = "lblEtiquetaMovimientos";
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.BackgroundColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.dgvMovimientos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMovimientos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMovimientos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.dgvMovimientos.Name = "dgvMovimientos";
            // 
            // tlpTotales
            // 
            this.tlpTotales.Controls.Add(this.lblEtiquetaTotAlojamiento, 0, 0);
            this.tlpTotales.Controls.Add(this.lblEtiquetaTotRenovacion, 1, 0);
            this.tlpTotales.Controls.Add(this.lblEtiquetaTotCargos, 2, 0);
            this.tlpTotales.Controls.Add(this.lblEtiquetaTotTotal, 3, 0);
            this.tlpTotales.Controls.Add(this.lblEtiquetaTotPagado, 4, 0);
            this.tlpTotales.Controls.Add(this.lblEtiquetaTotSaldo, 5, 0);
            this.tlpTotales.Controls.Add(this.lblTotAlojamiento, 0, 1);
            this.tlpTotales.Controls.Add(this.lblTotRenovacion, 1, 1);
            this.tlpTotales.Controls.Add(this.lblTotCargos, 2, 1);
            this.tlpTotales.Controls.Add(this.lblTotTotal, 3, 1);
            this.tlpTotales.Controls.Add(this.lblTotPagado, 4, 1);
            this.tlpTotales.Controls.Add(this.lblTotSaldo, 5, 1);
            this.tlpTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tlpTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tlpTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tlpTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tlpTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tlpTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpTotales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTotales.AutoSize = true;
            this.tlpTotales.ColumnCount = 6;
            this.tlpTotales.RowCount = 2;
            this.tlpTotales.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTotales.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.tlpTotales.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.tlpTotales.Name = "tlpTotales";
            // 
            // lblEtiquetaTotAlojamiento
            // 
            this.lblEtiquetaTotAlojamiento.AutoSize = true;
            this.lblEtiquetaTotAlojamiento.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTotAlojamiento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTotAlojamiento.Tag = "DetalleEst_lblAlojamiento";
            this.lblEtiquetaTotAlojamiento.Text = "Alojamiento";
            this.lblEtiquetaTotAlojamiento.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaTotAlojamiento.Name = "lblEtiquetaTotAlojamiento";
            // 
            // lblEtiquetaTotRenovacion
            // 
            this.lblEtiquetaTotRenovacion.AutoSize = true;
            this.lblEtiquetaTotRenovacion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTotRenovacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTotRenovacion.Tag = "DetalleEst_lblRenovacion";
            this.lblEtiquetaTotRenovacion.Text = "Renovación";
            this.lblEtiquetaTotRenovacion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaTotRenovacion.Name = "lblEtiquetaTotRenovacion";
            // 
            // lblEtiquetaTotCargos
            // 
            this.lblEtiquetaTotCargos.AutoSize = true;
            this.lblEtiquetaTotCargos.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTotCargos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTotCargos.Tag = "DetalleEst_lblCargos";
            this.lblEtiquetaTotCargos.Text = "Cargos adicionales";
            this.lblEtiquetaTotCargos.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaTotCargos.Name = "lblEtiquetaTotCargos";
            // 
            // lblEtiquetaTotTotal
            // 
            this.lblEtiquetaTotTotal.AutoSize = true;
            this.lblEtiquetaTotTotal.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTotTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTotTotal.Tag = "DetalleEst_lblTotal";
            this.lblEtiquetaTotTotal.Text = "Total de la estadía";
            this.lblEtiquetaTotTotal.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaTotTotal.Name = "lblEtiquetaTotTotal";
            // 
            // lblEtiquetaTotPagado
            // 
            this.lblEtiquetaTotPagado.AutoSize = true;
            this.lblEtiquetaTotPagado.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTotPagado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTotPagado.Tag = "DetalleEst_lblPagado";
            this.lblEtiquetaTotPagado.Text = "Pagado";
            this.lblEtiquetaTotPagado.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaTotPagado.Name = "lblEtiquetaTotPagado";
            // 
            // lblEtiquetaTotSaldo
            // 
            this.lblEtiquetaTotSaldo.AutoSize = true;
            this.lblEtiquetaTotSaldo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTotSaldo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTotSaldo.Tag = "DetalleEst_lblSaldo";
            this.lblEtiquetaTotSaldo.Text = "Saldo pendiente";
            this.lblEtiquetaTotSaldo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 2);
            this.lblEtiquetaTotSaldo.Name = "lblEtiquetaTotSaldo";
            // 
            // lblTotAlojamiento
            // 
            this.lblTotAlojamiento.AutoSize = false;
            this.lblTotAlojamiento.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotAlojamiento.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotAlojamiento.Text = "-";
            this.lblTotAlojamiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotAlojamiento.Size = new System.Drawing.Size(140, 26);
            this.lblTotAlojamiento.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTotAlojamiento.Name = "lblTotAlojamiento";
            // 
            // lblTotRenovacion
            // 
            this.lblTotRenovacion.AutoSize = false;
            this.lblTotRenovacion.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotRenovacion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotRenovacion.Text = "-";
            this.lblTotRenovacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotRenovacion.Size = new System.Drawing.Size(140, 26);
            this.lblTotRenovacion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTotRenovacion.Name = "lblTotRenovacion";
            // 
            // lblTotCargos
            // 
            this.lblTotCargos.AutoSize = false;
            this.lblTotCargos.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotCargos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotCargos.Text = "-";
            this.lblTotCargos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotCargos.Size = new System.Drawing.Size(140, 26);
            this.lblTotCargos.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTotCargos.Name = "lblTotCargos";
            // 
            // lblTotTotal
            // 
            this.lblTotTotal.AutoSize = false;
            this.lblTotTotal.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTotTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotTotal.Text = "-";
            this.lblTotTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotTotal.Size = new System.Drawing.Size(140, 30);
            this.lblTotTotal.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTotTotal.Name = "lblTotTotal";
            // 
            // lblTotPagado
            // 
            this.lblTotPagado.AutoSize = false;
            this.lblTotPagado.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotPagado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotPagado.Text = "-";
            this.lblTotPagado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotPagado.Size = new System.Drawing.Size(140, 26);
            this.lblTotPagado.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTotPagado.Name = "lblTotPagado";
            // 
            // lblTotSaldo
            // 
            this.lblTotSaldo.AutoSize = false;
            this.lblTotSaldo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTotSaldo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotSaldo.Text = "-";
            this.lblTotSaldo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotSaldo.Size = new System.Drawing.Size(140, 30);
            this.lblTotSaldo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTotSaldo.Name = "lblTotSaldo";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.lblNota);
            this.pnlFooter.Controls.Add(this.btnFactura);
            this.pnlFooter.Controls.Add(this.btnCerrarPie);
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
            this.lblNota.Tag = "DetalleEst_lblNota";
            this.lblNota.Text = "Los cargos adicionales y las renovaciones se cobran en el check-out.";
            this.lblNota.Name = "lblNota";
            // 
            // btnFactura
            // 
            this.btnFactura.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFactura.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnFactura.IconChar = FontAwesome.Sharp.IconChar.FileInvoiceDollar;
            this.btnFactura.IconColor = System.Drawing.Color.Gold;
            this.btnFactura.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFactura.IconSize = 22;
            this.btnFactura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFactura.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnFactura.Size = new System.Drawing.Size(180, 64);
            this.btnFactura.Tag = "DetalleEst_btnFactura";
            this.btnFactura.Text = "Ver factura";
            this.btnFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnFactura.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFactura.UseVisualStyleBackColor = false;
            this.btnFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFactura.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnFactura.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFactura.Name = "btnFactura";
            // 
            // btnCerrarPie
            // 
            this.btnCerrarPie.BackColor = System.Drawing.Color.Goldenrod;
            this.btnCerrarPie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarPie.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarPie.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnCerrarPie.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnCerrarPie.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnCerrarPie.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCerrarPie.IconSize = 22;
            this.btnCerrarPie.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarPie.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnCerrarPie.Size = new System.Drawing.Size(160, 64);
            this.btnCerrarPie.Tag = "DetalleEst_btnCerrar";
            this.btnCerrarPie.Text = "Cerrar";
            this.btnCerrarPie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCerrarPie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCerrarPie.UseVisualStyleBackColor = false;
            this.btnCerrarPie.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarPie.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnCerrarPie.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrarPie.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCerrarPie.Name = "btnCerrarPie";
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
            this.lblTitulo.Tag = "DetalleEst_lblTitulo";
            this.lblTitulo.Text = "Detalle de la estadía";
            this.lblTitulo.Name = "lblTitulo";
            // 
            // iconTitulo
            // 
            this.iconTitulo.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            this.iconTitulo.IconColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconSize = 34;
            this.iconTitulo.Location = new System.Drawing.Point(18, 16);
            this.iconTitulo.Size = new System.Drawing.Size(38, 38);
            this.iconTitulo.TabStop = false;
            this.iconTitulo.ForeColor = System.Drawing.Color.Gold;
            this.iconTitulo.Name = "iconTitulo";
            // 
            // FormDetalleEstadia_68SA
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
            this.Text = "Detalle de la estadía";
            this.CancelButton = this.btnCerrarPie;
            this.AcceptButton = this.btnCerrarPie;
            this.Controls.Add(this.pnlBorde);
            this.Name = "FormDetalleEstadia_68SA";
            ((System.ComponentModel.ISupportInitialize)(this.iconTitulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.tlpTotales.ResumeLayout(false);
            this.tlpTotales.PerformLayout();
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
        private System.Windows.Forms.Label lblEtiquetaReserva;
        private System.Windows.Forms.Label lblEtiquetaTitular;
        private System.Windows.Forms.Label lblEtiquetaHabitacion;
        private System.Windows.Forms.Label lblEtiquetaFechas;
        private System.Windows.Forms.Label lblEtiquetaEstado;
        private System.Windows.Forms.Label lblReserva;
        private System.Windows.Forms.Label lblTitular;
        private System.Windows.Forms.Label lblHabitacion;
        private System.Windows.Forms.Label lblFechas;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Panel pnlLinea;
        private System.Windows.Forms.Label lblEtiquetaMovimientos;
        private System.Windows.Forms.DataGridView dgvMovimientos;
        private System.Windows.Forms.TableLayoutPanel tlpTotales;
        private System.Windows.Forms.Label lblEtiquetaTotAlojamiento;
        private System.Windows.Forms.Label lblEtiquetaTotRenovacion;
        private System.Windows.Forms.Label lblEtiquetaTotCargos;
        private System.Windows.Forms.Label lblEtiquetaTotTotal;
        private System.Windows.Forms.Label lblEtiquetaTotPagado;
        private System.Windows.Forms.Label lblEtiquetaTotSaldo;
        private System.Windows.Forms.Label lblTotAlojamiento;
        private System.Windows.Forms.Label lblTotRenovacion;
        private System.Windows.Forms.Label lblTotCargos;
        private System.Windows.Forms.Label lblTotTotal;
        private System.Windows.Forms.Label lblTotPagado;
        private System.Windows.Forms.Label lblTotSaldo;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblNota;
        private FontAwesome.Sharp.IconButton btnFactura;
        private FontAwesome.Sharp.IconButton btnCerrarPie;
        private System.Windows.Forms.Panel pnlHeader;
        private FontAwesome.Sharp.IconButton btnCerrar;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconPictureBox iconTitulo;
    }
}
