using BLL_08YS;
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
    public partial class FormCambiarContraseña : Form
    {
        private UserBLL_08YS _userBLL;

        public FormCambiarContraseña()
        {
            InitializeComponent();
            _userBLL = BLLFactory_08YS.CreateUserBLL();
        }

        private void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())return;
            try
            {
                _userBLL.CambiarContraseña(txtContraseñaActual.Text, txtNuevaContraseña.Text);
                MessageBox.Show("Contraseña cambiada exitosamente.");
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
                return true; // Hubo un error, retornar true para que el botón corte
            }
            if (txtNuevaContraseña.Text != txtConfirmarContraseña.Text)
            {
                MessageBox.Show("La nueva contraseña y la confirmación no coinciden.");
                return true; // Hubo un error, retornar true para que el botón corte
            }
            return false; // Todo impecable, no hay errores
        }
    }
}
