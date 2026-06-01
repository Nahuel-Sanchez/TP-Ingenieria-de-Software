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
    public partial class FormAccesoAM_08YS : Form
    {
        private readonly TipoEntidad _tipo;
        private readonly OperacionAM _operacion;
        private readonly FamiliaBLL_08YS _familiaBLL;
        private readonly RolBLL_08YS _rolBLL;
        private readonly Familia_08YS _familiaAEditar;
        private readonly Rol_08YS _rolAEditar;
        private readonly Action _onGuardado;
        private readonly Action _onCancelado;

        private List<AccessComponent_08YS> _seleccionados = new List<AccessComponent_08YS>();
        private List<AccessComponent_08YS> _disponibles = new List<AccessComponent_08YS>();

        // Evita cascada de eventos entre los dos dgv durante refreshes
        private bool _actualizandoSeleccion = false;
        private AccesoBLL BLL => _tipo == TipoEntidad.Familia
       ? (AccesoBLL)_familiaBLL
       : _rolBLL;

        public FormAccesoAM_08YS(
            TipoEntidad tipo,
            OperacionAM operacion,
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

            dgvDisponibles.AutoGenerateColumns = false;
            dgvSeleccionados.AutoGenerateColumns = false;
        }

        private void FormAccesoAM_08YS_Load(object sender, EventArgs e)
        {
            ConfigurarSegunTipo();
            //ConfigurarBotones();
            CargarDatos();
        }

        private void ConfigurarSegunTipo()
        {
            bool esFamilia = _tipo == TipoEntidad.Familia;

            iconPictureBox.IconChar = esFamilia
                ? FontAwesome.Sharp.IconChar.LayerGroup
                : FontAwesome.Sharp.IconChar.UserShield;

            string entidad = esFamilia ? "Familia" : "Rol";
            string operacion = _operacion == OperacionAM.Alta ? "Nueva" : "Modificar";
            lblTitulo.Text = $"{operacion} {entidad}";

            if (_operacion == OperacionAM.Modificacion)
                txtNombre.Text = esFamilia ? _familiaAEditar.Nombre : _rolAEditar.Nombre;
        }

        private void CargarDatos()
        {
            // Precargar seleccionados en Modificacion
            if (_operacion == OperacionAM.Modificacion)
            {
                _seleccionados = _tipo == TipoEntidad.Familia
                    ? _familiaAEditar.Hijos.ToList()
                    : _rolAEditar.Componentes.ToList();
            }

            int? familiaId = null;

            if (_operacion == OperacionAM.Modificacion)
                familiaId = _familiaAEditar.FamiliaID;

            var todos = _tipo == TipoEntidad.Familia
                ? _familiaBLL.GetComponentesDisponibles(familiaId)
                : _rolBLL.GetComponentesDisponibles();
            _disponibles = todos
                .Where(c => !_seleccionados.Any(s => MismoComponente(s, c)))
                .ToList();

            RefrescarDGVSeleccionados();
            RefrescarDGVDisponibles();
        }

        private void dgvSeleccionados_SelectionChanged(object sender, EventArgs e)
        {
            if (_actualizandoSeleccion) return;

            _actualizandoSeleccion = true;
            dgvDisponibles.ClearSelection();
            _actualizandoSeleccion = false;

            var row = dgvSeleccionados.CurrentRow?.DataBoundItem as ComponenteRow;
            if (row != null) ActualizarDetalle(row.Componente);
            else LimpiarDetalle();
        }

        private void dgvDisponibles_SelectionChanged(object sender, EventArgs e)
        {
            if (_actualizandoSeleccion) return;

            _actualizandoSeleccion = true;
            dgvSeleccionados.ClearSelection();
            _actualizandoSeleccion = false;

            var row = dgvDisponibles.CurrentRow?.DataBoundItem as ComponenteRow;
            if (row != null) ActualizarDetalle(row.Componente);
            else LimpiarDetalle();
        }

        private void ActualizarDetalle(AccessComponent_08YS componente)
        {
            if (componente is Permiso_08YS permiso)
            {
                lblDescripcion.Text = string.IsNullOrWhiteSpace(permiso.Descripcion)
                    ? "(Sin descripción)"
                    : permiso.Descripcion;
                lblDescripcion.Visible = true;
                trvDetalle.Visible = false;
            }
            else if (componente is Familia_08YS familia)
            {
                trvDetalle.Nodes.Clear();
                var raiz = CrearNodo(familia);
                raiz.Expand();
                trvDetalle.Nodes.Add(raiz);
                trvDetalle.Visible = true;
                lblDescripcion.Visible = false;
            }
        }

        private void LimpiarDetalle()
        {
            lblDescripcion.Visible = false;
            trvDetalle.Visible = false;
            trvDetalle.Nodes.Clear();
        }

        private TreeNode CrearNodo(AccessComponent_08YS componente)
        {
            var nodo = new TreeNode(
                componente is Familia_08YS ? $"📁 {componente.Nombre}" : $"🔑 {componente.Nombre}");

            if (componente is Familia_08YS f)
                foreach (var hijo in f.Hijos)
                    nodo.Nodes.Add(CrearNodo(hijo));

            return nodo;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvDisponibles.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un componente para agregar.", "Sin selección",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var candidato = (dgvDisponibles.CurrentRow.DataBoundItem as ComponenteRow)?.Componente;
            if (candidato == null)
            {
                MessageBox.Show("Error al obtener el componente seleccionado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var resultado = BLL.EvaluarAgregarComponente(_seleccionados, candidato);

            switch (resultado.Resultado)
            {
                case ResultadoEvaluacion_08YS.Tipo.Valido:
                    EjecutarAgregado(candidato);
                    break;

                case ResultadoEvaluacion_08YS.Tipo.SugerenciaReemplazo:
                    var confirm = MessageBox.Show(
                        resultado.Mensaje,
                        "Reemplazo sugerido",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);

                    if (confirm == DialogResult.OK)
                    {
                        // Devuelve los reemplazados a disponibles antes de agregar el candidato
                        foreach (var reemplazar in resultado.ComponentesAReemplazar)
                        {
                            _seleccionados.Remove(reemplazar);
                            if (!_disponibles.Any(d => MismoComponente(d, reemplazar)))
                                _disponibles.Add(reemplazar);
                        }
                        EjecutarAgregado(candidato);
                    }
                    break;

                case ResultadoEvaluacion_08YS.Tipo.ConflictoIrresoluble:
                    MessageBox.Show(resultado.Mensaje, "No se puede agregar",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void EjecutarAgregado(AccessComponent_08YS componente)
        {
            _seleccionados.Add(componente);

            var aQuitar = _disponibles.FirstOrDefault(d => MismoComponente(d, componente));
            if (aQuitar != null) _disponibles.Remove(aQuitar);

            LimpiarDetalle();
            RefrescarDGVSeleccionados();
            RefrescarDGVDisponibles();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvSeleccionados.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un componente para eliminar.", "Sin selección",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var componente = (dgvSeleccionados.CurrentRow.DataBoundItem as ComponenteRow)?.Componente;
            if (componente == null)
            {
                MessageBox.Show("Error al obtener el componente seleccionado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _seleccionados.Remove(componente);

            // Devuelve a disponibles si no está ya
            if (!_disponibles.Any(d => MismoComponente(d, componente)))
                _disponibles.Add(componente);

            LimpiarDetalle();
            RefrescarDGVSeleccionados();
            RefrescarDGVDisponibles();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();

                if (_tipo == TipoEntidad.Familia)
                {
                    if (_operacion == OperacionAM.Alta)
                        _familiaBLL.Crear(nombre, _seleccionados);
                    else
                        _familiaBLL.Modificar(_familiaAEditar.FamiliaID, nombre, _seleccionados);
                }
                else
                {
                    if (_operacion == OperacionAM.Alta)
                        _rolBLL.Crear(nombre, _seleccionados);
                    else
                        _rolBLL.Modificar(_rolAEditar.RolID, nombre, _seleccionados);
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

        private void RefrescarDGVSeleccionados()
        {
            _actualizandoSeleccion = true;
            dgvSeleccionados.DataSource = null;
            dgvSeleccionados.DataSource = _seleccionados
                .OrderBy(c => c is Familia_08YS ? 1 : 0)
                .ThenBy(c => c.Nombre)
                .Select(c => new ComponenteRow(c))
                .ToList();
            _actualizandoSeleccion = false;
        }

        private void RefrescarDGVDisponibles()
        {
            _actualizandoSeleccion = true;
            dgvDisponibles.DataSource = null;
            dgvDisponibles.DataSource = _disponibles
                .OrderBy(c => c is Familia_08YS ? 1 : 0)
                .ThenBy(c => c.Nombre)
                .Select(c => new ComponenteRow(c))
                .ToList();
            _actualizandoSeleccion = false;
        }

        private bool MismoComponente(AccessComponent_08YS a, AccessComponent_08YS b)
        {
            if (a is Familia_08YS fa && b is Familia_08YS fb) return fa.FamiliaID == fb.FamiliaID;
            if (a is Permiso_08YS pa && b is Permiso_08YS pb) return pa.PermisoID == pb.PermisoID;
            return false;
        }

        private class ComponenteRow
        {
            public AccessComponent_08YS Componente { get; }
            public string Nombre => Componente.Nombre;
            public string TipoDisplay => Componente is Familia_08YS ? "Familia" : "Permiso";

            public ComponenteRow(AccessComponent_08YS c) => Componente = c;
        }

        private void trvDetalle_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            bool seleccionado = (e.State & TreeNodeStates.Selected) != 0;

            Color colorFondo = seleccionado
                ? Color.FromArgb(5, 5, 100)
                : Color.FromArgb(5, 10, 30);

            Color colorTexto = seleccionado ? Color.Goldenrod : Color.White;

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
    }
}
