namespace GUI_08YS.Recepcionista
{
    partial class FormModificarReserva_68SA
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

        private void InitializeComponent()
        {
            this.pnlBorde = new System.Windows.Forms.Panel();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.lblEtiquetaMontoTotal = new System.Windows.Forms.Label();
            this.lblNoches = new System.Windows.Forms.Label();
            this.lblEtiquetaNoches = new System.Windows.Forms.Label();
            this.pnlLinea2 = new System.Windows.Forms.Panel();
            this.lblAdvertenciaCapacidad = new System.Windows.Forms.Label();
            this.nudNinos = new CustomControls.IconNumericUpDown();
            this.lblEtiquetaNinos = new System.Windows.Forms.Label();
            this.nudAdultos = new CustomControls.IconNumericUpDown();
            this.lblEtiquetaAdultos = new System.Windows.Forms.Label();
            this.dtpFechaEgreso = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaFechaEgreso = new System.Windows.Forms.Label();
            this.dtpFechaIngreso = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaFechaIngreso = new System.Windows.Forms.Label();
            this.pnlLinea1 = new System.Windows.Forms.Panel();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblEtiquetaTipo = new System.Windows.Forms.Label();
            this.lblTitular = new System.Windows.Forms.Label();
            this.lblEtiquetaTitular = new System.Windows.Forms.Label();
            this.lblHabitacion = new System.Windows.Forms.Label();
            this.lblEtiquetaHabitacion = new System.Windows.Forms.Label();
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
            this.pnlBorde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.pnlBorde.Controls.Add(this.lblMontoTotal);
            this.pnlBorde.Controls.Add(this.lblEtiquetaMontoTotal);
            this.pnlBorde.Controls.Add(this.lblNoches);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNoches);
            this.pnlBorde.Controls.Add(this.pnlLinea2);
            this.pnlBorde.Controls.Add(this.lblAdvertenciaCapacidad);
            this.pnlBorde.Controls.Add(this.nudNinos);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNinos);
            this.pnlBorde.Controls.Add(this.nudAdultos);
            this.pnlBorde.Controls.Add(this.lblEtiquetaAdultos);
            this.pnlBorde.Controls.Add(this.dtpFechaEgreso);
            this.pnlBorde.Controls.Add(this.lblEtiquetaFechaEgreso);
            this.pnlBorde.Controls.Add(this.dtpFechaIngreso);
            this.pnlBorde.Controls.Add(this.lblEtiquetaFechaIngreso);
            this.pnlBorde.Controls.Add(this.pnlLinea1);
            this.pnlBorde.Controls.Add(this.lblTipo);
            this.pnlBorde.Controls.Add(this.lblEtiquetaTipo);
            this.pnlBorde.Controls.Add(this.lblTitular);
            this.pnlBorde.Controls.Add(this.lblEtiquetaTitular);
            this.pnlBorde.Controls.Add(this.lblHabitacion);
            this.pnlBorde.Controls.Add(this.lblEtiquetaHabitacion);
            this.pnlBorde.Controls.Add(this.pnlFooter);
            this.pnlBorde.Controls.Add(this.pnlHeader);
            this.pnlBorde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorde.Location = new System.Drawing.Point(3, 3);
            this.pnlBorde.Name = "pnlBorde";
            this.pnlBorde.Size = new System.Drawing.Size(474, 584);
            this.pnlBorde.TabIndex = 0;
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
            this.pnlHeader.Size = new System.Drawing.Size(474, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnCerrar.IconColor = System.Drawing.Color.Gold;
            this.btnCerrar.IconSize = 26;
            this.btnCerrar.Location = new System.Drawing.Point(424, 17);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(34, 34);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Location = new System.Drawing.Point(66, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(200, 25);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Modificar Reserva";
            // 
            // iconTitulo
            // 
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.PenToSquare;
            this.iconTitulo.IconColor = System.Drawing.Color.Gold;
            this.iconTitulo.IconSize = 34;
            this.iconTitulo.Location = new System.Drawing.Point(18, 16);
            this.iconTitulo.Name = "iconTitulo";
            this.iconTitulo.Size = new System.Drawing.Size(38, 38);
            this.iconTitulo.TabIndex = 0;
            this.iconTitulo.TabStop = false;
            // 
            // lblEtiquetaHabitacion
            // 
            this.lblEtiquetaHabitacion.AutoSize = true;
            this.lblEtiquetaHabitacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaHabitacion.Location = new System.Drawing.Point(20, 88);
            this.lblEtiquetaHabitacion.Name = "lblEtiquetaHabitacion";
            this.lblEtiquetaHabitacion.Size = new System.Drawing.Size(70, 15);
            this.lblEtiquetaHabitacion.TabIndex = 1;
            this.lblEtiquetaHabitacion.Text = "Habitación:";
            // 
            // lblHabitacion
            // 
            this.lblHabitacion.AutoSize = true;
            this.lblHabitacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHabitacion.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblHabitacion.Location = new System.Drawing.Point(140, 88);
            this.lblHabitacion.Name = "lblHabitacion";
            this.lblHabitacion.Size = new System.Drawing.Size(50, 15);
            this.lblHabitacion.TabIndex = 2;
            this.lblHabitacion.Text = "-";
            // 
            // lblEtiquetaTitular
            // 
            this.lblEtiquetaTitular.AutoSize = true;
            this.lblEtiquetaTitular.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaTitular.Location = new System.Drawing.Point(20, 114);
            this.lblEtiquetaTitular.Name = "lblEtiquetaTitular";
            this.lblEtiquetaTitular.Size = new System.Drawing.Size(50, 15);
            this.lblEtiquetaTitular.TabIndex = 3;
            this.lblEtiquetaTitular.Text = "Titular:";
            // 
            // lblTitular
            // 
            this.lblTitular.AutoSize = true;
            this.lblTitular.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitular.Location = new System.Drawing.Point(140, 114);
            this.lblTitular.Name = "lblTitular";
            this.lblTitular.Size = new System.Drawing.Size(50, 15);
            this.lblTitular.TabIndex = 4;
            this.lblTitular.Text = "-";
            // 
            // lblEtiquetaTipo
            // 
            this.lblEtiquetaTipo.AutoSize = true;
            this.lblEtiquetaTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaTipo.Location = new System.Drawing.Point(20, 140);
            this.lblEtiquetaTipo.Name = "lblEtiquetaTipo";
            this.lblEtiquetaTipo.Size = new System.Drawing.Size(40, 15);
            this.lblEtiquetaTipo.TabIndex = 5;
            this.lblEtiquetaTipo.Text = "Tipo:";
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTipo.Location = new System.Drawing.Point(140, 140);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(50, 15);
            this.lblTipo.TabIndex = 6;
            this.lblTipo.Text = "-";
            // 
            // pnlLinea1
            // 
            this.pnlLinea1.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLinea1.Location = new System.Drawing.Point(20, 172);
            this.pnlLinea1.Name = "pnlLinea1";
            this.pnlLinea1.Size = new System.Drawing.Size(434, 2);
            this.pnlLinea1.TabIndex = 7;
            // 
            // lblEtiquetaFechaIngreso
            // 
            this.lblEtiquetaFechaIngreso.AutoSize = true;
            this.lblEtiquetaFechaIngreso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaFechaIngreso.Location = new System.Drawing.Point(20, 186);
            this.lblEtiquetaFechaIngreso.Name = "lblEtiquetaFechaIngreso";
            this.lblEtiquetaFechaIngreso.Size = new System.Drawing.Size(110, 15);
            this.lblEtiquetaFechaIngreso.TabIndex = 8;
            this.lblEtiquetaFechaIngreso.Text = "Fecha de Ingreso";
            // 
            // dtpFechaIngreso
            // 
            this.dtpFechaIngreso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaIngreso.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaIngreso.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaIngreso.BorderWidth = 2;
            this.dtpFechaIngreso.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaIngreso.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaIngreso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaIngreso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIngreso.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaIngreso.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpFechaIngreso.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaIngreso.IconSize = 22;
            this.dtpFechaIngreso.Location = new System.Drawing.Point(20, 204);
            this.dtpFechaIngreso.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaIngreso.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaIngreso.Name = "dtpFechaIngreso";
            this.dtpFechaIngreso.Size = new System.Drawing.Size(434, 40);
            this.dtpFechaIngreso.TabIndex = 9;
            this.dtpFechaIngreso.Value = null;
            // 
            // lblEtiquetaFechaEgreso
            // 
            this.lblEtiquetaFechaEgreso.AutoSize = true;
            this.lblEtiquetaFechaEgreso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaFechaEgreso.Location = new System.Drawing.Point(20, 252);
            this.lblEtiquetaFechaEgreso.Name = "lblEtiquetaFechaEgreso";
            this.lblEtiquetaFechaEgreso.Size = new System.Drawing.Size(105, 15);
            this.lblEtiquetaFechaEgreso.TabIndex = 10;
            this.lblEtiquetaFechaEgreso.Text = "Fecha de Egreso";
            // 
            // dtpFechaEgreso
            // 
            this.dtpFechaEgreso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaEgreso.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaEgreso.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaEgreso.BorderWidth = 2;
            this.dtpFechaEgreso.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpFechaEgreso.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaEgreso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaEgreso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaEgreso.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFechaEgreso.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpFechaEgreso.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpFechaEgreso.IconSize = 22;
            this.dtpFechaEgreso.Location = new System.Drawing.Point(20, 270);
            this.dtpFechaEgreso.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaEgreso.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaEgreso.Name = "dtpFechaEgreso";
            this.dtpFechaEgreso.Size = new System.Drawing.Size(434, 40);
            this.dtpFechaEgreso.TabIndex = 11;
            this.dtpFechaEgreso.Value = null;
            // 
            // lblEtiquetaAdultos
            // 
            this.lblEtiquetaAdultos.AutoSize = true;
            this.lblEtiquetaAdultos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaAdultos.Location = new System.Drawing.Point(20, 318);
            this.lblEtiquetaAdultos.Name = "lblEtiquetaAdultos";
            this.lblEtiquetaAdultos.Size = new System.Drawing.Size(55, 15);
            this.lblEtiquetaAdultos.TabIndex = 12;
            this.lblEtiquetaAdultos.Text = "Adultos";
            // 
            // nudAdultos
            // 
            this.nudAdultos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.nudAdultos.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.BorderWidth = 2;
            this.nudAdultos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudAdultos.IconChar = FontAwesome.Sharp.IconChar.User;
            this.nudAdultos.IconColor = System.Drawing.Color.Goldenrod;
            this.nudAdultos.Location = new System.Drawing.Point(20, 336);
            this.nudAdultos.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudAdultos.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.nudAdultos.Name = "nudAdultos";
            this.nudAdultos.Size = new System.Drawing.Size(200, 40);
            this.nudAdultos.TabIndex = 13;
            this.nudAdultos.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblEtiquetaNinos
            // 
            this.lblEtiquetaNinos.AutoSize = true;
            this.lblEtiquetaNinos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaNinos.Location = new System.Drawing.Point(254, 318);
            this.lblEtiquetaNinos.Name = "lblEtiquetaNinos";
            this.lblEtiquetaNinos.Size = new System.Drawing.Size(40, 15);
            this.lblEtiquetaNinos.TabIndex = 14;
            this.lblEtiquetaNinos.Text = "Niños";
            // 
            // nudNinos
            // 
            this.nudNinos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.nudNinos.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.BorderWidth = 2;
            this.nudNinos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudNinos.IconChar = FontAwesome.Sharp.IconChar.ChildReaching;
            this.nudNinos.IconColor = System.Drawing.Color.Goldenrod;
            this.nudNinos.Location = new System.Drawing.Point(254, 336);
            this.nudNinos.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.nudNinos.Name = "nudNinos";
            this.nudNinos.Size = new System.Drawing.Size(200, 40);
            this.nudNinos.TabIndex = 15;
            // 
            // lblAdvertenciaCapacidad
            // 
            this.lblAdvertenciaCapacidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblAdvertenciaCapacidad.Location = new System.Drawing.Point(20, 380);
            this.lblAdvertenciaCapacidad.Name = "lblAdvertenciaCapacidad";
            this.lblAdvertenciaCapacidad.Size = new System.Drawing.Size(434, 30);
            this.lblAdvertenciaCapacidad.TabIndex = 16;
            this.lblAdvertenciaCapacidad.Text = "La composición supera la capacidad de la habitación.";
            this.lblAdvertenciaCapacidad.Visible = false;
            // 
            // pnlLinea2
            // 
            this.pnlLinea2.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLinea2.Location = new System.Drawing.Point(20, 414);
            this.pnlLinea2.Name = "pnlLinea2";
            this.pnlLinea2.Size = new System.Drawing.Size(434, 2);
            this.pnlLinea2.TabIndex = 17;
            // 
            // lblEtiquetaNoches
            // 
            this.lblEtiquetaNoches.AutoSize = true;
            this.lblEtiquetaNoches.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaNoches.Location = new System.Drawing.Point(20, 428);
            this.lblEtiquetaNoches.Name = "lblEtiquetaNoches";
            this.lblEtiquetaNoches.Size = new System.Drawing.Size(50, 15);
            this.lblEtiquetaNoches.TabIndex = 18;
            this.lblEtiquetaNoches.Text = "Noches:";
            // 
            // lblNoches
            // 
            this.lblNoches.AutoSize = true;
            this.lblNoches.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblNoches.Location = new System.Drawing.Point(140, 428);
            this.lblNoches.Name = "lblNoches";
            this.lblNoches.Size = new System.Drawing.Size(50, 15);
            this.lblNoches.TabIndex = 19;
            this.lblNoches.Text = "-";
            // 
            // lblEtiquetaMontoTotal
            // 
            this.lblEtiquetaMontoTotal.AutoSize = true;
            this.lblEtiquetaMontoTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaMontoTotal.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblEtiquetaMontoTotal.Location = new System.Drawing.Point(20, 456);
            this.lblEtiquetaMontoTotal.Name = "lblEtiquetaMontoTotal";
            this.lblEtiquetaMontoTotal.Size = new System.Drawing.Size(105, 20);
            this.lblEtiquetaMontoTotal.TabIndex = 20;
            this.lblEtiquetaMontoTotal.Text = "Monto Total:";
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.AutoSize = true;
            this.lblMontoTotal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoTotal.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblMontoTotal.Location = new System.Drawing.Point(140, 454);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(80, 24);
            this.lblMontoTotal.TabIndex = 21;
            this.lblMontoTotal.Text = "$0";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlFooter.Controls.Add(this.btnGuardar);
            this.pnlFooter.Controls.Add(this.btnCancelar);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 520);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(474, 64);
            this.pnlFooter.TabIndex = 22;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnGuardar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnGuardar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnGuardar.IconSize = 22;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(214, 0);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnGuardar.Size = new System.Drawing.Size(180, 64);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnCancelar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnCancelar.IconColor = System.Drawing.Color.Gold;
            this.btnCancelar.IconSize = 20;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(74, 0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnCancelar.Size = new System.Drawing.Size(140, 64);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // FormModificarReserva_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(480, 590);
            this.Controls.Add(this.pnlBorde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModificarReserva_68SA";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modificar Reserva";
            this.pnlBorde.ResumeLayout(false);
            this.pnlBorde.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconTitulo)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlBorde;
        private System.Windows.Forms.Panel pnlHeader;
        private FontAwesome.Sharp.IconPictureBox iconTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnCerrar;
        private System.Windows.Forms.Label lblEtiquetaHabitacion;
        private System.Windows.Forms.Label lblHabitacion;
        private System.Windows.Forms.Label lblEtiquetaTitular;
        private System.Windows.Forms.Label lblTitular;
        private System.Windows.Forms.Label lblEtiquetaTipo;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Panel pnlLinea1;
        private System.Windows.Forms.Label lblEtiquetaFechaIngreso;
        private CustomControls.IconDateTimePicker dtpFechaIngreso;
        private System.Windows.Forms.Label lblEtiquetaFechaEgreso;
        private CustomControls.IconDateTimePicker dtpFechaEgreso;
        private System.Windows.Forms.Label lblEtiquetaAdultos;
        private CustomControls.IconNumericUpDown nudAdultos;
        private System.Windows.Forms.Label lblEtiquetaNinos;
        private CustomControls.IconNumericUpDown nudNinos;
        private System.Windows.Forms.Label lblAdvertenciaCapacidad;
        private System.Windows.Forms.Panel pnlLinea2;
        private System.Windows.Forms.Label lblEtiquetaNoches;
        private System.Windows.Forms.Label lblNoches;
        private System.Windows.Forms.Label lblEtiquetaMontoTotal;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Panel pnlFooter;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
    }
}