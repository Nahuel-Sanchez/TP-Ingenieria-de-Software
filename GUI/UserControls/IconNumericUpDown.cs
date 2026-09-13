using FontAwesome.Sharp;
using GUI_08YS.UserControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CustomControls
{
    // ══════════════════════════════════════════════════════════════════════
    //  IconNumericUpDown
    //
    //  Numeric up-down con ícono y borde personalizables, en la misma línea
    //  que IconDateTimePicker/IconComboBox/IconPlaceholderTextBox. A
    //  diferencia de IconDateTimePicker (texto solo decorativo), acá el
    //  valor SÍ se puede tipear directamente: se usa un TextBox interno sin
    //  borde para eso, y se pintan a mano el ícono, el borde y los botones
    //  de incremento/decremento.
    // ══════════════════════════════════════════════════════════════════════
    [ToolboxItem(true)]
    [DefaultProperty(nameof(Value))]
    [DefaultEvent(nameof(ValueChanged))]
    public class IconNumericUpDown : UserControl
    {
        private const int MinSpinnerWidth = 18;

        // ──────────────────────────────────────────────────────────────────
        // Controles internos
        // ──────────────────────────────────────────────────────────────────
        private TextBox _textBox;
        private PictureBox _iconPicture;
        private Timer _repeatTimer;
        private bool _suppressTextChanged;

        // ──────────────────────────────────────────────────────────────────
        // Valor
        // ──────────────────────────────────────────────────────────────────
        private decimal _value = 0;
        private decimal _minimum = 0;
        private decimal _maximum = 100;
        private decimal _increment = 1;
        private decimal _largeIncrement = 10;
        private int _decimalPlaces = 0;
        private bool _thousandsSeparator = false;
        private bool _wrapAround = false;
        private string _prefix = string.Empty;
        private string _suffix = string.Empty;
        private bool _readOnly = false;
        private HorizontalAlignment _numberAlignment = HorizontalAlignment.Right;

        // ──────────────────────────────────────────────────────────────────
        // Ícono
        // ──────────────────────────────────────────────────────────────────
        private IconChar _iconChar = IconChar.None;
        private IconFont _iconFont = IconFont.Auto;
        private Color _iconColor = Color.DimGray;
        private int _iconSize = 16;
        private IconTextBoxAlignment _iconAlign = IconTextBoxAlignment.Left;
        private int _iconPadding = 6;

        // ──────────────────────────────────────────────────────────────────
        // Borde
        // ──────────────────────────────────────────────────────────────────
        private Color _borderColor = Color.FromArgb(180, 180, 180);
        private Color _borderFocusColor = Color.FromArgb(100, 149, 237);
        private int _borderWidth = 1;
        private int _cornerRadius = 0;
        private bool _isFocused = false;

        // ──────────────────────────────────────────────────────────────────
        // Spinner (▲ ▼)
        // ──────────────────────────────────────────────────────────────────
        private int _spinnerWidth = 22;
        private Color _spinnerArrowColor = Color.DimGray;
        private Color _spinnerHoverBackColor = Color.FromArgb(230, 230, 230);
        private Color _spinnerPressedBackColor = Color.FromArgb(210, 210, 210);
        private Color _disabledForeColor = Color.FromArgb(160, 160, 160);

        private Rectangle _upRect;
        private Rectangle _downRect;
        private bool _upHover, _downHover, _upPressed, _downPressed;
        private bool _repeatIsUp;
        private int _repeatStep;

        // ══════════════════════════════════════════════════════════════════
        // Eventos
        // ══════════════════════════════════════════════════════════════════
        public event EventHandler ValueChanged;

        // ══════════════════════════════════════════════════════════════════
        // Constructor
        // ══════════════════════════════════════════════════════════════════
        public IconNumericUpDown()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.StandardDoubleClick, false); // clicks rápidos en ▲▼

            _iconPicture = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
                Visible = false
            };
            _iconPicture.Click += (s, e) => _textBox.Focus();
            Controls.Add(_iconPicture);

            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                TextAlign = _numberAlignment
            };
            _textBox.KeyPress += TextBox_KeyPress;
            _textBox.KeyDown += TextBox_KeyDown;
            _textBox.Enter += TextBox_Enter;
            _textBox.Leave += TextBox_Leave;
            _textBox.TextChanged += TextBox_TextChanged; // atrapa lo que KeyPress no ve (pegado, drag&drop)
            _textBox.MouseWheel += (s, e) => HandleWheel(((HandledMouseEventArgs)e).Delta);
            Controls.Add(_textBox);

            _repeatTimer = new Timer { Interval = 400 };
            _repeatTimer.Tick += RepeatTimer_Tick;

            BackColor = SystemColors.Window;
            ForeColor = SystemColors.WindowText;
            Size = new Size(120, 32);
            TabStop = true;

            DisplayValue();
            RefreshIcon();
            UpdateLayout();
            SyncTextBoxAppearance();
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
        [DefaultValue(typeof(decimal), "0")]
        public decimal Value
        {
            get => _value;
            set => SetValue(value, true);
        }

        [Category("Data")]
        [DefaultValue(typeof(decimal), "0")]
        public decimal Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_maximum < _minimum) _maximum = _minimum;
                SetValue(_value, true);
            }
        }

        [Category("Data")]
        [DefaultValue(typeof(decimal), "100")]
        public decimal Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_minimum > _maximum) _minimum = _maximum;
                SetValue(_value, true);
            }
        }

        [Category("Data")]
        [DefaultValue(typeof(decimal), "1")]
        public decimal Increment { get => _increment; set => _increment = value; }

        /// <summary>Paso usado con Av Pág / Re Pág.</summary>
        [Category("Data")]
        [DefaultValue(typeof(decimal), "10")]
        public decimal LargeIncrement { get => _largeIncrement; set => _largeIncrement = value; }

        [Category("Data")]
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get => _decimalPlaces;
            set { _decimalPlaces = Math.Max(0, value); SetValue(_value, true); }
        }

        [Category("Data")]
        [DefaultValue(false)]
        public bool ThousandsSeparator
        {
            get => _thousandsSeparator;
            set { _thousandsSeparator = value; DisplayValue(); }
        }

        /// <summary>Si es true, superar el Maximum vuelve al Minimum (y viceversa)
        /// en vez de detenerse en el límite.</summary>
        [Category("Data")]
        [DefaultValue(false)]
        public bool WrapAround { get => _wrapAround; set => _wrapAround = value; }

        [Category("Data")]
        public string Prefix { get => _prefix; set { _prefix = value ?? string.Empty; DisplayValue(); } }

        [Category("Data")]
        public string Suffix { get => _suffix; set { _suffix = value ?? string.Empty; DisplayValue(); } }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get => _readOnly;
            set { _readOnly = value; _textBox.ReadOnly = value; Invalidate(); }
        }

        [Category("Behavior")]
        [DefaultValue(HorizontalAlignment.Right)]
        public HorizontalAlignment NumberAlignment
        {
            get => _numberAlignment;
            set { _numberAlignment = value; _textBox.TextAlign = value; }
        }

        [Category("Behavior")]
        public Color DisabledForeColor
        {
            get => _disabledForeColor;
            set { _disabledForeColor = value; SyncTextBoxAppearance(); Invalidate(); }
        }

        /// <summary>Sube el valor un Increment — equivalente a clickear ▲.</summary>
        public void UpButton() => Step(_increment);

        /// <summary>Baja el valor un Increment — equivalente a clickear ▼.</summary>
        public void DownButton() => Step(-_increment);

        private void Step(decimal delta)
        {
            if (_readOnly || !Enabled) return;
            decimal next = _value + delta;

            if (next > _maximum) next = _wrapAround ? _minimum : _maximum;
            else if (next < _minimum) next = _wrapAround ? _maximum : _minimum;

            SetValue(next, true);
        }

        private void SetValue(decimal newValue, bool updateText)
        {
            newValue = Clamp(newValue, _minimum, _maximum);
            newValue = Math.Round(newValue, _decimalPlaces);
            bool changed = newValue != _value;
            _value = newValue;
            if (updateText) DisplayValue(); // sin guarda de foco: Step()/CommitText() necesitan refrescar SIEMPRE
            if (changed) ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DisplayValue()
        {
            if (_textBox == null) return;
            _suppressTextChanged = true;
            _textBox.Text = FormatValue(_value);
            _suppressTextChanged = false;
        }

        private string FormatValue(decimal v)
        {
            string numberFormat = (_thousandsSeparator ? "N" : "F") + _decimalPlaces;
            return _prefix + v.ToString(numberFormat, CultureInfo.CurrentCulture) + _suffix;
        }

        /// <summary>Representación editable: sin prefijo/sufijo/separador de miles,
        /// siempre con "." como separador decimal (independiente de la cultura),
        /// para que la edición y el filtro de teclas sean simples y predecibles.</summary>
        private string RawValueString(decimal v) => v.ToString("F" + _decimalPlaces, CultureInfo.CurrentCulture);

        private void CommitText()
        {
            string raw = _textBox.Text.Trim();
            if (string.IsNullOrEmpty(raw))
            {
                SetValue(_minimum, true);
                return;
            }

            string decSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (decSep == "," && raw.Contains(".") && !raw.Contains(","))
            {
                raw = raw.Replace(".", ",");
            }

            if (!decimal.TryParse(raw, NumberStyles.Number | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out decimal parsed))
            {
                parsed = _minimum;
            }

            SetValue(parsed, true);
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: ÍCONO ────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Category("Icon")]
        [DefaultValue(IconChar.None)]
        public IconChar IconChar
        {
            get => _iconChar;
            set { _iconChar = value; RefreshIcon(); UpdateLayout(); Invalidate(); }
        }

        [Category("Icon")]
        [DefaultValue(IconFont.Auto)]
        public IconFont IconFont
        {
            get => _iconFont;
            set { _iconFont = value; RefreshIcon(); }
        }

        [Category("Icon")]
        public Color IconColor
        {
            get => _iconColor;
            set { _iconColor = value; RefreshIcon(); }
        }

        [Category("Icon")]
        [DefaultValue(16)]
        public int IconSize
        {
            get => _iconSize;
            set { _iconSize = Math.Max(8, value); RefreshIcon(); UpdateLayout(); Invalidate(); }
        }

        [Category("Icon")]
        [DefaultValue(IconTextBoxAlignment.Left)]
        public IconTextBoxAlignment IconAlignment
        {
            get => _iconAlign;
            set { _iconAlign = value; UpdateLayout(); Invalidate(); }
        }

        [Category("Icon")]
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
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

        [Category("Border")]
        public Color BorderFocusColor { get => _borderFocusColor; set { _borderFocusColor = value; Invalidate(); } }

        [Category("Border")]
        [DefaultValue(1)]
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = Math.Max(1, value); UpdateLayout(); Invalidate(); }
        }

        [Category("Border")]
        [DefaultValue(0)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = Math.Max(0, value); UpdateRegion(); Invalidate(); }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: SPINNER ──────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Category("Spinner")]
        [DefaultValue(22)]
        public int SpinnerWidth
        {
            get => _spinnerWidth;
            set { _spinnerWidth = Math.Max(MinSpinnerWidth, value); UpdateLayout(); Invalidate(); }
        }

        [Category("Spinner")]
        public Color SpinnerArrowColor { get => _spinnerArrowColor; set { _spinnerArrowColor = value; Invalidate(); } }

        [Category("Spinner")]
        public Color SpinnerHoverBackColor { get => _spinnerHoverBackColor; set { _spinnerHoverBackColor = value; Invalidate(); } }

        [Category("Spinner")]
        public Color SpinnerPressedBackColor { get => _spinnerPressedBackColor; set { _spinnerPressedBackColor = value; Invalidate(); } }

        // ══════════════════════════════════════════════════════════════════
        // ── PINTURA ───────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var bg = new SolidBrush(BackColor))
                g.FillRectangle(bg, ClientRectangle);

            PaintSpinner(g);

            Color borderColor = _isFocused ? _borderFocusColor : _borderColor;
            var borderRect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (_cornerRadius > 0)
                PaintRoundedBorder(g, borderRect, borderColor);
            else
                PaintSquareBorder(g, borderRect, borderColor);
        }

        private void PaintSpinner(Graphics g)
        {
            int sepX = Width - _spinnerWidth - _borderWidth;
            Color sepColor = _isFocused ? _borderFocusColor : _borderColor;
            bool interactive = Enabled && !_readOnly;

            using (var pen = new Pen(sepColor))
                g.DrawLine(pen, sepX, _borderWidth + 2, sepX, Height - _borderWidth - 2);

            if (interactive)
            {
                if (_upPressed) FillRect(g, _upRect, _spinnerPressedBackColor);
                else if (_upHover) FillRect(g, _upRect, _spinnerHoverBackColor);

                if (_downPressed) FillRect(g, _downRect, _spinnerPressedBackColor);
                else if (_downHover) FillRect(g, _downRect, _spinnerHoverBackColor);
            }

            using (var pen = new Pen(sepColor))
                g.DrawLine(pen, sepX, Height / 2, Width - _borderWidth, Height / 2);

            Color arrowColor = interactive ? _spinnerArrowColor : _disabledForeColor;
            DrawTriangle(g, _upRect, true, arrowColor);
            DrawTriangle(g, _downRect, false, arrowColor);
        }

        private static void FillRect(Graphics g, Rectangle rect, Color color)
        {
            using (var br = new SolidBrush(color))
                g.FillRectangle(br, rect);
        }

        private static void DrawTriangle(Graphics g, Rectangle rect, bool up, Color color)
        {
            int cx = rect.X + rect.Width / 2;
            int cy = rect.Y + rect.Height / 2;
            int s = 4;
            Point[] pts = up
                ? new[] { new Point(cx - s, cy + 2), new Point(cx + s, cy + 2), new Point(cx, cy - s + 1) }
                : new[] { new Point(cx - s, cy - 2), new Point(cx + s, cy - 2), new Point(cx, cy + s - 1) };
            using (var brush = new SolidBrush(color))
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
        // ── INTERACCIÓN: TEXTO ────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        private void TextBox_Enter(object sender, EventArgs e)
        {
            _isFocused = true;
            _suppressTextChanged = true;
            _textBox.Text = RawValueString(_value);
            _suppressTextChanged = false;
            _textBox.SelectAll();
            Invalidate();
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            _isFocused = false;
            CommitText();
            Invalidate();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_readOnly) { e.Handled = true; return; }

            char c = e.KeyChar;
            if (char.IsControl(c)) return;
            if (char.IsDigit(c)) return;
            if (c == '-' && _minimum < 0 && _textBox.SelectionStart == 0 && !_textBox.Text.Contains('-')) return;

            string decSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if ((c == '.' || c == ',') && _decimalPlaces > 0 && !_textBox.Text.Contains(decSep))
            {
                e.KeyChar = decSep[0];
                return;
            }

            e.Handled = true;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressTextChanged) return;

            string original = _textBox.Text;
            string sanitized = SanitizeRawInput(original);
            if (sanitized == original) return;

            int caret = _textBox.SelectionStart - (original.Length - sanitized.Length);
            if (caret < 0) caret = 0;

            _suppressTextChanged = true;
            _textBox.Text = sanitized;
            _textBox.SelectionStart = Math.Min(caret, sanitized.Length);
            _suppressTextChanged = false;
        }

        /// <summary>Deja pasar solo dígitos, un único "-" al inicio (si Minimum permite
        /// negativos) y un único "." (si DecimalPlaces > 0) — mismo criterio que
        /// TextBox_KeyPress, pero aplicado también a texto pegado o soltado por drag&drop.</summary>
        private string SanitizeRawInput(string text)
        {
            var sb = new System.Text.StringBuilder();
            bool seenDecimal = false;
            string decSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            foreach (char c in text)
            {
                if (char.IsDigit(c)) { sb.Append(c); continue; }
                if (c == '-' && sb.Length == 0 && _minimum < 0) { sb.Append(c); continue; }

                // Permite coma o punto y lo convierte al separador regional (',')
                if ((c == ',' || c == '.') && _decimalPlaces > 0 && !seenDecimal)
                {
                    seenDecimal = true;
                    sb.Append(decSep[0]);
                    continue;
                }
            }
            return sb.ToString();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: UpButton(); e.Handled = true; e.SuppressKeyPress = true; break;
                case Keys.Down: DownButton(); e.Handled = true; e.SuppressKeyPress = true; break;
                case Keys.PageUp: Step(_largeIncrement); e.Handled = true; e.SuppressKeyPress = true; break;
                case Keys.PageDown: Step(-_largeIncrement); e.Handled = true; e.SuppressKeyPress = true; break;
                case Keys.Enter: CommitText(); e.Handled = true; e.SuppressKeyPress = true; break;
            }
        }

        private void HandleWheel(int delta)
        {
            if (delta > 0) UpButton();
            else if (delta < 0) DownButton();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            HandleWheel(e.Delta);
        }

        // ══════════════════════════════════════════════════════════════════
        // ── INTERACCIÓN: SPINNER ──────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool newUpHover = _upRect.Contains(e.Location);
            bool newDownHover = _downRect.Contains(e.Location);

            if ((_upPressed && !newUpHover) || (_downPressed && !newDownHover))
                StopRepeat();

            if (newUpHover != _upHover || newDownHover != _downHover)
            {
                _upHover = newUpHover;
                _downHover = newDownHover;
                Cursor = (newUpHover || newDownHover) ? Cursors.Default : Cursors.IBeam;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _upHover = _downHover = false;
            StopRepeat();
            Cursor = Cursors.IBeam;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left || _readOnly || !Enabled) return;

            if (_upRect.Contains(e.Location))
            {
                _upPressed = true;
                _repeatIsUp = true;
                _repeatStep = 0;
                UpButton();
                _textBox.Focus();
                _repeatTimer.Interval = 400;
                _repeatTimer.Start();
                Invalidate();
            }
            else if (_downRect.Contains(e.Location))
            {
                _downPressed = true;
                _repeatIsUp = false;
                _repeatStep = 0;
                DownButton();
                _textBox.Focus();
                _repeatTimer.Interval = 400;
                _repeatTimer.Start();
                Invalidate();
            }
            else
            {
                _textBox.Focus();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            StopRepeat();
        }

        private void StopRepeat()
        {
            _repeatTimer.Stop();
            if (_upPressed || _downPressed)
            {
                _upPressed = _downPressed = false;
                Invalidate();
            }
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            _repeatStep++;
            if (_repeatStep == 1) _repeatTimer.Interval = 80; // acelera tras el primer disparo

            if (_repeatIsUp) UpButton(); else DownButton();
        }

        // ══════════════════════════════════════════════════════════════════
        // ── LAYOUT / ESTADO ───────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
            UpdateRegion();
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            _textBox.Font = Font;
            UpdateLayout();
            Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            SyncTextBoxAppearance();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            SyncTextBoxAppearance();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _textBox.Enabled = Enabled;
            SyncTextBoxAppearance();
            Invalidate();
        }

        private void SyncTextBoxAppearance()
        {
            if (_textBox == null) return;
            _textBox.BackColor = BackColor;
            _textBox.ForeColor = Enabled ? ForeColor : _disabledForeColor;
        }

        private void RefreshIcon()
        {
            if (_iconChar == IconChar.None)
            {
                _iconPicture.Visible = false;
                _iconPicture.Image = null;
                return;
            }
            _iconPicture.Image = IconCache.Get(_iconChar, _iconFont, _iconSize, _iconColor);
            _iconPicture.Visible = true;
        }

        private void UpdateLayout()
        {
            bool hasIcon = _iconChar != IconChar.None;
            int iconW = hasIcon ? (_iconSize + _iconPadding * 2) : 0;
            int bw = _borderWidth;
            int padX = bw + 3;
            int innerHeight = Height - bw * 2;

            if (hasIcon)
            {
                int iconH = Math.Min(_iconSize + _iconPadding * 2, Math.Max(0, innerHeight));
                int iconY = bw + (innerHeight - iconH) / 2;
                _iconPicture.Size = new Size(iconW, iconH);
                _iconPicture.Location = _iconAlign == IconTextBoxAlignment.Left
                    ? new Point(padX - 2, iconY)
                    : new Point(Width - padX - iconW - _spinnerWidth + 2, iconY);
                _iconPicture.Visible = true;
            }
            else
            {
                _iconPicture.Visible = false;
            }

            int leftIconOffset = (hasIcon && _iconAlign == IconTextBoxAlignment.Left) ? iconW : 0;
            int rightIconOffset = (hasIcon && _iconAlign == IconTextBoxAlignment.Right) ? iconW : 0;

            int textX = padX + leftIconOffset;
            int textW = Width - textX - _spinnerWidth - rightIconOffset - padX;

            _textBox.Width = Math.Max(10, textW);
            _textBox.Location = new Point(textX, Math.Max(0, (Height - _textBox.Height) / 2));

            int spinnerX = Width - _spinnerWidth - bw;
            int upH = innerHeight / 2;
            _upRect = new Rectangle(spinnerX, bw, _spinnerWidth, upH);
            _downRect = new Rectangle(spinnerX, bw + upH, _spinnerWidth, innerHeight - upH);
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

        private static decimal Clamp(decimal val, decimal min, decimal max)
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
                _repeatTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}