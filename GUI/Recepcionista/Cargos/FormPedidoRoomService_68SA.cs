using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static GUI_08YS.Recepcionista.UiCargos_68SA;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Carga de un pedido de servicio a la habitación a la cuenta de una estadía en curso (PN2).
    /// A la izquierda la carta (solo ítems activos), a la derecha el pedido con cantidades y total.
    /// </summary>
    public partial class FormPedidoRoomService_68SA : Form
    {
        #region Campos

        private const string ColAgregar = "colAgregar";
        private const string ColMenos = "colMenos";
        private const string ColMas = "colMas";
        private const string ColQuitar = "colQuitar";

        private readonly RoomServiceBLL_68SA _roomServiceBLL = BLLFactory_08YS.CreateRoomServiceBLL();
        private readonly Reserva_68SA _reserva;

        private List<ItemRoomService_68SA> _carta = new List<ItemRoomService_68SA>();
        // ItemId -> cantidad (conserva el orden en que se fueron agregando)
        private readonly List<KeyValuePair<int, int>> _pedido = new List<KeyValuePair<int, int>>();

        #endregion

        #region Propiedades

        /// <summary>Número de pedido registrado (0 si se canceló).</summary>
        public int PedidoId { get; private set; }

        #endregion

        #region Constructor

        public FormPedidoRoomService_68SA(Reserva_68SA reserva)
        {
            _reserva = reserva ?? throw new ArgumentNullException(nameof(reserva));

            InitializeComponent();
            LayoutAdaptable_68SA.LimitarAPantalla(this);   // en pantallas chicas se achica y queda entera a la vista
            Traducir(this);
            txtBuscar.PlaceholderText = T("PedidoRS_phBuscar");

            CrudEstilo_68SA.AplicarGrilla(dgvCarta, 40);
            CrudEstilo_68SA.AplicarGrilla(dgvPedido, 40);
            dgvCarta.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvPedido.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvCarta.CellClick += DgvCarta_CellClick;
            dgvCarta.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Agregar(IdFila(dgvCarta, e.RowIndex)); };
            dgvPedido.CellClick += DgvPedido_CellClick;
            txtBuscar.InnerTextBox.TextChanged += (s, e) => MostrarCarta();
            btnConfirmar.Click += (s, e) => Registrar();

            CargarEstadia();
            // Las grillas se llenan con el formulario ya creado: si no, WinForms descarta los íconos de las celdas
            Shown += (s, e) =>
            {
                CargarCarta();
                MostrarPedido();
            };
        }

        #endregion

        #region Carta

        private sealed class FilaCarta_68SA
        {
            public int Id { get; set; }
            public string Item { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
        }

        private void CargarCarta()
        {
            _carta = _roomServiceBLL.GetCarta();
            MostrarCarta();
        }

        private void MostrarCarta()
        {
            string buscar = txtBuscar.RealText?.Trim() ?? string.Empty;
            var filas = _carta
                .Where(i => buscar.Length == 0
                            || i.Nombre.IndexOf(buscar, StringComparison.CurrentCultureIgnoreCase) >= 0
                            || (i.Descripcion ?? string.Empty).IndexOf(buscar, StringComparison.CurrentCultureIgnoreCase) >= 0)
                .Select(i => new FilaCarta_68SA { Id = i.Id, Item = i.Nombre, Descripcion = i.Descripcion, Precio = i.PrecioUnitario })
                .ToList();

            dgvCarta.DataSource = null;
            dgvCarta.DataSource = filas;

            var col = dgvCarta.Columns;
            col[nameof(FilaCarta_68SA.Id)].Visible = false;
            col[nameof(FilaCarta_68SA.Item)].HeaderText = T("PedidoRS_colItem");
            col[nameof(FilaCarta_68SA.Item)].FillWeight = 34;
            col[nameof(FilaCarta_68SA.Descripcion)].HeaderText = T("PedidoRS_colDescripcion");
            col[nameof(FilaCarta_68SA.Descripcion)].FillWeight = 46;
            col[nameof(FilaCarta_68SA.Precio)].HeaderText = T("PedidoRS_colPrecio");
            col[nameof(FilaCarta_68SA.Precio)].FillWeight = 20;
            col[nameof(FilaCarta_68SA.Precio)].DefaultCellStyle.Format = "$#,##0.00";
            col[nameof(FilaCarta_68SA.Precio)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ColumnaIconoAlFinal(dgvCarta, ColAgregar);
            Image icoAgregar = IconCache.Get(IconChar.CirclePlus, IconFont.Auto, 26, Color.Goldenrod);
            foreach (DataGridViewRow fila in dgvCarta.Rows)
            {
                fila.Cells[ColAgregar].Value = icoAgregar;
                fila.Cells[ColAgregar].ToolTipText = T("PedidoRS_ttAgregar");
            }
        }

        private void DgvCarta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvCarta.Columns[e.ColumnIndex].Name == ColAgregar)
                Agregar(IdFila(dgvCarta, e.RowIndex));
        }

        private static int IdFila(DataGridView dgv, int fila)
        {
            object item = dgv.Rows[fila].DataBoundItem;
            if (item is FilaCarta_68SA carta) return carta.Id;
            if (item is FilaPedido_68SA pedido) return pedido.Id;
            return 0;
        }

        #endregion

        #region Pedido

        private sealed class FilaPedido_68SA
        {
            public int Id { get; set; }
            public string Item { get; set; }
            public int Cantidad { get; set; }
            public decimal Subtotal { get; set; }
        }

        private int Cantidad(int itemId) => _pedido.Where(r => r.Key == itemId).Select(r => r.Value).FirstOrDefault();

        private void FijarCantidad(int itemId, int cantidad)
        {
            int indice = _pedido.FindIndex(r => r.Key == itemId);
            if (cantidad <= 0)
            {
                if (indice >= 0) _pedido.RemoveAt(indice);
            }
            else if (indice >= 0)
                _pedido[indice] = new KeyValuePair<int, int>(itemId, cantidad);
            else
                _pedido.Add(new KeyValuePair<int, int>(itemId, cantidad));
            MostrarPedido(itemId);
        }

        private void Agregar(int itemId)
        {
            if (itemId <= 0) return;
            int actual = Cantidad(itemId);
            if (actual >= RoomServiceBLL_68SA.MaximoPorItem)
            {
                Aviso(this, string.Format(T("PedidoRS_msgMaximo"), RoomServiceBLL_68SA.MaximoPorItem));
                return;
            }
            FijarCantidad(itemId, actual + 1);
        }

        private void DgvPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            int itemId = IdFila(dgvPedido, e.RowIndex);
            switch (dgvPedido.Columns[e.ColumnIndex].Name)
            {
                case ColMas: Agregar(itemId); break;
                case ColMenos: FijarCantidad(itemId, Cantidad(itemId) - 1); break;
                case ColQuitar: FijarCantidad(itemId, 0); break;
            }
        }

        private void MostrarPedido(int? itemSeleccionado = null)
        {
            var carta = _carta.ToDictionary(i => i.Id);
            var renglones = _pedido
                .Where(r => carta.ContainsKey(r.Key))
                .Select(r => new DetallePedidoRoomService_68SA
                {
                    ItemId = r.Key,
                    NombreItem = carta[r.Key].Nombre,
                    Cantidad = r.Value,
                    PrecioUnitario = carta[r.Key].PrecioUnitario
                }).ToList();

            dgvPedido.DataSource = null;
            dgvPedido.DataSource = renglones
                .Select(r => new FilaPedido_68SA { Id = r.ItemId, Item = r.NombreItem, Cantidad = r.Cantidad, Subtotal = r.Subtotal })
                .ToList();

            var col = dgvPedido.Columns;
            col[nameof(FilaPedido_68SA.Id)].Visible = false;
            col[nameof(FilaPedido_68SA.Item)].HeaderText = T("PedidoRS_colItem");
            col[nameof(FilaPedido_68SA.Item)].FillWeight = 50;
            col[nameof(FilaPedido_68SA.Cantidad)].HeaderText = T("PedidoRS_colCantidad");
            col[nameof(FilaPedido_68SA.Cantidad)].FillWeight = 16;
            col[nameof(FilaPedido_68SA.Cantidad)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col[nameof(FilaPedido_68SA.Subtotal)].HeaderText = T("PedidoRS_colSubtotal");
            col[nameof(FilaPedido_68SA.Subtotal)].FillWeight = 34;
            col[nameof(FilaPedido_68SA.Subtotal)].DefaultCellStyle.Format = "$#,##0.00";
            col[nameof(FilaPedido_68SA.Subtotal)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (string nombre in new[] { ColMenos, ColMas, ColQuitar })
                ColumnaIconoAlFinal(dgvPedido, nombre);
            col[ColMenos].Width = col[ColMas].Width = col[ColQuitar].Width = 36;

            Image icoMenos = IconCache.Get(IconChar.CircleMinus, IconFont.Auto, 22, Color.Goldenrod);
            Image icoMas = IconCache.Get(IconChar.CirclePlus, IconFont.Auto, 22, Color.Goldenrod);
            Image icoQuitar = IconCache.Get(IconChar.Trash, IconFont.Auto, 20, ColorError);
            foreach (DataGridViewRow fila in dgvPedido.Rows)
            {
                fila.Cells[ColMenos].Value = icoMenos;
                fila.Cells[ColMas].Value = icoMas;
                fila.Cells[ColQuitar].Value = icoQuitar;
                fila.Cells[ColMenos].ToolTipText = T("PedidoRS_ttMenos");
                fila.Cells[ColMas].ToolTipText = T("PedidoRS_ttMas");
                fila.Cells[ColQuitar].ToolTipText = T("PedidoRS_ttQuitar");
                if (itemSeleccionado.HasValue && IdFila(dgvPedido, fila.Index) == itemSeleccionado.Value)
                    fila.Selected = true;
            }

            decimal total = RoomServiceBLL_68SA.CalcularTotal(renglones);
            lblTotal.Text = Moneda(total);
            lblEtiquetaPedido.Text = renglones.Count == 0
                ? T("PedidoRS_lblPedidoVacio")
                : string.Format(T("PedidoRS_lblPedidoUnidades"), renglones.Sum(r => r.Cantidad));
            btnConfirmar.Enabled = renglones.Count > 0;
        }

        #endregion

        #region Carga de datos

        private void CargarEstadia()
        {
            lblHabitacion.Text = _reserva.Habitacion.NroHabitacion;
            lblTitular.Text = $"{_reserva.Titular.Nombre} {_reserva.Titular.Apellido}";
            lblEgreso.Text = _reserva.FechaEgreso.ToString("dd/MM/yyyy");
            lblCargosCuenta.Text = Moneda(BLLFactory_08YS.CreateCuentaEstadiaBLL().GetTotalCargos(_reserva.Id));
        }

        #endregion

        #region Métodos auxiliares

        private void Registrar()
        {
            if (_pedido.Count == 0) return;

            var solicitud = new SolicitudPedidoRoomService_68SA { ReservaId = _reserva.Id };
            foreach (var renglon in _pedido)
                solicitud.CantidadesPorItem[renglon.Key] = renglon.Value;

            if (!UiCargos_68SA.Confirmar(this, string.Format(T("PedidoRS_msgConfirmar"), lblTotal.Text, _reserva.Habitacion.NroHabitacion)))
                return;

            try
            {
                PedidoId = _roomServiceBLL.RegistrarPedido(solicitud);
                Info(this, string.Format(T("PedidoRS_msgRegistrado"), PedidoId, _reserva.Habitacion.NroHabitacion),
                     T("PedidoRS_tituloRegistrado"));
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                Aviso(this, Describir(ex), T("PedidoRS_tituloNoSePudo"));
                // Si un ítem se dio de baja mientras tanto, se recarga la carta y se descarta del pedido
                if (ex is PedidoRoomServiceInvalidoException_68SA)
                {
                    CargarCarta();
                    _pedido.RemoveAll(r => _carta.All(i => i.Id != r.Key));
                    MostrarPedido();
                }
            }
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
