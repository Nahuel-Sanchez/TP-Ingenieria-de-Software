namespace GUI_08YS
{
    partial class FormLogin_08YS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin_08YS));
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.dummyFocusTarget = new System.Windows.Forms.Label();
            this.btnAccederLogin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCambiarIdioma = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cerrar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.Gray;
            this.txtUsername.Location = new System.Drawing.Point(351, 81);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(228, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Text = "Username";
            this.txtUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUsername.Enter += new System.EventHandler(this.textBox1_Enter);
            this.txtUsername.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtPassword.Location = new System.Drawing.Point(351, 135);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(228, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "Contraseña";
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassword.Enter += new System.EventHandler(this.textBox2_Enter);
            this.txtPassword.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // dummyFocusTarget
            // 
            this.dummyFocusTarget.AutoSize = true;
            this.dummyFocusTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.dummyFocusTarget.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dummyFocusTarget.Location = new System.Drawing.Point(528, 30);
            this.dummyFocusTarget.Name = "dummyFocusTarget";
            this.dummyFocusTarget.Size = new System.Drawing.Size(0, 16);
            this.dummyFocusTarget.TabIndex = 6;
            this.dummyFocusTarget.Visible = false;
            // 
            // btnAccederLogin
            // 
            this.btnAccederLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAccederLogin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAccederLogin.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccederLogin.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnAccederLogin.Location = new System.Drawing.Point(351, 186);
            this.btnAccederLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAccederLogin.Name = "btnAccederLogin";
            this.btnAccederLogin.Size = new System.Drawing.Size(229, 39);
            this.btnAccederLogin.TabIndex = 3;
            this.btnAccederLogin.Text = " Acceder";
            this.btnAccederLogin.UseVisualStyleBackColor = false;
            this.btnAccederLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel1.Controls.Add(this.btnCambiarIdioma);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 256);
            this.panel1.TabIndex = 4;
            // 
            // btnCambiarIdioma
            // 
            this.btnCambiarIdioma.BackColor = System.Drawing.Color.DarkBlue;
            this.btnCambiarIdioma.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCambiarIdioma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarIdioma.Font = new System.Drawing.Font("Myanmar Text", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarIdioma.ForeColor = System.Drawing.SystemColors.Window;
            this.btnCambiarIdioma.Location = new System.Drawing.Point(0, 225);
            this.btnCambiarIdioma.Margin = new System.Windows.Forms.Padding(4);
            this.btnCambiarIdioma.Name = "btnCambiarIdioma";
            this.btnCambiarIdioma.Size = new System.Drawing.Size(267, 31);
            this.btnCambiarIdioma.TabIndex = 11;
            this.btnCambiarIdioma.Text = "Cambiar Idioma";
            this.btnCambiarIdioma.UseVisualStyleBackColor = false;
            this.btnCambiarIdioma.Click += new System.EventHandler(this.btnCambiarIdioma_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(265, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("MS PGothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblLogin.Location = new System.Drawing.Point(416, 9);
            this.lblLogin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(105, 40);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "Login";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(349, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(231, 1);
            this.label3.TabIndex = 5;
            this.label3.Text = "__________________";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(349, 159);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 1);
            this.label4.TabIndex = 7;
            this.label4.Text = "__________________";
            // 
            // Cerrar
            // 
            this.Cerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Cerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cerrar.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cerrar.ForeColor = System.Drawing.Color.Silver;
            this.Cerrar.Location = new System.Drawing.Point(624, 0);
            this.Cerrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cerrar.Name = "Cerrar";
            this.Cerrar.Size = new System.Drawing.Size(37, 33);
            this.Cerrar.TabIndex = 8;
            this.Cerrar.Text = "X";
            this.Cerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cerrar.UseVisualStyleBackColor = false;
            this.Cerrar.Click += new System.EventHandler(this.Cerrar_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(661, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0, 256);
            this.panel2.TabIndex = 9;
            // 
            // FormLogin_08YS
            // 
            this.AcceptButton = this.btnAccederLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.CancelButton = this.Cerrar;
            this.ClientSize = new System.Drawing.Size(661, 256);
            this.Controls.Add(this.Cerrar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAccederLogin);
            this.Controls.Add(this.dummyFocusTarget);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormLogin_08YS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLogin";
            this.Load += new System.EventHandler(this.FormLogin_Load_1);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label dummyFocusTarget;
        private System.Windows.Forms.Button btnAccederLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Cerrar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCambiarIdioma;
    }
}