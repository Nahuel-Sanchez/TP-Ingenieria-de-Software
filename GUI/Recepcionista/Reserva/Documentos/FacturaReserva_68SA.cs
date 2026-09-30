using BE_08YS;
using GUI_08YS.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Factura de la reserva en hoja A4: encabezado con la marca, datos del cliente y de la estadía,
    /// detalle de conceptos (alojamiento y renovaciones), totales, pagos registrados y sello de estado.
    /// </summary>
    public class FacturaReserva_68SA : DocumentoReserva_68SA
    {
        private const float AnchoA4 = 827f;
        private const float AltoA4 = 1169f;
        private const float Margen = 56f;

        public FacturaReserva_68SA(DatosDocumentoReserva_68SA datos) : base(datos) { }

        public override string Titulo => T("Documento_tituloFactura");
        public override string NombreArchivo => $"{T("Documento_archivoFactura")}_{NumeroFactura}.pdf";
        public override SizeF TamanoVista => new SizeF(AnchoA4, AltoA4);

        private string NumeroFactura => $"0001-{Reserva.Id:00000000}";

        private sealed class Concepto
        {
            public string Descripcion;
            public string Detalle;
            public int Cantidad;
            public decimal PrecioUnitario;
            public decimal Importe;
        }

        private List<Concepto> Conceptos()
        {
            var lista = new List<Concepto>();
            decimal tarifa = Reserva.TarifaNoche;
            int nochesOriginales = tarifa > 0 ? (int)Math.Round(Reserva.MontoOriginal / tarifa) : Reserva.Noches;
            string habitacion = $"{Reserva.Habitacion?.NroHabitacion} · {Reserva.Habitacion?.Tipo?.Nombre}";

            lista.Add(new Concepto
            {
                Descripcion = string.Format(T("Factura_conceptoAlojamiento"), habitacion),
                Detalle = string.Format(T("Factura_detalleAlojamiento"), Fecha(Reserva.FechaIngreso),
                    Fecha(Reserva.FechaIngreso.AddDays(nochesOriginales)), TextoHuespedes()),
                Cantidad = nochesOriginales,
                PrecioUnitario = tarifa,
                Importe = Reserva.MontoOriginal
            });

            decimal renovacion = Reserva.MontoTotal - Reserva.MontoOriginal;
            if (renovacion > 0)
            {
                int nochesExtra = tarifa > 0 ? (int)Math.Round(renovacion / tarifa) : 0;
                lista.Add(new Concepto
                {
                    Descripcion = T("Factura_conceptoRenovacion"),
                    Detalle = string.Format(T("Factura_detalleRenovacion"), Fecha(Reserva.FechaIngreso.AddDays(nochesOriginales)), Fecha(Reserva.FechaEgreso)),
                    Cantidad = nochesExtra,
                    PrecioUnitario = tarifa,
                    Importe = renovacion
                });
            }
            return lista;
        }

        protected override void DibujarContenido(Graphics g, RectangleF area, Color fondo)
        {
            float escalaX = area.Width / AnchoA4;
            float escalaY = area.Height / AltoA4;
            float escala = Math.Min(1f, Math.Min(escalaX, escalaY));

            // La factura se diseña en A4; si la hoja es distinta (Carta, por ejemplo) se ajusta sin deformarse
            var estado = g.Save();
            g.TranslateTransform(area.X + (area.Width - AnchoA4 * escala) / 2, area.Y);
            g.ScaleTransform(escala, escala);
            DibujarA4(g);
            g.Restore(estado);
        }

        private void DibujarA4(Graphics g)
        {
            float x = Margen, ancho = AnchoA4 - Margen * 2;

            using (var fFactura = new Font("Segoe UI", 24F, FontStyle.Bold))
            using (var fNumero = new Font("Segoe UI", 10F, FontStyle.Bold))
            using (var fEtiqueta = new Font("Segoe UI", 7F, FontStyle.Bold))
            using (var fTitularNombre = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (var fTexto = new Font("Segoe UI", 8.5F))
            using (var fNegrita = new Font("Segoe UI", 8.5F, FontStyle.Bold))
            using (var fChico = new Font("Segoe UI", 7.5F))
            using (var fTotal = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (var fSello = new Font("Segoe UI", 14F, FontStyle.Bold))
            using (var fGracias = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic))
            {
                Rellenar(g, new RectangleF(0, 0, AnchoA4, AltoA4), Color.White);

                // ---------- Encabezado ----------
                var banda = new RectangleF(0, 0, AnchoA4, 132);
                using (var brush = new LinearGradientBrush(banda, Marca, MarcaSuave, LinearGradientMode.Horizontal))
                    g.FillRectangle(brush, banda);
                Rellenar(g, new RectangleF(0, 132, AnchoA4, 5), Dorado);

                DibujarLogo(g, new RectangleF(x - 6, 18, 210, 96));

                var derecha = new RectangleF(AnchoA4 - Margen - 330, 24, 330, 40);
                Escribir(g, T("Factura_titulo").ToUpper(), fFactura, Color.White, derecha, StringAlignment.Far);
                Escribir(g, $"N° {NumeroFactura}", fNumero, Dorado, new RectangleF(derecha.X, 66, 330, 18), StringAlignment.Far);
                Escribir(g, $"{T("Factura_fechaEmision")}: {Datos.FechaEmision:dd/MM/yyyy}", fChico, Color.FromArgb(200, 205, 222),
                    new RectangleF(derecha.X, 86, 330, 14), StringAlignment.Far);
                Escribir(g, $"{T("Documento_nroReserva")}: {NumeroReserva}", fChico, Color.FromArgb(200, 205, 222),
                    new RectangleF(derecha.X, 101, 330, 14), StringAlignment.Far);

                // ---------- Cliente y estadía ----------
                float y = 162;
                float anchoCaja = (ancho - 20) / 2, altoCaja = 168;
                var cajaCliente = new RectangleF(x, y, anchoCaja, altoCaja);
                var cajaEstadia = new RectangleF(x + anchoCaja + 20, y, anchoCaja, altoCaja);
                foreach (var caja in new[] { cajaCliente, cajaEstadia })
                {
                    Rellenar(g, caja, FondoCaja, 8);
                    Contorno(g, caja, Borde, 8);
                    Rellenar(g, new RectangleF(caja.X, caja.Y + 12, 4, 20), Dorado);
                }

                var titular = Datos.Titular;
                Escribir(g, T("Factura_facturarA").ToUpper(), fEtiqueta, TextoSuave, new RectangleF(cajaCliente.X + 16, y + 16, anchoCaja - 30, 14));
                Escribir(g, titular == null ? "-" : $"{titular.Nombre} {titular.Apellido}", fTitularNombre, Marca,
                    new RectangleF(cajaCliente.X + 16, y + 36, anchoCaja - 30, 22));
                float yc = y + 64;
                yc = Dato(g, cajaCliente.X + 16, yc, anchoCaja - 30, T("Documento_documento"), TextoDocumentoTitular(), fChico, fTexto);
                yc = Dato(g, cajaCliente.X + 16, yc, anchoCaja - 30, T("Documento_nacionalidad"), titular?.Nacionalidad, fChico, fTexto);
                yc = Dato(g, cajaCliente.X + 16, yc, anchoCaja - 30, T("Documento_email"), titular?.Email, fChico, fTexto);
                Dato(g, cajaCliente.X + 16, yc, anchoCaja - 30, T("Documento_telefono"), titular?.Telefono, fChico, fTexto);

                Escribir(g, T("Factura_detalleReserva").ToUpper(), fEtiqueta, TextoSuave, new RectangleF(cajaEstadia.X + 16, y + 16, anchoCaja - 30, 14));
                float ye = y + 36;
                ye = Dato(g, cajaEstadia.X + 16, ye, anchoCaja - 30, T("Documento_habitacion"),
                    $"N° {Reserva.Habitacion?.NroHabitacion} · {Reserva.Habitacion?.Tipo?.Nombre}", fChico, fNegrita);
                ye = Dato(g, cajaEstadia.X + 16, ye, anchoCaja - 30, T("Documento_ingreso"),
                    $"{Fecha(Reserva.FechaIngreso)} · {Hora(Datos.HoraCheckIn)}", fChico, fTexto);
                ye = Dato(g, cajaEstadia.X + 16, ye, anchoCaja - 30, T("Documento_egreso"),
                    $"{Fecha(Reserva.FechaEgreso)} · {Hora(Datos.HoraCheckOut)}", fChico, fTexto);
                ye = Dato(g, cajaEstadia.X + 16, ye, anchoCaja - 30, T("Documento_huespedes"),
                    $"{TextoHuespedes()} · {TextoNoches(Reserva.Noches)}", fChico, fTexto);
                var colores = ColoresEstado(Reserva.Estado);
                string estado = TextoEstado(Reserva.Estado).ToUpper();
                float anchoEstado = g.MeasureString(estado, fEtiqueta).Width + 22;
                var pastilla = new RectangleF(cajaEstadia.Right - anchoEstado - 14, y + 12, anchoEstado, 20);
                Rellenar(g, pastilla, colores.Fondo, 10);
                Escribir(g, estado, fEtiqueta, colores.Frente, pastilla, StringAlignment.Center, StringAlignment.Center);

                // ---------- Conceptos ----------
                y += altoCaja + 30;
                float[] columnas = { ancho - 330, 70, 120, 140 };
                string[] encabezados = { T("Factura_colDescripcion"), T("Factura_colCantidad"), T("Factura_colPrecio"), T("Factura_colImporte") };
                var alineaciones = new[] { StringAlignment.Near, StringAlignment.Center, StringAlignment.Far, StringAlignment.Far };

                Rellenar(g, new RectangleF(x, y, ancho, 30), Marca, 4);
                float cxCol = x;
                for (int i = 0; i < columnas.Length; i++)
                {
                    Escribir(g, encabezados[i].ToUpper(), fEtiqueta, Color.White, new RectangleF(cxCol + 12, y, columnas[i] - 24, 30), alineaciones[i], StringAlignment.Center);
                    cxCol += columnas[i];
                }
                y += 30;

                int indice = 0;
                foreach (var concepto in Conceptos())
                {
                    const float alto = 48;
                    if (indice++ % 2 == 1) Rellenar(g, new RectangleF(x, y, ancho, alto), FondoCaja);
                    Escribir(g, concepto.Descripcion, fNegrita, Texto, new RectangleF(x + 12, y + 8, columnas[0] - 24, 18));
                    Escribir(g, concepto.Detalle, fChico, TextoSuave, new RectangleF(x + 12, y + 26, columnas[0] - 24, 16));
                    cxCol = x + columnas[0];
                    string[] valores = { concepto.Cantidad.ToString(), Moneda(concepto.PrecioUnitario), Moneda(concepto.Importe) };
                    for (int i = 0; i < valores.Length; i++)
                    {
                        Escribir(g, valores[i], i == 2 ? fNegrita : fTexto, Texto,
                            new RectangleF(cxCol + 12, y, columnas[i + 1] - 24, alto), alineaciones[i + 1], StringAlignment.Center);
                        cxCol += columnas[i + 1];
                    }
                    Linea(g, x, y + alto, x + ancho, y + alto, Borde);
                    y += alto;
                }

                // ---------- Totales y sello ----------
                y += 18;
                float anchoTotales = 300, xTotales = x + ancho - anchoTotales;
                y = FilaTotal(g, xTotales, y, anchoTotales, T("Factura_subtotal"), Moneda(Reserva.MontoTotal), fTexto, fTexto, Texto);
                var bandaTotal = new RectangleF(xTotales, y + 4, anchoTotales, 36);
                Rellenar(g, bandaTotal, Marca, 4);
                Escribir(g, T("Factura_total").ToUpper(), fNegrita, Dorado, new RectangleF(bandaTotal.X + 14, bandaTotal.Y, 140, bandaTotal.Height), StringAlignment.Near, StringAlignment.Center);
                Escribir(g, Moneda(Reserva.MontoTotal), fTotal, Color.White, new RectangleF(bandaTotal.X, bandaTotal.Y, bandaTotal.Width - 14, bandaTotal.Height), StringAlignment.Far, StringAlignment.Center);
                y = bandaTotal.Bottom + 6;
                y = FilaTotal(g, xTotales, y, anchoTotales, T("Documento_pagado"), Moneda(Datos.TotalPagado), fTexto, fNegrita, Verde);
                y = FilaTotal(g, xTotales, y, anchoTotales, T("Documento_saldo"), Moneda(Datos.Saldo), fTexto, fNegrita, Datos.Saldo > 0 ? Rojo : Texto);

                DibujarSello(g, new PointF(x + 150, bandaTotal.Y + 14), fSello);

                // ---------- Pagos ----------
                y += 34;
                Escribir(g, T("Factura_pagosRegistrados").ToUpper(), fEtiqueta, Marca, new RectangleF(x, y, ancho, 14));
                y += 20;
                float[] colPagos = { 200, ancho - 200 - 160, 160 };
                Rellenar(g, new RectangleF(x, y, ancho, 26), FondoCaja, 4);
                Escribir(g, T("Factura_colFecha").ToUpper(), fEtiqueta, TextoSuave, new RectangleF(x + 12, y, colPagos[0] - 24, 26), StringAlignment.Near, StringAlignment.Center);
                Escribir(g, T("Factura_colMedio").ToUpper(), fEtiqueta, TextoSuave, new RectangleF(x + colPagos[0] + 12, y, colPagos[1] - 24, 26), StringAlignment.Near, StringAlignment.Center);
                Escribir(g, T("Factura_colImporte").ToUpper(), fEtiqueta, TextoSuave, new RectangleF(x + colPagos[0] + colPagos[1], y, colPagos[2] - 12, 26), StringAlignment.Far, StringAlignment.Center);
                y += 26;

                float limitePagos = AltoA4 - Margen - 110;
                if (Datos.Pagos.Count == 0)
                {
                    Escribir(g, T("Factura_sinPagos"), fChico, TextoSuave, new RectangleF(x + 12, y, ancho - 24, 24), StringAlignment.Near, StringAlignment.Center);
                    y += 24;
                }
                for (int i = 0; i < Datos.Pagos.Count; i++)
                {
                    if (y + 24 > limitePagos)
                    {
                        Escribir(g, string.Format(T("Factura_masPagos"), Datos.Pagos.Count - i), fChico, TextoSuave,
                            new RectangleF(x + 12, y, ancho - 24, 20), StringAlignment.Near, StringAlignment.Center);
                        break;
                    }
                    var pago = Datos.Pagos[i];
                    Escribir(g, pago.FechaPago.ToString("dd/MM/yyyy HH:mm"), fTexto, Texto, new RectangleF(x + 12, y, colPagos[0] - 24, 24), StringAlignment.Near, StringAlignment.Center);
                    Escribir(g, OpcionEnum_68SA.Texto(pago.MetodoPago), fTexto, Texto, new RectangleF(x + colPagos[0] + 12, y, colPagos[1] - 24, 24), StringAlignment.Near, StringAlignment.Center);
                    Escribir(g, Moneda(pago.Monto), fNegrita, Texto, new RectangleF(x + colPagos[0] + colPagos[1], y, colPagos[2] - 12, 24), StringAlignment.Far, StringAlignment.Center);
                    Linea(g, x, y + 24, x + ancho, y + 24, Borde);
                    y += 24;
                }

                // ---------- Pie ----------
                float yPie = AltoA4 - Margen - 62;
                Linea(g, x, yPie, x + ancho, yPie, Dorado, 1.5f);
                Escribir(g, T("Documento_gracias"), fGracias, Marca, new RectangleF(x, yPie + 12, ancho, 20), StringAlignment.Center);
                Escribir(g, string.Format(T("Documento_emitidoPor"), Datos.FechaEmision.ToString("dd/MM/yyyy HH:mm"), Datos.EmitidoPor),
                    fChico, TextoSuave, new RectangleF(x, yPie + 34, ancho, 14), StringAlignment.Center);
                Escribir(g, T("Factura_leyenda"), fChico, TextoSuave, new RectangleF(x, yPie + 48, ancho, 14), StringAlignment.Center);
            }
        }

        private static float Dato(Graphics g, float x, float y, float ancho, string etiqueta, string valor, Font fEtiqueta, Font fValor)
        {
            Escribir(g, etiqueta, fEtiqueta, TextoSuave, new RectangleF(x, y, 90, 20), StringAlignment.Near, StringAlignment.Center);
            Escribir(g, string.IsNullOrWhiteSpace(valor) ? "-" : valor, fValor, Texto, new RectangleF(x + 92, y, ancho - 92, 20), StringAlignment.Near, StringAlignment.Center);
            return y + 22;
        }

        private static float FilaTotal(Graphics g, float x, float y, float ancho, string etiqueta, string valor, Font fEtiqueta, Font fValor, Color colorValor)
        {
            Escribir(g, etiqueta, fEtiqueta, TextoSuave, new RectangleF(x + 14, y, 150, 24), StringAlignment.Near, StringAlignment.Center);
            Escribir(g, valor, fValor, colorValor, new RectangleF(x, y, ancho - 14, 24), StringAlignment.Far, StringAlignment.Center);
            return y + 24;
        }

        /// <summary>Sello girado con el estado del pago: PAGADA, SALDO PENDIENTE o ANULADA.</summary>
        private void DibujarSello(Graphics g, PointF centro, Font fuente)
        {
            string texto;
            Color color;
            if (Reserva.Estado == EstadoReserva.Cancelada) { texto = T("Factura_selloAnulada"); color = Rojo; }
            else if (Datos.Saldo <= 0) { texto = T("Factura_selloPagada"); color = Verde; }
            else { texto = T("Factura_selloPendiente"); color = Color.FromArgb(200, 120, 20); }

            var estado = g.Save();
            g.TranslateTransform(centro.X, centro.Y);
            g.RotateTransform(-10);
            float ancho = g.MeasureString(texto.ToUpper(), fuente).Width + 36;
            var marco = new RectangleF(-ancho / 2, -24, ancho, 48);
            Contorno(g, marco, Color.FromArgb(200, color), 8, 3f);
            Contorno(g, RectangleF.Inflate(marco, -5, -5), Color.FromArgb(140, color), 5, 1f);
            Escribir(g, texto.ToUpper(), fuente, Color.FromArgb(210, color), marco, StringAlignment.Center, StringAlignment.Center);
            g.Restore(estado);
        }
    }
}
