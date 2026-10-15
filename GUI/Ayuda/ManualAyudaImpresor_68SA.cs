using GUI_08YS.Reportes;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;

namespace GUI_08YS.Ayuda
{
    /// <summary>
    /// Manual de usuario completo, armado con el contenido de la ayuda en línea: portada, índice escalonado
    /// ("menú escalera": capítulos y subtemas numerados 1, 1.1, 1.2... con su página) y un apartado por tema.
    /// Se muestra en la vista previa de la ventana de ayuda, desde donde se imprime o se guarda en PDF.
    /// </summary>
    public class ManualAyudaImpresor_68SA : IReporteImprimible_68SA
    {
        #region Campos

        private static readonly Color ColorMarca = Color.FromArgb(10, 18, 50);
        private static readonly Color ColorDorado = Color.FromArgb(184, 134, 11);
        private static readonly Color ColorTexto = Color.FromArgb(30, 33, 45);
        private static readonly Color ColorSuave = Color.FromArgb(100, 105, 120);
        private static readonly Color ColorBorde = Color.FromArgb(214, 218, 226);
        private static readonly Color ColorConsejo = Color.FromArgb(250, 245, 228);

        private const int AltoEncabezado = 30;
        private const int AltoPie = 30;
        private const int AltoRenglonIndice = 19;

        private readonly List<TemaAyuda_68SA> _temas;
        private readonly DateTime _generado = DateTime.Now;

        private List<List<Bloque>> _paginas;          // páginas de contenido (después de la portada y el índice)
        private Dictionary<TemaAyuda_68SA, int> _paginaDeTema;
        private int _paginasIndice;
        private int _pagina;                          // página que se está imprimiendo (0 = portada)
        private Font _fPortada, _fPortadaSub, _fCapitulo, _fTema, _fSeccion, _fTexto, _fNegrita, _fChico, _fIndice, _fIndiceRaiz;

        #endregion

        #region Propiedades

        public bool TieneDatos => _temas.Count > 0;

        private int TotalPaginas => 1 + _paginasIndice + (_paginas?.Count ?? 0);

        #endregion

        #region Constructor

        public ManualAyudaImpresor_68SA(List<TemaAyuda_68SA> temas)
        {
            _temas = temas ?? new List<TemaAyuda_68SA>();
        }

        #endregion

        #region Ciclo de impresión

        private void Iniciar()
        {
            _paginas = null;
            _pagina = 0;
            Liberar();
            _fPortada = new Font("Segoe UI", 26f, FontStyle.Bold);
            _fPortadaSub = new Font("Segoe UI", 13f);
            _fCapitulo = new Font("Segoe UI", 18f, FontStyle.Bold);
            _fTema = new Font("Segoe UI", 13.5f, FontStyle.Bold);
            _fSeccion = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            _fTexto = new Font("Segoe UI", 9.5f);
            _fNegrita = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            _fChico = new Font("Segoe UI", 8f);
            _fIndice = new Font("Segoe UI", 9.5f);
            _fIndiceRaiz = new Font("Segoe UI", 10.5f, FontStyle.Bold);
        }

        private void Liberar()
        {
            foreach (var f in new[] { _fPortada, _fPortadaSub, _fCapitulo, _fTema, _fSeccion, _fTexto, _fNegrita, _fChico, _fIndice, _fIndiceRaiz })
                f?.Dispose();
            _fPortada = _fPortadaSub = _fCapitulo = _fTema = _fSeccion = _fTexto = _fNegrita = _fChico = _fIndice = _fIndiceRaiz = null;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Rectangle area = e.MarginBounds;
            if (_paginas == null) Paginar(g, area);

            if (_pagina == 0)
                DibujarPortada(g, e.PageBounds, area);
            else if (_pagina <= _paginasIndice)
                DibujarIndice(g, area, _pagina - 1);
            else
            {
                var bloques = _paginas[_pagina - 1 - _paginasIndice];
                int y = DibujarEncabezado(g, area, bloques);
                foreach (var b in bloques)
                {
                    b.Dibujar(g, new Rectangle(area.Left, y, area.Width, b.Alto));
                    y += b.Alto;
                }
            }

            _pagina++;
            if (_pagina > 1) DibujarPie(g, area);
            e.HasMorePages = _pagina < TotalPaginas;
        }

        #endregion

        #region Paginado

        private void Paginar(Graphics g, Rectangle area)
        {
            var bloques = ArmarBloques(g, area.Width);
            int arriba = area.Top + AltoEncabezado + 8, limite = area.Bottom - AltoPie;
            _paginas = new List<List<Bloque>> { new List<Bloque>() };
            int y = arriba;
            for (int i = 0; i < bloques.Count; i++)
            {
                var b = bloques[i];
                int necesario = b.Alto;
                for (int j = i; j < bloques.Count - 1 && bloques[j].UnirConSiguiente; j++) necesario += bloques[j + 1].Alto;
                var actual = _paginas[_paginas.Count - 1];
                if (actual.Count > 0 && (b.NuevaPagina || y + necesario > limite))
                {
                    actual = new List<Bloque>();
                    _paginas.Add(actual);
                    y = arriba;
                }
                actual.Add(b);
                y += b.Alto;
                if (_paginas.Count > 800) break;
            }

            // Índice: renglones por página → cuántas páginas ocupa, y la página donde empieza cada tema
            int renglones = Math.Max(1, (limite - (area.Top + 70)) / AltoRenglonIndice);
            _paginasIndice = Math.Max(1, (int)Math.Ceiling(_temas.Count / (double)renglones));
            _paginaDeTema = new Dictionary<TemaAyuda_68SA, int>();
            for (int p = 0; p < _paginas.Count; p++)
                foreach (var b in _paginas[p].Where(x => x.Tema != null))
                    if (!_paginaDeTema.ContainsKey(b.Tema))
                        _paginaDeTema[b.Tema] = 1 + _paginasIndice + p + 1;   // portada + índice + página (1 = primera)
        }

        private List<Bloque> ArmarBloques(Graphics g, int ancho)
        {
            var lista = new List<Bloque>();
            foreach (var tema in _temas)
            {
                bool capitulo = tema.PadreTema == null;
                lista.Add(TituloTema(g, tema, capitulo, ancho));
                if (!string.IsNullOrEmpty(tema.Resumen))
                    lista.Add(Parrafo(g, tema.Resumen, _fTexto, ColorTexto, ancho, 0, 8, true));

                foreach (var seccion in tema.Secciones)
                {
                    lista.Add(Subtitulo(g, seccion.Titulo, ancho));
                    foreach (var parrafo in seccion.Parrafos ?? new List<string>())
                        lista.Add(Parrafo(g, parrafo, _fTexto, ColorTexto, ancho, 0, 5));
                }

                if (tema.Pasos.Count > 0)
                {
                    lista.Add(Subtitulo(g, T("Ayuda_secPasos"), ancho));
                    for (int i = 0; i < tema.Pasos.Count; i++)
                        lista.Add(Item(g, (i + 1) + ".", tema.Pasos[i], ancho));
                }

                var elementos = tema.ElementosVisibles;   // solo los que el usuario ve según sus permisos
                if (elementos.Count > 0)
                {
                    lista.Add(Subtitulo(g, T("Ayuda_secElementos"), ancho));
                    foreach (var el in elementos)
                        lista.Add(Elemento(g, el, ancho));
                }

                if (tema.Consejos.Count > 0)
                {
                    lista.Add(Subtitulo(g, T("Ayuda_secConsejos"), ancho));
                    foreach (var consejo in tema.Consejos)
                        lista.Add(Consejo(g, consejo, ancho));
                }

                var relacionados = tema.Relacionados.Select(AyudaContenido_68SA.Tema).Where(t => t != null).ToList();
                if (relacionados.Count > 0)
                    lista.Add(Parrafo(g, T("Ayuda_secRelacionados") + ": " + string.Join(" · ", relacionados.Select(r => r.Numero + " " + r.Titulo)),
                                      _fChico, ColorSuave, ancho, 8, 4));
                lista.Add(new Bloque { Alto = 14, Dibujar = (gr, r) => { } });
            }
            return lista;
        }

        #endregion

        #region Bloques

        private Bloque TituloTema(Graphics g, TemaAyuda_68SA tema, bool capitulo, int ancho)
        {
            Font fuente = capitulo ? _fCapitulo : _fTema;
            string texto = tema.Numero + "   " + tema.Titulo;
            int alto = (int)Math.Ceiling(g.MeasureString(texto, fuente, ancho).Height) + (capitulo ? 24 : 14);
            return new Bloque
            {
                Alto = alto,
                NuevaPagina = capitulo,
                UnirConSiguiente = true,
                Tema = tema,
                Dibujar = (gr, r) =>
                {
                    using (var marca = new SolidBrush(capitulo ? ColorMarca : ColorDorado))
                        gr.DrawString(texto, fuente, marca, new RectangleF(r.Left, r.Top + 4, r.Width, r.Height));
                    using (var linea = new Pen(capitulo ? ColorDorado : ColorBorde, capitulo ? 2f : 1f))
                        gr.DrawLine(linea, r.Left, r.Bottom - 6, r.Right, r.Bottom - 6);
                }
            };
        }

        private Bloque Subtitulo(Graphics g, string texto, int ancho) => new Bloque
        {
            Alto = (int)Math.Ceiling(g.MeasureString(texto ?? string.Empty, _fSeccion, ancho).Height) + 12,
            UnirConSiguiente = true,
            Dibujar = (gr, r) =>
            {
                using (var marca = new SolidBrush(ColorMarca))
                    gr.DrawString(texto ?? string.Empty, _fSeccion, marca, new RectangleF(r.Left, r.Top + 8, r.Width, r.Height));
            }
        };

        private Bloque Parrafo(Graphics g, string texto, Font fuente, Color color, int ancho, int arriba, int abajo, bool destacado = false)
        {
            int sangria = destacado ? 10 : 0;
            int alto = (int)Math.Ceiling(g.MeasureString(texto ?? string.Empty, fuente, ancho - sangria).Height);
            return new Bloque
            {
                Alto = alto + arriba + abajo,
                Dibujar = (gr, r) =>
                {
                    if (destacado)
                        using (var barra = new SolidBrush(ColorDorado))
                            gr.FillRectangle(barra, r.Left, r.Top + arriba + 2, 3, alto - 4);
                    using (var pincel = new SolidBrush(color))
                        gr.DrawString(texto ?? string.Empty, fuente, pincel, new RectangleF(r.Left + sangria, r.Top + arriba, r.Width - sangria, alto + 2));
                }
            };
        }

        private Bloque Item(Graphics g, string vineta, string texto, int ancho)
        {
            const int sangria = 26;
            int alto = (int)Math.Ceiling(g.MeasureString(texto ?? string.Empty, _fTexto, ancho - sangria).Height);
            return new Bloque
            {
                Alto = alto + 4,
                Dibujar = (gr, r) =>
                {
                    using (var dorado = new SolidBrush(ColorDorado))
                        gr.DrawString(vineta, _fNegrita, dorado, r.Left + 4, r.Top);
                    using (var pincel = new SolidBrush(ColorTexto))
                        gr.DrawString(texto ?? string.Empty, _fTexto, pincel, new RectangleF(r.Left + sangria, r.Top, r.Width - sangria, alto + 2));
                }
            };
        }

        private Bloque Elemento(Graphics g, ElementoAyuda_68SA el, int ancho)
        {
            int anchoNombre = (int)(ancho * 0.30);
            int altoNombre = (int)Math.Ceiling(g.MeasureString(el.Nombre ?? string.Empty, _fNegrita, anchoNombre - 10).Height);
            int altoTexto = (int)Math.Ceiling(g.MeasureString(el.Texto ?? string.Empty, _fTexto, ancho - anchoNombre - 10).Height);
            int alto = Math.Max(altoNombre, altoTexto) + 10;
            return new Bloque
            {
                Alto = alto,
                Dibujar = (gr, r) =>
                {
                    using (var borde = new Pen(ColorBorde))
                        gr.DrawLine(borde, r.Left, r.Bottom - 1, r.Right, r.Bottom - 1);
                    using (var marca = new SolidBrush(ColorMarca))
                        gr.DrawString(el.Nombre ?? string.Empty, _fNegrita, marca, new RectangleF(r.Left + 4, r.Top + 5, anchoNombre - 10, altoNombre + 2));
                    using (var pincel = new SolidBrush(ColorTexto))
                        gr.DrawString(el.Texto ?? string.Empty, _fTexto, pincel, new RectangleF(r.Left + anchoNombre, r.Top + 5, r.Width - anchoNombre - 4, altoTexto + 2));
                }
            };
        }

        private Bloque Consejo(Graphics g, string texto, int ancho)
        {
            int alto = (int)Math.Ceiling(g.MeasureString(texto ?? string.Empty, _fTexto, ancho - 24).Height);
            return new Bloque
            {
                Alto = alto + 16,
                Dibujar = (gr, r) =>
                {
                    var caja = new Rectangle(r.Left, r.Top + 3, r.Width, alto + 10);
                    using (var fondo = new SolidBrush(ColorConsejo))
                        gr.FillRectangle(fondo, caja);
                    using (var barra = new SolidBrush(ColorDorado))
                        gr.FillRectangle(barra, caja.Left, caja.Top, 3, caja.Height);
                    using (var pincel = new SolidBrush(ColorTexto))
                        gr.DrawString(texto ?? string.Empty, _fTexto, pincel, new RectangleF(caja.Left + 14, caja.Top + 5, caja.Width - 20, alto + 2));
                }
            };
        }

        #endregion

        #region Portada, índice, encabezado y pie

        private void DibujarPortada(Graphics g, Rectangle pagina, Rectangle area)
        {
            var banda = new Rectangle(pagina.Left, pagina.Top + (int)(pagina.Height * 0.30), pagina.Width, 190);
            using (var fondo = new SolidBrush(ColorMarca))
                g.FillRectangle(fondo, banda);
            using (var linea = new SolidBrush(ColorDorado))
                g.FillRectangle(linea, banda.Left, banda.Bottom - 6, banda.Width, 6);
            using (var centro = new StringFormat { Alignment = StringAlignment.Center })
            using (var dorado = new SolidBrush(Color.Goldenrod))
            using (var claro = new SolidBrush(Color.FromArgb(215, 220, 232)))
            {
                g.DrawString(T("Reporte_hotel"), _fPortadaSub, dorado, new RectangleF(banda.Left, banda.Top + 30, banda.Width, 30), centro);
                g.DrawString(T("Manual_titulo"), _fPortada, Brushes.White, new RectangleF(banda.Left, banda.Top + 64, banda.Width, 50), centro);
                g.DrawString(T("Manual_subtitulo"), _fPortadaSub, claro, new RectangleF(banda.Left, banda.Top + 118, banda.Width, 30), centro);
            }
            // El manual incluye solo lo que el usuario puede usar: la portada dice para quién se generó
            var sesion = SessionManager_08YS.Instance.Current;
            if (sesion != null)
                using (var marca = new SolidBrush(ColorMarca))
                using (var centro = new StringFormat { Alignment = StringAlignment.Center })
                    g.DrawString(string.Format(T("Manual_edicionPara"), $"{sesion.Nombre} {sesion.Apellido}".Trim(), sesion.Rol?.Nombre ?? "-"),
                        _fTexto, marca, new RectangleF(area.Left, banda.Bottom + 24, area.Width, 40), centro);
            using (var suave = new SolidBrush(ColorSuave))
            using (var centro = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString(string.Format(T("Manual_generado"), _generado.ToString("dd/MM/yyyy HH:mm")), _fTexto, suave,
                    new RectangleF(area.Left, area.Bottom - 40, area.Width, 20), centro);
        }

        /// <summary>Índice escalonado: cada nivel con más sangría, número, título, puntos guía y página.</summary>
        private void DibujarIndice(Graphics g, Rectangle area, int paginaIndice)
        {
            int y = area.Top;
            if (paginaIndice == 0)
            {
                using (var marca = new SolidBrush(ColorMarca))
                    g.DrawString(T("Manual_indice"), _fCapitulo, marca, area.Left, y);
                using (var linea = new Pen(ColorDorado, 2f))
                    g.DrawLine(linea, area.Left, y + 40, area.Right, y + 40);
            }
            y += 56;
            int limite = area.Bottom - AltoPie;
            int renglones = Math.Max(1, (limite - (area.Top + 70)) / AltoRenglonIndice);
            var temas = _temas.Skip(paginaIndice * renglones).Take(renglones);

            using (var texto = new SolidBrush(ColorTexto))
            using (var marca = new SolidBrush(ColorMarca))
            using (var suave = new SolidBrush(ColorSuave))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            using (var puntos = new Pen(ColorBorde) { DashStyle = DashStyle.Dot })
            {
                foreach (var tema in temas)
                {
                    bool raiz = tema.PadreTema == null;
                    Font fuente = raiz ? _fIndiceRaiz : _fIndice;
                    int sangria = tema.Nivel * 24;
                    string etiqueta = tema.Numero + "   " + tema.Titulo;
                    int pagina = _paginaDeTema != null && _paginaDeTema.TryGetValue(tema, out int p) ? p : 0;
                    float anchoTexto = g.MeasureString(etiqueta, fuente).Width;
                    float x = area.Left + sangria;
                    g.DrawString(etiqueta, fuente, raiz ? marca : texto, x, y);
                    float inicioPuntos = x + anchoTexto + 4, finPuntos = area.Right - 34;
                    if (finPuntos > inicioPuntos)
                        g.DrawLine(puntos, inicioPuntos, y + AltoRenglonIndice - 6, finPuntos, y + AltoRenglonIndice - 6);
                    g.DrawString(pagina > 0 ? pagina.ToString() : "", fuente, raiz ? marca : suave, new RectangleF(area.Left, y, area.Width, AltoRenglonIndice), derecha);
                    y += AltoRenglonIndice;
                }
            }
        }

        private int DibujarEncabezado(Graphics g, Rectangle area, List<Bloque> bloques)
        {
            // Capítulo al que pertenece la página (el del primer tema que aparece, o el anterior)
            string capitulo = T("Manual_titulo");
            var primero = bloques.FirstOrDefault(b => b.Tema != null)?.Tema;
            for (var t = primero; t != null; t = t.PadreTema) capitulo = t.Numero + "  " + t.Titulo;
            using (var suave = new SolidBrush(ColorSuave))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            {
                g.DrawString(T("Reporte_hotel") + " · " + T("Manual_titulo"), _fChico, suave, area.Left, area.Top);
                if (primero != null)
                    g.DrawString(capitulo, _fChico, suave, new RectangleF(area.Left, area.Top, area.Width, 16), derecha);
            }
            using (var linea = new Pen(ColorBorde))
                g.DrawLine(linea, area.Left, area.Top + 18, area.Right, area.Top + 18);
            return area.Top + AltoEncabezado + 8;
        }

        private void DibujarPie(Graphics g, Rectangle area)
        {
            int y = area.Bottom - AltoPie + 10;
            using (var linea = new Pen(ColorBorde))
                g.DrawLine(linea, area.Left, y, area.Right, y);
            using (var suave = new SolidBrush(ColorSuave))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
                g.DrawString(string.Format(T("Reporte_pagina"), _pagina, TotalPaginas), _fChico, suave,
                    new RectangleF(area.Left, y + 4, area.Width, 16), derecha);
        }

        #endregion

        #region Métodos públicos

        public PrintDocument CrearDocumento()
        {
            var documento = new PrintDocument { DocumentName = T("Manual_titulo") };
            documento.DefaultPageSettings.Landscape = false;
            documento.DefaultPageSettings.Margins = new Margins(60, 60, 50, 50);
            documento.BeginPrint += (s, e) => Iniciar();
            documento.EndPrint += (s, e) => Liberar();
            documento.PrintPage += ImprimirPagina;
            return documento;
        }

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        #endregion

        #region Tipos anidados

        private sealed class Bloque
        {
            public int Alto;
            public Action<Graphics, Rectangle> Dibujar;
            public bool NuevaPagina;          // los capítulos empiezan en página nueva
            public bool UnirConSiguiente;     // títulos: no quedan solos al pie de la página
            public TemaAyuda_68SA Tema;       // el bloque es el título de este tema (para el índice)
        }

        #endregion
    }
}
