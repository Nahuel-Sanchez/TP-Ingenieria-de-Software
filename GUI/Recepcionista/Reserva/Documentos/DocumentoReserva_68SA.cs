using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Todo lo que necesitan el comprobante y la factura de una reserva, leído una sola vez.
    /// </summary>
    public class DatosDocumentoReserva_68SA
    {
        public Reserva_68SA Reserva { get; set; }
        public Huesped_68SA Titular { get; set; }
        public List<Pago_68SA> Pagos { get; set; } = new List<Pago_68SA>();
        public TimeSpan HoraCheckIn { get; set; } = TimeSpan.FromHours(14);
        public TimeSpan HoraCheckOut { get; set; } = TimeSpan.FromHours(10);
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        public string EmitidoPor { get; set; }

        public decimal TotalPagado => Pagos.Sum(p => p.Monto);
        public decimal Saldo => Reserva.Estado == EstadoReserva.Cancelada ? 0 : Math.Max(0, Reserva.MontoTotal - TotalPagado);
        public string Email => Titular?.Email;

        /// <summary>
        /// Arma los datos de la reserva. <paramref name="titularCompleto"/> evita volver a buscar al huésped
        /// cuando ya se tiene (por ejemplo, recién registrada la reserva).
        /// </summary>
        public static DatosDocumentoReserva_68SA Cargar(int reservaId, Huesped_68SA titularCompleto = null)
        {
            var reserva = BLLFactory_08YS.CreateReservaBLL().GetById(reservaId);
            var pagos = BLLFactory_08YS.CreatePagoBLL().GetByReserva(reservaId) ?? new List<Pago_68SA>();

            Huesped_68SA titular = titularCompleto;
            if (titular == null && reserva.Titular != null)
            {
                // El listado de reservas trae solo nombre y documento del titular: el resto (email, teléfono...) se busca aparte
                try { titular = BLLFactory_08YS.CreateHuespedBLL().GetByDocumento(reserva.Titular.Documento); }
                catch { titular = null; }
            }

            var datos = new DatosDocumentoReserva_68SA
            {
                Reserva = reserva,
                Titular = titular ?? reserva.Titular,
                Pagos = pagos.OrderBy(p => p.FechaPago).ToList(),
                EmitidoPor = UsuarioActual()
            };

            try
            {
                var configuracion = BLLFactory_08YS.CreateConfiguracionHotelBLL().GetConfiguracion();
                if (configuracion != null)
                {
                    datos.HoraCheckIn = configuracion.HoraCheckIn;
                    datos.HoraCheckOut = configuracion.HoraCheckOut;
                }
            }
            catch { /* se usan los horarios por defecto */ }

            return datos;
        }

        private static string UsuarioActual()
        {
            var usuario = SessionManager_08YS.Instance.Current;
            return usuario == null ? "-" : $"{usuario.Nombre} {usuario.Apellido}".Trim();
        }
    }

    /// <summary>
    /// Documento de una reserva (comprobante o factura). Se dibuja UNA sola vez con GDI+ y ese mismo dibujo
    /// sirve para la vista previa en pantalla, la impresión y el PDF: siempre se ven iguales.
    /// Las coordenadas están en centésimas de pulgada (la unidad que usa la impresora).
    /// </summary>
    public abstract class DocumentoReserva_68SA
    {
        public const string ImpresoraPdf = "Microsoft Print to PDF";

        protected static readonly Color Marca = Color.FromArgb(10, 18, 50);
        protected static readonly Color MarcaSuave = Color.FromArgb(28, 40, 86);
        protected static readonly Color Dorado = Color.FromArgb(196, 150, 40);
        protected static readonly Color DoradoClaro = Color.FromArgb(250, 244, 226);
        protected static readonly Color Texto = Color.FromArgb(30, 33, 45);
        protected static readonly Color TextoSuave = Color.FromArgb(110, 115, 130);
        protected static readonly Color Borde = Color.FromArgb(226, 222, 212);
        protected static readonly Color FondoCaja = Color.FromArgb(249, 247, 242);
        protected static readonly Color Verde = Color.FromArgb(26, 135, 84);
        protected static readonly Color VerdeClaro = Color.FromArgb(222, 244, 232);
        protected static readonly Color Rojo = Color.FromArgb(192, 57, 43);
        protected static readonly Color RojoClaro = Color.FromArgb(252, 231, 228);
        protected static readonly Color Azul = Color.FromArgb(40, 100, 190);
        protected static readonly Color AzulClaro = Color.FromArgb(226, 237, 252);

        protected DatosDocumentoReserva_68SA Datos { get; }
        protected Reserva_68SA Reserva => Datos.Reserva;
        protected CultureInfo Cultura { get; } = OpcionEnum_68SA.CulturaActual();

        protected DocumentoReserva_68SA(DatosDocumentoReserva_68SA datos)
        {
            Datos = datos ?? throw new ArgumentNullException(nameof(datos));
        }

        /// <summary>Nombre para la pestaña del visor.</summary>
        public abstract string Titulo { get; }

        /// <summary>Nombre sugerido del archivo PDF.</summary>
        public abstract string NombreArchivo { get; }

        /// <summary>Tamaño del documento en la vista previa (centésimas de pulgada).</summary>
        public abstract SizeF TamanoVista { get; }

        /// <summary>Dibuja el documento dentro del área (página impresa o PDF, sobre papel blanco).</summary>
        public void Dibujar(Graphics g, RectangleF area) => DibujarContenido(g, area, Color.White);

        /// <param name="fondo">Color de lo que queda alrededor del documento (papel blanco o el fondo de la vista previa).</param>
        protected abstract void DibujarContenido(Graphics g, RectangleF area, Color fondo);

        protected static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        #region Pantalla, impresión y PDF

        /// <summary>Dibuja en pantalla con las mismas medidas físicas que en papel, a la escala indicada.</summary>
        public void DibujarEnPantalla(Graphics g, float escala, Color fondo)
        {
            g.PageUnit = GraphicsUnit.Inch;
            g.PageScale = 0.01f;
            g.ScaleTransform(escala, escala);
            PrepararCalidad(g);
            DibujarContenido(g, new RectangleF(PointF.Empty, TamanoVista), fondo);
        }

        private static void PrepararCalidad(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        public PrintDocument CrearDocumentoImpresion()
        {
            var documento = new PrintDocument { DocumentName = Path.GetFileNameWithoutExtension(NombreArchivo) };
            documento.DefaultPageSettings.Landscape = false;
            documento.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            UsarA4SiExiste(documento);
            documento.PrintPage += (s, e) =>
            {
                PrepararCalidad(e.Graphics);
                // Se dibuja sobre la hoja completa (cada documento maneja sus márgenes);
                // se descuenta el área no imprimible para que nada quede cortado.
                var hoja = e.PageSettings.Bounds;
                var util = new RectangleF(
                    Math.Max(0, e.PageSettings.HardMarginX), Math.Max(0, e.PageSettings.HardMarginY),
                    hoja.Width - 2 * Math.Max(0, e.PageSettings.HardMarginX),
                    hoja.Height - 2 * Math.Max(0, e.PageSettings.HardMarginY));
                e.Graphics.TranslateTransform(-e.PageSettings.HardMarginX, -e.PageSettings.HardMarginY);
                Dibujar(e.Graphics, new RectangleF(util.X, util.Y, util.Width, util.Height));
                e.HasMorePages = false;
            };
            return documento;
        }

        private static void UsarA4SiExiste(PrintDocument documento)
        {
            try
            {
                var a4 = documento.PrinterSettings.PaperSizes.Cast<PaperSize>().FirstOrDefault(p => p.Kind == PaperKind.A4);
                if (a4 != null) documento.DefaultPageSettings.PaperSize = a4;
            }
            catch { /* sin impresoras instaladas: se usa el tamaño por defecto */ }
        }

        public void Imprimir(IWin32Window propietario)
        {
            using (var documento = CrearDocumentoImpresion())
            using (var dialogo = new PrintDialog { Document = documento, UseEXDialog = true })
            {
                if (dialogo.ShowDialog(propietario) == DialogResult.OK)
                    documento.Print();
            }
        }

        public static bool HayImpresoraPdf()
        {
            try { return PrinterSettings.InstalledPrinters.Cast<string>().Any(p => p == ImpresoraPdf); }
            catch { return false; }
        }

        /// <summary>
        /// Genera el PDF con la impresora "Microsoft Print to PDF" de Windows (no requiere librerías externas).
        /// Se puede llamar desde un hilo secundario. Devuelve null si salió bien, o el motivo del error.
        /// </summary>
        public string GuardarPdf(string ruta)
        {
            if (!HayImpresoraPdf())
                return string.Format(T("Documento_errorSinImpresoraPdf"), ImpresoraPdf);

            try
            {
                if (File.Exists(ruta)) File.Delete(ruta);

                using (var documento = CrearDocumentoImpresion())
                {
                    documento.PrinterSettings.PrinterName = ImpresoraPdf;
                    documento.PrinterSettings.PrintToFile = true;
                    documento.PrinterSettings.PrintFileName = ruta;
                    documento.PrintController = new StandardPrintController(); // sin ventana de "Imprimiendo..."
                    UsarA4SiExiste(documento);
                    documento.Print();
                }

                // El controlador escribe el archivo al terminar la cola de impresión: se espera a que esté completo
                for (int intento = 0; intento < 60; intento++)
                {
                    if (ArchivoListo(ruta)) return null;
                    Thread.Sleep(250);
                }
                return T("Documento_errorPdfDemorado");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>Genera el PDF en un archivo temporal y devuelve su contenido (para adjuntarlo a un mail).</summary>
        public byte[] GenerarPdf(out string error)
        {
            string temporal = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}_{NombreArchivo}");
            try
            {
                error = GuardarPdf(temporal);
                return error == null ? File.ReadAllBytes(temporal) : null;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
            finally
            {
                try { if (File.Exists(temporal)) File.Delete(temporal); } catch { }
            }
        }

        private static bool ArchivoListo(string ruta)
        {
            try
            {
                if (!File.Exists(ruta) || new FileInfo(ruta).Length == 0) return false;
                using (File.Open(ruta, FileMode.Open, FileAccess.Read, FileShare.None)) return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        #endregion

        #region Ayudas de dibujo

        protected string Moneda(decimal valor) => "$ " + valor.ToString("N2", Cultura);

        protected string Fecha(DateTime fecha) => fecha.ToString("dd/MM/yyyy", Cultura);

        protected string DiaSemana(DateTime fecha) => Cultura.TextInfo.ToTitleCase(fecha.ToString("dddd", Cultura));

        protected string Hora(TimeSpan hora) => DateTime.Today.Add(hora).ToString("HH:mm");

        protected string NumeroReserva => Reserva.Id.ToString("000000");

        protected string TextoEstado(EstadoReserva estado) => T("Documento_estado" + estado);

        protected string TextoHuespedes()
        {
            string adultos = string.Format(T(Reserva.CantidadAdultos == 1 ? "Documento_adulto" : "Documento_adultos"), Reserva.CantidadAdultos);
            if (Reserva.CantidadNinos == 0) return adultos;
            string ninos = string.Format(T(Reserva.CantidadNinos == 1 ? "Documento_nino" : "Documento_ninos"), Reserva.CantidadNinos);
            return $"{adultos} · {ninos}";
        }

        protected string TextoNoches(int noches) =>
            $"{noches} {T(noches == 1 ? "RegistrarReserva_txtNoche" : "RegistrarReserva_txtNoches")}";

        protected string TextoDocumentoTitular()
        {
            var titular = Datos.Titular;
            if (titular == null) return "-";
            return $"{OpcionEnum_68SA.Texto(titular.TipoDocumento)} {titular.Documento}";
        }

        protected (Color Fondo, Color Frente) ColoresEstado(EstadoReserva estado)
        {
            switch (estado)
            {
                case EstadoReserva.Confirmada: return (VerdeClaro, Verde);
                case EstadoReserva.EnCurso: return (AzulClaro, Azul);
                case EstadoReserva.Finalizada: return (Color.FromArgb(236, 238, 242), TextoSuave);
                default: return (RojoClaro, Rojo);
            }
        }

        protected static GraphicsPath Redondeado(RectangleF r, float radio)
        {
            var path = new GraphicsPath();
            float d = Math.Min(radio * 2, Math.Min(r.Width, r.Height));
            if (d <= 0) { path.AddRectangle(r); return path; }
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected static void Rellenar(Graphics g, RectangleF r, Color color, float radio = 0)
        {
            using (var brush = new SolidBrush(color))
            {
                if (radio <= 0) g.FillRectangle(brush, r);
                else using (var path = Redondeado(r, radio)) g.FillPath(brush, path);
            }
        }

        protected static void Contorno(Graphics g, RectangleF r, Color color, float radio = 0, float grosor = 1f)
        {
            using (var pen = new Pen(color, grosor))
            {
                if (radio <= 0) g.DrawRectangle(pen, r.X, r.Y, r.Width, r.Height);
                else using (var path = Redondeado(r, radio)) g.DrawPath(pen, path);
            }
        }

        protected static void Linea(Graphics g, float x1, float y1, float x2, float y2, Color color, float grosor = 1f, bool punteada = false)
        {
            using (var pen = new Pen(color, grosor))
            {
                if (punteada) pen.DashPattern = new[] { 4f, 3f };
                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        /// <summary>Escribe un texto en un rectángulo (una línea, con "..." si no entra).</summary>
        protected static void Escribir(Graphics g, string texto, Font fuente, Color color, RectangleF zona,
            StringAlignment horizontal = StringAlignment.Near, StringAlignment vertical = StringAlignment.Near, bool varias = false)
        {
            using (var brush = new SolidBrush(color))
            using (var formato = new StringFormat
            {
                Alignment = horizontal,
                LineAlignment = vertical,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = varias ? 0 : StringFormatFlags.NoWrap
            })
            {
                g.DrawString(texto ?? string.Empty, fuente, brush, zona, formato);
            }
        }

        /// <summary>Logo del hotel (dorado sobre fondo transparente), recortado a su contenido y centrado en la zona.</summary>
        // Una imagen GDI+ no se puede usar desde dos hilos a la vez (la vista previa y el PDF que se genera
        // para el mail en segundo plano): cada hilo tiene su propia copia del logo.
        [ThreadStatic] private static Bitmap _logoDelHilo;

        protected static void DibujarLogo(Graphics g, RectangleF zona)
        {
            var logo = _logoDelHilo ?? (_logoDelHilo = Properties.Resources.HorizonLogoPuro1);
            if (logo == null) return;
            var origen = new Rectangle(11, 17, Math.Min(764, logo.Width - 11), Math.Min(312, logo.Height - 17));
            float escala = Math.Min(zona.Width / origen.Width, zona.Height / origen.Height);
            float ancho = origen.Width * escala, alto = origen.Height * escala;
            var destino = new RectangleF(zona.X + (zona.Width - ancho) / 2, zona.Y + (zona.Height - alto) / 2, ancho, alto);
            g.DrawImage(logo, destino, origen, GraphicsUnit.Pixel);
        }

        #endregion
    }
}
