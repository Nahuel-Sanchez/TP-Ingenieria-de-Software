using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp; // NuGet: FontAwesome.Sharp (>= 6.x)

namespace CustomControls
{
    /// <summary>Posición del ícono respecto al texto.</summary>
    public enum IconTextBoxAlignment { Left, Right }

    // ══════════════════════════════════════════════════════════════════════
    //  IconPlaceholderTextBox
    //  Combina en un único UserControl:
    //    1. Ícono FontAwesome.Sharp  (mismo mecanismo que IconButton/IconPictureBox)
    //    2. Placeholder  (EM_SETCUEBANNER nativo + color personalizable)
    //    3. MaskedInput  (placeholder visible, escritura enmascarada)
    //    4. Borde pintado con color de foco y esquinas opcionales redondeadas
    // ══════════════════════════════════════════════════════════════════════
    [ToolboxItem(true)]
    [DefaultProperty(nameof(PlaceholderText))]
    [DefaultEvent("TextChanged")]
    public class IconPlaceholderTextBox : UserControl
    {
        // ──────────────────────────────────────────────────────────────────
        // Controles internos
        // ──────────────────────────────────────────────────────────────────

        // PictureBox para el ícono  →  usa ToBitmap(), la API pública real
        // de FontAwesome.Sharp para WinForms (FormsIconHelper.cs)
        private readonly PictureBox _iconPicture;

        // TextBox con placeholder (control del ejercicio anterior)
        private readonly PlaceholderTextBox _innerTextBox;

        // ──────────────────────────────────────────────────────────────────
        // Campos — ícono
        // ──────────────────────────────────────────────────────────────────
        private IconChar             _iconChar    = IconChar.None;
        private IconFont             _iconFont    = IconFont.Auto;
        private Color                _iconColor   = Color.DimGray;
        private int                  _iconSize    = 18;          // píxeles
        private IconTextBoxAlignment _iconAlign   = IconTextBoxAlignment.Left;
        private int                  _iconPadding = 6;           // a cada lado del ícono

        // ──────────────────────────────────────────────────────────────────
        // Campos — borde
        // ──────────────────────────────────────────────────────────────────
        private Color _borderColor      = Color.FromArgb(180, 180, 180);
        private Color _borderFocusColor = Color.FromArgb(100, 149, 237);
        private int   _borderWidth      = 1;
        private int   _cornerRadius     = 0;
        private bool  _isFocused        = false;

        // ──────────────────────────────────────────────────────────────────
        // Evento IconClick
        // ──────────────────────────────────────────────────────────────────
        /// <summary>
        /// Se dispara cuando el usuario hace clic sobre el ícono.
        /// Permite implementar comportamientos personalizados como alternar
        /// la visibilidad de una contraseña (ojo / ojo tachado).
        /// Si no hay suscriptores, el clic sigue pasando el foco al TextBox.
        /// </summary>
        public event EventHandler IconClick;

        // ══════════════════════════════════════════════════════════════════
        // Constructor
        // ══════════════════════════════════════════════════════════════════
        public IconPlaceholderTextBox()
        {
            SetStyle(ControlStyles.UserPaint           |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            // ── PictureBox del ícono ──────────────────────────────────────
            _iconPicture = new PictureBox
            {
                SizeMode  = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
                Cursor    = Cursors.IBeam,
                Visible   = false
            };
            _iconPicture.Cursor = Cursors.Hand;   // cursor de mano sobre el ícono
            _iconPicture.Click += OnIconPictureClick;

            // ── TextBox interno con placeholder ───────────────────────────
            _innerTextBox = new PlaceholderTextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor   = SystemColors.Window
            };
            _innerTextBox.GotFocus += (_, e) =>
            {
                _isFocused = true;
                _innerTextBox.Visible = true; // mostrar al ganar foco
                Invalidate();
                OnGotFocus(e);
            };

            _innerTextBox.LostFocus += (_, e) =>
            {
                _isFocused = false;
                // Ocultar si está vacío para que el placeholder del UserControl se vea completo
                if (string.IsNullOrEmpty(_innerTextBox.RealText))
                    _innerTextBox.Visible = false;
                Invalidate();
                OnLostFocus(e);
            };

            _innerTextBox.TextChanged += (_, e) =>
            {
                // Mostrar/ocultar según si hay texto
                _innerTextBox.Visible = !string.IsNullOrEmpty(_innerTextBox.RealText) || _innerTextBox.Focused;
                Invalidate();
                OnTextChanged(e);
            };
            _innerTextBox.KeyDown     += (_, e) => OnKeyDown(e);
            _innerTextBox.KeyPress    += (_, e) => OnKeyPress(e);
            _innerTextBox.KeyUp       += (_, e) => OnKeyUp(e);

            Controls.Add(_innerTextBox);
            Controls.Add(_iconPicture);

            BackColor = SystemColors.Window;
            Size      = new Size(250, 32);
            TabStop   = true;

            UpdateLayout();
            _innerTextBox.Visible = false;
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: ÍCONO ────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Category("Icon")]
        [Description("Ícono de FontAwesome.Sharp (igual que en IconButton/IconPictureBox).")]
        [DefaultValue(IconChar.None)]
        public IconChar IconChar
        {
            get => _iconChar;
            set { _iconChar = value; RefreshIcon(); UpdateLayout(); }
        }

        [Category("Icon")]
        [Description("Estilo FontAwesome: Auto, Solid, Regular, Brands, Light, DuoTone…")]
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
        [Description("Tamaño del ícono en píxeles (igual que en IconPictureBox).")]
        [DefaultValue(18)]
        public int IconSize
        {
            get => _iconSize;
            set { _iconSize = Math.Max(8, value); RefreshIcon(); UpdateLayout(); }
        }

        [Category("Icon")]
        [Description("Posición del ícono: Left o Right respecto al texto.")]
        [DefaultValue(IconTextBoxAlignment.Left)]
        public IconTextBoxAlignment IconAlignment
        {
            get => _iconAlign;
            set { _iconAlign = value; UpdateLayout(); }
        }

        [Category("Icon")]
        [Description("Espacio horizontal en píxeles a cada lado del ícono.")]
        [DefaultValue(6)]
        public int IconPadding
        {
            get => _iconPadding;
            set { _iconPadding = Math.Max(0, value); UpdateLayout(); }
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
        [Description("Color del borde con foco.")]
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
        [Description("Radio de las esquinas (0 = esquinas cuadradas).")]
        [DefaultValue(0)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: PLACEHOLDER ──────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Category("Placeholder")]
        [Description("Texto de ayuda cuando el campo está vacío.")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PlaceholderText
        {
            get => _innerTextBox.PlaceholderText;
            set => _innerTextBox.PlaceholderText = value;
        }

        [Category("Placeholder")]
        [Description("Color del texto de ayuda.")]
        public Color PlaceholderColor
        {
            get => _innerTextBox.PlaceholderColor;
            set => _innerTextBox.PlaceholderColor = value;
        }

        [Category("Placeholder")]
        [Description("true → placeholder visible en claro, escritura enmascarada.")]
        [DefaultValue(false)]
        public bool MaskedInput
        {
            get => _innerTextBox.MaskedInput;
            set => _innerTextBox.MaskedInput = value;
        }

        [Category("Placeholder")]
        [Description("Carácter de enmascaramiento cuando MaskedInput = true.")]
        [DefaultValue('●')]
        public char MaskChar
        {
            get => _innerTextBox.MaskChar;
            set => _innerTextBox.MaskChar = value;
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES: TEXTBOX ──────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => _innerTextBox.Text;
            set => _innerTextBox.Text = value;
        }

        /// <summary>Texto real del usuario (devuelve "" cuando el placeholder está visible).</summary>
        [Browsable(false)]
        public string RealText => _innerTextBox.RealText;

        // Font, ForeColor y BackColor NO se sobreescriben con setter propio.
        // Hacerlo causa StackOverflow porque asignar la propiedad al hijo
        // notifica al padre UserControl, que vuelve a disparar el setter → recursión.
        // La solución correcta es interceptar el evento *Changed del base:

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (_innerTextBox != null)
            {
                _innerTextBox.Font = base.Font;   // base.Font: sin llamar al override
                UpdateLayout();
            }
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            if (_innerTextBox != null)
                _innerTextBox.ForeColor = base.ForeColor;
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (_innerTextBox != null)
                _innerTextBox.BackColor = base.BackColor;
            Invalidate(); // repintar borde con nuevo fondo
        }

        [DefaultValue(false)]
        public bool ReadOnly
        {
            get => _innerTextBox.ReadOnly;
            set => _innerTextBox.ReadOnly = value;
        }

        [DefaultValue(32767)]
        public int MaxLength
        {
            get => _innerTextBox.MaxLength;
            set => _innerTextBox.MaxLength = value;
        }

        [DefaultValue(false)]
        public bool Multiline
        {
            get => _innerTextBox.Multiline;
            set { _innerTextBox.Multiline = value; UpdateLayout(); }
        }

        [DefaultValue(HorizontalAlignment.Left)]
        public HorizontalAlignment TextAlign
        {
            get => _innerTextBox.TextAlign;
            set => _innerTextBox.TextAlign = value;
        }

        [DefaultValue(true)]
        public bool WordWrap
        {
            get => _innerTextBox.WordWrap;
            set => _innerTextBox.WordWrap = value;
        }

        public ScrollBars ScrollBars
        {
            get => _innerTextBox.ScrollBars;
            set => _innerTextBox.ScrollBars = value;
        }

        /// <summary>Referencia directa al TextBox interno para usos avanzados.</summary>
        [Browsable(false)]
        public TextBox InnerTextBox => _innerTextBox;

        // ══════════════════════════════════════════════════════════════════
        // ── FOCO ──────────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        private void OnIconPictureClick(object sender, EventArgs e)
        {
            IconClick?.Invoke(this, e);
            if (IconClick == null)
                _innerTextBox.Focus();
        }

        protected override void OnEnter(EventArgs e)
        {
            _innerTextBox.Visible = true;
            _innerTextBox.Focus();
            base.OnEnter(e);
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PINTURA DEL BORDE ─────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var bgBrush = new SolidBrush(BackColor))
                g.FillRectangle(bgBrush, ClientRectangle);

            // ✅ Pintar placeholder desde el UserControl si el TextBox está vacío
            if (!_innerTextBox.Multiline
                && !_innerTextBox.Focused
                && string.IsNullOrEmpty(_innerTextBox.RealText)
                && !string.IsNullOrEmpty(_innerTextBox.PlaceholderText))
            {
                PaintPlaceholder(g);
            }

            Color border = _isFocused ? _borderFocusColor : _borderColor;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (_cornerRadius > 0)
                PaintRoundedBorder(g, rect, border);
            else
                PaintSquareBorder(g, rect, border);
        }

        private void PaintPlaceholder(Graphics g)
        {
            int bw = _borderWidth;
            int padX = bw + 3;
            bool hasIcon = _iconChar != IconChar.None;
            int iconW = hasIcon ? (_iconSize + _iconPadding * 2) : 0;
            int leftOffset = (_iconAlign == IconTextBoxAlignment.Left) ? iconW : 0;

            int textX = padX + leftOffset;
            int textW = Width - textX - padX;

            var rect = new Rectangle(textX, 0, textW, Height);
            var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                      | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;

            TextRenderer.DrawText(g, _innerTextBox.PlaceholderText, _innerTextBox.Font,
                                  rect, _innerTextBox.PlaceholderColor, flags);
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
                path.AddArc(rect.X,         rect.Y,          d, d, 180, 90);
                path.AddArc(rect.Right - d, rect.Y,          d, d, 270, 90);
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d,   0, 90);
                path.AddArc(rect.X,         rect.Bottom - d, d, d,  90, 90);
                path.CloseFigure();

                using (var bgBrush = new SolidBrush(BackColor))
                    g.FillPath(bgBrush, path);

                for (int i = 0; i < _borderWidth; i++)
                    using (var pen = new Pen(color))
                        g.DrawPath(pen, path);
            }

            // Recortar hijos al interior redondeado
            int  bw = _borderWidth;
            float r2 = Math.Max(0, _cornerRadius - bw);
            float d2 = r2 * 2f;

            using (var clip = new GraphicsPath())
            {
                clip.AddArc(bw,              bw,               d2, d2, 180, 90);
                clip.AddArc(Width - bw - d2, bw,               d2, d2, 270, 90);
                clip.AddArc(Width - bw - d2, Height - bw - d2, d2, d2,   0, 90);
                clip.AddArc(bw,              Height - bw - d2, d2, d2,  90, 90);
                clip.CloseFigure();
                Region = new Region(clip);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── ÍCONO: FontAwesome.Sharp → ToBitmap() ─────────────────────────
        // ══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Genera el bitmap del ícono usando la API pública real de FontAwesome.Sharp:
        ///   IconChar.ToBitmap(IconFont, int size, Color?)
        /// Es exactamente el mismo mecanismo interno de IconButton e IconPictureBox.
        /// </summary>
        private void RefreshIcon()
        {
            if (_iconChar == IconChar.None)
            {
                _iconPicture.Visible = false;
                // Liberar bitmap anterior si existía
                var prev = _iconPicture.Image;
                _iconPicture.Image = null;
                prev?.Dispose();
                return;
            }

            // ToBitmap(IconFont, int size, Color?) — firma de FormsIconHelper.cs
            // color es nullable; pasamos el valor directo
            var bmp = _iconChar.ToBitmap(_iconFont, _iconSize, _iconColor);

            var old = _iconPicture.Image;
            _iconPicture.Image   = bmp;
            _iconPicture.Visible = true;
            old?.Dispose();
        }

        // ══════════════════════════════════════════════════════════════════
        // ── LAYOUT ───────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
            Invalidate();
        }

        private void UpdateLayout()
        {
            int  bw      = _borderWidth;
            int  padX    = bw + 3;   // borde + margen lateral (alineado con TextBox nativo)
            int  padY    = bw + 1;   // borde + margen vertical
            bool hasIcon = _iconChar != IconChar.None;
            int  iconW   = hasIcon ? (_iconSize + _iconPadding * 2) : 0;

            int availW = Width  - padX * 2;
            int availH = Height - padY * 2;
            if (availW <= 0 || availH <= 0) return;

            // ── Altura y posición vertical del TextBox ─────────────────────
            // Para BorderStyle.None: PreferredHeight = Font.Height (sin espacio de borde).
            // Centramos el TextBox dentro del UserControl usando esa altura real.
            int tbH = _innerTextBox.Multiline
                ? availH
                : TextRenderer.MeasureText("Ag", _innerTextBox.Font).Height + 4;

            int tbTop = _innerTextBox.Multiline
                ? padY
                : Math.Max(padY, (Height - tbH) / 2);

            if (!_innerTextBox.Multiline)
                _innerTextBox.Height = tbH; // ← forzar alto explícitamente

            // ── Tamaño del PictureBox: ocupa toda la altura del UserControl ─
            // CenterImage centra el bitmap dentro; así el ícono queda
            // visualmente alineado con el texto aunque su bitmap sea más pequeño.
            if (hasIcon)
                _iconPicture.Size = new Size(iconW, Height);

            // ── Posición horizontal según lado del ícono ───────────────────
            if (hasIcon)
            {
                if (_iconAlign == IconTextBoxAlignment.Left)
                {
                    _iconPicture.Location  = new Point(padX - 2, 0);   // ícono pegado al borde izq
                    _innerTextBox.Location = new Point(padX + iconW - 2, tbTop);
                    _innerTextBox.Width    = availW - iconW;
                }
                else // Right
                {
                    _innerTextBox.Location = new Point(padX, tbTop);
                    _innerTextBox.Width    = availW - iconW;
                    _iconPicture.Location  = new Point(Width - padX - iconW + 2, 0);
                }
            }
            else
            {
                _innerTextBox.Location = new Point(padX, tbTop);
                _innerTextBox.Width    = availW;
            }

            if (_innerTextBox.Multiline)
                _innerTextBox.Height = availH;
        }

        // ══════════════════════════════════════════════════════════════════
        // Dispose
        // ══════════════════════════════════════════════════════════════════
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _iconPicture.Image?.Dispose();
            base.Dispose(disposing);
        }
    }
}
