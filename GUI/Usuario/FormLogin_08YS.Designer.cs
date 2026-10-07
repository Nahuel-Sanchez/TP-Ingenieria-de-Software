namespace GUI_08YS
{
    partial class FormLogin_08YS
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
            this.pnlFormulario = new System.Windows.Forms.Panel();
            this.lblPie = new System.Windows.Forms.Label();
            this.btn_Acceder = new FontAwesome.Sharp.IconButton();
            this.lblBloqMayus = new System.Windows.Forms.Label();
            this.txtPassword = new CustomControls.IconPlaceholderTextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtUsername = new CustomControls.IconPlaceholderTextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.pnlAcento = new System.Windows.Forms.Panel();
            this.IdiomaCombobox = new CustomControls.IconComboBox();
            this.btnCerrar = new FontAwesome.Sharp.IconButton();
            this.btnMinimizar = new FontAwesome.Sharp.IconButton();
            this.pnlMarca = new GUI_08YS.UserControls.PanelImagen_68SA();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblMarcaDetalle = new System.Windows.Forms.Label();
            this.lblDescripcionLogin = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlFormulario.SuspendLayout();
            this.pnlAcento.SuspendLayout();
            this.pnlMarca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFormulario
            // 
            this.pnlFormulario.Controls.Add(this.lblPie);
            this.pnlFormulario.Controls.Add(this.btn_Acceder);
            this.pnlFormulario.Controls.Add(this.lblBloqMayus);
            this.pnlFormulario.Controls.Add(this.txtPassword);
            this.pnlFormulario.Controls.Add(this.lblContrasena);
            this.pnlFormulario.Controls.Add(this.txtUsername);
            this.pnlFormulario.Controls.Add(this.lblUsuario);
            this.pnlFormulario.Controls.Add(this.lblSubtitulo);
            this.pnlFormulario.Controls.Add(this.lblLogin);
            this.pnlFormulario.Controls.Add(this.pnlAcento);
            this.pnlFormulario.Controls.Add(this.IdiomaCombobox);
            this.pnlFormulario.Controls.Add(this.btnCerrar);
            this.pnlFormulario.Controls.Add(this.btnMinimizar);
            this.pnlFormulario.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.pnlFormulario.BackColor = System.Drawing.Color.FromArgb(8, 14, 40);
            this.pnlFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormulario.Size = new System.Drawing.Size(558, 598);
            this.pnlFormulario.Name = "pnlFormulario";
            // 
            // lblPie
            // 
            this.lblPie.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblPie.ForeColor = System.Drawing.Color.FromArgb(110, 118, 140);
            this.lblPie.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPie.Location = new System.Drawing.Point(90, 552);
            this.lblPie.Size = new System.Drawing.Size(380, 20);
            this.lblPie.Tag = "Login_lblPie";
            this.lblPie.Text = "Acceso exclusivo para el personal autorizado del hotel";
            this.lblPie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPie.Name = "lblPie";
            // 
            // btn_Acceder
            // 
            this.btn_Acceder.Click += new System.EventHandler(this.btnAcceder_Click);
            this.btn_Acceder.BackColor = System.Drawing.Color.Goldenrod;
            this.btn_Acceder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Acceder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Acceder.ForeColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btn_Acceder.IconChar = FontAwesome.Sharp.IconChar.RightToBracket;
            this.btn_Acceder.IconColor = System.Drawing.Color.FromArgb(10, 15, 35);
            this.btn_Acceder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_Acceder.IconSize = 22;
            this.btn_Acceder.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Acceder.Padding = new System.Windows.Forms.Padding(0);
            this.btn_Acceder.Size = new System.Drawing.Size(380, 52);
            this.btn_Acceder.Tag = "Login_btn_Acceder";
            this.btn_Acceder.Text = "Acceder";
            this.btn_Acceder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Acceder.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_Acceder.UseVisualStyleBackColor = false;
            this.btn_Acceder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Acceder.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btn_Acceder.Location = new System.Drawing.Point(90, 414);
            this.btn_Acceder.TabIndex = 3;
            this.btn_Acceder.FlatAppearance.BorderSize = 0;
            this.btn_Acceder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btn_Acceder.Name = "btn_Acceder";
            // 
            // lblBloqMayus
            // 
            this.lblBloqMayus.AutoSize = true;
            this.lblBloqMayus.ForeColor = System.Drawing.Color.FromArgb(240, 180, 60);
            this.lblBloqMayus.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBloqMayus.Location = new System.Drawing.Point(92, 382);
            this.lblBloqMayus.Tag = "Login_lblBloqMayus";
            this.lblBloqMayus.Text = "Bloq Mayús está activado";
            this.lblBloqMayus.Visible = false;
            this.lblBloqMayus.Name = "lblBloqMayus";
            // 
            // txtPassword
            // 
            this.txtPassword.IconRightClick += new System.EventHandler(this.txtPassword_IconRightClick);
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.txtPassword.BorderColor = System.Drawing.Color.FromArgb(70, 82, 120);
            this.txtPassword.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtPassword.BorderWidth = 2;
            this.txtPassword.CornerRadius = 10;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPassword.IconColor = System.Drawing.Color.Goldenrod;
            this.txtPassword.IconColorRight = System.Drawing.Color.Goldenrod;
            this.txtPassword.IconPadding = 10;
            this.txtPassword.IconSize = 20;
            this.txtPassword.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassword.Size = new System.Drawing.Size(380, 48);
            this.txtPassword.IconChar = FontAwesome.Sharp.IconChar.Lock;
            this.txtPassword.IconCharRight = FontAwesome.Sharp.IconChar.EyeSlash;
            this.txtPassword.IconSizeRight = 22;
            this.txtPassword.IconPaddingRight = 12;
            this.txtPassword.Location = new System.Drawing.Point(90, 328);
            this.txtPassword.MaskedInput = true;
            this.txtPassword.PlaceholderText = "Contraseña";
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Tag = "Login_txtPassword";
            this.txtPassword.Name = "txtPassword";
            // 
            // lblContrasena
            // 
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.ForeColor = System.Drawing.Color.FromArgb(185, 190, 205);
            this.lblContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContrasena.Tag = "Login_lblContrasena";
            this.lblContrasena.Text = "Contraseña";
            this.lblContrasena.Location = new System.Drawing.Point(92, 306);
            this.lblContrasena.Name = "lblContrasena";
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.txtUsername.BorderColor = System.Drawing.Color.FromArgb(70, 82, 120);
            this.txtUsername.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.txtUsername.BorderWidth = 2;
            this.txtUsername.CornerRadius = 10;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtUsername.IconColor = System.Drawing.Color.Goldenrod;
            this.txtUsername.IconColorRight = System.Drawing.Color.Goldenrod;
            this.txtUsername.IconPadding = 10;
            this.txtUsername.IconSize = 20;
            this.txtUsername.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUsername.Size = new System.Drawing.Size(380, 48);
            this.txtUsername.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtUsername.Location = new System.Drawing.Point(90, 246);
            this.txtUsername.PlaceholderText = "Usuario";
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Tag = "Login_txtUsername";
            this.txtUsername.Name = "txtUsername";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(185, 190, 205);
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Tag = "Login_lblUsuario";
            this.lblUsuario.Text = "Usuario";
            this.lblUsuario.Location = new System.Drawing.Point(92, 224);
            this.lblUsuario.Name = "lblUsuario";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(150, 156, 175);
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.Location = new System.Drawing.Point(92, 166);
            this.lblSubtitulo.Size = new System.Drawing.Size(380, 40);
            this.lblSubtitulo.Tag = "Login_lblSubtitulo";
            this.lblSubtitulo.Text = "Ingresá con tu usuario para continuar.";
            this.lblSubtitulo.Name = "lblSubtitulo";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblLogin.Location = new System.Drawing.Point(86, 112);
            this.lblLogin.Tag = "Login_lblLogin";
            this.lblLogin.Text = "Bienvenido";
            this.lblLogin.Name = "lblLogin";
            // 
            // pnlAcento
            // 
            this.pnlAcento.BackColor = System.Drawing.Color.Goldenrod;
            this.pnlAcento.Location = new System.Drawing.Point(92, 100);
            this.pnlAcento.Size = new System.Drawing.Size(44, 4);
            this.pnlAcento.Name = "pnlAcento";
            // 
            // IdiomaCombobox
            // 
            this.IdiomaCombobox.SelectedIndexChanged += new System.EventHandler(this.IdiomaComboBox_SelectedIndexChanged);
            this.IdiomaCombobox.BackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.IdiomaCombobox.BorderColor = System.Drawing.Color.FromArgb(70, 82, 120);
            this.IdiomaCombobox.BorderFocusColor = System.Drawing.Color.Goldenrod;
            this.IdiomaCombobox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IdiomaCombobox.DropDownBackColor = System.Drawing.Color.FromArgb(14, 22, 54);
            this.IdiomaCombobox.DropDownBorderColor = System.Drawing.Color.Goldenrod;
            this.IdiomaCombobox.DropDownForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.IdiomaCombobox.DropDownHighlightBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.IdiomaCombobox.DropDownHighlightForeColor = System.Drawing.Color.Goldenrod;
            this.IdiomaCombobox.DropDownItemHeight = 32;
            this.IdiomaCombobox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdiomaCombobox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.IdiomaCombobox.IconChar = FontAwesome.Sharp.IconChar.Earth;
            this.IdiomaCombobox.IconColor = System.Drawing.Color.Goldenrod;
            this.IdiomaCombobox.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.IdiomaCombobox.IconPadding = 6;
            this.IdiomaCombobox.IconSize = 20;
            this.IdiomaCombobox.Location = new System.Drawing.Point(24, 18);
            this.IdiomaCombobox.SelectedItem = null;
            this.IdiomaCombobox.SelectedValue = null;
            this.IdiomaCombobox.Size = new System.Drawing.Size(160, 34);
            this.IdiomaCombobox.TabIndex = 4;
            this.IdiomaCombobox.Name = "IdiomaCombobox";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Click += new System.EventHandler(this.button1_Click_1);
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(8, 14, 40);
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(185, 190, 205);
            this.btnCerrar.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnCerrar.IconColor = System.Drawing.Color.FromArgb(185, 190, 205);
            this.btnCerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCerrar.IconSize = 18;
            this.btnCerrar.Location = new System.Drawing.Point(512, 0);
            this.btnCerrar.Size = new System.Drawing.Size(46, 40);
            this.btnCerrar.TabStop = false;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.Name = "btnCerrar";
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Click += new System.EventHandler(this.button3_Click);
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnMinimizar.BackColor = System.Drawing.Color.FromArgb(8, 14, 40);
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.ForeColor = System.Drawing.Color.FromArgb(185, 190, 205);
            this.btnMinimizar.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.btnMinimizar.IconColor = System.Drawing.Color.FromArgb(185, 190, 205);
            this.btnMinimizar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMinimizar.IconSize = 18;
            this.btnMinimizar.Location = new System.Drawing.Point(466, 0);
            this.btnMinimizar.Size = new System.Drawing.Size(46, 40);
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(24, 38, 86);
            this.btnMinimizar.Name = "btnMinimizar";
            // 
            // pnlMarca
            // 
            this.pnlMarca.Controls.Add(this.lblCopyright);
            this.pnlMarca.Controls.Add(this.lblMarcaDetalle);
            this.pnlMarca.Controls.Add(this.lblDescripcionLogin);
            this.pnlMarca.Controls.Add(this.picLogo);
            this.pnlMarca.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.pnlMarca.BackColor = System.Drawing.Color.FromArgb(5, 10, 40);
            this.pnlMarca.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMarca.FocoX = 0.05F;
            this.pnlMarca.FocoY = 0.85F;
            this.pnlMarca.Imagen = global::GUI_08YS.Properties.Resources.BackGroundHorizon;
            this.pnlMarca.Size = new System.Drawing.Size(420, 598);
            this.pnlMarca.VeloAbajo = System.Drawing.Color.FromArgb(215, 4, 8, 30);
            this.pnlMarca.VeloArriba = System.Drawing.Color.FromArgb(70, 4, 8, 30);
            this.pnlMarca.Name = "pnlMarca";
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.ForeColor = System.Drawing.Color.FromArgb(140, 146, 165);
            this.lblCopyright.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.Location = new System.Drawing.Point(0, 560);
            this.lblCopyright.Size = new System.Drawing.Size(420, 20);
            this.lblCopyright.Tag = "Login_lblCopyright";
            this.lblCopyright.Text = "© Horizon Hotel & Resort";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCopyright.UseMnemonic = false;
            this.lblCopyright.Name = "lblCopyright";
            // 
            // lblMarcaDetalle
            // 
            this.lblMarcaDetalle.BackColor = System.Drawing.Color.Transparent;
            this.lblMarcaDetalle.ForeColor = System.Drawing.Color.FromArgb(200, 204, 216);
            this.lblMarcaDetalle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarcaDetalle.Location = new System.Drawing.Point(40, 360);
            this.lblMarcaDetalle.Size = new System.Drawing.Size(340, 48);
            this.lblMarcaDetalle.Tag = "Login_lblMarcaDetalle";
            this.lblMarcaDetalle.Text = "Sistema de gestión hotelera";
            this.lblMarcaDetalle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMarcaDetalle.Name = "lblMarcaDetalle";
            // 
            // lblDescripcionLogin
            // 
            this.lblDescripcionLogin.BackColor = System.Drawing.Color.Transparent;
            this.lblDescripcionLogin.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblDescripcionLogin.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionLogin.Location = new System.Drawing.Point(20, 320);
            this.lblDescripcionLogin.Size = new System.Drawing.Size(380, 32);
            this.lblDescripcionLogin.Tag = "Login_lblDescripcionLogin";
            this.lblDescripcionLogin.Text = "Excelencia en cada estadía";
            this.lblDescripcionLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDescripcionLogin.Name = "lblDescripcionLogin";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::GUI_08YS.Properties.Resources.HorizonLogoPuro1;
            this.picLogo.Location = new System.Drawing.Point(40, 130);
            this.picLogo.Size = new System.Drawing.Size(340, 170);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabStop = false;
            this.picLogo.Name = "picLogo";
            // 
            // FormLogin_08YS
            // 
            this.AcceptButton = this.btn_Acceder;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(150, 115, 30);
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Horizon Hotel & Resort";
            this.Controls.Add(this.pnlFormulario);
            this.Controls.Add(this.pnlMarca);
            this.Name = "FormLogin_08YS";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlMarca.ResumeLayout(false);
            this.pnlAcento.ResumeLayout(false);
            this.pnlFormulario.ResumeLayout(false);
            this.pnlFormulario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFormulario;
        private System.Windows.Forms.Label lblPie;
        private FontAwesome.Sharp.IconButton btn_Acceder;
        private System.Windows.Forms.Label lblBloqMayus;
        private CustomControls.IconPlaceholderTextBox txtPassword;
        private System.Windows.Forms.Label lblContrasena;
        private CustomControls.IconPlaceholderTextBox txtUsername;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Panel pnlAcento;
        private CustomControls.IconComboBox IdiomaCombobox;
        private FontAwesome.Sharp.IconButton btnCerrar;
        private FontAwesome.Sharp.IconButton btnMinimizar;
        private GUI_08YS.UserControls.PanelImagen_68SA pnlMarca;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblMarcaDetalle;
        private System.Windows.Forms.Label lblDescripcionLogin;
        private System.Windows.Forms.PictureBox picLogo;
    }
}
