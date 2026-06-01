using BLL_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_08YS.Admin
{
    public partial class FormAccesoAM : Form
    {
        private readonly TipoEntidad _tipo;
        private readonly OperacionABM _operacion;
        private readonly FamiliaBLL_08YS _familiaBLL;
        private readonly RolBLL_08YS _rolBLL;
        private readonly Familia_08YS _familiaAEditar;
        private readonly Rol_08YS _rolAEditar;
        private readonly Action _onGuardado;
        private readonly Action _onCancelado;

        private List<AccessComponent> _componentesSeleccionados = new List<AccessComponent>();

        public FormAccesoAM(
            TipoEntidad tipo,
            OperacionABM operacion,
            FamiliaBLL_08YS familiaBLL,
            RolBLL_08YS rolBLL,
            Familia_08YS familiaAEditar,
            Rol_08YS rolAEditar,
            Action onGuardado,
            Action onCancelado)
        {
            InitializeComponent();
            _tipo = tipo;
            _operacion = operacion;
            _familiaBLL = familiaBLL;
            _rolBLL = rolBLL;
            _familiaAEditar = familiaAEditar;
            _rolAEditar = rolAEditar;
            _onGuardado = onGuardado;
            _onCancelado = onCancelado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();

                if (_tipo == TipoEntidad.Familia)
                {
                    if (_operacion == OperacionABM.Alta)
                        _familiaBLL.Crear(nombre, _componentesSeleccionados);
                    else
                        _familiaBLL.Modificar(_familiaAEditar.FamiliaID, nombre, _componentesSeleccionados);
                }
                else
                {
                    if (_operacion == OperacionABM.Alta)
                        _rolBLL.Crear(nombre, _componentesSeleccionados);
                    else
                        _rolBLL.Modificar(_rolAEditar.RolID, nombre, _componentesSeleccionados);
                }

                _onGuardado?.Invoke();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error de validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
            => _onCancelado?.Invoke();
    }
}
