using Service_08YS;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUI_08YS.Ayuda
{
    /// <summary>
    /// Ayuda en línea de todo el sistema:
    ///  - F1: ayuda contextual. Si el puntero está sobre un elemento que tiene explicación, aparece un globo
    ///    con esa explicación; si no, se abre la ventana de ayuda en el tema de la pantalla actual.
    ///  - Shift+F1 o "¿Qué es esto?": el puntero cambia a "?" y el siguiente clic muestra la ayuda del elemento.
    ///  - Índice de la ayuda (árbol de temas) y manual completo en PDF.
    /// Escucha el teclado y el mouse de toda la aplicación con un filtro de mensajes: ninguna pantalla necesita código propio.
    /// </summary>
    public static class AyudaManager_68SA
    {
        #region Campos

        private const int WM_KEYDOWN = 0x0100, WM_SYSKEYDOWN = 0x0104, WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201, WM_LBUTTONUP = 0x0202, WM_RBUTTONDOWN = 0x0204, WM_MBUTTONDOWN = 0x0207;
        private const int WM_NCLBUTTONDOWN = 0x00A1, WM_NCRBUTTONDOWN = 0x00A4, WM_NCMOUSEMOVE = 0x00A0;

        private static bool _instalado;
        private static bool _modoQueEsEsto;
        private static bool _tragarSoltar;          // se descarta el "soltar" del clic usado en el modo "¿Qué es esto?"
        private static GloboAyuda_68SA _globo;
        private static FormAyuda_68SA _ventana;

        #endregion

        #region Propiedades

        public static bool ModoQueEsEstoActivo => _modoQueEsEsto;

        #endregion

        #region Acciones (también las usa el botón "?" del MDI)

        /// <summary>Ayuda de la pantalla que se está usando (o del elemento bajo el puntero).</summary>
        public static void AyudaContextual() => AyudaEn(Cursor.Position, true);

        /// <summary>Ayuda de la pantalla actual, sin mirar dónde está el puntero.</summary>
        public static void AyudaDePantalla() => MostrarAyuda(TemaDeFormulario(FormularioActivo()));

        public static void MostrarIndice() => MostrarAyuda(null);

        public static void MostrarManual()
        {
            var ventana = Ventana();
            Traer(ventana);              // primero se muestra (su Load arma el idioma) y después el contenido
            ventana.MostrarManual();
        }

        /// <summary>Abre la ventana de ayuda en el tema indicado (null = índice) y, opcionalmente, en un elemento.</summary>
        public static void MostrarAyuda(TemaAyuda_68SA tema, ElementoAyuda_68SA elemento = null)
        {
            CerrarGlobo();
            var temaPantalla = TemaDeFormulario(FormularioActivo());
            var ventana = Ventana();
            Traer(ventana);              // primero se muestra (su Load arma el idioma) y después el tema
            ventana.MostrarTema(tema ?? AyudaContenido_68SA.Temas.FirstOrDefault(), elemento, temaPantalla);
        }

        public static void ActivarModoQueEsEsto()
        {
            CerrarGlobo();
            _modoQueEsEsto = true;
            Cursor.Current = Cursors.Help;
            // Indicación junto al puntero (desaparece con el clic)
            _globo = new GloboAyuda_68SA(T("Ayuda_globoModo"), T("Ayuda_modoTexto"), null, false);
            _globo.MostrarEn(Cursor.Position);
        }

        public static void CancelarModoQueEsEsto()
        {
            _modoQueEsEsto = false;
            Cursor.Current = Cursors.Default;
            CerrarGlobo();
        }

        #endregion

        #region Búsqueda del tema y del elemento

        /// <summary>
        /// Busca qué hay en ese punto de la pantalla: el formulario (pantalla) al que pertenece y, subiendo por los
        /// contenedores, el primer control que tenga explicación en el tema de esa pantalla.
        /// </summary>
        private static void AyudaEn(Point punto, bool abrirVentanaSiNoHayElemento)
        {
            CerrarGlobo();
            Control control = ControlEn(punto);
            TemaAyuda_68SA tema = null;
            ElementoAyuda_68SA elemento = null;

            if (control != null && !(control.FindForm() is FormAyuda_68SA) && !(control.FindForm() is GloboAyuda_68SA))
            {
                // El formulario más cercano que tenga tema (una pantalla dentro del MDI, una ventana emergente o el MDI)
                Form pantalla = null;
                for (Control c = control; c != null; c = c.Parent)
                {
                    if (c is Form f)
                    {
                        tema = TemaDeFormulario(f);
                        if (tema != null) { pantalla = f; break; }
                    }
                }
                if (tema == null)
                    tema = TemaDeFormulario(control.FindForm()?.Owner);

                if (tema != null)
                    for (Control c = control; c != null && elemento == null; c = c.Parent)
                    {
                        elemento = tema.ElementoDe(c.Name);
                        if (ReferenceEquals(c, pantalla)) break;
                    }
            }

            if (tema == null) tema = TemaDeFormulario(FormularioActivo());

            if (elemento != null)
                MostrarGlobo(tema, elemento, punto);
            else if (abrirVentanaSiNoHayElemento)
                MostrarAyuda(TemaDeFormulario(FormularioActivo()) ?? tema);   // sin elemento: la pantalla actual
        }

        private static void MostrarGlobo(TemaAyuda_68SA tema, ElementoAyuda_68SA elemento, Point punto)
        {
            CerrarGlobo();
            _globo = new GloboAyuda_68SA(elemento.Nombre, elemento.Texto, T("Ayuda_globoVerMas"));
            _globo.VerAyudaCompleta += (s, e) => MostrarAyuda(tema, elemento);
            _globo.MostrarEn(punto);
        }

        private static void CerrarGlobo()
        {
            if (_globo == null) return;
            var globo = _globo;
            _globo = null;
            if (!globo.IsDisposed) globo.Close();
        }

        /// <summary>Pantalla con la que se está trabajando: la que está abierta dentro del MDI o la ventana activa.</summary>
        private static Form FormularioActivo()
        {
            Form activo = Form.ActiveForm;
            if (activo is FormAyuda_68SA || activo is GloboAyuda_68SA || activo == null)
                activo = Application.OpenForms.Cast<Form>().LastOrDefault(f => f.Visible && !(f is FormAyuda_68SA) && !(f is GloboAyuda_68SA) && f.TopLevel);
            if (activo is FormMDI_08YS mdi && mdi.PantallaActual != null && TemaDeFormulario(mdi.PantallaActual) != null)
                return mdi.PantallaActual;
            return activo;
        }

        private static TemaAyuda_68SA TemaDeFormulario(Form formulario)
            => formulario == null ? null : AyudaContenido_68SA.TemaDePantalla(formulario.GetType().Name);

        private static Control ControlEn(Point punto)
        {
            IntPtr hwnd = WindowFromPoint(new POINT { X = punto.X, Y = punto.Y });
            Control c = hwnd == IntPtr.Zero ? null : Control.FromChildHandle(hwnd);
            // WindowFromPoint saltea los controles deshabilitados: se baja hasta el control que está realmente debajo
            for (var hijo = c?.GetChildAtPoint(c.PointToClient(punto), GetChildAtPointSkip.Invisible); hijo != null;
                 hijo = hijo.GetChildAtPoint(hijo.PointToClient(punto), GetChildAtPointSkip.Invisible))
                c = hijo;
            return c;
        }

        #endregion

        #region Ventana de ayuda

        private static FormAyuda_68SA Ventana()
        {
            // Si se abrió una ventana modal después, la de ayuda quedó deshabilitada: se crea de nuevo
            if (_ventana != null && !_ventana.IsDisposed && _ventana.IsHandleCreated && !IsWindowEnabled(_ventana.Handle))
                _ventana.Close();
            if (_ventana == null || _ventana.IsDisposed)
            {
                _ventana = new FormAyuda_68SA();
                _ventana.FormClosed += (s, e) => _ventana = null;
            }
            return _ventana;
        }

        private static void Traer(Form ventana)
        {
            if (!ventana.Visible) ventana.Show();
            if (ventana.WindowState == FormWindowState.Minimized) ventana.WindowState = FormWindowState.Normal;
            ventana.Activate();
            ventana.BringToFront();
        }

        #endregion

        #region Métodos públicos

        /// <summary>Se llama una vez en Program.Main.</summary>
        public static void Instalar()
        {
            if (_instalado) return;
            _instalado = true;
            Application.AddMessageFilter(new Filtro());
        }

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        #endregion

        #region Win32

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(POINT punto);

        [DllImport("user32.dll")]
        private static extern bool IsWindowEnabled(IntPtr hWnd);

        #endregion

        #region Tipos anidados

        /// <summary>Filtro de mensajes de la aplicación: F1, Shift+F1, clics del modo "¿Qué es esto?" y cierre del globo.</summary>
        private sealed class Filtro : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                try
                {
                    switch (m.Msg)
                    {
                        case WM_KEYDOWN:
                        case WM_SYSKEYDOWN:
                            return Tecla((Keys)(m.WParam.ToInt64() & 0xFFFF));

                        case WM_MOUSEMOVE:
                        case WM_NCMOUSEMOVE:
                            if (_modoQueEsEsto) Cursor.Current = Cursors.Help;
                            return false;

                        case WM_LBUTTONDOWN:
                        case WM_NCLBUTTONDOWN:
                            if (_modoQueEsEsto)
                            {
                                _modoQueEsEsto = false;
                                _tragarSoltar = true;
                                Cursor.Current = Cursors.Default;
                                AyudaEn(Cursor.Position, true);
                                return true;            // el clic no llega al control (no ejecuta el botón)
                            }
                            CerrarGloboSiEsAfuera(m.HWnd);
                            return false;

                        case WM_LBUTTONUP:
                            if (_tragarSoltar) { _tragarSoltar = false; return true; }
                            return false;

                        case WM_RBUTTONDOWN:
                        case WM_MBUTTONDOWN:
                        case WM_NCRBUTTONDOWN:
                            if (_modoQueEsEsto) { CancelarModoQueEsEsto(); return true; }
                            CerrarGloboSiEsAfuera(m.HWnd);
                            return false;
                    }
                }
                catch (Exception)
                {
                    // La ayuda nunca interrumpe el trabajo
                    _modoQueEsEsto = false;
                }
                return false;
            }

            private static bool Tecla(Keys tecla)
            {
                if (tecla == Keys.F1)
                {
                    if (Form.ActiveForm is FormAyuda_68SA) return false;
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) ActivarModoQueEsEsto();
                    else AyudaContextual();
                    return true;
                }
                if (_modoQueEsEsto && tecla == Keys.Escape)
                {
                    CancelarModoQueEsEsto();
                    return true;
                }
                if (_globo != null && tecla != Keys.ShiftKey && tecla != Keys.ControlKey && tecla != Keys.Menu)
                    CerrarGlobo();
                return false;
            }

            private static void CerrarGloboSiEsAfuera(IntPtr hwnd)
            {
                if (_globo == null || _globo.IsDisposed) return;
                var control = Control.FromChildHandle(hwnd);
                if (control != null && ReferenceEquals(control.FindForm(), _globo)) return;
                CerrarGlobo();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT { public int X, Y; }

        #endregion
    }
}
