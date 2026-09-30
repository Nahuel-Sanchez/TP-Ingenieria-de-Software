using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Service_08YS
{
    /// <summary>Archivo adjunto de un mail (por ejemplo, el PDF del comprobante).</summary>
    public class AdjuntoCorreo_68SA
    {
        public string Nombre { get; }
        public byte[] Contenido { get; }
        public string TipoMime { get; }

        public AdjuntoCorreo_68SA(string nombre, byte[] contenido, string tipoMime = "application/pdf")
        {
            Nombre = nombre;
            Contenido = contenido ?? throw new ArgumentNullException(nameof(contenido));
            TipoMime = tipoMime;
        }
    }

    /// <summary>
    /// Envío de mails por SMTP. La cuenta se configura en el App.config de la aplicación (sección appSettings):
    /// Correo.Host, Correo.Puerto, Correo.UsarSsl, Correo.Usuario, Correo.Clave, Correo.Remitente, Correo.NombreRemitente.
    /// El envío se hace en un hilo aparte para no congelar la pantalla mientras se conecta con el servidor.
    /// </summary>
    public sealed class CorreoService_68SA
    {
        private static readonly Lazy<CorreoService_68SA> _instancia = new Lazy<CorreoService_68SA>(() => new CorreoService_68SA());
        public static CorreoService_68SA Instance => _instancia.Value;

        public string Host { get; }
        public int Puerto { get; }
        public bool UsarSsl { get; }
        public string Usuario { get; }
        public string Clave { get; }
        public string Remitente { get; }
        public string NombreRemitente { get; }

        private CorreoService_68SA()
        {
            var config = ConfigurationManager.AppSettings;
            Host = config["Correo.Host"]?.Trim();
            Puerto = int.TryParse(config["Correo.Puerto"], out int puerto) ? puerto : 587;
            UsarSsl = !bool.TryParse(config["Correo.UsarSsl"], out bool ssl) || ssl;
            Usuario = config["Correo.Usuario"]?.Trim();
            Clave = config["Correo.Clave"];
            Remitente = string.IsNullOrWhiteSpace(config["Correo.Remitente"]) ? Usuario : config["Correo.Remitente"].Trim();
            NombreRemitente = config["Correo.NombreRemitente"] ?? "Horizon Hotel & Resort";
        }

        /// <summary>true si hay servidor y remitente cargados en el App.config.</summary>
        public bool EstaConfigurado => !string.IsNullOrWhiteSpace(Host) && !string.IsNullOrWhiteSpace(Remitente);

        public static bool EsDireccionValida(string direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion)) return false;
            try { return new MailAddress(direccion.Trim()).Address == direccion.Trim(); }
            catch (FormatException) { return false; }
        }

        /// <summary>Envía el mail de forma sincrónica (se usa desde el hilo de envío).</summary>
        public void Enviar(string destinatario, string asunto, string cuerpoHtml, IEnumerable<AdjuntoCorreo_68SA> adjuntos)
        {
            if (!EstaConfigurado)
                throw new InvalidOperationException("El envío de correo no está configurado (App.config, claves Correo.*).");
            if (!EsDireccionValida(destinatario))
                throw new ArgumentException($"La dirección de correo \"{destinatario}\" no es válida.", nameof(destinatario));

            // Servidores como Gmail u Outlook exigen TLS 1.2
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            using (var mensaje = new MailMessage())
            {
                mensaje.From = new MailAddress(Remitente, NombreRemitente, Encoding.UTF8);
                mensaje.To.Add(destinatario.Trim());
                mensaje.Subject = asunto;
                mensaje.SubjectEncoding = Encoding.UTF8;
                mensaje.Body = cuerpoHtml;
                mensaje.BodyEncoding = Encoding.UTF8;
                mensaje.IsBodyHtml = true;

                foreach (var adjunto in adjuntos ?? Enumerable.Empty<AdjuntoCorreo_68SA>())
                {
                    // MailMessage.Dispose libera los adjuntos y, con ellos, los streams
                    mensaje.Attachments.Add(new Attachment(new MemoryStream(adjunto.Contenido), adjunto.Nombre, adjunto.TipoMime));
                }

                using (var smtp = new SmtpClient(Host, Puerto))
                {
                    smtp.EnableSsl = UsarSsl;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    if (!string.IsNullOrWhiteSpace(Usuario))
                        smtp.Credentials = new NetworkCredential(Usuario, Clave);
                    smtp.Timeout = 30000;
                    smtp.Send(mensaje);
                }
            }
        }

        /// <summary>
        /// Envía el mail en un hilo propio: la pantalla sigue respondiendo mientras se generan los adjuntos
        /// y se habla con el servidor SMTP. <paramref name="alTerminar"/> recibe (enviado, mensajeDeError) y se
        /// ejecuta en el hilo de la interfaz (el que llamó a este método), así puede tocar controles sin Invoke.
        /// </summary>
        /// <param name="generarAdjuntos">Se ejecuta dentro del hilo de envío (por ejemplo, para generar los PDF).</param>
        public Thread EnviarEnSegundoPlano(string destinatario, string asunto, string cuerpoHtml,
            Func<IEnumerable<AdjuntoCorreo_68SA>> generarAdjuntos, Action<bool, string> alTerminar)
        {
            var contextoInterfaz = SynchronizationContext.Current;

            var hilo = new Thread(() =>
            {
                bool enviado = false;
                string error = null;
                try
                {
                    var adjuntos = generarAdjuntos?.Invoke()?.ToList() ?? new List<AdjuntoCorreo_68SA>();
                    Enviar(destinatario, asunto, cuerpoHtml, adjuntos);
                    enviado = true;
                }
                catch (Exception ex)
                {
                    error = DescribirError(ex);
                }

                if (alTerminar == null) return;
                try
                {
                    if (contextoInterfaz != null)
                        contextoInterfaz.Post(_ => alTerminar(enviado, error), null);
                    else
                        alTerminar(enviado, error);
                }
                catch (InvalidOperationException)
                {
                    // La aplicación ya se cerró (no hay a quién avisar): el mail igual se envió o falló en silencio
                }
            })
            {
                Name = "EnvioCorreo_68SA",
                // No es de fondo: si se cierra la aplicación justo después de reservar, el mail igual sale
                IsBackground = false
            };
            // La impresión a PDF (para los adjuntos) usa componentes COM del administrador de impresión
            hilo.SetApartmentState(ApartmentState.STA);
            hilo.Start();
            return hilo;
        }

        private static string DescribirError(Exception ex)
        {
            var mensajes = new List<string>();
            for (var actual = ex; actual != null; actual = actual.InnerException)
                if (!string.IsNullOrWhiteSpace(actual.Message) && !mensajes.Contains(actual.Message))
                    mensajes.Add(actual.Message);
            return string.Join(" — ", mensajes);
        }
    }
}
