using BLL_08YS;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GUI
{
    public partial class FormGestionUsuarios : Form
    {
        private UserBLL_08YS _bll = BLLFactory_08YS.CreateUserBLL();
        
        public FormGestionUsuarios()
        {
            InitializeComponent();
        }
        private enum EstadoUI { Consulta, Insertando, Editando }
        private EstadoUI _estadoActual;
        private void FormGestionUsuarios_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            cmbRol.DataSource = Enum.GetValues(typeof(UserRole));
            CambiarEstado(EstadoUI.Consulta); // Inicia en modo lectura
        }
        #region Manejo de Interfaz y Estados

        private void CambiarEstado(EstadoUI nuevoEstado)
        {
            _estadoActual = nuevoEstado;

            switch (nuevoEstado)
            {
                case EstadoUI.Consulta:
                    SetCamposReadOnly(true);
                    btnCrear.Enabled = true;
                    btnModificar.Enabled = dgvUsuarios.CurrentRow != null;
                    btnAplicar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnDesbloquear.Enabled = ValidarSiEstaBloqueado();
                    txtMensaje.Text = "Modo Consulta";
                    break;

                case EstadoUI.Insertando:
                    LimpiarCampos();
                    SetCamposReadOnly(false);
                    btnCrear.Enabled = false;
                    btnModificar.Enabled = false;
                    btnDesbloquear.Enabled = false;
                    btnAplicar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnActDes.Enabled = false;
                    txtMensaje.Text = "Modo Inserción: Complete los datos y presione Aplicar.";
                    txtDNI.Focus();
                    break;
            }
        }

        private void SetCamposReadOnly(bool status)
        {
            txtDNI.ReadOnly = status;
            txtApellidos.ReadOnly = status;
            txtNombres.ReadOnly = status;
            txtEmail.ReadOnly = status;
            txtLogin.ReadOnly = status;
            cmbRol.Enabled = !status;
            txtActivo.Enabled = !status;
            txtBloqueado.Enabled = !status;
        }

        private void LimpiarCampos()
        {
            txtDNI.Clear();
            txtApellidos.Clear();
            txtNombres.Clear();
            txtEmail.Clear();
            txtLogin.Clear();
            cmbRol.SelectedIndex = -1;
            txtActivo.Clear();
            txtBloqueado.Clear();
        }

        private bool ValidarSiEstaBloqueado()
        {
            if (dgvUsuarios.CurrentRow == null) return false;
            var user = (User)dgvUsuarios.CurrentRow.DataBoundItem;
            return user.Bloqueado;
        }

        #endregion
        #region Validaciones
        private bool ValidarDNI(string dni)
        {
            string patron = @"^\d+$";
            return Regex.IsMatch(dni, patron);
        }
        private bool ValidarCorreo(string email)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patron);
        }
        private bool ValidarCampos()
        {
            return !string.IsNullOrEmpty(txtDNI.Text) &&
                   !string.IsNullOrEmpty(txtApellidos.Text) &&
                   !string.IsNullOrEmpty(txtNombres.Text) &&
                   !string.IsNullOrEmpty(txtEmail.Text) &&
                   cmbRol.SelectedIndex != -1;
        }
        #endregion
        private void CargarGrilla()
        {
            dgvUsuarios.DataSource = null;
            //dgvUsuarios.DataSource = _bll.GetAll();
            dgvUsuarios.DataSource = UserBLL_08YS._usuariosLocal;
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            CambiarEstado(EstadoUI.Insertando);
            var validaciones = new (bool condicion, string mensaje)[]
   {            
                //Cambiar completar campos para que no se active al tocar el boton
                (!ValidarCampos(), "Complete todos los campos."),
                (!ValidarCorreo(txtEmail.Text), "Email inválido."),
                (!ValidarDNI(txtDNI.Text), "DNI inválido.")
   };

            // Buscamos la primera que falle
            var fallo = validaciones.FirstOrDefault(v => v.condicion);

            if (fallo.mensaje != null)
            {
                MessageBox.Show(fallo.mensaje, "Error");
                return;
            }

            int DNI = int.Parse(txtDNI.Text);
            string apellido = txtApellidos.Text;
            string nombre = txtNombres.Text;
            string email = txtEmail.Text;
            string rol = cmbRol.SelectedItem.ToString();


            MessageBox.Show($"Usuario {nombre} creado");
        }

      
        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
         
                // Obtenemos el objeto vinculado a la fila
                var user = (User)dgvUsuarios.CurrentRow.DataBoundItem;

                txtDNI.Text = user.DNI.ToString();
                txtApellidos.Text = user.Apellido;
                txtNombres.Text = user.Nombre;
                txtEmail.Text = user.Email;
                cmbRol.SelectedItem = user.Rol;
                txtLogin.Text = user.Username;
                txtBloqueado.Text = user.Bloqueado.ToString();
                //txtActivo.Text = user.Activo;

                btnDesbloquear.Enabled = user.Bloqueado;
                btnModificar.Enabled = true;
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {

            if (dgvUsuarios.CurrentRow == null) return;
            var user = (User)dgvUsuarios.CurrentRow.DataBoundItem;

            DialogResult resp = MessageBox.Show($"¿Desea desbloquear a {user.Nombre}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                try
                {
                    _bll.DesbloquearUsuario(user.DNI);


                    MessageBox.Show($"El usuario {user.Nombre} fue desbloqueado con exito");
                    CargarGrilla();
                    CambiarEstado(EstadoUI.Consulta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarEstado(EstadoUI.Consulta);
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
           
        }
    }
}
