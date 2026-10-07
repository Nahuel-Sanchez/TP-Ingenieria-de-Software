using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Recepcionista
{
    /// <summary>
    /// Nueva reserva — paso 1 de 3: fechas y huéspedes.
    /// A la izquierda el calendario de rango (con accesos rápidos: esta noche, fin de semana, una y dos semanas);
    /// a la derecha el resumen de la estadía y la disponibilidad por tipo de habitación para esas fechas y esa
    /// cantidad de personas, con la tarifa y el total estimado. Elegir un tipo es opcional: filtra el paso siguiente.
    /// </summary>
    public partial class FormDisponibilidad : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private static readonly Color Fila = Color.FromArgb(14, 24, 62);
        private static readonly Color FilaSobre = Color.FromArgb(20, 32, 76);
        private static readonly Color FilaElegida = Color.FromArgb(28, 44, 96);
        private static readonly Color TextoSuave = Color.FromArgb(160, 165, 180);
        private static readonly Color Verde = Color.FromArgb(76, 217, 100);
        private static readonly Color Naranja = Color.FromArgb(235, 160, 60);
        private static readonly Color Rojo = Color.FromArgb(230, 100, 90);
        private static readonly Font FuenteNombre = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        private static readonly Font FuenteDetalle = new Font("Segoe UI", 8.75F);
        private static readonly Font FuenteEstado = new Font("Segoe UI", 9F, FontStyle.Bold);
        private static readonly Font FuenteTotal = new Font("Segoe UI", 9F);
        private static readonly Font FuenteMensaje = new Font("Segoe UI", 9.5F);

        private readonly Action<Form> _openChildForm;
        private readonly HabitacionBLL_68SA _habitacionBLL = BLLFactory_08YS.CreateHabitacionBLL();
        private readonly Timer _demora = new Timer { Interval = 250 };

        private List<TipoHabitacion_68SA> _tipos = new List<TipoHabitacion_68SA>();
        private Dictionary<int, int> _disponiblesPorTipo = new Dictionary<int, int>();
        private bool _consultado;          // ya se consultó la disponibilidad para el rango actual
        private string _errorConsulta;
        private int? _tipoElegido;

        #endregion

        #region Propiedades

        private int Personas => (int)nudAdultos.Value + (int)nudNinos.Value;

        #endregion

        #region Constructor

        /// <param name="ingreso">Valores para volver a este paso desde la elección de habitación (opcionales).</param>
        public FormDisponibilidad(Action<Form> abrirForm, DateTime? ingreso = null, DateTime? egreso = null,
                                  int adultos = 0, int ninos = 0, int? tipo = null)
        {
            InitializeComponent();
            _openChildForm = abrirForm;

            calRangoReservas.MinDate = DateTime.Today;
            pasosReserva.PasoActual = 0;
            flpDisponibilidad.Resize += (s, e) => AjustarAnchoFilas();
            LayoutAdaptable_68SA.OcultarScrollHorizontal(flpDisponibilidad);
            _demora.Tick += (s, e) => { _demora.Stop(); ConsultarDisponibilidad(); };
            Disposed += (s, e) => _demora.Dispose();

            try
            {
                _tipos = BLLFactory_08YS.CreateTipoHabitacionBLL().GetAll().OrderBy(t => t.TarifaNoche).ThenBy(t => t.Nombre).ToList();
            }
            catch (Exception)
            {
                _tipos = new List<TipoHabitacion_68SA>();   // sin conexión: se ve igual, sin la lista de tipos
            }

            if (adultos > 0) nudAdultos.Value = Math.Min(nudAdultos.Maximum, adultos);
            if (ninos > 0) nudNinos.Value = Math.Min(nudNinos.Maximum, ninos);
            _tipoElegido = tipo;
            if (ingreso.HasValue && egreso.HasValue && ingreso.Value.Date >= DateTime.Today && egreso.Value.Date > ingreso.Value.Date)
                calRangoReservas.SetRange(ingreso.Value.Date, egreso.Value.Date);

            CrearAccesosRapidos();
            Load += (s, e) => { _demora.Stop(); ActualizarResumen(); ConsultarDisponibilidad(); };
        }

        #endregion

        #region Eventos

        private void calRangoReservas_RangeChanged(object sender, EventArgs e)
        {
            _consultado = false;
            ActualizarResumen();
            ProgramarConsulta();
        }

        private void nudHuespedes_ValueChanged(object sender, EventArgs e)
        {
            _consultado = false;
            ProgramarConsulta();
        }

        /// <summary>La consulta se hace un instante después del último cambio (no en cada clic intermedio).</summary>
        private void ProgramarConsulta()
        {
            _demora.Stop();
            if (RangoCompleto(out _, out _))
            {
                // Mientras se espera la consulta no se puede continuar con datos viejos
                btnContinuar.Enabled = false;
                lblAviso.ForeColor = Color.FromArgb(240, 200, 130);
                lblAviso.Text = T("Disponibilidad_avisoConsultando");
                _demora.Start();
            }
            else
            {
                _errorConsulta = null;
                MostrarDisponibilidad();
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            _demora.Stop();
            if (!RangoCompleto(out DateTime ingreso, out DateTime egreso)) return;
            int? tipo = _tipoElegido.HasValue && Disponibles(_tipoElegido.Value) > 0 ? _tipoElegido : null;
            _openChildForm(new FormControlHabitaciones_68SA(_openChildForm, ModoHabitaciones.Reserva, ingreso, egreso,
                                                            tipo, (int)nudAdultos.Value, (int)nudNinos.Value));
        }

        #endregion

        #region Accesos rápidos

        private void CrearAccesosRapidos()
        {
            flpRapidos.Controls.Clear();
            foreach (var (clave, icono, rango) in new (string, IconChar, Func<(DateTime, DateTime)>)[]
            {
                ("Disponibilidad_rapidoHoy",     IconChar.Moon,         () => (DateTime.Today, DateTime.Today.AddDays(1))),
                ("Disponibilidad_rapidoFinde",   IconChar.Sun,          FinDeSemana),
                ("Disponibilidad_rapidoSemana",  IconChar.CalendarWeek, () => (DateTime.Today, DateTime.Today.AddDays(7))),
                ("Disponibilidad_rapidoQuincena", IconChar.CalendarDays, () => (DateTime.Today, DateTime.Today.AddDays(14))),
            })
            {
                var boton = new IconButton
                {
                    AutoSize = true,
                    BackColor = Color.FromArgb(15, 25, 55),
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(215, 220, 232),
                    IconChar = icono,
                    IconColor = Color.Goldenrod,
                    IconFont = IconFont.Auto,
                    IconSize = 18,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(0, 0, 10, 0),
                    MinimumSize = new Size(0, 38),
                    Padding = new Padding(10, 0, 12, 0),
                    Tag = clave,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    UseVisualStyleBackColor = false
                };
                boton.FlatAppearance.BorderColor = Color.FromArgb(60, 75, 110);
                boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 38, 86);
                var obtener = rango;
                boton.Click += (s, e) =>
                {
                    var (desde, hasta) = obtener();
                    calRangoReservas.SetRange(desde, hasta);   // SetRange ya avisa el cambio de rango
                };
                flpRapidos.Controls.Add(boton);
            }
        }

        /// <summary>Próximo fin de semana: viernes a domingo (si hoy es sábado, desde hoy).</summary>
        private static (DateTime, DateTime) FinDeSemana()
        {
            DateTime hoy = DateTime.Today;
            if (hoy.DayOfWeek == DayOfWeek.Saturday) return (hoy, hoy.AddDays(1));
            if (hoy.DayOfWeek == DayOfWeek.Sunday) hoy = hoy.AddDays(1);
            int hastaViernes = ((int)DayOfWeek.Friday - (int)hoy.DayOfWeek + 7) % 7;
            DateTime viernes = hoy.AddDays(hastaViernes);
            return (viernes, viernes.AddDays(2));
        }

        #endregion

        #region Resumen y disponibilidad

        private void ActualizarResumen()
        {
            var cultura = OpcionEnum_68SA.CulturaActual();
            string formato = T("Disponibilidad_formatoFecha");
            lblLlegadaValor.Text = calRangoReservas.RangeStart.HasValue ? Capital(calRangoReservas.RangeStart.Value.ToString(formato, cultura), cultura) : "—";
            lblSalidaValor.Text = calRangoReservas.RangeEnd.HasValue ? Capital(calRangoReservas.RangeEnd.Value.ToString(formato, cultura), cultura) : "—";

            if (RangoCompleto(out DateTime ingreso, out DateTime egreso))
            {
                int noches = (egreso - ingreso).Days;
                int finde = Enumerable.Range(0, noches).Count(i => { var d = ingreso.AddDays(i).DayOfWeek; return d == DayOfWeek.Friday || d == DayOfWeek.Saturday; });
                lblNoches.Text = noches.ToString();
                lblNochesDetalle.Text = string.Format(T(noches == 1 ? "Disponibilidad_txtNoche" : "Disponibilidad_txtNoches"), noches)
                                        + (finde > 0 ? Environment.NewLine + string.Format(T("Disponibilidad_txtNochesFinde"), finde) : string.Empty);
            }
            else
            {
                lblNoches.Text = "—";
                lblNochesDetalle.Text = T(calRangoReservas.RangeStart.HasValue ? "Disponibilidad_txtFaltaSalida" : "Disponibilidad_txtFaltaLlegada");
            }
        }

        private static string Capital(string texto, System.Globalization.CultureInfo cultura)
            => string.IsNullOrEmpty(texto) ? texto : char.ToUpper(texto[0], cultura) + texto.Substring(1);

        private int Disponibles(int tipoId) => _disponiblesPorTipo.TryGetValue(tipoId, out int n) ? n : 0;

        private void ConsultarDisponibilidad()
        {
            _errorConsulta = null;
            if (!RangoCompleto(out DateTime ingreso, out DateTime egreso))
            {
                _disponiblesPorTipo = new Dictionary<int, int>();
                _consultado = false;
                MostrarDisponibilidad();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var libres = _habitacionBLL.GetDisponibles(ingreso, egreso, null, Personas);
                _disponiblesPorTipo = libres.Where(h => h.Tipo != null).GroupBy(h => h.Tipo.Id).ToDictionary(g => g.Key, g => g.Count());
                // Tipos que aparecen en la consulta pero no estaban en la lista (por ejemplo, recién creados)
                foreach (var tipo in libres.Where(h => h.Tipo != null).Select(h => h.Tipo).GroupBy(t => t.Id).Select(g => g.First()))
                    if (_tipos.All(t => t.Id != tipo.Id)) _tipos.Add(tipo);
                _consultado = true;
            }
            catch (Exception ex)
            {
                _disponiblesPorTipo = new Dictionary<int, int>();
                _consultado = false;
                _errorConsulta = ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            MostrarDisponibilidad();
        }

        /// <summary>Un renglón por tipo de habitación: cuántas quedan libres, tarifa y total estimado de la estadía.</summary>
        private void MostrarDisponibilidad()
        {
            flpDisponibilidad.SuspendLayout();
            foreach (Control c in flpDisponibilidad.Controls.Cast<Control>().ToList()) c.Dispose();
            flpDisponibilidad.Controls.Clear();

            bool rango = RangoCompleto(out DateTime ingreso, out DateTime egreso);
            int noches = rango ? (egreso - ingreso).Days : 0;
            int total = _disponiblesPorTipo.Values.Sum();

            if (!rango || !_consultado)
            {
                flpDisponibilidad.Controls.Add(Mensaje(_errorConsulta != null
                    ? string.Format(T("Disponibilidad_msgError"), _errorConsulta)
                    : T("Disponibilidad_msgElegirFechas")));
            }
            else
            {
                foreach (var tipo in _tipos)
                    flpDisponibilidad.Controls.Add(CrearFila(tipo, Disponibles(tipo.Id), noches));
            }

            if (_tipoElegido.HasValue && Disponibles(_tipoElegido.Value) == 0 && _consultado)
                _tipoElegido = null;

            flpDisponibilidad.ResumeLayout(true);
            AjustarAnchoFilas();

            // Aviso y botón
            btnContinuar.Enabled = rango && _consultado && total > 0;
            lblAviso.ForeColor = Color.FromArgb(240, 200, 130);
            if (!rango)
                lblAviso.Text = T(calRangoReservas.RangeStart.HasValue ? "Disponibilidad_txtFaltaSalida" : "Disponibilidad_txtFaltaLlegada");
            else if (!_consultado)
                lblAviso.Text = _errorConsulta != null ? T("Disponibilidad_avisoError") : T("Disponibilidad_avisoConsultando");
            else if (total == 0)
                lblAviso.Text = string.Format(T("Disponibilidad_avisoSinLugar"), Personas);
            else
            {
                lblAviso.ForeColor = Color.FromArgb(170, 220, 180);
                var elegido = _tipos.FirstOrDefault(t => t.Id == _tipoElegido);
                lblAviso.Text = elegido != null
                    ? string.Format(T("Disponibilidad_avisoTipo"), elegido.Nombre)
                    : string.Format(T("Disponibilidad_avisoTotal"), total);
            }
        }

        private Control Mensaje(string texto) => new Label
        {
            AutoSize = false,
            Font = FuenteMensaje,
            ForeColor = TextoSuave,
            Margin = new Padding(0, 4, 0, 0),
            Size = new Size(Math.Max(100, flpDisponibilidad.ClientSize.Width - 4), 60),
            Text = texto,
            UseMnemonic = false
        };

        private Control CrearFila(TipoHabitacion_68SA tipo, int libres, int noches)
        {
            bool admite = tipo.Capacidad >= Personas;
            bool elegible = admite && libres > 0;
            bool elegida = elegible && _tipoElegido == tipo.Id;

            var fila = new RoundedPanel_68SA
            {
                BackColor = elegida ? FilaElegida : Fila,
                CornerRadius = 10,
                Cursor = elegible ? Cursors.Hand : Cursors.Default,
                Margin = new Padding(0, 0, 0, 8),
                Size = new Size(Math.Max(200, flpDisponibilidad.ClientSize.Width - 4), 64),
                Tag = tipo.Id
            };

            if (elegida)
                fila.Controls.Add(new Panel { BackColor = Color.Goldenrod, Location = new Point(0, 12), Size = new Size(4, 40) });

            Color colorEstado = !admite || libres == 0 ? Rojo : libres <= 2 ? Naranja : Verde;
            string estado = !admite ? string.Format(T("Disponibilidad_txtNoAdmite"), tipo.Capacidad)
                          : libres == 0 ? T("Disponibilidad_txtSinLugar")
                          : string.Format(T(libres == 1 ? "Disponibilidad_txtLibre" : "Disponibilidad_txtLibres"), libres);

            var nombre = new Label
            {
                AutoEllipsis = true, Font = FuenteNombre, UseMnemonic = false,
                ForeColor = elegible ? Color.WhiteSmoke : TextoSuave, Location = new Point(14, 9), Size = new Size(fila.Width - 140, 22),
                Text = tipo.Nombre, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var detalle = new Label
            {
                AutoEllipsis = true, Font = FuenteDetalle, ForeColor = TextoSuave, UseMnemonic = false,
                Location = new Point(14, 34), Size = new Size(fila.Width - 140, 20),
                Text = string.Format(T("Disponibilidad_txtDetalleTipo"), tipo.Capacidad, $"${tipo.TarifaNoche:N0}"),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var libresLbl = new Label
            {
                Font = FuenteEstado, ForeColor = colorEstado, UseMnemonic = false,
                Location = new Point(fila.Width - 124, 9), Size = new Size(112, 22), Text = estado,
                TextAlign = ContentAlignment.MiddleRight, Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            var totalLbl = new Label
            {
                Font = FuenteTotal, ForeColor = elegible ? Color.Goldenrod : TextoSuave, UseMnemonic = false,
                Location = new Point(fila.Width - 124, 34), Size = new Size(112, 20),
                Text = elegible ? string.Format(T("Disponibilidad_txtTotal"), $"${tipo.TarifaNoche * noches:N0}") : string.Empty,
                TextAlign = ContentAlignment.MiddleRight, Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            fila.Controls.AddRange(new Control[] { nombre, detalle, libresLbl, totalLbl });

            if (elegible)
            {
                EventHandler entrar = (s, e) => { if (_tipoElegido != tipo.Id) fila.BackColor = FilaSobre; };
                EventHandler salir = (s, e) =>
                {
                    if (fila.ClientRectangle.Contains(fila.PointToClient(Cursor.Position))) return;
                    fila.BackColor = _tipoElegido == tipo.Id ? FilaElegida : Fila;
                };
                EventHandler elegir = (s, e) =>
                {
                    // Un clic elige el tipo; otro clic sobre el mismo vuelve a "todos los tipos"
                    _tipoElegido = _tipoElegido == tipo.Id ? (int?)null : tipo.Id;
                    // Se rearma la lista después del clic (no se puede desechar el control dentro de su propio evento)
                    BeginInvoke((Action)MostrarDisponibilidad);
                };
                foreach (Control c in new Control[] { fila, nombre, detalle, libresLbl, totalLbl })
                {
                    c.MouseEnter += entrar;
                    c.MouseLeave += salir;
                    c.Click += elegir;
                    c.Cursor = Cursors.Hand;
                }
            }
            return fila;
        }

        /// <summary>Los renglones ocupan todo el ancho (también cuando aparece la barra vertical).</summary>
        private void AjustarAnchoFilas()
        {
            int ancho = Math.Max(200, flpDisponibilidad.ClientSize.Width - 4);
            foreach (Control c in flpDisponibilidad.Controls)
                if (c.Width != ancho) c.Width = ancho;
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            TraducirControles(this);
            calRangoReservas.ClearButtonText = T("Comun_txtLimpiarSeleccion");
            pasosReserva.Pasos = new[] { T("Disponibilidad_paso1"), T("Disponibilidad_paso2"), T("Disponibilidad_paso3") };
            btnContinuar.Text = T("Disponibilidad_btnContinuarDisponibilidad") + "  ";
            foreach (Control boton in flpRapidos.Controls)
                if (boton.Tag is string clave) boton.Text = " " + T(clave);
            ActualizarResumen();
            MostrarDisponibilidad();
        }

        private void TraducirControles(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Tag is string clave && !string.IsNullOrWhiteSpace(clave) && !(c.Parent == flpRapidos))
                    c.Text = T(clave);
                if (c.HasChildren && c != flpDisponibilidad)
                    TraducirControles(c);
            }
        }

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        private bool RangoCompleto(out DateTime ingreso, out DateTime egreso)
        {
            ingreso = calRangoReservas.RangeStart?.Date ?? DateTime.MinValue;
            egreso = calRangoReservas.RangeEnd?.Date ?? DateTime.MinValue;
            return calRangoReservas.RangeStart.HasValue && calRangoReservas.RangeEnd.HasValue && egreso > ingreso;
        }

        #endregion

        #region Sobrescrituras

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion
    }
}
