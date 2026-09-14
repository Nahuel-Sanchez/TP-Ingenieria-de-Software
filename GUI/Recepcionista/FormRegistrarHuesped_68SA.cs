using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormRegistrarHuesped_68SA : Form
    {
        private readonly HuespedBLL_68SA _huespedBLL = BLLFactory_08YS.CreateHuespedBLL();

        /// <summary>
        /// Huésped que quedó dado de alta (o el ya existente, si el documento cargado
        /// pertenecía a uno) una vez que el diálogo cierra con DialogResult.OK.
        /// </summary>
        public Huesped_68SA HuespedCreado { get; private set; }

        /// <param name="documentoPrellenado">
        /// DNI que ya se tipeó en el form que abre este diálogo (por ej. FormCheckIn_68SA
        /// al buscar un acompañante), para no hacerlo cargar de nuevo.
        /// </param>
        public FormRegistrarHuesped_68SA(string documentoPrellenado = null)
        {
            InitializeComponent();

            cmbTipoDocumento.DataSource = Enum.GetValues(typeof(TipoDocumento));
            cmbTipoDocumento.SelectedIndex = -1;

            dtpFechaNacimiento.MaxDate = DateTime.Today;
            dtpFechaNacimiento.Value = null;

            if (!string.IsNullOrWhiteSpace(documentoPrellenado))
                txtDocumento.Text = documentoPrellenado.Trim();

            btnRegistrar.Click += BtnRegistrar_Click;
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            string documento = txtDocumento.RealText.Trim();

            try
            {
                if (_huespedBLL.Exists(documento))
                {
                    MessageBox.Show(
                        "Ya existe un huésped registrado con ese documento. Buscalo en lugar de registrarlo de nuevo.",
                        "Huésped existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoHuesped = new Huesped_68SA
                {
                    Nombre = txtNombre.RealText.Trim(),
                    Apellido = txtApellido.RealText.Trim(),
                    Documento = documento,
                    TipoDocumento = (TipoDocumento)cmbTipoDocumento.SelectedItem,
                    Nacionalidad = txtNacionalidad.RealText.Trim(),
                    FechaNacimiento = dtpFechaNacimiento.Value.Value.Date,
                    Telefono = txtTelefono.RealText.Trim(),
                    Email = txtEmail.RealText.Trim()
                };

                // Si por alguna carrera ya existiera, ObtenerOCrear devuelve el existente
                // en lugar de duplicarlo; en el camino normal, lo da de alta.
                HuespedCreado = _huespedBLL.ObtenerOCrear(nuevoHuesped);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidarCampos()
        {
            const string tituloError = "Datos incompletos";

            if (string.IsNullOrWhiteSpace(txtNombre.RealText))
            {
                MessageBox.Show("Ingresá el nombre del huésped.", tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.RealText))
            {
                MessageBox.Show("Ingresá el apellido del huésped.", tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbTipoDocumento.SelectedItem == null)
            {
                MessageBox.Show("Seleccioná el tipo de documento.", tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDocumento.RealText))
            {
                MessageBox.Show("Ingresá el número de documento.", tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!dtpFechaNacimiento.Value.HasValue)
            {
                MessageBox.Show("Seleccioná la fecha de nacimiento.", tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpFechaNacimiento.Value.Value.Date > DateTime.Today)
            {
                MessageBox.Show("La fecha de nacimiento no puede ser futura.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtEmail.RealText) && !EsEmailValido(txtEmail.RealText.Trim()))
            {
                MessageBox.Show("El email ingresado no tiene un formato válido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private static bool EsEmailValido(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}