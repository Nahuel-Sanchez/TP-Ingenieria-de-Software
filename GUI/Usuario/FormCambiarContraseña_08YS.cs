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
    public partial class FormCambiarContraseña_08YS : Form
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
                MessageBox.Show("Contraseña cambiada exitosamente.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtContraseñaActual.Text) || string.IsNullOrWhiteSpace(txtNuevaContraseña.Text) || string.IsNullOrWhiteSpace(txtConfirmarContraseña.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return false;
            }
            if (txtNuevaContraseña.Text != txtConfirmarContraseña.Text)
            {
                MessageBox.Show("La nueva contraseña y la confirmación no coinciden.");
                return false;
            }
            if(txtNuevaContraseña.Text == txtContraseñaActual.Text)
            {
                MessageBox.Show("La nueva contraseña no puede ser igual a la actual.");
                return false; 
            }
            return true;
        }

        
    }
}
