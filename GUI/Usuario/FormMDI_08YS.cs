using BLL_08YS;
using BLL_08YS.Negocio;
using FontAwesome.Sharp;
using GUI_08YS.Admin;
using GUI_08YS.Ayuda;
using GUI_08YS.Recepcionista;
using GUI_08YS.Recepcionista.Dashboard;
using GUI_08YS.Reportes;
using GUI_08YS.UserControls;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_08YS
{
    public partial class FormMDI_08YS : Form,IIdiomaObserver_08YS
    {
        #region Campos

        private UserBLL_08YS _userBLL;
        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();

        #endregion

        #region Eventos

        public event Action CerrarSesion;

        #endregion

        #region Constructor

        public FormMDI_08YS()
        {
            InitializeComponent();
            AsignarIconosMenus();
            ConfigurarNavegacion();
            _userBLL = BLLFactory_08YS.CreateUserBLL();
            lblRolSistema.Text = SessionManager_08YS.Instance.Current.Rol.Nombre;
            lblNombreApellido.Text= SessionManager_08YS.Instance.Current.Nombre + " " + SessionManager_08YS.Instance.Current.Apellido;
            picAvatar.Image = CrearAvatar(SessionManager_08YS.Instance.Current.Nombre, SessionManager_08YS.Instance.Current.Apellido);
            ConfigurarCombo();
            AplicarPermisos();
            TraductorManager_08YS.Instance.CambiarIdioma(SessionManager_08YS.Instance.Current.Idioma);
            TraductorManager_08YS.Instance.Suscribir(this);
            SessionManager_08YS.Instance.SesionInvalidada += OnSesionInvalidada;
            UpdateIdioma();

            ProcesarNoShows();
            timerNoShow.Tick += (s, e) => ProcesarNoShows();
            timerNoShow.Start();

            ActualizarReloj();
            timerReloj.Tick += (s, e) => ActualizarReloj();
            timerReloj.Start();
        }

        #endregion

        #region Menús

        /// <summary>Ícono dorado para cada opción de los menús desplegables.</summary>
        private void AsignarIconosMenus()
        {
            var iconos = new (ToolStripMenuItem Item, IconChar Icono)[]
            {
                (gestionUsuarioToolStripMenuItem,             IconChar.UsersGear),
                (bitacoraToolStripMenuItem,                   IconChar.ClipboardList),
                (gestionAccesosToolStripMenuItem,             IconChar.ShieldHalved),
                (rolesToolStripMenuItem,                      IconChar.UserTag),
                (familiasToolStripMenuItem,                   IconChar.LayerGroup),
                (gestionRespaldosToolStripMenuItem,           IconChar.Database),
                (cambiarContraseñaToolStripMenuItem,          IconChar.Key),
                (ReLoginToolStripMenuItem,                    IconChar.RightToBracket),
                (cerrarSesionToolStripMenuItem1,              IconChar.RightFromBracket),
                (reservarToolStripMenuItem,                   IconChar.CalendarPlus),
                (controlDeHabitacionesToolStripMenuItem,      IconChar.TableCells),
                (checkInToolStripMenuItem,                    IconChar.DoorOpen),
                (checkOutToolStripMenuItem,                   IconChar.DoorClosed),
                (cambioHabitacionToolStripMenuItem,           IconChar.Retweet),
                (servicioHabitacionToolStripMenuItem,         IconChar.CirclePlus),
                (pedidosRoomServiceToolStripMenuItem,         IconChar.ListCheck),
                (cartaRoomServiceToolStripMenuItem,           IconChar.Utensils),
                (habitacionesToolStripMenuItem,               IconChar.Bed),
                (tiposDeHabitacionToolStripMenuItem,          IconChar.Tags),
                (pisosToolStripMenuItem,                      IconChar.Building),
                (ayudaPantallaToolStripMenuItem,              IconChar.CircleQuestion),
                (queEsEstoToolStripMenuItem,                  IconChar.ArrowPointer),
                (indiceAyudaToolStripMenuItem,                IconChar.Sitemap),
                (manualAyudaToolStripMenuItem,                IconChar.FilePdf),
            };
            foreach (var (item, icono) in iconos)
                DropdownMenuStrip_08YS.AsignarIcono(item, icono);
        }

        #endregion

        #region Navegación (botón activo y sección actual)

        private static readonly Color FondoLateral = Color.FromArgb(8, 13, 38);
        private static readonly Color FondoActivo = Color.FromArgb(20, 32, 76);
        private static readonly Color TextoNavegacion = Color.FromArgb(205, 210, 225);
        private static readonly Font FuenteNavegacion = new Font("Segoe UI", 10.5f, FontStyle.Regular);
        // El activo se distingue por el color, el fondo y la barra dorada: misma fuente para que el texto
        // ocupe lo mismo y no se parta en dos renglones (por ejemplo "Configuración del hotel")
        private static readonly Font FuenteNavegacionActiva = FuenteNavegacion;

        private IconButton _botonActivo;
        private ToolStripItem _itemActivo;          // opción del menú desplegable que abrió la pantalla actual
        private object _pantallaMarcada;

        // Botón del panel lateral al que pertenece cada menú desplegable
        private Dictionary<ContextMenuStrip, IconButton> _botonDeMenu;

        private IEnumerable<IconButton> BotonesNavegacion => flowLayoutPanel1.Controls.OfType<IconButton>();

        private void ConfigurarNavegacion()
        {
            _botonDeMenu = new Dictionary<ContextMenuStrip, IconButton>
            {
                { AdministrativoDropDownMenu, btnAdministrativo },
                { PerfilDropDownMenu,         btnPerfil         },
                { RecepcionDropDownMenu,      btnRecepcion      },
                { RoomServiceDropDownMenu,    btnRoomService    },
                { HabitacionesDropdownMenu,   btnHabitaciones   },
            };

            // Cuando una opción de un menú abre una pantalla nueva, su botón queda marcado como activo.
            // Se engancha después del Click del diseñador, así ya se sabe si la pantalla cambió
            // (las opciones que abren una ventanita, como el pedido de room service, no cambian nada).
            foreach (var par in _botonDeMenu)
                foreach (ToolStripItem item in par.Key.Items)
                    EngancharSeccion(item, par.Value);

            foreach (var boton in BotonesNavegacion)
                boton.Paint += PintarIndicador;

            // Los botones ocupan todo el ancho disponible (sin barra horizontal aunque aparezca la vertical)
            flowLayoutPanel1.Layout += (s, e) => AjustarAnchoBotones();
            // Al aparecer la barra vertical cambia el ancho útil: se vuelve a acomodar (y nunca hay barra horizontal)
            flowLayoutPanel1.ClientSizeChanged += (s, e) => flowLayoutPanel1.PerformLayout();
            LayoutAdaptable_68SA.OcultarScrollHorizontal(flowLayoutPanel1);
            btnMenuLateral.Click += (s, e) => AlternarMenuLateral();
            Disposed += (s, e) => _tipsMenu.Dispose();
            Resize += (s, e) => button2.IconChar = WindowState == FormWindowState.Maximized ? IconChar.WindowRestore : IconChar.WindowMaximize;
        }

        private void EngancharSeccion(ToolStripItem item, IconButton boton)
        {
            item.Click += (s, e) =>
            {
                if (ReferenceEquals(panel2.Tag, _pantallaMarcada)) return;
                MarcarSeccion(boton, item);
            };
            if (item is ToolStripMenuItem conSubmenu)
                foreach (ToolStripItem sub in conSubmenu.DropDownItems)
                    EngancharSeccion(sub, boton);
        }

        private bool _ajustandoMenu;
        private bool _menuRetraido;
        private int _anchoLateralDiseno;          // ancho del panel lateral desplegado (el del diseñador, ya escalado)
        private int _izquierdaAvatar = -1;        // posición del avatar con el menú desplegado
        private readonly ToolTip _tipsMenu = new ToolTip();

        private const int AltoBotonMenu = 46;
        private const int AltoBotonMenuMinimo = 34;
        private const int AltoSegundaLinea = 20;  // lo que crece un botón cuyo texto ocupa dos renglones
        private const int AnchoMenuRetraido = 64;

        /// <summary>
        /// Acomoda el panel lateral al espacio disponible:
        /// - los botones ocupan todo el ancho, sin barra horizontal;
        /// - si el texto de un botón no entra en un renglón (por ejemplo "Configuración del hotel"),
        ///   el botón se hace más alto y el texto pasa a dos renglones;
        /// - en pantallas bajas los botones se compactan para que entren todos sin barra vertical
        ///   (la barra aparece solo si ni compactados entran).
        /// </summary>
        private void AjustarAnchoBotones()
        {
            if (_ajustandoMenu || !flowLayoutPanel1.IsHandleCreated) return;
            _ajustandoMenu = true;
            flowLayoutPanel1.SuspendLayout();
            try
            {
                int ancho = flowLayoutPanel1.ClientSize.Width - 16;
                if (ancho <= 0) return;
                foreach (Control c in flowLayoutPanel1.Controls)
                    if (c.Width != ancho) c.Width = ancho;

                var botones = flowLayoutPanel1.Controls.OfType<IconButton>().Where(b => b.Visible).ToList();
                var grupos = flowLayoutPanel1.Controls.OfType<Label>().Where(l => l.Visible).ToList();
                if (!flowLayoutPanel1.Visible || botones.Count == 0) return;

                // Botones cuyo texto no entra en un renglón: se parte en dos, con la misma sangría en ambos
                var dosRenglones = new HashSet<IconButton>();
                if (!_menuRetraido)
                    foreach (var b in botones)
                    {
                        string texto = b.Tag is string clave ? TraductorManager_08YS.Instance.GetTexto(clave) : b.Text.Trim();
                        int anchoTexto = ancho - b.Padding.Horizontal - b.IconSize - 14;
                        string partido = PartirEnDosRenglones(texto, anchoTexto);
                        if (partido.Contains("\n")) dosRenglones.Add(b);
                        if (b.Text != partido) b.Text = partido;
                    }
                int extra = dosRenglones.Count * AltoSegundaLinea;

                // Alto: normal (46) si entra; si no, lo justo hasta un mínimo de 34
                int disponible = flowLayoutPanel1.ClientSize.Height - flowLayoutPanel1.Padding.Vertical - extra;
                int altoGrupoNormal = _menuRetraido ? 10 + 4 : 24 + 12;
                bool compacto = botones.Count * (AltoBotonMenu + 2) + grupos.Count * altoGrupoNormal > disponible;
                int altoGrupo = _menuRetraido ? 10 : compacto ? 18 : 24;
                var margenGrupo = _menuRetraido || compacto ? new Padding(8, 4, 8, 0) : new Padding(8, 10, 8, 2);
                int altoBoton = AltoBotonMenu;
                if (compacto)
                {
                    int resto = disponible - grupos.Count * (altoGrupo + margenGrupo.Vertical);
                    altoBoton = Math.Max(AltoBotonMenuMinimo, Math.Min(AltoBotonMenu, resto / botones.Count - 2));
                }
                foreach (var b in botones)
                {
                    int alto = altoBoton + (dosRenglones.Contains(b) ? AltoSegundaLinea : 0);
                    if (b.Height != alto) b.Height = alto;
                }
                foreach (var g in grupos)
                {
                    if (g.Height != altoGrupo) g.Height = altoGrupo;
                    if (g.Margin != margenGrupo) g.Margin = margenGrupo;
                }
            }
            finally
            {
                flowLayoutPanel1.ResumeLayout(false);
                _ajustandoMenu = false;
            }
        }

        /// <summary>
        /// Texto del botón con la separación del ícono ("  "). Si no entra en el ancho, se corta en la última
        /// palabra que entra y el segundo renglón lleva la misma separación (así ambos quedan alineados).
        /// </summary>
        private static string PartirEnDosRenglones(string texto, int ancho)
        {
            texto = (texto ?? string.Empty).Trim();
            const string sangria = "  ";
            if (TextRenderer.MeasureText(sangria + texto, FuenteNavegacionActiva).Width <= ancho) return sangria + texto;

            var palabras = texto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int cuantas = 1;
            while (cuantas < palabras.Length - 1 &&
                   TextRenderer.MeasureText(sangria + string.Join(" ", palabras.Take(cuantas + 1)), FuenteNavegacionActiva).Width <= ancho)
                cuantas++;
            if (palabras.Length < 2) return sangria + texto;
            return sangria + string.Join(" ", palabras.Take(cuantas)) + "\n" + sangria + string.Join(" ", palabras.Skip(cuantas));
        }

        /// <summary>
        /// Menú lateral desplegado (ícono y texto) o retraído (solo íconos, con el nombre en un tooltip).
        /// Se alterna con el botón de las tres rayas de arriba del menú; deja más lugar a la pantalla abierta.
        /// </summary>
        private void AplicarModoMenu()
        {
            if (_anchoLateralDiseno == 0) _anchoLateralDiseno = panelLateral.Width;
            if (_izquierdaAvatar < 0) _izquierdaAvatar = picAvatar.Left;
            bool r = _menuRetraido;

            SuspendLayout();
            foreach (var boton in flowLayoutPanel1.Controls.OfType<IconButton>().Concat(new[] { btnCerrarSesion }))
            {
                string texto = boton.Tag is string clave ? TraductorManager_08YS.Instance.GetTexto(clave) : boton.Text.Trim();
                boton.Text = r ? string.Empty : "  " + texto;
                boton.TextImageRelation = r ? TextImageRelation.Overlay : TextImageRelation.ImageBeforeText;
                if (!ReferenceEquals(boton, btnCerrarSesion))
                {
                    boton.ImageAlign = r ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
                    boton.Padding = r ? Padding.Empty : new Padding(18, 0, 0, 0);
                }
                _tipsMenu.SetToolTip(boton, r ? texto : null);
            }
            foreach (var grupo in flowLayoutPanel1.Controls.OfType<Label>())
                grupo.Text = r || !(grupo.Tag is string clave) ? string.Empty : TraductorManager_08YS.Instance.GetTexto(clave);

            lblNombreApellido.Visible = lblRolSistema.Visible = !r;
            picAvatar.Left = r ? (AnchoMenuRetraido - picAvatar.Width) / 2 : _izquierdaAvatar;
            panel3.Padding = r ? new Padding(8, 10, 8, 12) : new Padding(12, 10, 12, 12);
            panelLateral.Width = r ? AnchoMenuRetraido : _anchoLateralDiseno;

            _tipsMenu.SetToolTip(btnMenuLateral, TraductorManager_08YS.Instance.GetTexto(r ? "MDI_tipExpandirMenu" : "MDI_tipRetraerMenu"));
            ResumeLayout(true);
            flowLayoutPanel1.PerformLayout();
        }

        private void AlternarMenuLateral()
        {
            _menuRetraido = !_menuRetraido;
            AplicarModoMenu();
        }

        /// <summary>Marca el botón de la sección actual y muestra "Sección › Pantalla" en la barra superior.</summary>
        private void MarcarSeccion(IconButton boton, ToolStripItem item = null)
        {
            _pantallaMarcada = panel2.Tag;
            _botonActivo = boton;
            _itemActivo = item;

            foreach (var b in BotonesNavegacion)
            {
                bool activo = ReferenceEquals(b, boton);
                b.BackColor = activo ? FondoActivo : FondoLateral;
                b.ForeColor = activo ? Color.Gold : TextoNavegacion;
                b.IconColor = activo ? Color.Gold : Color.Goldenrod;
                b.Font = activo ? FuenteNavegacionActiva : FuenteNavegacion;
                b.Invalidate();
            }
            ActualizarTextoSeccion();
        }

        private void ActualizarTextoSeccion()
        {
            if (_botonActivo == null) { lblSeccion.Text = string.Empty; return; }
            // Con el menú retraído el botón no tiene texto: el nombre se toma de su clave de traducción
            string seccion = _botonActivo.Tag is string clave ? TraductorManager_08YS.Instance.GetTexto(clave) : _botonActivo.Text.Trim();
            lblSeccion.Text = _itemActivo == null ? seccion : $"{seccion}  ›  {_itemActivo.Text}";
        }

        // Barra dorada a la izquierda del botón activo
        private void PintarIndicador(object sender, PaintEventArgs e)
        {
            if (!ReferenceEquals(sender, _botonActivo)) return;
            var boton = (Control)sender;
            using (var barra = new SolidBrush(Color.Goldenrod))
                e.Graphics.FillRectangle(barra, 0, 8, 4, boton.Height - 16);
        }

        /// <summary>Círculo dorado con las iniciales del usuario.</summary>
        private static Image CrearAvatar(string nombre, string apellido)
        {
            string iniciales = ((string.IsNullOrWhiteSpace(nombre) ? "" : nombre.Trim().Substring(0, 1)) +
                                (string.IsNullOrWhiteSpace(apellido) ? "" : apellido.Trim().Substring(0, 1))).ToUpper();
            const int lado = 104;   // se dibuja al doble y la PictureBox lo achica: bordes más suaves
            var imagen = new Bitmap(lado, lado);
            using (var g = Graphics.FromImage(imagen))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.Clear(FondoLateral);
                using (var fondo = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, lado, lado),
                           Color.FromArgb(214, 170, 60), Color.FromArgb(150, 110, 25), 45f))
                    g.FillEllipse(fondo, 2, 2, lado - 4, lado - 4);
                using (var fuente = new Font("Segoe UI", 34f, FontStyle.Bold, GraphicsUnit.Pixel))
                using (var texto = new SolidBrush(Color.FromArgb(10, 15, 35)))
                using (var formato = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    g.DrawString(iniciales, fuente, texto, new RectangleF(0, 2, lado, lado), formato);
            }
            return imagen;
        }

        private void ActualizarReloj()
        {
            var cultura = OpcionEnum_68SA.CulturaActual();
            string texto = DateTime.Now.ToString(TraductorManager_08YS.Instance.GetTexto("MDI_formatoReloj"), cultura);
            lblReloj.Text = texto.Length > 0 ? char.ToUpper(texto[0], cultura) + texto.Substring(1) : texto;
        }

        #endregion

        #region Ayuda en línea

        private readonly ToolTip _ttAyuda = new ToolTip();

        /// <summary>Pantalla abierta en el área de trabajo (la usa la ayuda en línea para saber qué explicar).</summary>
        public Form PantallaActual => panel2.Tag as Form;

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            AyudaDropDownMenu.Show(btnAyuda, btnAyuda.Width - AyudaDropDownMenu.Width, btnAyuda.Height);
        }

        private void ayudaPantallaToolStripMenuItem_Click(object sender, EventArgs e) => AyudaManager_68SA.AyudaDePantalla();

        private void queEsEstoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Se activa cuando el menú ya se cerró, para que el próximo clic sea sobre la pantalla
            BeginInvoke((Action)AyudaManager_68SA.ActivarModoQueEsEsto);
        }

        private void indiceAyudaToolStripMenuItem_Click(object sender, EventArgs e) => AyudaManager_68SA.MostrarIndice();

        private void manualAyudaToolStripMenuItem_Click(object sender, EventArgs e) => AyudaManager_68SA.MostrarManual();

        #endregion

        #region Inicio

        private void btnInicio_Click(object sender, EventArgs e) => MostrarInicio();

        /// <summary>
        /// Pantalla de inicio: la misma que aparece al entrar al sistema. Los accesos directos dependen
        /// de los permisos del usuario (solo aparecen las tareas que puede hacer).
        /// </summary>
        private void MostrarInicio()
        {
            var sesion = SessionManager_08YS.Instance;
            var accesos = new List<AccesoInicio_68SA>();
            void Agregar(bool visible, string clave, IconChar icono, Action accion)
            {
                if (visible) accesos.Add(new AccesoInicio_68SA { Clave = clave, Icono = icono, Accion = accion });
            }

            Agregar(sesion.HasPermission(Permisos.RegistrarReserva), "Inicio_accesoReservar", IconChar.CalendarPlus,
                    () => reservarToolStripMenuItem.PerformClick());
            Agregar(sesion.HasPermission(Permisos.RegistrarCheckIn), "Inicio_accesoCheckIn", IconChar.DoorOpen,
                    () => checkInToolStripMenuItem.PerformClick());
            Agregar(sesion.HasPermission(Permisos.RegistrarCheckOut), "Inicio_accesoCheckOut", IconChar.DoorClosed,
                    () => checkOutToolStripMenuItem.PerformClick());
            Agregar(sesion.HasPermission(Permisos.VerControlHabitaciones), "Inicio_accesoControl", IconChar.TableCells,
                    () => controlDeHabitacionesToolStripMenuItem.PerformClick());
            Agregar(sesion.HasPermission(Permisos.RegistrarRoomService), "Inicio_accesoPedidoRS", IconChar.BellConcierge,
                    () => servicioHabitacionToolStripMenuItem.PerformClick());
            Agregar(sesion.HasPermission(Permisos.ConfirmarEntregaRoomService), "Inicio_accesoPedidosRS", IconChar.ListCheck,
                    () => pedidosRoomServiceToolStripMenuItem.PerformClick());
            Agregar(sesion.HasPermission(Permisos.VerDashboard), "Inicio_accesoDashboard", IconChar.ChartColumn,
                    () => btnDashboard.PerformClick());
            Agregar(PuedeVerReportes(), "Inicio_accesoReportes", IconChar.FileLines,
                    () => btnReportes.PerformClick());

            bool verMovimiento = sesion.HasPermission(Permisos.VerDashboard)
                              || sesion.HasPermission(Permisos.VerControlHabitaciones)
                              || sesion.HasPermission(Permisos.RegistrarCheckIn)
                              || sesion.HasPermission(Permisos.RegistrarCheckOut);

            OpenChildForm(new FormInicio_68SA(accesos, verMovimiento));
            MarcarSeccion(btnInicio);
        }

        #endregion

        #region Permisos

        // Mapeo de controles del menu administrativo a los permisos necesarios para verlos y ejecutarlos
        private static readonly Dictionary<string, Permisos> _mapaMenuAdmin =
            new Dictionary<string, Permisos>
            {
                { nameof(gestionUsuarioToolStripMenuItem),   Permisos.VerUsuarios    },
                { nameof(bitacoraToolStripMenuItem),         Permisos.VerBitacora    },
                { nameof(familiasToolStripMenuItem),         Permisos.VerFamilias    },
                { nameof(rolesToolStripMenuItem),            Permisos.VerRoles       },
                { nameof(gestionRespaldosToolStripMenuItem), Permisos.VerRespaldos   },
            };

        // Botones del panel lateral que abren directamente una pantalla de negocio
        private static readonly Dictionary<string, Permisos> _mapaBotonesNegocio =
            new Dictionary<string, Permisos>
            {
                { nameof(btnReservas),    Permisos.VerReservas           },
                { nameof(btnHuespedes),   Permisos.VerHuespedes          },
                { nameof(btnDashboard),   Permisos.VerDashboard          },
                { nameof(btnHotelConfig), Permisos.VerConfiguracionHotel },
                { nameof(btnBitacoraReservas), Permisos.VerBitacoraReservas },
            };

        // Items del menu desplegable de recepción
        private static readonly Dictionary<string, Permisos> _mapaMenuRecepcion =
            new Dictionary<string, Permisos>
            {
                { nameof(reservarToolStripMenuItem),              Permisos.RegistrarReserva       },
                { nameof(controlDeHabitacionesToolStripMenuItem), Permisos.VerControlHabitaciones },
                { nameof(checkInToolStripMenuItem),               Permisos.RegistrarCheckIn       },
                { nameof(checkOutToolStripMenuItem),              Permisos.RegistrarCheckOut      },
            };

        // Items del menu desplegable de room service
        private static readonly Dictionary<string, Permisos> _mapaMenuRoomService =
            new Dictionary<string, Permisos>
            {
                { nameof(servicioHabitacionToolStripMenuItem),    Permisos.RegistrarRoomService        },
                { nameof(pedidosRoomServiceToolStripMenuItem),    Permisos.ConfirmarEntregaRoomService },
                { nameof(cartaRoomServiceToolStripMenuItem),      Permisos.GestionarItemsRoomService   },
            };

        // Items del menu desplegable de habitaciones
        private static readonly Dictionary<string, Permisos> _mapaMenuHabitaciones =
            new Dictionary<string, Permisos>
            {
                { nameof(habitacionesToolStripMenuItem),      Permisos.VerHabitaciones    },
                { nameof(tiposDeHabitacionToolStripMenuItem), Permisos.VerTiposHabitacion },
                { nameof(pisosToolStripMenuItem),             Permisos.VerPisos           },
            };

        private void AplicarPermisos()
        {
            var sesion = SessionManager_08YS.Instance;
            bool puedeCambiarHabitacion = sesion.HasPermission(Permisos.RegistrarUpgradeHabitacion)
                                       || sesion.HasPermission(Permisos.ReubicarHuesped);

            // Visibilidad de cada botón del panel lateral. Se calcula acá (y no se lee de .Visible, que da
            // false mientras el formulario todavía no se mostró) para decidir también los títulos de grupo.
            var visibles = new Dictionary<IconButton, bool>
            {
                { btnInicio, true },
                { btnPerfil, true },
                // Con menú desplegable: visibles si tiene algún permiso de su menú
                { btnAdministrativo, sesion.HasPermission(Permisos.VerUsuarios)
                                  || sesion.HasPermission(Permisos.VerBitacora)
                                  || sesion.HasPermission(Permisos.VerFamilias)
                                  || sesion.HasPermission(Permisos.VerRoles)
                                  || sesion.HasPermission(Permisos.VerRespaldos) },
                { btnRecepcion, sesion.HasPermission(Permisos.RegistrarReserva)
                             || sesion.HasPermission(Permisos.VerControlHabitaciones)
                             || sesion.HasPermission(Permisos.RegistrarCheckIn)
                             || sesion.HasPermission(Permisos.RegistrarCheckOut)
                             || puedeCambiarHabitacion },
                { btnRoomService, sesion.HasPermission(Permisos.RegistrarRoomService)
                               || sesion.HasPermission(Permisos.ConfirmarEntregaRoomService)
                               || sesion.HasPermission(Permisos.GestionarItemsRoomService) },
                { btnHabitaciones, sesion.HasPermission(Permisos.VerHabitaciones)
                                || sesion.HasPermission(Permisos.VerTiposHabitacion)
                                || sesion.HasPermission(Permisos.VerPisos) },
                { btnReportes, PuedeVerReportes() },
            };
            // Botones que abren directamente una pantalla de negocio
            foreach (var par in _mapaBotonesNegocio)
            {
                var boton = flowLayoutPanel1.Controls.Find(par.Key, false).OfType<IconButton>().FirstOrDefault();
                if (boton != null) visibles[boton] = sesion.HasPermission(par.Value);
            }
            foreach (var par in visibles)
                par.Key.Visible = par.Value;

            // Opciones de cada menú desplegable
            gestionAccesosToolStripMenuItem.Visible = sesion.HasPermission(Permisos.VerRoles) || sesion.HasPermission(Permisos.VerFamilias);
            PermissionFilter_08YS.AplicarMenuStrip(AdministrativoDropDownMenu, _mapaMenuAdmin);
            PermissionFilter_08YS.AplicarMenuStrip(RecepcionDropDownMenu, _mapaMenuRecepcion);
            PermissionFilter_08YS.AplicarMenuStrip(RoomServiceDropDownMenu, _mapaMenuRoomService);
            PermissionFilter_08YS.AplicarMenuStrip(HabitacionesDropdownMenu, _mapaMenuHabitaciones);
            // Cambio de habitación: alcanza con uno de sus dos permisos (upgrade o reubicación)
            cambioHabitacionToolStripMenuItem.Visible = puedeCambiarHabitacion;

            // Títulos de grupo del panel lateral: solo si queda algún botón visible debajo
            bool Alguno(params IconButton[] botones) => botones.Any(b => visibles.TryGetValue(b, out bool v) && v);
            lblGrupoOperacion.Visible = Alguno(btnRecepcion, btnReservas, btnHuespedes, btnRoomService);
            lblGrupoHotel.Visible     = Alguno(btnHabitaciones, btnHotelConfig);
            lblGrupoAnalisis.Visible  = Alguno(btnDashboard, btnReportes, btnBitacoraReservas);
            lblGrupoSistema.Visible   = Alguno(btnAdministrativo, btnPerfil);
        }

        /// <summary>Puede abrir la pantalla de reportes si tiene permiso para al menos uno de ellos.</summary>
        internal static bool PuedeVerReportes()
            => SessionManager_08YS.Instance.HasPermission(Permisos.GenerarReporteReservas)
            || SessionManager_08YS.Instance.HasPermission(Permisos.GenerarReporteServicios)
            || SessionManager_08YS.Instance.HasPermission(Permisos.GenerarAnalisisDemanda);

        #endregion

        #region idioma

        private void ConfigurarCombo()
        {
            RefrescarContenidoComboIdioma();
        }

        private void RefrescarContenidoComboIdioma()
        {
            // Bloqueamos el evento para evitar recursividad infinita al limpiar/agregar ítems
            IdiomaCombobox.SelectedIndexChanged -= IdiomaCombobox_SelectedIndexChanged;

            int indexTemporal = IdiomaCombobox.SelectedIndex;

            IdiomaCombobox.Items.Clear();
            // Obtenemos los nombres de los idiomas traducidos dinámicamente
            IdiomaCombobox.Items.Add(TraductorManager_08YS.Instance.GetTexto("Comun_idioma_es")); // "Español"
            IdiomaCombobox.Items.Add(TraductorManager_08YS.Instance.GetTexto("Comun_idioma_en")); // "Inglés"

            // Si es la primera carga, inicializamos según la sesión del usuario
            if (indexTemporal < 0)
            {
                IdiomaCombobox.SelectedIndex = (SessionManager_08YS.Instance.Current.Idioma == "en") ? 1 : 0;
            }
            else
            {
                IdiomaCombobox.SelectedIndex = indexTemporal;
            }

            IdiomaCombobox.SelectedIndexChanged += IdiomaCombobox_SelectedIndexChanged;
        }

        public void UpdateIdioma()
        {
            // 1. Traduce los controles nativos y paneles del formulario (los botones del panel lateral, labels, etc.)
            TraducirControles(this);
            AplicarModoMenu();   // con el menú retraído los botones quedan solo con el ícono

            // 2. TRADUCCIÓN EXPLÍCITA DE MENÚS FLOTANTES COMPOSITE
            // Como no están en Controls, los enviamos individualmente a procesar
            if (AdministrativoDropDownMenu != null)
            {
                TraducirMenuFlotanteCustom(AdministrativoDropDownMenu);
            }

            if (PerfilDropDownMenu != null)
            {
                TraducirMenuFlotanteCustom(PerfilDropDownMenu);
            }

            if (RecepcionDropDownMenu != null)
            {
                TraducirMenuFlotanteCustom(RecepcionDropDownMenu);
            }

            if (HabitacionesDropdownMenu != null)
            {
                TraducirMenuFlotanteCustom(HabitacionesDropdownMenu);
            }

            if (RoomServiceDropDownMenu != null)
            {
                TraducirMenuFlotanteCustom(RoomServiceDropDownMenu);
            }

            if (AyudaDropDownMenu != null)
            {
                TraducirMenuFlotanteCustom(AyudaDropDownMenu);
            }
            _ttAyuda.SetToolTip(btnAyuda, TraductorManager_08YS.Instance.GetTexto("MDI_ttAyuda"));
            RefrescarContenidoComboIdioma();
            ActualizarTextoSeccion();
            ActualizarReloj();
        }

        // NUEVO MÉTODO: Dedicado a los ContextMenuStrip / DropdownMenuStrip personalizados
        private void TraducirMenuFlotanteCustom(ContextMenuStrip menuFlotante)
        {
            foreach (ToolStripItem item in menuFlotante.Items)
            {
                TraducirItemsDesplegables(item);
            }
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                // 1. Si el control tiene hijos (un Panel, GroupBox, FlowLayoutPanel, etc.) y NO es una barra de herramientas,
                // nos metemos inmediatamente a traducirlos de forma recursiva primero.
                if (c.HasChildren && !(c is ToolStrip))
                {
                    TraducirControles(c);
                }

                // 2. SI ES UNA BARRA DE MENÚ (MenuStrip / ToolStrip tradicional)
                if (c is ToolStrip barraMenu)
                {
                    TraducirBarraHerramientas(barraMenu);
                    continue;
                }

                // 3. SI ES UN ICONBUTTON (Como btnPerfil, btnAdministrativo, btnReservar, btnCerrarSesion)
                if (c is IconButton botonIcono)
                {
                    if (botonIcono.Tag != null && !string.IsNullOrWhiteSpace(botonIcono.Tag.ToString()))
                    {
                        // Los botones del panel lateral llevan un pequeño espacio entre el ícono y el texto
                        string separacion = ReferenceEquals(botonIcono.Parent, flowLayoutPanel1) || ReferenceEquals(botonIcono, btnCerrarSesion) ? "  " : "";
                        botonIcono.Text = separacion + TraductorManager_08YS.Instance.GetTexto(botonIcono.Tag.ToString());
                    }
                    continue;
                }

                // 4. Control común y corriente (Labels, Checkbox, etc.)
                if (c.Tag != null && !string.IsNullOrWhiteSpace(c.Tag.ToString()))
                {
                    c.Text = TraductorManager_08YS.Instance.GetTexto(c.Tag.ToString());
                }
            }
        }

        // Recorre los ítems que están adentro de la barra de herramientas de FontAwesome
        private void TraducirBarraHerramientas(ToolStrip barra)
        {
            foreach (ToolStripItem item in barra.Items)
            {
                TraducirItemsDesplegables(item);
            }
        }

        // Se mete de forma recursiva en los submenús del Dropdown (sirve para ToolStrip y ContextMenuStrip)
        private void TraducirItemsDesplegables(ToolStripItem item)
        {
            if (item.Tag != null && !string.IsNullOrWhiteSpace(item.Tag.ToString()))
            {
                item.Text = TraductorManager_08YS.Instance.GetTexto(item.Tag.ToString());
            }

            if (item is ToolStripDropDownItem itemDesplegable && itemDesplegable.HasDropDownItems)
            {
                foreach (ToolStripItem subItem in itemDesplegable.DropDownItems)
                {
                    TraducirItemsDesplegables(subItem); // Recursividad para sub-ítems anidados
                }
            }
        }

        private void IdiomaCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idiomaSeleccionado = IdiomaCombobox.SelectedIndex == 1 ? "en" : "es";

            // 1. Sincronizamos la memoria de la sesión actual a través de la BLL (asumiendo que tenés la referencia '_userBll')
            // Si no tenés la instancia de la BLL inyectada o mapeada en el MDI, accedés directo:
            //SessionManager_08YS.Instance.Current.Idioma = idiomaSeleccionado;
            _userBLL.CambiarIdiomaUsuario(idiomaSeleccionado);
            // 2. Le avisamos al Manager para que muten todas las pantallas abiertas por el Observer
            TraductorManager_08YS.Instance.CambiarIdioma(idiomaSeleccionado);
        }

        #endregion

        #region MdiLifeTime
        private void FormMDI_Load(object sender, EventArgs e)
        {
            AdministrativoDropDownMenu.IsMainMenu = true;
            PerfilDropDownMenu.IsMainMenu = true;
            RecepcionDropDownMenu.IsMainMenu = true;
            HabitacionesDropdownMenu.IsMainMenu = true;
            RoomServiceDropDownMenu.IsMainMenu = true;
            AyudaDropDownMenu.IsMainMenu = true;

            AjustarAPantalla();
            // En pantallas angostas el menú lateral arranca retraído (solo íconos) para dejarle lugar a las pantallas
            _menuRetraido = Screen.FromControl(this).WorkingArea.Width < 1360;
            AplicarModoMenu();
            MostrarInicio();   // lo primero que se ve al entrar es la pantalla de inicio
        }

        // Área de trabajo del monitor actual (relativa al monitor, como la espera MaximizedBounds)
        private Rectangle AreaMaximizada()
        {
            Screen pantalla = Screen.FromControl(this);
            Rectangle area = pantalla.WorkingArea;
            return new Rectangle(area.Left - pantalla.Bounds.Left, area.Top - pantalla.Bounds.Top, area.Width, area.Height);
        }

        // El tamaño de diseño (1800x966) no entra en todas las pantallas: si no entra, la ventana
        // arranca maximizada dentro del área de trabajo (sin tapar la barra de tareas); si entra, centrada.
        private void AjustarAPantalla()
        {
            Rectangle area = Screen.FromControl(this).WorkingArea;
            MaximizedBounds = AreaMaximizada();

            if (Width > area.Width || Height > area.Height)
                WindowState = FormWindowState.Maximized;
            else
                Location = new Point(area.Left + (area.Width - Width) / 2, area.Top + (area.Height - Height) / 2);
        }

        private void FormMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager_08YS.Instance.SesionInvalidada -= OnSesionInvalidada;

            if (SessionManager_08YS.Instance.IsLogged)
            {
                _userBLL.Logout();
                CerrarSesion?.Invoke(); // Desoculta el Username y limpia Singleton
            }
        }

        private void OnSesionInvalidada()
        {
            MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("MDI_msg_sesion_invalidada"),
                            TraductorManager_08YS.Instance.GetTexto("MDI_sesion_invalidada"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            this.Close();
        }

        #endregion

        #region AbrirForms

        public void OpenChildForm(Form childForm)
        {
            var anterior = panel2.Tag as Form;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.AutoScaleMode = AutoScaleMode.None;
            childForm.Dock = DockStyle.Fill;
            // Tamaño final desde el principio: los controles se acomodan una sola vez y antes de
            // mostrarse. Si no, el form aparece con el tamaño del diseñador y se ve cómo se reacomoda
            // (la "línea blanca" de barras de desplazamiento que aparecen y desaparecen).
            childForm.Bounds = panel2.ClientRectangle;

            panel2.SuspendLayout();
            panel2.Controls.Clear();
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            panel2.ResumeLayout(false);

            // La pantalla anterior ya no se ve: deja de recibir los cambios de idioma
            // (si se vuelve a abrir, se suscribe de nuevo acá abajo)
            if (anterior != null && !ReferenceEquals(anterior, childForm) && anterior is IIdiomaObserver_08YS observadorAnterior)
                TraductorManager_08YS.Instance.Desuscribir(observadorAnterior);

            if(childForm is IIdiomaObserver_08YS observer)
            {
                TraductorManager_08YS.Instance.Suscribir(observer); // Suscribir al nuevo formulario al cambio de idioma
                observer.UpdateIdioma(); // Forzar actualización inmediata del idioma al abrir la pantalla
            }

            childForm.PerformLayout();              // layout con el tamaño y los textos definitivos
            ForceCustomControlsLayout(childForm);

            childForm.Load += (s, e) => TraductorManager_08YS.Instance.Suscribir(childForm as IIdiomaObserver_08YS); // Suscribir al nuevo formulario al cambio de idioma
            childForm.FormClosed += (s, e) => TraductorManager_08YS.Instance.Desuscribir(childForm as IIdiomaObserver_08YS); // Desuscribir al cerrar

            childForm.Show();                       // ← mostrar DESPUÉS de que todo esté listo
            childForm.Refresh();                    // ← forzar un pintado limpio único
        }

        private void ForceCustomControlsLayout(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.HasChildren)
                    ForceCustomControlsLayout(ctrl);

                var onResize = ctrl.GetType().GetMethod("OnResize",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);
                onResize?.Invoke(ctrl, new object[] { EventArgs.Empty });

                if (ctrl is FontAwesome.Sharp.IconButton iconBtn)
                {
                    var updateImage = iconBtn.GetType()
                        .GetMethod("UpdateImage",
                            System.Reflection.BindingFlags.NonPublic |
                            System.Reflection.BindingFlags.Instance |
                            System.Reflection.BindingFlags.FlattenHierarchy);
                    updateImage?.Invoke(iconBtn, null);
                    iconBtn.Invalidate();
                    iconBtn.Update();
                }

                if (ctrl is Label lbl && lbl.AutoSize)
                {
                    lbl.MaximumSize = Size.Empty;  // elimina cualquier límite
                    lbl.Size = Size.Empty;         // fuerza recálculo desde cero
                    lbl.Refresh();
                }
                if (ctrl is CustomControls.IconPlaceholderTextBox txt)
                {
                    // Forzar estado visual correcto
                    bool hasText = !string.IsNullOrEmpty(txt.RealText);
                    txt.InnerTextBox.Visible = hasText || txt.InnerTextBox.Focused;
                    txt.Invalidate();
                }
            }
        }

        #endregion

        #region Menu

        #region Administrativo

        private void btnAdministrativo_Click(object sender, EventArgs e)
        {
            AdministrativoDropDownMenu.Show(btnAdministrativo, btnAdministrativo.Width, 0);
        }

        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerUsuarios);
            OpenChildForm(new FormGestionUsuarios_08YS());
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerBitacora);
            OpenChildForm(new FormBitacora_08YS());
        }

        private void familiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerFamilias);
            OpenChildForm(new FormGestionAcceso_08YS(TipoEntidad.Familia, OpenChildForm));
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerRoles);
            OpenChildForm(new FormGestionAcceso_08YS(TipoEntidad.Rol, OpenChildForm));
        }

        private void gestionRespaldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerRespaldos);
            OpenChildForm(new FormGestionRespaldos_08YS());
        }

        #endregion

        #region Perfil

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            PerfilDropDownMenu.Show(btnPerfil, btnPerfil.Width, 0);
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCambiarContraseña_08YS form = new FormCambiarContraseña_08YS();

            form.FormClosed += (s, args) =>
            {
                if (form.DialogResult == DialogResult.OK)
                {
                    // Ejecutamos el cierre de sesión e invocamos el evento para desocultar el Username
                    CerrarSesion?.Invoke();
                    this.Close(); // Cerramos el MDI
                }
            };
            OpenChildForm(form);
        }

        private void ReLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin_08YS frmRelogin = new FormLogin_08YS(true);
            frmRelogin.ShowDialog();
        }

        private void cerrarSesionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("MDI_msg_cerrar_sesion"),
                                        TraductorManager_08YS.Instance.GetTexto("MDI_cerrar_sesion"),
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {

                CerrarSesion?.Invoke();
                this.Close();
            }
        }

        #endregion

        #region Recepcion

        private void btnRecepcion_Click(object sender, EventArgs e)
        {
            RecepcionDropDownMenu.Show(btnRecepcion, btnRecepcion.Width, 0);
        }

        private void reservarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.RegistrarReserva);
            OpenChildForm(new FormDisponibilidad(OpenChildForm));
        }

        private void controlDeHabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerControlHabitaciones);
            OpenChildForm(new FormControlHabitaciones_68SA(OpenChildForm, ModoHabitaciones.Gestion));
        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.RegistrarCheckIn);
            OpenChildForm(new FormCheckIn_68SA(OpenChildForm, ModoHabitaciones.Gestion));
        }

        private void checkOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.RegistrarCheckOut);
            OpenChildForm(new FormCheckOut_68SA(OpenChildForm, ModoHabitaciones.Gestion));
        }

        #region Cargos adicionales (PN2)

        private void servicioHabitacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.RegistrarRoomService);
            var reserva = BuscarEstadiaEnCurso("MDI_tituloBuscarServicio", IconChar.BellConcierge);
            if (reserva == null) return;
            using (var popup = new FormPedidoRoomService_68SA(reserva))
                popup.ShowDialog(this);
        }

        private void cambioHabitacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager_08YS.Instance.HasPermission(Permisos.ReubicarHuesped))
                SessionManager_08YS.Instance.ValidatePermission(Permisos.RegistrarUpgradeHabitacion);
            var reserva = BuscarEstadiaEnCurso("MDI_tituloBuscarCambio", IconChar.Retweet);
            if (reserva == null) return;
            using (var popup = new FormCambioHabitacion_68SA(reserva))
            {
                // Si se está viendo Control de habitaciones, se vuelve a abrir para reflejar el cambio
                if (popup.ShowDialog(this) == DialogResult.OK && panel2.Tag is FormControlHabitaciones_68SA)
                {
                    OpenChildForm(new FormControlHabitaciones_68SA(OpenChildForm, ModoHabitaciones.Gestion));
                    MarcarSeccion(btnRecepcion, controlDeHabitacionesToolStripMenuItem);
                }
            }
        }

        private void pedidosRoomServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.ConfirmarEntregaRoomService);
            OpenChildForm(new FormPedidosRoomService_68SA());
        }

        private void cartaRoomServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.GestionarItemsRoomService);
            OpenChildForm(new FormCartaRoomService_68SA());
        }

        private void btnBitacoraReservas_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerBitacoraReservas);
            OpenChildForm(new FormBitacoraReservas_68SA());
            MarcarSeccion(btnBitacoraReservas);
        }

        /// <summary>Pide la estadía en curso (por documento del titular) y la devuelve completa.</summary>
        private BE_08YS.Reserva_68SA BuscarEstadiaEnCurso(string claveTitulo, IconChar icono)
        {
            using (var popup = new FormBuscarReserva_68SA(BE_08YS.EstadoReserva.EnCurso, TraductorManager_08YS.Instance.GetTexto(claveTitulo), icono))
            {
                if (popup.ShowDialog(this) != DialogResult.OK || popup.ReservaSeleccionada == null) return null;
                return _reservaBLL.GetById(popup.ReservaSeleccionada.Id);
            }
        }

        #endregion

        private void ProcesarNoShows()
        {
            try
            {
                _reservaBLL.ProcesarNoShows();
            }
            catch
            {
                // Si falla un tick (ej. problema de conexión momentáneo), no tumbamos el MDI —
                // se reintenta solo en el próximo ciclo del timer.
            }
        }

        #endregion

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show(TraductorManager_08YS.Instance.GetTexto("MDI_msg_cerrar_sesion"),
                                       TraductorManager_08YS.Instance.GetTexto("MDI_cerrar_sesion"),
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                _userBLL.Logout();
                CerrarSesion?.Invoke();
                this.Close();
            }
        }

        #endregion

        #region BarraSuperior

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximizedBounds = AreaMaximizada();
                this.WindowState = FormWindowState.Maximized;
            }
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        #endregion

        #region Eventos de controles

        private void btnReservas_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerReservas);
            OpenChildForm(new FormReservas_68SA(OpenChildForm));
            MarcarSeccion(btnReservas);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerDashboard);
            OpenChildForm(new FormDashboard_68SA());
            MarcarSeccion(btnDashboard);
        }

        private void btnReportes_Click(object sender, EventArgs e) => AbrirReportes(null);

        private void btnRoomService_Click(object sender, EventArgs e)
        {
            RoomServiceDropDownMenu.Show(btnRoomService, btnRoomService.Width, 0);
        }

        private void btnHabitaciones_Click(object sender, EventArgs e)
        {
            HabitacionesDropdownMenu.Show(btnHabitaciones, btnHabitaciones.Width, 0);
        }

        private void pisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerPisos);
            OpenChildForm(new FormPisos_68SA());
        }

        private void tiposDeHabitacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerTiposHabitacion);
            OpenChildForm(new FormTiposHabitacion_68SA());
        }

        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerHabitaciones);
            OpenChildForm(new FormHabitaciones_68SA());
        }

        private void btnHuespedes_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerHuespedes);
            OpenChildForm(new FormHuespedes_68SA());
            MarcarSeccion(btnHuespedes);
        }

        private void btnHotelConfig_Click(object sender, EventArgs e)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerConfiguracionHotel);
            OpenChildForm(new FormConfiguracionHotel_68SA());
            MarcarSeccion(btnHotelConfig);
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Abre la pantalla de reportes (también la usa el Dashboard para ir directo a un reporte).
        /// Si se indica una pestaña y el usuario tiene permiso, arranca en ella.
        /// </summary>
        public void AbrirReportes(TipoReporte_68SA? pestaña, DateTime? desde = null, DateTime? hasta = null, bool generar = false)
        {
            if (!PuedeVerReportes())
                SessionManager_08YS.Instance.ValidatePermission(Permisos.GenerarReporteReservas);
            OpenChildForm(new FormReportes_68SA(pestaña, desde, hasta, generar));
            MarcarSeccion(btnReportes);
        }

        #endregion
    }
}
