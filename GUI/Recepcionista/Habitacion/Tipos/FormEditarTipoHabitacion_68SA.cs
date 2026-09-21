using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using Service_08YS;
using System;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormEditarTipoHabitacion_68SA : Form
    {
        private readonly TipoHabitacionBLL_68SA _tipoBLL = BLLFactory_08YS.CreateTipoHabitacionBLL();
        private readonly TipoHabitacion _tipo; // null = alta

        public FormEditarTipoHabitacion_68SA(TipoHabitacion tipo = null)
        {
            InitializeComponent();
            _tipo = tipo;

            TraducirControles(this);

            var t = TraductorManager_08YS.Instance;
            txtNombre.PlaceholderText = t.GetTexto("Tipos_lblNombre");
            txtDescripcion.PlaceholderText = t.GetTexto("Tipos_lblDescripcion");

            string titulo = t.GetTexto(tipo == null ? "Tipos_tituloNuevo" : "Tipos_tituloEditar");
            lblTitulo.Text = titulo;
            Text = titulo;

            if (tipo != null)
            {
                txtNombre.Text = tipo.Nombre;
                txtDescripcion.Text = tipo.Descripcion ?? string.Empty;
                nudCapacidad.Value = tipo.Capacidad;
                nudTarifa.Value = tipo.TarifaNoche;
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

            if (string.IsNullOrWhiteSpace(txtNombre.RealText))
            {
                MessageBox.Show(t.GetTexto("Tipos_msgIngreseNombre"), t.GetTexto("Comun_msgDatosIncompletos"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tipo = new TipoHabitacion
            {
                Id = _tipo?.Id ?? 0,
                Nombre = txtNombre.RealText.Trim(),
                Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.RealText) ? null : txtDescripcion.RealText.Trim(),
                Capacidad = (int)nudCapacidad.Value,
                TarifaNoche = nudTarifa.Value
            };

            try
            {
                if (_tipo == null) _tipoBLL.Crear(tipo);
                else _tipoBLL.Modificar(tipo);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (TipoHabitacionNombreDuplicadoException_68SA)
            {
                MessageBox.Show(t.GetTexto("Tipos_msgNombreDuplicado"), t.GetTexto("Comun_tituloErrorValidacion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TipoHabitacionNoEncontradoException_68SA)
            {
                MessageBox.Show(t.GetTexto("Tipos_msgNoEncontrado"), t.GetTexto("Comun_tituloErrorValidacion"),
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