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
    public partial class FormGestionUsuarios_08YS : Form
    {
        private UserBLL_08YS _bll = BLLFactory_08YS.CreateUserBLL();
        
        public FormGestionUsuarios_08YS()
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
                    btnActDes.BackColor=Color.White;
                    btnActDes.ForeColor = Color.Black;
                    txtLogin.Enabled = false;
                    txtBloqueado.Enabled = false;
                    txtActivo.Enabled = false;
                    txtMensaje.Text = "Modo Inserción: Complete los datos y presione Aplicar.";
                    txtDNI.Focus();
                    break;
                case EstadoUI.Editando:
                    SetCamposReadOnly(false);
                    btnCrear.Enabled = false;
                    btnModificar.Enabled = false;
                    btnDesbloquear.Enabled = false;
                    btnAplicar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnActDes.Enabled = false;
                    btnActDes.BackColor = Color.White;
                    btnActDes.ForeColor = Color.Black;
                    txtLogin.Enabled = false;
                    txtBloqueado.Enabled = false;
                    txtActivo.Enabled = false;
                    txtEmail.Enabled = true;
                    cmbRol.Enabled = true;
                    txtMensaje.Text = "Modo Edicion: Complete los datos y presione Aplicar.";
                    txtEmail.Focus();
                    break;
            }
        }

        private void SetCamposReadOnly(bool status)
        {
            // Si status es true (Consulta), todo bloqueado.
            // Si status es false, depende de si es Inserción o Edición.
            bool esEdicion = _estadoActual == EstadoUI.Editando;

            // Campos que NUNCA se editan (DNI, Nombres, Apellidos, Login)
            // Se bloquean si estamos en Consulta O si estamos en Edición.
            txtDNI.ReadOnly = status || esEdicion;
            txtNombres.ReadOnly = status || esEdicion;
            txtApellidos.ReadOnly = status || esEdicion;
            txtLogin.ReadOnly = status || esEdicion;

            // Campos que SÍ se pueden editar (Email y Rol)
            // Solo se bloquean si status es true (Consulta).
            txtEmail.ReadOnly = status;
            cmbRol.Enabled = !status;

            // Campos de estado (Bloqueado/Activo) - Siempre solo lectura para el usuario
            txtBloqueado.ReadOnly = true;
            txtActivo.ReadOnly = true;
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
        #region Grilla
        private void CargarGrilla()
        {
            dgvUsuarios.DataSource = null;
            var listaCompleta = _bll.GetAll();
            //var listaCompleta = UserBLL_08YS._usuariosLocal;

            // Aplicamos el filtro según el RadioButton seleccionado
            if (rbBloqueados.Checked)
            {
                // Solo los que tienen Bloqueado == true
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = listaCompleta.Where(u => u.Bloqueado == true).ToList();
            }
            else if (rbActivos.Checked)
            {
                // Solo los que tienen Activo == true
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = listaCompleta.Where(u => u.Activo == true).ToList();
            }
            else
            { 
                // Todos los usuarios
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = listaCompleta;

            }

                // Actualizamos el contador
                int cantidadMostrada = dgvUsuarios.Rows.Count;
            lblCantUsuarios.Text = $"Cantidad mostrada: {cantidadMostrada}";
        }
      
      
        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (_estadoActual != EstadoUI.Consulta)
            {
                return;
            }

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
                txtActivo.Text = user.Activo.ToString();

                btnModificar.Enabled = true;
                btnDesbloquear.Enabled = user.Bloqueado;

                if (user.Activo)
                {
                    btnActDes.Text = "Desactivar Usuario";
                    btnActDes.BackColor = Color.LightCoral; // Un tono rojo suave
                    btnActDes.ForeColor = Color.White;      // Texto blanco para contraste
                }
                else
                {
                    btnActDes.Text = "Activar Usuario";
                    btnActDes.BackColor = Color.MediumSeaGreen; // Un tono verde suave
                    btnActDes.ForeColor = Color.White;
                }
                btnActDes.Enabled = true;
            }
        }
        #endregion
        #region Botones
        private void btnCrear_Click(object sender, EventArgs e)
        {
            CambiarEstado(EstadoUI.Insertando);
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            CambiarEstado(EstadoUI.Editando);
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
        
        private void btnActDes_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            var user = (User)dgvUsuarios.CurrentRow.DataBoundItem;
            string accion = user.Activo ? "desactivar" : "activar";

            DialogResult resp = MessageBox.Show($"¿Está seguro que desea {accion} al usuario {user.Username}?",
                                "Confirmar Cambio de Estado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                try
                {
                  
                    _bll.AlternarEstadoActivo(user.Username);

                    MessageBox.Show($"Usuario {user.Username} actualizado correctamente.");

                    CargarGrilla();
                    CambiarEstado(EstadoUI.Consulta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
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

            // PROCESAR: Si pasó las validaciones, ejecutamos la acción
            try
            {
                if (_estadoActual == EstadoUI.Insertando)
                {
                    EjecutarAlta(); // Aca se crea el objeto User y se manda a la BLL
                }
                else if (_estadoActual == EstadoUI.Editando)
                {
                    EjecutarModificacion();
                }

                // FINALIZAR: Si todo salió bien, volvemos a consulta
                CargarGrilla();
                CambiarEstado(EstadoUI.Consulta);
                MessageBox.Show("Operación realizada con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarEstado(EstadoUI.Consulta);
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        private void EjecutarAlta()
        {
            // 1. Obtener datos de los campos
            int dni = int.Parse(txtDNI.Text);
            string nombre = txtNombres.Text.Trim();
            string apellido = txtApellidos.Text.Trim();
            string email = txtEmail.Text.Trim();
            UserRole rol = (UserRole)cmbRol.SelectedItem;

             // 2. Enviar a la BLL
            _bll.CrearUsuario(dni,nombre,apellido,email,rol);
        }
        private void EjecutarModificacion()
        {
            string username = txtLogin.Text; // Usamos el login como ID
            string email = txtEmail.Text;
            UserRole rol = (UserRole)cmbRol.SelectedItem;

            // Llamada a la BLL
            _bll.ModificarUsuario(username, email, rol);
        }

        #region Filtros
        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked)
            {
                CargarGrilla();
            }
        }

        private void rbBloqueados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBloqueados.Checked)
            {
                CargarGrilla();
            }
        }
        private void rbActivos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActivos.Checked)
            {
                CargarGrilla();
            }
        }




        #endregion

      
    }
}
