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
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblCriticidad = new System.Windows.Forms.Label();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblModulo = new System.Windows.Forms.Label();
            this.lblFechaIni = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvEventos = new System.Windows.Forms.DataGridView();
            this.comboBoxModulo = new System.Windows.Forms.ComboBox();
            this.comboBoxEvento = new System.Windows.Forms.ComboBox();
            this.comboBoxCriticidad = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(662, 370);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(132, 20);
            this.dtpHasta.TabIndex = 46;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(398, 370);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(132, 20);
            this.dtpDesde.TabIndex = 45;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(134, 370);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(130, 20);
            this.txtUsername.TabIndex = 44;
            // 
            // lblCriticidad
            // 
            this.lblCriticidad.AutoSize = true;
            this.lblCriticidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriticidad.Location = new System.Drawing.Point(555, 423);
            this.lblCriticidad.Name = "lblCriticidad";
            this.lblCriticidad.Size = new System.Drawing.Size(66, 16);
            this.lblCriticidad.TabIndex = 43;
            this.lblCriticidad.Text = "Criticidad:";
            this.lblCriticidad.Click += new System.EventHandler(this.lblCriticidad_Click);
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFin.Location = new System.Drawing.Point(555, 371);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(87, 16);
            this.lblFechaFin.TabIndex = 42;
            this.lblFechaFin.Text = "Fecha Hasta:";
            this.lblFechaFin.Click += new System.EventHandler(this.lblFechaFin_Click);
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEvento.Location = new System.Drawing.Point(300, 424);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(52, 16);
            this.lblEvento.TabIndex = 41;
            this.lblEvento.Text = "Evento:";
            this.lblEvento.Click += new System.EventHandler(this.lblEvento_Click);
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
            this.lblModulo.Click += new System.EventHandler(this.lblModulo_Click);
            // 
            // lblFechaIni
            // 
            this.lblFechaIni.AutoSize = true;
            this.lblFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaIni.Location = new System.Drawing.Point(300, 371);
            this.lblFechaIni.Name = "lblFechaIni";
            this.lblFechaIni.Size = new System.Drawing.Size(92, 16);
            this.lblFechaIni.TabIndex = 39;
            this.lblFechaIni.Text = "Fecha Desde:";
            this.lblFechaIni.Click += new System.EventHandler(this.lblFechaIni_Click);
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.Location = new System.Drawing.Point(47, 370);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(73, 16);
            this.lblLogin.TabIndex = 38;
            this.lblLogin.Text = "Username:";
            this.lblLogin.Click += new System.EventHandler(this.lblLogin_Click);
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
            this.lblNombre.Click += new System.EventHandler(this.lblNombre_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(680, 484);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(98, 46);
            this.btnImprimir.TabIndex = 35;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(373, 484);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(98, 46);
            this.btnFiltrar.TabIndex = 34;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(50, 484);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(98, 46);
            this.btnLimpiar.TabIndex = 33;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // dgvEventos
            // 
            this.dgvEventos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventos.Location = new System.Drawing.Point(50, 12);
            this.dgvEventos.Name = "dgvEventos";
            this.dgvEventos.ReadOnly = true;
            this.dgvEventos.RowHeadersWidth = 51;
            this.dgvEventos.Size = new System.Drawing.Size(646, 272);
            this.dgvEventos.TabIndex = 31;
            this.dgvEventos.SelectionChanged += new System.EventHandler(this.dgvEventos_SelectionChanged);
            // 
            // comboBoxModulo
            // 
            this.comboBoxModulo.FormattingEnabled = true;
            this.comboBoxModulo.Location = new System.Drawing.Point(134, 423);
            this.comboBoxModulo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxModulo.Name = "comboBoxModulo";
            this.comboBoxModulo.Size = new System.Drawing.Size(130, 21);
            this.comboBoxModulo.TabIndex = 50;
            // 
            // comboBoxEvento
            // 
            this.comboBoxEvento.FormattingEnabled = true;
            this.comboBoxEvento.Location = new System.Drawing.Point(398, 423);
            this.comboBoxEvento.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxEvento.Name = "comboBoxEvento";
            this.comboBoxEvento.Size = new System.Drawing.Size(132, 21);
            this.comboBoxEvento.TabIndex = 51;
            // 
            // comboBoxCriticidad
            // 
            this.comboBoxCriticidad.FormattingEnabled = true;
            this.comboBoxCriticidad.Location = new System.Drawing.Point(662, 423);
            this.comboBoxCriticidad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxCriticidad.Name = "comboBoxCriticidad";
            this.comboBoxCriticidad.Size = new System.Drawing.Size(132, 21);
            this.comboBoxCriticidad.TabIndex = 52;
            // 
            // FormBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(870, 565);
            this.Controls.Add(this.comboBoxCriticidad);
            this.Controls.Add(this.comboBoxEvento);
            this.Controls.Add(this.comboBoxModulo);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblCriticidad);
            this.Controls.Add(this.lblFechaFin);
            this.Controls.Add(this.lblEvento);
            this.Controls.Add(this.lblModulo);
            this.Controls.Add(this.lblFechaIni);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvEventos);
            this.Name = "FormBitacora";
            this.Text = " ";
            this.Load += new System.EventHandler(this.FormBitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblCriticidad;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.Label lblFechaIni;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvEventos;
        private System.Windows.Forms.ComboBox comboBoxModulo;
        private System.Windows.Forms.ComboBox comboBoxEvento;
        private System.Windows.Forms.ComboBox comboBoxCriticidad;
    }
}