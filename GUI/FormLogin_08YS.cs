using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_08YS;
using Service_08YS;
using System.Security.Authentication;

namespace GUI_08YS
{
    public partial class FormLogin_08YS : Form
    {
        private UserBLL_08YS _userBLL;
        private int IntentosFallidos = 0;

        public FormLogin_08YS()
        {
            InitializeComponent();
            _userBLL = BLLFactory_08YS.CreateUserBLL();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || txtUsername.Text == "Username" || txtPassword.Text == "Contraseña")
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            string email = txtUsername.Text?.Trim();
            string password = txtPassword.Text?.Trim();
            
            try
            {
                User user = _userBLL.Login(email, password);

                Service_08YS.SessionManager.Instance.SetCurrentUser(user);

                FormMDI_08YS formMDI = new FormMDI_08YS();
                this.Hide();
                
                formMDI.CerrarSesion += () =>
                {
                    SessionManager.Instance.CerrarSesion();
                    this.Show();
                };

                formMDI.Show();
                formMDI.Activate();

                txtUsername.Text = UsernameFieldText;
                txtPassword.Text = PasswordFieldText;
            }
            catch (UserNoRegistradoException_08YS)
            {
                MessageBox.Show("Usuario no encontrado. Por favor, verifique sus credenciales.");
                return;
            }
            catch(InvalidCredentialException)
            {
                IntentosFallidos++;
                MessageBox.Show("La contraseña ingresada es incorrecta.");

                if(IntentosFallidos == 3)
                    {
                        MessageBox.Show("Ha alcanzado el máximo de intentos fallidos. La cuenta ha sido bloqueada.");
                        
                    }
                return;
            }
            catch(UserBloqueadoException_08YS)
            {
                MessageBox.Show("Su cuenta se encuentra bloqueada debido a múltiples intentos fallidos. Por favor, contacte al soporte.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #region FrontEnd

        private const string UsernameFieldText = "Username";
        private const string PasswordFieldText = "Contraseña";

        private new void Enter(TextBox textbox, string txt)
        {
            if (textbox.Text == txt)
            {
                textbox.Text = "";
                textbox.ForeColor = Color.LightGray;
            }
        }

        private new void Leave(TextBox textbox, string txt)
        {
            if (textbox.Text == "")
            {
                textbox.Text = txt;
                textbox.ForeColor = Color.Gray;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Enter(txtUsername, UsernameFieldText);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Leave(txtUsername, UsernameFieldText);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            Enter(txtPassword, PasswordFieldText);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Leave(txtPassword, PasswordFieldText);
        }

        private void FormLogin_Load_1(object sender, EventArgs e)
        {

            this.BeginInvoke(new Action(() =>
            {
                dummyFocusTarget.Select();
            }));


        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCambiarIdioma_Click(object sender, EventArgs e)
        {
            //    string nuevoIdioma = Thread.CurrentThread.CurrentUICulture.Name.StartsWith("es") ? "en" : "es";
            //    Service.Traductor.CambiarIdioma(nuevoIdioma);
            //InicializarIdioma();
            //      string nuevoIdioma =
            //IdiomaService.IdiomaActual.StartsWith("es")
            // ? "en"
            // : "es";

            //     IdiomaService.CambiarIdioma(nuevoIdioma);
            //string actual = IdiomaService.Instancia.IdiomaActual;
            //string nuevo = actual.StartsWith("es") ? "en" : "es";

            //IdiomaService.Instancia.CambiarIdioma(nuevo);


        }

        //private void ActualizarTextos()
        //{
        //    btnAccederLogin.Text = Resources.btnAccederLogin;
        //    linkLabel1.Text = Resources.linkLabel1;

        //    txtcontra.Text = Resources.Contraseña;
        //    txtMail.Text = Resources.Correo_Electronico;

        //    btnCambiarIdioma.Text =
        //        IdiomaService.Instancia.IdiomaActual.StartsWith("es")
        //        ? "Cambiar a idioma Inglés"
        //        : "Switch to Spanish";
        //}

        //private void InicializarIdioma()
        //{
        //    btnAccederLogin.Text = Resources.btnAccederLogin;
        //    linkLabel1.Text = Resources.linkLabel1;
        //    txtcontra.Text = Resources.Contraseña;
        //    txtMail.Text = Resources.Correo_Electronico;
        //    btnCambiarIdioma.Text = Thread.CurrentThread.CurrentUICulture.Name.StartsWith("es")
        //    ? "Cambiar a idioma Inglés"
        //    : "Switch to Spanish";
        //}

        //public void ActualizarIdioma()
        //{
        //    ActualizarTextos();
        //}
        #endregion
    }
}
