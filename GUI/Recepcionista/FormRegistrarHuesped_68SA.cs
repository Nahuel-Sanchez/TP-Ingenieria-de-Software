using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using Service_08YS;
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
            TraducirControles(this);
            Text = TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_lblTitulo");

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
            var tipoDocumento = (TipoDocumento)cmbTipoDocumento.SelectedItem;
            string nacionalidad = string.IsNullOrWhiteSpace(txtNacionalidad.RealText) ? null : txtNacionalidad.RealText.Trim();

            try
            {
                if (_huespedBLL.ExistePorClaveCompleta(documento, tipoDocumento, nacionalidad))
                {
                    MessageBox.Show(
                        TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_msgHuespedExistente"),
                        TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_tituloHuespedExistente"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoHuesped = new Huesped_68SA
                {
                    Nombre = txtNombre.RealText.Trim(),
                    Apellido = txtApellido.RealText.Trim(),
                    Documento = documento,
                    TipoDocumento = tipoDocumento,
                    Nacionalidad = nacionalidad,
                    FechaNacimiento = dtpFechaNacimiento.Value.Value.Date,
                    Telefono = txtTelefono.RealText.Trim(),
                    Email = txtEmail.RealText.Trim()
                };

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
            string tituloError = TraductorManager_08YS.Instance.GetTexto("Comun_msgDatosIncompletos");

            if (string.IsNullOrWhiteSpace(txtNombre.RealText))
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_msgIngreseNombre"), tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.RealText))
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_msgIngreseApellido"), tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbTipoDocumento.SelectedItem == null)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_msgSeleccioneTipoDocumento"), tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDocumento.RealText))
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_msgIngreseDocumento"), tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!dtpFechaNacimiento.Value.HasValue)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_msgSeleccioneFechaNacimiento"), tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpFechaNacimiento.Value.Value.Date > DateTime.Today)
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_msgFechaNacimientoFutura"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloErrorValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtEmail.RealText) && !EsEmailValido(txtEmail.RealText.Trim()))
            {
                MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("RegistrarHuesped_msgEmailInvalido"), TraductorManager_08YS.Instance.GetTexto("Comun_tituloErrorValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private static bool EsEmailValido(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag != null && c.Tag is string tag && !string.IsNullOrWhiteSpace(tag))
                    c.Text = TraductorManager_08YS.Instance.GetTexto(tag);

                if (c.HasChildren)
                    TraducirControles(c);
            }
        }
    }
}