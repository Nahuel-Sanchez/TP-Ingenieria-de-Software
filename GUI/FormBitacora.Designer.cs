namespace GUI
{
    partial class FormBitacora
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtCriticidad = new System.Windows.Forms.TextBox();
            this.txtEvento = new System.Windows.Forms.TextBox();
            this.txtModulo = new System.Windows.Forms.TextBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblCriticidad = new System.Windows.Forms.Label();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblModulo = new System.Windows.Forms.Label();
            this.lblFechaIni = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblApellido = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.dgvEventos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCriticidad
            // 
            this.txtCriticidad.Location = new System.Drawing.Point(662, 423);
            this.txtCriticidad.Name = "txtCriticidad";
            this.txtCriticidad.Size = new System.Drawing.Size(130, 20);
            this.txtCriticidad.TabIndex = 49;
            // 
            // txtEvento
            // 
            this.txtEvento.Location = new System.Drawing.Point(398, 419);
            this.txtEvento.Name = "txtEvento";
            this.txtEvento.Size = new System.Drawing.Size(130, 20);
            this.txtEvento.TabIndex = 48;
            // 
            // txtModulo
            // 
            this.txtModulo.Location = new System.Drawing.Point(134, 419);
            this.txtModulo.Name = "txtModulo";
            this.txtModulo.Size = new System.Drawing.Size(130, 20);
            this.txtModulo.TabIndex = 47;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Location = new System.Drawing.Point(662, 366);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(127, 20);
            this.dtpFechaFin.TabIndex = 46;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Location = new System.Drawing.Point(398, 371);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(127, 20);
            this.dtpFechaIni.TabIndex = 45;
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(134, 370);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(130, 20);
            this.txtLogin.TabIndex = 44;
            // 
            // lblCriticidad
            // 
            this.lblCriticidad.AutoSize = true;
            this.lblCriticidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriticidad.Location = new System.Drawing.Point(576, 423);
            this.lblCriticidad.Name = "lblCriticidad";
            this.lblCriticidad.Size = new System.Drawing.Size(66, 16);
            this.lblCriticidad.TabIndex = 43;
            this.lblCriticidad.Text = "Criticidad:";
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFin.Location = new System.Drawing.Point(576, 370);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(69, 16);
            this.lblFechaFin.TabIndex = 42;
            this.lblFechaFin.Text = "Fecha Fin:";
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEvento.Location = new System.Drawing.Point(309, 423);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(52, 16);
            this.lblEvento.TabIndex = 41;
            this.lblEvento.Text = "Evento:";
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = true;
            this.lblModulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModulo.Location = new System.Drawing.Point(47, 423);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(55, 16);
            this.lblModulo.TabIndex = 40;
            this.lblModulo.Text = "Modulo:";
            // 
            // lblFechaIni
            // 
            this.lblFechaIni.AutoSize = true;
            this.lblFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaIni.Location = new System.Drawing.Point(309, 370);
            this.lblFechaIni.Name = "lblFechaIni";
            this.lblFechaIni.Size = new System.Drawing.Size(64, 16);
            this.lblFechaIni.TabIndex = 39;
            this.lblFechaIni.Text = "Fecha Ini:";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.Location = new System.Drawing.Point(47, 370);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(43, 16);
            this.lblLogin.TabIndex = 38;
            this.lblLogin.Text = "Login:";
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellido.Location = new System.Drawing.Point(453, 324);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(60, 16);
            this.lblApellido.TabIndex = 37;
            this.lblApellido.Text = "Apellido:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(175, 324);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(59, 16);
            this.lblNombre.TabIndex = 36;
            this.lblNombre.Text = "Nombre:";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(579, 484);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(98, 46);
            this.btnImprimir.TabIndex = 35;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.Location = new System.Drawing.Point(312, 484);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(98, 46);
            this.btnAplicar.TabIndex = 34;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(50, 484);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(98, 46);
            this.btnLimpiar.TabIndex = 33;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(742, 60);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(98, 46);
            this.btnSalir.TabIndex = 32;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // dgvEventos
            // 
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventos.Location = new System.Drawing.Point(50, 12);
            this.dgvEventos.Name = "dgvEventos";
            this.dgvEventos.ReadOnly = true;
            this.dgvEventos.Size = new System.Drawing.Size(646, 272);
            this.dgvEventos.TabIndex = 31;
            // 
            // FormBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 565);
            this.Controls.Add(this.txtCriticidad);
            this.Controls.Add(this.txtEvento);
            this.Controls.Add(this.txtModulo);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.dtpFechaIni);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblCriticidad);
            this.Controls.Add(this.lblFechaFin);
            this.Controls.Add(this.lblEvento);
            this.Controls.Add(this.lblModulo);
            this.Controls.Add(this.lblFechaIni);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.dgvEventos);
            this.Name = "FormBitacora";
            this.Text = "FormBitacora";
            this.Load += new System.EventHandler(this.FormBitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCriticidad;
        private System.Windows.Forms.TextBox txtEvento;
        private System.Windows.Forms.TextBox txtModulo;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblCriticidad;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.Label lblFechaIni;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridView dgvEventos;
    }
}