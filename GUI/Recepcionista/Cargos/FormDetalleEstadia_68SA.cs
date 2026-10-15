using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
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
    /// <summary>
    /// Detalle de una estadía (PN2): datos de la reserva y todos los movimientos de su cuenta
    /// (alojamiento, renovación, upgrades, reubicaciones, pedidos de room service y pagos) con los totales.
    /// Se abre a pedido desde el check-out ("Ver detalle de la cuenta") y desde Control de habitaciones.
    /// </summary>
    public partial class FormDetalleEstadia_68SA : Form
    {
        #region Campos

        private readonly int _reservaId;
        private List<FilaMovimiento_68SA> _movimientos = new List<FilaMovimiento_68SA>();

        #endregion

        #region Constructor

        public FormDetalleEstadia_68SA(int reservaId)
        {
            _reservaId = reservaId;
            InitializeComponent();
            LayoutAdaptable_68SA.LimitarAPantalla(this);   // en pantallas chicas se achica y queda entera a la vista
            Traducir(this);

            CrudEstilo_68SA.AplicarGrilla(dgvMovimientos, 40);
            dgvMovimientos.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvMovimientos.CellFormatting += DgvMovimientos_CellFormatting;

            btnFactura.Click += (s, e) => FormDocumentosReserva_68SA.Mostrar(this, _reservaId, 1);

            Shown += (s, e) =>
            {
                try
                {
                    Cargar();
                }
                catch (Exception ex) when (EsErrorDeNegocio(ex))
                {
                    Aviso(this, Describir(ex));
                    Close();
                }
            };
        }

        #endregion

        #region Carga de datos

        private void Cargar()
        {
            var reserva = BLLFactory_08YS.CreateReservaBLL().GetById(_reservaId);
            var cuenta = BLLFactory_08YS.CreateCuentaEstadiaBLL().GetEstadoCuenta(_reservaId);

            lblReserva.Text = "#" + reserva.Id.ToString("000000");
            lblTitular.Text = $"{reserva.Titular.Nombre} {reserva.Titular.Apellido}";
            string tipo = reserva.Habitacion.Tipo?.Nombre;
            lblHabitacion.Text = string.IsNullOrEmpty(tipo) ? reserva.Habitacion.NroHabitacion : $"{reserva.Habitacion.NroHabitacion} · {tipo}";
            lblFechas.Text = $"{reserva.FechaIngreso:dd/MM} → {reserva.FechaEgreso:dd/MM/yyyy} ({reserva.Noches} {T(reserva.Noches == 1 ? "DetalleEst_txtNoche" : "DetalleEst_txtNoches")})";
            lblEstado.Text = OpcionEnum_68SA.Texto(reserva.Estado);

            _movimientos = ArmarMovimientos(reserva, cuenta);
            dgvMovimientos.DataSource = null;
            dgvMovimientos.DataSource = _movimientos;
            AplicarColumnas();

            lblTotAlojamiento.Text = Moneda(cuenta.AlojamientoReservado);
            lblTotRenovacion.Text = Moneda(cuenta.Renovacion);
            lblTotCargos.Text = Moneda(cuenta.TotalCargos);
            lblTotTotal.Text = Moneda(cuenta.Total);
            lblTotPagado.Text = Moneda(cuenta.TotalPagado);
            lblTotSaldo.Text = Moneda(cuenta.Saldo);
            lblTotPagado.ForeColor = ColorExito;
            lblTotSaldo.ForeColor = cuenta.Saldo > 0 ? ColorError : ColorExito;
        }

        #endregion

        #region Eventos de controles

        private void DgvMovimientos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _movimientos.Count) return;
            var fila = _movimientos[e.RowIndex];
            string columna = dgvMovimientos.Columns[e.ColumnIndex].Name;

            if (columna == nameof(FilaMovimiento_68SA.Fecha) && fila.Tipo == TipoMovimiento.Alojamiento && fila.Fecha.HasValue)
            {
                e.Value = fila.Fecha.Value.ToString("dd/MM/yyyy"); // el alojamiento no tiene hora
                e.FormattingApplied = true;
            }
            else if (columna == nameof(FilaMovimiento_68SA.Importe))
            {
                e.Value = fila.Importe.HasValue
                    ? (fila.Importe.Value < 0 ? "−" + Moneda(-fila.Importe.Value) : Moneda(fila.Importe.Value))
                    : T("CambioHab_txtSinCargo");
                e.FormattingApplied = true;
                e.CellStyle.ForeColor = fila.Tipo == TipoMovimiento.Pago ? ColorExito
                                      : fila.Importe.HasValue ? Color.WhiteSmoke : TextoSuave;
            }
            else if (columna == nameof(FilaMovimiento_68SA.Concepto))
            {
                e.CellStyle.ForeColor = fila.Tipo == TipoMovimiento.Pago ? ColorExito
                                      : fila.Tipo == TipoMovimiento.Upgrade || fila.Tipo == TipoMovimiento.Renovacion ? Color.Goldenrod
                                      : Color.WhiteSmoke;
            }
            else if (columna == nameof(FilaMovimiento_68SA.Detalle))
                e.CellStyle.ForeColor = TextoSuave;
        }

        #endregion

        #region Métodos auxiliares

        private List<FilaMovimiento_68SA> ArmarMovimientos(Reserva_68SA reserva, EstadoCuentaReserva_68SA cuenta)
        {
            var filas = new List<FilaMovimiento_68SA>();
            var cambios = BLLFactory_08YS.CreateCambioHabitacionBLL().GetHistorial(_reservaId);
            var roomServiceBLL = BLLFactory_08YS.CreateRoomServiceBLL();
            // Los pedidos (con sus ítems) solo los ve quien trabaja con room service; el resto ve el cargo resumido
            var sesion = SessionManager_08YS.Instance;
            bool veePedidos = sesion.HasPermission(Permisos.RegistrarRoomService) || sesion.HasPermission(Permisos.ConfirmarEntregaRoomService);
            var pedidos = veePedidos ? roomServiceBLL.GetPorReserva(_reservaId) : new List<PedidoRoomService_68SA>();
            var pagos = BLLFactory_08YS.CreatePagoBLL().GetByReserva(_reservaId);

            // Alojamiento tal como se reservó (en la primera habitación si después hubo cambios)
            decimal tarifa = reserva.TarifaNoche;
            int nochesOriginales = tarifa > 0 ? (int)Math.Round(cuenta.AlojamientoReservado / tarifa) : reserva.Noches;
            string habitacionOriginal = cambios.FirstOrDefault()?.NroHabitacionAnterior ?? reserva.Habitacion.NroHabitacion;
            filas.Add(new FilaMovimiento_68SA
            {
                Fecha = reserva.FechaIngreso,
                Concepto = T("DetalleEst_movAlojamiento"),
                Detalle = string.Format(T("DetalleEst_detAlojamiento"), habitacionOriginal, nochesOriginales, Moneda(tarifa)),
                Importe = cuenta.AlojamientoReservado,
                Tipo = TipoMovimiento.Alojamiento,
                Orden = 0
            });

            if (cuenta.Renovacion > 0)
            {
                int nochesExtra = Math.Max(1, reserva.Noches - nochesOriginales);
                filas.Add(new FilaMovimiento_68SA
                {
                    Fecha = reserva.FechaIngreso.AddDays(nochesOriginales),
                    Concepto = T("DetalleEst_movRenovacion"),
                    Detalle = string.Format(T("DetalleEst_detRenovacion"), nochesExtra, reserva.FechaEgreso.ToString("dd/MM/yyyy")),
                    Importe = cuenta.Renovacion,
                    Tipo = TipoMovimiento.Renovacion,
                    Orden = 1
                });
            }

            foreach (var cambio in cambios)
            {
                bool esUpgrade = cambio.Motivo == MotivoCambioHabitacion.Upgrade;
                filas.Add(new FilaMovimiento_68SA
                {
                    Fecha = cambio.FechaCambio,
                    Concepto = T(esUpgrade ? "DetalleEst_movUpgrade" : "DetalleEst_movReubicacion"),
                    Detalle = esUpgrade
                        ? string.Format(T("DetalleEst_detUpgrade"), cambio.NroHabitacionAnterior, cambio.NroHabitacionNueva,
                            cambio.NochesRestantes, Moneda(cambio.TarifaNueva - cambio.TarifaAnterior))
                        : string.Format(T("DetalleEst_detReubicacion"), cambio.NroHabitacionAnterior, cambio.NroHabitacionNueva, cambio.Observacion),
                    Importe = esUpgrade ? cambio.Monto : (decimal?)null,
                    Tipo = esUpgrade ? TipoMovimiento.Upgrade : TipoMovimiento.Reubicacion,
                    Orden = 2
                });
            }

            foreach (var pedido in pedidos)
            {
                var detalle = roomServiceBLL.GetDetalle(pedido.Id);
                string items = string.Join(", ", detalle.Select(d => $"{d.Cantidad} × {d.NombreItem}"));
                if (pedido.Estado == EstadoPedidoRoomService.Pendiente)
                    items += "  ·  " + T("DetalleEst_txtSinEntregar");
                filas.Add(new FilaMovimiento_68SA
                {
                    Fecha = pedido.FechaHoraPedido,
                    Concepto = string.Format(T("DetalleEst_movRoomService"), pedido.Id.ToString("00000")),
                    Detalle = items,
                    Importe = pedido.Monto,
                    Tipo = TipoMovimiento.RoomService,
                    Orden = 2
                });
            }

            if (!veePedidos)
            {
                foreach (var cargo in cuenta.Cargos.Where(c => c.Tipo == TipoCargo.RoomService))
                {
                    filas.Add(new FilaMovimiento_68SA
                    {
                        Fecha = cargo.FechaHora,
                        Concepto = T("DetalleEst_movRoomServiceCargo"),
                        Detalle = cargo.Descripcion,
                        Importe = cargo.Monto,
                        Tipo = TipoMovimiento.RoomService,
                        Orden = 2
                    });
                }
            }

            foreach (var pago in pagos)
            {
                filas.Add(new FilaMovimiento_68SA
                {
                    Fecha = pago.FechaPago,
                    Concepto = string.Format(T("DetalleEst_movPago"), OpcionEnum_68SA.Texto(pago.MetodoPago)),
                    Detalle = T("DetalleEst_detPago"),
                    Importe = -pago.Monto,
                    Tipo = TipoMovimiento.Pago,
                    Orden = 3
                });
            }

            // Alojamiento y renovación arriba; el resto en el orden en que ocurrió
            return filas.OrderBy(f => f.Orden <= 1 ? f.Orden : 2).ThenBy(f => f.Fecha ?? DateTime.MaxValue).ToList();
        }

        private void AplicarColumnas()
        {
            var col = dgvMovimientos.Columns;
            if (col.Count == 0) return;
            col[nameof(FilaMovimiento_68SA.Fecha)].HeaderText = T("DetalleEst_colFecha");
            col[nameof(FilaMovimiento_68SA.Fecha)].FillWeight = 14;
            col[nameof(FilaMovimiento_68SA.Fecha)].DefaultCellStyle.Format = "dd/MM HH:mm";
            col[nameof(FilaMovimiento_68SA.Concepto)].HeaderText = T("DetalleEst_colConcepto");
            col[nameof(FilaMovimiento_68SA.Concepto)].FillWeight = 24;
            col[nameof(FilaMovimiento_68SA.Detalle)].HeaderText = T("DetalleEst_colDetalle");
            col[nameof(FilaMovimiento_68SA.Detalle)].FillWeight = 46;
            col[nameof(FilaMovimiento_68SA.Importe)].HeaderText = T("DetalleEst_colImporte");
            col[nameof(FilaMovimiento_68SA.Importe)].FillWeight = 16;
            col[nameof(FilaMovimiento_68SA.Importe)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

        #region Tipos anidados

        private enum TipoMovimiento { Alojamiento, Renovacion, Upgrade, Reubicacion, RoomService, Pago }

        private sealed class FilaMovimiento_68SA
        {
            public DateTime? Fecha { get; set; }
            public string Concepto { get; set; }
            public string Detalle { get; set; }
            public decimal? Importe { get; set; }
            internal TipoMovimiento Tipo;
            internal int Orden;
        }

        #endregion
    }
}
