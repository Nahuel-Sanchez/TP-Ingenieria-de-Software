using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    /// <summary>
    /// Cuadros de ayuda (tooltips) con la estética del hotel en toda la aplicación: fondo azul noche,
    /// borde dorado y texto claro, en lugar del cuadro blanco de Windows.
    /// El hook de creación de ventanas (BarrasDesplazamiento_68SA) avisa cada vez que se crea un tooltip
    /// (los de los botones, los de las celdas de las grillas, los de las barras de herramientas...).
    /// Cuando la aplicación queda libre se toma esa ventana y se la dibuja a mano; no hace falta cambiar
    /// nada en los formularios. Si por algún motivo no se puede leer el texto, se deja el dibujo normal.
    /// </summary>
    public static class TooltipsHotel_68SA
    {
        #region Campos

        internal static readonly Color Fondo = Color.FromArgb(10, 18, 50);
        internal static readonly Color Borde = Color.FromArgb(150, 115, 30);
        internal static readonly Color Texto = Color.FromArgb(228, 231, 240);
        internal static readonly Font Fuente = new Font("Segoe UI", 9.5f, FontStyle.Regular);

        private static readonly List<IntPtr> _pendientes = new List<IntPtr>();
        private static readonly HashSet<VentanaTooltip> _activos = new HashSet<VentanaTooltip>();
        private static IntPtr _hFuente = IntPtr.Zero;
        private static bool _escuchandoIdle;

        #endregion

        #region Win32

        private const int WM_SETFONT = 0x0030;
        private const int WM_GETTEXT = 0x000D;
        private const int WM_GETTEXTLENGTH = 0x000E;
        private const int WM_PAINT = 0x000F;
        private const int WM_ERASEBKGND = 0x0014;
        private const int WM_PRINTCLIENT = 0x0318;
        private const int WM_WINDOWPOSCHANGING = 0x0046;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int RellenoX = 11;
        private const int RellenoY = 7;

        [StructLayout(LayoutKind.Sequential)]
        private struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x, y, cx, cy;
            public int flags;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        private const int TTM_SETMARGIN = 0x0400 + 26;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        private static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref RECT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, StringBuilder lParam);

        #endregion

        #region Eventos de controles

        private static void AlQuedarLibre(object sender, EventArgs e)
        {
            if (_pendientes.Count == 0) return;
            var lote = _pendientes.ToArray();
            _pendientes.Clear();

            foreach (IntPtr hwnd in lote)
            {
                try
                {
                    if (!IsWindow(hwnd)) continue;
                    var ventana = new VentanaTooltip();
                    ventana.AssignHandle(hwnd);
                    _activos.Add(ventana);
                    Configurar(hwnd);
                }
                catch (Exception)
                {
                    // Ese tooltip queda con el estilo de Windows; la aplicación sigue igual.
                }
            }
        }

        #endregion

        #region Métodos públicos

        /// <summary>Clase de ventana de los tooltips (nativa o la que registra WinForms a partir de ella).</summary>
        internal static bool EsTooltip(string clase)
            => clase != null && clase.IndexOf("tooltips_class32", StringComparison.OrdinalIgnoreCase) >= 0;

        /// <summary>
        /// Se llama desde el hook, mientras la ventana todavía se está creando. No se la toma en ese
        /// momento: WinForms engancha su propio procedimiento con el primer mensaje y pisaría el nuestro.
        /// Se espera a que la aplicación quede libre (los tooltips recién se muestran al pasar el mouse).
        /// </summary>
        internal static void Registrar(IntPtr hwnd)
        {
            _pendientes.Add(hwnd);
            if (_escuchandoIdle) return;
            _escuchandoIdle = true;
            Application.Idle += AlQuedarLibre;
        }

        #endregion

        #region Métodos auxiliares

        private static void Configurar(IntPtr hwnd)
        {
            if (_hFuente == IntPtr.Zero) _hFuente = Fuente.ToHfont();
            // Misma fuente con la que se dibuja: el tooltip calcula su tamaño con ella y el texto entra justo
            SendMessage(hwnd, WM_SETFONT, _hFuente, IntPtr.Zero);
            var margen = new RECT { Left = 9, Top = 6, Right = 9, Bottom = 6 };
            SendMessage(hwnd, TTM_SETMARGIN, IntPtr.Zero, ref margen);
        }

        private static void Liberar(VentanaTooltip ventana) => _activos.Remove(ventana);

        #endregion

        #region Tipos anidados

        private sealed class VentanaTooltip : NativeWindow
        {
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case WM_ERASEBKGND:
                        m.Result = (IntPtr)1;
                        return;

                    case WM_PAINT:
                        if (Pintar()) { m.Result = IntPtr.Zero; return; }
                        break;

                    case WM_PRINTCLIENT:
                        if (Dibujar(m.WParam, LeerTexto())) { m.Result = IntPtr.Zero; return; }
                        break;

                    case WM_WINDOWPOSCHANGING:
                        base.WndProc(ref m);
                        AjustarTamaño(m.LParam);
                        return;
                }
                base.WndProc(ref m);
            }

            /// <summary>
            /// Windows calcula el tamaño del cuadro con su propia fuente y márgenes; acá se lo agranda para
            /// que entre el texto completo con la fuente y el relleno del hotel (si no, queda cortado).
            /// </summary>
            private void AjustarTamaño(IntPtr lParam)
            {
                try
                {
                    string texto = LeerTexto();
                    if (string.IsNullOrEmpty(texto) || lParam == IntPtr.Zero) return;
                    var pos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));

                    Size medida = TextRenderer.MeasureText(texto, Fuente, new Size(int.MaxValue, int.MaxValue),
                                                           TextFormatFlags.NoPrefix | TextFormatFlags.ExpandTabs);
                    // Diferencia entre la ventana y su área interior (borde, si lo tiene)
                    int bordeX = 0, bordeY = 0;
                    if (GetWindowRect(Handle, out RECT ventana) && GetClientRect(Handle, out RECT cliente))
                    {
                        bordeX = Math.Max(0, (ventana.Right - ventana.Left) - (cliente.Right - cliente.Left));
                        bordeY = Math.Max(0, (ventana.Bottom - ventana.Top) - (cliente.Bottom - cliente.Top));
                    }
                    int ancho = medida.Width + RellenoX * 2 + bordeX;
                    int alto = medida.Height + RellenoY * 2 + bordeY;

                    bool sinTamaño = (pos.flags & SWP_NOSIZE) != 0;
                    int anchoActual = sinTamaño ? ventana.Right - ventana.Left : pos.cx;
                    int altoActual = sinTamaño ? ventana.Bottom - ventana.Top : pos.cy;
                    if (ancho <= anchoActual && alto <= altoActual) return;

                    pos.cx = Math.Max(ancho, anchoActual);
                    pos.cy = Math.Max(alto, altoActual);
                    pos.flags &= ~SWP_NOSIZE;

                    // Que no se salga de la pantalla por la derecha ni por abajo
                    if ((pos.flags & SWP_NOMOVE) == 0)
                    {
                        Rectangle area = Screen.FromPoint(new Point(pos.x, pos.y)).WorkingArea;
                        if (pos.x + pos.cx > area.Right) pos.x = Math.Max(area.Left, area.Right - pos.cx);
                        if (pos.y + pos.cy > area.Bottom) pos.y = Math.Max(area.Top, area.Bottom - pos.cy);
                    }
                    Marshal.StructureToPtr(pos, lParam, false);
                }
                catch (Exception)
                {
                    // Si algo falla, el cuadro queda con el tamaño que calculó Windows
                }
            }

            protected override void OnHandleChange()
            {
                base.OnHandleChange();
                if (Handle == IntPtr.Zero) Liberar(this);
            }

            private bool Pintar()
            {
                string texto = LeerTexto();
                if (string.IsNullOrEmpty(texto)) return false;   // se deja el dibujo normal de Windows

                var ps = new PAINTSTRUCT();
                IntPtr hdc = BeginPaint(Handle, ref ps);
                try
                {
                    Dibujar(hdc, texto);
                    return true;
                }
                finally
                {
                    EndPaint(Handle, ref ps);
                }
            }

            private string LeerTexto()
            {
                int largo = (int)SendMessage(Handle, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                if (largo <= 0) return null;
                var texto = new StringBuilder(largo + 1);
                SendMessage(Handle, WM_GETTEXT, (IntPtr)texto.Capacity, texto);
                return texto.ToString();
            }

            private bool Dibujar(IntPtr hdc, string texto)
            {
                if (hdc == IntPtr.Zero || string.IsNullOrEmpty(texto)) return false;
                if (!GetClientRect(Handle, out RECT rc)) return false;
                var area = new Rectangle(0, 0, rc.Right - rc.Left, rc.Bottom - rc.Top);
                if (area.Width <= 0 || area.Height <= 0) return false;

                using (var g = Graphics.FromHdc(hdc))
                {
                    using (var fondo = new SolidBrush(Fondo))
                        g.FillRectangle(fondo, area);
                    using (var borde = new Pen(Borde))
                        g.DrawRectangle(borde, 0, 0, area.Width - 1, area.Height - 1);

                    const TextFormatFlags formato = TextFormatFlags.NoPrefix | TextFormatFlags.ExpandTabs | TextFormatFlags.Left;
                    Size medida = TextRenderer.MeasureText(g, texto, Fuente, new Size(area.Width, int.MaxValue), formato);
                    var destino = new Rectangle(Math.Max(4, (area.Width - medida.Width) / 2),
                                                Math.Max(2, (area.Height - medida.Height) / 2),
                                                Math.Min(area.Width - 4, medida.Width + 2), medida.Height);
                    TextRenderer.DrawText(g, texto, Fuente, destino, Texto, formato);
                }
                return true;
            }
        }

        #endregion
    }
}
