using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormEditarHabitacion_68SA : Form
    {
        private readonly HabitacionBLL_68SA _habitacionBLL = BLLFactory_08YS.CreateHabitacionBLL();
        private readonly Habitacion_68SA _habitacion; // null = alta

        public FormEditarHabitacion_68SA(Habitacion_68SA habitacion = null)
        {
            InitializeComponent();
            _habitacion = habitacion;

            TraducirControles(this);

            var t = TraductorManager_08YS.Instance;
            txtNumero.PlaceholderText = t.GetTexto("Habitaciones_lblNumero");

            string titulo = t.GetTexto(habitacion == null ? "Habitaciones_tituloNueva" : "Habitaciones_tituloEditar");
            lblTitulo.Text = titulo;
            Text = titulo;

            foreach (var piso in BLLFactory_08YS.CreatePisoBLL().GetAll())
                cmbPiso.Items.Add(new ItemCombo_68SA(piso.PisoId, $"{piso.Numero} - {piso.Nombre}"));

            foreach (var tipo in BLLFactory_08YS.CreateTipoHabitacionBLL().GetAll())
                cmbTipo.Items.Add(new ItemCombo_68SA(tipo.Id, tipo.Nombre));

            if (habitacion != null)
            {
                txtNumero.Text = habitacion.NroHabitacion;
                ItemCombo_68SA.Seleccionar(cmbPiso, habitacion.Piso.PisoId);
                ItemCombo_68SA.Seleccionar(cmbTipo, habitacion.Tipo.Id);

                // Con reservas Confirmada/En curso el tipo no se puede cambiar (el BLL también lo valida)
                if (habitacion.CantidadReservasActivas > 0)
                {
                    cmbTipo.Enabled = false;
                    cmbTipo.ForeColor = Color.Gray;
                    lblAvisoTipo.Text = t.GetTexto("Habitaciones_msgTipoBloqueado");
                    lblAvisoTipo.Visible = true;
                }
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

            if (string.IsNullOrWhiteSpace(txtNumero.RealText))
            {
                MessageBox.Show(t.GetTexto("Habitaciones_msgIngreseNumero"), tituloIncompleto, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? pisoId = ItemCombo_68SA.IdSeleccionado(cmbPiso);
            if (!pisoId.HasValue)
            {
                MessageBox.Show(t.GetTexto("Habitaciones_msgSeleccionePiso"), tituloIncompleto, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? tipoId = ItemCombo_68SA.IdSeleccionado(cmbTipo);
            if (!tipoId.HasValue)
            {
                MessageBox.Show(t.GetTexto("Habitaciones_msgSeleccioneTipo"), tituloIncompleto, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // El Estado no se edita acá: el alta nace Disponible y la modificación no lo toca
            var habitacion = new Habitacion_68SA
            {
                Id = _habitacion?.Id ?? 0,
                NroHabitacion = txtNumero.RealText.Trim(),
                Piso = new Piso_68SA { PisoId = pisoId.Value },
                Tipo = new TipoHabitacion { Id = tipoId.Value }
            };

            try
            {
                if (_habitacion == null) _habitacionBLL.Crear(habitacion);
                else _habitacionBLL.Modificar(habitacion);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (HabitacionNumeroDuplicadoException_68SA)
            {
                MessageBox.Show(t.GetTexto("Habitaciones_msgNumeroDuplicado"), t.GetTexto("Comun_tituloErrorValidacion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (HabitacionConReservasActivasException_68SA)
            {
                MessageBox.Show(t.GetTexto("Habitaciones_msgTipoBloqueado"), t.GetTexto("Comun_tituloErrorValidacion"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (HabitacionNoEncontradaException_68SA)
            {
                MessageBox.Show(t.GetTexto("Habitaciones_msgNoEncontrada"), t.GetTexto("Comun_tituloErrorValidacion"),
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