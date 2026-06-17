using BLL_08YS;
using CustomControls;
using FontAwesome.Sharp;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GUI_08YS
{
    public partial class FormLogin_08YS : Form,IIdiomaObserver_08YS
    {
        private UserBLL_08YS _userBLL;
        public bool ModoRelogin { get; set; } = false;
        public FormLogin_08YS()
        {
            InitializeComponent();
            _userBLL = BLLFactory_08YS.CreateUserBLL();
            ConfigurarCombo();

            TraductorManager_08YS.Instance.Suscribir(this);

         
            UpdateIdioma();
           
        }
        private void ConfigurarCombo()
        {
            IdiomaCombobox.Items.Add("Español");
            IdiomaCombobox.Items.Add("Ingles");

            IdiomaCombobox.SelectedIndex = 0;
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

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_completar_campos"),
                                TraductorManager_08YS.Instance.GetTexto("error_validacion"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                User_08YS user = _userBLL.Login(username, password, out bool passwordDefault);

                if (passwordDefault)
                {
                    MessageBox.Show(
                        TraductorManager_08YS.Instance.GetTexto("msg_pwd_cambio_req"),
                        TraductorManager_08YS.Instance.GetTexto("pwd_cambio_req"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    FormCambiarContraseña_08YS formCambiarContraseña = new FormCambiarContraseña_08YS();
                    formCambiarContraseña.ShowDialog();

                    if (formCambiarContraseña.DialogResult == DialogResult.OK)
                    {
                        SessionManager_08YS.Instance.CerrarSesion();
                        txtUsername.Text = "";
                        txtPassword.Text = "";

                        MessageBox.Show(
                            TraductorManager_08YS.Instance.GetTexto("msg_sesion_reiniciada"),
                            TraductorManager_08YS.Instance.GetTexto("sesion_reiniciada"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {
                        SessionManager_08YS.Instance.CerrarSesion();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(user.Idioma))
                    TraductorManager_08YS.Instance.CambiarIdioma(user.Idioma);

                FormMDI_08YS formMDI = new FormMDI_08YS();
                this.Hide();

                formMDI.CerrarSesion += () =>
                {
                    try
                    {
                        _userBLL.Logout();
                    }
                    catch (LogoutPersistenceException_08YS)
                    {
                        MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_logout_fallido"),
                                        TraductorManager_08YS.Instance.GetTexto("error_critico"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.Show();
                };

                formMDI.Show();
                formMDI.Activate();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_ya_hay_login"),
                                TraductorManager_08YS.Instance.GetTexto("error_sesion"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (AuthenticationException)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_error_credenciales"),
                                TraductorManager_08YS.Instance.GetTexto("error_autenticacion"),
                                MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (UserBloqueadoException_08YS)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_user_bloqueado"),
                                TraductorManager_08YS.Instance.GetTexto("acceso_denegado"),
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (UserNoRegistradoException_08YS)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_user_no_registrado"),
                                TraductorManager_08YS.Instance.GetTexto("usuario_invalido"),
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (UserDesactivadoException_08YS)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_user_desactivado"),
                                TraductorManager_08YS.Instance.GetTexto("usuario_suspendido"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                string msgBase = TraductorManager_08YS.Instance.GetTexto("msg_error_inesperado");
                MessageBox.Show($"{msgBase}{ex.Message}",
                                TraductorManager_08YS.Instance.GetTexto("error_critico"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button2_Click(object sender, EventArgs e)
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

        private void txtPassword_IconRightClick(object sender, EventArgs e)
        {
            var tb = (IconPlaceholderTextBox)sender;
            tb.MaskedInput = !tb.MaskedInput;
            tb.IconCharRight = tb.MaskedInput ? IconChar.EyeSlash : IconChar.Eye;
        }

        private void IdiomaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idiomaSeleccionado = IdiomaCombobox.SelectedIndex == 1 ? "en" : "es";

            // 1. Le avisamos al Manager para que muten todas las pantallas abiertas por el Observer
            TraductorManager_08YS.Instance.CambiarIdioma(idiomaSeleccionado);

            // 2. Si hay un usuario logueado en el SessionManager, impactamos su perfil en la BD
            if (SessionManager_08YS.Instance.IsLogged)
            {
                User_08YS usuarioActual = SessionManager_08YS.Instance.Current;
                _userBLL.CambiarIdiomaUsuario(idiomaSeleccionado);
            }
        }
    }
}
