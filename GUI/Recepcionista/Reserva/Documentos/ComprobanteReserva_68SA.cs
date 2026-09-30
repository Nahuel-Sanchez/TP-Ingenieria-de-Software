using BE_08YS;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Comprobante (voucher) de la reserva: tarjeta tipo "ticket" con el número de reserva, las fechas con
    /// los horarios de check-in / check-out, la habitación, los huéspedes, los datos del titular y el pago.
    /// Es lo que el huésped presenta al llegar; se envía por mail al registrar la reserva.
    /// </summary>
    public class ComprobanteReserva_68SA : DocumentoReserva_68SA
    {
        private const float Ancho = 460f;
        private const float Alto = 710f;
        private const float Margen = 20f;
        private const float Relleno = 24f;

        public ComprobanteReserva_68SA(DatosDocumentoReserva_68SA datos) : base(datos) { }

        public override string Titulo => T("Documento_tituloComprobante");
        public override string NombreArchivo => $"{T("Documento_archivoComprobante")}_{NumeroReserva}.pdf";
        public override SizeF TamanoVista => new SizeF(Ancho + Margen * 2, Alto + Margen * 2);

        protected override void DibujarContenido(Graphics g, RectangleF area, Color fondo)
        {
            float margenSuperior = Math.Max(Margen, Math.Min(60f, (area.Height - Alto) / 2));
            float x = area.X + (area.Width - Ancho) / 2;
            float y = area.Y + margenSuperior;
            var tarjeta = new RectangleF(x, y, Ancho, Alto);

            using (var fTituloHotel = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (var fEtiqueta = new Font("Segoe UI", 6.5F, FontStyle.Bold))
            using (var fSeccion = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (var fNumero = new Font("Segoe UI", 24F, FontStyle.Bold))
            using (var fValorGrande = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (var fValor = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (var fTexto = new Font("Segoe UI", 8.5F))
            using (var fChico = new Font("Segoe UI", 7F))
            using (var fPie = new Font("Segoe UI", 8F, FontStyle.Italic))
            using (var fGracias = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic))
            {
                // Sombra y tarjeta
                Rellenar(g, new RectangleF(x + 3, y + 4, Ancho, Alto), Color.FromArgb(40, 0, 0, 0), 12);
                Rellenar(g, tarjeta, Color.White, 12);

                // ---------- Encabezado ----------
                var encabezado = new RectangleF(x, y, Ancho, 96);
                using (var path = RedondeadoArriba(encabezado, 12))
                using (var brush = new LinearGradientBrush(encabezado, Marca, MarcaSuave, LinearGradientMode.Horizontal))
                    g.FillPath(brush, path);
                Rellenar(g, new RectangleF(x, y + 92, Ancho, 4), Dorado);

                DibujarLogo(g, new RectangleF(x + 16, y + 12, 150, 70));
                Linea(g, x + 180, y + 22, x + 180, y + 72, Color.FromArgb(120, Dorado), 1.2f);
                Escribir(g, T("Documento_tituloComprobante").ToUpper(), fSeccion, Dorado, new RectangleF(x + 194, y + 20, Ancho - 204, 16));
                Escribir(g, T("Reporte_hotel"), fTituloHotel, Color.White, new RectangleF(x + 194, y + 36, Ancho - 204, 24));
                Escribir(g, string.Format(T("Documento_emitido"), Datos.FechaEmision.ToString("dd/MM/yyyy HH:mm")), fChico,
                    Color.FromArgb(190, 196, 215), new RectangleF(x + 194, y + 62, Ancho - 204, 14));

                // ---------- Número y estado ----------
                float cx = x + Relleno, ancho = Ancho - Relleno * 2;
                float yy = y + 112;
                Escribir(g, T("Documento_nroReserva").ToUpper(), fEtiqueta, TextoSuave, new RectangleF(cx, yy, 200, 12));
                Escribir(g, NumeroReserva, fNumero, Marca, new RectangleF(cx - 2, yy + 10, 260, 44));
                DibujarEstado(g, TextoEstado(Reserva.Estado).ToUpper(), Reserva.Estado, cx + ancho, yy + 18, fEtiqueta);

                yy = y + 176;
                SeparadorTicket(g, x, yy, fondo);

                // ---------- Detalle de la estadía ----------
                yy += 16;
                Escribir(g, T("Documento_detalleEstadia").ToUpper(), fSeccion, Marca, new RectangleF(cx, yy, ancho, 16));
                yy += 20;

                float anchoCaja = (ancho - 10) / 2, altoCaja = 62;
                Caja(g, new RectangleF(cx, yy, anchoCaja, altoCaja), T("Documento_ingreso"), DiaSemana(Reserva.FechaIngreso),
                    Fecha(Reserva.FechaIngreso), string.Format(T("Documento_desdeLas"), Hora(Datos.HoraCheckIn)), fEtiqueta, fValorGrande, fChico);
                Caja(g, new RectangleF(cx + anchoCaja + 10, yy, anchoCaja, altoCaja), T("Documento_egreso"), DiaSemana(Reserva.FechaEgreso),
                    Fecha(Reserva.FechaEgreso), string.Format(T("Documento_hastaLas"), Hora(Datos.HoraCheckOut)), fEtiqueta, fValorGrande, fChico);
                yy += altoCaja + 10;

                Caja(g, new RectangleF(cx, yy, anchoCaja, altoCaja), T("Documento_habitacion"), null,
                    $"N° {Reserva.Habitacion?.NroHabitacion}", Reserva.Habitacion?.Tipo?.Nombre ?? "", fEtiqueta, fValorGrande, fChico);
                Caja(g, new RectangleF(cx + anchoCaja + 10, yy, anchoCaja, altoCaja), T("Documento_huespedes"), null,
                    TextoHuespedes(), TextoNoches(Reserva.Noches), fEtiqueta, fValor, fChico);
                yy += altoCaja + 18;

                // ---------- Titular ----------
                Escribir(g, T("Documento_datosTitular").ToUpper(), fSeccion, Marca, new RectangleF(cx, yy, ancho, 16));
                yy += 20;
                var titular = Datos.Titular;
                yy = FilaDato(g, cx, yy, ancho, T("Documento_nombre"), titular == null ? "-" : $"{titular.Nombre} {titular.Apellido}", fTexto, fTexto);
                yy = FilaDato(g, cx, yy, ancho, T("Documento_documento"), TextoDocumentoTitular(), fTexto, fTexto);
                yy = FilaDato(g, cx, yy, ancho, T("Documento_nacionalidad"), titular?.Nacionalidad, fTexto, fTexto);
                yy = FilaDato(g, cx, yy, ancho, T("Documento_email"), titular?.Email, fTexto, fTexto);
                yy = FilaDato(g, cx, yy, ancho, T("Documento_telefono"), titular?.Telefono, fTexto, fTexto);
                yy += 14;

                // ---------- Pago ----------
                Escribir(g, T("Documento_pago").ToUpper(), fSeccion, Marca, new RectangleF(cx, yy, ancho, 16));
                yy += 20;
                string medios = string.Join(", ", Datos.Pagos.Select(p => OpcionEnum_68SA_Texto(p.MetodoPago)).Distinct());
                yy = FilaDato(g, cx, yy, ancho, T("Documento_totalEstadia"), Moneda(Reserva.MontoTotal), fTexto, fValor);
                yy = FilaDato(g, cx, yy, ancho,
                    string.IsNullOrEmpty(medios) ? T("Documento_pagado") : $"{T("Documento_pagado")} ({medios})",
                    Moneda(Datos.TotalPagado), fTexto, fValor);
                yy = FilaDato(g, cx, yy, ancho, T("Documento_saldo"), Moneda(Datos.Saldo), fTexto, fValor,
                    Datos.Saldo > 0 ? Rojo : Verde, sinLinea: true);

                yy = y + Alto - 92;
                SeparadorTicket(g, x, yy, fondo);

                // ---------- Pie ----------
                Escribir(g, T("Comprobante_presentar"), fPie, TextoSuave, new RectangleF(x, yy + 14, Ancho, 16), StringAlignment.Center);
                Escribir(g, string.Format(T("Documento_emitidoPor"), Datos.FechaEmision.ToString("dd/MM/yyyy HH:mm"), Datos.EmitidoPor),
                    fChico, TextoSuave, new RectangleF(x, yy + 32, Ancho, 14), StringAlignment.Center);
                Escribir(g, T("Documento_gracias"), fGracias, Marca, new RectangleF(x, yy + 50, Ancho, 18), StringAlignment.Center);

                Contorno(g, tarjeta, Borde, 12);
            }
        }

        private static string OpcionEnum_68SA_Texto(MetodoPago metodo) => GUI_08YS.UserControls.OpcionEnum_68SA.Texto(metodo);

        private void DibujarEstado(Graphics g, string texto, EstadoReserva estado, float derecha, float y, Font fuente)
        {
            var colores = ColoresEstado(estado);
            using (var negrita = new Font(fuente.FontFamily, 7.5F, FontStyle.Bold))
            {
                float ancho = g.MeasureString(texto, negrita).Width + 26;
                var pastilla = new RectangleF(derecha - ancho, y, ancho, 24);
                Rellenar(g, pastilla, colores.Fondo, 12);
                Escribir(g, texto, negrita, colores.Frente, pastilla, StringAlignment.Center, StringAlignment.Center);
            }
        }

        private void Caja(Graphics g, RectangleF r, string etiqueta, string etiquetaDerecha, string valor, string detalle,
            Font fEtiqueta, Font fValor, Font fDetalle)
        {
            Rellenar(g, r, FondoCaja, 6);
            Contorno(g, r, Borde, 6);
            Escribir(g, etiqueta.ToUpper(), fEtiqueta, TextoSuave, new RectangleF(r.X + 10, r.Y + 8, r.Width - 20, 12));
            if (!string.IsNullOrEmpty(etiquetaDerecha))
                Escribir(g, etiquetaDerecha, fDetalle, TextoSuave, new RectangleF(r.X + 10, r.Y + 7, r.Width - 20, 12), StringAlignment.Far);
            Escribir(g, valor, fValor, Marca, new RectangleF(r.X + 10, r.Y + 21, r.Width - 20, 22));
            Escribir(g, detalle, fDetalle, TextoSuave, new RectangleF(r.X + 10, r.Y + 43, r.Width - 20, 14));
        }

        private static float FilaDato(Graphics g, float x, float y, float ancho, string etiqueta, string valor,
            Font fEtiqueta, Font fValor, Color? colorValor = null, bool sinLinea = false)
        {
            const float alto = 22;
            const float anchoEtiqueta = 170;
            Escribir(g, etiqueta, fEtiqueta, TextoSuave, new RectangleF(x, y, anchoEtiqueta, alto), StringAlignment.Near, StringAlignment.Center);
            Escribir(g, string.IsNullOrWhiteSpace(valor) ? "-" : valor, fValor, colorValor ?? Texto,
                new RectangleF(x + anchoEtiqueta, y, ancho - anchoEtiqueta, alto), StringAlignment.Near, StringAlignment.Center);
            if (!sinLinea) Linea(g, x, y + alto, x + ancho, y + alto, Borde);
            return y + alto;
        }

        /// <summary>Línea punteada con "muescas" a los costados, como un ticket que se corta.</summary>
        private static void SeparadorTicket(Graphics g, float x, float y, Color fondo)
        {
            const float radio = 9;
            Linea(g, x + radio + 8, y, x + Ancho - radio - 8, y, Borde, 1.2f, punteada: true);
            foreach (float cx in new[] { x, x + Ancho })
            {
                var circulo = new RectangleF(cx - radio, y - radio, radio * 2, radio * 2);
                using (var brush = new SolidBrush(fondo))
                    g.FillEllipse(brush, circulo);
                // Solo se ve el borde de la mitad que "entra" en la tarjeta
                var estado = g.Save();
                g.SetClip(new RectangleF(x, y - radio - 1, Ancho, radio * 2 + 2));
                using (var pen = new Pen(Borde))
                    g.DrawEllipse(pen, circulo);
                g.Restore(estado);
            }
        }

        private static GraphicsPath RedondeadoArriba(RectangleF r, float radio)
        {
            var path = new GraphicsPath();
            float d = radio * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddLine(r.Right, r.Y + radio, r.Right, r.Bottom);
            path.AddLine(r.Right, r.Bottom, r.X, r.Bottom);
            path.CloseFigure();
            return path;
        }
    }
}
