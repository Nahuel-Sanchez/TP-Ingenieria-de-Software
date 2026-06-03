using GUI;
using GUI_08YS.Admin;
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
    public partial class FormMDI_08YS : Form,IIdiomaObserver_08YS
    {
        public event Action CerrarSesion;
        public FormMDI_08YS()
        {
            InitializeComponent();
            lblRolSistema.Text = SessionManager_08YS.Instance.Current.Rol.ToString();
            lblNombreApellido.Text= SessionManager_08YS.Instance.Current.Nombre + " " + SessionManager_08YS.Instance.Current.Apellido;
            GestionarRol();
            TraductorManager_08YS.Instance.Suscribir(this);
            UpdateIdioma(); 
        }
        private void GestionarRol()
        {
            if (SessionManager_08YS.Instance.Current.Rol.ToString() == "Basico")
            {
                iconButton2.Visible = false;
                iconButton2.Enabled = false;
            }
        }
        public void UpdateIdioma()
        {
            TraducirControles(this);
        }
        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                // Si el control tiene un Tag asignado, buscamos su traducción
                if (c.Tag != null && !string.IsNullOrWhiteSpace(c.Tag.ToString()))
                {
                    c.Text = TraductorManager_08YS.Instance.GetTexto(c.Tag.ToString());
                }

                // Si el control tiene hijos (como un Panel o GroupBox), hacemos recursividad
                if (c.HasChildren)
                {
                    TraducirControles(c);
                }
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
            if (SessionManager_08YS.Instance.IsLogged)
            {
                CerrarSesion?.Invoke(); // Desoculta el Username y limpia Singleton
            }
        }


        private void FormMDI_Load(object sender, EventArgs e)
        {
            AdministrativoDropDownMenu.IsMainMenu = true;
            PerfilDropDownMenu.IsMainMenu = true;
        }

        public void OpenChildForm(Form childForm)
        {
            panel2.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.AutoScaleMode = AutoScaleMode.None;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.Show();

            childForm.BeginInvoke(new Action(() =>
            {
                childForm.BeginInvoke(new Action(() =>
                {
                    ForceCustomControlsLayout(childForm);
                }));
            }));
        }

        private void ForceCustomControlsLayout(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.HasChildren)
                    ForceCustomControlsLayout(ctrl);

                var onResize = ctrl.GetType().GetMethod("OnResize",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);
                onResize?.Invoke(ctrl, new object[] { EventArgs.Empty });

                if (ctrl is FontAwesome.Sharp.IconButton iconBtn)
                {
                    var updateImage = iconBtn.GetType()
                        .GetMethod("UpdateImage",
                            System.Reflection.BindingFlags.NonPublic |
                            System.Reflection.BindingFlags.Instance |
                            System.Reflection.BindingFlags.FlattenHierarchy);
                    updateImage?.Invoke(iconBtn, null);
                    iconBtn.Invalidate();
                    iconBtn.Update();
                }

                if (ctrl is Label lbl && lbl.AutoSize)
                {
                    lbl.MaximumSize = Size.Empty;  // elimina cualquier límite
                    lbl.Size = Size.Empty;         // fuerza recálculo desde cero
                    lbl.Refresh();
                }
                if (ctrl is CustomControls.IconPlaceholderTextBox txt)
                {
                    // Forzar estado visual correcto
                    bool hasText = !string.IsNullOrEmpty(txt.RealText);
                    txt.InnerTextBox.Visible = hasText || txt.InnerTextBox.Focused;
                    txt.Invalidate();
                }
            }
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
                    // Ejecutamos el cierre de sesión e invocamos el evento para desocultar el Username
                    CerrarSesion?.Invoke();
                    this.Close(); // Cerramos el MDI
                }
            };
            OpenChildForm(form);
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
            PerfilDropDownMenu.Show(iconButton1, iconButton1.Width, 0);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AdministrativoDropDownMenu.Show(iconButton2, iconButton2.Width, 0);
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


        private void familiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormGestionAcceso(TipoEntidad.Familia, OpenChildForm));
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormGestionAcceso(TipoEntidad.Rol, OpenChildForm));
        }


    }
}
