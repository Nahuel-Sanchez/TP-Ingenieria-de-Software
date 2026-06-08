using BLL_08YS;
using Service_08YS;
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
using static Service_08YS.Entities.Acceso.ResultadoEvaluacion_08YS;

namespace GUI_08YS.Admin
{
    public enum TipoEntidad { Familia, Rol }
    public enum OperacionAM { Alta, Modificacion }

    public partial class FormGestionAcceso_08YS : Form
    {
        private static readonly Dictionary<string, Permisos> _mapaFamilia =
            new Dictionary<string, Permisos>
            {
                { nameof(btnCrear),     Permisos.CrearFamilias     },
                { nameof(btnModificar), Permisos.ModificarFamilias },
                { nameof(btnEliminar),  Permisos.EliminarFamilias  },
            };

        private static readonly Dictionary<string, Permisos> _mapaRol =
            new Dictionary<string, Permisos>
            {
                { nameof(btnCrear),     Permisos.CrearRoles     },
                { nameof(btnModificar), Permisos.ModificarRoles },
                { nameof(btnEliminar),  Permisos.EliminarRoles  },
            };


        private readonly TipoEntidad _modo;
        private readonly FamiliaBLL_08YS _familiaBLL;
        private readonly RolBLL_08YS _rolBLL;
        private readonly Action<Form> _openChildForm;

        public FormGestionAcceso_08YS(TipoEntidad tipo, Action<Form> openChildForm)
        {
            InitializeComponent();
            _modo = tipo;
            _familiaBLL = BLLFactory_08YS.CreateFamiliaBLL();
            _rolBLL = BLLFactory_08YS.CreateRolBLL();
            _openChildForm = openChildForm;
            string entidad = _modo == TipoEntidad.Familia ? "Familias" : "Roles";
            Text = $"Gestión de {entidad}";
            lblTitulo.Text = $"Gestión de {entidad}";

            dgvEntidades.AutoGenerateColumns = false;
        }

        private void FormGestionAcceso_Load(object sender, EventArgs e)
        {
            ConfigurarPorTipo();
            CargarGrid();
        }

        private void AplicarPermisos()
        {
            var mapa = _modo == TipoEntidad.Familia ? _mapaFamilia : _mapaRol;
            PermissionFilter_08YS.Aplicar(this, mapa);
        }

        private void ConfigurarPorTipo()
        {
            bool esFamilia = _modo == TipoEntidad.Familia;

            lblTitulo.Text = esFamilia ? "Gestión de Familias" : "Gestión de Roles";
            iconPictureBox.IconChar = esFamilia
                ? FontAwesome.Sharp.IconChar.LayerGroup
                : FontAwesome.Sharp.IconChar.UserShield;

            AplicarPermisos();

            if( esFamilia && 
                   !SessionManager_08YS.Instance.HasPermission(Permisos.CrearFamilias    )  &&
                   !SessionManager_08YS.Instance.HasPermission(Permisos.ModificarFamilias)  &&
                   !SessionManager_08YS.Instance.HasPermission(Permisos.EliminarFamilias )
              )
              pnlBottom.Visible = false;

            else if( !esFamilia && 
                       !SessionManager_08YS.Instance.HasPermission(Permisos.CrearRoles    )  &&
                       !SessionManager_08YS.Instance.HasPermission(Permisos.ModificarRoles)  &&
                       !SessionManager_08YS.Instance.HasPermission(Permisos.EliminarRoles )
                   )
                   pnlBottom.Visible = false;
        }

        private void CargarGrid()
        {
            try
            {
                trvDetalle.Nodes.Clear();

                if (_modo == TipoEntidad.Familia)
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

        private void AbrirAM(
            OperacionAM operacion,
            Familia_08YS familiaAEditar = null,
            Rol_08YS rolAEditar = null)
        {
            // Captura referencia a este form antes de que el panel lo reemplace
            var formGestion = this;

            Action onSave = () =>
            {
                formGestion.CargarGrid();
                _openChildForm(formGestion);   // vuelve a mostrar FormGestion actualizado
            };

            Action onCancel = () =>
                _openChildForm(formGestion);   // vuelve a FormGestion sin cambios

            var formABM = new FormAccesoAM_08YS(
                _modo,
                operacion,
                _familiaBLL,
                _rolBLL,
                familiaAEditar,
                rolAEditar,
                onSave,
                onCancel);

            _openChildForm(formABM);           // reemplaza FormGestion por FormAM en el panel del MDI
        }

        #region Buttons

        private void btnCrear_Click(object sender, EventArgs e) => AbrirAM(OperacionAM.Alta);

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvEntidades.CurrentRow == null) return;

            dynamic row = dgvEntidades.CurrentRow.DataBoundItem;

            //infiero el dynamic en funcion del tipo de entidad que se esta gestionando
            if (_modo == TipoEntidad.Familia)
                AbrirAM(OperacionAM.Modificacion, familiaAEditar: (Familia_08YS)row.Entidad);
            else
                AbrirAM(OperacionAM.Modificacion, rolAEditar: (Rol_08YS)row.Entidad);
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
                if (_modo == TipoEntidad.Familia)
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
                // Error inesperado
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEstadoBotones()
        {
            bool hay = dgvEntidades.CurrentRow != null;
            btnModificar.Enabled = hay;
            btnEliminar.Enabled = hay;
        }

        #endregion

        private void dgvEntidades_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
            ActualizarTreeView();
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
            if (_modo == TipoEntidad.Familia)
                raiz = CrearNodo((Familia_08YS)entidad);
            else
            {
                var rol = (Rol_08YS)entidad;
                raiz = new TreeNode(rol.Nombre);
                PopularNodos(rol.Componentes, raiz.Nodes);
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

        private void trvDetalle_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            bool seleccionado = (e.State & TreeNodeStates.Selected) != 0;

            Color colorFondo = seleccionado
                ? Color.FromArgb(5, 5, 100)
                : Color.FromArgb(5, 10, 40);

            Color colorTexto = seleccionado ? Color.Gold : Color.White;

            using (var brush = new SolidBrush(colorFondo))
                e.Graphics.FillRectangle(brush, e.Bounds);

            TextRenderer.DrawText(
                e.Graphics,
                e.Node.Text,
                trvDetalle.Font,
                e.Bounds,
                colorTexto,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        #endregion

    }
}
