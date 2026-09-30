using BE_08YS;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Arma el mail de la reserva (cuerpo HTML + comprobante y factura en PDF) y lo envía en un hilo aparte.
    /// </summary>
    public static class EnvioDocumentosReserva_68SA
    {
        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        /// <summary>Motivo por el que no se puede enviar, o null si está todo listo.</summary>
        public static string MotivoNoEnvio(DatosDocumentoReserva_68SA datos)
        {
            if (!CorreoService_68SA.Instance.EstaConfigurado) return T("Correo_noConfigurado");
            if (string.IsNullOrWhiteSpace(datos?.Email)) return T("Correo_sinEmail");
            if (!CorreoService_68SA.EsDireccionValida(datos.Email)) return string.Format(T("Correo_emailInvalido"), datos.Email);
            return null;
        }

        /// <summary>
        /// Envía el mail sin bloquear la pantalla. <paramref name="alTerminar"/> se ejecuta en el hilo de la
        /// interfaz con (enviado, detalle): si se envió, el detalle trae avisos (por ejemplo, un PDF que no se pudo generar).
        /// </summary>
        public static void Enviar(DatosDocumentoReserva_68SA datos, IList<DocumentoReserva_68SA> documentos, Action<bool, string> alTerminar)
        {
            string asunto = string.Format(T("Correo_asunto"), datos.Reserva.Id.ToString("000000"));
            string cuerpo = ArmarCuerpoHtml(datos);
            var avisos = new List<string>();

            CorreoService_68SA.Instance.EnviarEnSegundoPlano(datos.Email, asunto, cuerpo,
                generarAdjuntos: () =>
                {
                    // Corre dentro del hilo de envío: los PDF se generan sin congelar la pantalla
                    var adjuntos = new List<AdjuntoCorreo_68SA>();
                    foreach (var documento in documentos)
                    {
                        byte[] pdf = documento.GenerarPdf(out string error);
                        if (pdf != null) adjuntos.Add(new AdjuntoCorreo_68SA(documento.NombreArchivo, pdf));
                        else avisos.Add($"{documento.Titulo}: {error}");
                    }
                    return adjuntos;
                },
                alTerminar: (enviado, error) =>
                    alTerminar?.Invoke(enviado, enviado ? (avisos.Count > 0 ? string.Join(" | ", avisos) : null) : error));
        }

        private static string ArmarCuerpoHtml(DatosDocumentoReserva_68SA datos)
        {
            var r = datos.Reserva;
            var cultura = OpcionEnum_68SA.CulturaActual();
            string H(string texto) => WebUtility.HtmlEncode(texto ?? string.Empty);
            string Moneda(decimal valor) => "$ " + valor.ToString("N2", cultura);
            string FechaHora(DateTime fecha, TimeSpan hora) =>
                cultura.TextInfo.ToTitleCase(fecha.ToString("dddd dd/MM/yyyy", cultura)) + " · " + DateTime.Today.Add(hora).ToString("HH:mm");

            string nombre = datos.Titular?.Nombre ?? r.Titular?.Nombre;
            string huespedes = string.Format(T(r.CantidadAdultos == 1 ? "Documento_adulto" : "Documento_adultos"), r.CantidadAdultos)
                + (r.CantidadNinos > 0 ? " · " + string.Format(T(r.CantidadNinos == 1 ? "Documento_nino" : "Documento_ninos"), r.CantidadNinos) : "");

            var filas = new List<(string Etiqueta, string Valor)>
            {
                (T("Documento_nroReserva"), r.Id.ToString("000000")),
                (T("Documento_habitacion"), $"N° {r.Habitacion?.NroHabitacion} · {r.Habitacion?.Tipo?.Nombre}"),
                (T("Documento_ingreso"), FechaHora(r.FechaIngreso, datos.HoraCheckIn)),
                (T("Documento_egreso"), FechaHora(r.FechaEgreso, datos.HoraCheckOut)),
                (T("Documento_huespedes"), $"{huespedes} · {r.Noches} {T(r.Noches == 1 ? "RegistrarReserva_txtNoche" : "RegistrarReserva_txtNoches")}"),
                (T("Documento_totalEstadia"), Moneda(r.MontoTotal)),
                (T("Documento_pagado"), Moneda(datos.TotalPagado)),
                (T("Documento_saldo"), Moneda(datos.Saldo)),
            };

            var html = new StringBuilder();
            html.Append("<!DOCTYPE html><html><head><meta charset=\"utf-8\"></head>");
            html.Append("<body style=\"margin:0;padding:0;background:#f1efe9;font-family:'Segoe UI',Arial,sans-serif;color:#1e212d;\">");
            html.Append("<table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:#f1efe9;padding:24px 0;\"><tr><td align=\"center\">");
            html.Append("<table role=\"presentation\" width=\"560\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:#ffffff;border-radius:12px;overflow:hidden;border:1px solid #e2ded4;\">");

            // Encabezado
            html.Append("<tr><td style=\"background:#0a1232;padding:26px 32px;border-bottom:4px solid #c49628;\">");
            html.Append($"<div style=\"color:#c49628;font-size:12px;font-weight:bold;letter-spacing:1px;\">{H(T("Documento_tituloComprobante").ToUpper())}</div>");
            html.Append($"<div style=\"color:#ffffff;font-size:22px;font-weight:bold;margin-top:4px;\">{H(T("Reporte_hotel"))}</div>");
            html.Append("</td></tr>");

            // Saludo
            html.Append("<tr><td style=\"padding:28px 32px 8px 32px;\">");
            html.Append($"<div style=\"font-size:18px;font-weight:bold;color:#0a1232;\">{H(string.Format(T("Correo_saludo"), nombre))}</div>");
            html.Append($"<p style=\"font-size:14px;line-height:21px;color:#4b5060;margin:10px 0 0 0;\">{H(T("Correo_intro"))}</p>");
            html.Append("</td></tr>");

            // Resumen
            html.Append("<tr><td style=\"padding:16px 32px;\"><table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:#f9f7f2;border:1px solid #e2ded4;border-radius:8px;\">");
            foreach (var (etiqueta, valor) in filas)
            {
                html.Append("<tr>");
                html.Append($"<td style=\"padding:9px 16px;font-size:12px;color:#6e7382;border-bottom:1px solid #ece8de;\">{H(etiqueta)}</td>");
                html.Append($"<td style=\"padding:9px 16px;font-size:13px;font-weight:bold;color:#1e212d;text-align:right;border-bottom:1px solid #ece8de;\">{H(valor)}</td>");
                html.Append("</tr>");
            }
            html.Append("</table></td></tr>");

            // Adjuntos y pie
            html.Append("<tr><td style=\"padding:8px 32px 24px 32px;\">");
            html.Append($"<p style=\"font-size:13px;line-height:20px;color:#4b5060;margin:0;\">{H(T("Correo_adjuntos"))}</p>");
            html.Append($"<p style=\"font-size:13px;line-height:20px;color:#4b5060;margin:12px 0 0 0;\">{H(string.Format(T("Correo_horarios"), DateTime.Today.Add(datos.HoraCheckIn).ToString("HH:mm"), DateTime.Today.Add(datos.HoraCheckOut).ToString("HH:mm")))}</p>");
            html.Append("</td></tr>");
            html.Append("<tr><td style=\"background:#0a1232;padding:18px 32px;text-align:center;\">");
            html.Append($"<div style=\"color:#c49628;font-size:14px;font-weight:bold;font-style:italic;\">{H(T("Documento_gracias"))}</div>");
            html.Append($"<div style=\"color:#9aa0b8;font-size:11px;margin-top:6px;\">{H(T("Correo_noResponder"))}</div>");
            html.Append("</td></tr>");

            html.Append("</table></td></tr></table></body></html>");
            return html.ToString();
        }
    }
}
