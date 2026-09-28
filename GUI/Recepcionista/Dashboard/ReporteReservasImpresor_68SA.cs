using BE_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public class ReporteReservasImpresor_68SA
    {
        private readonly List<Reserva_68SA> _reservas;
        private readonly DateTime? _desde;
        private readonly DateTime? _hasta;

        private int _filaActual = 0;

        public ReporteReservasImpresor_68SA(List<Reserva_68SA> reservas, DateTime? desde, DateTime? hasta)
        {
            _reservas = reservas;
            _desde = desde;
            _hasta = hasta;
        }

        public void Imprimir()
        {
            if (_reservas.Count == 0)
            {
                MessageBox.Show("No hay reservas para el período seleccionado.", "Sin datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _filaActual = 0;

            var pd = new PrintDocument();
            pd.DocumentName = "Reporte de Reservas de Habitaciones";
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += Pd_PrintPage;

            var printDialog = new PrintDialog { Document = pd };
            if (printDialog.ShowDialog() == DialogResult.OK)
                pd.Print();
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Font fuenteTitulo = new Font("Arial", 15, FontStyle.Bold);
            Font fuenteInfo = new Font("Arial", 9, FontStyle.Italic);
            Font fuenteEncabezado = new Font("Arial", 9, FontStyle.Bold);
            Font fuenteCuerpo = new Font("Arial", 8.5F, FontStyle.Regular);

            Brush pincelNegro = Brushes.Black;
            Pen lapizGris = new Pen(Color.LightGray, 1);

            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            int anchoTotal = e.MarginBounds.Width;

            if (_filaActual == 0)
            {
                g.DrawString("HORIZON HOTEL & RESORT — REPORTE DE RESERVAS DE HABITACIONES", fuenteTitulo, pincelNegro, x, y);
                y += 26;

                string periodo = _desde.HasValue && _hasta.HasValue
                    ? $"Período: {_desde.Value:dd/MM/yyyy} al {_hasta.Value:dd/MM/yyyy}"
                    : "Período: histórico total";
                g.DrawString(periodo, fuenteInfo, pincelNegro, x, y);
                g.DrawString($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fuenteInfo, pincelNegro, x + anchoTotal - 260, y);
                y += 30;
            }

            int[] pesos = { 2, 5, 3, 3, 3, 4, 2, 3 }; // ID, Huésped, DNI, F.Ingreso, F.Egreso, Tipo, N°Hab, Estado
            int pesoTotal = 25;
            int[] anchos = new int[pesos.Length];
            for (int i = 0; i < pesos.Length; i++)
                anchos[i] = anchoTotal * pesos[i] / pesoTotal;

            string[] encabezados = { "ID", "Huésped", "DNI", "F. Ingreso", "F. Egreso", "Tipo Hab.", "N° Hab.", "Estado" };
            y = DibujarFilaTabla(g, x, y, anchos, encabezados, fuenteEncabezado, true);

            while (_filaActual < _reservas.Count)
            {
                var reserva = _reservas[_filaActual];
                string[] datosFila =
                {
                    reserva.Id.ToString(),
                    $"{reserva.Titular.Nombre} {reserva.Titular.Apellido}",
                    reserva.Titular.Documento,
                    reserva.FechaIngreso.ToString("dd/MM/yyyy"),
                    reserva.FechaEgreso.ToString("dd/MM/yyyy"),
                    reserva.Habitacion.Tipo?.Nombre ?? "-",
                    reserva.Habitacion.NroHabitacion,
                    TextoEstado(reserva.Estado)
                };

                y = DibujarFilaTabla(g, x, y, anchos, datosFila, fuenteCuerpo, false, lapizGris);
                _filaActual++;

                if (y > e.MarginBounds.Bottom - 40 && _filaActual < _reservas.Count)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            y += 10;
            g.DrawString($"Total de reservas: {_reservas.Count}", new Font("Arial", 9, FontStyle.Bold), pincelNegro, x, y);

            e.HasMorePages = false;
        }

        private static string TextoEstado(EstadoReserva estado)
        {
            switch (estado)
            {
                case EstadoReserva.Confirmada: return "Confirmada";
                case EstadoReserva.EnCurso: return "Activa";
                case EstadoReserva.Finalizada: return "Finalizada";
                case EstadoReserva.Cancelada: return "Cancelada";
                default: return estado.ToString();
            }
        }

        private static int DibujarFilaTabla(Graphics g, int x, int y, int[] anchos, string[] valores,
            Font fuente, bool esEncabezado, Pen borde = null)
        {
            int altoFila = 20;
            int xTemp = x;

            for (int i = 0; i < valores.Length; i++)
            {
                if (esEncabezado)
                {
                    g.FillRectangle(Brushes.LightGray, xTemp, y, anchos[i], altoFila);
                    g.DrawRectangle(Pens.Black, xTemp, y, anchos[i], altoFila);
                }
                else if (borde != null)
                {
                    g.DrawRectangle(borde, xTemp, y, anchos[i], altoFila);
                }

                g.DrawString(valores[i], fuente, Brushes.Black, xTemp + 3, y + 3);
                xTemp += anchos[i];
            }

            return y + altoFila;
        }
    }
}