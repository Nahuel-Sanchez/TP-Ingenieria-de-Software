using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using System;
using System.Windows.Forms;
using static GUI_08YS.Recepcionista.UiCargos_68SA;

namespace GUI_08YS.Recepcionista
{
    /// <summary>Alta / modificación de un ítem de la carta de servicio a la habitación.</summary>
    public partial class FormEditarItemRoomService_68SA : Form
    {
        #region Campos

        private readonly CatalogoRoomServiceBLL_68SA _catalogoBLL = BLLFactory_08YS.CreateCatalogoRoomServiceBLL();
        private readonly ItemRoomService_68SA _item; // null = alta

        #endregion

        #region Constructor

        public FormEditarItemRoomService_68SA(ItemRoomService_68SA item = null)
        {
            _item = item;
            InitializeComponent();
            Traducir(this);

            txtNombre.PlaceholderText = T("ItemRS_phNombre");
            txtDescripcion.PlaceholderText = T("ItemRS_phDescripcion");
            lblTitulo.Text = Text = T(item == null ? "ItemRS_tituloNuevo" : "ItemRS_tituloEditar");

            if (item != null)
            {
                txtNombre.Text = item.Nombre;
                txtDescripcion.Text = item.Descripcion ?? string.Empty;
                nudPrecio.Value = Math.Min(nudPrecio.Maximum, Math.Max(nudPrecio.Minimum, item.PrecioUnitario));
                chkActivo.Checked = item.Activo;
            }

            btnGuardar.Click += (s, e) => Guardar();
        }

        #endregion

        #region Métodos auxiliares

        private void Guardar()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.RealText))
            {
                Aviso(this, T("ItemRS_msgIngreseNombre"), T("Comun_msgDatosIncompletos"));
                return;
            }

            var item = new ItemRoomService_68SA
            {
                Id = _item?.Id ?? 0,
                Nombre = txtNombre.RealText,
                Descripcion = txtDescripcion.RealText,
                PrecioUnitario = nudPrecio.Value,
                Activo = chkActivo.Checked
            };

            try
            {
                if (_item == null) _catalogoBLL.Crear(item);
                else _catalogoBLL.Modificar(item);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ItemRoomServiceNoEncontradoException_68SA ex)
            {
                Aviso(this, Describir(ex), T("Comun_tituloErrorValidacion"));
                DialogResult = DialogResult.OK; // cierra y refresca el listado
                Close();
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                Aviso(this, Describir(ex), T("Comun_tituloErrorValidacion"));
            }
        }

        #endregion
    }
}
