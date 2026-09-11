namespace GUI_08YS.Recepcionista
{
    partial class UC_HabitacionCard_68SA
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTarjeta = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblCapacidad = new System.Windows.Forms.Label();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.pnlEstado = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblEstado = new System.Windows.Forms.Label();
            this.pnlPunto = new GUI_08YS.UserControls.RoundedPanel_68SA();
            this.lblTipo = new System.Windows.Forms.Label();
            this.pnlLinea = new System.Windows.Forms.Panel();
            this.lblNumero = new System.Windows.Forms.Label();
            this.lblEtiqueta = new System.Windows.Forms.Label();
            this.pnlTarjeta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.pnlEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTarjeta
            // 
            this.pnlTarjeta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.pnlTarjeta.Controls.Add(this.lblCapacidad);
            this.pnlTarjeta.Controls.Add(this.iconPictureBox1);
            this.pnlTarjeta.Controls.Add(this.pnlEstado);
            this.pnlTarjeta.Controls.Add(this.lblTipo);
            this.pnlTarjeta.Controls.Add(this.pnlLinea);
            this.pnlTarjeta.Controls.Add(this.lblNumero);
            this.pnlTarjeta.Controls.Add(this.lblEtiqueta);
            this.pnlTarjeta.CornerRadius = 16;
            this.pnlTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjeta.Location = new System.Drawing.Point(0, 0);
            this.pnlTarjeta.Name = "pnlTarjeta";
            this.pnlTarjeta.Padding = new System.Windows.Forms.Padding(8, 0, 8, 6);
            this.pnlTarjeta.Size = new System.Drawing.Size(260, 160);
            this.pnlTarjeta.TabIndex = 0;
            // 
            // lblCapacidad
            // 
            this.lblCapacidad.AutoSize = true;
            this.lblCapacidad.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblCapacidad.Location = new System.Drawing.Point(174, 94);
            this.lblCapacidad.Name = "lblCapacidad";
            this.lblCapacidad.Size = new System.Drawing.Size(45, 16);
            this.lblCapacidad.TabIndex = 6;
            this.lblCapacidad.Text = "Max: 0";
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(18)))), ((int)(((byte)(50)))));
            this.iconPictureBox1.ForeColor = System.Drawing.Color.Gold;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.iconPictureBox1.IconColor = System.Drawing.Color.Gold;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 73;
            this.iconPictureBox1.Location = new System.Drawing.Point(174, 12);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(75, 73);
            this.iconPictureBox1.TabIndex = 5;
            this.iconPictureBox1.TabStop = false;
            // 
            // pnlEstado
            // 
            this.pnlEstado.Controls.Add(this.lblEstado);
            this.pnlEstado.Controls.Add(this.pnlPunto);
            this.pnlEstado.CornerRadius = 15;
            this.pnlEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEstado.Location = new System.Drawing.Point(8, 124);
            this.pnlEstado.Name = "pnlEstado";
            this.pnlEstado.Size = new System.Drawing.Size(244, 30);
            this.pnlEstado.TabIndex = 4;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblEstado.Location = new System.Drawing.Point(36, 5);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(52, 18);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.Text = "label1";
            // 
            // pnlPunto
            // 
            this.pnlPunto.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlPunto.CornerRadius = 5;
            this.pnlPunto.Location = new System.Drawing.Point(20, 10);
            this.pnlPunto.Name = "pnlPunto";
            this.pnlPunto.Size = new System.Drawing.Size(10, 10);
            this.pnlPunto.TabIndex = 0;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTipo.Location = new System.Drawing.Point(15, 97);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(131, 18);
            this.lblTipo.TabIndex = 3;
            this.lblTipo.Text = "Tipo de Habitacion";
            // 
            // pnlLinea
            // 
            this.pnlLinea.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlLinea.Location = new System.Drawing.Point(16, 88);
            this.pnlLinea.Name = "pnlLinea";
            this.pnlLinea.Size = new System.Drawing.Size(24, 2);
            this.pnlLinea.TabIndex = 2;
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumero.ForeColor = System.Drawing.Color.Gold;
            this.lblNumero.Location = new System.Drawing.Point(4, 30);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(97, 51);
            this.lblNumero.TabIndex = 1;
            this.lblNumero.Text = "101";
            // 
            // lblEtiqueta
            // 
            this.lblEtiqueta.AutoSize = true;
            this.lblEtiqueta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiqueta.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblEtiqueta.Location = new System.Drawing.Point(12, 10);
            this.lblEtiqueta.Name = "lblEtiqueta";
            this.lblEtiqueta.Size = new System.Drawing.Size(78, 18);
            this.lblEtiqueta.TabIndex = 0;
            this.lblEtiqueta.Text = "Habitación";
            // 
            // UC_HabitacionCard_68SA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlTarjeta);
            this.Name = "UC_HabitacionCard_68SA";
            this.Size = new System.Drawing.Size(260, 160);
            this.pnlTarjeta.ResumeLayout(false);
            this.pnlTarjeta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.pnlEstado.ResumeLayout(false);
            this.pnlEstado.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.RoundedPanel_68SA pnlTarjeta;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Label lblEtiqueta;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Panel pnlLinea;
        private UserControls.RoundedPanel_68SA pnlEstado;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.Label lblEstado;
        private UserControls.RoundedPanel_68SA pnlPunto;
        private System.Windows.Forms.Label lblCapacidad;
    }
}
