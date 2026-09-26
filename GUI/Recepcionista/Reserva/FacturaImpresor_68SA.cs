using BE_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    public class FacturaImpresor_68SA
    {
        private readonly Reserva_68SA _reserva;
        private readonly List<Pago_68SA> _pagos;

        private int _filaPagoActual = 0;
        private bool _detalleYaImpreso = false; // encabezado/datos/cargos se dibujan una sola vez, no en cada página

        public FacturaImpresor_68SA(Reserva_68SA reserva, List<Pago_68SA> pagos)
        {
            _reserva = reserva;
            _pagos = pagos;
        }

        public void Imprimir()
        {
            _filaPagoActual = 0;
            _detalleYaImpreso = false;

            var pd = new PrintDocument();
            pd.DocumentName = $"Comprobante Reserva #{_reserva.Id}";
            pd.PrintPage += Pd_PrintPage;

            var printDialog = new PrintDialog { Document = pd };
            if (printDialog.ShowDialog() == DialogResult.OK)
                pd.Print();
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Font fuenteTitulo = new Font("Arial", 16, FontStyle.Bold);
            Font fuenteSubtitulo = new Font("Arial", 11, FontStyle.Regular);
            Font fuenteInfo = new Font("Arial", 9, FontStyle.Italic);
            Font fuenteSeccion = new Font("Arial", 11, FontStyle.Bold);
            Font fuenteEncabezado = new Font("Arial", 9, FontStyle.Bold);
            Font fuenteCuerpo = new Font("Arial", 9, FontStyle.Regular);
            Font fuenteEtiqueta = new Font("Arial", 9, FontStyle.Bold);

            Brush pincelNegro = Brushes.Black;
            Pen lapizGris = new Pen(Color.LightGray, 1);

            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            int anchoTotal = e.MarginBounds.Width;

            if (!_detalleYaImpreso)
            {
                // ---- Encabezado ----
                g.DrawString("HORIZON HOTEL & RESORT", fuenteTitulo, pincelNegro, x, y);
                y += 28;
                g.DrawString($"Comprobante de Reserva N.° {_reserva.Id}", fuenteSubtitulo, pincelNegro, x, y);
                y += 20;
                g.DrawString($"Fecha de emisión: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fuenteInfo, pincelNegro, x, y);
                y += 30;

                // ---- Datos de la reserva ----
                void FilaDato(string etiqueta1, string valor1, string etiqueta2, string valor2)
                {
                    g.DrawString(etiqueta1, fuenteEtiqueta, pincelNegro, x, y);
                    g.DrawString(valor1 ?? "-", fuenteCuerpo, pincelNegro, x + 100, y);
                    g.DrawString(etiqueta2, fuenteEtiqueta, pincelNegro, x + 300, y);
                    g.DrawString(valor2 ?? "-", fuenteCuerpo, pincelNegro, x + 400, y);
                    y += 20;
                }

                FilaDato("Huésped:", $"{_reserva.Titular.Nombre} {_reserva.Titular.Apellido}", "Documento:", _reserva.Titular.Documento);
                FilaDato("Habitación:", _reserva.Habitacion.NroHabitacion, "Estado:", _reserva.Estado.ToString());
                FilaDato("Check-in:", _reserva.FechaIngreso.ToString("dd/MM/yyyy"), "Check-out:", _reserva.FechaEgreso.ToString("dd/MM/yyyy"));
                FilaDato("Adultos:", _reserva.CantidadAdultos.ToString(), "Niños:", _reserva.CantidadNinos.ToString());
                y += 10;

                // ---- Detalle de cargos ----
                g.DrawString("DETALLE", fuenteSeccion, pincelNegro, x, y);
                y += 22;

                int[] anchosCargos = { anchoTotal - 260, 130, 130 };
                y = DibujarFilaTabla(g, x, y, anchosCargos, new[] { "Concepto", "Noches", "Monto" }, fuenteEncabezado, true);

                int nochesOriginales = (int)(_reserva.FechaEgreso - _reserva.FechaIngreso).TotalDays;
                y = DibujarFilaTabla(g, x, y, anchosCargos,
                    new[] { $"Estadía — Habitación {_reserva.Habitacion.NroHabitacion}", nochesOriginales.ToString(), $"${_reserva.MontoOriginal:N2}" },
                    fuenteCuerpo, false, lapizGris);

                decimal montoRenovacion = _reserva.MontoTotal - _reserva.MontoOriginal;
                if (montoRenovacion > 0)
                {
                    y = DibujarFilaTabla(g, x, y, anchosCargos,
                        new[] { "Renovación de estadía", "-", $"${montoRenovacion:N2}" },
                        fuenteCuerpo, false, lapizGris);
                }

                y = DibujarFilaTabla(g, x, y, anchosCargos,
                    new[] { "TOTAL", "", $"${_reserva.MontoTotal:N2}" },
                    fuenteEncabezado, false, lapizGris);
                y += 15;

                // ---- Pagos: título ----
                g.DrawString("PAGOS REGISTRADOS", fuenteSeccion, pincelNegro, x, y);
                y += 22;

                _detalleYaImpreso = true;
            }

            int[] anchosPagos = { anchoTotal / 3, anchoTotal / 3, anchoTotal - 2 * (anchoTotal / 3) };
            y = DibujarFilaTabla(g, x, y, anchosPagos, new[] { "Fecha", "Método", "Monto" }, fuenteEncabezado, true);

            while (_filaPagoActual < _pagos.Count)
            {
                var pago = _pagos[_filaPagoActual];
                y = DibujarFilaTabla(g, x, y, anchosPagos,
                    new[] { pago.FechaPago.ToString("dd/MM/yyyy HH:mm"), pago.MetodoPago.ToString(), $"${pago.Monto:N2}" },
                    fuenteCuerpo, false, lapizGris);

                _filaPagoActual++;

                if (y > e.MarginBounds.Bottom - 80 && _filaPagoActual < _pagos.Count)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            // ---- Resumen y pie (solo en la última página) ----
            y += 15;
            decimal totalPagado = 0;
            foreach (var pago in _pagos) totalPagado += pago.Monto;

            Font fuenteResumen = new Font("Arial", 10, FontStyle.Bold);
            g.DrawString($"Total abonado: ${totalPagado:N2}    |    Saldo: ${Math.Max(0, _reserva.MontoTotal - totalPagado):N2}",
                fuenteResumen, pincelNegro, x, y);
            y += 30;

            Font fuentePie = new Font("Arial", 9, FontStyle.Italic);
            string pie = "Gracias por elegir Horizon Hotel & Resort.";
            SizeF medida = g.MeasureString(pie, fuentePie);
            g.DrawString(pie, fuentePie, pincelNegro, x + (anchoTotal - medida.Width) / 2, y);

            e.HasMorePages = false;
        }

        private static int DibujarFilaTabla(Graphics g, int x, int y, int[] anchos, string[] valores,
            Font fuente, bool esEncabezado, Pen borde = null)
        {
            int altoFila = 22;
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

                g.DrawString(valores[i], fuente, Brushes.Black, xTemp + 5, y + 4);
                xTemp += anchos[i];
            }

            return y + altoFila;
        }
    }
}