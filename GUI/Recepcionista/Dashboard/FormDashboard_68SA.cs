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

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        public FormDashboard_68SA()
        {
            InitializeComponent();

            dtpDesdeGeneral.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpHastaGeneral.Value = DateTime.Today;

            btnTabGeneral.Click += (s, e) => CambiarTab(0);
            btnTabOcupacion.Click += (s, e) => CambiarTab(1);
            btnTabHuespedes.Click += (s, e) => CambiarTab(2);

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
            LayoutAdaptable_68SA.EstirarHijosAlAncho(flpGeneral, 0, pnlOrigenGasto, AltoMinimoTarjetaDonut);
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
                LimpiarContenedor(flpHuespedes);
                _huespedesInicializado = false;
            }

            // La primera traducción llega antes de mostrar el formulario; los datos se cargan en Shown
            if (!Visible) return;

            if (flpGeneral.Visible) CargarGeneral();
            else if (flpOcupacion.Visible) CargarOcupacion();
            else if (flpHuespedes.Visible) CargarHuespedes();
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

            EstilizarTab(btnTabGeneral, indice == 0);
            EstilizarTab(btnTabOcupacion, indice == 1);
            EstilizarTab(btnTabHuespedes, indice == 2);

            if (indice == 0) CargarGeneral();
            if (indice == 1) CargarOcupacion();
            if (indice == 2) CargarHuespedes();
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
        }

        private void CargarDonutOrigenGasto(ResumenIngresos_68SA resumen)
        {
            // El origen llega de la base con el texto en español: se usa como clave de color y de traducción
            var coloresPorOrigen = new Dictionary<string, Color>
            {
                { "Reserva", Color.Goldenrod },
                { "Renovación", Color.FromArgb(90, 155, 235) },
                { "Servicio a la Habitación", Color.FromArgb(76, 217, 100) },
                { "Cambio de Habitación", Color.FromArgb(160, 165, 180) }
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

            var filtro = new ReservaFiltro_68SA { FechaDesde = desde, FechaHasta = hasta };
            var reservas = _reservaBLL.GetTodas(filtro);

            new ReporteReservasImpresor_68SA(reservas, desde, hasta).Imprimir();
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

                // Dos tarjetas lado a lado que se reparten el ancho en partes iguales
                var filaDonuts = new TableLayoutPanel
                {
                    ColumnCount = 2,
                    RowCount = 1,
                    Size = new Size(1424, AltoMinimoTarjetaDonut),
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

                _rellenoOcupacion = filaDonuts;
                flpOcupacion.Controls.Add(filaDonuts);

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

            // ---- Donut de nacionalidad ----
            var panelNacionalidad = CrearTarjetaDonut(T("Dashboard_tituloNacionalidad"), new Size(1424, AltoMinimoTarjetaDonut), out _donutNacionalidad, out _leyendaNacionalidad);
            panelNacionalidad.Margin = new Padding(3, 0, 3, 3);
            _rellenoHuespedes = panelNacionalidad;
            flpHuespedes.Controls.Add(panelNacionalidad);
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
        }

        #endregion
    

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
    }
}
