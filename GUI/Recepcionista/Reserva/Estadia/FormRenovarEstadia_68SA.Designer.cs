namespace GUI_08YS.Recepcionista
{
    partial class FormRenovarEstadia_68SA
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
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.lblCostoAdicional = new System.Windows.Forms.Label();
            this.lblEtiquetaCostoAdicional = new System.Windows.Forms.Label();
            this.lblNochesAdicionales = new System.Windows.Forms.Label();
            this.lblEtiquetaNoches = new System.Windows.Forms.Label();
            this.dtpNuevaFechaEgreso = new CustomControls.IconDateTimePicker();
            this.lblEtiquetaNuevaFecha = new System.Windows.Forms.Label();
            this.pnlLinea = new System.Windows.Forms.Panel();
            this.lblTarifa = new System.Windows.Forms.Label();
            this.lblEtiquetaTarifa = new System.Windows.Forms.Label();
            this.lblFechaActual = new System.Windows.Forms.Label();
            this.lblEtiquetaFechaActual = new System.Windows.Forms.Label();
            this.lblTitular = new System.Windows.Forms.Label();
            this.lblEtiquetaTitular = new System.Windows.Forms.Label();
            this.lblHabitacion = new System.Windows.Forms.Label();
            this.lblEtiquetaHabitacion = new System.Windows.Forms.Label();
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
            this.pnlBorde.Controls.Add(this.lblCostoAdicional);
            this.pnlBorde.Controls.Add(this.lblEtiquetaCostoAdicional);
            this.pnlBorde.Controls.Add(this.lblNochesAdicionales);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNoches);
            this.pnlBorde.Controls.Add(this.dtpNuevaFechaEgreso);
            this.pnlBorde.Controls.Add(this.lblEtiquetaNuevaFecha);
            this.pnlBorde.Controls.Add(this.pnlLinea);
            this.pnlBorde.Controls.Add(this.lblTarifa);
            this.pnlBorde.Controls.Add(this.lblEtiquetaTarifa);
            this.pnlBorde.Controls.Add(this.lblFechaActual);
            this.pnlBorde.Controls.Add(this.lblEtiquetaFechaActual);
            this.pnlBorde.Controls.Add(this.lblTitular);
            this.pnlBorde.Controls.Add(this.lblEtiquetaTitular);
            this.pnlBorde.Controls.Add(this.lblHabitacion);
            this.pnlBorde.Controls.Add(this.lblEtiquetaHabitacion);
            this.pnlBorde.Controls.Add(this.pnlFooter);
            this.pnlBorde.Controls.Add(this.pnlHeader);
            this.pnlBorde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorde.Location = new System.Drawing.Point(3, 3);
            this.pnlBorde.Name = "pnlBorde";
            this.pnlBorde.Size = new System.Drawing.Size(474, 434);
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
            this.lblTitulo.Text = "Renovar Estadía";
            // 
            // iconTitulo
            // 
            this.iconTitulo.IconChar = FontAwesome.Sharp.IconChar.ArrowRotateRight;
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
            // lblEtiquetaFechaActual
            // 
            this.lblEtiquetaFechaActual.AutoSize = true;
            this.lblEtiquetaFechaActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaFechaActual.Location = new System.Drawing.Point(20, 140);
            this.lblEtiquetaFechaActual.Name = "lblEtiquetaFechaActual";
            this.lblEtiquetaFechaActual.Size = new System.Drawing.Size(110, 15);
            this.lblEtiquetaFechaActual.TabIndex = 5;
            this.lblEtiquetaFechaActual.Text = "Egreso Actual:";
            // 
            // lblFechaActual
            // 
            this.lblFechaActual.AutoSize = true;
            this.lblFechaActual.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblFechaActual.Location = new System.Drawing.Point(140, 140);
            this.lblFechaActual.Name = "lblFechaActual";
            this.lblFechaActual.Size = new System.Drawing.Size(50, 15);
            this.lblFechaActual.TabIndex = 6;
            this.lblFechaActual.Text = "-";
            // 
            // lblEtiquetaTarifa
            // 
            this.lblEtiquetaTarifa.AutoSize = true;
            this.lblEtiquetaTarifa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaTarifa.Location = new System.Drawing.Point(20, 166);
            this.lblEtiquetaTarifa.Name = "lblEtiquetaTarifa";
            this.lblEtiquetaTarifa.Size = new System.Drawing.Size(95, 15);
            this.lblEtiquetaTarifa.TabIndex = 7;
            this.lblEtiquetaTarifa.Text = "Tarifa/noche:";
            // 
            // lblTarifa
            // 
            this.lblTarifa.AutoSize = true;
            this.lblTarifa.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTarifa.Location = new System.Drawing.Point(140, 166);
            this.lblTarifa.Name = "lblTarifa";
            this.lblTarifa.Size = new System.Drawing.Size(50, 15);
            this.lblTarifa.TabIndex = 8;
            this.lblTarifa.Text = "-";
            // 
            // pnlLinea
            // 
            this.pnlLinea.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLinea.Location = new System.Drawing.Point(20, 198);
            this.pnlLinea.Name = "pnlLinea";
            this.pnlLinea.Size = new System.Drawing.Size(434, 2);
            this.pnlLinea.TabIndex = 9;
            // 
            // lblEtiquetaNuevaFecha
            // 
            this.lblEtiquetaNuevaFecha.AutoSize = true;
            this.lblEtiquetaNuevaFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaNuevaFecha.Location = new System.Drawing.Point(20, 212);
            this.lblEtiquetaNuevaFecha.Name = "lblEtiquetaNuevaFecha";
            this.lblEtiquetaNuevaFecha.Size = new System.Drawing.Size(130, 15);
            this.lblEtiquetaNuevaFecha.TabIndex = 10;
            this.lblEtiquetaNuevaFecha.Text = "Nueva Fecha de Egreso";
            // 
            // dtpNuevaFechaEgreso
            // 
            this.dtpNuevaFechaEgreso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpNuevaFechaEgreso.BorderColor = System.Drawing.Color.Goldenrod;
            this.dtpNuevaFechaEgreso.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.dtpNuevaFechaEgreso.BorderWidth = 2;
            this.dtpNuevaFechaEgreso.CalendarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.dtpNuevaFechaEgreso.CalendarForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpNuevaFechaEgreso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpNuevaFechaEgreso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNuevaFechaEgreso.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpNuevaFechaEgreso.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this.dtpNuevaFechaEgreso.IconColor = System.Drawing.Color.Goldenrod;
            this.dtpNuevaFechaEgreso.IconSize = 22;
            this.dtpNuevaFechaEgreso.Location = new System.Drawing.Point(20, 230);
            this.dtpNuevaFechaEgreso.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNuevaFechaEgreso.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNuevaFechaEgreso.Name = "dtpNuevaFechaEgreso";
            this.dtpNuevaFechaEgreso.Size = new System.Drawing.Size(434, 40);
            this.dtpNuevaFechaEgreso.TabIndex = 11;
            this.dtpNuevaFechaEgreso.Value = null;
            // 
            // lblEtiquetaNoches
            // 
            this.lblEtiquetaNoches.AutoSize = true;
            this.lblEtiquetaNoches.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.lblEtiquetaNoches.Location = new System.Drawing.Point(20, 284);
            this.lblEtiquetaNoches.Name = "lblEtiquetaNoches";
            this.lblEtiquetaNoches.Size = new System.Drawing.Size(120, 15);
            this.lblEtiquetaNoches.TabIndex = 12;
            this.lblEtiquetaNoches.Text = "Noches adicionales:";
            // 
            // lblNochesAdicionales
            // 
            this.lblNochesAdicionales.AutoSize = true;
            this.lblNochesAdicionales.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblNochesAdicionales.Location = new System.Drawing.Point(200, 284);
            this.lblNochesAdicionales.Name = "lblNochesAdicionales";
            this.lblNochesAdicionales.Size = new System.Drawing.Size(50, 15);
            this.lblNochesAdicionales.TabIndex = 13;
            this.lblNochesAdicionales.Text = "-";
            // 
            // lblEtiquetaCostoAdicional
            // 
            this.lblEtiquetaCostoAdicional.AutoSize = true;
            this.lblEtiquetaCostoAdicional.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaCostoAdicional.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblEtiquetaCostoAdicional.Location = new System.Drawing.Point(20, 312);
            this.lblEtiquetaCostoAdicional.Name = "lblEtiquetaCostoAdicional";
            this.lblEtiquetaCostoAdicional.Size = new System.Drawing.Size(130, 20);
            this.lblEtiquetaCostoAdicional.TabIndex = 14;
            this.lblEtiquetaCostoAdicional.Text = "Costo Adicional:";
            // 
            // lblCostoAdicional
            // 
            this.lblCostoAdicional.AutoSize = true;
            this.lblCostoAdicional.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCostoAdicional.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblCostoAdicional.Location = new System.Drawing.Point(200, 310);
            this.lblCostoAdicional.Name = "lblCostoAdicional";
            this.lblCostoAdicional.Size = new System.Drawing.Size(80, 24);
            this.lblCostoAdicional.TabIndex = 15;
            this.lblCostoAdicional.Text = "$0";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlFooter.Controls.Add(this.btnConfirmar);
            this.pnlFooter.Controls.Add(this.btnCancelar);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 370);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(474, 64);
            this.pnlFooter.TabIndex = 16;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnConfirmar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnConfirmar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnConfirmar.IconSize = 22;
            this.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmar.Location = new System.Drawing.Point(214, 0);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnConfirmar.Size = new System.Drawing.Size(180, 64);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirmar.UseVisualStyleBackColor = false;
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
            // FormRenovarEstadia_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(480, 440);
            this.Controls.Add(this.pnlBorde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRenovarEstadia_68SA";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Renovar Estadía";
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
        private System.Windows.Forms.Label lblEtiquetaFechaActual;
        private System.Windows.Forms.Label lblFechaActual;
        private System.Windows.Forms.Label lblEtiquetaTarifa;
        private System.Windows.Forms.Label lblTarifa;
        private System.Windows.Forms.Panel pnlLinea;
        private System.Windows.Forms.Label lblEtiquetaNuevaFecha;
        private CustomControls.IconDateTimePicker dtpNuevaFechaEgreso;
        private System.Windows.Forms.Label lblEtiquetaNoches;
        private System.Windows.Forms.Label lblNochesAdicionales;
        private System.Windows.Forms.Label lblEtiquetaCostoAdicional;
        private System.Windows.Forms.Label lblCostoAdicional;
        private System.Windows.Forms.Panel pnlFooter;
        private FontAwesome.Sharp.IconButton btnConfirmar;
        private FontAwesome.Sharp.IconButton btnCancelar;
    }
}