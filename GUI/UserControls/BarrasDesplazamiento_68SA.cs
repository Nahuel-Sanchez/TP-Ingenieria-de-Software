using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    /// <summary>
    /// Barras de desplazamiento oscuras en toda la aplicación.
    /// Instala un hook de creación de ventanas en el hilo de la interfaz y, a cada panel, grilla,
    /// barra o cuadro de texto que se crea, le aplica el tema oscuro de Windows ("DarkMode_Explorer").
    /// Así ningún formulario necesita código propio y las barras dejan de verse blancas.
    /// En versiones de Windows sin ese tema, el sistema usa el estilo normal (no falla).
    /// También pasa a modo oscuro los menús nativos de Windows (por ejemplo, el de clic derecho
    /// de los cuadros de texto: Copiar / Pegar...), que si no aparecen como un cuadro blanco.
    /// Además avisa a TooltipsHotel_68SA de cada tooltip que se crea, para dibujarlo con el estilo del hotel.
    /// </summary>
    public static class BarrasDesplazamiento_68SA
    {
        #region Campos

        private const int WH_CBT = 5;
        private const int HCBT_CREATEWND = 3;
        private const string TemaOscuro = "DarkMode_Explorer";

        // Clases de ventana que reciben el tema: contenedores con AutoScroll, grillas (sus barras son
        // SCROLLBAR), textos multilínea y la lista desplegable de los combos nativos.
        // Los LISTBOX quedan afuera: el desplegable de IconComboBox es un ListBox dibujado a mano y con
        // el tema aplicado al crearse dejaba de mostrar el texto de las opciones.
        private static readonly string[] ClasesConBarras = { ".Window.", ".SCROLLBAR.", ".EDIT." };
        private const string ListaDeCombo = "ComboLBox";

        // El delegado se guarda en un campo estático para que el recolector de basura no lo libere
        // mientras Windows lo sigue llamando.
        private static HookProc _proc;
        private static IntPtr _hook = IntPtr.Zero;

        private const int ForzarOscuro = 2;   // PreferredAppMode.ForceDark

        #endregion

        #region Métodos públicos

        /// <summary>Se llama una vez en Program.Main, antes de abrir el primer formulario.</summary>
        public static void Instalar()
        {
            if (_hook != IntPtr.Zero) return;
            if (Environment.OSVersion.Platform != PlatformID.Win32NT) return;

            try
            {
                ActivarMenusOscuros();

                _proc = AlCrearVentana;
                _hook = SetWindowsHookEx(WH_CBT, _proc, IntPtr.Zero, GetCurrentThreadId());
                if (_hook != IntPtr.Zero)
                    Application.ApplicationExit += (s, e) => Desinstalar();
            }
            catch (Exception)
            {
                // Sin el hook la aplicación funciona igual, con las barras del sistema.
                _hook = IntPtr.Zero;
            }
        }

        public static void Desinstalar()
        {
            if (_hook == IntPtr.Zero) return;
            UnhookWindowsHookEx(_hook);
            _hook = IntPtr.Zero;
        }

        #endregion

        #region Métodos auxiliares

        /// <summary>
        /// Menús nativos oscuros (Windows 10 1809 o posterior). Usa dos funciones sin nombre público de
        /// uxtheme.dll (las mismas que usan el Explorador y el Bloc de notas); si no existen, no hace nada.
        /// </summary>
        private static void ActivarMenusOscuros()
        {
            try
            {
                if (VersionWindows() < 17763) return;
                SetPreferredAppMode(ForzarOscuro);
                FlushMenuThemes();
            }
            catch (Exception)
            {
                // Versión de Windows sin estas funciones: los menús nativos quedan con el estilo normal.
            }
        }

        private static int VersionWindows()
        {
            var info = new OSVERSIONINFOEX { dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };
            return RtlGetVersion(ref info) == 0 && info.dwMajorVersion >= 10 ? info.dwBuildNumber : 0;
        }

        private static IntPtr AlCrearVentana(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == HCBT_CREATEWND)
            {
                try
                {
                    string clase = NombreClase(wParam);
                    if (TooltipsHotel_68SA.EsTooltip(clase))
                        TooltipsHotel_68SA.Registrar(wParam);   // cuadros de ayuda con la estética del hotel
                    else if (TieneBarras(clase))
                        SetWindowTheme(wParam, TemaOscuro, null);
                }
                catch (Exception)
                {
                    // Nunca se interrumpe la creación de una ventana por el tema.
                }
            }
            return CallNextHookEx(_hook, nCode, wParam, lParam);
        }

        private static bool TieneBarras(string clase)
        {
            if (string.IsNullOrEmpty(clase)) return false;
            if (clase == ListaDeCombo) return true;
            if (!clase.StartsWith("WindowsForms10.", StringComparison.Ordinal)) return false;
            foreach (string parte in ClasesConBarras)
                if (clase.IndexOf(parte, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            return false;
        }

        private static string NombreClase(IntPtr hwnd)
        {
            var nombre = new StringBuilder(256);
            return GetClassName(hwnd, nombre, nombre.Capacity) > 0 ? nombre.ToString() : null;
        }

        #endregion

        #region Win32

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        private static extern int SetPreferredAppMode(int modo);

        [DllImport("uxtheme.dll", EntryPoint = "#136")]
        private static extern void FlushMenuThemes();

        [DllImport("ntdll.dll")]
        private static extern int RtlGetVersion(ref OSVERSIONINFOEX versionInfo);

        #endregion

        #region Tipos anidados

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public ushort wServicePackMajor;
            public ushort wServicePackMinor;
            public ushort wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        #endregion
    }
}
