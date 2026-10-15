using BE_08YS;
using BE_08YS.Metricas;
using BLL_08YS;
using BLL_08YS.Negocio;
using GUI_08YS.Recepcionista.Dashboard;
using GUI_08YS.UserControls;
using Service_08YS;
using CustomControls;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Service_08YS.Entities.Acceso;

namespace GUI_08YS.Recepcionista
{
    public partial class FormDashboard_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos y constructor

        // Ancho máximo de una fila de leyenda de los donuts: más ancha solo separa texto y valor
        private const int AnchoMaximoFilaLeyenda = 420;
        private const int AltoFilaLeyenda = 30;
        private const int SeparacionFilaLeyenda = 4;

        // Alto mínimo de la tarjeta que completa la pantalla en cada pestaña: por debajo aparece la barra vertical
        private const int AltoMinimoTarjetaDonut = 250;

        private readonly MetricasBLL_68SA _metricasBLL = BLLFactory_08YS.CreateMetricasBLL();
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private bool _historicoTotal = false;
        private bool _huespedesInicializado = false;
        private bool _historicoTotalHuespedes = false;
        private IconButton _btnGenerarReporte;
        private IconDateTimePicker _dtpDesdeHuespedes;
        private IconDateTimePicker _dtpHastaHuespedes;
        private IconButton _btnHistoricoHuespedes;
        private Label _lblTituloFiltroHuespedes;
        private Label _lblValorNuevos;
        private Label _lblValorRecurrentes;
        private Label _lblValorTasaRecurrencia;
        private Label _lblValorGastoPromedio;
        private UC_DonutChart_68SA _donutNacionalidad;
        private FlowLayoutPanel _leyendaNacionalidad;
        private Control _rellenoOcupacion;
        private Control _rellenoHuespedes;

        // General: ingresos por día junto al donut del origen del gasto
        private TableLayoutPanel _filaGeneral;
        private UC_BarChart_68SA _graficoIngresos;
        private Label _lblTituloIngresosDia;

        // Pestaña Servicios (PN2): se arma por código como la de Huéspedes
        private IconButton _btnTabServicios;
        private FlowLayoutPanel _flpServicios;
        private bool _serviciosInicializado;
        private bool _historicoServicios;
        private Control _rellenoServicios;
        private IconDateTimePicker _dtpDesdeServicios;
        private IconDateTimePicker _dtpHastaServicios;
        private IconButton _btnHistoricoServicios;
        private Label _lblTituloFiltroServicios;
        private readonly Dictionary<string, (Label Valor, Label Detalle)> _kpisServicios = new Dictionary<string, (Label Valor, Label Detalle)>();
        private UC_BarChart_68SA _graficoServiciosDia;
        private UC_BarChart_68SA _graficoTopProductos;
        private UC_BarChart_68SA _graficoHoras;
        private Label _lblTituloServiciosDia;

        // General: indicadores hoteleros del período, cobros por medio de pago y llegadas por día de la semana
        private TableLayoutPanel _filaIndicadores;
        private TableLayoutPanel _filaGeneral2;
        private readonly Dictionary<string, (Label Valor, Label Detalle)> _kpisGeneral = new Dictionary<string, (Label Valor, Label Detalle)>();
        private UC_DonutChart_68SA _donutMediosPago;
        private FlowLayoutPanel _leyendaMediosPago;
        private UC_BarChart_68SA _graficoDiaSemana;

        // Huéspedes: perfil de las reservas, ranking de titulares, edades y composición de los grupos
        private readonly Dictionary<string, (Label Valor, Label Detalle)> _kpisHuespedes = new Dictionary<string, (Label Valor, Label Detalle)>();
        private UC_BarChart_68SA _graficoTopHuespedes;
        private UC_BarChart_68SA _graficoEdades;
        private UC_DonutChart_68SA _donutComposicion;
        private FlowLayoutPanel _leyendaComposicion;

        // Servicios: upgrades por tipo, tiempos de entrega, cola de pedidos y últimos cambios de habitación
        private UC_BarChart_68SA _graficoUpgradesTipo;
        private UC_BarChart_68SA _graficoTiemposEntrega;
        private FlowLayoutPanel _listaPendientes;
        private FlowLayoutPanel _listaCambios;
        private Label _lblTituloPendientes;

        private static readonly Color ColorAzul = Color.FromArgb(90, 155, 235);
        private static readonly Color ColorNaranja = Color.FromArgb(235, 140, 60);
        private static readonly Color ColorRojo = Color.FromArgb(235, 90, 90);
        private static readonly Color ColorTextoSecundario = Color.FromArgb(150, 156, 175);
        private static readonly Font FuenteFilaLista = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        private static readonly Font FuenteDetalleLista = new Font("Segoe UI", 8.25F);
        private static readonly Font FuenteValorLista = new Font("Segoe UI", 9.75F, FontStyle.Bold);

        private static readonly Color ColorRoomService = Color.FromArgb(76, 217, 100);
        private static readonly Color ColorUpgrade = Color.FromArgb(160, 125, 235);

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        public FormDashboard_68SA()
        {
            InitializeComponent();

            // Tarjetas de indicadores: el valor nunca queda debajo del ícono (se achica o el ícono se oculta)
            LayoutAdaptable_68SA.AjustarValorTarjeta(pnlCardDisponibles, lblValorDisponibles, iconDisponibles);
            LayoutAdaptable_68SA.AjustarValorTarjeta(pnlCardOcupadas, lblValorOcupadasDash, iconOcupadasDash);
            LayoutAdaptable_68SA.AjustarValorTarjeta(pnlCardLimpieza, lblValorLimpieza, iconLimpieza);
            LayoutAdaptable_68SA.AjustarValorTarjeta(pnlCardMantenimiento, lblValorMantenimiento, iconMantenimiento);
            LayoutAdaptable_68SA.AjustarValorTarjeta(pnlCardTasaOcupacion, lblValorTasaOcupacion, iconTasaOcupacion);
            LayoutAdaptable_68SA.AjustarValorTarjeta(pnlCardIngresos, lblValorIngresos, iconIngresos);
            LayoutAdaptable_68SA.AjustarValorTarjeta(pnlCardTicketPromedio, lblValorTicketPromedio, iconTicketPromedio);
            LayoutAdaptable_68SA.AjustarValorTarjeta(pnlCardAnuladas, lblValorAnuladas, iconAnuladas);
            LayoutAdaptable_68SA.AjustarValorTarjeta(pnlCardMontoAnulado, lblValorMontoAnulado, iconMontoAnulado);

            dtpDesdeGeneral.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpHastaGeneral.Value = DateTime.Today;

            btnTabGeneral.Click += (s, e) => CambiarTab(0);
            btnTabOcupacion.Click += (s, e) => CambiarTab(1);
            btnTabHuespedes.Click += (s, e) => CambiarTab(2);
            CrearPestanaServicios();

            btnFiltroHoy.Click += (s, e) => AplicarPreset(DateTime.Today, DateTime.Today);
            btnFiltro7Dias.Click += (s, e) => AplicarPreset(DateTime.Today.AddDays(-7), DateTime.Today);
            btnFiltro30Dias.Click += (s, e) => AplicarPreset(DateTime.Today.AddDays(-30), DateTime.Today);
            btnFiltroEsteMes.Click += (s, e) => AplicarPreset(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);
            btnFiltroHistorico.Click += (s, e) => AplicarHistoricoTotal();
            btnActualizarGeneral.Click += (s, e) => { _historicoTotal = false; ActualizarEstiloHistorico(); CargarTarjetasIngresosGeneral(); };

            _btnGenerarReporte = new IconButton
            {
                BackColor = Color.FromArgb(90, 155, 235),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(10, 15, 35),
                IconChar = IconChar.FilePdf,
                IconColor = Color.FromArgb(10, 15, 35),
                IconSize = 35,
                ImageAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                Size = new Size(260, 44),
                Text = T("Dashboard_btnGenerarReporte"),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            _btnGenerarReporte.Click += (s, e) => GenerarReporteReservas();
            _btnGenerarReporte.Visible = SessionManager_08YS.Instance.HasPermission(Permisos.GenerarReporteReservas);

            // Layout adaptable: las tarjetas ocupan el ancho disponible (sin scroll horizontal),
            // los filtros pasan a otra fila cuando no entran en una sola y la última tarjeta de cada
            // pestaña (la del donut) toma el alto que sobra, así la pantalla se llena sin barra vertical.
            ArmarBarraFiltro(pnlFiltroFechasGeneral, lblTituloFiltroGeneral,
                new Control[] { btnFiltroHoy, btnFiltro7Dias, btnFiltro30Dias, btnFiltroEsteMes, btnFiltroHistorico },
                new Control[] { lblEtiquetaDesdeGeneral, dtpDesdeGeneral, lblEtiquetaHastaGeneral, dtpHastaGeneral, btnActualizarGeneral, _btnGenerarReporte });

            flpHuespedes.AutoScroll = true;
            ArmarFilaGeneral();
            ArmarExtrasGeneral();
            LayoutAdaptable_68SA.EstirarHijosAlAncho(flpGeneral, 0, () => _filaGeneral2, AltoMinimoTarjetaDonut);
            LayoutAdaptable_68SA.EstirarHijosAlAncho(flpOcupacion, 0, () => _rellenoOcupacion, AltoMinimoTarjetaDonut);
            LayoutAdaptable_68SA.EstirarHijosAlAncho(flpHuespedes, 0, () => _rellenoHuespedes, AltoMinimoTarjetaDonut);

            ConfigurarTarjetaDonut(pnlOrigenGasto, ucDonutOrigenGasto, flpLeyendaOrigenGasto);

            this.Shown += (s, e) => CargarGeneral();
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            TraducirControles(this);
            _btnGenerarReporte.Text = T("Dashboard_btnGenerarReporte");
            ActualizarEstiloHistorico();

            // La pestaña Huéspedes se arma por código: se vuelve a crear con los textos del idioma nuevo
            if (_huespedesInicializado)
            {
                _rellenoHuespedes = null;
                _kpisHuespedes.Clear();
                LimpiarContenedor(flpHuespedes);
                _huespedesInicializado = false;
            }
            _btnTabServicios.Text = T("Dashboard_btnTabServicios");
            _lblTituloIngresosDia.Text = T("Dashboard_tituloIngresosPorDia");
            ArmarExtrasGeneral();   // indicadores y gráficos agregados por código, con los textos nuevos
            if (_serviciosInicializado)
            {
                _rellenoServicios = null;
                _historicoServicios = false;   // la pestaña se rearma con el mes actual
                _kpisServicios.Clear();
                LimpiarContenedor(_flpServicios);
                _serviciosInicializado = false;
            }

            // La primera traducción llega antes de mostrar el formulario; los datos se cargan en Shown
            if (!Visible) return;

            if (flpGeneral.Visible) CargarGeneral();
            else if (flpOcupacion.Visible) CargarOcupacion();
            else if (flpHuespedes.Visible) CargarHuespedes();
            else if (_flpServicios.Visible) CargarServicios();
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag is string tag && !string.IsNullOrWhiteSpace(tag))
                    c.Text = T(tag);

                if (c.HasChildren)
                    TraducirControles(c);
            }
        }

        #endregion

        #region Navegación de pestañas

        private void CambiarTab(int indice)
        {
            flpGeneral.Visible = indice == 0;
            flpOcupacion.Visible = indice == 1;
            flpHuespedes.Visible = indice == 2;
            _flpServicios.Visible = indice == 3;

            EstilizarTab(btnTabGeneral, indice == 0);
            EstilizarTab(btnTabOcupacion, indice == 1);
            EstilizarTab(btnTabHuespedes, indice == 2);
            EstilizarTab(_btnTabServicios, indice == 3);

            if (indice == 0) CargarGeneral();
            if (indice == 1) CargarOcupacion();
            if (indice == 2) CargarHuespedes();
            if (indice == 3) CargarServicios();
        }

        /// <summary>Cuarta pestaña (PN2): mismo estilo que las del diseñador, a la derecha de Huéspedes.</summary>
        private void CrearPestanaServicios()
        {
            _btnTabServicios = new IconButton
            {
                BackColor = btnTabHuespedes.BackColor,
                FlatStyle = FlatStyle.Flat,
                Font = btnTabHuespedes.Font,
                ForeColor = btnTabHuespedes.ForeColor,
                IconChar = IconChar.BellConcierge,
                IconColor = btnTabHuespedes.IconColor,
                IconSize = 20,
                ImageAlign = ContentAlignment.MiddleLeft,
                Location = new Point(btnTabHuespedes.Right + 10, btnTabHuespedes.Top),
                Padding = new Padding(12, 0, 0, 0),
                Size = new Size(170, btnTabHuespedes.Height),
                Text = T("Dashboard_btnTabServicios"),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            _btnTabServicios.FlatAppearance.BorderSize = btnTabHuespedes.FlatAppearance.BorderSize;
            _btnTabServicios.FlatAppearance.BorderColor = btnTabHuespedes.FlatAppearance.BorderColor;
            _btnTabServicios.Click += (s, e) => CambiarTab(3);
            pnlTabs.Controls.Add(_btnTabServicios);

            _flpServicios = new FlowLayoutPanel
            {
                AutoScroll = true,
                BackColor = flpHuespedes.BackColor,
                Dock = DockStyle.Fill,
                Padding = flpHuespedes.Padding,
                Visible = false
            };
            pnlContenido.Controls.Add(_flpServicios);
            _flpServicios.BringToFront();
            LayoutAdaptable_68SA.EstirarHijosAlAncho(_flpServicios, 0, () => _rellenoServicios, AltoMinimoTarjetaDonut);
        }

        private static void EstilizarTab(FontAwesome.Sharp.IconButton boton, bool activo)
        {
            boton.BackColor = activo ? Color.FromArgb(10, 18, 50) : Color.FromArgb(5, 10, 40);
            boton.ForeColor = activo ? Color.Goldenrod : Color.FromArgb(160, 165, 180);
            boton.IconColor = boton.ForeColor;
            boton.Font = new Font("Segoe UI", 10F, activo ? FontStyle.Bold : FontStyle.Regular);
        }

        #endregion

        #region Helpers de layout y leyendas

        /// <summary>
        /// Acomoda los controles de filtro de una tarjeta en un FlowLayoutPanel debajo del título:
        /// primera fila los accesos rápidos, segunda fila fechas y acciones. Si el ancho no alcanza,
        /// los controles pasan a la fila siguiente y la tarjeta crece en alto (nunca se superponen).
        /// </summary>
        private static void ArmarBarraFiltro(Control tarjeta, Label titulo, Control[] fila1, Control[] fila2)
        {
            titulo.Location = new Point(16, 10);

            var flp = new FlowLayoutPanel
            {
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Location = new Point(10, titulo.Bottom + 8),
                Width = Math.Max(100, tarjeta.ClientSize.Width - 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            foreach (var control in fila1.Concat(fila2))
            {
                tarjeta.Controls.Remove(control);
                control.Margin = control is Label ? new Padding(6, 12, 2, 0) : new Padding(4, 0, 8, 8);
                flp.Controls.Add(control);
            }
            flp.SetFlowBreak(fila1[fila1.Length - 1], true);

            tarjeta.Controls.Add(flp);
            LayoutAdaptable_68SA.AjustarAltoAlContenido(tarjeta, flp, 6);
        }

        private static void LimpiarContenedor(Control contenedor)
        {
            foreach (var hijo in contenedor.Controls.Cast<Control>().ToList())
                hijo.Dispose();
        }

        private static Panel CrearFilaLeyenda(string etiqueta, string valorFormateado, decimal porcentaje, Color color)
        {
            const int anchoValor = 150;
            var fila = new Panel { Size = new Size(AnchoMaximoFilaLeyenda, AltoFilaLeyenda), Margin = new Padding(0, 0, 0, SeparacionFilaLeyenda) };

            var punto = new UserControls.RoundedPanel_68SA
            {
                Size = new Size(12, 12),
                Location = new Point(0, (AltoFilaLeyenda - 12) / 2),
                BackColor = color,
                CornerRadius = 6
            };
            fila.Controls.Add(punto);

            var lblEtiqueta = new Label
            {
                Text = etiqueta,
                Location = new Point(24, (AltoFilaLeyenda - 20) / 2),
                Size = new Size(fila.Width - 24 - anchoValor - 6, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoEllipsis = true,
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI", 9.5F)
            };
            fila.Controls.Add(lblEtiqueta);

            var lblValor = new Label
            {
                Text = $"{valorFormateado}  ({porcentaje:0.#}%)",
                Location = new Point(fila.Width - anchoValor, (AltoFilaLeyenda - 20) / 2),
                Size = new Size(anchoValor, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                ForeColor = Color.FromArgb(160, 165, 180),
                Font = new Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleRight
            };
            fila.Controls.Add(lblValor);

            return fila;
        }

        private static void PoblarLeyenda(FlowLayoutPanel contenedor, List<SegmentoDonut_68SA> segmentos, Func<decimal, string> formatearValor)
        {
            contenedor.SuspendLayout();
            LimpiarContenedor(contenedor);
            decimal total = 0;
            foreach (var s in segmentos) total += s.Valor;

            foreach (var segmento in segmentos)
            {
                decimal porcentaje = total > 0 ? segmento.Valor / total * 100 : 0;
                contenedor.Controls.Add(CrearFilaLeyenda(segmento.Etiqueta, formatearValor(segmento.Valor), porcentaje, segmento.Color));
            }
            contenedor.ResumeLayout(true);
        }

        /// <summary>
        /// Carga el donut y su leyenda. La leyenda nunca muestra barra de desplazamiento: si hay más
        /// segmentos que filas visibles, los más chicos se agrupan en "Otros". Cuando la tarjeta cambia
        /// de alto se vuelve a calcular cuántas filas entran.
        /// </summary>
        private static void MostrarDonut(UC_DonutChart_68SA donut, FlowLayoutPanel leyenda, List<SegmentoDonut_68SA> segmentos,
            string textoCentral, string subtexto, Func<decimal, string> formatearValor)
        {
            int capacidadMostrada = -1;
            void Pintar()
            {
                int capacidad = Math.Max(1, (leyenda.ClientSize.Height + SeparacionFilaLeyenda) / (AltoFilaLeyenda + SeparacionFilaLeyenda));
                if (capacidad == capacidadMostrada) return;
                capacidadMostrada = capacidad;

                var visibles = AgruparRestoEnOtros(segmentos, capacidad);
                donut.Cargar(visibles, textoCentral, subtexto);
                PoblarLeyenda(leyenda, visibles, formatearValor);
            }

            // Se reemplaza el manejador de la carga anterior (queda uno solo por leyenda)
            if (leyenda.Tag is EventHandler anterior) leyenda.Resize -= anterior;
            EventHandler alRedimensionar = (s, e) => Pintar();
            leyenda.Tag = alRedimensionar;
            leyenda.Resize += alRedimensionar;
            Pintar();
        }

        private static List<SegmentoDonut_68SA> AgruparRestoEnOtros(List<SegmentoDonut_68SA> segmentos, int capacidad)
        {
            if (segmentos.Count <= capacidad) return segmentos;

            var principales = segmentos.OrderByDescending(s => s.Valor).Take(capacidad - 1).ToList();
            principales.Add(new SegmentoDonut_68SA
            {
                Etiqueta = T("Dashboard_txtOtros"),
                Valor = segmentos.Except(principales).Sum(s => s.Valor),
                Color = Color.FromArgb(110, 115, 130)
            });
            return principales;
        }

        /// <summary>
        /// Donut a la izquierda y leyenda a la derecha, ambos proporcionales a la tarjeta:
        /// el donut se achica si la tarjeta es baja y la leyenda usa todo el ancho restante.
        /// </summary>
        private static void ConfigurarTarjetaDonut(Control tarjeta, UC_DonutChart_68SA donut, FlowLayoutPanel leyenda)
        {
            const int arriba = 46, margen = 14, izquierda = 36;
            leyenda.AutoScroll = false;
            leyenda.WrapContents = false;
            leyenda.FlowDirection = FlowDirection.TopDown;
            leyenda.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            LayoutAdaptable_68SA.EstirarHijosAlAncho(leyenda, AnchoMaximoFilaLeyenda);

            void Acomodar()
            {
                int alto = tarjeta.ClientSize.Height - arriba - margen;
                if (alto <= 0 || tarjeta.ClientSize.Width <= 0) return;

                int lado = Math.Max(120, Math.Min(240, Math.Min(alto, tarjeta.ClientSize.Width / 3)));
                donut.SetBounds(izquierda, arriba + Math.Max(0, (alto - lado) / 2), lado, lado);

                int xLeyenda = donut.Right + 36;
                leyenda.SetBounds(xLeyenda, arriba + 6,
                    Math.Max(120, tarjeta.ClientSize.Width - xLeyenda - 20),
                    Math.Max(AltoFilaLeyenda, alto - 6));
            }

            tarjeta.Resize += (s, e) => Acomodar();
            Acomodar();
        }

        #endregion

        #region Pestaña General

        private void CargarGeneral()
        {
            CargarTarjetasOcupacionGeneral();
            CargarTarjetasIngresosGeneral();
        }

        private void CargarTarjetasOcupacionGeneral()
        {
            ResumenOcupacion_68SA resumen = _metricasBLL.GetResumenOcupacion();
            string deTotal = string.Format(T("Dashboard_txtDeXHab"), resumen.Total);

            lblValorDisponibles.Text = resumen.Disponibles.ToString();
            lblSubDisponibles.Text = deTotal;

            lblValorOcupadasDash.Text = resumen.Ocupadas.ToString();
            lblSubOcupadasDash.Text = deTotal;

            lblValorLimpieza.Text = resumen.EnLimpieza.ToString();
            lblSubLimpieza.Text = deTotal;

            lblValorMantenimiento.Text = resumen.FueraDeServicio.ToString();
            lblSubMantenimiento.Text = deTotal;

            lblValorTasaOcupacion.Text = $"{resumen.TasaOcupacionPorcentaje:0.##}%";
        }

        private void AplicarPreset(DateTime desde, DateTime hasta)
        {
            _historicoTotal = false;
            dtpDesdeGeneral.Enabled = true;
            dtpHastaGeneral.Enabled = true;
            dtpDesdeGeneral.Value = desde;
            dtpHastaGeneral.Value = hasta;
            ActualizarEstiloHistorico();
            CargarTarjetasIngresosGeneral();
        }

        private void AplicarHistoricoTotal()
        {
            _historicoTotal = true;
            dtpDesdeGeneral.Enabled = false;
            dtpHastaGeneral.Enabled = false;
            ActualizarEstiloHistorico();
            CargarTarjetasIngresosGeneral();
        }

        private void ActualizarEstiloHistorico()
        {
            btnFiltroHistorico.BackColor = _historicoTotal ? Color.Goldenrod : Color.FromArgb(5, 15, 45);
            btnFiltroHistorico.ForeColor = _historicoTotal ? Color.FromArgb(10, 15, 35) : Color.Goldenrod;
            btnFiltroHistorico.IconColor = btnFiltroHistorico.ForeColor;
            lblTituloFiltroGeneral.Text = T(_historicoTotal ? "Dashboard_tituloIngresosHistorico" : "Dashboard_lblTituloFiltroGeneral");
        }

        private void CargarTarjetasIngresosGeneral()
        {
            DateTime? desde = _historicoTotal ? (DateTime?)null : dtpDesdeGeneral.Value;
            DateTime? hasta = _historicoTotal ? (DateTime?)null : dtpHastaGeneral.Value;

            if (!_historicoTotal)
            {
                if (!desde.HasValue || !hasta.HasValue) return;
                if (desde.Value.Date > hasta.Value.Date)
                {
                    MessageBox.Show(T("Dashboard_msgFiltroInvalido"), T("Reservas_tituloFiltroInvalido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            ResumenIngresos_68SA resumen = _metricasBLL.GetResumenIngresos(desde, hasta);

            lblValorIngresos.Text = $"${resumen.IngresosPeriodo:N2}";
            lblValorTicketPromedio.Text = $"${resumen.TicketPromedio:N2}";
            lblValorAnuladas.Text = $"{resumen.PorcentajeReservasAnuladas:0.##}%";
            lblValorMontoAnulado.Text = $"${resumen.MontoAnuladoPeriodo:N2}";

            CargarDonutOrigenGasto(resumen);
            CargarIngresosPorDia(desde, hasta);
            CargarIndicadoresGeneral(desde, hasta, resumen);
        }

        /// <summary>
        /// Arma (o vuelve a armar al cambiar el idioma) lo que el General agrega por código: la fila de
        /// indicadores hoteleros debajo de los ingresos y, al final, medios de pago y llegadas por día.
        /// </summary>
        private void ArmarExtrasGeneral()
        {
            flpGeneral.SuspendLayout();
            try
            {
                if (_filaIndicadores != null) { flpGeneral.Controls.Remove(_filaIndicadores); _filaIndicadores.Dispose(); }
                if (_filaGeneral2 != null) { flpGeneral.Controls.Remove(_filaGeneral2); _filaGeneral2.Dispose(); }
                _kpisGeneral.Clear();

                _filaIndicadores = CrearFilaKpis(new[]
                {
                    (T("Dashboard_kpiAdr"), IconChar.Tag, Color.Goldenrod, "—", ""),
                    (T("Dashboard_kpiRevpar"), IconChar.ChartLine, ColorAzul, "—", ""),
                    (T("Dashboard_kpiOcupacionPeriodo"), IconChar.Bed, ColorRoomService, "—", ""),
                    (T("Dashboard_kpiEstadiaPromedio"), IconChar.Moon, ColorNaranja, "—", ""),
                    (T("Dashboard_kpiPesoServicios"), IconChar.BellConcierge, ColorUpgrade, "—", ""),
                }, out var etiquetas);
                string[] claves = { "adr", "revpar", "ocupacion", "estadia", "servicios" };
                for (int i = 0; i < claves.Length; i++) _kpisGeneral[claves[i]] = etiquetas[i];
                flpGeneral.Controls.Add(_filaIndicadores);
                flpGeneral.Controls.SetChildIndex(_filaIndicadores, flpGeneral.Controls.GetChildIndex(pnlCardsIngresos) + 1);

                var tarjetaPagos = CrearTarjetaDonut(T("Dashboard_tituloMediosPago"), new Size(600, AltoMinimoTarjetaDonut),
                    out _donutMediosPago, out _leyendaMediosPago);
                var tarjetaDias = CrearTarjetaGrafico(T("Dashboard_tituloLlegadasDiaSemana"), out _graficoDiaSemana, out _);
                _filaGeneral2 = CrearFila(AltoMinimoTarjetaDonut, new Padding(3, 0, 3, 3), (tarjetaPagos, 44F), (tarjetaDias, 56F));
                flpGeneral.Controls.Add(_filaGeneral2);
            }
            finally
            {
                flpGeneral.ResumeLayout(true);
            }
        }

        private void CargarIndicadoresGeneral(DateTime? desde, DateTime? hasta, ResumenIngresos_68SA ingresos)
        {
            IndicadoresPeriodo_68SA ind = _metricasBLL.GetIndicadoresPeriodo(desde, hasta);
            ResumenServicios_68SA servicios = _metricasBLL.GetResumenServicios(desde, hasta);

            PonerKpi(_kpisGeneral["adr"], $"${ind.TarifaPromedio:N0}", string.Format(T("Dashboard_kpiAdrDetalle"), ind.NochesVendidas));
            PonerKpi(_kpisGeneral["revpar"], $"${ind.RevPar:N0}", T("Dashboard_kpiRevparDetalle"));
            PonerKpi(_kpisGeneral["ocupacion"], $"{ind.OcupacionPorcentaje:0.#}%",
                string.Format(T("Dashboard_kpiOcupacionPeriodoDetalle"), ind.NochesVendidas, ind.NochesDisponibles));
            PonerKpi(_kpisGeneral["estadia"],
                ind.EstadiaPromedioNoches.HasValue ? string.Format(T("Dashboard_txtNoches"), ind.EstadiaPromedioNoches.Value) : "—",
                string.Format(T("Dashboard_kpiEstadiaDetalle"), ind.Reservas));
            decimal baseServicios = ind.IngresoAlojamiento + servicios.Total;
            decimal peso = baseServicios > 0 ? Math.Round(servicios.Total / baseServicios * 100, 1) : 0;
            PonerKpi(_kpisGeneral["servicios"], $"{peso:0.#}%",
                string.Format(T("Dashboard_kpiPesoServiciosDetalle"), $"${servicios.Total:N0}"));

            // Cobros por medio de pago
            var coloresPago = new Dictionary<MetodoPago, Color>
            {
                { MetodoPago.Efectivo, ColorRoomService },
                { MetodoPago.Tarjeta, ColorAzul },
                { MetodoPago.Transferencia, ColorUpgrade }
            };
            var segmentos = new List<SegmentoDonut_68SA>();
            foreach (var item in _metricasBLL.GetPagosPorMetodo(desde, hasta))
            {
                bool conocido = int.TryParse(item.Etiqueta, out int numero) && Enum.IsDefined(typeof(MetodoPago), numero);
                var metodo = conocido ? (MetodoPago)numero : MetodoPago.Efectivo;
                segmentos.Add(new SegmentoDonut_68SA
                {
                    Etiqueta = conocido ? OpcionEnum_68SA.Texto(metodo) : item.Etiqueta,
                    Valor = item.Valor,
                    Color = conocido ? coloresPago[metodo] : Color.Gray
                });
            }
            MostrarDonut(_donutMediosPago, _leyendaMediosPago, segmentos, $"${segmentos.Sum(x => x.Valor):N0}",
                T("Dashboard_txtCobrado"), v => $"${v:N2}");

            // Llegadas según el día de la semana del ingreso
            int[] llegadas = _metricasBLL.GetLlegadasPorDiaSemana(desde, hasta);
            var cultura = OpcionEnum_68SA.CulturaActual();
            _graficoDiaSemana.TextoVacio = T("Dashboard_txtSinLlegadas");
            _graficoDiaSemana.Cargar(llegadas.Sum() == 0 ? new List<BarraGrafico_68SA>() : Enumerable.Range(0, 7).Select(i => new BarraGrafico_68SA
                {
                    // 0 = lunes; en DayNames el 0 es domingo
                    Etiqueta = cultura.DateTimeFormat.AbbreviatedDayNames[(i + 1) % 7],
                    Titulo = cultura.TextInfo.ToTitleCase(cultura.DateTimeFormat.DayNames[(i + 1) % 7]),
                    Valores = new decimal[] { llegadas[i] }
                }).ToList(),
                new[] { ColorAzul }, new[] { T("Dashboard_serieLlegadas") }, v => v.ToString("0"));
        }

        /// <summary>
        /// El donut del origen del gasto comparte la última fila del General con el gráfico de cobros
        /// por día del período (la fila completa el alto de la pantalla).
        /// </summary>
        private void ArmarFilaGeneral()
        {
            _filaGeneral = new TableLayoutPanel
            {
                BackColor = flpGeneral.BackColor,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(3, 0, 3, 10),
                Padding = Padding.Empty,
                Size = new Size(pnlOrigenGasto.Width, Math.Max(AltoMinimoTarjetaDonut, pnlOrigenGasto.Height))
            };
            _filaGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            _filaGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));
            _filaGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            int indice = flpGeneral.Controls.GetChildIndex(pnlOrigenGasto);
            flpGeneral.Controls.Remove(pnlOrigenGasto);
            pnlOrigenGasto.Dock = DockStyle.Fill;
            pnlOrigenGasto.Margin = new Padding(0, 0, 12, 0);
            _filaGeneral.Controls.Add(pnlOrigenGasto, 0, 0);

            var tarjeta = CrearTarjetaGrafico(T("Dashboard_tituloIngresosPorDia"), out _graficoIngresos, out _lblTituloIngresosDia);
            tarjeta.Dock = DockStyle.Fill;
            tarjeta.Margin = new Padding(12, 0, 0, 0);
            _filaGeneral.Controls.Add(tarjeta, 1, 0);

            flpGeneral.Controls.Add(_filaGeneral);
            flpGeneral.Controls.SetChildIndex(_filaGeneral, indice);
        }

        private void CargarIngresosPorDia(DateTime? desde, DateTime? hasta)
        {
            var serie = MetricasBLL_68SA.Agrupar(_metricasBLL.GetIngresosPorDia(desde, hasta), out var agrupacion);
            _lblTituloIngresosDia.Text = T("Dashboard_tituloIngresosPorDia") + "  ·  " + T("Dashboard_agrupado" + agrupacion);
            _graficoIngresos.TextoVacio = T("Dashboard_txtSinCobros");
            _graficoIngresos.TextoTotal = T("Dashboard_txtTotal");
            _graficoIngresos.Cargar(Barras(serie, agrupacion, false), new[] { Color.Goldenrod },
                new[] { T("Dashboard_serieCobros") }, ValorCorto);
        }

        private void CargarDonutOrigenGasto(ResumenIngresos_68SA resumen)
        {
            // El origen llega de la base con el texto en español: se usa como clave de color y de traducción
            var coloresPorOrigen = new Dictionary<string, Color>
            {
                { "Reserva", Color.Goldenrod },
                { "Renovación", Color.FromArgb(90, 155, 235) },
                { "Servicio a la Habitación", Color.FromArgb(76, 217, 100) },
                { "Cambio de Habitación", ColorUpgrade }
            };

            var clavesPorOrigen = new Dictionary<string, string>
            {
                { "Reserva", "Dashboard_origenReserva" },
                { "Renovación", "Dashboard_origenRenovacion" },
                { "Servicio a la Habitación", "Dashboard_origenServicio" },
                { "Cambio de Habitación", "Dashboard_origenCambioHabitacion" }
            };

            var segmentos = new List<SegmentoDonut_68SA>();
            decimal total = 0;
            foreach (var item in resumen.OrigenDelGasto)
            {
                total += item.Monto;
                string etiqueta = clavesPorOrigen.TryGetValue(item.Origen, out var clave)
                    ? T(clave)
                    : item.Origen;
                segmentos.Add(new SegmentoDonut_68SA
                {
                    Etiqueta = etiqueta,
                    Valor = item.Monto,
                    Color = coloresPorOrigen.TryGetValue(item.Origen, out var color) ? color : Color.Gray
                });
            }

            MostrarDonut(ucDonutOrigenGasto, flpLeyendaOrigenGasto, segmentos, $"${total:N0}", T("Dashboard_txtTotal"), v => $"${v:N2}");
        }

        private void GenerarReporteReservas()
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.GenerarReporteReservas);

            DateTime? desde = _historicoTotal ? (DateTime?)null : dtpDesdeGeneral.Value;
            DateTime? hasta = _historicoTotal ? (DateTime?)null : dtpHastaGeneral.Value;

            if (!_historicoTotal && (!desde.HasValue || !hasta.HasValue))
            {
                MessageBox.Show(T("Dashboard_msgFaltaPeriodo"), T("Dashboard_tituloFaltaPeriodo"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // El reporte se arma en la pantalla de Reportes (con vista previa), con el período elegido acá
            AbrirEnReportes(Reportes.TipoReporte_68SA.Reservas, desde, hasta);
        }

        /// <summary>Abre la pantalla de Reportes en la pestaña indicada y genera el reporte con este período.</summary>
        private void AbrirEnReportes(Reportes.TipoReporte_68SA pestaña, DateTime? desde, DateTime? hasta)
        {
            if (ParentForm is FormMDI_08YS mdi)
                mdi.AbrirReportes(pestaña, desde, hasta, generar: true);
            else
                new Reportes.FormReportes_68SA(pestaña, desde, hasta, true).Show(this);
        }

        #endregion

        #region Pestaña Ocupación

        private void CargarOcupacion()
        {
            flpOcupacion.SuspendLayout();
            try
            {
                _rellenoOcupacion = null;
                LimpiarContenedor(flpOcupacion);

                ResumenOcupacion_68SA resumenEstado = _metricasBLL.GetResumenOcupacion();
                List<OcupacionPorTipoItem_68SA> porTipo = _metricasBLL.GetOcupacionPorTipo();
                MovimientoHoy_68SA hoy = _metricasBLL.GetMovimientoHoy();

                // ---- Movimiento de hoy (recepción)
                var filaHoy = CrearFilaKpis(new[]
                {
                    (T("Dashboard_kpiLlegadasHoy"), IconChar.PlaneArrival, Color.FromArgb(90, 155, 235),
                        hoy.Llegadas.ToString(), string.Format(T("Dashboard_kpiLlegadasHoyDetalle"), hoy.LlegadasPendientes)),
                    (T("Dashboard_kpiSalidasHoy"), IconChar.PlaneDeparture, Color.FromArgb(235, 140, 60),
                        hoy.Salidas.ToString(), string.Format(T("Dashboard_kpiSalidasHoyDetalle"), hoy.SalidasPendientes)),
                    (T("Dashboard_kpiAlojados"), IconChar.PeopleRoof, Color.FromArgb(76, 217, 100),
                        hoy.HuespedesAlojados.ToString(), string.Format(T("Dashboard_kpiAlojadosDetalle"), hoy.EstadiasEnCurso)),
                    (T("Dashboard_kpiTarifaPromedio"), IconChar.Tag, Color.Goldenrod,
                        $"${hoy.TarifaPromedio:N0}", T("Dashboard_kpiTarifaPromedioDetalle")),
                }, out _);
                flpOcupacion.Controls.Add(filaHoy);

                // Dos tarjetas lado a lado que se reparten el ancho en partes iguales
                var filaDonuts = new TableLayoutPanel
                {
                    ColumnCount = 2,
                    RowCount = 1,
                    Size = new Size(1424, 300),
                    Margin = new Padding(3, 3, 3, 10),
                    Padding = Padding.Empty
                };
                filaDonuts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                filaDonuts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                filaDonuts.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

                var panelEstado = CrearTarjetaDonut(T("Dashboard_tituloOcupacionEstado"), new Size(700, 340), out var donutEstado, out var leyendaEstado);
                panelEstado.Dock = DockStyle.Fill;
                panelEstado.Margin = new Padding(0, 0, 12, 0);
                filaDonuts.Controls.Add(panelEstado, 0, 0);

                var panelTipo = CrearTarjetaDonut(T("Dashboard_tituloOcupacionTipo"), new Size(700, 340), out var donutTipo, out var leyendaTipo);
                panelTipo.Dock = DockStyle.Fill;
                panelTipo.Margin = new Padding(12, 0, 0, 0);
                filaDonuts.Controls.Add(panelTipo, 1, 0);

                flpOcupacion.Controls.Add(filaDonuts);

                // ---- Llegadas y salidas de la próxima semana + habitaciones más ocupadas
                var cultura = OpcionEnum_68SA.CulturaActual();
                var proximos = _metricasBLL.GetMovimientoProximosDias(7);
                var tarjetaProximos = CrearTarjetaGrafico(T("Dashboard_tituloProximosDias"), out var graficoProximos, out _);
                graficoProximos.TextoVacio = T("Dashboard_txtSinDatos");
                graficoProximos.TextoTotal = T("Dashboard_txtMovimientos");
                graficoProximos.Cargar(proximos.Select(d => new BarraGrafico_68SA
                    {
                        Etiqueta = d.Fecha.ToString("ddd dd", cultura),
                        Titulo = d.Fecha.ToString("dddd dd/MM", cultura),
                        Valores = new[] { d.Valor, d.Valor2 }
                    }).ToList(),
                    new[] { ColorAzul, ColorNaranja }, new[] { T("Dashboard_serieLlegadas"), T("Dashboard_serieSalidas") }, v => v.ToString("0"));

                var masOcupadas = _metricasBLL.GetHabitacionesMasOcupadas(90, 8);
                var tarjetaRanking = CrearTarjetaGrafico(T("Dashboard_tituloHabitacionesMasOcupadas"), out var graficoRanking, out _);
                graficoRanking.Horizontal = true;
                graficoRanking.TextoVacio = T("Dashboard_txtSinDatos");
                graficoRanking.Cargar(masOcupadas.Select(h => new BarraGrafico_68SA
                    {
                        Etiqueta = h.Etiqueta,
                        Titulo = h.Etiqueta + "  ·  " + string.Format(T("Dashboard_txtEstadias"), h.Cantidad),
                        Valores = new[] { h.Valor }
                    }).ToList(),
                    new[] { Color.Goldenrod }, new[] { T("Dashboard_serieNoches") }, v => string.Format(T("Dashboard_txtNochesCorto"), v.ToString("0")));

                flpOcupacion.Controls.Add(CrearFila(290, new Padding(3, 0, 3, 10), (tarjetaProximos, 55F), (tarjetaRanking, 45F)));

                var segmentosEstado = new List<SegmentoDonut_68SA>
                {
                    new SegmentoDonut_68SA { Etiqueta = T("ControlHabitaciones_estadoDisponible"), Valor = resumenEstado.Disponibles, Color = Color.FromArgb(76, 217, 100) },
                    new SegmentoDonut_68SA { Etiqueta = T("ControlHabitaciones_estadoReservada"), Valor = resumenEstado.Reservadas, Color = Color.MediumPurple },
                    new SegmentoDonut_68SA { Etiqueta = T("ControlHabitaciones_estadoOcupada"), Valor = resumenEstado.Ocupadas, Color = Color.Goldenrod },
                    new SegmentoDonut_68SA { Etiqueta = T("ControlHabitaciones_estadoLimpieza"), Valor = resumenEstado.EnLimpieza, Color = Color.FromArgb(90, 155, 235) },
                    new SegmentoDonut_68SA { Etiqueta = T("Dashboard_segMantenimiento"), Valor = resumenEstado.FueraDeServicio, Color = Color.FromArgb(235, 140, 60) }
                };
                MostrarDonut(donutEstado, leyendaEstado, segmentosEstado, resumenEstado.Total.ToString(), T("Dashboard_txtHabitaciones"), v => v.ToString("0"));

                var paletaTipo = new[]
                {
                    Color.Goldenrod, Color.FromArgb(90, 155, 235), Color.FromArgb(76, 217, 100),
                    Color.MediumPurple, Color.FromArgb(235, 140, 60), Color.FromArgb(235, 90, 90)
                };
                var segmentosTipo = new List<SegmentoDonut_68SA>();
                int totalOcupadasPorTipo = 0;
                for (int i = 0; i < porTipo.Count; i++)
                {
                    segmentosTipo.Add(new SegmentoDonut_68SA
                    {
                        Etiqueta = porTipo[i].TipoHabitacion,
                        Valor = porTipo[i].CantidadOcupadas,
                        Color = paletaTipo[i % paletaTipo.Length]
                    });
                    totalOcupadasPorTipo += porTipo[i].CantidadOcupadas;
                }
                MostrarDonut(donutTipo, leyendaTipo, segmentosTipo, totalOcupadasPorTipo.ToString(), T("Dashboard_txtOcupadas"), v => v.ToString("0"));

                // ---- Ocupación prevista: % de habitaciones con estadía o reserva cada noche (completa el alto)
                var prevista = _metricasBLL.GetOcupacionPrevista(14);
                var tarjetaPrevista = CrearTarjetaGrafico(T("Dashboard_tituloOcupacionPrevista"), out var graficoPrevista, out _);
                tarjetaPrevista.Size = new Size(1424, AltoMinimoTarjetaDonut);
                tarjetaPrevista.Margin = new Padding(3, 0, 3, 10);
                graficoPrevista.TextoVacio = T("Dashboard_txtSinDatos");
                graficoPrevista.Cargar(prevista.Select(d => new BarraGrafico_68SA
                    {
                        Etiqueta = d.Fecha.ToString("ddd dd", cultura),
                        Titulo = d.Fecha.ToString("dddd dd/MM", cultura) + "  ·  " + string.Format(T("Dashboard_txtHabitacionesOcupadas"), d.Ocupadas, d.Total),
                        Valores = new[] { d.Porcentaje }
                    }).ToList(),
                    new[] { Color.Goldenrod }, new[] { T("Dashboard_serieOcupacion") }, v => v.ToString("0") + "%", 100);
                _rellenoOcupacion = tarjetaPrevista;
                flpOcupacion.Controls.Add(tarjetaPrevista);

                // ---- Ocupación de los últimos 12 meses (noches vendidas sobre noches disponibles)
                var mensual = _metricasBLL.GetOcupacionMensual(12);
                var tarjetaMensual = CrearTarjetaGrafico(T("Dashboard_tituloOcupacionMensual"), out var graficoMensual, out _);
                tarjetaMensual.Size = new Size(1424, 260);
                tarjetaMensual.Margin = new Padding(3, 0, 3, 10);
                graficoMensual.TextoVacio = T("Dashboard_txtSinDatos");
                graficoMensual.Cargar(mensual.Select(m => new BarraGrafico_68SA
                    {
                        Etiqueta = m.Fecha.ToString("MMM yy", cultura),
                        Titulo = cultura.TextInfo.ToTitleCase(m.Fecha.ToString("MMMM yyyy", cultura)) + "  ·  "
                                 + string.Format(T("Dashboard_kpiOcupacionPeriodoDetalle"), m.Ocupadas, m.Total),
                        Valores = new[] { m.Porcentaje }
                    }).ToList(),
                    new[] { ColorRoomService }, new[] { T("Dashboard_serieOcupacion") }, v => v.ToString("0") + "%", 100);
                flpOcupacion.Controls.Add(tarjetaMensual);

                var panelEstadia = new UserControls.RoundedPanel_68SA
                {
                    BackColor = Color.FromArgb(10, 18, 50),
                    CornerRadius = 12,
                    Size = new Size(1424, 100),
                    Margin = new Padding(3, 0, 3, 3)
                };
                panelEstadia.Controls.Add(new Label
                {
                    Text = T("Dashboard_tituloDuracionEstadia"),
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(160, 165, 180),
                    Location = new Point(16, 14),
                    AutoSize = true
                });
                panelEstadia.Controls.Add(new Label
                {
                    Text = string.Format(T("Dashboard_txtHoras"), resumenEstado.DuracionPromedioEstadiaHoras),
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                    ForeColor = Color.Goldenrod,
                    Location = new Point(16, 34),
                    AutoSize = true
                });
                flpOcupacion.Controls.Add(panelEstadia);
            }
            finally
            {
                flpOcupacion.ResumeLayout(true);
            }
        }

        private static UserControls.RoundedPanel_68SA CrearTarjetaDonut(string titulo, Size tamano,
            out UC_DonutChart_68SA donut, out FlowLayoutPanel leyenda)
        {
            var panel = new UserControls.RoundedPanel_68SA
            {
                BackColor = Color.FromArgb(10, 18, 50),
                CornerRadius = 12,
                Size = tamano
            };

            panel.Controls.Add(new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Location = new Point(16, 14),
                AutoSize = true
            });

            donut = new UC_DonutChart_68SA
            {
                Size = new Size(220, 220),
                Location = new Point(40, 50),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(donut);

            // La leyenda ocupa el espacio a la derecha del donut y acompaña el tamaño de la tarjeta
            leyenda = new FlowLayoutPanel { Location = new Point(290, 60), Size = new Size(tamano.Width - 310, tamano.Height - 80) };
            panel.Controls.Add(leyenda);
            ConfigurarTarjetaDonut(panel, donut, leyenda);

            return panel;
        }

        #endregion

        #region Pestaña Huéspedes

        private void CargarHuespedes()
        {
            if (!_huespedesInicializado)
            {
                InicializarControlesHuespedes();
                _huespedesInicializado = true;
            }

            ActualizarDatosHuespedes();
        }

        private void InicializarControlesHuespedes()
        {
            // ---- Tarjeta de filtro (mismo armado que en General) ----
            var pnlFiltro = new UserControls.RoundedPanel_68SA
            {
                BackColor = Color.FromArgb(10, 18, 50),
                CornerRadius = 12,
                Size = new Size(1424, 112),
                Margin = new Padding(3, 3, 3, 10)
            };

            _lblTituloFiltroHuespedes = new Label
            {
                Text = T("Dashboard_tituloHuespedesPeriodo"),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Location = new Point(16, 10),
                AutoSize = true
            };
            pnlFiltro.Controls.Add(_lblTituloFiltroHuespedes);

            var lblDesde = new Label
            {
                Text = T("Dashboard_lblEtiquetaDesdeGeneral"),
                ForeColor = Color.FromArgb(160, 165, 180),
                AutoSize = true
            };

            _dtpDesdeHuespedes = CrearSelectorFecha(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));

            var lblHasta = new Label
            {
                Text = T("Dashboard_lblEtiquetaHastaGeneral"),
                ForeColor = Color.FromArgb(160, 165, 180),
                AutoSize = true
            };

            _dtpHastaHuespedes = CrearSelectorFecha(DateTime.Today);

            var btnActualizar = new IconButton
            {
                BackColor = Color.Goldenrod,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(10, 15, 35),
                IconChar = IconChar.ArrowsRotate,
                IconColor = Color.FromArgb(10, 15, 35),
                IconSize = 20,
                ImageAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 0, 0),
                Size = new Size(170, 44),
                Text = T("Dashboard_btnActualizarGeneral"),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            btnActualizar.Click += (s, e) =>
            {
                _historicoTotalHuespedes = false;
                ActualizarEstiloHistoricoHuespedes();
                ActualizarDatosHuespedes();
            };

            void AplicarPresetHuespedes(DateTime desde, DateTime hasta)
            {
                _historicoTotalHuespedes = false;
                _dtpDesdeHuespedes.Enabled = true;
                _dtpHastaHuespedes.Enabled = true;
                _dtpDesdeHuespedes.Value = desde;
                _dtpHastaHuespedes.Value = hasta;
                ActualizarEstiloHistoricoHuespedes();
                ActualizarDatosHuespedes();
            }

            var btnHoy = CrearBotonPreset(T("Dashboard_btnFiltroHoy"), 130);
            btnHoy.Click += (s, e) => AplicarPresetHuespedes(DateTime.Today, DateTime.Today);

            var btn7Dias = CrearBotonPreset(T("Dashboard_btnFiltro7Dias"), 140);
            btn7Dias.Click += (s, e) => AplicarPresetHuespedes(DateTime.Today.AddDays(-7), DateTime.Today);

            var btn30Dias = CrearBotonPreset(T("Dashboard_btnFiltro30Dias"), 140);
            btn30Dias.Click += (s, e) => AplicarPresetHuespedes(DateTime.Today.AddDays(-30), DateTime.Today);

            var btnEsteMes = CrearBotonPreset(T("Dashboard_btnFiltroEsteMes"), 130);
            btnEsteMes.Click += (s, e) => AplicarPresetHuespedes(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);

            _btnHistoricoHuespedes = new IconButton
            {
                BackColor = Color.FromArgb(5, 15, 45),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Goldenrod,
                IconChar = IconChar.Infinity,
                IconColor = Color.Goldenrod,
                IconSize = 16,
                ImageAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Size = new Size(150, 36),
                Text = T("Dashboard_btnFiltroHistorico"),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            _btnHistoricoHuespedes.Click += (s, e) =>
            {
                _historicoTotalHuespedes = true;
                _dtpDesdeHuespedes.Enabled = false;
                _dtpHastaHuespedes.Enabled = false;
                ActualizarEstiloHistoricoHuespedes();
                ActualizarDatosHuespedes();
            };

            ArmarBarraFiltro(pnlFiltro, _lblTituloFiltroHuespedes,
                new Control[] { btnHoy, btn7Dias, btn30Dias, btnEsteMes, _btnHistoricoHuespedes },
                new Control[] { lblDesde, _dtpDesdeHuespedes, lblHasta, _dtpHastaHuespedes, btnActualizar });
            flpHuespedes.Controls.Add(pnlFiltro);

            // ---- Tarjetas KPI (4 columnas iguales) ----
            var filaCards = new TableLayoutPanel
            {
                ColumnCount = 4,
                Size = new Size(1424, 126),
                Margin = new Padding(3, 0, 3, 10)
            };
            for (int i = 0; i < 4; i++)
                filaCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            filaCards.RowCount = 1;
            filaCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var cardNuevos = CrearTarjetaKpi(T("Dashboard_kpiNuevos"), IconChar.UserPlus, Color.FromArgb(76, 217, 100), out _lblValorNuevos);
            cardNuevos.Dock = DockStyle.Fill;
            cardNuevos.Margin = new Padding(3, 3, 12, 3);
            filaCards.Controls.Add(cardNuevos, 0, 0);

            var cardRecurrentes = CrearTarjetaKpi(T("Dashboard_kpiRecurrentes"), IconChar.UserClock, Color.FromArgb(90, 155, 235), out _lblValorRecurrentes);
            cardRecurrentes.Dock = DockStyle.Fill;
            cardRecurrentes.Margin = new Padding(3, 3, 12, 3);
            filaCards.Controls.Add(cardRecurrentes, 1, 0);

            var cardTasa = CrearTarjetaKpi(T("Dashboard_kpiTasaRecurrencia"), IconChar.ArrowRotateRight, Color.Goldenrod, out _lblValorTasaRecurrencia);
            cardTasa.Dock = DockStyle.Fill;
            cardTasa.Margin = new Padding(3, 3, 12, 3);
            filaCards.Controls.Add(cardTasa, 2, 0);

            var cardGasto = CrearTarjetaKpi(T("Dashboard_kpiGastoPromedio"), IconChar.Wallet, Color.FromArgb(235, 140, 60), out _lblValorGastoPromedio);
            cardGasto.Dock = DockStyle.Fill;
            cardGasto.Margin = new Padding(3, 3, 3, 3);
            filaCards.Controls.Add(cardGasto, 3, 0);

            flpHuespedes.Controls.Add(filaCards);

            // ---- Perfil de las reservas del período
            var filaPerfil = CrearFilaKpis(new[]
            {
                (T("Dashboard_kpiEstadiaPromedio"), IconChar.Moon, ColorAzul, "—", ""),
                (T("Dashboard_kpiAnticipacion"), IconChar.CalendarCheck, Color.Goldenrod, "—", ""),
                (T("Dashboard_kpiPersonasReserva"), IconChar.PeopleGroup, ColorRoomService, "—", ""),
                (T("Dashboard_kpiConNinos"), IconChar.Child, ColorNaranja, "—", ""),
            }, out var etiquetasPerfil);
            string[] claves = { "estadia", "anticipacion", "personas", "ninos" };
            for (int i = 0; i < claves.Length; i++) _kpisHuespedes[claves[i]] = etiquetasPerfil[i];
            flpHuespedes.Controls.Add(filaPerfil);

            // ---- Titulares que más gastaron + edades de los alojados
            var tarjetaTop = CrearTarjetaGrafico(T("Dashboard_tituloTopHuespedes"), out _graficoTopHuespedes, out _);
            _graficoTopHuespedes.Horizontal = true;
            var tarjetaEdades = CrearTarjetaGrafico(T("Dashboard_tituloRangoEtario"), out _graficoEdades, out _);
            flpHuespedes.Controls.Add(CrearFila(320, new Padding(3, 0, 3, 10), (tarjetaTop, 55F), (tarjetaEdades, 45F)));

            // ---- Nacionalidad y composición de los grupos (completan el alto de la pantalla)
            var panelNacionalidad = CrearTarjetaDonut(T("Dashboard_tituloNacionalidad"), new Size(700, AltoMinimoTarjetaDonut), out _donutNacionalidad, out _leyendaNacionalidad);
            var panelComposicion = CrearTarjetaDonut(T("Dashboard_tituloComposicion"), new Size(700, AltoMinimoTarjetaDonut), out _donutComposicion, out _leyendaComposicion);
            var filaDonuts = CrearFila(AltoMinimoTarjetaDonut, new Padding(3, 0, 3, 3), (panelNacionalidad, 50F), (panelComposicion, 50F));
            _rellenoHuespedes = filaDonuts;
            flpHuespedes.Controls.Add(filaDonuts);
        }

        private static IconDateTimePicker CrearSelectorFecha(DateTime valor)
        {
            return new IconDateTimePicker
            {
                BackColor = Color.FromArgb(5, 15, 45),
                BorderColor = Color.Goldenrod,
                BorderFocusColor = Color.Goldenrod,
                BorderWidth = 2,
                CalendarBackColor = Color.FromArgb(5, 15, 45),
                CalendarForeColor = SystemColors.ControlLightLight,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9F),
                ForeColor = SystemColors.ControlLightLight,
                IconChar = IconChar.CalendarDay,
                IconColor = Color.Goldenrod,
                IconSize = 20,
                Size = new Size(220, 40),
                Value = valor
            };
        }

        private static IconButton CrearBotonPreset(string texto, int ancho)
        {
            return new IconButton
            {
                BackColor = Color.FromArgb(5, 15, 45),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Goldenrod,
                Size = new Size(ancho, 36),
                Text = texto,
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false
            };
        }

        private static UserControls.RoundedPanel_68SA CrearTarjetaKpi(string titulo, IconChar icono, Color color, out Label lblValor)
        {
            var panel = new UserControls.RoundedPanel_68SA
            {
                BackColor = Color.FromArgb(10, 18, 50),
                CornerRadius = 12
            };

            var iconoKpi = new IconPictureBox
            {
                IconChar = icono,
                IconColor = color,
                IconSize = 28,
                Location = new Point(288, 32),
                Size = new Size(36, 36)
            };
            panel.Controls.Add(iconoKpi);
            // El ícono queda siempre contra el borde derecho de la tarjeta, sea cual sea su ancho
            panel.Resize += (s, e) => iconoKpi.Left = Math.Max(16, panel.ClientSize.Width - iconoKpi.Width - 16);

            panel.Controls.Add(new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(160, 165, 180),
                Location = new Point(16, 14),
                AutoSize = true
            });

            lblValor = new Label
            {
                Text = "0",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.WhiteSmoke,
                Location = new Point(16, 32),
                AutoSize = true
            };
            panel.Controls.Add(lblValor);
            LayoutAdaptable_68SA.AjustarValorTarjeta(panel, lblValor, iconoKpi);

            return panel;
        }

        private void ActualizarEstiloHistoricoHuespedes()
        {
            _btnHistoricoHuespedes.BackColor = _historicoTotalHuespedes ? Color.Goldenrod : Color.FromArgb(5, 15, 45);
            _btnHistoricoHuespedes.ForeColor = _historicoTotalHuespedes ? Color.FromArgb(10, 15, 35) : Color.Goldenrod;
            _btnHistoricoHuespedes.IconColor = _btnHistoricoHuespedes.ForeColor;
            _lblTituloFiltroHuespedes.Text = T(_historicoTotalHuespedes ? "Dashboard_tituloHuespedesHistorico" : "Dashboard_tituloHuespedesPeriodo");
        }

        private void ActualizarDatosHuespedes()
        {
            DateTime? desde = _historicoTotalHuespedes ? (DateTime?)null : _dtpDesdeHuespedes.Value;
            DateTime? hasta = _historicoTotalHuespedes ? (DateTime?)null : _dtpHastaHuespedes.Value;

            if (!_historicoTotalHuespedes)
            {
                if (!desde.HasValue || !hasta.HasValue) return;
                if (desde.Value.Date > hasta.Value.Date)
                {
                    MessageBox.Show(T("Dashboard_msgFiltroInvalido"), T("Reservas_tituloFiltroInvalido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            ResumenHuespedes_68SA resumen = _metricasBLL.GetResumenHuespedes(desde, hasta);

            _lblValorNuevos.Text = resumen.Nuevos.ToString();
            _lblValorRecurrentes.Text = resumen.Recurrentes.ToString();
            _lblValorTasaRecurrencia.Text = $"{resumen.TasaRecurrenciaPorcentaje:0.##}%";
            _lblValorGastoPromedio.Text = $"${resumen.GastoPromedioPorHuesped:N2}";

            var paleta = new[]
            {
                Color.Goldenrod, Color.FromArgb(90, 155, 235), Color.FromArgb(76, 217, 100),
                Color.MediumPurple, Color.FromArgb(235, 140, 60), Color.FromArgb(235, 90, 90),
                Color.FromArgb(160, 165, 180)
            };
            var segmentos = new List<SegmentoDonut_68SA>();
            for (int i = 0; i < resumen.PorNacionalidad.Count; i++)
            {
                segmentos.Add(new SegmentoDonut_68SA
                {
                    Etiqueta = resumen.PorNacionalidad[i].Nacionalidad,
                    Valor = resumen.PorNacionalidad[i].Cantidad,
                    Color = paleta[i % paleta.Length]
                });
            }
            MostrarDonut(_donutNacionalidad, _leyendaNacionalidad, segmentos, resumen.TotalAtendidos.ToString(), T("Dashboard_txtHuespedes"), v => v.ToString("0"));

            CargarPerfilHuespedes(desde, hasta);
        }

        private void CargarPerfilHuespedes(DateTime? desde, DateTime? hasta)
        {
            IndicadoresPeriodo_68SA ind = _metricasBLL.GetIndicadoresPeriodo(desde, hasta);
            PonerKpi(_kpisHuespedes["estadia"],
                ind.EstadiaPromedioNoches.HasValue ? string.Format(T("Dashboard_txtNoches"), ind.EstadiaPromedioNoches.Value) : "—",
                string.Format(T("Dashboard_kpiEstadiaDetalle"), ind.Reservas));
            PonerKpi(_kpisHuespedes["anticipacion"],
                ind.AnticipacionPromedioDias.HasValue ? string.Format(T("Dashboard_txtDias"), ind.AnticipacionPromedioDias.Value) : "—",
                T("Dashboard_kpiAnticipacionDetalle"));
            PonerKpi(_kpisHuespedes["personas"],
                ind.PersonasPorReserva.HasValue ? ind.PersonasPorReserva.Value.ToString("0.#", OpcionEnum_68SA.CulturaActual()) : "—",
                T("Dashboard_kpiPersonasReservaDetalle"));
            PonerKpi(_kpisHuespedes["ninos"],
                ind.PorcentajeConNinos.HasValue ? $"{ind.PorcentajeConNinos.Value:0.#}%" : "—",
                T("Dashboard_kpiConNinosDetalle"));

            // Ranking de titulares: la barra es el total y el globo separa alojamiento y servicios
            var top = _metricasBLL.GetTopHuespedes(desde, hasta, 8);
            _graficoTopHuespedes.TextoVacio = T("Dashboard_txtSinHuespedes");
            _graficoTopHuespedes.TextoTotal = T("Dashboard_txtTotal");
            _graficoTopHuespedes.Cargar(top.Select(h => new BarraGrafico_68SA
                {
                    Etiqueta = h.Nombre,
                    Titulo = h.Nombre + "  ·  " + string.Format(T("Dashboard_txtEstadiasNoches"), h.Estadias, h.Noches),
                    Valores = new[] { h.MontoAlojamiento, h.MontoServicios }
                }).ToList(),
                new[] { Color.Goldenrod, ColorRoomService }, new[] { T("Dashboard_serieAlojamiento"), T("Dashboard_serieServicios") }, ValorCorto);

            // Edades: siempre los 6 rangos, en orden (los que no tienen huéspedes quedan en cero)
            var edades = _metricasBLL.GetRangoEtario(desde, hasta);
            var porRango = new decimal[6];
            foreach (var e in edades)
                if (int.TryParse(e.Etiqueta, out int rango) && rango >= 0 && rango < porRango.Length) porRango[rango] = e.Cantidad;
            _graficoEdades.TextoVacio = T("Dashboard_txtSinHuespedes");
            _graficoEdades.Cargar(porRango.Sum() == 0 ? new List<BarraGrafico_68SA>() : Enumerable.Range(0, porRango.Length).Select(i => new BarraGrafico_68SA
                {
                    Etiqueta = T("Dashboard_edad" + i),
                    Titulo = T("Dashboard_edadTitulo" + i),
                    Valores = new[] { porRango[i] }
                }).ToList(),
                new[] { ColorAzul }, new[] { T("Dashboard_serieHuespedes") }, v => v.ToString("0"));

            // Composición de los grupos de cada reserva
            var coloresGrupo = new[] { ColorAzul, Color.Goldenrod, ColorRoomService, ColorUpgrade };
            var segmentosGrupo = new List<SegmentoDonut_68SA>();
            foreach (var g in _metricasBLL.GetComposicionGrupos(desde, hasta))
            {
                if (!int.TryParse(g.Etiqueta, out int grupo) || grupo < 0 || grupo > 3) continue;
                segmentosGrupo.Add(new SegmentoDonut_68SA { Etiqueta = T("Dashboard_grupo" + grupo), Valor = g.Cantidad, Color = coloresGrupo[grupo] });
            }
            MostrarDonut(_donutComposicion, _leyendaComposicion, segmentosGrupo, segmentosGrupo.Sum(x => x.Valor).ToString("0"),
                T("Dashboard_txtReservas"), v => v.ToString("0"));
        }

        #endregion

        #region Pestaña Servicios (PN2)

        private void CargarServicios()
        {
            if (!_serviciosInicializado)
            {
                InicializarControlesServicios();
                _serviciosInicializado = true;
            }
            ActualizarDatosServicios();
        }

        private void InicializarControlesServicios()
        {
            _flpServicios.SuspendLayout();

            // ---- Filtro (mismo armado que Huéspedes) + acceso al reporte de servicios
            var pnlFiltro = new UserControls.RoundedPanel_68SA
            {
                BackColor = Color.FromArgb(10, 18, 50),
                CornerRadius = 12,
                Size = new Size(1424, 112),
                Margin = new Padding(3, 3, 3, 10)
            };
            _lblTituloFiltroServicios = new Label
            {
                Text = T("Dashboard_tituloServiciosPeriodo"),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Location = new Point(16, 10),
                AutoSize = true
            };
            pnlFiltro.Controls.Add(_lblTituloFiltroServicios);

            var lblDesde = new Label { Text = T("Dashboard_lblEtiquetaDesdeGeneral"), ForeColor = Color.FromArgb(160, 165, 180), AutoSize = true };
            var lblHasta = new Label { Text = T("Dashboard_lblEtiquetaHastaGeneral"), ForeColor = Color.FromArgb(160, 165, 180), AutoSize = true };
            _dtpDesdeServicios = CrearSelectorFecha(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
            _dtpHastaServicios = CrearSelectorFecha(DateTime.Today);

            var btnActualizar = new IconButton
            {
                BackColor = Color.Goldenrod,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(10, 15, 35),
                IconChar = IconChar.ArrowsRotate,
                IconColor = Color.FromArgb(10, 15, 35),
                IconSize = 20,
                ImageAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 0, 0),
                Size = new Size(170, 44),
                Text = T("Dashboard_btnActualizarGeneral"),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            btnActualizar.Click += (s, e) => { _historicoServicios = false; ActualizarEstiloHistoricoServicios(); ActualizarDatosServicios(); };

            var btnReporte = new IconButton
            {
                BackColor = Color.FromArgb(90, 155, 235),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(10, 15, 35),
                IconChar = IconChar.FileInvoiceDollar,
                IconColor = Color.FromArgb(10, 15, 35),
                IconSize = 26,
                ImageAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                Size = new Size(260, 44),
                Text = T("Dashboard_btnReporteServicios"),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false,
                Visible = SessionManager_08YS.Instance.HasPermission(Permisos.GenerarReporteServicios)
            };
            btnReporte.Click += (s, e) => AbrirReporteServicios();

            void Preset(DateTime desde, DateTime hasta)
            {
                _historicoServicios = false;
                _dtpDesdeServicios.Enabled = _dtpHastaServicios.Enabled = true;
                _dtpDesdeServicios.Value = desde;
                _dtpHastaServicios.Value = hasta;
                ActualizarEstiloHistoricoServicios();
                ActualizarDatosServicios();
            }

            var btnHoy = CrearBotonPreset(T("Dashboard_btnFiltroHoy"), 130);
            btnHoy.Click += (s, e) => Preset(DateTime.Today, DateTime.Today);
            var btn7 = CrearBotonPreset(T("Dashboard_btnFiltro7Dias"), 140);
            btn7.Click += (s, e) => Preset(DateTime.Today.AddDays(-7), DateTime.Today);
            var btn30 = CrearBotonPreset(T("Dashboard_btnFiltro30Dias"), 140);
            btn30.Click += (s, e) => Preset(DateTime.Today.AddDays(-30), DateTime.Today);
            var btnMes = CrearBotonPreset(T("Dashboard_btnFiltroEsteMes"), 130);
            btnMes.Click += (s, e) => Preset(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);

            _btnHistoricoServicios = new IconButton
            {
                BackColor = Color.FromArgb(5, 15, 45),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Goldenrod,
                IconChar = IconChar.Infinity,
                IconColor = Color.Goldenrod,
                IconSize = 16,
                ImageAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Size = new Size(150, 36),
                Text = T("Dashboard_btnFiltroHistorico"),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            _btnHistoricoServicios.Click += (s, e) =>
            {
                _historicoServicios = true;
                _dtpDesdeServicios.Enabled = _dtpHastaServicios.Enabled = false;
                ActualizarEstiloHistoricoServicios();
                ActualizarDatosServicios();
            };

            ArmarBarraFiltro(pnlFiltro, _lblTituloFiltroServicios,
                new Control[] { btnHoy, btn7, btn30, btnMes, _btnHistoricoServicios },
                new Control[] { lblDesde, _dtpDesdeServicios, lblHasta, _dtpHastaServicios, btnActualizar, btnReporte });
            _flpServicios.Controls.Add(pnlFiltro);

            // ---- Indicadores
            var filaKpis = CrearFilaKpis(new[]
            {
                (T("Dashboard_kpiIngresosServicios"), IconChar.MoneyBillTrendUp, Color.Goldenrod, "—", ""),
                (T("Dashboard_kpiPedidosRS"), IconChar.BellConcierge, ColorRoomService, "—", ""),
                (T("Dashboard_kpiUpgrades"), IconChar.ArrowUpRightDots, ColorUpgrade, "—", ""),
                (T("Dashboard_kpiEstadiasConConsumo"), IconChar.Percent, Color.FromArgb(90, 155, 235), "—", ""),
                (T("Dashboard_kpiEntregaPromedio"), IconChar.Stopwatch, Color.FromArgb(80, 200, 200), "—", ""),
            }, out var etiquetas);
            string[] claves = { "ingresos", "pedidos", "upgrades", "estadias", "entrega" };
            for (int i = 0; i < claves.Length; i++) _kpisServicios[claves[i]] = etiquetas[i];
            _flpServicios.Controls.Add(filaKpis);

            // ---- Ingresos por día (apilado) y productos más pedidos
            var fila = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 1,
                Size = new Size(1424, 300),
                Margin = new Padding(3, 0, 3, 10),
                Padding = Padding.Empty
            };
            fila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            fila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            fila.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var tarjetaDia = CrearTarjetaGrafico(T("Dashboard_tituloServiciosPorDia"), out _graficoServiciosDia, out _lblTituloServiciosDia);
            tarjetaDia.Dock = DockStyle.Fill;
            tarjetaDia.Margin = new Padding(0, 0, 12, 0);
            fila.Controls.Add(tarjetaDia, 0, 0);

            var tarjetaTop = CrearTarjetaGrafico(T("Dashboard_tituloTopProductos"), out _graficoTopProductos, out _);
            _graficoTopProductos.Horizontal = true;
            tarjetaTop.Dock = DockStyle.Fill;
            tarjetaTop.Margin = new Padding(12, 0, 0, 0);
            fila.Controls.Add(tarjetaTop, 1, 0);
            _flpServicios.Controls.Add(fila);

            // ---- Pedidos según la hora (completa el alto de la pantalla)
            var tarjetaHoras = CrearTarjetaGrafico(T("Dashboard_tituloPedidosPorHora"), out _graficoHoras, out _);
            tarjetaHoras.Size = new Size(1424, AltoMinimoTarjetaDonut);
            tarjetaHoras.Margin = new Padding(3, 0, 3, 10);
            _rellenoServicios = tarjetaHoras;
            _flpServicios.Controls.Add(tarjetaHoras);

            // ---- Upgrades por tipo de habitación y tiempos de entrega del room service
            var tarjetaUpgrades = CrearTarjetaGrafico(T("Dashboard_tituloUpgradesPorTipo"), out _graficoUpgradesTipo, out _);
            _graficoUpgradesTipo.Horizontal = true;
            var tarjetaTiempos = CrearTarjetaGrafico(T("Dashboard_tituloTiemposEntrega"), out _graficoTiemposEntrega, out _);
            _flpServicios.Controls.Add(CrearFila(280, new Padding(3, 0, 3, 10), (tarjetaUpgrades, 50F), (tarjetaTiempos, 50F)));

            // ---- Cola de pedidos sin entregar y últimos cambios de habitación (con acceso a sus pantallas)
            var tarjetaPendientes = CrearTarjetaLista(T("Dashboard_tituloPedidosPendientes"), out _listaPendientes, out _lblTituloPendientes,
                T("Dashboard_btnVerPedidos"), SessionManager_08YS.Instance.HasPermission(Permisos.ConfirmarEntregaRoomService),
                () => AbrirPantalla(Permisos.ConfirmarEntregaRoomService, () => new FormPedidosRoomService_68SA()));
            var tarjetaCambios = CrearTarjetaLista(T("Dashboard_tituloCambiosHabitacion"), out _listaCambios, out _,
                T("Dashboard_btnVerBitacoraCambios"), SessionManager_08YS.Instance.HasPermission(Permisos.VerBitacoraReservas),
                () => AbrirPantalla(Permisos.VerBitacoraReservas, () => new FormBitacoraReservas_68SA()));
            _flpServicios.Controls.Add(CrearFila(320, new Padding(3, 0, 3, 3), (tarjetaPendientes, 50F), (tarjetaCambios, 50F)));

            _flpServicios.ResumeLayout(true);
        }

        private void ActualizarEstiloHistoricoServicios()
        {
            _btnHistoricoServicios.BackColor = _historicoServicios ? Color.Goldenrod : Color.FromArgb(5, 15, 45);
            _btnHistoricoServicios.ForeColor = _historicoServicios ? Color.FromArgb(10, 15, 35) : Color.Goldenrod;
            _btnHistoricoServicios.IconColor = _btnHistoricoServicios.ForeColor;
            _lblTituloFiltroServicios.Text = T(_historicoServicios ? "Dashboard_tituloServiciosHistorico" : "Dashboard_tituloServiciosPeriodo");
        }

        private bool PeriodoServicios(out DateTime? desde, out DateTime? hasta)
        {
            desde = _historicoServicios ? null : _dtpDesdeServicios.Value?.Date;
            hasta = _historicoServicios ? null : _dtpHastaServicios.Value?.Date;
            if (_historicoServicios) return true;
            if (!desde.HasValue || !hasta.HasValue) return false;
            if (desde.Value > hasta.Value)
            {
                MessageBox.Show(T("Dashboard_msgFiltroInvalido"), T("Reservas_tituloFiltroInvalido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ActualizarDatosServicios()
        {
            if (!PeriodoServicios(out var desde, out var hasta)) return;

            ResumenServicios_68SA resumen = _metricasBLL.GetResumenServicios(desde, hasta);
            PonerKpi(_kpisServicios["ingresos"], $"${resumen.Total:N2}",
                string.Format(T("Dashboard_kpiIngresosServiciosDetalle"), $"${resumen.MontoRoomService:N0}", $"${resumen.MontoUpgrades:N0}"));
            PonerKpi(_kpisServicios["pedidos"], resumen.Pedidos.ToString(),
                string.Format(T("Dashboard_kpiPedidosRSDetalle"), resumen.PedidosPendientes, $"${resumen.TicketPromedioPedido:N0}"));
            PonerKpi(_kpisServicios["upgrades"], resumen.Upgrades.ToString(),
                string.Format(T("Dashboard_kpiUpgradesDetalle"), resumen.Reubicaciones));
            PonerKpi(_kpisServicios["estadias"], $"{resumen.PorcentajeEstadiasConConsumo:0.#}%",
                string.Format(T("Dashboard_kpiEstadiasConConsumoDetalle"), resumen.EstadiasConConsumo, resumen.EstadiasPeriodo));
            PonerKpi(_kpisServicios["entrega"],
                resumen.MinutosEntregaPromedio.HasValue ? string.Format(T("RepServ_txtMinutos"), Math.Round(resumen.MinutosEntregaPromedio.Value)) : "—",
                T(resumen.MinutosEntregaPromedio.HasValue ? "RepServ_kpiEntregaDetalle" : "RepServ_kpiEntregaSinDatos"));

            var serie = MetricasBLL_68SA.Agrupar(_metricasBLL.GetServiciosPorDia(desde, hasta), out var agrupacion);
            _lblTituloServiciosDia.Text = T("Dashboard_tituloServiciosPorDia") + "  ·  " + T("Dashboard_agrupado" + agrupacion);
            _graficoServiciosDia.TextoVacio = T("Dashboard_txtSinServicios");
            _graficoServiciosDia.TextoTotal = T("Dashboard_txtTotal");
            _graficoServiciosDia.Cargar(Barras(serie, agrupacion, true), new[] { ColorRoomService, ColorUpgrade },
                new[] { OpcionEnum_68SA.Texto(TipoCargo.RoomService), OpcionEnum_68SA.Texto(TipoCargo.Upgrade) }, ValorCorto);

            var top = _metricasBLL.GetTopProductos(desde, hasta, 8);
            _graficoTopProductos.TextoVacio = T("RepServ_txtSinProductos");
            _graficoTopProductos.Cargar(top.Select(p => new BarraGrafico_68SA
                {
                    Etiqueta = p.Nombre,
                    Titulo = $"{p.Nombre}  ·  ${p.Monto:N0}",
                    Valores = new decimal[] { p.Cantidad }
                }).ToList(),
                new[] { Color.Gold }, new[] { T("Dashboard_serieUnidades") }, v => string.Format(T("RepServ_txtUnidades"), v.ToString("0")));

            int[] horas = _metricasBLL.GetPedidosPorHora(desde, hasta);
            _graficoHoras.TextoVacio = T("Dashboard_txtSinServicios");
            _graficoHoras.Cargar(Enumerable.Range(0, 24).Select(h => new BarraGrafico_68SA
                {
                    Etiqueta = h.ToString("00"),
                    Titulo = string.Format(T("Dashboard_txtFranjaHoraria"), h.ToString("00"), ((h + 1) % 24).ToString("00")),
                    Valores = new decimal[] { horas[h] }
                }).ToList(),
                new[] { ColorRoomService }, new[] { T("Dashboard_seriePedidos") }, v => v.ToString("0"));

            // Upgrades por tipo de la habitación nueva (monto cobrado; el globo muestra la cantidad)
            var upgrades = _metricasBLL.GetUpgradesPorTipo(desde, hasta);
            _graficoUpgradesTipo.TextoVacio = T("Dashboard_txtSinUpgrades");
            _graficoUpgradesTipo.Cargar(upgrades.Select(u => new BarraGrafico_68SA
                {
                    Etiqueta = u.Etiqueta,
                    Titulo = u.Etiqueta + "  ·  " + string.Format(T("Dashboard_txtCantidadUpgrades"), u.Cantidad),
                    Valores = new[] { u.Valor }
                }).ToList(),
                new[] { ColorUpgrade }, new[] { T("Dashboard_serieMonto") }, ValorCorto);

            // Pedidos entregados según cuánto tardaron
            int[] tramos = _metricasBLL.GetTiemposEntrega(desde, hasta);
            _graficoTiemposEntrega.TextoVacio = T("RepServ_kpiEntregaSinDatos");
            _graficoTiemposEntrega.Cargar(tramos.Sum() == 0 ? new List<BarraGrafico_68SA>() : Enumerable.Range(0, tramos.Length).Select(i => new BarraGrafico_68SA
                {
                    Etiqueta = T("Dashboard_tramoEntrega" + i),
                    Titulo = T("Dashboard_tramoEntrega" + i),
                    Valores = new decimal[] { tramos[i] }
                }).ToList(),
                new[] { Color.FromArgb(80, 200, 200) }, new[] { T("Dashboard_seriePedidos") }, v => v.ToString("0"));

            CargarListasServicios(desde, hasta);
        }

        private void CargarListasServicios(DateTime? desde, DateTime? hasta)
        {
            // La cola de pedidos es la de ahora (no depende del período elegido)
            var pendientes = _metricasBLL.GetPedidosPendientes();
            _lblTituloPendientes.Text = string.Format(T("Dashboard_tituloPedidosPendientesCantidad"), pendientes.Count);
            LlenarLista(_listaPendientes, pendientes.Select(p =>
            {
                int minutos = (int)Math.Max(0, (DateTime.Now - p.FechaHora).TotalMinutes);
                Color color = minutos > 30 ? ColorRojo : minutos > 15 ? ColorNaranja : ColorRoomService;
                return CrearFilaLista(string.Format(T("Dashboard_txtHabitacionNro"), p.NroHabitacion),
                    string.Format(T("Dashboard_txtPedidoDetalle"), p.PedidoID, p.Unidades, $"${p.Monto:N0}"),
                    TiempoTranscurrido(minutos), color);
            }), T("Dashboard_txtSinPendientes"));

            var cambios = _metricasBLL.GetCambiosHabitacion(desde, hasta, 10);
            LlenarLista(_listaCambios, cambios.Select(c => CrearFilaLista(
                $"{c.HabitacionAnterior} → {c.HabitacionNueva}  ·  {c.TipoNuevo}",
                string.Format(T("Dashboard_txtCambioDetalle"), c.FechaCambio.ToString("dd/MM/yyyy HH:mm"), c.ReservaID),
                c.Motivo == 0 ? $"${c.Monto:N0}" : T("Dashboard_txtReubicacion"),
                c.Motivo == 0 ? ColorUpgrade : ColorAzul)), T("Dashboard_txtSinCambios"));
        }

        private static string TiempoTranscurrido(int minutos)
            => minutos < 60
                ? string.Format(T("Dashboard_txtHaceMinutos"), minutos)
                : string.Format(T("Dashboard_txtHaceHoras"), minutos / 60, minutos % 60);

        /// <summary>Abre otra pantalla del sistema dentro del MDI (con su permiso) desde el Dashboard.</summary>
        private void AbrirPantalla(Permisos permiso, Func<Form> crear)
        {
            SessionManager_08YS.Instance.ValidatePermission(permiso);
            if (ParentForm is FormMDI_08YS mdi)
                mdi.OpenChildForm(crear());
            else
                crear().Show(this);
        }

        private static void PonerKpi((Label Valor, Label Detalle) kpi, string valor, string detalle)
        {
            kpi.Valor.Text = valor;
            kpi.Detalle.Text = detalle;
        }

        /// <summary>Abre el reporte de servicios con el período elegido en la pestaña.</summary>
        private void AbrirReporteServicios()
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.GenerarReporteServicios);
            if (!PeriodoServicios(out var desde, out var hasta)) return;

            AbrirEnReportes(Reportes.TipoReporte_68SA.Servicios, desde, hasta);
        }

        #endregion

        #region Gráficos e indicadores reutilizables

        /// <summary>Tarjeta con título arriba y un gráfico de barras que ocupa el resto.</summary>
        private static UserControls.RoundedPanel_68SA CrearTarjetaGrafico(string titulo, out UC_BarChart_68SA grafico, out Label lblTitulo)
        {
            var panel = new UserControls.RoundedPanel_68SA
            {
                BackColor = Color.FromArgb(10, 18, 50),
                CornerRadius = 12,
                Padding = new Padding(16, 46, 18, 12),
                Size = new Size(700, AltoMinimoTarjetaDonut)
            };
            grafico = new UC_BarChart_68SA { Dock = DockStyle.Fill, BackColor = Color.FromArgb(10, 18, 50) };
            panel.Controls.Add(grafico);
            lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Location = new Point(16, 14),
                AutoSize = true
            };
            panel.Controls.Add(lblTitulo);
            lblTitulo.BringToFront();
            return panel;
        }

        /// <summary>Fila de tarjetas KPI con título, valor, detalle e ícono (columnas iguales).</summary>
        private static TableLayoutPanel CrearFilaKpis((string Titulo, IconChar Icono, Color Color, string Valor, string Detalle)[] kpis,
            out List<(Label Valor, Label Detalle)> etiquetas)
        {
            var fila = new TableLayoutPanel
            {
                ColumnCount = kpis.Length,
                RowCount = 1,
                Size = new Size(1424, 116),
                Margin = new Padding(3, 0, 3, 10),
                Padding = Padding.Empty
            };
            for (int i = 0; i < kpis.Length; i++)
                fila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / kpis.Length));
            fila.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            etiquetas = new List<(Label Valor, Label Detalle)>();
            for (int i = 0; i < kpis.Length; i++)
            {
                var k = kpis[i];
                var panel = new UserControls.RoundedPanel_68SA
                {
                    BackColor = Color.FromArgb(10, 18, 50),
                    CornerRadius = 12,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(i == 0 ? 3 : 6, 3, i == kpis.Length - 1 ? 3 : 6, 3)
                };
                var icono = new IconPictureBox { IconChar = k.Icono, IconColor = k.Color, IconSize = 26, Size = new Size(30, 30), BackColor = Color.FromArgb(10, 18, 50) };
                var lblTitulo = new Label
                {
                    Text = k.Titulo.ToUpper(), AutoEllipsis = true, Location = new Point(16, 14), Size = new Size(200, 18),
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold), ForeColor = Color.FromArgb(160, 165, 180)
                };
                var lblValor = new Label
                {
                    Text = k.Valor, AutoEllipsis = true, Location = new Point(14, 34), Size = new Size(200, 36),
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.WhiteSmoke
                };
                var lblDetalle = new Label
                {
                    Text = k.Detalle, AutoEllipsis = true, Location = new Point(16, 74), Size = new Size(200, 18),
                    Font = new Font("Segoe UI", 8.25F), ForeColor = Color.FromArgb(150, 156, 175)
                };
                var barra = new Panel { BackColor = k.Color, Dock = DockStyle.Left, Width = 4 };
                panel.Controls.Add(icono);
                panel.Controls.Add(lblTitulo);
                panel.Controls.Add(lblValor);
                panel.Controls.Add(lblDetalle);
                panel.Controls.Add(barra);
                panel.Resize += (s, e) =>
                {
                    icono.Location = new Point(Math.Max(16, panel.ClientSize.Width - icono.Width - 14), 14);
                    int ancho = Math.Max(40, panel.ClientSize.Width - 30);
                    lblTitulo.Width = Math.Max(20, ancho - icono.Width - 6);
                    lblValor.Width = ancho;
                    lblDetalle.Width = ancho;
                };
                LayoutAdaptable_68SA.AjustarValorTarjeta(panel, lblValor, icono);
                fila.Controls.Add(panel, i, 0);
                etiquetas.Add((lblValor, lblDetalle));
            }
            return fila;
        }

        /// <summary>Fila de tarjetas lado a lado que se reparten el ancho según el porcentaje de cada una.</summary>
        private static TableLayoutPanel CrearFila(int alto, Padding margen, params (Control Tarjeta, float Porcentaje)[] celdas)
        {
            var fila = new TableLayoutPanel
            {
                ColumnCount = celdas.Length,
                RowCount = 1,
                Size = new Size(1424, alto),
                Margin = margen,
                Padding = Padding.Empty
            };
            fila.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            for (int i = 0; i < celdas.Length; i++)
            {
                fila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, celdas[i].Porcentaje));
                var tarjeta = celdas[i].Tarjeta;
                tarjeta.Dock = DockStyle.Fill;
                tarjeta.Margin = new Padding(i == 0 ? 0 : 12, 0, i == celdas.Length - 1 ? 0 : 12, 0);
                fila.Controls.Add(tarjeta, i, 0);
            }
            return fila;
        }

        /// <summary>
        /// Tarjeta con título y una lista de filas (con barra si no entran). Opcionalmente, un botón
        /// arriba a la derecha que lleva a la pantalla donde se gestiona lo que muestra la lista.
        /// </summary>
        private static UserControls.RoundedPanel_68SA CrearTarjetaLista(string titulo, out FlowLayoutPanel lista, out Label lblTitulo,
            string textoAccion, bool accionVisible, Action accion)
        {
            var panel = new UserControls.RoundedPanel_68SA
            {
                BackColor = Color.FromArgb(10, 18, 50),
                CornerRadius = 12,
                Padding = new Padding(16, 50, 16, 12),
                Size = new Size(700, 300)
            };
            lista = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = panel.BackColor,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            panel.Controls.Add(lista);
            LayoutAdaptable_68SA.EstirarHijosAlAncho(lista);

            // Variable local: el parámetro out no se puede usar dentro de la función local de abajo
            var etiquetaTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Location = new Point(16, 14),
                AutoSize = true,
                UseMnemonic = false
            };
            lblTitulo = etiquetaTitulo;
            panel.Controls.Add(etiquetaTitulo);
            etiquetaTitulo.BringToFront();

            if (accionVisible && accion != null)
            {
                var boton = new IconButton
                {
                    BackColor = Color.FromArgb(5, 15, 45),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.Goldenrod,
                    IconChar = IconChar.ArrowUpRightFromSquare,
                    IconColor = Color.Goldenrod,
                    IconSize = 14,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(8, 0, 6, 0),
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    MinimumSize = new Size(0, 30),
                    Text = textoAccion,
                    TextAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    UseVisualStyleBackColor = false,
                    Cursor = Cursors.Hand
                };
                boton.FlatAppearance.BorderColor = Color.Goldenrod;
                boton.Click += (s, e) => accion();
                panel.Controls.Add(boton);
                boton.BringToFront();
                void Ubicar() => boton.Location = new Point(Math.Max(etiquetaTitulo.Right + 10, panel.ClientSize.Width - boton.Width - 14), 9);
                panel.Resize += (s, e) => Ubicar();
                boton.SizeChanged += (s, e) => Ubicar();
                etiquetaTitulo.SizeChanged += (s, e) => Ubicar();
                Ubicar();
            }
            return panel;
        }

        /// <summary>Fila de una lista: punto de color, texto principal, detalle debajo y valor a la derecha.</summary>
        private static Panel CrearFilaLista(string texto, string detalle, string valor, Color color)
        {
            const int alto = 46, anchoValor = 130;
            var fila = new Panel { Size = new Size(400, alto), Margin = new Padding(0, 0, 0, 4), BackColor = Color.FromArgb(14, 24, 60) };
            var barra = new Panel { BackColor = color, Dock = DockStyle.Left, Width = 4 };
            var lblTexto = new Label
            {
                Text = texto, Font = FuenteFilaLista, ForeColor = Color.WhiteSmoke, AutoEllipsis = true, UseMnemonic = false,
                Location = new Point(14, 5), Size = new Size(fila.Width - 14 - anchoValor - 8, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var lblDetalle = new Label
            {
                Text = detalle, Font = FuenteDetalleLista, ForeColor = ColorTextoSecundario, AutoEllipsis = true, UseMnemonic = false,
                Location = new Point(14, 25), Size = new Size(fila.Width - 14 - anchoValor - 8, 17),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var lblValor = new Label
            {
                Text = valor, Font = FuenteValorLista, ForeColor = color, AutoEllipsis = true, UseMnemonic = false,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(fila.Width - anchoValor - 10, 0), Size = new Size(anchoValor, alto),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            fila.Controls.Add(lblTexto);
            fila.Controls.Add(lblDetalle);
            fila.Controls.Add(lblValor);
            fila.Controls.Add(barra);
            return fila;
        }

        private static void LlenarLista(FlowLayoutPanel lista, IEnumerable<Control> filas, string textoVacio)
        {
            lista.SuspendLayout();
            try
            {
                LimpiarContenedor(lista);
                foreach (var fila in filas) lista.Controls.Add(fila);
                if (lista.Controls.Count == 0)
                    lista.Controls.Add(new Label
                    {
                        Text = textoVacio, Font = FuenteDetalleLista, ForeColor = ColorTextoSecundario,
                        Size = new Size(400, 30), TextAlign = ContentAlignment.MiddleLeft, UseMnemonic = false
                    });
            }
            finally
            {
                lista.ResumeLayout(true);
            }
        }

        /// <summary>Barras del gráfico a partir de una serie por fecha (con la etiqueta según la agrupación).</summary>
        private static List<BarraGrafico_68SA> Barras(List<PuntoSerie_68SA> serie, AgrupacionSerie agrupacion, bool dosSeries)
        {
            var cultura = OpcionEnum_68SA.CulturaActual();
            return serie.Select(p => new BarraGrafico_68SA
            {
                Etiqueta = agrupacion == AgrupacionSerie.Mes ? p.Fecha.ToString("MMM yy", cultura) : p.Fecha.ToString("dd/MM"),
                Titulo = agrupacion == AgrupacionSerie.Mes ? p.Fecha.ToString("MMMM yyyy", cultura)
                       : agrupacion == AgrupacionSerie.Semana ? string.Format(T("Dashboard_txtSemanaDel"), p.Fecha.ToString("dd/MM/yyyy"))
                       : p.Fecha.ToString("dddd dd/MM/yyyy", cultura),
                Valores = dosSeries ? new[] { p.Valor, p.Valor2 } : new[] { p.Valor }
            }).ToList();
        }

        /// <summary>Montos abreviados para los ejes ($850, $12,5k, $1,2M).</summary>
        private static string ValorCorto(decimal valor)
        {
            var cultura = OpcionEnum_68SA.CulturaActual();
            if (valor >= 1000000) return "$" + (valor / 1000000).ToString("0.#", cultura) + "M";
            if (valor >= 1000) return "$" + (valor / 1000).ToString("0.#", cultura) + "k";
            return "$" + valor.ToString("0", cultura);
        }

        #endregion

        #region Sobrescrituras

        // Pinta el formulario completo en memoria y lo vuelca de una vez: sin parpadeo ni franjas
        // blancas mientras los controles se acomodan al abrir o redimensionar la ventana.
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
