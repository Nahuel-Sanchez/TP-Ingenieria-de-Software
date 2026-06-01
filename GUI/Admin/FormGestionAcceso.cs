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
    public enum TipoEntidad { Familia, Rol }
    public enum OperacionABM { Alta, Modificacion }

    public partial class FormGestionAcceso : Form
    {
        private readonly TipoEntidad _tipo;
        private readonly FamiliaBLL_08YS _familiaBLL;
        private readonly RolBLL_08YS _rolBLL;
        private readonly Action<Form> _openChildForm;

        public FormGestionAcceso(TipoEntidad tipo, Action<Form> openChildForm)
        {
            InitializeComponent();
            _tipo = tipo;
            _familiaBLL = BLLFactory_08YS.CreateFamiliaBLL();
            _rolBLL = BLLFactory_08YS.CreateRolBLL();
            _openChildForm = openChildForm;

            string entidad = _tipo == TipoEntidad.Familia ? "Familias" : "Roles";
            Text = $"Gestión de {entidad}";
            lblTitulo.Text = $"Gestión de {entidad}";
        }

        private void AbrirABM(
        OperacionABM operacion,
        Familia_08YS familiaAEditar = null,
        Rol_08YS rolAEditar = null)
        {
            // Captura referencia a este form antes de que el panel lo reemplace
            var formGestion = this;

            Action onGuardado = () =>
            {
                formGestion.CargarGrid();
                _openChildForm(formGestion);   // vuelve a mostrar FormGestion actualizado
            };

            Action onCancelado = () =>
            {
                _openChildForm(formGestion);   // vuelve a FormGestion sin cambios
            };

            var formABM = new FormAccesoAM_08YS(
                _tipo,
                operacion,
                _familiaBLL,
                _rolBLL,
                familiaAEditar,
                rolAEditar,
                onGuardado,
                onCancelado);

            _openChildForm(formABM);           // reemplaza FormGestion con FormABM en el panel
        }

        #region Buttons

        private void btnCrear_Click(object sender, EventArgs e) => AbrirABM(OperacionABM.Alta);

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvEntidades.CurrentRow == null) return;

            dynamic row = dgvEntidades.CurrentRow.DataBoundItem;

            if (_tipo == TipoEntidad.Familia)
                AbrirABM(OperacionABM.Modificacion, familiaAEditar: (Familia_08YS)row.Entidad);
            else
                AbrirABM(OperacionABM.Modificacion, rolAEditar: (Rol_08YS)row.Entidad);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEntidades.CurrentRow == null) return;

            dynamic row = dgvEntidades.CurrentRow.DataBoundItem;
            string nombre = row.Nombre;
            object entidad = row.Entidad;

            var confirmacion = MessageBox.Show(
                $"¿Desea eliminar \"{nombre}\"?",
                "Confirmar eliminación",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.OK) return;

            try
            {
                if (_tipo == TipoEntidad.Familia)
                    _familiaBLL.Eliminar(((Familia_08YS)entidad).FamiliaID);
                else
                    _rolBLL.Eliminar(((Rol_08YS)entidad).RolID);

                CargarGrid();
            }
            catch (InvalidOperationException ex)
            {
                // Excepción de negocio: mensaje entendible para el usuario
                MessageBox.Show(ex.Message, "No se puede eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Error técnico inesperado
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dgvEntidades_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
            ActualizarTreeView();
        }

        private void ActualizarEstadoBotones()
        {
            bool hay = dgvEntidades.CurrentRow != null;
            btnModificar.Enabled = hay;
            btnEliminar.Enabled = hay;
        }

        #region Treeview

        private void ActualizarTreeView()
        {
            trvDetalle.Nodes.Clear();
            if (dgvEntidades.CurrentRow == null) return;

            // La columna Entidad guarda el objeto original
            dynamic row = dgvEntidades.CurrentRow.DataBoundItem;
            object entidad = row.Entidad;

            TreeNode raiz;
            if (_tipo == TipoEntidad.Familia)
            {
                var f = (Familia_08YS)entidad;
                raiz = CrearNodo(f);
            }
            else
            {
                var r = (Rol_08YS)entidad;
                raiz = new TreeNode(r.Nombre);
                PopularNodos(r.Componentes, raiz.Nodes);
            }

            raiz.Expand();
            trvDetalle.Nodes.Add(raiz);
        }

        private TreeNode CrearNodo(AccessComponent_08YS componente)
        {
            var nodo = new TreeNode(
                componente is Familia_08YS ? $"📁 {componente.Nombre}" : $"🔑 {componente.Nombre}");

            if (componente is Familia_08YS f)
                PopularNodos(f.Hijos, nodo.Nodes);

            return nodo;
        }

        private void PopularNodos(IEnumerable<AccessComponent_08YS> componentes, TreeNodeCollection destino)
        {
            foreach (var c in componentes)
                destino.Add(CrearNodo(c));
        }

        #endregion

        private void FormGestionAcceso_Load(object sender, EventArgs e)
        {
            ConfigurarPorTipo();
            //ConfigurarBotones();
            CargarGrid();
        }

        private void ConfigurarPorTipo()
        {
            bool esFamilia = _tipo == TipoEntidad.Familia;

            lblTitulo.Text = esFamilia ? "Gestión de Familias" : "Gestión de Roles";
            iconPictureBox.IconChar = esFamilia
                ? FontAwesome.Sharp.IconChar.LayerGroup
                : FontAwesome.Sharp.IconChar.UserShield;
        }

        private void CargarGrid()
        {
            try
            {
                trvDetalle.Nodes.Clear();

                if (_tipo == TipoEntidad.Familia)
                {
                    var familias = _familiaBLL.GetAll();
                    dgvEntidades.DataSource = familias
                        .Select(f => new { f.Nombre, TipoDisplay = "Familia", Entidad = (object)f })
                        .ToList();
                }
                else
                {
                    var roles = _rolBLL.GetAll();
                    dgvEntidades.DataSource = roles
                        .Select(r => new { r.Nombre, TipoDisplay = "Rol", Entidad = (object)r })
                        .ToList();
                }

                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
