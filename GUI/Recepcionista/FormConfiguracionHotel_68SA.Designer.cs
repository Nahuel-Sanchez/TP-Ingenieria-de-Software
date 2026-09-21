namespace GUI_08YS.Recepcionista
{
    partial class FormConfiguracionHotel_68SA
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
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlTarjeta = new System.Windows.Forms.Panel();
            this.lblAviso = new System.Windows.Forms.Label();
            this.btnDescartar = new FontAwesome.Sharp.IconButton();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.pnlLinea = new System.Windows.Forms.Panel();
            this.lblAyudaGracia = new System.Windows.Forms.Label();
            this.nudGracia = new CustomControls.IconNumericUpDown();
            this.lblEtiquetaGracia = new System.Windows.Forms.Label();
            this.dtpCheckOut = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaCheckOut = new System.Windows.Forms.Label();
            this.dtpCheckIn = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaCheckIn = new System.Windows.Forms.Label();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlContenido.SuspendLayout();
            this.pnlTarjeta.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlContenido.Controls.Add(this.pnlTarjeta);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 72);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 8, 24, 16);
            this.pnlContenido.Size = new System.Drawing.Size(1000, 528);
            this.pnlContenido.TabIndex = 0;
            // 
            // pnlTarjeta
            // 
            this.pnlTarjeta.BackColor = System.Drawing.Color.FromArgb(10, 18, 50);
            this.pnlTarjeta.Controls.Add(this.lblAviso);
            this.pnlTarjeta.Controls.Add(this.btnDescartar);
            this.pnlTarjeta.Controls.Add(this.btnGuardar);
            this.pnlTarjeta.Controls.Add(this.pnlLinea);
            this.pnlTarjeta.Controls.Add(this.lblAyudaGracia);
            this.pnlTarjeta.Controls.Add(this.nudGracia);
            this.pnlTarjeta.Controls.Add(this.lblEtiquetaGracia);
            this.pnlTarjeta.Controls.Add(this.dtpCheckOut);
            this.pnlTarjeta.Controls.Add(this.lblEtiquetaCheckOut);
            this.pnlTarjeta.Controls.Add(this.dtpCheckIn);
            this.pnlTarjeta.Controls.Add(this.lblEtiquetaCheckIn);
            this.pnlTarjeta.Location = new System.Drawing.Point(24, 8);
            this.pnlTarjeta.Name = "pnlTarjeta";
            this.pnlTarjeta.Size = new System.Drawing.Size(600, 360);
            this.pnlTarjeta.TabIndex = 0;
            // 
            // lblEtiquetaCheckIn
            // 
            this.lblEtiquetaCheckIn.AutoSize = true;
            this.lblEtiquetaCheckIn.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaCheckIn.Location = new System.Drawing.Point(24, 22);
            this.lblEtiquetaCheckIn.Name = "lblEtiquetaCheckIn";
            this.lblEtiquetaCheckIn.Size = new System.Drawing.Size(105, 16);
            this.lblEtiquetaCheckIn.TabIndex = 0;
            this.lblEtiquetaCheckIn.Tag = "Config_lblCheckIn";
            this.lblEtiquetaCheckIn.Text = "Hora de check-in";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpCheckIn.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpCheckIn.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpCheckIn.BorderWidth = 2;
            this.dtpCheckIn.CalendarBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpCheckIn.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpCheckIn.CalendarTitleBackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.dtpCheckIn.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpCheckIn.CalendarTrailingForeColor = System.Drawing.Color.Silver;
            this.dtpCheckIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpCheckIn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpCheckIn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpCheckIn.IconChar = FontAwesome.Sharp.IconChar.Clock;
            this.dtpCheckIn.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpCheckIn.IconSize = 19;
            this.dtpCheckIn.Location = new System.Drawing.Point(24, 44);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(260, 49);
            this.dtpCheckIn.TabIndex = 1;
            // 
            // lblEtiquetaCheckOut
            // 
            this.lblEtiquetaCheckOut.AutoSize = true;
            this.lblEtiquetaCheckOut.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaCheckOut.Location = new System.Drawing.Point(316, 22);
            this.lblEtiquetaCheckOut.Name = "lblEtiquetaCheckOut";
            this.lblEtiquetaCheckOut.Size = new System.Drawing.Size(115, 16);
            this.lblEtiquetaCheckOut.TabIndex = 2;
            this.lblEtiquetaCheckOut.Tag = "Config_lblCheckOut";
            this.lblEtiquetaCheckOut.Text = "Hora de check-out";
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpCheckOut.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpCheckOut.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpCheckOut.BorderWidth = 2;
            this.dtpCheckOut.CalendarBackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.dtpCheckOut.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpCheckOut.CalendarTitleBackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.dtpCheckOut.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpCheckOut.CalendarTrailingForeColor = System.Drawing.Color.Silver;
            this.dtpCheckOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpCheckOut.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpCheckOut.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpCheckOut.IconChar = FontAwesome.Sharp.IconChar.Clock;
            this.dtpCheckOut.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpCheckOut.IconSize = 19;
            this.dtpCheckOut.Location = new System.Drawing.Point(316, 44);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(260, 49);
            this.dtpCheckOut.TabIndex = 3;
            // 
            // lblEtiquetaGracia
            // 
            this.lblEtiquetaGracia.AutoSize = true;
            this.lblEtiquetaGracia.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblEtiquetaGracia.Location = new System.Drawing.Point(24, 112);
            this.lblEtiquetaGracia.Name = "lblEtiquetaGracia";
            this.lblEtiquetaGracia.Size = new System.Drawing.Size(196, 16);
            this.lblEtiquetaGracia.TabIndex = 4;
            this.lblEtiquetaGracia.Tag = "Config_lblGracia";
            this.lblEtiquetaGracia.Text = "Tolerancia de no-show (horas)";
            // 
            // nudGracia
            // 
            this.nudGracia.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.nudGracia.BorderColor = System.Drawing.Color.Goldenrod;
            this.nudGracia.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.nudGracia.BorderWidth = 2;
            this.nudGracia.DecimalPlaces = 2;
            this.nudGracia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudGracia.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudGracia.IconChar = FontAwesome.Sharp.IconChar.HourglassHalf;
            this.nudGracia.IconColor = System.Drawing.Color.Goldenrod;
            this.nudGracia.IconPadding = 4;
            this.nudGracia.IconSize = 20;
            this.nudGracia.Increment = new decimal(new int[] { 50, 0, 0, 131072 });
            this.nudGracia.Location = new System.Drawing.Point(24, 134);
            this.nudGracia.Maximum = new decimal(new int[] { 99999, 0, 0, 131072 });
            this.nudGracia.Name = "nudGracia";
            this.nudGracia.Size = new System.Drawing.Size(260, 49);
            this.nudGracia.SpinnerArrowColor = System.Drawing.Color.Goldenrod;
            this.nudGracia.SpinnerHoverBackColor = System.Drawing.Color.FromArgb(10, 25, 65);
            this.nudGracia.SpinnerPressedBackColor = System.Drawing.Color.FromArgb(15, 35, 85);
            this.nudGracia.TabIndex = 5;
            // 
            // lblAyudaGracia
            // 
            this.lblAyudaGracia.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this.lblAyudaGracia.Location = new System.Drawing.Point(24, 192);
            this.lblAyudaGracia.Name = "lblAyudaGracia";
            this.lblAyudaGracia.Size = new System.Drawing.Size(552, 40);
            this.lblAyudaGracia.TabIndex = 6;
            this.lblAyudaGracia.Tag = "Config_ayudaGracia";
            this.lblAyudaGracia.Text = "Pasado este tiempo desde la hora de check-in, las reservas confirmadas sin check-in se cancelan automáticamente como no-show.";
            // 
            // pnlLinea
            // 
            this.pnlLinea.BackColor = System.Drawing.Color.FromArgb(60, 75, 110);
            this.pnlLinea.Location = new System.Drawing.Point(24, 246);
            this.pnlLinea.Name = "pnlLinea";
            this.pnlLinea.Size = new System.Drawing.Size(552, 1);
            this.pnlLinea.TabIndex = 7;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnGuardar.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 22;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(24, 264);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(180, 49);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Tag = "Crud_btnGuardar";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnDescartar
            // 
            this.btnDescartar.BackColor = System.Drawing.Color.FromArgb(5, 15, 45);
            this.btnDescartar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescartar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDescartar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnDescartar.IconChar = FontAwesome.Sharp.IconChar.RotateLeft;
            this.btnDescartar.IconColor = System.Drawing.Color.Goldenrod;
            this.btnDescartar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDescartar.IconSize = 22;
            this.btnDescartar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDescartar.Location = new System.Drawing.Point(216, 264);
            this.btnDescartar.Name = "btnDescartar";
            this.btnDescartar.Size = new System.Drawing.Size(220, 49);
            this.btnDescartar.TabIndex = 9;
            this.btnDescartar.Tag = "Config_btnDescartar";
            this.btnDescartar.Text = "Descartar cambios";
            this.btnDescartar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDescartar.UseVisualStyleBackColor = false;
            // 
            // lblAviso
            // 
            this.lblAviso.ForeColor = System.Drawing.Color.FromArgb(230, 175, 46);
            this.lblAviso.Location = new System.Drawing.Point(24, 324);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(552, 20);
            this.lblAviso.TabIndex = 10;
            this.lblAviso.Visible = false;
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 12, 24, 8);
            this.pnlEncabezado.Size = new System.Drawing.Size(1000, 72);
            this.pnlEncabezado.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Location = new System.Drawing.Point(24, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(952, 52);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Tag = "Config_titulo";
            this.lblTitulo.Text = "Configuración del hotel";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormConfiguracionHotel_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormConfiguracionHotel_68SA";
            this.Text = "Configuración del hotel";
            this.pnlContenido.ResumeLayout(false);
            this.pnlTarjeta.ResumeLayout(false);
            this.pnlTarjeta.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlTarjeta;
        private System.Windows.Forms.Label lblAviso;
        private FontAwesome.Sharp.IconButton btnDescartar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private System.Windows.Forms.Panel pnlLinea;
        private System.Windows.Forms.Label lblAyudaGracia;
        private CustomControls.IconNumericUpDown nudGracia;
        private System.Windows.Forms.Label lblEtiquetaGracia;
        private CustomControls.IconDateTimePicker dtpCheckOut;
        private System.Windows.Forms.Label lblEtiquetaCheckOut;
        private CustomControls.IconDateTimePicker dtpCheckIn;
        private System.Windows.Forms.Label lblEtiquetaCheckIn;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
    }
}