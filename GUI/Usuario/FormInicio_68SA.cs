using BLL_08YS;
using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS
{
    /// <summary>Acceso directo de la pantalla de inicio (lo arma el MDI según los permisos del usuario).</summary>
    public class AccesoInicio_68SA
    {
        #region Propiedades

        public string Clave { get; set; }
        public IconChar Icono { get; set; }
        public Action Accion { get; set; }

        #endregion
    }

    /// <summary>
    /// Pantalla de inicio: es lo primero que se ve al entrar al sistema y a lo que se vuelve con el botón
    /// "Inicio". La foto del hotel de fondo, un saludo, el movimiento del día y accesos directos a las
    /// tareas que el usuario tiene permitidas.
    /// </summary>
    public class FormInicio_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private static readonly Color Tarjeta = Color.FromArgb(10, 18, 50);
        private static readonly Color TarjetaSobre = Color.FromArgb(20, 34, 80);
        private static readonly Color TextoSuave = Color.FromArgb(175, 182, 200);
        private const int Margen = 56;
        // Cada acceso a Properties.Resources crea un Bitmap nuevo: la foto se carga una sola vez
        private static readonly Image FotoFondo = Properties.Resources.ChatGPT_Image_19_may_2026__22_23_23;

        private readonly IList<AccesoInicio_68SA> _accesos;
        private readonly bool _verMovimiento;
        private BE_08YS.Metricas.MovimientoHoy_68SA _hoy;
        private BE_08YS.Metricas.ResumenOcupacion_68SA _ocupacion;
        private bool _cargado;

        private readonly PanelImagen_68SA _fondo;
        private readonly Label _lblMarca, _lblSaludo, _lblFecha, _lblHoy, _lblAccesos;
        private readonly FlowLayoutPanel _flpKpis, _flpAccesos;

        #endregion

        #region Constructor

        public FormInicio_68SA(IList<AccesoInicio_68SA> accesos, bool verMovimiento)
        {
            _accesos = accesos ?? new List<AccesoInicio_68SA>();
            _verMovimiento = verMovimiento;

            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(5, 10, 40);
            ClientSize = new Size(1520, 910);
            FormBorderStyle = FormBorderStyle.None;
            Font = new Font("Segoe UI", 9F);
            Text = "Inicio";

            _fondo = new PanelImagen_68SA
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(5, 10, 40),
                Imagen = FotoFondo,
                FocoX = 0.5f,
                FocoY = 0.45f,
                VeloArriba = Color.FromArgb(25, 5, 10, 40),
                VeloAbajo = Color.FromArgb(235, 5, 10, 40)
            };
            Controls.Add(_fondo);

            _lblMarca = Etiqueta(new Font("Segoe UI", 10F, FontStyle.Bold), Color.Goldenrod);
            _lblSaludo = Etiqueta(new Font("Segoe UI", 30F, FontStyle.Bold), Color.WhiteSmoke);
            _lblFecha = Etiqueta(new Font("Segoe UI", 13F), TextoSuave);
            _lblHoy = Etiqueta(new Font("Segoe UI", 9F, FontStyle.Bold), Color.Goldenrod);
            _lblAccesos = Etiqueta(new Font("Segoe UI", 9F, FontStyle.Bold), Color.Goldenrod);

            _flpKpis = Fila();
            _flpKpis.Name = "pnlMovimiento";      // nombres para la ayuda contextual (F1 sobre el control)
            _flpAccesos = Fila();
            _flpAccesos.Name = "pnlAccesos";

            foreach (var (clave, icono, color) in new[]
            {
                ("llegadas", IconChar.DoorOpen, Color.FromArgb(76, 217, 100)),
                ("salidas", IconChar.DoorClosed, Color.FromArgb(235, 140, 60)),
                ("alojados", IconChar.PeopleRoof, Color.FromArgb(90, 155, 235)),
                ("ocupacion", IconChar.Bed, Color.Goldenrod)
            })
                _flpKpis.Controls.Add(CrearKpi(clave, icono, color));

            foreach (var acceso in _accesos.Take(8))
                _flpAccesos.Controls.Add(CrearAcceso(acceso));

            _lblHoy.Visible = _flpKpis.Visible = _verMovimiento;
            _lblAccesos.Visible = _flpAccesos.Visible = _accesos.Count > 0;

            _fondo.Resize += (s, e) => Acomodar();
            ResumeLayout(false);

            Load += (s, e) => Acomodar();
        }

        #endregion

        #region Construcción

        private Label Etiqueta(Font fuente, Color color)
        {
            var etiqueta = new Label { AutoSize = true, BackColor = Color.Transparent, Font = fuente, ForeColor = color, UseMnemonic = false };
            _fondo.Controls.Add(etiqueta);
            return etiqueta;
        }

        private FlowLayoutPanel Fila()
        {
            var fila = new FlowLayoutPanel
            {
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            _fondo.Controls.Add(fila);
            return fila;
        }

        private Control CrearKpi(string clave, IconChar icono, Color color)
        {
            var tarjeta = new RoundedPanel_68SA { BackColor = Tarjeta, CornerRadius = 12, Size = new Size(236, 106), Margin = new Padding(0, 0, 14, 14) };
            tarjeta.Controls.Add(new Panel { BackColor = color, Location = new Point(0, 20), Size = new Size(4, 66) });
            tarjeta.Controls.Add(new IconPictureBox
            {
                BackColor = Tarjeta, IconChar = icono, IconColor = color, IconSize = 26,
                Location = new Point(194, 16), Size = new Size(28, 28)
            });
            var titulo = new Label { AutoSize = true, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = TextoSuave, Location = new Point(18, 14) };
            var valor = new Label { AutoSize = true, Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = Color.WhiteSmoke, BackColor = Color.Transparent, Location = new Point(16, 32), Text = "-" };
            var detalle = new Label { AutoSize = true, Font = new Font("Segoe UI", 8.5F), ForeColor = TextoSuave, BackColor = Color.Transparent, Location = new Point(18, 80) };
            tarjeta.Controls.Add(titulo);
            tarjeta.Controls.Add(valor);
            tarjeta.Controls.Add(detalle);
            detalle.BringToFront();   // el número (más alto) no le pinta encima
            _kpis[clave] = (valor, detalle, titulo);
            return tarjeta;
        }

        private Control CrearAcceso(AccesoInicio_68SA acceso)
        {
            var boton = new IconButton
            {
                BackColor = Tarjeta,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.WhiteSmoke,
                IconChar = acceso.Icono,
                IconColor = Color.Goldenrod,
                IconFont = IconFont.Auto,
                IconSize = 30,
                ImageAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 14, 14),
                Padding = new Padding(6, 14, 6, 12),
                Size = new Size(170, 104),
                Tag = acceso.Clave,
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageAboveText,
                UseVisualStyleBackColor = false
            };
            boton.FlatAppearance.BorderColor = Color.FromArgb(70, 60, 30);
            boton.FlatAppearance.MouseOverBackColor = TarjetaSobre;
            boton.FlatAppearance.MouseDownBackColor = Color.FromArgb(28, 44, 96);
            boton.MouseEnter += (s, e) => boton.FlatAppearance.BorderColor = Color.Goldenrod;
            boton.MouseLeave += (s, e) => boton.FlatAppearance.BorderColor = Color.FromArgb(70, 60, 30);
            boton.Click += (s, e) => acceso.Accion?.Invoke();
            return boton;
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            var sesion = SessionManager_08YS.Instance.Current;
            var cultura = OpcionEnum_68SA.CulturaActual();
            string fecha = DateTime.Now.ToString(T("Inicio_formatoFecha"), cultura);

            _lblMarca.Text = T("Inicio_marca");
            _lblSaludo.Text = $"{Saludo()}, {sesion?.Nombre}";
            _lblFecha.Text = fecha.Length > 0 ? char.ToUpper(fecha[0], cultura) + fecha.Substring(1) : fecha;
            _lblHoy.Text = T("Inicio_hoy");
            _lblAccesos.Text = T("Inicio_accesos");

            _kpis["llegadas"].Titulo.Text = T("Inicio_kpiLlegadas");
            _kpis["salidas"].Titulo.Text = T("Inicio_kpiSalidas");
            _kpis["alojados"].Titulo.Text = T("Inicio_kpiAlojados");
            _kpis["ocupacion"].Titulo.Text = T("Inicio_kpiOcupacion");

            foreach (Control boton in _flpAccesos.Controls)
                if (boton.Tag is string clave) boton.Text = T(clave);

            CargarMovimiento();
            MostrarMovimiento();
            Acomodar();
        }

        #endregion

        #region Carga de datos

        /// <summary>El movimiento del día se lee una sola vez; al cambiar el idioma solo se vuelven a armar los textos.</summary>
        private void CargarMovimiento()
        {
            if (!_verMovimiento || _cargado) return;
            _cargado = true;
            try
            {
                var metricas = BLLFactory_08YS.CreateMetricasBLL();
                _hoy = metricas.GetMovimientoHoy();
                _ocupacion = metricas.GetResumenOcupacion();
            }
            catch (Exception)
            {
                // Sin conexión o sin datos: el inicio se muestra igual, sin el movimiento del día
                _hoy = null;
                _ocupacion = null;
            }
            _lblHoy.Visible = _flpKpis.Visible = _hoy != null && _ocupacion != null;
        }

        #endregion

        #region Métodos auxiliares

        private readonly Dictionary<string, (Label Valor, Label Detalle, Label Titulo)> _kpis =
            new Dictionary<string, (Label, Label, Label)>();

        /// <summary>
        /// El contenido se acomoda de arriba hacia abajo como un bloque centrado en el alto disponible
        /// (un poco más arriba del centro), pegado al margen izquierdo.
        /// </summary>
        private void Acomodar()
        {
            int ancho = Math.Max(200, _fondo.ClientSize.Width - Margen * 2);

            Size kpis = Size.Empty, accesos = Size.Empty;
            if (_flpKpis.Visible) kpis = _flpKpis.GetPreferredSize(new Size(ancho, 0));
            if (_flpAccesos.Visible)
            {
                _flpAccesos.MaximumSize = new Size(ancho, 0);
                accesos = _flpAccesos.GetPreferredSize(new Size(ancho, 0));
            }

            int alto = _lblMarca.Height + _lblSaludo.Height + _lblFecha.Height
                     + (_flpKpis.Visible ? 26 + _lblHoy.Height + 8 + kpis.Height : 0)
                     + (_flpAccesos.Visible ? 22 + _lblAccesos.Height + 8 + accesos.Height : 0);
            int y = Math.Max(36, (_fondo.ClientSize.Height - alto) / 2 - 30);

            _lblMarca.Location = new Point(Margen, y);
            y += _lblMarca.Height;
            _lblSaludo.Location = new Point(Margen - 4, y);
            y += _lblSaludo.Height;
            _lblFecha.Location = new Point(Margen, y);
            y += _lblFecha.Height;

            if (_flpKpis.Visible)
            {
                y += 26;
                _lblHoy.Location = new Point(Margen, y);
                y += _lblHoy.Height + 8;
                _flpKpis.Bounds = new Rectangle(Margen, y, ancho, kpis.Height);
                y += kpis.Height;
            }

            if (_flpAccesos.Visible)
            {
                y += 22;
                _lblAccesos.Location = new Point(Margen, y);
                y += _lblAccesos.Height + 8;
                _flpAccesos.Bounds = new Rectangle(Margen, y, ancho, accesos.Height);
            }
        }

        private void MostrarMovimiento()
        {
            if (_hoy == null || _ocupacion == null) return;
            Poner("llegadas", _hoy.Llegadas.ToString(), string.Format(T("Inicio_kpiPendientes"), _hoy.LlegadasPendientes));
            Poner("salidas", _hoy.Salidas.ToString(), string.Format(T("Inicio_kpiPendientes"), _hoy.SalidasPendientes));
            Poner("alojados", _hoy.HuespedesAlojados.ToString(), string.Format(T("Inicio_kpiEstadias"), _hoy.EstadiasEnCurso));
            Poner("ocupacion", $"{_ocupacion.TasaOcupacionPorcentaje:0.#}%",
                  string.Format(T("Inicio_kpiOcupadas"), _ocupacion.Ocupadas, _ocupacion.Total));
        }

        private void Poner(string clave, string valor, string detalle)
        {
            _kpis[clave].Valor.Text = valor;
            _kpis[clave].Detalle.Text = detalle;
        }

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        private static string Saludo()
        {
            int hora = DateTime.Now.Hour;
            return T(hora >= 6 && hora < 12 ? "Inicio_buenosDias" : hora >= 12 && hora < 20 ? "Inicio_buenasTardes" : "Inicio_buenasNoches");
        }

        #endregion
    }
}
