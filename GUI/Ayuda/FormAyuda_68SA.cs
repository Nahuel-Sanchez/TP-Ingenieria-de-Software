using CustomControls;
using FontAwesome.Sharp;
using GUI_08YS.Reportes;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GUI_08YS.Ayuda
{
    /// <summary>
    /// Ventana de ayuda en línea: a la izquierda el índice en árbol ("menú escalera") con buscador; a la derecha el
    /// tema elegido (resumen, secciones, paso a paso, elementos de la pantalla, consejos y temas relacionados) o el
    /// manual completo en vista previa, listo para imprimir o guardar en PDF.
    /// </summary>
    public class FormAyuda_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private static readonly Color Fondo = Color.FromArgb(5, 10, 40);
        private static readonly Color Barra = Color.FromArgb(10, 18, 50);
        private static readonly Color Lateral = Color.FromArgb(8, 13, 38);
        private static readonly Color TextoSuave = Color.FromArgb(160, 165, 180);
        private const int AnchoPreferido = 1180, AltoPreferido = 760;
        private const int BordeRedimension = 5;

        private readonly Label _lblTitulo, _lblSubtitulo;
        private readonly IconButton _btnManual, _btnCerrar;
        private readonly IconPlaceholderTextBox _txtBuscar;
        private readonly TreeView _arbol;
        private readonly WebBrowser _navegador;
        private readonly VisorReporte_68SA _visor;
        private readonly Panel _pnlContenido;

        private TemaAyuda_68SA _tema;
        private TemaAyuda_68SA _temaPantalla;      // tema de la pantalla desde la que se pidió la ayuda
        private ElementoAyuda_68SA _elemento;      // elemento a resaltar dentro del tema
        private bool _mostrandoManual;
        private bool _armandoArbol;
        private List<TemaAyuda_68SA> _temasDelArbol;   // lista con la que se armó el árbol (cambia con el usuario o el idioma)

        #endregion

        #region Constructor

        public FormAyuda_68SA()
        {
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Barra;                           // el borde dorado de 1 px se dibuja en OnPaint
            Padding = new Padding(BordeRedimension);     // franja para tomar la ventana desde los bordes y cambiarle el tamaño
            DoubleBuffered = true;
            ResizeRedraw = true;
            ClientSize = new Size(AnchoPreferido, AltoPreferido);
            MinimumSize = new Size(640, 420);
            FormBorderStyle = FormBorderStyle.None;
            Font = new Font("Segoe UI", 9.5f);
            KeyPreview = true;
            ShowIcon = false;
            StartPosition = FormStartPosition.Manual;    // se ubica en Load, dentro del área de trabajo de la pantalla
            Text = "Ayuda";

            // ------------------------------------------------ contenido (Fill)
            _pnlContenido = new Panel { Dock = DockStyle.Fill, BackColor = Barra };
            Controls.Add(_pnlContenido);

            _navegador = new WebBrowser
            {
                Dock = DockStyle.Fill,
                AllowWebBrowserDrop = false,
                IsWebBrowserContextMenuEnabled = false,
                ScriptErrorsSuppressed = true,
                WebBrowserShortcutsEnabled = false   // F5 dejaría la página en blanco
            };
            _navegador.Navigating += AlNavegar;
            _navegador.DocumentCompleted += (s, e) => ResaltarElemento();
            _pnlContenido.Controls.Add(_navegador);

            _visor = new VisorReporte_68SA { Dock = DockStyle.Fill, Visible = false };
            _pnlContenido.Controls.Add(_visor);

            // ------------------------------------------------ índice (Left; el separador va antes para quedar a su derecha)
            Controls.Add(new Panel { Dock = DockStyle.Left, Width = 1, BackColor = Color.FromArgb(40, 52, 92) });
            var lateral = new Panel { Dock = DockStyle.Left, Width = 330, BackColor = Lateral, Padding = new Padding(14, 14, 0, 10) };
            Controls.Add(lateral);
            // En ventanas angostas el índice ocupa menos para dejarle lugar al contenido
            Resize += (s, e) => lateral.Width = ClientSize.Width < 1000 ? 260 : 330;
            _arbol = new TreeView
            {
                Dock = DockStyle.Fill,
                BackColor = Lateral,
                ForeColor = Color.FromArgb(215, 220, 232),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10f),
                FullRowSelect = true,
                HideSelection = false,
                ItemHeight = 30,
                Indent = 18,
                ShowLines = false,
                ShowRootLines = false,
                ShowPlusMinus = true,
                DrawMode = TreeViewDrawMode.OwnerDrawText
            };
            _arbol.DrawNode += DibujarNodo;
            _arbol.AfterSelect += (s, e) =>
            {
                if (_armandoArbol || !(e.Node?.Tag is TemaAyuda_68SA tema)) return;
                _elemento = null;
                Mostrar(tema);
            };
            lateral.Controls.Add(_arbol);
            lateral.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 10, BackColor = Lateral });
            _txtBuscar = new IconPlaceholderTextBox
            {
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(5, 15, 45), BorderColor = Color.FromArgb(150, 115, 30), BorderFocusColor = Color.Goldenrod,
                BorderWidth = 2, Font = new Font("Segoe UI", 10f), ForeColor = SystemColors.ControlLightLight,
                IconChar = IconChar.MagnifyingGlass, IconColor = Color.Goldenrod, IconSize = 20, IconPadding = 4, MaxLength = 60,
                PlaceholderColor = Color.LightGray, Size = new Size(302, 44)
            };
            _txtBuscar.InnerTextBox.TextChanged += (s, e) => ArmarArbol();
            lateral.Controls.Add(_txtBuscar);

            // ------------------------------------------------ encabezado (Top)
            var encabezado = new Panel { Dock = DockStyle.Top, Height = 66, BackColor = Barra };
            encabezado.MouseDown += Arrastrar;   // doble clic: maximizar / restaurar
            Controls.Add(encabezado);   // último en agregarse: queda arriba, a todo el ancho

            var icono = new IconPictureBox
            {
                BackColor = Barra, IconChar = IconChar.CircleQuestion, IconColor = Color.Goldenrod, IconFont = IconFont.Auto,
                IconSize = 34, Location = new Point(18, 16), Size = new Size(36, 36)
            };
            icono.MouseDown += Arrastrar;
            encabezado.Controls.Add(icono);
            _lblTitulo = new Label { AutoSize = true, BackColor = Barra, Font = new Font("Segoe UI", 14f, FontStyle.Bold), ForeColor = Color.Goldenrod, Location = new Point(62, 9), UseMnemonic = false };
            _lblTitulo.MouseDown += Arrastrar;
            encabezado.Controls.Add(_lblTitulo);
            _lblSubtitulo = new Label { AutoSize = true, BackColor = Barra, ForeColor = TextoSuave, Location = new Point(64, 38), UseMnemonic = false };
            _lblSubtitulo.MouseDown += Arrastrar;
            encabezado.Controls.Add(_lblSubtitulo);

            _btnCerrar = new IconButton
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right, BackColor = Barra, Cursor = Cursors.Hand, FlatStyle = FlatStyle.Flat,
                IconChar = IconChar.Xmark, IconColor = Color.Gold, IconSize = 24, Size = new Size(40, 40), TabStop = false,
                UseVisualStyleBackColor = false
            };
            _btnCerrar.FlatAppearance.BorderSize = 0;
            _btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(160, 30, 35);
            _btnCerrar.Click += (s, e) => Close();
            encabezado.Controls.Add(_btnCerrar);

            _btnManual = new IconButton
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right, BackColor = Color.FromArgb(5, 15, 45), Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = Color.Goldenrod,
                IconChar = IconChar.FilePdf, IconColor = Color.Gold, IconSize = 20, ImageAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText, Size = new Size(190, 40), UseVisualStyleBackColor = false
            };
            _btnManual.FlatAppearance.BorderColor = Color.Goldenrod;
            _btnManual.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 38, 86);
            _btnManual.Click += (s, e) => { if (_mostrandoManual) Mostrar(_tema ?? AyudaContenido_68SA.Temas.FirstOrDefault()); else MostrarManual(); };
            encabezado.Controls.Add(_btnManual);
            encabezado.Resize += (s, e) => AcomodarEncabezado(encabezado);

            ResumeLayout(false);

            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
            Load += (s, e) => { UbicarEnPantalla(); TraductorManager_08YS.Instance.Suscribir(this); UpdateIdioma(); AcomodarEncabezado(encabezado); };
            FormClosed += (s, e) => TraductorManager_08YS.Instance.Desuscribir(this);
        }

        #endregion

        #region Uso desde AyudaManager

        /// <summary>Muestra un tema (y resalta un elemento, si se indica). temaPantalla = pantalla desde la que se pidió.</summary>
        public void MostrarTema(TemaAyuda_68SA tema, ElementoAyuda_68SA elemento, TemaAyuda_68SA temaPantalla)
        {
            _temaPantalla = temaPantalla;
            _elemento = elemento;
            if (!ReferenceEquals(_temasDelArbol, AyudaContenido_68SA.Temas)) ArmarArbol();
            Mostrar(tema);
        }

        /// <summary>Muestra el manual completo (portada, índice escalonado y todos los temas) en la vista previa.</summary>
        public void MostrarManual()
        {
            if (!ReferenceEquals(_temasDelArbol, AyudaContenido_68SA.Temas)) ArmarArbol();
            _mostrandoManual = true;
            _navegador.Visible = false;
            _visor.Visible = true;
            ActualizarBotonManual();
            var manual = new ManualAyudaImpresor_68SA(AyudaContenido_68SA.Temas);
            if (!manual.TieneDatos) { Mostrar(null); return; }
            try
            {
                _visor.Mostrar(manual, T("Manual_archivo"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(T("Reportes_msgError"), ex.Message), T("Ayuda_titulo"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Árbol

        private void ArmarArbol()
        {
            string filtro = Normalizar(_txtBuscar.Text);
            _armandoArbol = true;
            _arbol.BeginUpdate();
            try
            {
                _arbol.Nodes.Clear();
                var temas = AyudaContenido_68SA.Temas;
                _temasDelArbol = temas;
                if (string.IsNullOrEmpty(filtro))
                {
                    foreach (var raiz in AyudaContenido_68SA.Raices)
                        _arbol.Nodes.Add(CrearNodo(raiz));
                    _arbol.ExpandAll();
                }
                else
                {
                    // Búsqueda: lista plana de los temas que contienen todas las palabras
                    var palabras = filtro.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var tema in temas.Where(t => palabras.All(p => Normalizar(t.TextoCompleto()).Contains(p))))
                        _arbol.Nodes.Add(new TreeNode($"{tema.Numero}  {tema.Titulo}") { Tag = tema, Name = tema.Id });
                    if (_arbol.Nodes.Count == 0)
                        _arbol.Nodes.Add(new TreeNode(string.Format(T("Ayuda_sinResultados"), _txtBuscar.Text.Trim())));
                }
                SeleccionarNodo(_tema);
            }
            finally
            {
                _arbol.EndUpdate();
                _armandoArbol = false;
            }
        }

        private static TreeNode CrearNodo(TemaAyuda_68SA tema)
        {
            var nodo = new TreeNode($"{tema.Numero}  {tema.Titulo}") { Tag = tema, Name = tema.Id };
            foreach (var hijo in tema.Hijos)
                nodo.Nodes.Add(CrearNodo(hijo));
            return nodo;
        }

        private void SeleccionarNodo(TemaAyuda_68SA tema)
        {
            if (tema == null) return;
            var nodo = _arbol.Nodes.Find(tema.Id, true).FirstOrDefault();
            if (nodo == null || ReferenceEquals(_arbol.SelectedNode, nodo)) return;
            bool antes = _armandoArbol;
            _armandoArbol = true;
            _arbol.SelectedNode = nodo;
            nodo.EnsureVisible();
            _armandoArbol = antes;
        }

        private void DibujarNodo(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Bounds.Width <= 0 || e.Bounds.Height <= 0) return;
            bool seleccionado = (e.State & TreeNodeStates.Selected) != 0;
            var tema = e.Node.Tag as TemaAyuda_68SA;
            bool raiz = tema != null && tema.PadreTema == null;
            var fila = new Rectangle(e.Bounds.X - 2, e.Bounds.Y + 1, _arbol.ClientSize.Width - e.Bounds.X - 6, e.Bounds.Height - 2);
            using (var fondo = new SolidBrush(seleccionado ? Color.FromArgb(24, 38, 86) : Lateral))
                e.Graphics.FillRectangle(fondo, fila);
            if (seleccionado)
                using (var barra = new SolidBrush(Color.Goldenrod))
                    e.Graphics.FillRectangle(barra, fila.X, fila.Y + 5, 3, fila.Height - 10);

            Color color = seleccionado ? Color.Gold : raiz ? Color.Goldenrod : tema == null ? TextoSuave : Color.FromArgb(215, 220, 232);
            using (var fuente = new Font(_arbol.Font, raiz || seleccionado ? FontStyle.Bold : FontStyle.Regular))
                TextRenderer.DrawText(e.Graphics, e.Node.Text, fuente, new Rectangle(fila.X + 8, fila.Y, fila.Width - 10, fila.Height),
                    color, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        #endregion

        #region Tema (HTML)

        private void Mostrar(TemaAyuda_68SA tema)
        {
            _mostrandoManual = false;
            _visor.Visible = false;
            _navegador.Visible = true;
            ActualizarBotonManual();

            _tema = tema;
            SeleccionarNodo(tema);
            _navegador.DocumentText = tema == null ? Html(T("Ayuda_sinContenido")) : HtmlDeTema(tema);
            _lblSubtitulo.Text = tema == null ? string.Empty : Migas(tema);
        }

        private static string Migas(TemaAyuda_68SA tema)
        {
            var partes = new List<string>();
            for (var t = tema; t != null; t = t.PadreTema) partes.Insert(0, t.Titulo);
            return string.Join("  ›  ", partes);
        }

        private string HtmlDeTema(TemaAyuda_68SA tema)
        {
            var h = new StringBuilder();
            h.Append("<div class='migas'>").Append(E(tema.Numero + "  ·  " + Migas(tema))).Append("</div>");
            h.Append("<h1>").Append(E(tema.Titulo)).Append("</h1>");
            if (_temaPantalla != null && ReferenceEquals(_temaPantalla, tema))
                h.Append("<div class='aqui'>&#9679; ").Append(E(T("Ayuda_pantallaActual"))).Append("</div>");
            if (!string.IsNullOrEmpty(tema.Resumen))
                h.Append("<p class='resumen'>").Append(E(tema.Resumen)).Append("</p>");

            foreach (var seccion in tema.Secciones)
            {
                h.Append("<h2>").Append(E(seccion.Titulo)).Append("</h2>");
                foreach (var parrafo in seccion.Parrafos ?? new List<string>())
                    h.Append("<p>").Append(E(parrafo)).Append("</p>");
            }

            if (tema.Pasos.Count > 0)
            {
                h.Append("<h2>").Append(E(T("Ayuda_secPasos"))).Append("</h2><ol class='pasos'>");
                foreach (var paso in tema.Pasos) h.Append("<li>").Append(E(paso)).Append("</li>");
                h.Append("</ol>");
            }

            // Solo los elementos que el usuario ve en la pantalla (según sus permisos); el id es la
            // posición en la lista completa, la misma que usa ResaltarElemento
            var elementos = tema.ElementosVisibles;
            if (elementos.Count > 0)
            {
                h.Append("<h2>").Append(E(T("Ayuda_secElementos"))).Append("</h2><table class='elem'>");
                foreach (var el in elementos)
                {
                    int i = tema.Elementos.IndexOf(el);
                    h.Append("<tr id='el").Append(i).Append("'><td class='n'>").Append(E(el.Nombre)).Append("</td><td>").Append(E(el.Texto)).Append("</td></tr>");
                }
                h.Append("</table>");
            }

            if (tema.Hijos.Count > 0)
            {
                h.Append("<h2>").Append(E(T("Ayuda_secContenido"))).Append("</h2><div class='hijos'>");
                foreach (var hijo in tema.Hijos)
                    h.Append("<a class='hijo' href='ayuda:").Append(hijo.Id).Append("'><b>").Append(E(hijo.Numero + "  " + hijo.Titulo))
                     .Append("</b><span>").Append(E(hijo.Resumen ?? string.Empty)).Append("</span></a>");
                h.Append("</div>");
            }

            if (tema.Consejos.Count > 0)
            {
                h.Append("<h2>").Append(E(T("Ayuda_secConsejos"))).Append("</h2>");
                foreach (var consejo in tema.Consejos) h.Append("<div class='consejo'>").Append(E(consejo)).Append("</div>");
            }

            var relacionados = tema.Relacionados.Select(AyudaContenido_68SA.Tema).Where(t => t != null).ToList();
            if (relacionados.Count > 0)
            {
                h.Append("<h2>").Append(E(T("Ayuda_secRelacionados"))).Append("</h2><div class='chips'>");
                foreach (var r in relacionados)
                    h.Append("<a href='ayuda:").Append(r.Id).Append("'>").Append(E(r.Titulo)).Append("</a>");
                h.Append("</div>");
            }
            return Html(h.ToString(), true);
        }

        private static string Html(string cuerpo, bool esTema = false)
        {
            if (!esTema) cuerpo = "<p class='resumen'>" + E(cuerpo) + "</p>";
            return "<!DOCTYPE html><html><head><meta charset='utf-8'><meta http-equiv='X-UA-Compatible' content='IE=edge'><style>"
                + "html,body{scrollbar-face-color:#24305e;scrollbar-track-color:#0a1232;scrollbar-arrow-color:#daa520;scrollbar-shadow-color:#0a1232;"
                + "scrollbar-highlight-color:#0a1232;scrollbar-3dlight-color:#0a1232;scrollbar-darkshadow-color:#0a1232;}"
                + "body{margin:0;padding:22px 34px 34px;background:#0a1232;color:#dfe3ee;font-family:'Segoe UI',Arial,sans-serif;font-size:10.5pt;line-height:1.55;}"
                + ".migas{color:#8f97b0;font-size:9pt;}h1{color:#daa520;font-size:20pt;margin:2px 0 8px;font-weight:600;}"
                + ".aqui{display:inline-block;background:#1b2a5e;color:#ffd76a;border:1px solid #3c4b8a;padding:2px 10px;font-size:9pt;margin-bottom:6px;}"
                + ".resumen{font-size:11.5pt;color:#f3f4f8;}h2{color:#daa520;font-size:12.5pt;border-bottom:1px solid #26336a;padding-bottom:4px;margin:24px 0 10px;font-weight:600;}"
                + "p{margin:6px 0;}ol.pasos{padding-left:22px;}ol.pasos li{margin:6px 0;}"
                + "table.elem{border-collapse:collapse;width:100%;}table.elem td{padding:8px 10px;border-bottom:1px solid #1d2856;vertical-align:top;}"
                + "table.elem td.n{color:#ffd76a;font-weight:600;width:30%;}tr.marcado td{background:#2a2a14;}"
                + ".consejo{background:#121c46;border-left:3px solid #daa520;padding:8px 12px;margin:8px 0;}"
                + "a{color:#7fb2ff;}.chips a{display:inline-block;padding:4px 14px;margin:6px 8px 0 0;border:1px solid #3c4b8a;color:#e6c35c;text-decoration:none;}"
                + ".chips a:hover{background:#1b2a5e;}a.hijo{display:block;padding:10px 14px;margin:8px 0;background:#101a44;border:1px solid #26336a;color:#dfe3ee;text-decoration:none;}"
                + "a.hijo:hover{border-color:#daa520;}a.hijo b{color:#e6c35c;display:block;}a.hijo span{color:#aab1c7;font-size:9.5pt;}"
                + "</style></head><body>" + cuerpo + "</body></html>";
        }

        private static string E(string texto) => WebUtility.HtmlEncode(texto ?? string.Empty);

        /// <summary>Los enlaces "ayuda:id" abren ese tema en la misma ventana.</summary>
        private void AlNavegar(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url?.OriginalString ?? string.Empty;
            if (url.StartsWith("about:", StringComparison.OrdinalIgnoreCase) && !url.Contains("ayuda:")) return;
            e.Cancel = true;
            int i = url.IndexOf("ayuda:", StringComparison.OrdinalIgnoreCase);
            if (i < 0) return;
            string id = url.Substring(i + 6).Trim('/', ' ');
            var tema = AyudaContenido_68SA.Tema(id);
            if (tema == null) return;
            _elemento = null;
            BeginInvoke((Action)(() => Mostrar(tema)));
        }

        /// <summary>Si la ayuda se abrió desde un elemento (globo), se lleva la vista a ese renglón y se lo marca.</summary>
        private void ResaltarElemento()
        {
            if (_elemento == null || _tema == null || _navegador.Document == null) return;
            int indice = _tema.Elementos.IndexOf(_elemento);
            var fila = indice < 0 ? null : _navegador.Document.GetElementById("el" + indice);
            if (fila == null) return;
            fila.SetAttribute("className", "marcado");
            fila.ScrollIntoView(true);
        }

        #endregion

        #region Ventana sin borde: arrastrar desde el encabezado

        private void Arrastrar(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (e.Clicks > 1) { AlternarMaximizado(); return; }
            if (WindowState == FormWindowState.Maximized) return;
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xF012, 0);
        }

        /// <summary>
        /// La ventana nunca es más grande que el área de trabajo de la pantalla (sin la barra de tareas):
        /// en pantallas chicas se achica y siempre queda entera a la vista, centrada.
        /// </summary>
        private void UbicarEnPantalla()
        {
            Form principal = Owner ?? Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Visible && !ReferenceEquals(f, this));
            Rectangle area = principal != null ? Screen.FromControl(principal).WorkingArea : Screen.FromPoint(Cursor.Position).WorkingArea;
            int ancho = Math.Max(MinimumSize.Width, Math.Min(AnchoPreferido, area.Width - 40));
            int alto = Math.Max(MinimumSize.Height, Math.Min(AltoPreferido, area.Height - 40));
            ancho = Math.Min(ancho, area.Width);
            alto = Math.Min(alto, area.Height);
            Bounds = new Rectangle(area.Left + (area.Width - ancho) / 2, area.Top + (area.Height - alto) / 2, ancho, alto);
        }

        /// <summary>Doble clic en el encabezado: ocupa toda el área de trabajo o vuelve al tamaño anterior.</summary>
        private void AlternarMaximizado()
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                return;
            }
            Screen pantalla = Screen.FromControl(this);
            Rectangle area = pantalla.WorkingArea;
            MaximizedBounds = new Rectangle(area.Left - pantalla.Bounds.Left, area.Top - pantalla.Bounds.Top, area.Width, area.Height);
            WindowState = FormWindowState.Maximized;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var borde = new Pen(Color.Goldenrod))
                e.Graphics.DrawRectangle(borde, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
        }

        /// <summary>Ventana sin borde de Windows pero con tamaño ajustable desde los bordes y las esquinas.</summary>
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            base.WndProc(ref m);
            if (m.Msg != WM_NCHITTEST || WindowState == FormWindowState.Maximized) return;

            long lParam = m.LParam.ToInt64();
            var punto = PointToClient(new Point((short)(lParam & 0xFFFF), (short)((lParam >> 16) & 0xFFFF)));
            const int zona = BordeRedimension + 2;
            bool izquierda = punto.X < zona, derecha = punto.X >= ClientSize.Width - zona;
            bool arriba = punto.Y < zona, abajo = punto.Y >= ClientSize.Height - zona;

            int resultado = arriba && izquierda ? 13 : arriba && derecha ? 14 : abajo && izquierda ? 16 : abajo && derecha ? 17
                          : izquierda ? 10 : derecha ? 11 : arriba ? 12 : abajo ? 15 : 0;
            if (resultado != 0) m.Result = (IntPtr)resultado;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            Text = T("Ayuda_titulo");
            _lblTitulo.Text = T("Ayuda_titulo");
            _txtBuscar.PlaceholderText = T("Ayuda_buscar");
            _visor.UpdateIdioma();
            ActualizarBotonManual();

            // El contenido cambia de idioma: se vuelve a armar el árbol y el tema actual (por id)
            string id = _tema?.Id;
            string idPantalla = _temaPantalla?.Id;
            int indiceElemento = _tema != null && _elemento != null ? _tema.Elementos.IndexOf(_elemento) : -1;
            _tema = id == null ? null : AyudaContenido_68SA.Tema(id);
            _temaPantalla = idPantalla == null ? null : AyudaContenido_68SA.Tema(idPantalla);
            _elemento = _tema != null && indiceElemento >= 0 && indiceElemento < _tema.Elementos.Count ? _tema.Elementos[indiceElemento] : null;
            ArmarArbol();
            if (_mostrandoManual) MostrarManual();
            else if (_tema != null) Mostrar(_tema);
        }

        #endregion

        #region Carga de datos

        private void ActualizarBotonManual()
        {
            _btnManual.Text = " " + T(_mostrandoManual ? "Ayuda_btnVolver" : "Ayuda_btnManual");
            _btnManual.IconChar = _mostrandoManual ? IconChar.ArrowLeft : IconChar.FilePdf;
            _btnManual.Width = TextRenderer.MeasureText(_btnManual.Text, _btnManual.Font).Width + 56;
            if (_btnManual.Parent != null) AcomodarEncabezado(_btnManual.Parent);
        }

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        private void AcomodarEncabezado(Control encabezado)
        {
            _btnCerrar.Location = new Point(encabezado.Width - _btnCerrar.Width - 12, 13);
            _btnManual.Location = new Point(_btnCerrar.Left - _btnManual.Width - 12, 13);
        }

        private static string Normalizar(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return string.Empty;
            var sinTildes = new StringBuilder();
            foreach (char c in texto.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sinTildes.Append(c);
            return sinTildes.ToString();
        }

        #endregion
    }
}
