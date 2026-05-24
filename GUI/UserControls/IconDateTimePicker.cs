using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControls
{
    // ══════════════════════════════════════════════════════════════════════
    //  IconDateTimePicker
    //
    //  DateTimePicker personalizado que soluciona las limitaciones del control
    //  nativo de WinForms:
    //    • El DateTimePicker estándar NO permite cambiar el color de fondo
    //      ni el color de texto de la parte visible (la "cara") antes de
    //      abrir el calendario. Solo expone propiedades de color para el
    //      calendario desplegado. Este control pinta toda la cara a mano.
    //
    //  CARACTERÍSTICAS:
    //    1. Ícono FontAwesome.Sharp (izquierda o derecha, sin evento de clic)
    //    2. Borde personalizable: color normal / color con foco / grosor / esquinas
    //    3. BackColor y ForeColor efectivos sobre la cara del control
    //    4. Colores completos del calendario desplegado
    //    5. Value, MinDate, MaxDate, Format, CustomFormat
    //    6. Evento ValueChanged
    //    7. Soporte de teclado: F4 / Enter / Space abren el calendario
    // ══════════════════════════════════════════════════════════════════════
    [ToolboxItem(true)]
    [DefaultProperty(nameof(Value))]
    [DefaultEvent("ValueChanged")]
    public class IconDateTimePicker : UserControl
    {
        // ──────────────────────────────────────────────────────────────────
        // Constantes de layout
        // ──────────────────────────────────────────────────────────────────
        private const int ArrowAreaWidth = 20;  // ancho del área del botón ▼

        // ──────────────────────────────────────────────────────────────────
        // Controles internos
        // ──────────────────────────────────────────────────────────────────
        private PictureBox _iconPicture;
        private MonthCalendar _calendar;
        private ToolStripDropDown _popup;

        // ──────────────────────────────────────────────────────────────────
        // Campos — valor
        // ──────────────────────────────────────────────────────────────────
        private DateTime? _value = null;
        private DateTime _minDate = new DateTime(1753, 1, 1);
        private DateTime _maxDate = new DateTime(9998, 12, 31);
        private DateTimePickerFormat _format = DateTimePickerFormat.Short;
        private string _customFormat = "dd/MM/yyyy";

        // ──────────────────────────────────────────────────────────────────
        // Campos — ícono
        // ──────────────────────────────────────────────────────────────────
        private IconChar _iconChar = IconChar.None;
        private IconFont _iconFont = IconFont.Auto;
        private Color _iconColor = Color.DimGray;
        private int _iconSize = 16;
        private IconTextBoxAlignment _iconAlign = IconTextBoxAlignment.Left;
        private int _iconPadding = 6;

        // ──────────────────────────────────────────────────────────────────
        // Campos — borde
        // ──────────────────────────────────────────────────────────────────
        private Color _borderColor = Color.FromArgb(180, 180, 180);
        private Color _borderFocusColor = Color.FromArgb(100, 149, 237);
        private int _borderWidth = 1;
        private int _cornerRadius = 0;
        private bool _isFocused = false;

        // ──────────────────────────────────────────────────────────────────
        // Campos — colores del calendario
        // ──────────────────────────────────────────────────────────────────
        private Color _calBackColor = SystemColors.Window;
        private Color _calForeColor = SystemColors.WindowText;
        private Color _calTitleBackColor = Color.FromArgb(0, 120, 215);
        private Color _calTitleForeColor = Color.White;
        private Color _calTrailingForeColor = Color.Silver;

        // ──────────────────────────────────────────────────────────────────
        // Campos — checked
        // ──────────────────────────────────────────────────────────────────
        private bool _showCheckBox = false;
        private bool _checked = true;
        private const int CheckBoxAreaWidth = 20;

        // ══════════════════════════════════════════════════════════════════
        // Eventos
        // ══════════════════════════════════════════════════════════════════
        /// <summary>Se dispara cuando el usuario selecciona una fecha en el calendario.</summary>
        public event EventHandler ValueChanged;
        public event EventHandler CheckedChanged;

        // ══════════════════════════════════════════════════════════════════
        // Constructor
        // ══════════════════════════════════════════════════════════════════
        public IconDateTimePicker()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            _iconPicture = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
                Visible = false
            };
            _iconPicture.Click += (s, e) => ToggleCalendar();
            _iconPicture.Cursor = Cursors.Hand;
            Controls.Add(_iconPicture);

            BackColor = SystemColors.Window;
            ForeColor = SystemColors.WindowText;
            Size = new Size(220, 32);
            Cursor = Cursors.Hand;
            TabStop = true;

            RefreshIcon();
            UpdateLayout();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateLayout();
            UpdateRegion();
            Invalidate();
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: VALOR ────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Category("Data")]
        [Description("Fecha seleccionada. Null si no hay valor.")]
        public DateTime? Value
        {
            get => _value;
            set
            {
                DateTime? clamped = value.HasValue ? Clamp(value.Value, _minDate, _maxDate) : (DateTime?)null;
                if (clamped == _value) return;
                _value = clamped;
                _checked = _value.HasValue;
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Category("Data")]
        [Description("Fecha mínima seleccionable.")]
        public DateTime MinDate
        {
            get => _minDate;
            set
            {
                _minDate = value;
                if (_value.HasValue) _value = Clamp(_value.Value, _minDate, _maxDate);
                if (_calendar != null) _calendar.MinDate = _minDate;
                Invalidate();
            }
        }

        [Category("Data")]
        [Description("Fecha máxima seleccionable.")]
        public DateTime MaxDate
        {
            get => _maxDate;
            set
            {
                _maxDate = value;
                if (_value.HasValue) _value = Clamp(_value.Value, _minDate, _maxDate);
                if (_calendar != null) _calendar.MaxDate = _maxDate;
                Invalidate();
            }
        }

        [Category("Data")]
        [Description("Formato de visualización de la fecha.")]
        [DefaultValue(DateTimePickerFormat.Short)]
        public DateTimePickerFormat Format
        {
            get => _format;
            set { _format = value; Invalidate(); }
        }

        [Category("Data")]
        [Description("Formato personalizado cuando Format = Custom.")]
        [DefaultValue("dd/MM/yyyy")]
        public string CustomFormat
        {
            get => _customFormat;
            set { _customFormat = value ?? "dd/MM/yyyy"; Invalidate(); }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: ÍCONO ────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Category("Icon")]
        [Description("Ícono de FontAwesome.Sharp.")]
        [DefaultValue(IconChar.None)]
        public IconChar IconChar
        {
            get => _iconChar;
            set { _iconChar = value; RefreshIcon(); UpdateLayout(); Invalidate(); }
        }

        [Category("Icon")]
        [Description("Estilo FontAwesome: Auto, Solid, Regular, Brands…")]
        [DefaultValue(IconFont.Auto)]
        public IconFont IconFont
        {
            get => _iconFont;
            set { _iconFont = value; RefreshIcon(); }
        }

        [Category("Icon")]
        [Description("Color del ícono.")]
        public Color IconColor
        {
            get => _iconColor;
            set { _iconColor = value; RefreshIcon(); }
        }

        [Category("Icon")]
        [Description("Tamaño del ícono en píxeles.")]
        [DefaultValue(16)]
        public int IconSize
        {
            get => _iconSize;
            set { _iconSize = Math.Max(8, value); RefreshIcon(); UpdateLayout(); Invalidate(); }
        }

        [Category("Icon")]
        [Description("Posición del ícono: Left o Right.")]
        [DefaultValue(IconTextBoxAlignment.Left)]
        public IconTextBoxAlignment IconAlignment
        {
            get => _iconAlign;
            set { _iconAlign = value; UpdateLayout(); Invalidate(); }
        }

        [Category("Icon")]
        [Description("Espacio a cada lado del ícono en píxeles.")]
        [DefaultValue(6)]
        public int IconPadding
        {
            get => _iconPadding;
            set { _iconPadding = Math.Max(0, value); UpdateLayout(); Invalidate(); }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: BORDE ────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Category("Border")]
        [Description("Color del borde sin foco.")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Border")]
        [Description("Color del borde con foco / desplegado.")]
        public Color BorderFocusColor
        {
            get => _borderFocusColor;
            set { _borderFocusColor = value; Invalidate(); }
        }

        [Category("Border")]
        [Description("Grosor del borde en píxeles.")]
        [DefaultValue(1)]
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = Math.Max(1, value); UpdateLayout(); Invalidate(); }
        }

        [Category("Border")]
        [Description("Radio de las esquinas (0 = cuadradas).")]
        [DefaultValue(0)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = Math.Max(0, value); UpdateRegion(); Invalidate(); }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: CHECKED ──────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════
        [Category("Behavior")]
        [Description("Muestra un checkbox que habilita o deshabilita la fecha.")]
        [DefaultValue(false)]
        public bool ShowCheckBox
        {
            get => _showCheckBox;
            set { _showCheckBox = value; Invalidate(); }
        }

        [Category("Behavior")]
        [Description("Indica si el control está habilitado (solo cuando ShowCheckBox = true).")]
        [DefaultValue(true)]
        [Bindable(true)]
        public bool Checked
        {
            get => _value.HasValue;
            set
            {
                if (_checked == value) return;
                _checked = value;
                if (!value)
                    _value = null;      // limpiar valor al desmarcar
                else if (_value == null)
                    _value = DateTime.Now; // valor por defecto al marcar
                Invalidate();
                CheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: COLORES DEL CALENDARIO ───────────────────────────
        //
        //  El DateTimePicker nativo solo expone estos colores para el
        //  calendario desplegado. Nosotros los replicamos en el MonthCalendar
        //  interno y además exponemos BackColor/ForeColor para la cara.
        // ══════════════════════════════════════════════════════════════════

        [Category("Calendar")]
        [Description("Color de fondo del calendario desplegado.")]
        public Color CalendarBackColor
        {
            get => _calBackColor;
            set { _calBackColor = value; ApplyCalendarColors(); }
        }

        [Category("Calendar")]
        [Description("Color de texto del calendario desplegado.")]
        public Color CalendarForeColor
        {
            get => _calForeColor;
            set { _calForeColor = value; ApplyCalendarColors(); }
        }

        [Category("Calendar")]
        [Description("Color de fondo de la barra de título del calendario.")]
        public Color CalendarTitleBackColor
        {
            get => _calTitleBackColor;
            set { _calTitleBackColor = value; ApplyCalendarColors(); }
        }

        [Category("Calendar")]
        [Description("Color de texto de la barra de título del calendario.")]
        public Color CalendarTitleForeColor
        {
            get => _calTitleForeColor;
            set { _calTitleForeColor = value; ApplyCalendarColors(); }
        }

        [Category("Calendar")]
        [Description("Color de los días del mes anterior/siguiente.")]
        public Color CalendarTrailingForeColor
        {
            get => _calTrailingForeColor;
            set { _calTrailingForeColor = value; ApplyCalendarColors(); }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PINTURA ───────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var bg = new SolidBrush(BackColor))
                g.FillRectangle(bg, ClientRectangle);

            if (_showCheckBox)
                PaintCheckBox(g);

            PaintDateText(g);
            PaintArrow(g);

            Color borderColor = (_isFocused || _calendarOpen) ? _borderFocusColor : _borderColor;
            var borderRect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (_cornerRadius > 0)
                PaintRoundedBorder(g, borderRect, borderColor);
            else
                PaintSquareBorder(g, borderRect, borderColor);
        }

        private void PaintCheckBox(Graphics g)
        {
            int size = 13;
            int x = _borderWidth + 4;
            int y = (Height - size) / 2;
            var rect = new Rectangle(x, y, size, size);

            // Caja del checkbox
            using (var pen = new Pen(_borderColor))
                g.DrawRectangle(pen, rect);

            // Tilde si está checked
            if (_checked)
            {
                using (var pen = new Pen(ForeColor, 2f) { LineJoin = LineJoin.Round })
                {
                    var pts = new[]
                    {
                new Point(x + 2,  y + 6),
                new Point(x + 5,  y + 10),
                new Point(x + 11, y + 3)
            };
                    g.DrawLines(pen, pts);
                }
            }
        }

        private void PaintDateText(Graphics g)
        {
            int bw = _borderWidth;
            int padX = bw + 3;
            int iconW = _iconChar != IconChar.None ? (_iconSize + _iconPadding * 2) : 0;
            int checkW = _showCheckBox ? CheckBoxAreaWidth : 0;   // ← nuevo

            int leftOffset = (_iconAlign == IconTextBoxAlignment.Left) ? iconW : 0;
            int rightOffset = (_iconAlign == IconTextBoxAlignment.Right) ? iconW : 0;

            int textX = padX + checkW + leftOffset;               // ← checkW sumado
            int textW = Width - textX - ArrowAreaWidth - rightOffset - padX;

            // Si no está checked, el texto se pinta grisado
            Color textColor = Checked ? ForeColor : Color.Gray;   // ← nuevo

            var rect = new Rectangle(textX, 0, textW, Height);
            var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                      | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;

            TextRenderer.DrawText(g, GetDisplayText(), Font, rect, textColor, flags);
        }

        private void PaintArrow(Graphics g)
        {
            int bw = _borderWidth;
            int arrowX = Width - ArrowAreaWidth - bw;

            // Línea separadora (usa el color de borde activo)
            Color sepColor = _isFocused ? _borderFocusColor : _borderColor;
            using (var pen = new Pen(sepColor))
                g.DrawLine(pen, arrowX, bw + 2, arrowX, Height - bw - 3);

            // Triángulo ▼ centrado en el área del botón
            int cx = arrowX + ArrowAreaWidth / 2;
            int cy = Height / 2;
            int s = 4;

            var pts = new[] {
                new Point(cx - s, cy - 2),
                new Point(cx + s, cy - 2),
                new Point(cx,     cy + s - 1)
            };

            using (var brush = new SolidBrush(ForeColor))
                g.FillPolygon(brush, pts);
        }

        private void PaintSquareBorder(Graphics g, Rectangle rect, Color color)
        {
            for (int i = 0; i < _borderWidth; i++)
                using (var pen = new Pen(color))
                    g.DrawRectangle(pen, rect.X + i, rect.Y + i,
                                    rect.Width - i * 2, rect.Height - i * 2);
        }

        private void PaintRoundedBorder(Graphics g, Rectangle rect, Color color)
        {
            float d = _cornerRadius * 2f;
            using (var path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                path.CloseFigure();

                using (var bgBrush = new SolidBrush(BackColor))
                    g.FillPath(bgBrush, path);

                using (var pen = new Pen(color, _borderWidth))
                {
                    pen.Alignment = PenAlignment.Inset;
                    g.DrawPath(pen, path);
                }
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── INTERACCIÓN ───────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (_showCheckBox && e is MouseEventArgs me)
            {
                int checkX = _borderWidth + 4;
                int checkSize = 13;
                var checkRect = new Rectangle(checkX, (Height - checkSize) / 2, checkSize + 4, Height);

                if (checkRect.Contains(me.Location))
                {
                    Checked = !_checked;   // toggle, NO abre calendario
                    return;
                }
            }

            ToggleCalendar();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.F4 || e.KeyCode == Keys.Return || e.KeyCode == Keys.Space)
            {
                ToggleCalendar();
                e.Handled = true;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _isFocused = true;
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            _isFocused = false;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
            UpdateRegion();
            Invalidate();
        }
        private void UpdateRegion()
        {
            if (_cornerRadius <= 0) { Region = null; return; }

            float r2 = Math.Max(0, _cornerRadius - _borderWidth);
            float d2 = r2 * 2f;
            int bw = _borderWidth;
            using (var clip = new GraphicsPath())
            {
                clip.AddArc(bw, bw, d2, d2, 180, 90);
                clip.AddArc(Width - bw - d2, bw, d2, d2, 270, 90);
                clip.AddArc(Width - bw - d2, Height - bw - d2, d2, d2, 0, 90);
                clip.AddArc(bw, Height - bw - d2, d2, d2, 90, 90);
                clip.CloseFigure();
                Region = new Region(clip);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── POPUP DEL CALENDARIO ──────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════
        private bool _calendarOpen = false;
        private void ToggleCalendar()
        {
            if (_showCheckBox && !_checked) return;

            if (_popup == null || _popup.IsDisposed)
                BuildPopup();

            if (_popup.Visible)
            {
                _popup.Close();
            }
            else
            {
                // Si no hay valor, usar DateTime.Now como punto de partida
                DateTime safeDate = Clamp(_value.HasValue ? _value.Value : DateTime.Now, _minDate, _maxDate);
                _calendar.SetDate(safeDate);
                _calendar.SelectionStart = safeDate;

                Point showAt = PointToScreen(new Point(0, Height));
                int calH = _calendar.Height + 4;
                if (showAt.Y + calH > Screen.FromControl(this).WorkingArea.Bottom)
                    showAt = PointToScreen(new Point(0, -calH));

                _popup.Show(showAt);
                _calendarOpen = true;
                _isFocused = true;
                Invalidate();
            }
        }

        private void BuildPopup()
        {
            _calendar = new MonthCalendar
            {
                MaxSelectionCount = 1,
                MinDate = _minDate,
                MaxDate = _maxDate
            };
            ApplyCalendarColors();

            _calendar.DateSelected += (s, e) =>
            {
                _value = e.Start;
                _checked = true;        // ← seleccionar una fecha lo marca automáticamente
                _popup.Close();
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            };

            var host = new ToolStripControlHost(_calendar)
            {
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                AutoSize = true
            };

            _popup = new ToolStripDropDown { Padding = Padding.Empty, AutoSize = true };
            _popup.Items.Add(host);
            _popup.Closed += (s, e) =>
            {
                _calendarOpen = false;
                _isFocused = ContainsFocus;
                Invalidate();
            };
        }

        private void ApplyCalendarColors()
        {
            if (_calendar == null) return;
            _calendar.BackColor = _calBackColor;
            _calendar.ForeColor = _calForeColor;
            _calendar.TitleBackColor = _calTitleBackColor;
            _calendar.TitleForeColor = _calTitleForeColor;
            _calendar.TrailingForeColor = _calTrailingForeColor;
        }

        // ══════════════════════════════════════════════════════════════════
        // ── ÍCONO ─────────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        private void RefreshIcon()
        {
            if (_iconChar == IconChar.None)
            {
                _iconPicture.Visible = false;
                var prev = _iconPicture.Image;
                _iconPicture.Image = null;
                prev?.Dispose();
                return;
            }
            var old = _iconPicture.Image;
            _iconPicture.Image = _iconChar.ToBitmap(_iconFont, _iconSize, _iconColor);
            _iconPicture.Visible = true;
            old?.Dispose();
        }

        private void UpdateLayout()
        {
            bool hasIcon = _iconChar != IconChar.None;
            int iconW = hasIcon ? (_iconSize + _iconPadding * 2) : 0;
            int bw = _borderWidth;
            int padX = bw + 3;

            if (!hasIcon)
            {
                _iconPicture.Visible = false;
                return;
            }

            int innerHeight = Height - bw * 2;          // espacio interior real
            int iconH = Math.Min(_iconSize + _iconPadding * 2, innerHeight); // no puede superar el interior
            int iconY = bw + (innerHeight - iconH) / 2; // centrado dentro del área interior

            _iconPicture.Size = new Size(iconW, iconH);

            if (_iconAlign == IconTextBoxAlignment.Left)
                _iconPicture.Location = new Point(padX - 2, iconY);
            else
                _iconPicture.Location = new Point(Width - padX - iconW - ArrowAreaWidth + 2, iconY);
        }

        // ══════════════════════════════════════════════════════════════════
        // ── HELPERS ───────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        private string GetDisplayText()
        {
            if (!_value.HasValue) return string.Empty; // ← sin valor, sin texto

            switch (_format)
            {
                case DateTimePickerFormat.Long: return _value.Value.ToLongDateString();
                case DateTimePickerFormat.Short: return _value.Value.ToShortDateString();
                case DateTimePickerFormat.Time: return _value.Value.ToShortTimeString();
                case DateTimePickerFormat.Custom: return _value.Value.ToString(_customFormat);
                default: return _value.Value.ToShortDateString();
            }
        }

        private static DateTime Clamp(DateTime val, DateTime min, DateTime max)
        {
            if (val < min) return min;
            if (val > max) return max;
            return val;
        }

        // ══════════════════════════════════════════════════════════════════
        // Dispose
        // ══════════════════════════════════════════════════════════════════
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _iconPicture?.Image?.Dispose();
                _popup?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
