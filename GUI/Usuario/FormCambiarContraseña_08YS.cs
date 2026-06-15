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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_08YS
{
    public partial class FormCambiarContraseña_08YS : Form,IIdiomaObserver_08YS
    {
        private UserBLL_08YS _userBLL;
        private bool _syncingPasswords = false;

        public FormCambiarContraseña_08YS()
        {
            InitializeComponent();
            _userBLL = BLLFactory_08YS.CreateUserBLL();

            txtContraseñaActual.IconClick += (s, e) =>
            {
                var tb = (IconPlaceholderTextBox)s;
                ApplyPasswordToggle(tb, !tb.MaskedInput);
            };

            txtNuevaContraseña.IconClick += OnLinkedPasswordToggle;
            txtConfirmarContraseña.IconClick += OnLinkedPasswordToggle;
            TraductorManager_08YS.Instance.Suscribir(this);
            UpdateIdioma(); 
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
        private void OnLinkedPasswordToggle(object sender, EventArgs e)
        {
            if (_syncingPasswords) return;

            _syncingPasswords = true;
            try
            {
                bool nowMasked = !((IconPlaceholderTextBox)sender).MaskedInput;
                ApplyPasswordToggle(txtNuevaContraseña, nowMasked);
                ApplyPasswordToggle(txtConfirmarContraseña, nowMasked);
            }
            finally
            {
                _syncingPasswords = false;
            }
        }

        private static void ApplyPasswordToggle(IconPlaceholderTextBox tb, bool masked)
        {
            tb.MaskedInput = masked;
            tb.IconChar = masked ? IconChar.EyeSlash : IconChar.Eye;
        }
        private void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try
            {
                _userBLL.CambiarContraseña(txtContraseñaActual.Text, txtNuevaContraseña.Text);
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_pwd_changed"));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (PwdActualIncorrectaException_08YS)
            {
                // Buscamos la clave de traducción exacta para este error de negocio
                string mensajeTraducido = TraductorManager_08YS.Instance.GetTexto("msg_pwd_actual_incorrecta");
                string tituloError = TraductorManager_08YS.Instance.GetTexto("error_validacion");

                MessageBox.Show(mensajeTraducido, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Error inesperado del sistema
                string msgInesperado = TraductorManager_08YS.Instance.GetTexto("msg_error_inesperado");
                string tituloError = TraductorManager_08YS.Instance.GetTexto("error");

                MessageBox.Show($"{msgInesperado}: {ex.Message}", tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtContraseñaActual.Text) || string.IsNullOrWhiteSpace(txtNuevaContraseña.Text) || string.IsNullOrWhiteSpace(txtConfirmarContraseña.Text))
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_completar_campos"));
                return false;
            }
            if (txtNuevaContraseña.Text != txtConfirmarContraseña.Text)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_pwd_no_coinciden"));
                return false;
            }
            if (txtNuevaContraseña.Text == txtContraseñaActual.Text)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("msg_pwd_igual_actual"));
                return false;
            }
            return true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

    }
}
