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
using System.Text;
using System.Windows.Forms;
using static GUI_08YS.Recepcionista.UiCargos_68SA;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Pantalla del personal de servicio (PN2): pedidos de room service pendientes, con el tiempo de espera,
    /// y confirmación de la entrega. Se actualiza sola cada minuto mientras está visible.
    /// </summary>
    public partial class FormPedidosRoomService_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private const string ColEntregar = "colEntregar";
        private const int MinutosDemora = 30; // a partir de acá la espera se marca en rojo

        private readonly RoomServiceBLL_68SA _roomServiceBLL = BLLFactory_08YS.CreateRoomServiceBLL();
        private readonly Timer _timer = new Timer { Interval = 60000 };
        private List<PedidoRoomService_68SA> _pedidos = new List<PedidoRoomService_68SA>();
        private EstadoPedidoRoomService? _filtroEstado = EstadoPedidoRoomService.Pendiente;
        private bool _soloHoy;

        #endregion

        #region Constructor

        public FormPedidosRoomService_68SA()
        {
            InitializeComponent();

            CrudEstilo_68SA.AplicarGrilla(dgvPedidos, 46);
            dgvPedidos.CellClick += DgvPedidos_CellClick;
            dgvPedidos.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) MostrarDetalle(PedidoDeFila(e.RowIndex)); };
            dgvPedidos.CellFormatting += DgvPedidos_CellFormatting;

            foreach (var chip in new[] { rbPendientes, rbEntregados, rbTodos })
                EstiloChip(chip);
            rbPendientes.Checked = true;
            rbPendientes.CheckedChanged += (s, e) => { if (rbPendientes.Checked) CambiarFiltro(EstadoPedidoRoomService.Pendiente, false); };
            rbEntregados.CheckedChanged += (s, e) => { if (rbEntregados.Checked) CambiarFiltro(EstadoPedidoRoomService.Entregado, true); };
            rbTodos.CheckedChanged += (s, e) => { if (rbTodos.Checked) CambiarFiltro(null, false); };

            btnFiltrar.Click += (s, e) => Cargar();
            btnLimpiar.Click += (s, e) => { txtHabitacion.Text = string.Empty; rbPendientes.Checked = true; Cargar(); };
            btnActualizar.Click += (s, e) => Cargar();
            txtHabitacion.InnerTextBox.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Cargar(); } };

            // La pantalla queda en el panel del menú aunque se abra otra: el refresco automático
            // corre solo mientras esté puesta (sin consultas a la base en segundo plano)
            _timer.Tick += (s, e) => Cargar(silencioso: true);
            ParentChanged += (s, e) => { if (Parent == null) _timer.Stop(); else _timer.Start(); };
            Disposed += (s, e) => _timer.Dispose();

            AplicarTextos();
            Shown += (s, e) => Cargar();
        }

        #endregion

        #region Grilla

        private sealed class FilaPedido_68SA
        {
            public int Id { get; set; }
            public string Pedido { get; set; }
            public string Habitacion { get; set; }
            public string Huesped { get; set; }
            public string Detalle { get; set; }
            public DateTime Hora { get; set; }
            public string Espera { get; set; }
            public string Estado { get; set; }
            public decimal Monto { get; set; }
        }

        private void MostrarPedidos()
        {
            DateTime ahora = DateTime.Now;
            int? seleccionado = (dgvPedidos.CurrentRow?.DataBoundItem as FilaPedido_68SA)?.Id;

            dgvPedidos.DataSource = null;
            dgvPedidos.DataSource = _pedidos.Select(p => new FilaPedido_68SA
            {
                Id = p.Id,
                Pedido = "#" + p.Id.ToString("00000"),
                Habitacion = p.NroHabitacion,
                Huesped = p.Huesped,
                Detalle = QuitarPrefijo(p.Descripcion),
                Hora = p.FechaHoraPedido,
                Espera = p.Estado == EstadoPedidoRoomService.Pendiente
                    ? string.Format(T("PedidosRS_txtEsperando"), p.MinutosEspera(ahora))
                    : string.Format(T("PedidosRS_txtEntregadoA"), p.FechaHoraEntrega?.ToString("HH:mm"), p.MinutosEspera(ahora)),
                Estado = T("Enum_EstadoPedidoRoomService_" + p.Estado),
                Monto = p.Monto
            }).ToList();
            AplicarColumnas();

            foreach (DataGridViewRow fila in dgvPedidos.Rows)
                if (fila.DataBoundItem is FilaPedido_68SA f && f.Id == seleccionado)
                {
                    fila.Selected = true;
                    break;
                }

            ActualizarResumen(ahora);
        }

        private void AplicarColumnas()
        {
            var col = dgvPedidos.Columns;
            if (col.Count == 0) return;
            col[nameof(FilaPedido_68SA.Id)].Visible = false;

            void Col(string nombre, string clave, float peso, string formato = null, DataGridViewContentAlignment? alineacion = null)
            {
                col[nombre].HeaderText = T(clave);
                col[nombre].FillWeight = peso;
                if (formato != null) col[nombre].DefaultCellStyle.Format = formato;
                if (alineacion.HasValue) col[nombre].DefaultCellStyle.Alignment = alineacion.Value;
            }
            Col(nameof(FilaPedido_68SA.Pedido), "PedidosRS_colPedido", 8);
            Col(nameof(FilaPedido_68SA.Habitacion), "PedidosRS_colHabitacion", 7, null, DataGridViewContentAlignment.MiddleCenter);
            Col(nameof(FilaPedido_68SA.Huesped), "PedidosRS_colHuesped", 15);
            Col(nameof(FilaPedido_68SA.Detalle), "PedidosRS_colDetalle", 34);
            Col(nameof(FilaPedido_68SA.Hora), "PedidosRS_colHora", 10, "dd/MM HH:mm");
            Col(nameof(FilaPedido_68SA.Espera), "PedidosRS_colEspera", 12);
            Col(nameof(FilaPedido_68SA.Estado), "PedidosRS_colEstado", 8);
            Col(nameof(FilaPedido_68SA.Monto), "PedidosRS_colMonto", 9, "$#,##0.00", DataGridViewContentAlignment.MiddleRight);

            ColumnaIconoAlFinal(dgvPedidos, ColEntregar);
            bool puedeEntregar = SessionManager_08YS.Instance.HasPermission(Permisos.ConfirmarEntregaRoomService);
            col[ColEntregar].Visible = puedeEntregar;
            col[ColEntregar].Width = 46;

            Image icoEntregar = IconCache.Get(IconChar.CheckDouble, IconFont.Auto, 26, ColorExito);
            foreach (DataGridViewRow fila in dgvPedidos.Rows)
            {
                var pedido = PedidoDeFila(fila.Index);
                bool pendiente = pedido?.Estado == EstadoPedidoRoomService.Pendiente;
                fila.Cells[ColEntregar].Value = pendiente ? icoEntregar : null;
                fila.Cells[ColEntregar].ToolTipText = pendiente ? T("PedidosRS_ttEntregar") : string.Empty;
            }
        }

        private void DgvPedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string columna = dgvPedidos.Columns[e.ColumnIndex].Name;
            if (columna != nameof(FilaPedido_68SA.Espera) && columna != nameof(FilaPedido_68SA.Estado)) return;

            var pedido = PedidoDeFila(e.RowIndex);
            if (pedido == null) return;
            if (pedido.Estado == EstadoPedidoRoomService.Entregado)
                e.CellStyle.ForeColor = ColorExito;
            else
                e.CellStyle.ForeColor = pedido.MinutosEspera(DateTime.Now) >= MinutosDemora ? ColorError : ColorAviso;
        }

        private PedidoRoomService_68SA PedidoDeFila(int fila)
        {
            if (fila < 0 || fila >= dgvPedidos.Rows.Count) return null;
            int id = (dgvPedidos.Rows[fila].DataBoundItem as FilaPedido_68SA)?.Id ?? 0;
            return _pedidos.FirstOrDefault(p => p.Id == id);
        }

        private void ActualizarResumen(DateTime ahora)
        {
            var pendientes = _pedidos.Where(p => p.Estado == EstadoPedidoRoomService.Pendiente).ToList();
            string hora = string.Format(T("PedidosRS_txtActualizado"), ahora.ToString("HH:mm"));
            lblResumen.Text = pendientes.Count == 0
                ? $"{T("PedidosRS_txtSinPendientes")}  ·  {hora}"
                : $"{string.Format(T("PedidosRS_txtPendientes"), pendientes.Count, pendientes.Max(p => p.MinutosEspera(ahora)))}  ·  {hora}";
            lblResumen.ForeColor = pendientes.Any(p => p.MinutosEspera(ahora) >= MinutosDemora) ? ColorError : TextoSuave;
        }

        // "Room service: 2 x Agua" -> "2 x Agua" (en esta pantalla ya se sabe que es room service)
        private static string QuitarPrefijo(string descripcion)
        {
            const string prefijo = "Room service: ";
            return descripcion != null && descripcion.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase)
                ? descripcion.Substring(prefijo.Length)
                : descripcion;
        }

        #endregion

        #region Acciones

        private void DgvPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dgvPedidos.Columns[e.ColumnIndex].Name != ColEntregar) return;
            var pedido = PedidoDeFila(e.RowIndex);
            if (pedido?.Estado == EstadoPedidoRoomService.Pendiente)
                ConfirmarEntrega(pedido);
        }

        private void ConfirmarEntrega(PedidoRoomService_68SA pedido)
        {
            if (!UiCargos_68SA.Confirmar(this, string.Format(T("PedidosRS_msgConfirmarEntrega"), pedido.Id.ToString("00000"), pedido.NroHabitacion)))
                return;
            try
            {
                _roomServiceBLL.ConfirmarEntrega(pedido);
            }
            catch (Exception ex) when (EsErrorDeNegocio(ex))
            {
                Aviso(this, Describir(ex));
            }
            Cargar();
        }

        private void MostrarDetalle(PedidoRoomService_68SA pedido)
        {
            if (pedido == null) return;
            var detalle = _roomServiceBLL.GetDetalle(pedido.Id);

            var texto = new StringBuilder();
            texto.AppendLine(string.Format(T("PedidosRS_txtDetalleEncabezado"), pedido.Id.ToString("00000"), pedido.NroHabitacion, pedido.Huesped));
            texto.AppendLine();
            foreach (var renglon in detalle)
                texto.AppendLine($"{renglon.Cantidad} x {renglon.NombreItem}    {Moneda(renglon.Subtotal)}");
            texto.AppendLine();
            texto.AppendLine($"{T("PedidosRS_colMonto")}: {Moneda(pedido.Monto)}");
            if (!string.IsNullOrEmpty(pedido.UsuarioRegistroNombre))
                texto.AppendLine(string.Format(T("PedidosRS_txtCargadoPor"), pedido.UsuarioRegistroNombre, pedido.FechaHoraPedido.ToString("dd/MM/yyyy HH:mm")));

            Info(this, texto.ToString(), T("PedidosRS_tituloDetalle"));
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            AplicarTextos();
            MostrarPedidos();
        }

        private void AplicarTextos()
        {
            Traducir(this);
            txtHabitacion.PlaceholderText = T("PedidosRS_phHabitacion");
        }

        #endregion

        #region Carga de datos

        private void Cargar(bool silencioso = false)
        {
            try
            {
                _pedidos = _roomServiceBLL.GetPedidos(new PedidoRoomServiceFiltro_68SA
                {
                    Estado = _filtroEstado,
                    NroHabitacion = string.IsNullOrWhiteSpace(txtHabitacion.RealText) ? null : txtHabitacion.RealText.Trim(),
                    // "Entregados hoy": por la hora de entrega (un pedido de anoche entregado hoy también cuenta)
                    EntregadosDesde = _soloHoy ? DateTime.Today : (DateTime?)null
                });
            }
            catch (Exception ex) when (silencioso || EsErrorDeNegocio(ex))
            {
                // En el refresco automático un corte momentáneo de la base no interrumpe con mensajes:
                // se reintenta en el próximo minuto. Sin permiso, se deja de refrescar.
                if (ex is UnauthorizedAccessException) _timer.Stop();
                if (silencioso) return;
                Aviso(this, Describir(ex));
                _pedidos = new List<PedidoRoomService_68SA>();
            }
            MostrarPedidos();
        }

        #endregion

        #region Métodos auxiliares

        private void CambiarFiltro(EstadoPedidoRoomService? estado, bool soloHoy)
        {
            _filtroEstado = estado;
            _soloHoy = soloHoy;
            Cargar();
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
