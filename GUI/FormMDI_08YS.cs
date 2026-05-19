using GUI;
using GUI_08YS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GUI_08YS
{
    public partial class FormMDI_08YS : Form
    {
        public event Action CerrarSesion;
        public FormMDI_08YS()
        {
            InitializeComponent();
        }

        private void FormMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarSesion?.Invoke();
        }

        private void FormMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show($"{Resources.ContinuarCierreSesion} \n\n{Resources.ConfirmarCierreSesion}", Resources.Advertencia, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            //{
            //    e.Cancel = true;
            //}
        }


        private void FormMDI_Load(object sender, EventArgs e)
        {
            dropdownMenuStrip_08YS1.IsMainMenu = true;
        }


        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormGestionUsuarios form = new FormGestionUsuarios();
            form.Show();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBitacora form = new FormBitacora();
            form.Show();
        }


        #region BarraSuperior

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            dropdownMenuStrip_08YS1.Show(button5, button5.Width, 0);
        }
    }
}
