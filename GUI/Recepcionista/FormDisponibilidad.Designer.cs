namespace GUI_08YS.Recepcionista
{
    partial class FormDisponibilidad
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
            this.calRangoReservas = new CustomControls.CustomDateRangePanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnContinuar = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // calRangoReservas
            // 
            this.calRangoReservas.ArrowForeColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.ArrowHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.calRangoReservas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.calRangoReservas.BlockedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.calRangoReservas.BlockedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.calRangoReservas.ClearButtonColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.ClearButtonText = "Limpiar selección";
            this.calRangoReservas.DayForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.calRangoReservas.DayNameForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(160)))), ((int)(((byte)(150)))));
            this.calRangoReservas.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.calRangoReservas.DisablePastDates = true;
            this.calRangoReservas.FooterForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.calRangoReservas.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(40)))), ((int)(((byte)(75)))));
            this.calRangoReservas.HoverForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.calRangoReservas.InRangeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(218)))), ((int)(((byte)(165)))), ((int)(((byte)(32)))));
            this.calRangoReservas.Location = new System.Drawing.Point(12, 197);
            this.calRangoReservas.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.calRangoReservas.MaxNights = null;
            this.calRangoReservas.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.calRangoReservas.MinimumSize = new System.Drawing.Size(620, 249);
            this.calRangoReservas.Name = "calRangoReservas";
            this.calRangoReservas.PreviewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(218)))), ((int)(((byte)(165)))), ((int)(((byte)(32)))));
            this.calRangoReservas.SelectedBackColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.calRangoReservas.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(75)))), ((int)(((byte)(40)))));
            this.calRangoReservas.ShowTodayHighlight = true;
            this.calRangoReservas.Size = new System.Drawing.Size(1428, 575);
            this.calRangoReservas.TabIndex = 0;
            this.calRangoReservas.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
            this.calRangoReservas.TitleForeColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.TodayHighlightColor = System.Drawing.Color.Goldenrod;
            this.calRangoReservas.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(100)))));
            this.calRangoReservas.RangeChanged += new System.EventHandler(this.calRangoReservas_RangeChanged);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblTitulo.Location = new System.Drawing.Point(44, 19);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(570, 79);
            this.lblTitulo.TabIndex = 55;
            this.lblTitulo.Tag = "lblAuditoriaEventos";
            this.lblTitulo.Text = "Registrar reserva";
            // 
            // btnContinuar
            // 
            this.btnContinuar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(15)))), ((int)(((byte)(45)))));
            this.btnContinuar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnContinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuar.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinuar.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnContinuar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnContinuar.IconColor = System.Drawing.Color.Gold;
            this.btnContinuar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnContinuar.IconSize = 40;
            this.btnContinuar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnContinuar.Location = new System.Drawing.Point(20, 10);
            this.btnContinuar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.btnContinuar.Size = new System.Drawing.Size(1430, 79);
            this.btnContinuar.TabIndex = 59;
            this.btnContinuar.Tag = "btnContinuar";
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnContinuar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnContinuar.UseVisualStyleBackColor = false;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnContinuar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 821);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panel1.Size = new System.Drawing.Size(1470, 99);
            this.panel1.TabIndex = 60;
            // 
            // FormDisponibilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GUI_08YS.Properties.Resources.BackGroundHorizon;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1470, 920);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.calRangoReservas);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDisponibilidad";
            this.Text = "FormDisponibilidad";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.CustomDateRangePanel calRangoReservas;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnContinuar;
        private System.Windows.Forms.Panel panel1;
    }
}