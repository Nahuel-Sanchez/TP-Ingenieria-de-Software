using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static GUI_08YS.Recepcionista.UiCargos_68SA;

namespace GUI_08YS.Recepcionista
{
    /// <summary>ABM de la carta de servicio a la habitación (PN2). La "baja" es lógica: el ítem deja de ofrecerse.</summary>
    public partial class FormCartaRoomService_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private const string ColEditar = "colEditar";
        private const string ColActivo = "colActivo";

        private static readonly Dictionary<string, Permisos> _mapaPermisos = new Dictionary<string, Permisos>
        {
            { nameof(btnNuevo), Permisos.GestionarItemsRoomService },
        };

        private readonly CatalogoRoomServiceBLL_68SA _catalogoBLL = BLLFactory_08YS.CreateCatalogoRoomServiceBLL();
        private List<ItemRoomService_68SA> _items = new List<ItemRoomService_68SA>();

        #endregion

        #region Constructor

        public FormCartaRoomService_68SA()
        {
            InitializeComponent();
            PermissionFilter_08YS.Aplicar(this, _mapaPermisos);

            CrudEstilo_68SA.AplicarGrilla(dgvItems);
            dgvItems.CellClick += DgvItems_CellClick;
            dgvItems.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && SessionManager_08YS.Instance.HasPermission(Permisos.GestionarItemsRoomService))
                    AbrirEdicion(ItemDeFila(e.RowIndex));
            };
            dgvItems.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || dgvItems.Columns[e.ColumnIndex].Name != nameof(FilaItem_68SA.Estado)) return;
                var item = ItemDeFila(e.RowIndex);
                if (item != null) e.CellStyle.ForeColor = item.Activo ? ColorExito : TextoSuave;
            };

            btnNuevo.Click += (s, e) => AbrirEdicion(null);
            btnFiltrar.Click += (s, e) => Cargar();
            chkInactivos.CheckedChanged += (s, e) => Cargar();
            btnLimpiar.Click += (s, e) =>
            {
                txtNombreFiltro.Text = string.Empty;
                chkInactivos.Checked = false;
                Cargar();
            };
            txtNombreFiltro.InnerTextBox.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Cargar(); } };

            AplicarTextos();
            Shown += (s, e) => Cargar();
        }

        #endregion

        #region Carga

        private sealed class FilaItem_68SA
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public string Estado { get; set; }
        }

        private void Cargar()
        {
            _items = _catalogoBLL.GetItems(incluirInactivos: chkInactivos.Checked);
            Mostrar();
        }

        private void Mostrar()
        {
            string buscar = txtNombreFiltro.RealText?.Trim() ?? string.Empty;
            dgvItems.DataSource = null;
            dgvItems.DataSource = _items
                .Where(i => buscar.Length == 0
                            || i.Nombre.IndexOf(buscar, StringComparison.CurrentCultureIgnoreCase) >= 0
                            || (i.Descripcion ?? string.Empty).IndexOf(buscar, StringComparison.CurrentCultureIgnoreCase) >= 0)
                .Select(i => new FilaItem_68SA
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Descripcion = i.Descripcion,
                    Precio = i.PrecioUnitario,
                    Estado = T(i.Activo ? "CartaRS_txtActivo" : "CartaRS_txtInactivo")
                }).ToList();
            AplicarColumnas();
        }

        private void AplicarColumnas()
        {
            var col = dgvItems.Columns;
            if (col.Count == 0) return;
            col[nameof(FilaItem_68SA.Id)].Visible = false;
            col[nameof(FilaItem_68SA.Nombre)].HeaderText = T("CartaRS_colNombre");
            col[nameof(FilaItem_68SA.Nombre)].FillWeight = 26;
            col[nameof(FilaItem_68SA.Descripcion)].HeaderText = T("CartaRS_colDescripcion");
            col[nameof(FilaItem_68SA.Descripcion)].FillWeight = 50;
            col[nameof(FilaItem_68SA.Precio)].HeaderText = T("CartaRS_colPrecio");
            col[nameof(FilaItem_68SA.Precio)].FillWeight = 12;
            col[nameof(FilaItem_68SA.Precio)].DefaultCellStyle.Format = "$#,##0.00";
            col[nameof(FilaItem_68SA.Precio)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col[nameof(FilaItem_68SA.Estado)].HeaderText = T("CartaRS_colEstado");
            col[nameof(FilaItem_68SA.Estado)].FillWeight = 12;

            foreach (string nombre in new[] { ColEditar, ColActivo })
                ColumnaIconoAlFinal(dgvItems, nombre);
            bool puedeGestionar = SessionManager_08YS.Instance.HasPermission(Permisos.GestionarItemsRoomService);
            col[ColEditar].Visible = col[ColActivo].Visible = puedeGestionar;

            Image icoEditar = IconCache.Get(IconChar.PenToSquare, IconFont.Auto, 32, Color.Goldenrod);
            Image icoActivo = IconCache.Get(IconChar.ToggleOn, IconFont.Auto, 32, ColorExito);
            Image icoInactivo = IconCache.Get(IconChar.ToggleOff, IconFont.Auto, 32, TextoSuave);
            foreach (DataGridViewRow fila in dgvItems.Rows)
            {
                var item = ItemDeFila(fila.Index);
                if (item == null) continue;
                fila.Cells[ColEditar].Value = icoEditar;
                fila.Cells[ColActivo].Value = item.Activo ? icoActivo : icoInactivo;
                fila.Cells[ColEditar].ToolTipText = T("CartaRS_ttEditar");
                fila.Cells[ColActivo].ToolTipText = T(item.Activo ? "CartaRS_ttDesactivar" : "CartaRS_ttActivar");
            }
        }

        private ItemRoomService_68SA ItemDeFila(int fila)
        {
            if (fila < 0 || fila >= dgvItems.Rows.Count) return null;
            int id = (dgvItems.Rows[fila].DataBoundItem as FilaItem_68SA)?.Id ?? 0;
            return _items.FirstOrDefault(i => i.Id == id);
        }

        #endregion

        #region Acciones

        private void DgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var item = ItemDeFila(e.RowIndex);
            if (item == null) return;

            string columna = dgvItems.Columns[e.ColumnIndex].Name;
            if (columna == ColEditar) AbrirEdicion(item);
            else if (columna == ColActivo) CambiarActivo(item);
        }

        private void AbrirEdicion(ItemRoomService_68SA item)
        {
            using (var popup = new FormEditarItemRoomService_68SA(item))
            {
                if (popup.ShowDialog(this) == DialogResult.OK)
                    Cargar();
            }
        }

        private void CambiarActivo(ItemRoomService_68SA item)
        {
            string clave = item.Activo ? "CartaRS_msgConfirmarDesactivar" : "CartaRS_msgConfirmarActivar";
            if (!UiCargos_68SA.Confirmar(this, string.Format(T(clave), item.Nombre))) return;

            try
            {
                _catalogoBLL.CambiarActivo(item.Id, !item.Activo);
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                Aviso(this, Describir(ex));
            }
            Cargar();
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            AplicarTextos();
            Mostrar();
        }

        private void AplicarTextos()
        {
            Traducir(this);
            txtNombreFiltro.PlaceholderText = T("CartaRS_phNombre");
        }

        #endregion

        #region Sobrescrituras

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion
    }
}
