namespace GUI_08YS.Recepcionista
{
    partial class FormCheckIn_68SA
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
            this.components = new System.ComponentModel.Container();
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
            this.pnlAcompanantes = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.flpFilas = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlLineaAcompanantes = new System.Windows.Forms.Panel();
            this.lblSeccionAcompanantes = new System.Windows.Forms.Label();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).BeginInit();
            this.flpContenido.SuspendLayout();
            this.pnlAccionBuscar.SuspendLayout();
            this.pnlReserva.SuspendLayout();
            this.pnlAcompanantes.SuspendLayout();
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
            this.lblEncabezado.Tag = "CheckIn_lblEncabezado";
            this.lblEncabezado.Size = new System.Drawing.Size(200, 30);
            this.lblEncabezado.TabIndex = 1;
            this.lblEncabezado.Text = "Registrar Check-in";
            // 
            // iconEncabezado
            // 
            this.iconEncabezado.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
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
            this.btnBuscarReserva.Tag = "CheckIn_btnBuscarReserva";
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
            this.lblSeccionReserva.Tag = "CheckIn_lblSeccionReserva";
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
            this.lblResEtiquetaHabitacion.Tag = "CheckIn_lblResEtiquetaHabitacion";
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
            this.lblResEtiquetaTipo.Tag = "CheckIn_lblResEtiquetaTipo";
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
            this.lblResEtiquetaTitular.Tag = "CheckIn_lblResEtiquetaTitular";
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
            this.lblResEtiquetaFechas.Tag = "CheckIn_lblResEtiquetaFechas";
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
            this.lblResEtiquetaComposicion.Tag = "CheckIn_lblResEtiquetaComposicion";
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
            // pnlAcompanantes
            // 
            this.pnlAcompanantes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlAcompanantes.Controls.Add(this.flpFilas);
            this.pnlAcompanantes.Controls.Add(this.pnlLineaAcompanantes);
            this.pnlAcompanantes.Controls.Add(this.lblSeccionAcompanantes);
            this.pnlAcompanantes.CornerRadius = 12;
            this.pnlAcompanantes.Location = new System.Drawing.Point(3, 3);
            this.pnlAcompanantes.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.pnlAcompanantes.Name = "pnlAcompanantes";
            this.pnlAcompanantes.Size = new System.Drawing.Size(700, 220);
            this.pnlAcompanantes.TabIndex = 2;
            // 
            // lblSeccionAcompanantes
            // 
            this.lblSeccionAcompanantes.AutoSize = true;
            this.lblSeccionAcompanantes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionAcompanantes.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblSeccionAcompanantes.Location = new System.Drawing.Point(16, 12);
            this.lblSeccionAcompanantes.Name = "lblSeccionAcompanantes";
            this.lblSeccionAcompanantes.Tag = "CheckIn_lblSeccionAcompanantes";
            this.lblSeccionAcompanantes.Size = new System.Drawing.Size(220, 19);
            this.lblSeccionAcompanantes.TabIndex = 0;
            this.lblSeccionAcompanantes.Text = "ACOMPAÑANTES";
            // 
            // pnlLineaAcompanantes
            // 
            this.pnlLineaAcompanantes.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLineaAcompanantes.Location = new System.Drawing.Point(16, 38);
            this.pnlLineaAcompanantes.Name = "pnlLineaAcompanantes";
            this.pnlLineaAcompanantes.Size = new System.Drawing.Size(668, 2);
            this.pnlLineaAcompanantes.TabIndex = 1;
            // 
            // flpFilas
            // 
            this.flpFilas.AutoScroll = true;
            this.flpFilas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.flpFilas.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFilas.Location = new System.Drawing.Point(16, 50);
            this.flpFilas.Name = "flpFilas";
            this.flpFilas.Size = new System.Drawing.Size(668, 158);
            this.flpFilas.TabIndex = 2;
            this.flpFilas.WrapContents = false;
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
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.btnConfirmar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnConfirmar.IconSize = 26;
            this.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmar.Location = new System.Drawing.Point(560, 0);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Tag = "CheckIn_btnConfirmar";
            this.btnConfirmar.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnConfirmar.Size = new System.Drawing.Size(200, 64);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar Check-in";
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
            this.btnCancelar.Tag = "CheckIn_btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCancelar.Size = new System.Drawing.Size(140, 64);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // FormCheckIn_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(760, 766);
            this.Controls.Add(this.flpContenido);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCheckIn_68SA";
            this.Text = "FormCheckIn_68SA";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconEncabezado)).EndInit();
            this.flpContenido.ResumeLayout(false);
            this.pnlAccionBuscar.ResumeLayout(false);
            this.pnlReserva.ResumeLayout(false);
            this.pnlReserva.PerformLayout();
            this.pnlAcompanantes.ResumeLayout(false);
            this.pnlAcompanantes.PerformLayout();
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
        private UserControls.RoundedPanel_68SA pnlAcompanantes;
        private System.Windows.Forms.Label lblSeccionAcompanantes;
        private System.Windows.Forms.Panel pnlLineaAcompanantes;
        private System.Windows.Forms.FlowLayoutPanel flpFilas;
        private System.Windows.Forms.Panel pnlBotones;
        private FontAwesome.Sharp.IconButton btnConfirmar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}