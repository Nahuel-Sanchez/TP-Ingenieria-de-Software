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

namespace GUI
{
    public partial class FormGestionUsuarios : Form
    {
        public FormGestionUsuarios()
        {
            InitializeComponent();
        }
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
        private void btnCrear_Click(object sender, EventArgs e)
        {
            var validaciones = new (bool condicion, string mensaje)[]
           {
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
    }
}
