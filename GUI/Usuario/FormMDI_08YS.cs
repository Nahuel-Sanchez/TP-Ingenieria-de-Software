using FontAwesome.Sharp;
using GUI;
using GUI_08YS.Admin;
using GUI_08YS.Properties;
using Service_08YS;
using Service_08YS.Entities.Acceso;
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
        // Mapeo de controles del menu administrativo a los permisos necesarios para verlos y ejecutarlos
        private static readonly Dictionary<string, Permisos> _mapaMenuAdmin =
            new Dictionary<string, Permisos>
            {
                { nameof(gestionUsuarioToolStripMenuItem),  Permisos.VerUsuarios    },
                { nameof(bitacoraToolStripMenuItem),        Permisos.VerBitacora    },
                { nameof(familiasToolStripMenuItem),        Permisos.VerFamilias    },
                { nameof(rolesToolStripMenuItem),           Permisos.VerRoles       },
            };

        public event Action CerrarSesion;

        public FormMDI_08YS()
        {
            InitializeComponent();
            lblRolSistema.Text = SessionManager_08YS.Instance.Current.Rol.Nombre;
            lblNombreApellido.Text= SessionManager_08YS.Instance.Current.Nombre + " " + SessionManager_08YS.Instance.Current.Apellido;
            AplicarPermisos();
            TraductorManager_08YS.Instance.Suscribir(this);
            UpdateIdioma(); 
        }

        private void AplicarPermisos()
        {
            // Botón "Administrativo" del panel lateral — solo visible si tiene algún permiso admin
            btnAdministrativo.Visible = SessionManager_08YS.Instance.HasPermission(Permisos.VerUsuarios)
                                     || SessionManager_08YS.Instance.HasPermission(Permisos.VerBitacora)
                                     || SessionManager_08YS.Instance.HasPermission(Permisos.VerFamilias)
                                     || SessionManager_08YS.Instance.HasPermission(Permisos.VerRoles   );

            gestionAccesosToolStripMenuItem.Visible = SessionManager_08YS.Instance.HasPermission(Permisos.VerRoles   )
                                                   || SessionManager_08YS.Instance.HasPermission(Permisos.VerFamilias);

            // Items del menu desplegable de admin
            PermissionFilter_08YS.AplicarMenuStrip(AdministrativoDropDownMenu, _mapaMenuAdmin);
        }

        #region idioma
        public void UpdateIdioma()
            => TraducirControles(this);

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
        #endregion

        private void FormMDI_Load(object sender, EventArgs e)
        {
            AdministrativoDropDownMenu.IsMainMenu = true;
            PerfilDropDownMenu.IsMainMenu = true;
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

        #region Menu

        #region Administrativo

        private void btnAdministrativo_Click(object sender, EventArgs e)
        {
            AdministrativoDropDownMenu.Show(btnAdministrativo, btnAdministrativo.Width, 0);
        }

        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerUsuarios);
            OpenChildForm(new FormGestionUsuarios_08YS());
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerBitacora);
            OpenChildForm(new FormBitacora_08YS());
        }

        private void familiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerFamilias);
            OpenChildForm(new FormGestionAcceso_08YS(TipoEntidad.Familia, OpenChildForm));
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerRoles);
            OpenChildForm(new FormGestionAcceso_08YS(TipoEntidad.Rol, OpenChildForm));
        }

        #endregion

        #region Perfil

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            PerfilDropDownMenu.Show(btnPerfil, btnPerfil.Width, 0);
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

        private void ReLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormLogin_08YS frmRelogin = new FormLogin_08YS();


            frmRelogin.ModoRelogin = true;


            frmRelogin.ShowDialog();
        }

        private void cerrarSesionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("¿Está seguro de que desea cerrar la sesión actual?",
                                        "Cerrar Sesión",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                CerrarSesion?.Invoke();
                this.Close();
            }
        }

        #endregion

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("¿Está seguro de que desea cerrar la sesión actual?",
                                        "Cerrar Sesión",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                CerrarSesion?.Invoke();
                this.Close();
            }
        }

        #endregion

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
    }
}
