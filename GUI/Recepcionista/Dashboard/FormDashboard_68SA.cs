using BE_08YS;
using BE_08YS.Metricas;
using BLL_08YS;
using BLL_08YS.Negocio;
using GUI_08YS.Recepcionista.Dashboard;
using Service_08YS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista.Dashboard
{
    public partial class FormDashboard_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos y constructor

        private readonly MetricasBLL_68SA _metricasBLL = BLLFactory_08YS.CreateMetricasBLL();

        public FormDashboard_68SA()
        {
            InitializeComponent();

            dtpDesdeGeneral.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpHastaGeneral.Value = DateTime.Today;

            btnTabGeneral.Click += (s, e) => CambiarTab(0);
            btnTabOcupacion.Click += (s, e) => CambiarTab(1);
            btnTabHuespedes.Click += (s, e) => CambiarTab(2);

            btnActualizarGeneral.Click += (s, e) => CargarGeneral();

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
            // Ocupación e Huéspedes se cargan cuando se implementen sus pestañas.
        }

        private static void EstilizarTab(FontAwesome.Sharp.IconButton boton, bool activo)
        {
            boton.BackColor = activo ? Color.FromArgb(10, 18, 50) : Color.FromArgb(5, 10, 40);
            boton.ForeColor = activo ? Color.Goldenrod : Color.FromArgb(160, 165, 180);
            boton.IconColor = boton.ForeColor;
            boton.Font = new Font("Segoe UI", 10F, activo ? FontStyle.Bold : FontStyle.Regular);
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

            lblValorTasaOcupacion.Text = $"{resumen.TasaOcupacionPorcentaje:0.##}%";
        }

        private void CargarTarjetasIngresosGeneral()
        {
            if (!dtpDesdeGeneral.Value.HasValue || !dtpHastaGeneral.Value.HasValue) return;

            DateTime desde = dtpDesdeGeneral.Value.Value.Date;
            DateTime hasta = dtpHastaGeneral.Value.Value.Date;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha Desde no puede ser posterior a Hasta.", "Filtro inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
            var coloresPorOrigen = new System.Collections.Generic.Dictionary<string, Color>
            {
                { "Reserva", Color.Goldenrod },
                { "Renovación", Color.FromArgb(90, 155, 235) },
                { "Servicio a la Habitación", Color.FromArgb(76, 217, 100) },
                { "Cambio de Habitación", Color.FromArgb(160, 165, 180) }
            };

            var segmentos = new System.Collections.Generic.List<SegmentoDonut_68SA>();
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

            flpLeyendaOrigenGasto.Controls.Clear();
            foreach (var segmento in segmentos)
            {
                decimal porcentaje = total > 0 ? segmento.Valor / total * 100 : 0;
                flpLeyendaOrigenGasto.Controls.Add(CrearFilaLeyenda(segmento.Etiqueta, segmento.Valor, porcentaje, segmento.Color));
            }
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

        // Pendiente: donut por estado de habitación + donut por tipo de habitación.

        #endregion

        #region Pestaña Huéspedes

        // Pendiente: KPIs de nuevos/recurrentes/tasa de recurrencia/gasto promedio + donut por nacionalidad.

        #endregion
    }
}