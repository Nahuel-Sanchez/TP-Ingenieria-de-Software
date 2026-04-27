namespace GUI
{
    partial class FormGestionUsuarios
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
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.rbBloqueados = new System.Windows.Forms.RadioButton();
            this.lblActivo = new System.Windows.Forms.Label();
            this.lblBloqueado = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.lblNombres = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblApellidos = new System.Windows.Forms.Label();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtActivo = new System.Windows.Forms.TextBox();
            this.txtBloqueado = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnActDes = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnDesbloquear = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.rbActivos = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.lblCantUsuarios = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbRol
            // 
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Items.AddRange(new object[] {
            "Admin",
            "Basico"});
            this.cmbRol.Location = new System.Drawing.Point(222, 568);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(182, 21);
            this.cmbRol.TabIndex = 61;
            // 
            // rbBloqueados
            // 
            this.rbBloqueados.AutoSize = true;
            this.rbBloqueados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBloqueados.Location = new System.Drawing.Point(540, 28);
            this.rbBloqueados.Name = "rbBloqueados";
            this.rbBloqueados.Size = new System.Drawing.Size(99, 20);
            this.rbBloqueados.TabIndex = 60;
            this.rbBloqueados.TabStop = true;
            this.rbBloqueados.Text = "Bloqueados";
            this.rbBloqueados.UseVisualStyleBackColor = true;
            // 
            // lblActivo
            // 
            this.lblActivo.AutoSize = true;
            this.lblActivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivo.Location = new System.Drawing.Point(127, 680);
            this.lblActivo.Name = "lblActivo";
            this.lblActivo.Size = new System.Drawing.Size(44, 16);
            this.lblActivo.TabIndex = 59;
            this.lblActivo.Text = "Activo";
            // 
            // lblBloqueado
            // 
            this.lblBloqueado.AutoSize = true;
            this.lblBloqueado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBloqueado.Location = new System.Drawing.Point(127, 644);
            this.lblBloqueado.Name = "lblBloqueado";
            this.lblBloqueado.Size = new System.Drawing.Size(74, 16);
            this.lblBloqueado.TabIndex = 58;
            this.lblBloqueado.Text = "Bloqueado";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.Location = new System.Drawing.Point(127, 608);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(40, 16);
            this.lblLogin.TabIndex = 57;
            this.lblLogin.Text = "Login";
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRol.Location = new System.Drawing.Point(127, 569);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(28, 16);
            this.lblRol.TabIndex = 56;
            this.lblRol.Text = "Rol";
            // 
            // lblNombres
            // 
            this.lblNombres.AutoSize = true;
            this.lblNombres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombres.Location = new System.Drawing.Point(127, 494);
            this.lblNombres.Name = "lblNombres";
            this.lblNombres.Size = new System.Drawing.Size(63, 16);
            this.lblNombres.TabIndex = 55;
            this.lblNombres.Text = "Nombres";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(127, 530);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(41, 16);
            this.lblEmail.TabIndex = 54;
            this.lblEmail.Text = "Email";
            // 
            // lblApellidos
            // 
            this.lblApellidos.AutoSize = true;
            this.lblApellidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellidos.Location = new System.Drawing.Point(127, 454);
            this.lblApellidos.Name = "lblApellidos";
            this.lblApellidos.Size = new System.Drawing.Size(64, 16);
            this.lblApellidos.TabIndex = 53;
            this.lblApellidos.Text = "Apellidos";
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI.Location = new System.Drawing.Point(127, 409);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(30, 16);
            this.lblDNI.TabIndex = 52;
            this.lblDNI.Text = "DNI";
            // 
            // txtActivo
            // 
            this.txtActivo.Location = new System.Drawing.Point(222, 673);
            this.txtActivo.Name = "txtActivo";
            this.txtActivo.Size = new System.Drawing.Size(182, 20);
            this.txtActivo.TabIndex = 51;
            // 
            // txtBloqueado
            // 
            this.txtBloqueado.Location = new System.Drawing.Point(222, 637);
            this.txtBloqueado.Name = "txtBloqueado";
            this.txtBloqueado.Size = new System.Drawing.Size(182, 20);
            this.txtBloqueado.TabIndex = 50;
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(222, 601);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(182, 20);
            this.txtLogin.TabIndex = 49;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(222, 523);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(182, 20);
            this.txtEmail.TabIndex = 48;
            // 
            // txtNombres
            // 
            this.txtNombres.Location = new System.Drawing.Point(222, 487);
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(182, 20);
            this.txtNombres.TabIndex = 47;
            // 
            // txtApellidos
            // 
            this.txtApellidos.Location = new System.Drawing.Point(222, 451);
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.Size = new System.Drawing.Size(182, 20);
            this.txtApellidos.TabIndex = 46;
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(222, 409);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(182, 20);
            this.txtDNI.TabIndex = 45;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(540, 421);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(227, 185);
            this.textBox1.TabIndex = 44;
            this.textBox1.Text = "                            Mensaje:";
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(868, 601);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(98, 46);
            this.btnSalir.TabIndex = 43;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(868, 513);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 46);
            this.btnCancelar.TabIndex = 42;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.Location = new System.Drawing.Point(868, 421);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(98, 46);
            this.btnAplicar.TabIndex = 41;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            // 
            // btnActDes
            // 
            this.btnActDes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActDes.Location = new System.Drawing.Point(868, 328);
            this.btnActDes.Name = "btnActDes";
            this.btnActDes.Size = new System.Drawing.Size(98, 46);
            this.btnActDes.TabIndex = 40;
            this.btnActDes.Text = "Act./Desact.";
            this.btnActDes.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            this.btnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Location = new System.Drawing.Point(868, 237);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(98, 46);
            this.btnModificar.TabIndex = 39;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnDesbloquear
            // 
            this.btnDesbloquear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesbloquear.Location = new System.Drawing.Point(868, 145);
            this.btnDesbloquear.Name = "btnDesbloquear";
            this.btnDesbloquear.Size = new System.Drawing.Size(98, 46);
            this.btnDesbloquear.TabIndex = 38;
            this.btnDesbloquear.Text = "Desbloquear";
            this.btnDesbloquear.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            this.btnCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrear.Location = new System.Drawing.Point(868, 61);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(98, 46);
            this.btnCrear.TabIndex = 37;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // rbActivos
            // 
            this.rbActivos.AutoSize = true;
            this.rbActivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbActivos.Location = new System.Drawing.Point(442, 28);
            this.rbActivos.Name = "rbActivos";
            this.rbActivos.Size = new System.Drawing.Size(69, 20);
            this.rbActivos.TabIndex = 36;
            this.rbActivos.TabStop = true;
            this.rbActivos.Text = "Activos";
            this.rbActivos.UseVisualStyleBackColor = true;
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTodos.Location = new System.Drawing.Point(340, 28);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(65, 20);
            this.rbTodos.TabIndex = 35;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Location = new System.Drawing.Point(93, 79);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.Size = new System.Drawing.Size(674, 307);
            this.dgvUsuarios.TabIndex = 34;
            // 
            // lblCantUsuarios
            // 
            this.lblCantUsuarios.AutoSize = true;
            this.lblCantUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantUsuarios.Location = new System.Drawing.Point(658, 28);
            this.lblCantUsuarios.Name = "lblCantUsuarios";
            this.lblCantUsuarios.Size = new System.Drawing.Size(137, 16);
            this.lblCantUsuarios.TabIndex = 33;
            this.lblCantUsuarios.Text = "Numero de Usuarios: ";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(105, 28);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(97, 25);
            this.lblTitulo.TabIndex = 32;
            this.lblTitulo.Text = "Usuarios";
            // 
            // FormGestionUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 724);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.rbBloqueados);
            this.Controls.Add(this.lblActivo);
            this.Controls.Add(this.lblBloqueado);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.lblNombres);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblApellidos);
            this.Controls.Add(this.lblDNI);
            this.Controls.Add(this.txtActivo);
            this.Controls.Add(this.txtBloqueado);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNombres);
            this.Controls.Add(this.txtApellidos);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnActDes);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnDesbloquear);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.rbActivos);
            this.Controls.Add(this.rbTodos);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.lblCantUsuarios);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormGestionUsuarios";
            this.Text = "FormGestionUsuarios";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.RadioButton rbBloqueados;
        private System.Windows.Forms.Label lblActivo;
        private System.Windows.Forms.Label lblBloqueado;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Label lblNombres;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblApellidos;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtActivo;
        private System.Windows.Forms.TextBox txtBloqueado;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnActDes;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnDesbloquear;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.RadioButton rbActivos;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Label lblCantUsuarios;
        private System.Windows.Forms.Label lblTitulo;
    }
}