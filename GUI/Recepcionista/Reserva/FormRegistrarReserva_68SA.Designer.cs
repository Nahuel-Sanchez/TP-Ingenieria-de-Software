namespace GUI_08YS.Recepcionista
{
    partial class FormRegistrarReserva_68SA
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
            this.components = new System.ComponentModel.Container();
            this.tblContenido = new System.Windows.Forms.TableLayoutPanel();
            this.pnlColHuesped = new System.Windows.Forms.Panel();
            this.pnlPago = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblSeccionPago = new System.Windows.Forms.Label();
            this.pnlLineaPago = new System.Windows.Forms.Panel();
            this.lblEtiquetaMetodoPago = new System.Windows.Forms.Label();
            this.cmbMetodoPago = new CustomControls.IconComboBox();
            this.lblEtiquetaMontoRecibido = new System.Windows.Forms.Label();
            this.nudMontoRecibido = new CustomControls.IconNumericUpDown();
            this.lblMarcaTarjetaDetectada = new System.Windows.Forms.Label();
            this.lblVueltoEtiqueta = new System.Windows.Forms.Label();
            this.lblVuelto = new System.Windows.Forms.Label();
            this.lblEtiquetaNumeroTarjeta = new System.Windows.Forms.Label();
            this.txtNumeroTarjeta = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaTitularTarjeta = new System.Windows.Forms.Label();
            this.txtTitularTarjeta = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaVencimiento = new System.Windows.Forms.Label();
            this.nudMesVencimiento = new CustomControls.IconNumericUpDown();
            this.lblBarraVencimiento = new System.Windows.Forms.Label();
            this.nudAnioVencimiento = new CustomControls.IconNumericUpDown();
            this.lblEtiquetaCvv = new System.Windows.Forms.Label();
            this.txtCvv = new CustomControls.IconPlaceholderTextBox();
            this.pnlSeparadorCol = new System.Windows.Forms.Panel();
            this.pnlIzquierda = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblSeccionHuesped = new System.Windows.Forms.Label();
            this.pnlLineaHuesped1 = new System.Windows.Forms.Panel();
            this.lblEtiquetaTipoDocumento = new System.Windows.Forms.Label();
            this.cmbTipoDocumento = new CustomControls.IconComboBox();
            this.lblEtiquetaDni = new System.Windows.Forms.Label();
            this.txtDni = new CustomControls.IconPlaceholderTextBox();
            this.btnConsultarDocumento = new FontAwesome.Sharp.IconButton();
            this.btnHuespedNuevo = new FontAwesome.Sharp.IconButton();
            this.lblEtiquetaNombre = new System.Windows.Forms.Label();
            this.txtNombre = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaApellido = new System.Windows.Forms.Label();
            this.txtApellido = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaNacionalidad = new System.Windows.Forms.Label();
            this.txtNacionalidad = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new CustomControls.IconPlaceholderTextBox();
            this.lblEtiquetaEmail = new System.Windows.Forms.Label();
            this.txtEmail = new CustomControls.IconPlaceholderTextBox();
            this.pnlColReserva = new System.Windows.Forms.Panel();
            this.pnlResumen = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.pnlLineaResumen1 = new System.Windows.Forms.Panel();
            this.lblSeccionResumen = new System.Windows.Forms.Label();
            this.lblEtiquetaFechaIngreso = new System.Windows.Forms.Label();
            this.dtpFechaIngreso = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaFechaEgreso = new System.Windows.Forms.Label();
            this.dtpFechaEgreso = new CustomControls.IconDateTimePicker();
            this.lblResEtiquetaNoches = new System.Windows.Forms.Label();
            this.lblResNoches = new System.Windows.Forms.Label();
            this.pnlLineaResumen2 = new System.Windows.Forms.Panel();
            this.lblSeccionComposicion = new System.Windows.Forms.Label();
            this.lblEtiquetaAdultos = new System.Windows.Forms.Label();
            this.nudAdultos = new CustomControls.IconNumericUpDown();
            this.lblEtiquetaNinos = new System.Windows.Forms.Label();
            this.nudNinos = new CustomControls.IconNumericUpDown();
            this.lblComposicionAdvertencia = new System.Windows.Forms.Label();
            this.pnlTotal = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.pnlLineaResumen3 = new System.Windows.Forms.Panel();
            this.lblResMontoTotal = new System.Windows.Forms.Label();
            this.lblResEtiquetaMontoTotal = new System.Windows.Forms.Label();
            this.pnlSeparadorDer = new System.Windows.Forms.Panel();
            this.pnlDetalles = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblResEtiquetaHabitacion = new System.Windows.Forms.Label();
            this.lblResHabitacion = new System.Windows.Forms.Label();
            this.lblSeccionDetalles = new System.Windows.Forms.Label();
            this.lblDetTipo = new System.Windows.Forms.Label();
            this.lblDetDescripcion = new System.Windows.Forms.Label();
            this.pnlLineaDetalles = new System.Windows.Forms.Panel();
            this.lblDetEtiquetaPiso = new System.Windows.Forms.Label();
            this.lblDetPiso = new System.Windows.Forms.Label();
            this.lblDetEtiquetaCapacidad = new System.Windows.Forms.Label();
            this.lblDetCapacidad = new System.Windows.Forms.Label();
            this.lblDetEtiquetaPrecio = new System.Windows.Forms.Label();
            this.lblDetPrecio = new System.Windows.Forms.Label();
            this.lblDetEtiquetaTipo = new System.Windows.Forms.Label();
            this.lblDetEtiquetaDescripcion = new System.Windows.Forms.Label();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.lblAvisoConfirmar = new System.Windows.Forms.Label();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.iconEncabezado = new FontAwesome.Sharp.IconPictureBox();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblEncabezado = new System.Windows.Forms.Label();
            this.pasosReserva = new GUI_08YS.UserControls.PasosReserva_68SA();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tblContenido.SuspendLayout();
            this.pnlColHuesped.SuspendLayout();
            this.pnlPago.SuspendLayout();
            this.pnlLineaPago.SuspendLayout();
            this.pnlSeparadorCol.SuspendLayout();
            this.pnlIzquierda.SuspendLayout();
            this.pnlLineaHuesped1.SuspendLayout();
            this.pnlColReserva.SuspendLayout();
            this.pnlResumen.SuspendLayout();
            this.pnlLineaResumen1.SuspendLayout();
            this.pnlLineaResumen2.SuspendLayout();
            this.pnlTotal.SuspendLayout();
            this.pnlLineaResumen3.SuspendLayout();
            this.pnlSeparadorDer.SuspendLayout();
            this.pnlDetalles.SuspendLayout();
            this.pnlLineaDetalles.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).BeginInit();
            this.SuspendLayout();
            // 
            // tblContenido
            // 
            this.tblContenido.Controls.Add(this.pnlColHuesped, 0, 0);
            this.tblContenido.Controls.Add(this.pnlColReserva, 1, 0);
            this.tblContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tblContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tblContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContenido.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.tblContenido.ColumnCount = 2;
            this.tblContenido.RowCount = 1;
            this.tblContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContenido.Padding = new System.Windows.Forms.Padding(0, 10, 0, 6);
            this.tblContenido.Size = new System.Drawing.Size(1464, 752);
            this.tblContenido.Name = "tblContenido";
            // 
            // pnlColHuesped
            // 
            this.pnlColHuesped.Controls.Add(this.pnlPago);
            this.pnlColHuesped.Controls.Add(this.pnlSeparadorCol);
            this.pnlColHuesped.Controls.Add(this.pnlIzquierda);
            this.pnlColHuesped.AutoScroll = true;
            this.pnlColHuesped.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlColHuesped.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColHuesped.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlColHuesped.Size = new System.Drawing.Size(836, 736);
            this.pnlColHuesped.Name = "pnlColHuesped";
            // 
            // pnlPago
            // 
            this.pnlPago.Controls.Add(this.lblSeccionPago);
            this.pnlPago.Controls.Add(this.pnlLineaPago);
            this.pnlPago.Controls.Add(this.lblEtiquetaMetodoPago);
            this.pnlPago.Controls.Add(this.cmbMetodoPago);
            this.pnlPago.Controls.Add(this.lblEtiquetaMontoRecibido);
            this.pnlPago.Controls.Add(this.nudMontoRecibido);
            this.pnlPago.Controls.Add(this.lblMarcaTarjetaDetectada);
            this.pnlPago.Controls.Add(this.lblVueltoEtiqueta);
            this.pnlPago.Controls.Add(this.lblVuelto);
            this.pnlPago.Controls.Add(this.lblEtiquetaNumeroTarjeta);
            this.pnlPago.Controls.Add(this.txtNumeroTarjeta);
            this.pnlPago.Controls.Add(this.lblEtiquetaTitularTarjeta);
            this.pnlPago.Controls.Add(this.txtTitularTarjeta);
            this.pnlPago.Controls.Add(this.lblEtiquetaVencimiento);
            this.pnlPago.Controls.Add(this.nudMesVencimiento);
            this.pnlPago.Controls.Add(this.lblBarraVencimiento);
            this.pnlPago.Controls.Add(this.nudAnioVencimiento);
            this.pnlPago.Controls.Add(this.lblEtiquetaCvv);
            this.pnlPago.Controls.Add(this.txtCvv);
            this.pnlPago.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlPago.CornerRadius = 14;
            this.pnlPago.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPago.Size = new System.Drawing.Size(836, 300);
            this.pnlPago.Name = "pnlPago";
            // 
            // lblSeccionPago
            // 
            this.lblSeccionPago.AutoSize = true;
            this.lblSeccionPago.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionPago.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblSeccionPago.Location = new System.Drawing.Point(24, 16);
            this.lblSeccionPago.Tag = "RegistrarReserva_lblSeccionPago";
            this.lblSeccionPago.Text = "Pago";
            this.lblSeccionPago.Name = "lblSeccionPago";
            // 
            // pnlLineaPago
            // 
            this.pnlLineaPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlLineaPago.BackColor = System.Drawing.Color.FromArgb(40, 52, 92);
            this.pnlLineaPago.Location = new System.Drawing.Point(24, 50);
            this.pnlLineaPago.Size = new System.Drawing.Size(788, 1);
            this.pnlLineaPago.Name = "pnlLineaPago";
            // 
            // lblEtiquetaMetodoPago
            // 
            this.lblEtiquetaMetodoPago.AutoSize = true;
            this.lblEtiquetaMetodoPago.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaMetodoPago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaMetodoPago.Tag = "RegistrarReserva_lblEtiquetaMetodoPago";
            this.lblEtiquetaMetodoPago.Text = "Método de pago";
            this.lblEtiquetaMetodoPago.Location = new System.Drawing.Point(26, 66);
            this.lblEtiquetaMetodoPago.Name = "lblEtiquetaMetodoPago";
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbMetodoPago.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPago.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPago.BorderWidth = 2;
            this.cmbMetodoPago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbMetodoPago.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbMetodoPago.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPago.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbMetodoPago.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.cmbMetodoPago.DropDownHighlightForeColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPago.DropDownItemHeight = 32;
            this.cmbMetodoPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMetodoPago.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbMetodoPago.IconChar = FontAwesome.Sharp.IconChar.CreditCard;
            this.cmbMetodoPago.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbMetodoPago.IconSize = 20;
            this.cmbMetodoPago.Location = new System.Drawing.Point(24, 88);
            this.cmbMetodoPago.SelectedItem = null;
            this.cmbMetodoPago.SelectedValue = null;
            this.cmbMetodoPago.Size = new System.Drawing.Size(386, 46);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            // 
            // lblEtiquetaMontoRecibido
            // 
            this.lblEtiquetaMontoRecibido.AutoSize = true;
            this.lblEtiquetaMontoRecibido.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaMontoRecibido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaMontoRecibido.Tag = "RegistrarReserva_lblEtiquetaMontoRecibido";
            this.lblEtiquetaMontoRecibido.Text = "Monto recibido";
            this.lblEtiquetaMontoRecibido.Location = new System.Drawing.Point(428, 66);
            this.lblEtiquetaMontoRecibido.Name = "lblEtiquetaMontoRecibido";
            // 
            // nudMontoRecibido
            // 
            this.nudMontoRecibido.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudMontoRecibido.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudMontoRecibido.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudMontoRecibido.BorderWidth = 2;
            this.nudMontoRecibido.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMontoRecibido.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudMontoRecibido.IconChar = FontAwesome.Sharp.IconChar.DollarSign;
            this.nudMontoRecibido.IconColor = System.Drawing.Color.Goldenrod;
            this.nudMontoRecibido.IconPadding = 4;
            this.nudMontoRecibido.IconSize = 20;
            this.nudMontoRecibido.Location = new System.Drawing.Point(426, 88);
            this.nudMontoRecibido.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            this.nudMontoRecibido.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudMontoRecibido.Size = new System.Drawing.Size(386, 46);
            this.nudMontoRecibido.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudMontoRecibido.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudMontoRecibido.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudMontoRecibido.Value = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudMontoRecibido.DecimalPlaces = 2;
            this.nudMontoRecibido.ThousandsSeparator = true;
            this.nudMontoRecibido.Name = "nudMontoRecibido";
            // 
            // lblMarcaTarjetaDetectada
            // 
            this.lblMarcaTarjetaDetectada.AutoSize = true;
            this.lblMarcaTarjetaDetectada.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarcaTarjetaDetectada.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblMarcaTarjetaDetectada.Location = new System.Drawing.Point(428, 102);
            this.lblMarcaTarjetaDetectada.Text = "Marca detectada: -";
            this.lblMarcaTarjetaDetectada.Visible = false;
            this.lblMarcaTarjetaDetectada.Name = "lblMarcaTarjetaDetectada";
            // 
            // lblVueltoEtiqueta
            // 
            this.lblVueltoEtiqueta.AutoSize = true;
            this.lblVueltoEtiqueta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVueltoEtiqueta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblVueltoEtiqueta.Location = new System.Drawing.Point(428, 146);
            this.lblVueltoEtiqueta.Tag = "RegistrarReserva_lblVueltoEtiqueta";
            this.lblVueltoEtiqueta.Text = "Vuelto";
            this.lblVueltoEtiqueta.Name = "lblVueltoEtiqueta";
            // 
            // lblVuelto
            // 
            this.lblVuelto.AutoSize = true;
            this.lblVuelto.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVuelto.ForeColor = System.Drawing.Color.FromArgb(120, 220, 140);
            this.lblVuelto.Location = new System.Drawing.Point(426, 164);
            this.lblVuelto.Text = "$0,00";
            this.lblVuelto.Name = "lblVuelto";
            // 
            // lblEtiquetaNumeroTarjeta
            // 
            this.lblEtiquetaNumeroTarjeta.AutoSize = true;
            this.lblEtiquetaNumeroTarjeta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNumeroTarjeta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaNumeroTarjeta.Tag = "RegistrarReserva_lblEtiquetaNumeroTarjeta";
            this.lblEtiquetaNumeroTarjeta.Text = "Número de tarjeta";
            this.lblEtiquetaNumeroTarjeta.Location = new System.Drawing.Point(26, 146);
            this.lblEtiquetaNumeroTarjeta.Visible = false;
            this.lblEtiquetaNumeroTarjeta.Name = "lblEtiquetaNumeroTarjeta";
            // 
            // txtNumeroTarjeta
            // 
            this.txtNumeroTarjeta.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNumeroTarjeta.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNumeroTarjeta.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNumeroTarjeta.BorderWidth = 2;
            this.txtNumeroTarjeta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroTarjeta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumeroTarjeta.IconChar = FontAwesome.Sharp.IconChar.CreditCard;
            this.txtNumeroTarjeta.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNumeroTarjeta.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNumeroTarjeta.IconPadding = 4;
            this.txtNumeroTarjeta.IconSize = 20;
            this.txtNumeroTarjeta.MaxLength = 19;
            this.txtNumeroTarjeta.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNumeroTarjeta.PlaceholderText = "0000 0000 0000 0000";
            this.txtNumeroTarjeta.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumeroTarjeta.Size = new System.Drawing.Size(386, 46);
            this.txtNumeroTarjeta.Location = new System.Drawing.Point(24, 168);
            this.txtNumeroTarjeta.Visible = false;
            this.txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            // 
            // lblEtiquetaTitularTarjeta
            // 
            this.lblEtiquetaTitularTarjeta.AutoSize = true;
            this.lblEtiquetaTitularTarjeta.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTitularTarjeta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTitularTarjeta.Tag = "RegistrarReserva_lblEtiquetaTitularTarjeta";
            this.lblEtiquetaTitularTarjeta.Text = "Titular de la tarjeta";
            this.lblEtiquetaTitularTarjeta.Location = new System.Drawing.Point(428, 146);
            this.lblEtiquetaTitularTarjeta.Visible = false;
            this.lblEtiquetaTitularTarjeta.Name = "lblEtiquetaTitularTarjeta";
            // 
            // txtTitularTarjeta
            // 
            this.txtTitularTarjeta.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtTitularTarjeta.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtTitularTarjeta.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtTitularTarjeta.BorderWidth = 2;
            this.txtTitularTarjeta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitularTarjeta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTitularTarjeta.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtTitularTarjeta.IconColor = System.Drawing.Color.Goldenrod;
            this.txtTitularTarjeta.IconColorRight = System.Drawing.Color.DimGray;
            this.txtTitularTarjeta.IconPadding = 4;
            this.txtTitularTarjeta.IconSize = 20;
            this.txtTitularTarjeta.MaxLength = 100;
            this.txtTitularTarjeta.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtTitularTarjeta.PlaceholderText = "";
            this.txtTitularTarjeta.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTitularTarjeta.Size = new System.Drawing.Size(386, 46);
            this.txtTitularTarjeta.Location = new System.Drawing.Point(426, 168);
            this.txtTitularTarjeta.Visible = false;
            this.txtTitularTarjeta.Name = "txtTitularTarjeta";
            // 
            // lblEtiquetaVencimiento
            // 
            this.lblEtiquetaVencimiento.AutoSize = true;
            this.lblEtiquetaVencimiento.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaVencimiento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaVencimiento.Tag = "RegistrarReserva_lblEtiquetaVencimiento";
            this.lblEtiquetaVencimiento.Text = "Vencimiento (MM/AA)";
            this.lblEtiquetaVencimiento.Location = new System.Drawing.Point(26, 226);
            this.lblEtiquetaVencimiento.Visible = false;
            this.lblEtiquetaVencimiento.Name = "lblEtiquetaVencimiento";
            // 
            // nudMesVencimiento
            // 
            this.nudMesVencimiento.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudMesVencimiento.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudMesVencimiento.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudMesVencimiento.BorderWidth = 2;
            this.nudMesVencimiento.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMesVencimiento.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudMesVencimiento.IconChar = FontAwesome.Sharp.IconChar.None;
            this.nudMesVencimiento.IconColor = System.Drawing.Color.Goldenrod;
            this.nudMesVencimiento.IconPadding = 4;
            this.nudMesVencimiento.IconSize = 20;
            this.nudMesVencimiento.Location = new System.Drawing.Point(24, 248);
            this.nudMesVencimiento.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            this.nudMesVencimiento.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudMesVencimiento.Size = new System.Drawing.Size(92, 46);
            this.nudMesVencimiento.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudMesVencimiento.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudMesVencimiento.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudMesVencimiento.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudMesVencimiento.Visible = false;
            this.nudMesVencimiento.Name = "nudMesVencimiento";
            // 
            // lblBarraVencimiento
            // 
            this.lblBarraVencimiento.AutoSize = true;
            this.lblBarraVencimiento.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarraVencimiento.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblBarraVencimiento.Location = new System.Drawing.Point(122, 254);
            this.lblBarraVencimiento.Text = "/";
            this.lblBarraVencimiento.Visible = false;
            this.lblBarraVencimiento.Name = "lblBarraVencimiento";
            // 
            // nudAnioVencimiento
            // 
            this.nudAnioVencimiento.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudAnioVencimiento.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudAnioVencimiento.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudAnioVencimiento.BorderWidth = 2;
            this.nudAnioVencimiento.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAnioVencimiento.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudAnioVencimiento.IconChar = FontAwesome.Sharp.IconChar.None;
            this.nudAnioVencimiento.IconColor = System.Drawing.Color.Goldenrod;
            this.nudAnioVencimiento.IconPadding = 4;
            this.nudAnioVencimiento.IconSize = 20;
            this.nudAnioVencimiento.Location = new System.Drawing.Point(142, 248);
            this.nudAnioVencimiento.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            this.nudAnioVencimiento.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudAnioVencimiento.Size = new System.Drawing.Size(92, 46);
            this.nudAnioVencimiento.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudAnioVencimiento.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudAnioVencimiento.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudAnioVencimiento.Value = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudAnioVencimiento.Visible = false;
            this.nudAnioVencimiento.Name = "nudAnioVencimiento";
            // 
            // lblEtiquetaCvv
            // 
            this.lblEtiquetaCvv.AutoSize = true;
            this.lblEtiquetaCvv.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaCvv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaCvv.Tag = "RegistrarReserva_lblEtiquetaCvv";
            this.lblEtiquetaCvv.Text = "CVV";
            this.lblEtiquetaCvv.Location = new System.Drawing.Point(428, 226);
            this.lblEtiquetaCvv.Visible = false;
            this.lblEtiquetaCvv.Name = "lblEtiquetaCvv";
            // 
            // txtCvv
            // 
            this.txtCvv.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtCvv.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtCvv.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtCvv.BorderWidth = 2;
            this.txtCvv.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCvv.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCvv.IconChar = FontAwesome.Sharp.IconChar.Lock;
            this.txtCvv.IconColor = System.Drawing.Color.Goldenrod;
            this.txtCvv.IconColorRight = System.Drawing.Color.DimGray;
            this.txtCvv.IconPadding = 4;
            this.txtCvv.IconSize = 20;
            this.txtCvv.MaxLength = 4;
            this.txtCvv.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtCvv.PlaceholderText = "123";
            this.txtCvv.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCvv.Size = new System.Drawing.Size(150, 46);
            this.txtCvv.Location = new System.Drawing.Point(426, 248);
            this.txtCvv.Visible = false;
            this.txtCvv.Name = "txtCvv";
            // 
            // pnlSeparadorCol
            // 
            this.pnlSeparadorCol.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlSeparadorCol.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparadorCol.Size = new System.Drawing.Size(836, 16);
            this.pnlSeparadorCol.Name = "pnlSeparadorCol";
            // 
            // pnlIzquierda
            // 
            this.pnlIzquierda.Controls.Add(this.lblSeccionHuesped);
            this.pnlIzquierda.Controls.Add(this.pnlLineaHuesped1);
            this.pnlIzquierda.Controls.Add(this.lblEtiquetaTipoDocumento);
            this.pnlIzquierda.Controls.Add(this.cmbTipoDocumento);
            this.pnlIzquierda.Controls.Add(this.lblEtiquetaDni);
            this.pnlIzquierda.Controls.Add(this.txtDni);
            this.pnlIzquierda.Controls.Add(this.btnConsultarDocumento);
            this.pnlIzquierda.Controls.Add(this.btnHuespedNuevo);
            this.pnlIzquierda.Controls.Add(this.lblEtiquetaNombre);
            this.pnlIzquierda.Controls.Add(this.txtNombre);
            this.pnlIzquierda.Controls.Add(this.lblEtiquetaApellido);
            this.pnlIzquierda.Controls.Add(this.txtApellido);
            this.pnlIzquierda.Controls.Add(this.lblEtiquetaFechaNacimiento);
            this.pnlIzquierda.Controls.Add(this.dtpFechaNacimiento);
            this.pnlIzquierda.Controls.Add(this.lblEtiquetaNacionalidad);
            this.pnlIzquierda.Controls.Add(this.txtNacionalidad);
            this.pnlIzquierda.Controls.Add(this.lblEtiquetaTelefono);
            this.pnlIzquierda.Controls.Add(this.txtTelefono);
            this.pnlIzquierda.Controls.Add(this.lblEtiquetaEmail);
            this.pnlIzquierda.Controls.Add(this.txtEmail);
            this.pnlIzquierda.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlIzquierda.CornerRadius = 14;
            this.pnlIzquierda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlIzquierda.Size = new System.Drawing.Size(836, 400);
            this.pnlIzquierda.Name = "pnlIzquierda";
            // 
            // lblSeccionHuesped
            // 
            this.lblSeccionHuesped.AutoSize = true;
            this.lblSeccionHuesped.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionHuesped.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblSeccionHuesped.Location = new System.Drawing.Point(24, 16);
            this.lblSeccionHuesped.Tag = "RegistrarReserva_lblSeccionHuesped";
            this.lblSeccionHuesped.Text = "Titular de la reserva";
            this.lblSeccionHuesped.Name = "lblSeccionHuesped";
            // 
            // pnlLineaHuesped1
            // 
            this.pnlLineaHuesped1.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlLineaHuesped1.BackColor = System.Drawing.Color.FromArgb(40, 52, 92);
            this.pnlLineaHuesped1.Location = new System.Drawing.Point(24, 50);
            this.pnlLineaHuesped1.Size = new System.Drawing.Size(788, 1);
            this.pnlLineaHuesped1.Name = "pnlLineaHuesped1";
            // 
            // lblEtiquetaTipoDocumento
            // 
            this.lblEtiquetaTipoDocumento.AutoSize = true;
            this.lblEtiquetaTipoDocumento.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTipoDocumento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTipoDocumento.Tag = "RegistrarReserva_lblEtiquetaTipoDocumento";
            this.lblEtiquetaTipoDocumento.Text = "Tipo de documento";
            this.lblEtiquetaTipoDocumento.Location = new System.Drawing.Point(26, 66);
            this.lblEtiquetaTipoDocumento.Name = "lblEtiquetaTipoDocumento";
            // 
            // cmbTipoDocumento
            // 
            this.cmbTipoDocumento.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipoDocumento.BorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.BorderWidth = 2;
            this.cmbTipoDocumento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbTipoDocumento.DropDownBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.cmbTipoDocumento.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocumento.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.cmbTipoDocumento.DropDownHighlightForeColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.DropDownItemHeight = 32;
            this.cmbTipoDocumento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoDocumento.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbTipoDocumento.IconChar = FontAwesome.Sharp.IconChar.IdCard;
            this.cmbTipoDocumento.IconColor = System.Drawing.Color.Goldenrod;
            this.cmbTipoDocumento.IconSize = 20;
            this.cmbTipoDocumento.Location = new System.Drawing.Point(24, 88);
            this.cmbTipoDocumento.SelectedItem = null;
            this.cmbTipoDocumento.SelectedValue = null;
            this.cmbTipoDocumento.Size = new System.Drawing.Size(190, 46);
            this.cmbTipoDocumento.Name = "cmbTipoDocumento";
            // 
            // lblEtiquetaDni
            // 
            this.lblEtiquetaDni.AutoSize = true;
            this.lblEtiquetaDni.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaDni.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaDni.Tag = "RegistrarReserva_lblEtiquetaDni";
            this.lblEtiquetaDni.Text = "N° de documento";
            this.lblEtiquetaDni.Location = new System.Drawing.Point(228, 66);
            this.lblEtiquetaDni.Name = "lblEtiquetaDni";
            // 
            // txtDni
            // 
            this.txtDni.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtDni.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtDni.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtDni.BorderWidth = 2;
            this.txtDni.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDni.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDni.IconChar = FontAwesome.Sharp.IconChar.Hashtag;
            this.txtDni.IconColor = System.Drawing.Color.Goldenrod;
            this.txtDni.IconColorRight = System.Drawing.Color.DimGray;
            this.txtDni.IconPadding = 4;
            this.txtDni.IconSize = 20;
            this.txtDni.MaxLength = 20;
            this.txtDni.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtDni.PlaceholderText = "N° Documento";
            this.txtDni.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDni.Size = new System.Drawing.Size(338, 46);
            this.txtDni.Location = new System.Drawing.Point(228, 88);
            this.txtDni.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtDni.Name = "txtDni";
            // 
            // btnConsultarDocumento
            // 
            this.btnConsultarDocumento.BackColor = System.Drawing.Color.Goldenrod;
            this.btnConsultarDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarDocumento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultarDocumento.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnConsultarDocumento.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnConsultarDocumento.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnConsultarDocumento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConsultarDocumento.IconSize = 22;
            this.btnConsultarDocumento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultarDocumento.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnConsultarDocumento.Size = new System.Drawing.Size(176, 46);
            this.btnConsultarDocumento.Tag = "RegistrarReserva_btnConsultarDocumento";
            this.btnConsultarDocumento.Text = "Consultar";
            this.btnConsultarDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConsultarDocumento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConsultarDocumento.UseVisualStyleBackColor = false;
            this.btnConsultarDocumento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarDocumento.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnConsultarDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnConsultarDocumento.Location = new System.Drawing.Point(578, 88);
            this.btnConsultarDocumento.FlatAppearance.BorderSize = 0;
            this.btnConsultarDocumento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnConsultarDocumento.Name = "btnConsultarDocumento";
            // 
            // btnHuespedNuevo
            // 
            this.btnHuespedNuevo.Click += new System.EventHandler(this.BtnHuespedNuevo_Click);
            this.btnHuespedNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnHuespedNuevo.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnHuespedNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuespedNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuespedNuevo.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnHuespedNuevo.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.btnHuespedNuevo.IconColor = System.Drawing.Color.Gold;
            this.btnHuespedNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHuespedNuevo.IconSize = 22;
            this.btnHuespedNuevo.Location = new System.Drawing.Point(762, 88);
            this.btnHuespedNuevo.Size = new System.Drawing.Size(50, 46);
            this.btnHuespedNuevo.UseVisualStyleBackColor = false;
            this.btnHuespedNuevo.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnHuespedNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.btnHuespedNuevo.Name = "btnHuespedNuevo";
            // 
            // lblEtiquetaNombre
            // 
            this.lblEtiquetaNombre.AutoSize = true;
            this.lblEtiquetaNombre.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaNombre.Tag = "RegistrarReserva_lblEtiquetaNombre";
            this.lblEtiquetaNombre.Text = "Nombre";
            this.lblEtiquetaNombre.Location = new System.Drawing.Point(26, 150);
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
            this.txtNombre.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtNombre.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNombre.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombre.IconPadding = 4;
            this.txtNombre.IconSize = 20;
            this.txtNombre.MaxLength = 100;
            this.txtNombre.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNombre.PlaceholderText = "";
            this.txtNombre.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombre.Size = new System.Drawing.Size(386, 46);
            this.txtNombre.Location = new System.Drawing.Point(24, 172);
            this.txtNombre.ReadOnly = true;
            this.txtNombre.TabStop = false;
            this.txtNombre.Name = "txtNombre";
            // 
            // lblEtiquetaApellido
            // 
            this.lblEtiquetaApellido.AutoSize = true;
            this.lblEtiquetaApellido.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaApellido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaApellido.Tag = "RegistrarReserva_lblEtiquetaApellido";
            this.lblEtiquetaApellido.Text = "Apellido";
            this.lblEtiquetaApellido.Location = new System.Drawing.Point(428, 150);
            this.lblEtiquetaApellido.Name = "lblEtiquetaApellido";
            // 
            // txtApellido
            // 
            this.txtApellido.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtApellido.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtApellido.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtApellido.BorderWidth = 2;
            this.txtApellido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellido.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtApellido.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtApellido.IconColor = System.Drawing.Color.Goldenrod;
            this.txtApellido.IconColorRight = System.Drawing.Color.DimGray;
            this.txtApellido.IconPadding = 4;
            this.txtApellido.IconSize = 20;
            this.txtApellido.MaxLength = 100;
            this.txtApellido.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtApellido.PlaceholderText = "";
            this.txtApellido.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtApellido.Size = new System.Drawing.Size(386, 46);
            this.txtApellido.Location = new System.Drawing.Point(426, 172);
            this.txtApellido.ReadOnly = true;
            this.txtApellido.TabStop = false;
            this.txtApellido.Name = "txtApellido";
            // 
            // lblEtiquetaFechaNacimiento
            // 
            this.lblEtiquetaFechaNacimiento.AutoSize = true;
            this.lblEtiquetaFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaFechaNacimiento.Tag = "RegistrarReserva_lblEtiquetaFechaNacimiento";
            this.lblEtiquetaFechaNacimiento.Text = "Fecha de nacimiento";
            this.lblEtiquetaFechaNacimiento.Location = new System.Drawing.Point(26, 230);
            this.lblEtiquetaFechaNacimiento.Name = "lblEtiquetaFechaNacimiento";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpFechaNacimiento.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaNacimiento.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaNacimiento.BorderWidth = 2;
            this.dtpFechaNacimiento.CalendarBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpFechaNacimiento.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaNacimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaNacimiento.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaNacimiento.IconChar = FontAwesome.Sharp.IconChar.Calendar;
            this.dtpFechaNacimiento.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaNacimiento.IconSize = 20;
            this.dtpFechaNacimiento.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaNacimiento.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(386, 46);
            this.dtpFechaNacimiento.Value = null;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(24, 252);
            this.dtpFechaNacimiento.Enabled = false;
            this.dtpFechaNacimiento.Checked = false;
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            // 
            // lblEtiquetaNacionalidad
            // 
            this.lblEtiquetaNacionalidad.AutoSize = true;
            this.lblEtiquetaNacionalidad.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNacionalidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaNacionalidad.Tag = "RegistrarReserva_lblEtiquetaNacionalidad";
            this.lblEtiquetaNacionalidad.Text = "Nacionalidad";
            this.lblEtiquetaNacionalidad.Location = new System.Drawing.Point(428, 230);
            this.lblEtiquetaNacionalidad.Name = "lblEtiquetaNacionalidad";
            // 
            // txtNacionalidad
            // 
            this.txtNacionalidad.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtNacionalidad.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtNacionalidad.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtNacionalidad.BorderWidth = 2;
            this.txtNacionalidad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNacionalidad.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNacionalidad.IconChar = FontAwesome.Sharp.IconChar.Flag;
            this.txtNacionalidad.IconColor = System.Drawing.Color.Goldenrod;
            this.txtNacionalidad.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNacionalidad.IconPadding = 4;
            this.txtNacionalidad.IconSize = 20;
            this.txtNacionalidad.MaxLength = 100;
            this.txtNacionalidad.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtNacionalidad.PlaceholderText = "";
            this.txtNacionalidad.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNacionalidad.Size = new System.Drawing.Size(386, 46);
            this.txtNacionalidad.Location = new System.Drawing.Point(426, 252);
            this.txtNacionalidad.ReadOnly = true;
            this.txtNacionalidad.TabStop = false;
            this.txtNacionalidad.Name = "txtNacionalidad";
            // 
            // lblEtiquetaTelefono
            // 
            this.lblEtiquetaTelefono.AutoSize = true;
            this.lblEtiquetaTelefono.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaTelefono.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaTelefono.Tag = "RegistrarReserva_lblEtiquetaTelefono";
            this.lblEtiquetaTelefono.Text = "Teléfono";
            this.lblEtiquetaTelefono.Location = new System.Drawing.Point(26, 310);
            this.lblEtiquetaTelefono.Name = "lblEtiquetaTelefono";
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtTelefono.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtTelefono.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtTelefono.BorderWidth = 2;
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTelefono.IconChar = FontAwesome.Sharp.IconChar.Phone;
            this.txtTelefono.IconColor = System.Drawing.Color.Goldenrod;
            this.txtTelefono.IconColorRight = System.Drawing.Color.DimGray;
            this.txtTelefono.IconPadding = 4;
            this.txtTelefono.IconSize = 20;
            this.txtTelefono.MaxLength = 100;
            this.txtTelefono.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtTelefono.PlaceholderText = "";
            this.txtTelefono.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTelefono.Size = new System.Drawing.Size(386, 46);
            this.txtTelefono.Location = new System.Drawing.Point(24, 332);
            this.txtTelefono.ReadOnly = true;
            this.txtTelefono.TabStop = false;
            this.txtTelefono.Name = "txtTelefono";
            // 
            // lblEtiquetaEmail
            // 
            this.lblEtiquetaEmail.AutoSize = true;
            this.lblEtiquetaEmail.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaEmail.Tag = "RegistrarReserva_lblEtiquetaEmail";
            this.lblEtiquetaEmail.Text = "Email";
            this.lblEtiquetaEmail.Location = new System.Drawing.Point(428, 310);
            this.lblEtiquetaEmail.Name = "lblEtiquetaEmail";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.txtEmail.BorderColor = System.Drawing.Color.Goldenrod;
            this.txtEmail.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtEmail.BorderWidth = 2;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEmail.IconChar = FontAwesome.Sharp.IconChar.Envelope;
            this.txtEmail.IconColor = System.Drawing.Color.Goldenrod;
            this.txtEmail.IconColorRight = System.Drawing.Color.DimGray;
            this.txtEmail.IconPadding = 4;
            this.txtEmail.IconSize = 20;
            this.txtEmail.MaxLength = 100;
            this.txtEmail.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtEmail.PlaceholderText = "";
            this.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmail.Size = new System.Drawing.Size(386, 46);
            this.txtEmail.Location = new System.Drawing.Point(426, 332);
            this.txtEmail.ReadOnly = true;
            this.txtEmail.TabStop = false;
            this.txtEmail.Name = "txtEmail";
            // 
            // pnlColReserva
            // 
            this.pnlColReserva.Controls.Add(this.pnlResumen);
            this.pnlColReserva.Controls.Add(this.pnlSeparadorDer);
            this.pnlColReserva.Controls.Add(this.pnlDetalles);
            this.pnlColReserva.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlColReserva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColReserva.Margin = new System.Windows.Forms.Padding(0);
            this.pnlColReserva.Size = new System.Drawing.Size(612, 736);
            this.pnlColReserva.Name = "pnlColReserva";
            // 
            // pnlResumen
            // 
            this.pnlResumen.Controls.Add(this.pnlLineaResumen1);
            this.pnlResumen.Controls.Add(this.lblSeccionResumen);
            this.pnlResumen.Controls.Add(this.lblEtiquetaFechaIngreso);
            this.pnlResumen.Controls.Add(this.dtpFechaIngreso);
            this.pnlResumen.Controls.Add(this.lblEtiquetaFechaEgreso);
            this.pnlResumen.Controls.Add(this.dtpFechaEgreso);
            this.pnlResumen.Controls.Add(this.lblResEtiquetaNoches);
            this.pnlResumen.Controls.Add(this.lblResNoches);
            this.pnlResumen.Controls.Add(this.pnlLineaResumen2);
            this.pnlResumen.Controls.Add(this.lblSeccionComposicion);
            this.pnlResumen.Controls.Add(this.lblEtiquetaAdultos);
            this.pnlResumen.Controls.Add(this.nudAdultos);
            this.pnlResumen.Controls.Add(this.lblEtiquetaNinos);
            this.pnlResumen.Controls.Add(this.nudNinos);
            this.pnlResumen.Controls.Add(this.lblComposicionAdvertencia);
            this.pnlResumen.Controls.Add(this.pnlTotal);
            this.pnlResumen.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlResumen.CornerRadius = 14;
            this.pnlResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResumen.Size = new System.Drawing.Size(612, 520);
            this.pnlResumen.Name = "pnlResumen";
            // 
            // pnlLineaResumen1
            // 
            this.pnlLineaResumen1.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlLineaResumen1.BackColor = System.Drawing.Color.FromArgb(40, 52, 92);
            this.pnlLineaResumen1.Location = new System.Drawing.Point(24, 50);
            this.pnlLineaResumen1.Size = new System.Drawing.Size(564, 1);
            this.pnlLineaResumen1.Name = "pnlLineaResumen1";
            // 
            // lblSeccionResumen
            // 
            this.lblSeccionResumen.AutoSize = true;
            this.lblSeccionResumen.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionResumen.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblSeccionResumen.Location = new System.Drawing.Point(24, 16);
            this.lblSeccionResumen.Tag = "RegistrarReserva_lblSeccionResumen";
            this.lblSeccionResumen.Text = "Estadía";
            this.lblSeccionResumen.Name = "lblSeccionResumen";
            // 
            // lblEtiquetaFechaIngreso
            // 
            this.lblEtiquetaFechaIngreso.AutoSize = true;
            this.lblEtiquetaFechaIngreso.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaFechaIngreso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaFechaIngreso.Tag = "RegistrarReserva_lblEtiquetaFechaIngreso";
            this.lblEtiquetaFechaIngreso.Text = "Llegada";
            this.lblEtiquetaFechaIngreso.Location = new System.Drawing.Point(26, 64);
            this.lblEtiquetaFechaIngreso.Name = "lblEtiquetaFechaIngreso";
            // 
            // dtpFechaIngreso
            // 
            this.dtpFechaIngreso.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpFechaIngreso.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaIngreso.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaIngreso.BorderWidth = 2;
            this.dtpFechaIngreso.CalendarBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpFechaIngreso.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaIngreso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaIngreso.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIngreso.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaIngreso.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpFechaIngreso.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaIngreso.IconSize = 20;
            this.dtpFechaIngreso.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaIngreso.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaIngreso.Size = new System.Drawing.Size(274, 46);
            this.dtpFechaIngreso.Value = null;
            this.dtpFechaIngreso.Location = new System.Drawing.Point(24, 86);
            this.dtpFechaIngreso.Name = "dtpFechaIngreso";
            // 
            // lblEtiquetaFechaEgreso
            // 
            this.lblEtiquetaFechaEgreso.AutoSize = true;
            this.lblEtiquetaFechaEgreso.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaFechaEgreso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaFechaEgreso.Tag = "RegistrarReserva_lblEtiquetaFechaEgreso";
            this.lblEtiquetaFechaEgreso.Text = "Salida";
            this.lblEtiquetaFechaEgreso.Location = new System.Drawing.Point(316, 64);
            this.lblEtiquetaFechaEgreso.Name = "lblEtiquetaFechaEgreso";
            // 
            // dtpFechaEgreso
            // 
            this.dtpFechaEgreso.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpFechaEgreso.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaEgreso.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaEgreso.BorderWidth = 2;
            this.dtpFechaEgreso.CalendarBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpFechaEgreso.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaEgreso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaEgreso.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaEgreso.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaEgreso.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpFechaEgreso.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaEgreso.IconSize = 20;
            this.dtpFechaEgreso.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaEgreso.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaEgreso.Size = new System.Drawing.Size(274, 46);
            this.dtpFechaEgreso.Value = null;
            this.dtpFechaEgreso.Location = new System.Drawing.Point(314, 86);
            this.dtpFechaEgreso.Name = "dtpFechaEgreso";
            // 
            // lblResEtiquetaNoches
            // 
            this.lblResEtiquetaNoches.AutoSize = true;
            this.lblResEtiquetaNoches.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResEtiquetaNoches.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblResEtiquetaNoches.Location = new System.Drawing.Point(26, 142);
            this.lblResEtiquetaNoches.Tag = "RegistrarReserva_lblResEtiquetaNoches";
            this.lblResEtiquetaNoches.Text = "Noches:";
            this.lblResEtiquetaNoches.Name = "lblResEtiquetaNoches";
            // 
            // lblResNoches
            // 
            this.lblResNoches.AutoSize = true;
            this.lblResNoches.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResNoches.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblResNoches.Location = new System.Drawing.Point(90, 140);
            this.lblResNoches.Text = "-";
            this.lblResNoches.Name = "lblResNoches";
            // 
            // pnlLineaResumen2
            // 
            this.pnlLineaResumen2.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlLineaResumen2.BackColor = System.Drawing.Color.FromArgb(40, 52, 92);
            this.pnlLineaResumen2.Location = new System.Drawing.Point(24, 172);
            this.pnlLineaResumen2.Size = new System.Drawing.Size(564, 1);
            this.pnlLineaResumen2.Name = "pnlLineaResumen2";
            // 
            // lblSeccionComposicion
            // 
            this.lblSeccionComposicion.AutoSize = true;
            this.lblSeccionComposicion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionComposicion.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblSeccionComposicion.Location = new System.Drawing.Point(24, 184);
            this.lblSeccionComposicion.Tag = "RegistrarReserva_lblSeccionComposicion";
            this.lblSeccionComposicion.Text = "Huéspedes";
            this.lblSeccionComposicion.Name = "lblSeccionComposicion";
            // 
            // lblEtiquetaAdultos
            // 
            this.lblEtiquetaAdultos.AutoSize = true;
            this.lblEtiquetaAdultos.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaAdultos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaAdultos.Tag = "RegistrarReserva_lblEtiquetaAdultos";
            this.lblEtiquetaAdultos.Text = "Adultos";
            this.lblEtiquetaAdultos.Location = new System.Drawing.Point(26, 214);
            this.lblEtiquetaAdultos.Name = "lblEtiquetaAdultos";
            // 
            // nudAdultos
            // 
            this.nudAdultos.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudAdultos.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.BorderWidth = 2;
            this.nudAdultos.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAdultos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudAdultos.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.nudAdultos.IconColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.IconPadding = 4;
            this.nudAdultos.IconSize = 20;
            this.nudAdultos.Location = new System.Drawing.Point(24, 236);
            this.nudAdultos.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudAdultos.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudAdultos.Size = new System.Drawing.Size(274, 46);
            this.nudAdultos.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudAdultos.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudAdultos.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudAdultos.Name = "nudAdultos";
            // 
            // lblEtiquetaNinos
            // 
            this.lblEtiquetaNinos.AutoSize = true;
            this.lblEtiquetaNinos.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaNinos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaNinos.Tag = "RegistrarReserva_lblEtiquetaNinos";
            this.lblEtiquetaNinos.Text = "Niños";
            this.lblEtiquetaNinos.Location = new System.Drawing.Point(316, 214);
            this.lblEtiquetaNinos.Name = "lblEtiquetaNinos";
            // 
            // nudNinos
            // 
            this.nudNinos.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudNinos.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.BorderWidth = 2;
            this.nudNinos.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudNinos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudNinos.IconChar = FontAwesome.Sharp.IconChar.Child;
            this.nudNinos.IconColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.IconPadding = 4;
            this.nudNinos.IconSize = 20;
            this.nudNinos.Location = new System.Drawing.Point(314, 236);
            this.nudNinos.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudNinos.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudNinos.Size = new System.Drawing.Size(274, 46);
            this.nudNinos.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudNinos.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudNinos.Value = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudNinos.Name = "nudNinos";
            // 
            // lblComposicionAdvertencia
            // 
            this.lblComposicionAdvertencia.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblComposicionAdvertencia.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComposicionAdvertencia.ForeColor = System.Drawing.Color.FromArgb(240, 120, 110);
            this.lblComposicionAdvertencia.Location = new System.Drawing.Point(24, 290);
            this.lblComposicionAdvertencia.Size = new System.Drawing.Size(564, 40);
            this.lblComposicionAdvertencia.Text = "-";
            this.lblComposicionAdvertencia.Visible = false;
            this.lblComposicionAdvertencia.Name = "lblComposicionAdvertencia";
            // 
            // pnlTotal
            // 
            this.pnlTotal.Controls.Add(this.pnlLineaResumen3);
            this.pnlTotal.Controls.Add(this.lblResMontoTotal);
            this.pnlTotal.Controls.Add(this.lblResEtiquetaMontoTotal);
            this.pnlTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlTotal.BackColor = System.Drawing.Color.FromArgb(14, 24, 62);
            this.pnlTotal.CornerRadius = 12;
            this.pnlTotal.Location = new System.Drawing.Point(24, 400);
            this.pnlTotal.Size = new System.Drawing.Size(564, 96);
            this.pnlTotal.Name = "pnlTotal";
            // 
            // pnlLineaResumen3
            // 
            this.pnlLineaResumen3.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLineaResumen3.Location = new System.Drawing.Point(0, 18);
            this.pnlLineaResumen3.Size = new System.Drawing.Size(4, 60);
            this.pnlLineaResumen3.Name = "pnlLineaResumen3";
            // 
            // lblResMontoTotal
            // 
            this.lblResMontoTotal.AutoSize = true;
            this.lblResMontoTotal.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResMontoTotal.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblResMontoTotal.Location = new System.Drawing.Point(18, 36);
            this.lblResMontoTotal.Text = "$0";
            this.lblResMontoTotal.Name = "lblResMontoTotal";
            // 
            // lblResEtiquetaMontoTotal
            // 
            this.lblResEtiquetaMontoTotal.AutoSize = true;
            this.lblResEtiquetaMontoTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResEtiquetaMontoTotal.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblResEtiquetaMontoTotal.Location = new System.Drawing.Point(20, 14);
            this.lblResEtiquetaMontoTotal.Tag = "RegistrarReserva_lblTotalCobrar";
            this.lblResEtiquetaMontoTotal.Text = "TOTAL A COBRAR";
            this.lblResEtiquetaMontoTotal.Name = "lblResEtiquetaMontoTotal";
            // 
            // pnlSeparadorDer
            // 
            this.pnlSeparadorDer.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlSeparadorDer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparadorDer.Size = new System.Drawing.Size(612, 16);
            this.pnlSeparadorDer.Name = "pnlSeparadorDer";
            // 
            // pnlDetalles
            // 
            this.pnlDetalles.Controls.Add(this.lblResEtiquetaHabitacion);
            this.pnlDetalles.Controls.Add(this.lblResHabitacion);
            this.pnlDetalles.Controls.Add(this.lblSeccionDetalles);
            this.pnlDetalles.Controls.Add(this.lblDetTipo);
            this.pnlDetalles.Controls.Add(this.lblDetDescripcion);
            this.pnlDetalles.Controls.Add(this.pnlLineaDetalles);
            this.pnlDetalles.Controls.Add(this.lblDetEtiquetaPiso);
            this.pnlDetalles.Controls.Add(this.lblDetPiso);
            this.pnlDetalles.Controls.Add(this.lblDetEtiquetaCapacidad);
            this.pnlDetalles.Controls.Add(this.lblDetCapacidad);
            this.pnlDetalles.Controls.Add(this.lblDetEtiquetaPrecio);
            this.pnlDetalles.Controls.Add(this.lblDetPrecio);
            this.pnlDetalles.Controls.Add(this.lblDetEtiquetaTipo);
            this.pnlDetalles.Controls.Add(this.lblDetEtiquetaDescripcion);
            this.pnlDetalles.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlDetalles.CornerRadius = 14;
            this.pnlDetalles.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetalles.Size = new System.Drawing.Size(612, 200);
            this.pnlDetalles.Name = "pnlDetalles";
            // 
            // lblResEtiquetaHabitacion
            // 
            this.lblResEtiquetaHabitacion.AutoSize = true;
            this.lblResEtiquetaHabitacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResEtiquetaHabitacion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblResEtiquetaHabitacion.Location = new System.Drawing.Point(26, 18);
            this.lblResEtiquetaHabitacion.Tag = "RegistrarReserva_lblResEtiquetaHabitacion";
            this.lblResEtiquetaHabitacion.Text = "HABITACIÓN";
            this.lblResEtiquetaHabitacion.Name = "lblResEtiquetaHabitacion";
            // 
            // lblResHabitacion
            // 
            this.lblResHabitacion.AutoSize = true;
            this.lblResHabitacion.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResHabitacion.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblResHabitacion.Location = new System.Drawing.Point(20, 38);
            this.lblResHabitacion.Text = "-";
            this.lblResHabitacion.UseMnemonic = false;
            this.lblResHabitacion.Name = "lblResHabitacion";
            // 
            // lblSeccionDetalles
            // 
            this.lblSeccionDetalles.AutoSize = true;
            this.lblSeccionDetalles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionDetalles.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSeccionDetalles.Location = new System.Drawing.Point(180, 18);
            this.lblSeccionDetalles.Tag = "RegistrarReserva_lblSeccionDetalles";
            this.lblSeccionDetalles.Text = "TIPO";
            this.lblSeccionDetalles.Name = "lblSeccionDetalles";
            // 
            // lblDetTipo
            // 
            this.lblDetTipo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblDetTipo.AutoEllipsis = true;
            this.lblDetTipo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetTipo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblDetTipo.Location = new System.Drawing.Point(178, 38);
            this.lblDetTipo.Size = new System.Drawing.Size(410, 30);
            this.lblDetTipo.Text = "-";
            this.lblDetTipo.UseMnemonic = false;
            this.lblDetTipo.Name = "lblDetTipo";
            // 
            // lblDetDescripcion
            // 
            this.lblDetDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblDetDescripcion.AutoEllipsis = true;
            this.lblDetDescripcion.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetDescripcion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblDetDescripcion.Location = new System.Drawing.Point(180, 70);
            this.lblDetDescripcion.Size = new System.Drawing.Size(408, 40);
            this.lblDetDescripcion.Text = "-";
            this.lblDetDescripcion.UseMnemonic = false;
            this.lblDetDescripcion.Name = "lblDetDescripcion";
            // 
            // pnlLineaDetalles
            // 
            this.pnlLineaDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlLineaDetalles.BackColor = System.Drawing.Color.FromArgb(40, 52, 92);
            this.pnlLineaDetalles.Location = new System.Drawing.Point(24, 120);
            this.pnlLineaDetalles.Size = new System.Drawing.Size(564, 1);
            this.pnlLineaDetalles.Name = "pnlLineaDetalles";
            // 
            // lblDetEtiquetaPiso
            // 
            this.lblDetEtiquetaPiso.AutoSize = true;
            this.lblDetEtiquetaPiso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetEtiquetaPiso.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblDetEtiquetaPiso.Location = new System.Drawing.Point(26, 134);
            this.lblDetEtiquetaPiso.Tag = "RegistrarReserva_lblDetEtiquetaPiso";
            this.lblDetEtiquetaPiso.Text = "Piso";
            this.lblDetEtiquetaPiso.Name = "lblDetEtiquetaPiso";
            // 
            // lblDetPiso
            // 
            this.lblDetPiso.AutoSize = true;
            this.lblDetPiso.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetPiso.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblDetPiso.Location = new System.Drawing.Point(24, 156);
            this.lblDetPiso.Text = "-";
            this.lblDetPiso.Name = "lblDetPiso";
            // 
            // lblDetEtiquetaCapacidad
            // 
            this.lblDetEtiquetaCapacidad.AutoSize = true;
            this.lblDetEtiquetaCapacidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetEtiquetaCapacidad.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblDetEtiquetaCapacidad.Location = new System.Drawing.Point(214, 134);
            this.lblDetEtiquetaCapacidad.Tag = "RegistrarReserva_lblDetEtiquetaCapacidad";
            this.lblDetEtiquetaCapacidad.Text = "Capacidad máx.";
            this.lblDetEtiquetaCapacidad.Name = "lblDetEtiquetaCapacidad";
            // 
            // lblDetCapacidad
            // 
            this.lblDetCapacidad.AutoSize = true;
            this.lblDetCapacidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetCapacidad.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblDetCapacidad.Location = new System.Drawing.Point(212, 156);
            this.lblDetCapacidad.Text = "-";
            this.lblDetCapacidad.Name = "lblDetCapacidad";
            // 
            // lblDetEtiquetaPrecio
            // 
            this.lblDetEtiquetaPrecio.AutoSize = true;
            this.lblDetEtiquetaPrecio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetEtiquetaPrecio.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblDetEtiquetaPrecio.Location = new System.Drawing.Point(402, 134);
            this.lblDetEtiquetaPrecio.Tag = "RegistrarReserva_lblDetEtiquetaPrecio";
            this.lblDetEtiquetaPrecio.Text = "Precio por noche";
            this.lblDetEtiquetaPrecio.Name = "lblDetEtiquetaPrecio";
            // 
            // lblDetPrecio
            // 
            this.lblDetPrecio.AutoSize = true;
            this.lblDetPrecio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetPrecio.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblDetPrecio.Location = new System.Drawing.Point(400, 156);
            this.lblDetPrecio.Text = "-";
            this.lblDetPrecio.Name = "lblDetPrecio";
            // 
            // lblDetEtiquetaTipo
            // 
            this.lblDetEtiquetaTipo.AutoSize = true;
            this.lblDetEtiquetaTipo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetEtiquetaTipo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblDetEtiquetaTipo.Location = new System.Drawing.Point(180, 18);
            this.lblDetEtiquetaTipo.Tag = "RegistrarReserva_lblDetEtiquetaTipo";
            this.lblDetEtiquetaTipo.Text = "Tipo";
            this.lblDetEtiquetaTipo.Visible = false;
            this.lblDetEtiquetaTipo.Name = "lblDetEtiquetaTipo";
            // 
            // lblDetEtiquetaDescripcion
            // 
            this.lblDetEtiquetaDescripcion.AutoSize = true;
            this.lblDetEtiquetaDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetEtiquetaDescripcion.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblDetEtiquetaDescripcion.Location = new System.Drawing.Point(180, 70);
            this.lblDetEtiquetaDescripcion.Tag = "RegistrarReserva_lblDetEtiquetaDescripcion";
            this.lblDetEtiquetaDescripcion.Text = "Descripción";
            this.lblDetEtiquetaDescripcion.Visible = false;
            this.lblDetEtiquetaDescripcion.Name = "lblDetEtiquetaDescripcion";
            // 
            // pnlBotones
            // 
            this.pnlBotones.Controls.Add(this.btnCancelar);
            this.pnlBotones.Controls.Add(this.btnConfirmar);
            this.pnlBotones.Controls.Add(this.lblAvisoConfirmar);
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotones.Size = new System.Drawing.Size(1464, 64);
            this.pnlBotones.Name = "pnlBotones";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.btnCancelar.IconColor = System.Drawing.Color.Gold;
            this.btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancelar.IconSize = 22;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnCancelar.Size = new System.Drawing.Size(200, 50);
            this.btnCancelar.Tag = "RegistrarReserva_btnCancelar";
            this.btnCancelar.Text = "Volver";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.btnCancelar.Location = new System.Drawing.Point(0, 10);
            this.btnCancelar.Name = "btnCancelar";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.btnConfirmar.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnConfirmar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConfirmar.IconSize = 22;
            this.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnConfirmar.Size = new System.Drawing.Size(300, 50);
            this.btnConfirmar.Tag = "RegistrarReserva_btnConfirmar";
            this.btnConfirmar.Text = "Confirmar reserva";
            this.btnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnConfirmar.Location = new System.Drawing.Point(1164, 10);
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnConfirmar.Name = "btnConfirmar";
            // 
            // lblAvisoConfirmar
            // 
            this.lblAvisoConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblAvisoConfirmar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvisoConfirmar.ForeColor = System.Drawing.Color.FromArgb(240, 200, 130);
            this.lblAvisoConfirmar.Location = new System.Drawing.Point(220, 10);
            this.lblAvisoConfirmar.Size = new System.Drawing.Size(924, 50);
            this.lblAvisoConfirmar.Text = "";
            this.lblAvisoConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAvisoConfirmar.Name = "lblAvisoConfirmar";
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.iconEncabezado);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblEncabezado);
            this.pnlEncabezado.Controls.Add(this.pasosReserva);
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Size = new System.Drawing.Size(1464, 72);
            this.pnlEncabezado.Name = "pnlEncabezado";
            // 
            // iconEncabezado
            // 
            this.iconEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.iconEncabezado.ForeColor = System.Drawing.Color.Goldenrod;
            this.iconEncabezado.IconChar = FontAwesome.Sharp.IconChar.CalendarCheck;
            this.iconEncabezado.IconColor = System.Drawing.Color.Goldenrod;
            this.iconEncabezado.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconEncabezado.IconSize = 32;
            this.iconEncabezado.Location = new System.Drawing.Point(0, 4);
            this.iconEncabezado.Size = new System.Drawing.Size(36, 36);
            this.iconEncabezado.TabStop = false;
            this.iconEncabezado.Name = "iconEncabezado";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblSubtitulo.Padding = new System.Windows.Forms.Padding(48, 0, 0, 0);
            this.lblSubtitulo.Tag = "RegistrarReserva_lblSubtitulo";
            this.lblSubtitulo.Text = "Buscá al titular por su documento, revisá la estadía y registrá el cobro.";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSubtitulo.UseMnemonic = false;
            this.lblSubtitulo.Name = "lblSubtitulo";
            // 
            // lblEncabezado
            // 
            this.lblEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEncabezado.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezado.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblEncabezado.Padding = new System.Windows.Forms.Padding(44, 0, 0, 0);
            this.lblEncabezado.Size = new System.Drawing.Size(944, 38);
            this.lblEncabezado.Tag = "RegistrarReserva_lblEncabezado";
            this.lblEncabezado.Text = "Nueva reserva";
            this.lblEncabezado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEncabezado.Name = "lblEncabezado";
            // 
            // pasosReserva
            // 
            this.pasosReserva.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pasosReserva.Dock = System.Windows.Forms.DockStyle.Right;
            this.pasosReserva.Size = new System.Drawing.Size(520, 72);
            this.pasosReserva.Name = "pasosReserva";
            // 
            // toolTip1
            // 
            // 
            // FormRegistrarReserva_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1520, 910);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Padding = new System.Windows.Forms.Padding(28, 18, 28, 16);
            this.Text = "Registrar Reserva";
            this.Controls.Add(this.tblContenido);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FormRegistrarReserva_68SA";
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlBotones.ResumeLayout(false);
            this.pnlLineaDetalles.ResumeLayout(false);
            this.pnlDetalles.ResumeLayout(false);
            this.pnlDetalles.PerformLayout();
            this.pnlSeparadorDer.ResumeLayout(false);
            this.pnlLineaResumen3.ResumeLayout(false);
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.pnlLineaResumen2.ResumeLayout(false);
            this.pnlLineaResumen1.ResumeLayout(false);
            this.pnlResumen.ResumeLayout(false);
            this.pnlResumen.PerformLayout();
            this.pnlColReserva.ResumeLayout(false);
            this.pnlLineaHuesped1.ResumeLayout(false);
            this.pnlIzquierda.ResumeLayout(false);
            this.pnlIzquierda.PerformLayout();
            this.pnlSeparadorCol.ResumeLayout(false);
            this.pnlLineaPago.ResumeLayout(false);
            this.pnlPago.ResumeLayout(false);
            this.pnlPago.PerformLayout();
            this.pnlColHuesped.ResumeLayout(false);
            this.tblContenido.ResumeLayout(false);
            this.tblContenido.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContenido;
        private System.Windows.Forms.Panel pnlColHuesped;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlPago;
        private System.Windows.Forms.Label lblSeccionPago;
        private System.Windows.Forms.Panel pnlLineaPago;
        private System.Windows.Forms.Label lblEtiquetaMetodoPago;
        private CustomControls.IconComboBox cmbMetodoPago;
        private System.Windows.Forms.Label lblEtiquetaMontoRecibido;
        private CustomControls.IconNumericUpDown nudMontoRecibido;
        private System.Windows.Forms.Label lblMarcaTarjetaDetectada;
        private System.Windows.Forms.Label lblVueltoEtiqueta;
        private System.Windows.Forms.Label lblVuelto;
        private System.Windows.Forms.Label lblEtiquetaNumeroTarjeta;
        private CustomControls.IconPlaceholderTextBox txtNumeroTarjeta;
        private System.Windows.Forms.Label lblEtiquetaTitularTarjeta;
        private CustomControls.IconPlaceholderTextBox txtTitularTarjeta;
        private System.Windows.Forms.Label lblEtiquetaVencimiento;
        private CustomControls.IconNumericUpDown nudMesVencimiento;
        private System.Windows.Forms.Label lblBarraVencimiento;
        private CustomControls.IconNumericUpDown nudAnioVencimiento;
        private System.Windows.Forms.Label lblEtiquetaCvv;
        private CustomControls.IconPlaceholderTextBox txtCvv;
        private System.Windows.Forms.Panel pnlSeparadorCol;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlIzquierda;
        private System.Windows.Forms.Label lblSeccionHuesped;
        private System.Windows.Forms.Panel pnlLineaHuesped1;
        private System.Windows.Forms.Label lblEtiquetaTipoDocumento;
        private CustomControls.IconComboBox cmbTipoDocumento;
        private System.Windows.Forms.Label lblEtiquetaDni;
        private CustomControls.IconPlaceholderTextBox txtDni;
        private FontAwesome.Sharp.IconButton btnConsultarDocumento;
        private FontAwesome.Sharp.IconButton btnHuespedNuevo;
        private System.Windows.Forms.Label lblEtiquetaNombre;
        private CustomControls.IconPlaceholderTextBox txtNombre;
        private System.Windows.Forms.Label lblEtiquetaApellido;
        private CustomControls.IconPlaceholderTextBox txtApellido;
        private System.Windows.Forms.Label lblEtiquetaFechaNacimiento;
        private CustomControls.IconDateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblEtiquetaNacionalidad;
        private CustomControls.IconPlaceholderTextBox txtNacionalidad;
        private System.Windows.Forms.Label lblEtiquetaTelefono;
        private CustomControls.IconPlaceholderTextBox txtTelefono;
        private System.Windows.Forms.Label lblEtiquetaEmail;
        private CustomControls.IconPlaceholderTextBox txtEmail;
        private System.Windows.Forms.Panel pnlColReserva;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlResumen;
        private System.Windows.Forms.Panel pnlLineaResumen1;
        private System.Windows.Forms.Label lblSeccionResumen;
        private System.Windows.Forms.Label lblEtiquetaFechaIngreso;
        private CustomControls.IconDateTimePicker dtpFechaIngreso;
        private System.Windows.Forms.Label lblEtiquetaFechaEgreso;
        private CustomControls.IconDateTimePicker dtpFechaEgreso;
        private System.Windows.Forms.Label lblResEtiquetaNoches;
        private System.Windows.Forms.Label lblResNoches;
        private System.Windows.Forms.Panel pnlLineaResumen2;
        private System.Windows.Forms.Label lblSeccionComposicion;
        private System.Windows.Forms.Label lblEtiquetaAdultos;
        private CustomControls.IconNumericUpDown nudAdultos;
        private System.Windows.Forms.Label lblEtiquetaNinos;
        private CustomControls.IconNumericUpDown nudNinos;
        private System.Windows.Forms.Label lblComposicionAdvertencia;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlTotal;
        private System.Windows.Forms.Panel pnlLineaResumen3;
        private System.Windows.Forms.Label lblResMontoTotal;
        private System.Windows.Forms.Label lblResEtiquetaMontoTotal;
        private System.Windows.Forms.Panel pnlSeparadorDer;
        private GUI_08YS.UserControls.RoundedPanel_68SA pnlDetalles;
        private System.Windows.Forms.Label lblResEtiquetaHabitacion;
        private System.Windows.Forms.Label lblResHabitacion;
        private System.Windows.Forms.Label lblSeccionDetalles;
        private System.Windows.Forms.Label lblDetTipo;
        private System.Windows.Forms.Label lblDetDescripcion;
        private System.Windows.Forms.Panel pnlLineaDetalles;
        private System.Windows.Forms.Label lblDetEtiquetaPiso;
        private System.Windows.Forms.Label lblDetPiso;
        private System.Windows.Forms.Label lblDetEtiquetaCapacidad;
        private System.Windows.Forms.Label lblDetCapacidad;
        private System.Windows.Forms.Label lblDetEtiquetaPrecio;
        private System.Windows.Forms.Label lblDetPrecio;
        private System.Windows.Forms.Label lblDetEtiquetaTipo;
        private System.Windows.Forms.Label lblDetEtiquetaDescripcion;
        private System.Windows.Forms.Panel pnlBotones;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnConfirmar;
        private System.Windows.Forms.Label lblAvisoConfirmar;
        private System.Windows.Forms.Panel pnlEncabezado;
        private FontAwesome.Sharp.IconPictureBox iconEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblEncabezado;
        private GUI_08YS.UserControls.PasosReserva_68SA pasosReserva;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
