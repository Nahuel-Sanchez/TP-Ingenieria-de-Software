using BLL_08YS;
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

            string username = txtUsername.Text?.Trim();
            string password = txtPassword.Text?.Trim();
            
            try
            {
                User user = _userBLL.Login(username, password);

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
    }
}
