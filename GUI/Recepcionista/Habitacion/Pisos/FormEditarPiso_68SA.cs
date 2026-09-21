using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using Service_08YS;
using System;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormEditarPiso_68SA : Form
    {
        private readonly PisoBLL_68SA _pisoBLL = BLLFactory_08YS.CreatePisoBLL();
        private readonly Piso_68SA _piso; // null = alta

        public FormEditarPiso_68SA(Piso_68SA piso = null)
        {
            InitializeComponent();
            _piso = piso;

            TraducirControles(this);

            var t = TraductorManager_08YS.Instance;
            txtNumero.PlaceholderText = t.GetTexto("Pisos_lblNumero");
            txtNombre.PlaceholderText = t.GetTexto("Pisos_lblNombre");

            string titulo = t.GetTexto(piso == null ? "Pisos_tituloNuevo" : "Pisos_tituloEditar");
            lblTitulo.Text = titulo;
            Text = titulo;

            if (piso != null) //modificacion
            {
                txtNumero.Text = piso.Numero.ToString();
                txtNombre.Text = piso.Nombre;
            }

            CancelButton = btnCancelar;
            btnGuardar.Click += (s, e) => Guardar();
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag is string tag && !string.IsNullOrWhiteSpace(tag))
                    c.Text = TraductorManager_08YS.Instance.GetTexto(tag);

                if (c.HasChildren)
                    TraducirControles(c);
            }
        }

        private void Guardar()
        {
            var t = TraductorManager_08YS.Instance;
            string tituloIncompleto = t.GetTexto("Comun_msgDatosIncompletos");

            if (!int.TryParse(txtNumero.RealText.Trim(), out int numero))
            {
                MessageBox.Show(t.GetTexto("Pisos_msgIngreseNumero"), tituloIncompleto, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.RealText))
            {
                MessageBox.Show(t.GetTexto("Pisos_msgIngreseNombre"), tituloIncompleto, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var piso = new Piso_68SA
            {
                PisoId = _piso?.PisoId ?? 0,
                Numero = numero,
                Nombre = txtNombre.RealText.Trim()
            };

            try
            {
                if (_piso == null) _pisoBLL.Crear(piso);
                else _pisoBLL.Modificar(piso);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (PisoNumeroDuplicadoException_68SA)
            {
                MessageBox.Show(t.GetTexto("Pisos_msgNumeroDuplicado"), t.GetTexto("Comun_tituloErrorValidacion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (PisoNoEncontradoException_68SA)
            {
                MessageBox.Show(t.GetTexto("Pisos_msgNoEncontrado"), t.GetTexto("Comun_tituloErrorValidacion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK; // cierra y hace que el listado se refresque
                Close();
            }
            catch (DatosInvalidosException_68SA ex)
            {
                MessageBox.Show(ex.Message, t.GetTexto("Comun_tituloErrorValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}