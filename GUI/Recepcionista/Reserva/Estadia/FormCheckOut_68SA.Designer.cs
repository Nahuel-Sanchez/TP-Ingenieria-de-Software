namespace GUI_08YS.Recepcionista
{
    partial class FormCheckOut_68SA
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
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblEncabezado = new System.Windows.Forms.Label();
            this.iconEncabezado = new FontAwesome.Sharp.IconPictureBox();
            this.flpContenido = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlAccionBuscar = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.btnBuscarReserva = new FontAwesome.Sharp.IconButton();
            this.pnlReserva = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblResComposicion = new System.Windows.Forms.Label();
            this.lblResEtiquetaComposicion = new System.Windows.Forms.Label();
            this.lblResFechas = new System.Windows.Forms.Label();
            this.lblResEtiquetaFechas = new System.Windows.Forms.Label();
            this.lblResTitular = new System.Windows.Forms.Label();
            this.lblResEtiquetaTitular = new System.Windows.Forms.Label();
            this.lblResTipo = new System.Windows.Forms.Label();
            this.lblResEtiquetaTipo = new System.Windows.Forms.Label();
            this.lblResHabitacion = new System.Windows.Forms.Label();
            this.lblResEtiquetaHabitacion = new System.Windows.Forms.Label();
            this.pnlLineaReserva = new System.Windows.Forms.Panel();
            this.lblSeccionReserva = new System.Windows.Forms.Label();
            this.pnlCuenta = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblVueltoFinal = new System.Windows.Forms.Label();
            this.lblEtiquetaVueltoFinal = new System.Windows.Forms.Label();
            this.nudMontoRecibidoFinal = new CustomControls.IconNumericUpDown();
            this.lblEtiquetaMontoRecibidoFinal = new System.Windows.Forms.Label();
            this.cmbMetodoPagoFinal = new CustomControls.IconComboBox();
            this.lblEtiquetaMetodoPagoFinal = new System.Windows.Forms.Label();
            this.pnlLineaCuenta2 = new System.Windows.Forms.Panel();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.lblEtiquetaSaldo = new System.Windows.Forms.Label();
            this.lblTotalPagado = new System.Windows.Forms.Label();
            this.lblEtiquetaTotalPagado = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.lblEtiquetaMontoTotal = new System.Windows.Forms.Label();
            this.pnlLineaCuenta1 = new System.Windows.Forms.Panel();
            this.lblSeccionCuenta = new System.Windows.Forms.Label();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).BeginInit();
            this.flpContenido.SuspendLayout();
            this.pnlAccionBuscar.SuspendLayout();
            this.pnlReserva.SuspendLayout();
            this.pnlCuenta.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlEncabezado.Controls.Add(this.lblEncabezado);
            this.pnlEncabezado.Controls.Add(this.iconEncabezado);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(760, 76);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblEncabezado
            // 
            this.lblEncabezado.AutoSize = true;
            this.lblEncabezado.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezado.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblEncabezado.Location = new System.Drawing.Point(72, 24);
            this.lblEncabezado.Name = "lblEncabezado";
            this.lblEncabezado.Size = new System.Drawing.Size(200, 30);
            this.lblEncabezado.TabIndex = 1;
            this.lblEncabezado.Text = "Registrar Check-out";
            // 
            // iconEncabezado
            // 
            this.iconEncabezado.IconChar = FontAwesome.Sharp.IconChar.DoorClosed;
            this.iconEncabezado.IconColor = System.Drawing.Color.Gold;
            this.iconEncabezado.IconSize = 40;
            this.iconEncabezado.Location = new System.Drawing.Point(20, 18);
            this.iconEncabezado.Name = "iconEncabezado";
            this.iconEncabezado.Size = new System.Drawing.Size(44, 44);
            this.iconEncabezado.TabIndex = 0;
            this.iconEncabezado.TabStop = false;
            // 
            // flpContenido
            // 
            this.flpContenido.AutoScroll = true;
            this.flpContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.flpContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpContenido.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpContenido.Location = new System.Drawing.Point(0, 76);
            this.flpContenido.Name = "flpContenido";
            this.flpContenido.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.flpContenido.Size = new System.Drawing.Size(760, 626);
            this.flpContenido.TabIndex = 1;
            this.flpContenido.WrapContents = false;
            // 
            // pnlAccionBuscar
            // 
            this.pnlAccionBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlAccionBuscar.Controls.Add(this.btnBuscarReserva);
            this.pnlAccionBuscar.CornerRadius = 12;
            this.pnlAccionBuscar.Location = new System.Drawing.Point(3, 3);
            this.pnlAccionBuscar.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.pnlAccionBuscar.Name = "pnlAccionBuscar";
            this.pnlAccionBuscar.Size = new System.Drawing.Size(700, 120);
            this.pnlAccionBuscar.TabIndex = 0;
            // 
            // btnBuscarReserva
            // 
            this.btnBuscarReserva.BackColor = System.Drawing.Color.Goldenrod;
            this.btnBuscarReserva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarReserva.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarReserva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnBuscarReserva.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarReserva.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnBuscarReserva.IconSize = 26;
            this.btnBuscarReserva.Location = new System.Drawing.Point(220, 38);
            this.btnBuscarReserva.Name = "btnBuscarReserva";
            this.btnBuscarReserva.Size = new System.Drawing.Size(260, 46);
            this.btnBuscarReserva.TabIndex = 0;
            this.btnBuscarReserva.Text = "Buscar Reserva por DNI";
            this.btnBuscarReserva.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBuscarReserva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscarReserva.UseVisualStyleBackColor = false;
            // 
            // pnlReserva
            // 
            this.pnlReserva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlReserva.Controls.Add(this.lblResComposicion);
            this.pnlReserva.Controls.Add(this.lblResEtiquetaComposicion);
            this.pnlReserva.Controls.Add(this.lblResFechas);
            this.pnlReserva.Controls.Add(this.lblResEtiquetaFechas);
            this.pnlReserva.Controls.Add(this.lblResTitular);
            this.pnlReserva.Controls.Add(this.lblResEtiquetaTitular);
            this.pnlReserva.Controls.Add(this.lblResTipo);
            this.pnlReserva.Controls.Add(this.lblResEtiquetaTipo);
            this.pnlReserva.Controls.Add(this.lblResHabitacion);
            this.pnlReserva.Controls.Add(this.lblResEtiquetaHabitacion);
            this.pnlReserva.Controls.Add(this.pnlLineaReserva);
            this.pnlReserva.Controls.Add(this.lblSeccionReserva);
            this.pnlReserva.CornerRadius = 12;
            this.pnlReserva.Location = new System.Drawing.Point(3, 3);
            this.pnlReserva.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.pnlReserva.Name = "pnlReserva";
            this.pnlReserva.Size = new System.Drawing.Size(700, 168);
            this.pnlReserva.TabIndex = 1;
            // 
            // lblSeccionReserva
            // 
            this.lblSeccionReserva.AutoSize = true;
            this.lblSeccionReserva.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionReserva.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblSeccionReserva.Location = new System.Drawing.Point(16, 12);
            this.lblSeccionReserva.Name = "lblSeccionReserva";
            this.lblSeccionReserva.Size = new System.Drawing.Size(260, 19);
            this.lblSeccionReserva.TabIndex = 0;
            this.lblSeccionReserva.Text = "DATOS DE LA RESERVA";
            // 
            // pnlLineaReserva
            // 
            this.pnlLineaReserva.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLineaReserva.Location = new System.Drawing.Point(16, 38);
            this.pnlLineaReserva.Name = "pnlLineaReserva";
            this.pnlLineaReserva.Size = new System.Drawing.Size(668, 2);
            this.pnlLineaReserva.TabIndex = 1;
            // 
            // lblResEtiquetaHabitacion
            // 
            this.lblResEtiquetaHabitacion.AutoSize = true;
            this.lblResEtiquetaHabitacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblResEtiquetaHabitacion.Location = new System.Drawing.Point(16, 50);
            this.lblResEtiquetaHabitacion.Name = "lblResEtiquetaHabitacion";
            this.lblResEtiquetaHabitacion.Size = new System.Drawing.Size(70, 15);
            this.lblResEtiquetaHabitacion.TabIndex = 2;
            this.lblResEtiquetaHabitacion.Text = "Habitación:";
            // 
            // lblResHabitacion
            // 
            this.lblResHabitacion.AutoSize = true;
            this.lblResHabitacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResHabitacion.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblResHabitacion.Location = new System.Drawing.Point(160, 50);
            this.lblResHabitacion.Name = "lblResHabitacion";
            this.lblResHabitacion.Size = new System.Drawing.Size(50, 15);
            this.lblResHabitacion.TabIndex = 3;
            this.lblResHabitacion.Text = "-";
            // 
            // lblResEtiquetaTipo
            // 
            this.lblResEtiquetaTipo.AutoSize = true;
            this.lblResEtiquetaTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblResEtiquetaTipo.Location = new System.Drawing.Point(350, 50);
            this.lblResEtiquetaTipo.Name = "lblResEtiquetaTipo";
            this.lblResEtiquetaTipo.Size = new System.Drawing.Size(34, 15);
            this.lblResEtiquetaTipo.TabIndex = 4;
            this.lblResEtiquetaTipo.Text = "Tipo:";
            // 
            // lblResTipo
            // 
            this.lblResTipo.AutoSize = true;
            this.lblResTipo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblResTipo.Location = new System.Drawing.Point(420, 50);
            this.lblResTipo.Name = "lblResTipo";
            this.lblResTipo.Size = new System.Drawing.Size(50, 15);
            this.lblResTipo.TabIndex = 5;
            this.lblResTipo.Text = "-";
            // 
            // lblResEtiquetaTitular
            // 
            this.lblResEtiquetaTitular.AutoSize = true;
            this.lblResEtiquetaTitular.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblResEtiquetaTitular.Location = new System.Drawing.Point(16, 78);
            this.lblResEtiquetaTitular.Name = "lblResEtiquetaTitular";
            this.lblResEtiquetaTitular.Size = new System.Drawing.Size(50, 15);
            this.lblResEtiquetaTitular.TabIndex = 6;
            this.lblResEtiquetaTitular.Text = "Titular:";
            // 
            // lblResTitular
            // 
            this.lblResTitular.AutoSize = true;
            this.lblResTitular.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblResTitular.Location = new System.Drawing.Point(160, 78);
            this.lblResTitular.Name = "lblResTitular";
            this.lblResTitular.Size = new System.Drawing.Size(50, 15);
            this.lblResTitular.TabIndex = 7;
            this.lblResTitular.Text = "-";
            // 
            // lblResEtiquetaFechas
            // 
            this.lblResEtiquetaFechas.AutoSize = true;
            this.lblResEtiquetaFechas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblResEtiquetaFechas.Location = new System.Drawing.Point(16, 106);
            this.lblResEtiquetaFechas.Name = "lblResEtiquetaFechas";
            this.lblResEtiquetaFechas.Size = new System.Drawing.Size(50, 15);
            this.lblResEtiquetaFechas.TabIndex = 8;
            this.lblResEtiquetaFechas.Text = "Fechas:";
            // 
            // lblResFechas
            // 
            this.lblResFechas.AutoSize = true;
            this.lblResFechas.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblResFechas.Location = new System.Drawing.Point(160, 106);
            this.lblResFechas.Name = "lblResFechas";
            this.lblResFechas.Size = new System.Drawing.Size(50, 15);
            this.lblResFechas.TabIndex = 9;
            this.lblResFechas.Text = "-";
            // 
            // lblResEtiquetaComposicion
            // 
            this.lblResEtiquetaComposicion.AutoSize = true;
            this.lblResEtiquetaComposicion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblResEtiquetaComposicion.Location = new System.Drawing.Point(16, 134);
            this.lblResEtiquetaComposicion.Name = "lblResEtiquetaComposicion";
            this.lblResEtiquetaComposicion.Size = new System.Drawing.Size(90, 15);
            this.lblResEtiquetaComposicion.TabIndex = 10;
            this.lblResEtiquetaComposicion.Text = "Composición:";
            // 
            // lblResComposicion
            // 
            this.lblResComposicion.AutoSize = true;
            this.lblResComposicion.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblResComposicion.Location = new System.Drawing.Point(160, 134);
            this.lblResComposicion.Name = "lblResComposicion";
            this.lblResComposicion.Size = new System.Drawing.Size(50, 15);
            this.lblResComposicion.TabIndex = 11;
            this.lblResComposicion.Text = "-";
            // 
            // pnlCuenta
            // 
            this.pnlCuenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlCuenta.Controls.Add(this.lblVueltoFinal);
            this.pnlCuenta.Controls.Add(this.lblEtiquetaVueltoFinal);
            this.pnlCuenta.Controls.Add(this.nudMontoRecibidoFinal);
            this.pnlCuenta.Controls.Add(this.lblEtiquetaMontoRecibidoFinal);
            this.pnlCuenta.Controls.Add(this.cmbMetodoPagoFinal);
            this.pnlCuenta.Controls.Add(this.lblEtiquetaMetodoPagoFinal);
            this.pnlCuenta.Controls.Add(this.pnlLineaCuenta2);
            this.pnlCuenta.Controls.Add(this.lblSaldo);
            this.pnlCuenta.Controls.Add(this.lblEtiquetaSaldo);
            this.pnlCuenta.Controls.Add(this.lblTotalPagado);
            this.pnlCuenta.Controls.Add(this.lblEtiquetaTotalPagado);
            this.pnlCuenta.Controls.Add(this.lblMontoTotal);
            this.pnlCuenta.Controls.Add(this.lblEtiquetaMontoTotal);
            this.pnlCuenta.Controls.Add(this.pnlLineaCuenta1);
            this.pnlCuenta.Controls.Add(this.lblSeccionCuenta);
            this.pnlCuenta.CornerRadius = 12;
            this.pnlCuenta.Location = new System.Drawing.Point(3, 3);
            this.pnlCuenta.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.pnlCuenta.Name = "pnlCuenta";
            this.pnlCuenta.Size = new System.Drawing.Size(700, 330);
            this.pnlCuenta.TabIndex = 2;
            // 
            // lblSeccionCuenta
            // 
            this.lblSeccionCuenta.AutoSize = true;
            this.lblSeccionCuenta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionCuenta.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblSeccionCuenta.Location = new System.Drawing.Point(16, 12);
            this.lblSeccionCuenta.Name = "lblSeccionCuenta";
            this.lblSeccionCuenta.Size = new System.Drawing.Size(100, 19);
            this.lblSeccionCuenta.TabIndex = 0;
            this.lblSeccionCuenta.Text = "CUENTA";
            // 
            // pnlLineaCuenta1
            // 
            this.pnlLineaCuenta1.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLineaCuenta1.Location = new System.Drawing.Point(16, 38);
            this.pnlLineaCuenta1.Name = "pnlLineaCuenta1";
            this.pnlLineaCuenta1.Size = new System.Drawing.Size(668, 2);
            this.pnlLineaCuenta1.TabIndex = 1;
            // 
            // lblEtiquetaMontoTotal
            // 
            this.lblEtiquetaMontoTotal.AutoSize = true;
            this.lblEtiquetaMontoTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaMontoTotal.Location = new System.Drawing.Point(16, 50);
            this.lblEtiquetaMontoTotal.Name = "lblEtiquetaMontoTotal";
            this.lblEtiquetaMontoTotal.Size = new System.Drawing.Size(80, 15);
            this.lblEtiquetaMontoTotal.TabIndex = 2;
            this.lblEtiquetaMontoTotal.Text = "Monto Total:";
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.AutoSize = true;
            this.lblMontoTotal.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblMontoTotal.Location = new System.Drawing.Point(160, 50);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(50, 15);
            this.lblMontoTotal.TabIndex = 3;
            this.lblMontoTotal.Text = "-";
            // 
            // lblEtiquetaTotalPagado
            // 
            this.lblEtiquetaTotalPagado.AutoSize = true;
            this.lblEtiquetaTotalPagado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaTotalPagado.Location = new System.Drawing.Point(16, 78);
            this.lblEtiquetaTotalPagado.Name = "lblEtiquetaTotalPagado";
            this.lblEtiquetaTotalPagado.Size = new System.Drawing.Size(90, 15);
            this.lblEtiquetaTotalPagado.TabIndex = 4;
            this.lblEtiquetaTotalPagado.Text = "Total Pagado:";
            // 
            // lblTotalPagado
            // 
            this.lblTotalPagado.AutoSize = true;
            this.lblTotalPagado.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotalPagado.Location = new System.Drawing.Point(160, 78);
            this.lblTotalPagado.Name = "lblTotalPagado";
            this.lblTotalPagado.Size = new System.Drawing.Size(50, 15);
            this.lblTotalPagado.TabIndex = 5;
            this.lblTotalPagado.Text = "-";
            // 
            // pnlLineaCuenta2
            // 
            this.pnlLineaCuenta2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(75)))), ((int)(((byte)(110)))));
            this.pnlLineaCuenta2.Location = new System.Drawing.Point(16, 106);
            this.pnlLineaCuenta2.Name = "pnlLineaCuenta2";
            this.pnlLineaCuenta2.Size = new System.Drawing.Size(668, 1);
            this.pnlLineaCuenta2.TabIndex = 6;
            // 
            // lblEtiquetaSaldo
            // 
            this.lblEtiquetaSaldo.AutoSize = true;
            this.lblEtiquetaSaldo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaSaldo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblEtiquetaSaldo.Location = new System.Drawing.Point(16, 118);
            this.lblEtiquetaSaldo.Name = "lblEtiquetaSaldo";
            this.lblEtiquetaSaldo.Size = new System.Drawing.Size(120, 22);
            this.lblEtiquetaSaldo.TabIndex = 7;
            this.lblEtiquetaSaldo.Text = "Saldo Pendiente:";
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblSaldo.Location = new System.Drawing.Point(200, 116);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(80, 25);
            this.lblSaldo.TabIndex = 8;
            this.lblSaldo.Text = "$0";
            // 
            // lblEtiquetaMetodoPagoFinal
            // 
            this.lblEtiquetaMetodoPagoFinal.AutoSize = true;
            this.lblEtiquetaMetodoPagoFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaMetodoPagoFinal.Location = new System.Drawing.Point(16, 156);
            this.lblEtiquetaMetodoPagoFinal.Name = "lblEtiquetaMetodoPagoFinal";
            this.lblEtiquetaMetodoPagoFinal.Size = new System.Drawing.Size(90, 15);
            this.lblEtiquetaMetodoPagoFinal.TabIndex = 9;
            this.lblEtiquetaMetodoPagoFinal.Text = "Método de Pago";
            // 
            // cmbMetodoPagoFinal
            // 
            this.cmbMetodoPagoFinal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.cmbMetodoPagoFinal.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPagoFinal.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPagoFinal.BorderWidth = 2;
            this.cmbMetodoPagoFinal.Cursor = System.Windows.Forms.Cursors.PanSouth;
            this.cmbMetodoPagoFinal.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.cmbMetodoPagoFinal.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPagoFinal.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbMetodoPagoFinal.DropDownHighlightBackColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPagoFinal.DropDownHighlightForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.cmbMetodoPagoFinal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMetodoPagoFinal.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbMetodoPagoFinal.IconChar = FontAwesome.Sharp.IconChar.CreditCard;
            this.cmbMetodoPagoFinal.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPagoFinal.IconSize = 24;
            this.cmbMetodoPagoFinal.Location = new System.Drawing.Point(16, 174);
            this.cmbMetodoPagoFinal.Name = "cmbMetodoPagoFinal";
            this.cmbMetodoPagoFinal.Size = new System.Drawing.Size(300, 40);
            this.cmbMetodoPagoFinal.TabIndex = 10;
            // 
            // lblEtiquetaMontoRecibidoFinal
            // 
            this.lblEtiquetaMontoRecibidoFinal.AutoSize = true;
            this.lblEtiquetaMontoRecibidoFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaMontoRecibidoFinal.Location = new System.Drawing.Point(16, 224);
            this.lblEtiquetaMontoRecibidoFinal.Name = "lblEtiquetaMontoRecibidoFinal";
            this.lblEtiquetaMontoRecibidoFinal.Size = new System.Drawing.Size(105, 15);
            this.lblEtiquetaMontoRecibidoFinal.TabIndex = 11;
            this.lblEtiquetaMontoRecibidoFinal.Text = "Monto Recibido";
            // 
            // nudMontoRecibidoFinal
            // 
            this.nudMontoRecibidoFinal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.nudMontoRecibidoFinal.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudMontoRecibidoFinal.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudMontoRecibidoFinal.BorderWidth = 2;
            this.nudMontoRecibidoFinal.DecimalPlaces = 2;
            this.nudMontoRecibidoFinal.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudMontoRecibidoFinal.IconChar = FontAwesome.Sharp.IconChar.DollarSign;
            this.nudMontoRecibidoFinal.IconColor = System.Drawing.Color.Goldenrod;
            this.nudMontoRecibidoFinal.IconSize = 22;
            this.nudMontoRecibidoFinal.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudMontoRecibidoFinal.Location = new System.Drawing.Point(16, 242);
            this.nudMontoRecibidoFinal.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            this.nudMontoRecibidoFinal.Name = "nudMontoRecibidoFinal";
            this.nudMontoRecibidoFinal.Size = new System.Drawing.Size(300, 40);
            this.nudMontoRecibidoFinal.TabIndex = 12;
            this.nudMontoRecibidoFinal.ThousandsSeparator = true;
            // 
            // lblEtiquetaVueltoFinal
            // 
            this.lblEtiquetaVueltoFinal.AutoSize = true;
            this.lblEtiquetaVueltoFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.lblEtiquetaVueltoFinal.Location = new System.Drawing.Point(16, 292);
            this.lblEtiquetaVueltoFinal.Name = "lblEtiquetaVueltoFinal";
            this.lblEtiquetaVueltoFinal.Size = new System.Drawing.Size(50, 15);
            this.lblEtiquetaVueltoFinal.TabIndex = 13;
            this.lblEtiquetaVueltoFinal.Text = "Vuelto:";
            // 
            // lblVueltoFinal
            // 
            this.lblVueltoFinal.AutoSize = true;
            this.lblVueltoFinal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVueltoFinal.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblVueltoFinal.Location = new System.Drawing.Point(100, 288);
            this.lblVueltoFinal.Name = "lblVueltoFinal";
            this.lblVueltoFinal.Size = new System.Drawing.Size(60, 20);
            this.lblVueltoFinal.TabIndex = 14;
            this.lblVueltoFinal.Text = "$0.00";
            // 
            // pnlBotones
            // 
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlBotones.Controls.Add(this.btnConfirmar);
            this.pnlBotones.Controls.Add(this.btnCancelar);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotones.Location = new System.Drawing.Point(0, 702);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(760, 64);
            this.pnlBotones.TabIndex = 2;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnConfirmar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.DoorClosed;
            this.btnConfirmar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnConfirmar.IconSize = 26;
            this.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmar.Location = new System.Drawing.Point(560, 0);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnConfirmar.Size = new System.Drawing.Size(200, 64);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar Check-out";
            this.btnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnCancelar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnCancelar.IconColor = System.Drawing.Color.Gold;
            this.btnCancelar.IconSize = 22;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(420, 0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCancelar.Size = new System.Drawing.Size(140, 64);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // FormCheckOut_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(760, 766);
            this.Controls.Add(this.flpContenido);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCheckOut_68SA";
            this.Text = "FormCheckOut_68SA";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).EndInit();
            this.flpContenido.ResumeLayout(false);
            this.pnlAccionBuscar.ResumeLayout(false);
            this.pnlReserva.ResumeLayout(false);
            this.pnlReserva.PerformLayout();
            this.pnlCuenta.ResumeLayout(false);
            this.pnlCuenta.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private FontAwesome.Sharp.IconPictureBox iconEncabezado;
        private System.Windows.Forms.Label lblEncabezado;
        private System.Windows.Forms.FlowLayoutPanel flpContenido;
        private UserControls.RoundedPanel_68SA pnlAccionBuscar;
        private FontAwesome.Sharp.IconButton btnBuscarReserva;
        private UserControls.RoundedPanel_68SA pnlReserva;
        private System.Windows.Forms.Label lblSeccionReserva;
        private System.Windows.Forms.Panel pnlLineaReserva;
        private System.Windows.Forms.Label lblResEtiquetaHabitacion;
        private System.Windows.Forms.Label lblResHabitacion;
        private System.Windows.Forms.Label lblResEtiquetaTipo;
        private System.Windows.Forms.Label lblResTipo;
        private System.Windows.Forms.Label lblResEtiquetaTitular;
        private System.Windows.Forms.Label lblResTitular;
        private System.Windows.Forms.Label lblResEtiquetaFechas;
        private System.Windows.Forms.Label lblResFechas;
        private System.Windows.Forms.Label lblResEtiquetaComposicion;
        private System.Windows.Forms.Label lblResComposicion;
        private UserControls.RoundedPanel_68SA pnlCuenta;
        private System.Windows.Forms.Label lblSeccionCuenta;
        private System.Windows.Forms.Panel pnlLineaCuenta1;
        private System.Windows.Forms.Label lblEtiquetaMontoTotal;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Label lblEtiquetaTotalPagado;
        private System.Windows.Forms.Label lblTotalPagado;
        private System.Windows.Forms.Panel pnlLineaCuenta2;
        private System.Windows.Forms.Label lblEtiquetaSaldo;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Label lblEtiquetaMetodoPagoFinal;
        private CustomControls.IconComboBox cmbMetodoPagoFinal;
        private System.Windows.Forms.Label lblEtiquetaMontoRecibidoFinal;
        private CustomControls.IconNumericUpDown nudMontoRecibidoFinal;
        private System.Windows.Forms.Label lblEtiquetaVueltoFinal;
        private System.Windows.Forms.Label lblVueltoFinal;
        private System.Windows.Forms.Panel pnlBotones;
        private FontAwesome.Sharp.IconButton btnConfirmar;
        private FontAwesome.Sharp.IconButton btnCancelar;
    }
}