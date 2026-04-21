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
using BLL;
using BE;
using BE.Usuarios;

namespace GUI
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
          
            ActualizarTextos();

        }
        private bool EsCorreoValido(string email)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patron);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMail.Text) || string.IsNullOrWhiteSpace(txtcontra.Text) || txtMail.Text == "Correo Electronico" || txtcontra.Text == "Contraseña")
            {
                MessageBox.Show(Resources.CompletarDatos);
                return;
            }

            if (!EsCorreoValido(txtMail.Text))
            {
                MessageBox.Show("Formato de correo electrónico inválido.");
                return;
            }
            string email = txtMail.Text?.Trim();
            string password = txtcontra.Text?.Trim();
            
            try
            {
                User user = UserBLL.Login(email, password);

                Service.SessionManager.SetCurrentUser(user);

                FormMDI formMDI = new FormMDI();
                this.Hide();

                formRegistro?.Close();
                formRegistro = null;
                
                formMDI.CerrarSesion += () =>
                {
                    this.Show();
                    Service.SessionManager.CerrarSesion();
                };

                FormMisProyectos formMisProyectos = new FormMisProyectos(formMDI, false);
                formMisProyectos.Show();
                formMisProyectos.Activate();

                txtcontra.Text = Resources.Contraseña;
                txtMail.Text = Resources.Correo_Electronico;
            }
            catch (UserNoRegistradoException)
            {
                bllBitacora.RegistrarEvento
                (
                    DateTime.Now,
                    "LoginFallido",
                    email,                   // ponemos el intento
                    BE.TipoEvento.Advertencia,
                    "GUI"
                );
                if (
                        MessageBox.Show($"{Resources.UsuarioNoEncontrado} \n\n {Resources.OfrecerRegistro}", $"{Resources.Advertencia}", MessageBoxButtons.YesNo)
                        == DialogResult.Yes
                    )
                {
                    AbrirRegistro();
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirRegistro();
        }
        private bool AbrirRegistro()
        {
            if (formseleccionado == null)
            {
                FormSeleccionRol formSeleccion = new FormSeleccionRol();
                formseleccionado = formSeleccion;
                formSeleccion.Cierre += () =>
                {
                    this.Show();
                    formseleccionado = null;
                };
                formSeleccion.Show();
                formSeleccion.Activate();
                this.Hide();
                return true;
            }

            formseleccionado.BringToFront();
            formseleccionado.Activate();
            return false;
        }

        #region FrontEnd

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
            Enter(txtMail, Resources.Correo_Electronico);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Leave(txtMail, Resources.Correo_Electronico);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            Enter(txtcontra, Resources.Contraseña);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Leave(txtcontra, Resources.Contraseña);
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
            string actual = IdiomaService.Instancia.IdiomaActual;
            string nuevo = actual.StartsWith("es") ? "en" : "es";

            IdiomaService.Instancia.CambiarIdioma(nuevo);


        }
        private void ActualizarTextos()
        {
            btnAccederLogin.Text = Resources.btnAccederLogin;
            linkLabel1.Text = Resources.linkLabel1;

            txtcontra.Text = Resources.Contraseña;
            txtMail.Text = Resources.Correo_Electronico;

            btnCambiarIdioma.Text =
                IdiomaService.Instancia.IdiomaActual.StartsWith("es")
                ? "Cambiar a idioma Inglés"
                : "Switch to Spanish";
        }
        private void InicializarIdioma()
        {

            btnAccederLogin.Text = Resources.btnAccederLogin;
            linkLabel1.Text = Resources.linkLabel1;
            txtcontra.Text = Resources.Contraseña;
            txtMail.Text = Resources.Correo_Electronico;
            btnCambiarIdioma.Text = Thread.CurrentThread.CurrentUICulture.Name.StartsWith("es")
            ? "Cambiar a idioma Inglés"
            : "Switch to Spanish";
        }

        public void ActualizarIdioma()
        {
            ActualizarTextos();
        }
        #endregion
    }
}
