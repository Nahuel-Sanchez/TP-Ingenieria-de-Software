using GUI;
using GUI_08YS.Properties;
using Service_08YS;
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
            lblRolSistema.Text = SessionManager.Instance.Current.Rol.ToString();
            lblNombreApellido.Text= SessionManager.Instance.Current.Nombre + " " + SessionManager.Instance.Current.Apellido;
            GestionarRol();
        }
        private void GestionarRol()
        {
            if (SessionManager.Instance.Current.Rol.ToString() == "Basico")
            {
                iconButton2.Visible = false;
                iconButton2.Enabled = false;
            }
        }
        private void FormMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            //CerrarSesion?.Invoke();
        }

        private void FormMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show($"{Resources.ContinuarCierreSesion} \n\n{Resources.ConfirmarCierreSesion}", Resources.Advertencia, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            //{
            //    e.Cancel = true;
            //}
            if (SessionManager.Instance.IsLogged)
            {
                CerrarSesion?.Invoke(); // Desoculta el Login y limpia Singleton
            }
        }


        private void FormMDI_Load(object sender, EventArgs e)
        {
            dropdownMenuStrip_08YS1.IsMainMenu = true;
            dropdownMenuStrip_08YS2.IsMainMenu = true;
        }

        private void OpenChildForm(Form childForm)
        {
            panel2.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.Show();
        }

        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormGestionUsuarios_08YS());
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormBitacora_08YS());
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCambiarContraseña_08YS form = new FormCambiarContraseña_08YS();

            form.FormClosed += (s, args) =>
            {
                if (form.DialogResult == DialogResult.OK)
                {
                    // Ejecutamos el cierre de sesión e invocamos el evento para desocultar el Login
                    CerrarSesion?.Invoke();
                    this.Close(); // Cerramos el MDI
                }
            };
            OpenChildForm(form);
            //if (form.DialogResult == DialogResult.OK)
            //    CerrarSesion.Invoke();
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

       

        private void cerrarSesionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("¿Está seguro de que desea cerrar la sesión actual?",
                                        "Cerrar Sesión - GastroGest",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                CerrarSesion?.Invoke();
                this.Close();
            }
        }

        private void ReLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            FormLogin_08YS frmRelogin = new FormLogin_08YS();

            
            frmRelogin.ModoRelogin = true;

          
            frmRelogin.ShowDialog();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            dropdownMenuStrip_08YS2.Show(iconButton1, iconButton1.Width, 0);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            dropdownMenuStrip_08YS1.Show(iconButton2, iconButton2.Width, 0);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("¿Está seguro de que desea cerrar la sesión actual?",
                                        "Cerrar Sesión - GastroGest",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                CerrarSesion?.Invoke();
                this.Close();
            }
        }
    }
}
