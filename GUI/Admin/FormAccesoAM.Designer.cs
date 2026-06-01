namespace GUI_08YS.Admin
{
    partial class FormAccesoAM
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
            this.txtNombre = new CustomControls.IconPlaceholderTextBox();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.SystemColors.Window;
            this.txtNombre.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtNombre.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(149)))), ((int)(((byte)(237)))));
            this.txtNombre.IconColor = System.Drawing.Color.DimGray;
            this.txtNombre.IconColorRight = System.Drawing.Color.DimGray;
            this.txtNombre.Location = new System.Drawing.Point(409, 99);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PlaceholderColor = System.Drawing.Color.Silver;
            this.txtNombre.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombre.Size = new System.Drawing.Size(250, 32);
            this.txtNombre.TabIndex = 0;
            this.txtNombre.Text = "iconPlaceholderTextBox1";
            // 
            // FormAccesoAM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtNombre);
            this.Name = "FormAccesoAM";
            this.Text = "FormAccesoAM";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.IconPlaceholderTextBox txtNombre;
    }
}