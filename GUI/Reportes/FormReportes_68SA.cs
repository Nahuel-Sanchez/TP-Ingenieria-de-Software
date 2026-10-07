using BE_08YS;
using BLL_08YS;
using BLL_08YS.Negocio;
using CustomControls;
using FontAwesome.Sharp;
using GUI_08YS.Recepcionista;
using GUI_08YS.UserControls;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_08YS.Reportes
{
    public enum TipoReporte_68SA { Reservas = 0, Servicios = 1, Analisis = 2 }

    /// <summary>
    /// Pantalla de reportes: una pestaña por reporte (como el Dashboard). Cada pestaña tiene a la
    /// izquierda los filtros y el botón "Generar reporte", y a la derecha la vista previa del reporte
    /// con sus páginas, desde donde se imprime o se guarda en PDF.
    /// Solo aparecen las pestañas de los reportes que el usuario tiene permitidos.
    /// </summary>
    public class FormReportes_68SA : Form, IIdiomaObserver_08YS
    {
        #region Campos

        private readonly Dictionary<TipoReporte_68SA, Permisos> _permisos = new Dictionary<TipoReporte_68SA, Permisos>
        {
            { TipoReporte_68SA.Reservas,  Permisos.GenerarReporteReservas  },
            { TipoReporte_68SA.Servicios, Permisos.GenerarReporteServicios },
            { TipoReporte_68SA.Analisis,  Permisos.GenerarAnalisisDemanda  },
        };

        private readonly ReservaBLL_68SA _reservaBLL = BLLFactory_08YS.CreateReservaBLL();
        private readonly ReporteServiciosBLL_68SA _serviciosBLL = BLLFactory_08YS.CreateReporteServiciosBLL();
        private readonly AnalisisDemandaBLL_68SA _analisisBLL = BLLFactory_08YS.CreateAnalisisDemandaBLL();

        private readonly Label _lblTitulo, _lblSubtitulo;
        private readonly FlowLayoutPanel _flpTabs;
        private readonly RoundedPanel_68SA _pnlLateral;
        private readonly VisorReporte_68SA _visor;
        private readonly Dictionary<TipoReporte_68SA, Pestaña> _pestañas = new Dictionary<TipoReporte_68SA, Pestaña>();
        private TipoReporte_68SA _actual;
        private readonly bool _generarAlAbrir;

        // Reservas
        private IconDateTimePicker _dtpResDesde, _dtpResHasta;
        private CheckBoxHotel_68SA _chkResHistorico;
        private IconComboBox _cmbResEstado;
        // Servicios
        private IconDateTimePicker _dtpSrvDesde, _dtpSrvHasta;
        private CheckBoxHotel_68SA _chkSrvHistorico;
        private IconComboBox _cmbSrvTipo, _cmbSrvEstado;
        private IconPlaceholderTextBox _txtSrvHuesped;
        // Análisis
        private IconComboBox _cmbAnaHorizonte, _cmbAnaHistorial, _cmbAnaTipo;
        private List<TipoHabitacion_68SA> _tiposHabitacion = new List<TipoHabitacion_68SA>();
        private static readonly int[] Horizontes = { 14, 30, 60, 90 };
        private static readonly int[] Historiales = { 6, 12, 24, 0 };   // 0 = toda la historia

        #endregion

        #region Constructor

        /// <param name="pestaña">Pestaña inicial (si el usuario la tiene permitida).</param>
        /// <param name="desde">Período inicial para los reportes de reservas y servicios.</param>
        /// <param name="generarAlAbrir">Genera el reporte de la pestaña inicial apenas se abre (accesos del Dashboard).</param>
        public FormReportes_68SA(TipoReporte_68SA? pestaña = null, DateTime? desde = null, DateTime? hasta = null, bool generarAlAbrir = false)
        {
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Fondo;
            ClientSize = new Size(1520, 910);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Padding = new Padding(28, 18, 28, 20);
            Text = "Reportes";

            // ---------------------------------------------------------------- cuerpo (Fill)
            var cuerpo = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Fondo,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0, 14, 0, 0),
                Size = new Size(1464, 760)
            };
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, AnchoLateral));
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            cuerpo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Controls.Add(cuerpo);

            _pnlLateral = new RoundedPanel_68SA
            {
                BackColor = Tarjeta,
                CornerRadius = 14,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 16, 0),
                // Sin margen a la derecha: si aparece la barra vertical, ocupa ese espacio y no tapa los campos
                Padding = new Padding(22, 20, 0, 18),
                Size = new Size(AnchoLateral - 16, 740)
            };
            cuerpo.Controls.Add(_pnlLateral, 0, 0);

            var pnlVista = new RoundedPanel_68SA
            {
                BackColor = Tarjeta,
                CornerRadius = 14,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Size = new Size(1076, 740)
            };
            cuerpo.Controls.Add(pnlVista, 1, 0);
            _visor = new VisorReporte_68SA { Dock = DockStyle.Fill, Name = "visorReporte" };
            pnlVista.Controls.Add(_visor);

            // ---------------------------------------------------------------- pestañas (Top)
            _flpTabs = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = Fondo,
                WrapContents = false,
                Padding = new Padding(0, 6, 0, 0)
            };
            // (el último que se agrega queda arriba: separador, pestañas y encabezado)
            Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = Color.FromArgb(40, 52, 92) });
            Controls.Add(_flpTabs);

            // ---------------------------------------------------------------- encabezado (Top)
            var encabezado = new Panel { Dock = DockStyle.Top, Height = 72, BackColor = Fondo };
            _lblSubtitulo = new Label { Dock = DockStyle.Fill, ForeColor = TextoSuave, Font = new Font("Segoe UI", 10F) };
            _lblTitulo = new Label { Dock = DockStyle.Top, Height = 38, ForeColor = Color.Goldenrod, Font = new Font("Segoe UI", 16F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft };
            encabezado.Controls.Add(_lblSubtitulo);
            encabezado.Controls.Add(_lblTitulo);
            Controls.Add(encabezado);

            // ---------------------------------------------------------------- pestañas permitidas
            // Desde el Dashboard con "histórico total" llegan las dos fechas vacías: se marca "Todo el historial"
            bool historico = generarAlAbrir && !desde.HasValue && !hasta.HasValue;
            ArmarReservas(desde, hasta);
            ArmarServicios(desde, hasta);
            _chkResHistorico.Checked = _chkSrvHistorico.Checked = historico;
            ArmarAnalisis();

            var permitidas = _pestañas.Keys.Where(t => SessionManager_08YS.Instance.HasPermission(_permisos[t])).OrderBy(t => t).ToList();
            foreach (var tipo in _pestañas.Keys.ToList())
                _pestañas[tipo].Boton.Visible = permitidas.Contains(tipo);

            _actual = pestaña.HasValue && permitidas.Contains(pestaña.Value) ? pestaña.Value
                    : permitidas.Count > 0 ? permitidas[0] : TipoReporte_68SA.Reservas;
            _generarAlAbrir = generarAlAbrir && permitidas.Contains(_actual);

            ResumeLayout(false);

            Load += (s, e) =>
            {
                // El MDI, al abrir la pantalla, les saca el ancho máximo a las etiquetas automáticas:
                // se les vuelve a poner para que los textos largos sigan partiéndose en renglones.
                foreach (var p in _pestañas.Values)
                {
                    p.Descripcion.MaximumSize = new Size(AnchoCampos, 0);
                    p.Resultado.MaximumSize = new Size(AnchoCampos, 0);
                }
                MostrarPestaña(_actual);
                if (_generarAlAbrir) BeginInvoke((Action)(() => Generar(_actual)));
            };
        }

        #endregion

        #region Estilo

        private static readonly Color Fondo = Color.FromArgb(5, 10, 40);
        private static readonly Color Tarjeta = Color.FromArgb(10, 18, 50);
        private static readonly Color Campo = Color.FromArgb(5, 15, 45);
        private static readonly Color TextoSuave = Color.FromArgb(160, 165, 180);
        private const int AnchoLateral = 372;
        private const int AnchoCampos = 314;
        private static readonly Font FuenteTab = new Font("Segoe UI", 10F, FontStyle.Regular);
        private static readonly Font FuenteTabActiva = new Font("Segoe UI", 10F, FontStyle.Bold);

        #endregion

        #region Construcción de las pestañas

        private Pestaña CrearPestaña(TipoReporte_68SA tipo, IconChar icono, int anchoBoton)
        {
            var p = new Pestaña { Tipo = tipo };

            p.Boton = new IconButton
            {
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                IconChar = icono,
                IconFont = IconFont.Auto,
                IconSize = 20,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 10, 0),
                Padding = new Padding(12, 0, 8, 0),
                Size = new Size(anchoBoton, 40),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            p.Boton.Name = "tab" + tipo;   // nombres para la ayuda contextual (F1 sobre el control)
            p.Boton.FlatAppearance.BorderSize = 0;
            p.Boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(18, 30, 70);
            p.Boton.Click += (s, e) => MostrarPestaña(tipo);
            p.Boton.Paint += (s, e) =>
            {
                if (_actual != tipo) return;
                using (var linea = new SolidBrush(Color.Goldenrod))
                    e.Graphics.FillRectangle(linea, 0, p.Boton.Height - 3, p.Boton.Width, 3);
            };
            _flpTabs.Controls.Add(p.Boton);

            p.Panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Tarjeta,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Visible = false
            };
            _pnlLateral.Controls.Add(p.Panel);

            // Encabezado de la pestaña: ícono + título y una descripción
            var cabecera = new Panel { BackColor = Tarjeta, Size = new Size(AnchoCampos, 40), Margin = Padding.Empty };
            cabecera.Controls.Add(new IconPictureBox
            {
                BackColor = Tarjeta, IconChar = icono, IconColor = Color.Goldenrod, IconFont = IconFont.Auto,
                IconSize = 30, Location = new Point(0, 4), Size = new Size(32, 32)
            });
            p.Titulo = new Label
            {
                AutoEllipsis = true, UseMnemonic = false, Font = new Font("Segoe UI", 13F, FontStyle.Bold), ForeColor = Color.WhiteSmoke,
                Location = new Point(42, 6), Size = new Size(AnchoCampos - 42, 28)
            };
            cabecera.Controls.Add(p.Titulo);
            p.Panel.Controls.Add(cabecera);

            p.Descripcion = new Label
            {
                AutoSize = true, MaximumSize = new Size(AnchoCampos, 0), Font = new Font("Segoe UI", 9.5F),
                ForeColor = TextoSuave, Margin = new Padding(0, 8, 0, 12)
            };
            p.Panel.Controls.Add(p.Descripcion);
            p.Panel.Controls.Add(new Panel { BackColor = Color.FromArgb(40, 52, 92), Size = new Size(AnchoCampos, 1), Margin = new Padding(0, 0, 0, 4) });

            _pestañas[tipo] = p;
            return p;
        }

        private void AgregarEtiqueta(Pestaña p, string clave)
        {
            var etiqueta = new Label
            {
                AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = TextoSuave,
                Margin = new Padding(0, 14, 0, 5)
            };
            p.Etiquetas.Add((etiqueta, clave));
            p.Panel.Controls.Add(etiqueta);
        }

        private void AgregarPie(Pestaña p)
        {
            p.Generar = new IconButton
            {
                BackColor = Color.Goldenrod,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(10, 15, 35),
                IconChar = IconChar.FileCirclePlus,
                IconColor = Color.FromArgb(10, 15, 35),
                IconFont = IconFont.Auto,
                IconSize = 22,
                ImageAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 24, 0, 0),
                Size = new Size(AnchoCampos, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            p.Generar.Name = "btnGenerar";
            p.Generar.FlatAppearance.BorderSize = 0;
            p.Generar.FlatAppearance.MouseOverBackColor = Color.Gold;
            var tipo = p.Tipo;
            p.Generar.Click += (s, e) => Generar(tipo);
            p.Panel.Controls.Add(p.Generar);

            p.Resultado = new Label
            {
                AutoSize = true, MaximumSize = new Size(AnchoCampos, 0), Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(200, 205, 220), Margin = new Padding(0, 14, 0, 0)
            };
            p.Panel.Controls.Add(p.Resultado);
        }

        private static IconDateTimePicker CrearFecha(DateTime valor) => new IconDateTimePicker
        {
            BackColor = Campo, BorderColor = Color.Goldenrod, BorderFocusColor = Color.Goldenrod, BorderWidth = 2,
            CalendarBackColor = Campo, CalendarForeColor = SystemColors.ControlLightLight, Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 9.5F), ForeColor = SystemColors.ControlLightLight, IconChar = IconChar.CalendarDay,
            IconColor = Color.Goldenrod, IconSize = 18, Margin = Padding.Empty, Size = new Size(AnchoCampos, 40), Value = valor
        };

        private static IconComboBox CrearCombo(IconChar icono) => new IconComboBox
        {
            BackColor = Campo, BorderColor = Color.Goldenrod, BorderFocusColor = Color.Goldenrod, BorderWidth = 2,
            Cursor = Cursors.Hand, DropDownBackColor = Campo, DropDownBorderColor = Color.Goldenrod,
            DropDownForeColor = SystemColors.ControlLightLight, DropDownHighlightBackColor = Color.FromArgb(24, 38, 86),
            DropDownHighlightForeColor = Color.Goldenrod, DropDownItemHeight = 32, DropDownMaxHeight = 220,
            Font = new Font("Segoe UI", 9.75F), ForeColor = SystemColors.ControlLightLight, IconChar = icono,
            IconColor = Color.Goldenrod, IconSize = 20, Margin = Padding.Empty, Size = new Size(AnchoCampos, 40)
        };

        private static CheckBoxHotel_68SA CrearCheck() => new CheckBoxHotel_68SA
        {
            AutoSize = true, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = Color.WhiteSmoke, Margin = new Padding(0, 14, 0, 0)
        };

        /// <summary>Accesos rápidos de período: este mes, últimos 30 días y este año.</summary>
        private FlowLayoutPanel CrearPeriodos(IconDateTimePicker desde, IconDateTimePicker hasta, CheckBoxHotel_68SA historico)
        {
            var fila = new FlowLayoutPanel { BackColor = Tarjeta, AutoSize = true, WrapContents = true, MaximumSize = new Size(AnchoCampos, 0), Margin = new Padding(0, 10, 0, 0) };
            foreach (var (clave, accion) in new (string, Action)[]
            {
                ("Reportes_periodoMes",  () => { desde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); hasta.Value = DateTime.Today; }),
                ("Reportes_periodo30",   () => { desde.Value = DateTime.Today.AddDays(-30); hasta.Value = DateTime.Today; }),
                ("Reportes_periodoAnio", () => { desde.Value = new DateTime(DateTime.Today.Year, 1, 1); hasta.Value = DateTime.Today; }),
            })
            {
                var chip = new Button
                {
                    AutoSize = true, BackColor = Color.FromArgb(15, 25, 55), Cursor = Cursors.Hand, FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = Color.FromArgb(200, 200, 210),
                    Margin = new Padding(0, 0, 6, 6), Padding = new Padding(6, 2, 6, 2), Tag = clave, UseVisualStyleBackColor = false
                };
                chip.FlatAppearance.BorderColor = Color.FromArgb(60, 75, 110);
                chip.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 38, 86);
                chip.Click += (s, e) => { historico.Checked = false; accion(); };
                fila.Controls.Add(chip);
            }
            return fila;
        }

        private void ArmarReservas(DateTime? desde, DateTime? hasta)
        {
            var p = CrearPestaña(TipoReporte_68SA.Reservas, IconChar.CalendarCheck, 200);
            p.NombreArchivo = "Reporte_reservas";

            AgregarEtiqueta(p, "Reportes_lblDesde");
            _dtpResDesde = CrearFecha(desde ?? DateTime.Today.AddDays(-30));
            _dtpResDesde.Name = "dtpDesde";
            p.Panel.Controls.Add(_dtpResDesde);
            AgregarEtiqueta(p, "Reportes_lblHasta");
            _dtpResHasta = CrearFecha(hasta ?? DateTime.Today);
            _dtpResHasta.Name = "dtpHasta";
            p.Panel.Controls.Add(_dtpResHasta);
            _chkResHistorico = CrearCheck();
            _chkResHistorico.Name = "chkHistorico";
            p.Panel.Controls.Add(CrearPeriodos(_dtpResDesde, _dtpResHasta, _chkResHistorico));
            _chkResHistorico.CheckedChanged += (s, e) => _dtpResDesde.Enabled = _dtpResHasta.Enabled = !_chkResHistorico.Checked;
            p.Panel.Controls.Add(_chkResHistorico);

            AgregarEtiqueta(p, "Reportes_lblEstado");
            _cmbResEstado = CrearCombo(IconChar.Filter);
            _cmbResEstado.Name = "cmbEstado";
            p.Panel.Controls.Add(_cmbResEstado);
            AgregarPie(p);
        }

        private void ArmarServicios(DateTime? desde, DateTime? hasta)
        {
            var p = CrearPestaña(TipoReporte_68SA.Servicios, IconChar.BellConcierge, 260);
            p.NombreArchivo = "Reporte_servicios";

            AgregarEtiqueta(p, "Reportes_lblDesde");
            _dtpSrvDesde = CrearFecha(desde ?? DateTime.Today.AddDays(-30));
            _dtpSrvDesde.Name = "dtpDesde";
            p.Panel.Controls.Add(_dtpSrvDesde);
            AgregarEtiqueta(p, "Reportes_lblHasta");
            _dtpSrvHasta = CrearFecha(hasta ?? DateTime.Today);
            _dtpSrvHasta.Name = "dtpHasta";
            p.Panel.Controls.Add(_dtpSrvHasta);
            _chkSrvHistorico = CrearCheck();
            _chkSrvHistorico.Name = "chkHistorico";
            p.Panel.Controls.Add(CrearPeriodos(_dtpSrvDesde, _dtpSrvHasta, _chkSrvHistorico));
            _chkSrvHistorico.CheckedChanged += (s, e) => _dtpSrvDesde.Enabled = _dtpSrvHasta.Enabled = !_chkSrvHistorico.Checked;
            p.Panel.Controls.Add(_chkSrvHistorico);

            AgregarEtiqueta(p, "Reportes_lblTipoServicio");
            _cmbSrvTipo = CrearCombo(IconChar.LayerGroup);
            _cmbSrvTipo.Name = "cmbTipoServicio";
            p.Panel.Controls.Add(_cmbSrvTipo);
            AgregarEtiqueta(p, "Reportes_lblEstadoServicio");
            _cmbSrvEstado = CrearCombo(IconChar.ListCheck);
            _cmbSrvEstado.Name = "cmbEstadoServicio";
            p.Panel.Controls.Add(_cmbSrvEstado);
            AgregarEtiqueta(p, "Reportes_lblHuesped");
            _txtSrvHuesped = new IconPlaceholderTextBox
            {
                BackColor = Campo, BorderColor = Color.Goldenrod, BorderFocusColor = Color.Goldenrod, BorderWidth = 2,
                Font = new Font("Segoe UI", 10F), ForeColor = SystemColors.ControlLightLight, IconChar = IconChar.MagnifyingGlass,
                IconColor = Color.Goldenrod, IconSize = 20, IconPadding = 4, MaxLength = 100, Margin = Padding.Empty,
                PlaceholderColor = Color.LightGray, Size = new Size(AnchoCampos, 46), Name = "txtHuesped"
            };
            _txtSrvHuesped.InnerTextBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                Generar(TipoReporte_68SA.Servicios);
            };
            p.Panel.Controls.Add(_txtSrvHuesped);
            AgregarPie(p);
        }

        private void ArmarAnalisis()
        {
            var p = CrearPestaña(TipoReporte_68SA.Analisis, IconChar.ChartLine, 270);
            p.NombreArchivo = "Analisis_ocupacion_demanda";

            AgregarEtiqueta(p, "Reportes_lblHorizonte");
            _cmbAnaHorizonte = CrearCombo(IconChar.CalendarDays);
            _cmbAnaHorizonte.Name = "cmbHorizonte";
            p.Panel.Controls.Add(_cmbAnaHorizonte);
            AgregarEtiqueta(p, "Reportes_lblHistorial");
            _cmbAnaHistorial = CrearCombo(IconChar.ClockRotateLeft);
            _cmbAnaHistorial.Name = "cmbHistorial";
            p.Panel.Controls.Add(_cmbAnaHistorial);
            AgregarEtiqueta(p, "Reportes_lblTipoHabitacion");
            _cmbAnaTipo = CrearCombo(IconChar.Bed);
            _cmbAnaTipo.Name = "cmbTipoHabitacion";
            p.Panel.Controls.Add(_cmbAnaTipo);

            try
            {
                _tiposHabitacion = BLLFactory_08YS.CreateTipoHabitacionBLL().GetAll().OrderBy(t => t.TarifaNoche).ToList();
            }
            catch (Exception)
            {
                _tiposHabitacion = new List<TipoHabitacion_68SA>();   // sin conexión: solo "Todos"
            }
            AgregarPie(p);
        }

        #endregion

        #region Pestañas

        private void MostrarPestaña(TipoReporte_68SA tipo)
        {
            _actual = tipo;
            foreach (var p in _pestañas.Values)
            {
                bool activa = p.Tipo == tipo;
                p.Boton.BackColor = activa ? Tarjeta : Fondo;
                p.Boton.ForeColor = activa ? Color.Goldenrod : TextoSuave;
                p.Boton.IconColor = p.Boton.ForeColor;
                p.Boton.Font = activa ? FuenteTabActiva : FuenteTab;
                p.Panel.Visible = activa;
                p.Boton.Invalidate();
            }

            // Cada pestaña recuerda su último reporte generado
            var actual = _pestañas[tipo];
            if (actual.Reporte == null)
            {
                _visor.Limpiar();
                return;
            }
            try
            {
                _visor.Mostrar(actual.Reporte, NombreArchivo(actual));
            }
            catch (Exception)
            {
                actual.Reporte = null;
                _visor.Limpiar();
            }
        }

        private static string NombreArchivo(Pestaña p) => $"{p.NombreArchivo}_{DateTime.Now:yyyyMMdd_HHmm}";

        #endregion

        #region Generar

        private void Generar(TipoReporte_68SA tipo)
        {
            var p = _pestañas[tipo];
            try
            {
                SessionManager_08YS.Instance.ValidatePermission(_permisos[tipo]);
                Cursor = Cursors.WaitCursor;
                IReporteImprimible_68SA reporte;
                string resumen;
                switch (tipo)
                {
                    case TipoReporte_68SA.Reservas: reporte = GenerarReservas(out resumen); break;
                    case TipoReporte_68SA.Servicios: reporte = GenerarServicios(out resumen); break;
                    default: reporte = GenerarAnalisis(out resumen); break;
                }
                if (reporte == null) return;

                if (!reporte.TieneDatos)
                {
                    p.Reporte = null;
                    p.Resultado.ForeColor = Color.FromArgb(240, 190, 120);
                    p.Resultado.Text = T("Reportes_sinDatos");
                    if (_actual == tipo) _visor.Limpiar();
                    return;
                }

                if (_actual == tipo) _visor.Mostrar(reporte, NombreArchivo(p));   // si falla (sin impresora), no se guarda
                p.Reporte = reporte;
                p.Resultado.ForeColor = Color.FromArgb(200, 205, 220);
                p.Resultado.Text = resumen;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, T("Reportes_titulo"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (RangoFechasInvalidoException_68SA)
            {
                MessageBox.Show(T("Reportes_msgRangoFechas"), T("Reportes_titulo"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DatosInvalidosException_68SA ex)
            {
                MessageBox.Show(ex.Message, T("Reportes_titulo"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(T("Reportes_msgError"), ex.Message), T("Reportes_titulo"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private IReporteImprimible_68SA GenerarReservas(out string resumen)
        {
            bool historico = _chkResHistorico.Checked;
            DateTime? desde = historico ? null : _dtpResDesde.Value?.Date;
            DateTime? hasta = historico ? null : _dtpResHasta.Value?.Date;
            if (!historico && (!desde.HasValue || !hasta.HasValue))
            {
                MessageBox.Show(T("Dashboard_msgFaltaPeriodo"), T("Dashboard_tituloFaltaPeriodo"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                resumen = null;
                return null;
            }
            if (desde > hasta) throw new RangoFechasInvalidoException_68SA();

            var filtro = new ReservaFiltro_68SA
            {
                FechaDesde = desde,
                FechaHasta = hasta,
                Estado = OpcionEnum_68SA.Seleccionado<EstadoReserva>(_cmbResEstado)
            };
            var reservas = _reservaBLL.GetTodas(filtro);
            resumen = string.Format(T("Reportes_resumenReservas"), reservas.Count,
                                    reservas.Where(r => r.Estado != EstadoReserva.Cancelada).Sum(r => r.Noches));
            return new ReporteReservasImpresor_68SA(reservas, desde, hasta);
        }

        private IReporteImprimible_68SA GenerarServicios(out string resumen)
        {
            bool historico = _chkSrvHistorico.Checked;
            var filtro = new FiltroServicios_68SA
            {
                Desde = historico ? null : _dtpSrvDesde.Value?.Date,
                Hasta = historico ? null : _dtpSrvHasta.Value?.Date,
                Tipo = OpcionEnum_68SA.Seleccionado<TipoCargo>(_cmbSrvTipo),
                Estado = OpcionEnum_68SA.Seleccionado<EstadoServicio>(_cmbSrvEstado),
                Huesped = string.IsNullOrWhiteSpace(_txtSrvHuesped.Text) ? null : _txtSrvHuesped.Text.Trim()
            };
            var reporte = _serviciosBLL.Generar(filtro);
            resumen = string.Format(T("Reportes_resumenServicios"), reporte.Servicios.Count, reporte.CantidadHuespedes,
                                    "$" + reporte.Total.ToString("N2", OpcionEnum_68SA.CulturaActual()));
            return new Recepcionista.ReporteServiciosImpresor_68SA(reporte);
        }

        private IReporteImprimible_68SA GenerarAnalisis(out string resumen)
        {
            int horizonte = Horizontes[Math.Max(0, _cmbAnaHorizonte.SelectedIndex)];
            int historial = Historiales[Math.Max(0, _cmbAnaHistorial.SelectedIndex)];
            int indiceTipo = _cmbAnaTipo.SelectedIndex;   // 0 = todos
            int? tipo = indiceTipo > 0 && indiceTipo - 1 < _tiposHabitacion.Count ? _tiposHabitacion[indiceTipo - 1].Id : (int?)null;

            var analisis = _analisisBLL.Generar(new FiltroAnalisisDemanda_68SA
            {
                Desde = DateTime.Today,
                DiasHorizonte = horizonte,
                MesesHistorial = historial == 0 ? 12 : historial,
                HistorialCompleto = historial == 0,
                TipoHabitacionId = tipo
            });

            var cultura = OpcionEnum_68SA.CulturaActual();
            resumen = string.Format(T("Reportes_resumenAnalisis"),
                AnalisisDemandaTextos_68SA.Porcentaje(analisis.OcupacionProyectada, cultura),
                analisis.DiasBajaOcupacion, analisis.DiasSaturados, analisis.Alertas.Count(a => a.Tipo != TipoAlertaDemanda.Feriado));
            if (analisis.NochesHistoria < 60)
                resumen += Environment.NewLine + Environment.NewLine + string.Format(T("Reportes_avisoPocaHistoria"), analisis.NochesHistoria);
            return new AnalisisDemandaImpresor_68SA(analisis);
        }

        #endregion

        #region Idioma

        public void UpdateIdioma()
        {
            _lblTitulo.Text = T("Reportes_titulo");
            _lblSubtitulo.Text = T("Reportes_subtitulo");

            var textos = new Dictionary<TipoReporte_68SA, (string Tab, string Titulo, string Descripcion)>
            {
                { TipoReporte_68SA.Reservas,  ("Reportes_tabReservas",  "Reportes_tituloReservas",  "Reportes_descReservas")  },
                { TipoReporte_68SA.Servicios, ("Reportes_tabServicios", "Reportes_tituloServicios", "Reportes_descServicios") },
                { TipoReporte_68SA.Analisis,  ("Reportes_tabAnalisis",  "Reportes_tituloAnalisis",  "Reportes_descAnalisis")  },
            };
            foreach (var p in _pestañas.Values)
            {
                p.Boton.Text = " " + T(textos[p.Tipo].Tab);
                p.Titulo.Text = T(textos[p.Tipo].Titulo);
                p.Descripcion.Text = T(textos[p.Tipo].Descripcion);
                p.Generar.Text = " " + T("Reportes_btnGenerar");
                foreach (var (etiqueta, clave) in p.Etiquetas)
                    etiqueta.Text = T(clave);
                foreach (Control c in p.Panel.Controls)
                    if (c is FlowLayoutPanel fila)
                        foreach (Control chip in fila.Controls)
                            if (chip.Tag is string clave) chip.Text = T(clave);
            }

            _chkResHistorico.Text = T("Reportes_chkHistorico");
            _chkSrvHistorico.Text = T("Reportes_chkHistorico");
            _txtSrvHuesped.PlaceholderText = T("RepServ_phHuesped");
            OpcionEnum_68SA.Cargar(_cmbResEstado, typeof(EstadoReserva), T("RepServ_txtTodos"));
            OpcionEnum_68SA.Cargar(_cmbSrvTipo, typeof(TipoCargo), T("RepServ_txtTodos"));
            OpcionEnum_68SA.Cargar(_cmbSrvEstado, typeof(EstadoServicio), T("RepServ_txtTodos"));
            if (_cmbResEstado.SelectedIndex < 0) _cmbResEstado.SelectedIndex = 0;
            if (_cmbSrvTipo.SelectedIndex < 0) _cmbSrvTipo.SelectedIndex = 0;
            if (_cmbSrvEstado.SelectedIndex < 0) _cmbSrvEstado.SelectedIndex = 0;

            RecargarCombo(_cmbAnaHorizonte, Horizontes.Select(d => string.Format(T("Reportes_proximosDias"), d)), 1);
            RecargarCombo(_cmbAnaHistorial, Historiales.Select(m => m == 0 ? T("Reportes_todoHistorial") : string.Format(T("Reportes_ultimosMeses"), m)), 1);
            RecargarCombo(_cmbAnaTipo, new[] { T("Reportes_todosLosTipos") }.Concat(_tiposHabitacion.Select(t => t.Nombre)), 0);

            _visor.UpdateIdioma();

            // Los reportes ya generados se vuelven a armar en el idioma nuevo
            foreach (var p in _pestañas.Values.Where(x => x.Reporte != null).ToList())
            {
                var tipo = p.Tipo;
                if (IsHandleCreated) BeginInvoke((Action)(() => Generar(tipo)));
            }
        }

        private static void RecargarCombo(IconComboBox combo, IEnumerable<string> opciones, int porDefecto)
        {
            int indice = combo.SelectedIndex;
            combo.Items.Clear();
            foreach (var opcion in opciones) combo.Items.Add(opcion);
            combo.SelectedIndex = indice >= 0 && indice < combo.Items.Count ? indice : Math.Min(porDefecto, combo.Items.Count - 1);
        }

        #endregion

        #region Métodos auxiliares

        private static string T(string clave) => TraductorManager_08YS.Instance.GetTexto(clave);

        #endregion

        #region Tipos anidados

        /// <summary>Lo que hay en cada pestaña: botón, panel de filtros y el último reporte generado.</summary>
        private sealed class Pestaña
        {
            public TipoReporte_68SA Tipo;
            public IconButton Boton;
            public FlowLayoutPanel Panel;
            public Label Titulo, Descripcion, Resultado;
            public IconButton Generar;
            public IReporteImprimible_68SA Reporte;
            public string NombreArchivo;
            public readonly List<(Label Etiqueta, string Clave)> Etiquetas = new List<(Label, string)>();
        }

        #endregion
    }
}
