using BLL_08YS;
using CustomControls;
using FontAwesome.Sharp;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_08YS
{
    public partial class FormLogin_08YS : Form
    {
        private UserBLL_08YS _userBLL;
        public bool ModoRelogin { get; set; } = false;
        public FormLogin_08YS()
        {
            InitializeComponent();
            _userBLL = BLLFactory_08YS.CreateUserBLL();
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                User_08YS user = _userBLL.Login(username, password, out bool passwordDefault);

                if (passwordDefault)
                {
                    MessageBox.Show("Debe cambiar su contraseña antes de continuar.", "Cambio de Contraseña Requerido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FormCambiarContraseña_08YS formCambiarContraseña = new FormCambiarContraseña_08YS();
                    formCambiarContraseña.ShowDialog();

                    if (formCambiarContraseña.DialogResult == DialogResult.OK)
                    {
                        // Limpiamos el Singleton para que la cuenta no quede tomada
                        SessionManager_08YS.Instance.CerrarSesion();

                        // Limpiamos los campos para obligarlo a escribir la nueva
                        txtUsername.Text = "";
                        txtPassword.Text = "";

                        MessageBox.Show("Por favor, inicie sesión nuevamente con su nueva contraseña.", "Sesión Reiniciada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return; // Cortamos el flujo ACÁ para que NO intente abrir el MDI
                    }
                    else
                    {
                        // Si el usuario canceló o cerró la ventana de cambio obligatorio sin éxito, lo sacamos
                        SessionManager_08YS.Instance.CerrarSesion();
                        return;
                    }
                }

                FormMDI_08YS formMDI = new FormMDI_08YS();
                this.Hide();
                
                formMDI.CerrarSesion += () =>
                {
                    SessionManager_08YS.Instance.CerrarSesion();
                    this.Show();
                    
                };

                formMDI.Show();
                formMDI.Activate();

            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UserBloqueadoException_08YS ex)
            {
                MessageBox.Show(ex.Message, "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            // CATCH 2: Captura específica si el usuario no existe
            catch (UserNoRegistradoException_08YS ex)
            {
                MessageBox.Show(ex.Message, "Usuario Inválido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            // CATCH 3: Captura específica si el usuario no esta activo
            catch (UserInactivoException_08YS ex)
            {
                MessageBox.Show(ex.Message, "Usuario Suspendido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // CATCH 4: Captura para errores de credenciales incorrectas (AuthenticationException)
            catch (AuthenticationException ex)
            {
                MessageBox.Show(ex.Message, "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado en el sistema:\n{ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
    }
}
