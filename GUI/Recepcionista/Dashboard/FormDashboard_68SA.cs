using BE_08YS;
using BE_08YS.Metricas;
using BLL_08YS;
using BLL_08YS.Negocio;
using GUI_08YS.Recepcionista.Dashboard;
using Service_08YS;
using CustomControls;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public partial class FormDashboard_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos y constructor

        private readonly MetricasBLL_68SA _metricasBLL = BLLFactory_08YS.CreateMetricasBLL();
        private bool _historicoTotal = false;
        private bool _huespedesInicializado = false;
        private bool _historicoTotalHuespedes = false;
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

            this.Shown += (s, e) => CargarGeneral();
        }

        public void UpdateIdioma()
        {
            // Pendiente junto con el resto de las traducciones de estos forms.
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

        #region Helpers compartidos (leyendas de donuts)

        private static Panel CrearFilaLeyenda(string etiqueta, string valorFormateado, decimal porcentaje, Color color)
        {
            var fila = new Panel { Size = new Size(380, 36), Margin = new Padding(0, 0, 0, 6) };

            var punto = new UserControls.RoundedPanel_68SA
            {
                Size = new Size(12, 12),
                Location = new Point(0, 12),
                BackColor = color,
                CornerRadius = 6
            };
            fila.Controls.Add(punto);

            var lblEtiqueta = new Label
            {
                Text = etiqueta,
                Location = new Point(24, 8),
                Size = new Size(180, 20),
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI", 9.5F)
            };
            fila.Controls.Add(lblEtiqueta);

            var lblValor = new Label
            {
                Text = $"{valorFormateado}  ({porcentaje:0.#}%)",
                Location = new Point(210, 8),
                Size = new Size(170, 20),
                ForeColor = Color.FromArgb(160, 165, 180),
                Font = new Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleRight
            };
            fila.Controls.Add(lblValor);

            return fila;
        }

        private static void PoblarLeyenda(FlowLayoutPanel contenedor, List<SegmentoDonut_68SA> segmentos, Func<decimal, string> formatearValor)
        {
            contenedor.Controls.Clear();
            decimal total = 0;
            foreach (var s in segmentos) total += s.Valor;

            foreach (var segmento in segmentos)
            {
                decimal porcentaje = total > 0 ? segmento.Valor / total * 100 : 0;
                contenedor.Controls.Add(CrearFilaLeyenda(segmento.Etiqueta, formatearValor(segmento.Valor), porcentaje, segmento.Color));
            }
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

            lblValorDisponibles.Text = resumen.Disponibles.ToString();
            lblSubDisponibles.Text = $"de {resumen.Total} hab.";

            lblValorOcupadasDash.Text = resumen.Ocupadas.ToString();
            lblSubOcupadasDash.Text = $"de {resumen.Total} hab.";

            lblValorLimpieza.Text = resumen.EnLimpieza.ToString();
            lblSubLimpieza.Text = $"de {resumen.Total} hab.";

            lblValorMantenimiento.Text = resumen.FueraDeServicio.ToString();
            lblSubMantenimiento.Text = $"de {resumen.Total} hab.";

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
            lblTituloFiltroGeneral.Text = _historicoTotal ? "INGRESOS — HISTÓRICO TOTAL" : "INGRESOS DEL PERÍODO";
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
                    MessageBox.Show("La fecha Desde no puede ser posterior a Hasta.", "Filtro inválido",
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
            var coloresPorOrigen = new Dictionary<string, Color>
            {
                { "Reserva", Color.Goldenrod },
                { "Renovación", Color.FromArgb(90, 155, 235) },
                { "Servicio a la Habitación", Color.FromArgb(76, 217, 100) },
                { "Cambio de Habitación", Color.FromArgb(160, 165, 180) }
            };

            var segmentos = new List<SegmentoDonut_68SA>();
            decimal total = 0;
            foreach (var item in resumen.OrigenDelGasto)
            {
                total += item.Monto;
                segmentos.Add(new SegmentoDonut_68SA
                {
                    Etiqueta = item.Origen,
                    Valor = item.Monto,
                    Color = coloresPorOrigen.TryGetValue(item.Origen, out var color) ? color : Color.Gray
                });
            }

            ucDonutOrigenGasto.Cargar(segmentos, $"${total:N0}", "Total");
            PoblarLeyenda(flpLeyendaOrigenGasto, segmentos, v => $"${v:N2}");
        }

        private static Panel CrearFilaLeyenda(string etiqueta, decimal monto, decimal porcentaje, Color color)
        {
            var fila = new Panel { Size = new Size(380, 36), Margin = new Padding(0, 0, 0, 6) };

            var punto = new UserControls.RoundedPanel_68SA
            {
                Size = new Size(12, 12),
                Location = new Point(0, 12),
                BackColor = color,
                CornerRadius = 6
            };
            fila.Controls.Add(punto);

            var lblEtiqueta = new Label
            {
                Text = etiqueta,
                Location = new Point(24, 8),
                Size = new Size(220, 20),
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI", 9.5F)
            };
            fila.Controls.Add(lblEtiqueta);

            var lblValor = new Label
            {
                Text = $"${monto:N2}  ({porcentaje:0.#}%)",
                Location = new Point(250, 8),
                Size = new Size(130, 20),
                ForeColor = Color.FromArgb(160, 165, 180),
                Font = new Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleRight
            };
            fila.Controls.Add(lblValor);

            return fila;
        }

        #endregion

        #region Pestaña Ocupación

        private void CargarOcupacion()
        {
            flpOcupacion.Controls.Clear();

            ResumenOcupacion_68SA resumenEstado = _metricasBLL.GetResumenOcupacion();
            List<OcupacionPorTipoItem_68SA> porTipo = _metricasBLL.GetOcupacionPorTipo();

            var filaDonuts = new Panel { Size = new Size(1424, 340), Margin = new Padding(3, 3, 3, 10) };

            var panelEstado = CrearTarjetaDonut("OCUPACIÓN POR ESTADO", new Size(700, 340), out var donutEstado, out var leyendaEstado);
            panelEstado.Location = new Point(0, 0);
            filaDonuts.Controls.Add(panelEstado);

            var panelTipo = CrearTarjetaDonut("OCUPACIÓN POR TIPO DE HABITACIÓN", new Size(700, 340), out var donutTipo, out var leyendaTipo);
            panelTipo.Location = new Point(724, 0);
            filaDonuts.Controls.Add(panelTipo);

            flpOcupacion.Controls.Add(filaDonuts);

            var segmentosEstado = new List<SegmentoDonut_68SA>
            {
                new SegmentoDonut_68SA { Etiqueta = "Disponible", Valor = resumenEstado.Disponibles, Color = Color.FromArgb(76, 217, 100) },
                new SegmentoDonut_68SA { Etiqueta = "Reservada", Valor = resumenEstado.Reservadas, Color = Color.MediumPurple },
                new SegmentoDonut_68SA { Etiqueta = "Ocupada", Valor = resumenEstado.Ocupadas, Color = Color.Goldenrod },
                new SegmentoDonut_68SA { Etiqueta = "En Limpieza", Valor = resumenEstado.EnLimpieza, Color = Color.FromArgb(90, 155, 235) },
                new SegmentoDonut_68SA { Etiqueta = "Mantenimiento", Valor = resumenEstado.FueraDeServicio, Color = Color.FromArgb(235, 140, 60) }
            };
            donutEstado.Cargar(segmentosEstado, resumenEstado.Total.ToString(), "Habitaciones");
            PoblarLeyenda(leyendaEstado, segmentosEstado, v => v.ToString("0"));

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
            donutTipo.Cargar(segmentosTipo, totalOcupadasPorTipo.ToString(), "Ocupadas");
            PoblarLeyenda(leyendaTipo, segmentosTipo, v => v.ToString("0"));

            var panelEstadia = new UserControls.RoundedPanel_68SA
            {
                BackColor = Color.FromArgb(10, 18, 50),
                CornerRadius = 12,
                Size = new Size(1424, 100),
                Margin = new Padding(3, 0, 3, 3)
            };
            panelEstadia.Controls.Add(new Label
            {
                Text = "DURACIÓN PROMEDIO DE ESTADÍA",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(160, 165, 180),
                Location = new Point(16, 14),
                AutoSize = true
            });
            panelEstadia.Controls.Add(new Label
            {
                Text = $"{resumenEstado.DuracionPromedioEstadiaHoras:0.#} horas",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Location = new Point(16, 34),
                AutoSize = true
            });
            flpOcupacion.Controls.Add(panelEstadia);
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

            leyenda = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Location = new Point(290, 60),
                Size = new Size(tamano.Width - 310, tamano.Height - 80),
                WrapContents = false
            };
            panel.Controls.Add(leyenda);

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
            // ---- Panel de filtro (mismo patrón que en General) ----
            var pnlFiltro = new UserControls.RoundedPanel_68SA
            {
                BackColor = Color.FromArgb(10, 18, 50),
                CornerRadius = 12,
                Size = new Size(1424, 112),
                Margin = new Padding(3, 3, 3, 10)
            };

            _lblTituloFiltroHuespedes = new Label
            {
                Text = "HUÉSPEDES DEL PERÍODO",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Location = new Point(16, 10),
                AutoSize = true
            };
            pnlFiltro.Controls.Add(_lblTituloFiltroHuespedes);

            pnlFiltro.Controls.Add(new Label
            {
                Text = "Desde",
                ForeColor = Color.FromArgb(160, 165, 180),
                Location = new Point(16, 70),
                AutoSize = true
            });

            _dtpDesdeHuespedes = new IconDateTimePicker
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
                Location = new Point(70, 58),
                Size = new Size(220, 40),
                Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
            };
            pnlFiltro.Controls.Add(_dtpDesdeHuespedes);

            pnlFiltro.Controls.Add(new Label
            {
                Text = "Hasta",
                ForeColor = Color.FromArgb(160, 165, 180),
                Location = new Point(310, 70),
                AutoSize = true
            });

            _dtpHastaHuespedes = new IconDateTimePicker
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
                Location = new Point(364, 58),
                Size = new Size(220, 40),
                Value = DateTime.Today
            };
            pnlFiltro.Controls.Add(_dtpHastaHuespedes);

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
                Location = new Point(604, 56),
                Padding = new Padding(14, 0, 0, 0),
                Size = new Size(170, 44),
                Text = "Actualizar",
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
            pnlFiltro.Controls.Add(btnActualizar);

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

            var btnHoy = CrearBotonPreset("Hoy", new Point(678, 8), 130);
            btnHoy.Click += (s, e) => AplicarPresetHuespedes(DateTime.Today, DateTime.Today);
            pnlFiltro.Controls.Add(btnHoy);

            var btn7Dias = CrearBotonPreset("Últimos 7 días", new Point(818, 8), 140);
            btn7Dias.Click += (s, e) => AplicarPresetHuespedes(DateTime.Today.AddDays(-7), DateTime.Today);
            pnlFiltro.Controls.Add(btn7Dias);

            var btn30Dias = CrearBotonPreset("Últimos 30 días", new Point(968, 8), 140);
            btn30Dias.Click += (s, e) => AplicarPresetHuespedes(DateTime.Today.AddDays(-30), DateTime.Today);
            pnlFiltro.Controls.Add(btn30Dias);

            var btnEsteMes = CrearBotonPreset("Este Mes", new Point(1118, 8), 130);
            btnEsteMes.Click += (s, e) => AplicarPresetHuespedes(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);
            pnlFiltro.Controls.Add(btnEsteMes);

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
                Location = new Point(1258, 8),
                Padding = new Padding(10, 0, 0, 0),
                Size = new Size(150, 36),
                Text = "Histórico Total",
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
            pnlFiltro.Controls.Add(_btnHistoricoHuespedes);

            flpHuespedes.Controls.Add(pnlFiltro);

            // ---- Tarjetas KPI ----
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

            var cardNuevos = CrearTarjetaKpi("HUÉSPEDES NUEVOS", IconChar.UserPlus, Color.FromArgb(76, 217, 100), out _lblValorNuevos);
            cardNuevos.Dock = DockStyle.Fill;
            cardNuevos.Margin = new Padding(3, 3, 12, 3);
            filaCards.Controls.Add(cardNuevos, 0, 0);

            var cardRecurrentes = CrearTarjetaKpi("HUÉSPEDES RECURRENTES", IconChar.UserClock, Color.FromArgb(90, 155, 235), out _lblValorRecurrentes);
            cardRecurrentes.Dock = DockStyle.Fill;
            cardRecurrentes.Margin = new Padding(3, 3, 12, 3);
            filaCards.Controls.Add(cardRecurrentes, 1, 0);

            var cardTasa = CrearTarjetaKpi("TASA DE RECURRENCIA", IconChar.ArrowRotateRight, Color.Goldenrod, out _lblValorTasaRecurrencia);
            cardTasa.Dock = DockStyle.Fill;
            cardTasa.Margin = new Padding(3, 3, 12, 3);
            filaCards.Controls.Add(cardTasa, 2, 0);

            var cardGasto = CrearTarjetaKpi("GASTO PROMEDIO", IconChar.Wallet, Color.FromArgb(235, 140, 60), out _lblValorGastoPromedio);
            cardGasto.Dock = DockStyle.Fill;
            cardGasto.Margin = new Padding(3, 3, 3, 3);
            filaCards.Controls.Add(cardGasto, 3, 0);

            flpHuespedes.Controls.Add(filaCards);

            // ---- Donut de nacionalidad ----
            var panelNacionalidad = CrearTarjetaDonut("HUÉSPEDES POR NACIONALIDAD", new Size(1424, 340), out _donutNacionalidad, out _leyendaNacionalidad);
            panelNacionalidad.Margin = new Padding(3, 0, 3, 3);
            flpHuespedes.Controls.Add(panelNacionalidad);
        }

        private static IconButton CrearBotonPreset(string texto, Point ubicacion, int ancho)
        {
            return new IconButton
            {
                BackColor = Color.FromArgb(5, 15, 45),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Goldenrod,
                Location = ubicacion,
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

            panel.Controls.Add(new IconPictureBox
            {
                IconChar = icono,
                IconColor = color,
                IconSize = 28,
                Location = new Point(288, 32),
                Size = new Size(36, 36)
            });

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
            _lblTituloFiltroHuespedes.Text = _historicoTotalHuespedes ? "HUÉSPEDES — HISTÓRICO TOTAL" : "HUÉSPEDES DEL PERÍODO";
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
                    MessageBox.Show("La fecha Desde no puede ser posterior a Hasta.", "Filtro inválido",
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
            _donutNacionalidad.Cargar(segmentos, resumen.TotalAtendidos.ToString(), "Huéspedes");
            PoblarLeyenda(_leyendaNacionalidad, segmentos, v => v.ToString("0"));
        }

        #endregion
    }
}