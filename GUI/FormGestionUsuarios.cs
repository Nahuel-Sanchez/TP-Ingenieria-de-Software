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
                    txtLogin.Enabled = false;
                    txtBloqueado.Enabled = false;
                    txtActivo.Enabled = false;
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
            lblCantUsuarios.Text = "Cantidad de usuarios: "+ UserBLL_08YS._usuariosLocal.Count.ToString();
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            CambiarEstado(EstadoUI.Insertando);
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

            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Error: Debe seleccionar un usuario de la grilla.");
                return;
            }
            var user = (User)dgvUsuarios.CurrentRow.DataBoundItem;

            DialogResult resp = MessageBox.Show($"¿Desea desbloquear a {user.Nombre}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                try
                {
                    _bll.DesbloquearUsuario(user.Username);

                
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
        private void EjecutarAlta()
        {
            // 1. Obtener datos de los campos
            int dni = int.Parse(txtDNI.Text);
            string nombre = txtNombres.Text.Trim();
            string apellido = txtApellidos.Text.Trim();
            string email = txtEmail.Text.Trim();
            UserRole rol = (UserRole)cmbRol.SelectedItem;

            // 2. Aplicar Reglas de Negocio para el nuevo usuario

            // Regla: Login = DNI + Nombre
            string nuevoLogin = dni.ToString() + nombre;

            // Regla: Password Inicial = DNI + Apellido
            string passwordDefault = dni.ToString() + apellido;

            // 3. Generar Seguridad
            string hashInicial, saltInicial;
            Encriptador.CrearHash(passwordDefault, out hashInicial, out saltInicial);

            // 4. Crear el objeto User 
            // Usamos valores dummy para celular y dirección 
            User nuevoUsuario = new User(
                nuevoLogin,
                dni,
                rol,
                nombre,
                apellido,
                email,
                hashInicial,
                saltInicial,
                "000000",      // Celular provisorio
                "Dirección",    // Dirección provisoria
                false          // Bloqueado = false por defecto
            );

            // 5. Enviar a la BLL
            _bll.CrearUsuario(nuevoUsuario);
        }
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var validaciones = new (bool condicion, string mensaje)[]
    {
        (!ValidarCampos(), "Complete todos los campos."),
        (!ValidarCorreo(txtEmail.Text), "Email inválido."),
        (!ValidarDNI(txtDNI.Text), "DNI inválido.")
    };

            var fallo = validaciones.FirstOrDefault(v => v.condicion);

            if (fallo.mensaje != null)
            {
                MessageBox.Show(fallo.mensaje, "Error de validación");
                return; // Cortamos el flujo, el usuario sigue en Modo Inserción para corregir
            }

            // 2. PROCESAR: Si pasó las validaciones, ejecutamos la acción
            try
            {
                if (_estadoActual == EstadoUI.Insertando)
                {
                    EjecutarAlta(); // Aquí creas el objeto User y lo mandas a la BLL
                }
                else if (_estadoActual == EstadoUI.Editando)
                {
                    // EjecutarModificacion(); 
                }

                // 3. FINALIZAR: Si todo salió bien, volvemos a consulta
                CargarGrilla();
                CambiarEstado(EstadoUI.Consulta);
                MessageBox.Show("Operación realizada con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
