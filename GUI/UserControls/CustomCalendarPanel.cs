using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace CustomControls
{
    // ══════════════════════════════════════════════════════════════════════
    //  CustomCalendarPanel
    //
    //  Calendario mensual pintado a mano, con tres vistas (Días/Meses/Años)
    //  y dos modos de selección (Single/Range). Las celdas de días escalan
    //  para llenar el espacio disponible si el control se agranda más allá
    //  de su tamaño mínimo (igual que un MonthCalendar nativo); el header
    //  se mantiene a altura fija.
    // ══════════════════════════════════════════════════════════════════════
    [ToolboxItem(false)]
    public class CustomCalendarPanel : Control
    {
        private enum CalView { Days, Months, Years }
        private CalView _view = CalView.Days;

        private const int Cols = 7;
        private const int GridCols = 4;
        private const int GridRows = 3;

        private DateTime _viewDate;
        private DateTime? _selectedDate;
        private int _hoverCell = -1;
        private int _yearRangeStart;

        private DateTime _minDate = new DateTime(1753, 1, 1);
        private DateTime _maxDate = new DateTime(9998, 12, 31);

        // Colores
        private Color _titleBackColor = Color.FromArgb(0, 120, 215);
        private Color _titleForeColor = Color.White;
        private Color _dayNameForeColor = Color.FromArgb(180, 180, 180);
        private Color _dayForeColor = SystemColors.WindowText;
        private Color _trailingForeColor = Color.Silver;
        private Color _todayHighlightColor = Color.FromArgb(0, 120, 215);
        private Color _selectedBackColor = Color.FromArgb(0, 120, 215);
        private Color _selectedForeColor = Color.White;
        private Color _hoverBackColor = Color.FromArgb(220, 235, 252);
        private Color _hoverForeColor = SystemColors.WindowText;
        private Color _disabledForeColor = Color.FromArgb(160, 160, 160);
        private Color _footerForeColor = SystemColors.WindowText;
        private Color _separatorColor = Color.FromArgb(200, 200, 200);
        private Color _navButtonHoverColor = Color.FromArgb(180, 210, 245);
        private Color _inRangeBackColor = Color.FromArgb(210, 230, 250);
        private Color _previewBackColor = Color.FromArgb(235, 242, 250);
        private Color _blockedBackColor = Color.FromArgb(246, 236, 236);
        private Color _blockedForeColor = Color.FromArgb(190, 120, 120);

        // Opciones
        private bool _showToday = true;
        private bool _showTodayHighlight = true;
        private bool _prevNavHover = false;
        private bool _nextNavHover = false;
        private bool _titleHover = false;
        private CalendarSelectionMode _mode = CalendarSelectionMode.Single;
        private DateRangeSelector _rangeSelector;
        private bool _allowNavigation = true;
        private bool _disablePastDates = false;

        // Métricas BASE — derivadas de la fuente, no cambian con el tamaño
        private int _baseCellW, _baseCellH, _baseHeaderH, _baseDayNameH, _baseFooterH;

        // Métricas APLICADAS — pueden ser más grandes que las base si el
        // control se expandió por encima de su tamaño mínimo
        private int _cellW;
        private int _cellH;
        private int _headerH;
        private int _dayNameH;
        private int _footerH;
        private int _gridTop;
        private Rectangle _prevBtn;
        private Rectangle _nextBtn;
        private Rectangle _titleBtn;

        private int _bigCellW;
        private int _bigCellH;

        private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
        private readonly DayOfWeek _firstDayOfWeek;

        public event DateRangeEventHandler DateSelected;
        public event EventHandler SizeChanged2;
        public event EventHandler ViewMonthChanged;
        public event EventHandler RangeChanged;
        public event DateRangeEventHandler RangeSelected;

        public CustomCalendarPanel()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            SetStyle(ControlStyles.StandardDoubleClick, false);

            _firstDayOfWeek = _culture.DateTimeFormat.FirstDayOfWeek;
            _viewDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            _yearRangeStart = DateTime.Today.Year - 5;

            Font = new Font("Segoe UI", 9.5f);
            BackColor = SystemColors.Window;
            ForeColor = SystemColors.WindowText;

            RecalcBaseMetrics();
            ApplyLayout();
            Size = PreferredSize;
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PROPIEDADES ───────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                if (value.HasValue)
                    UpdateViewDate(value.Value);
                Invalidate();
            }
        }

        public void SetDate(DateTime date)
        {
            UpdateViewDate(date);
            Invalidate();
        }

        public DateTime SelectionStart => _selectedDate ?? DateTime.Today;

        public DateTime MinDate { get => _minDate; set { _minDate = value; Invalidate(); } }
        public DateTime MaxDate { get => _maxDate; set { _maxDate = value; Invalidate(); } }

        [Browsable(false)]
        public bool CanGoToPreviousMonth => _viewDate.AddDays(-1) >= _minDate;

        [Browsable(false)]
        public bool CanGoToNextMonth => _viewDate.AddMonths(1) <= new DateTime(_maxDate.Year, _maxDate.Month, 1);

        public Color TitleBackColor { get => _titleBackColor; set { _titleBackColor = value; Invalidate(); } }
        public Color TitleForeColor { get => _titleForeColor; set { _titleForeColor = value; Invalidate(); } }
        public Color DayNameForeColor { get => _dayNameForeColor; set { _dayNameForeColor = value; Invalidate(); } }
        public Color DayForeColor { get => _dayForeColor; set { _dayForeColor = value; Invalidate(); } }
        public Color TrailingForeColor { get => _trailingForeColor; set { _trailingForeColor = value; Invalidate(); } }
        public Color TodayHighlightColor { get => _todayHighlightColor; set { _todayHighlightColor = value; Invalidate(); } }
        public Color SelectedBackColor { get => _selectedBackColor; set { _selectedBackColor = value; Invalidate(); } }
        public Color SelectedForeColor { get => _selectedForeColor; set { _selectedForeColor = value; Invalidate(); } }
        public Color HoverBackColor { get => _hoverBackColor; set { _hoverBackColor = value; Invalidate(); } }
        public Color HoverForeColor { get => _hoverForeColor; set { _hoverForeColor = value; Invalidate(); } }
        public Color DisabledForeColor { get => _disabledForeColor; set { _disabledForeColor = value; Invalidate(); } }
        public Color SeparatorColor { get => _separatorColor; set { _separatorColor = value; Invalidate(); } }
        public Color FooterForeColor { get => _footerForeColor; set { _footerForeColor = value; Invalidate(); } }
        public Color InRangeBackColor { get => _inRangeBackColor; set { _inRangeBackColor = value; Invalidate(); } }
        public Color PreviewBackColor { get => _previewBackColor; set { _previewBackColor = value; Invalidate(); } }
        public Color BlockedBackColor { get => _blockedBackColor; set { _blockedBackColor = value; Invalidate(); } }
        public Color BlockedForeColor { get => _blockedForeColor; set { _blockedForeColor = value; Invalidate(); } }

        public bool ShowToday
        {
            get => _showToday;
            set { _showToday = value; RecalcBaseMetrics(); ApplyLayout(); Invalidate(); }
        }
        public bool ShowTodayHighlight { get => _showTodayHighlight; set { _showTodayHighlight = value; Invalidate(); } }

        // ── Modo de selección ──
        [Category("Behavior")]
        [DefaultValue(CalendarSelectionMode.Single)]
        public CalendarSelectionMode SelectionMode
        {
            get => _mode;
            set
            {
                _mode = value;
                if (_mode == CalendarSelectionMode.Range && _rangeSelector == null)
                    RangeSelector = new DateRangeSelector
                    {
                        MinDate = _minDate,
                        MaxDate = _maxDate,
                        DisablePastDates = _disablePastDates
                    };
                RecalcBaseMetrics();
                ApplyLayout();
                Invalidate();
            }
        }

        [Browsable(false)]
        public DateRangeSelector RangeSelector
        {
            get => _rangeSelector;
            set
            {
                if (_rangeSelector == value) return;
                if (_rangeSelector != null)
                {
                    _rangeSelector.RangeChanged -= OnRangeSelectorChanged;
                    _rangeSelector.RangeCompleted -= OnRangeSelectorCompleted;
                }
                _rangeSelector = value;
                if (_rangeSelector != null)
                {
                    _rangeSelector.RangeChanged += OnRangeSelectorChanged;
                    _rangeSelector.RangeCompleted += OnRangeSelectorCompleted;
                }
                Invalidate();
            }
        }

        // Desactiva flechas/click de título — lo usa CustomDateRangePanel,
        // que dibuja su propia navegación fuera de los calendarios.
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool AllowNavigation
        {
            get => _allowNavigation;
            set { _allowNavigation = value; Invalidate(); }
        }

        // No permite seleccionar fechas anteriores a hoy (hoy sí se puede).
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool DisablePastDates
        {
            get => _disablePastDates;
            set
            {
                _disablePastDates = value;
                if (_rangeSelector != null) _rangeSelector.DisablePastDates = value;
                Invalidate();
            }
        }

        private bool IsPastDisabledFor(DateTime date)
        {
            bool flag = (_mode == CalendarSelectionMode.Range && _rangeSelector != null)
                ? _rangeSelector.DisablePastDates
                : _disablePastDates;
            return flag && date.Date < DateTime.Today;
        }

        [Browsable(false)]
        public DateTime ViewDate => _viewDate;

        private void OnRangeSelectorChanged(object sender, EventArgs e)
        {
            Invalidate();
            RangeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnRangeSelectorCompleted(object sender, DateRangeEventArgs e)
        {
            RangeSelected?.Invoke(this, e);
        }

        public override Size GetPreferredSize(Size proposedSize) => PreferredSize;

        public new Size PreferredSize => ComputeMinSize();

        // ══════════════════════════════════════════════════════════════════
        // ── LAYOUT ────────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        private Size ComputeMinSize()
        {
            switch (_view)
            {
                case CalView.Days:
                    int rows = GetVisibleRowsForMonth(_viewDate);
                    return new Size(_baseCellW * Cols,
                                    _baseHeaderH + _baseDayNameH + _baseCellH * rows + _baseFooterH);

                case CalView.Months:
                case CalView.Years:
                    return new Size(_baseCellW * Cols,
                                    _baseHeaderH + 4 + (_baseCellH + 6) * GridRows + 4);

                default:
                    return new Size(_baseCellW * Cols, _baseHeaderH);
            }
        }

        private void UpdateMinimumSize()
        {
            MinimumSize = ComputeMinSize();
        }

        private void RecalcBaseMetrics()
        {
            using (var g = Graphics.FromHwnd(Handle == IntPtr.Zero ? IntPtr.Zero : Handle))
            {
                SizeF sample = g.MeasureString("30", Font);
                _baseCellW = (int)Math.Ceiling(sample.Width) + 18;
                _baseCellH = (int)Math.Ceiling(sample.Height) + 14;
            }

            _baseHeaderH = _baseCellH + 6;
            _baseDayNameH = _baseCellH - 4;
            _baseFooterH = (_showToday && _mode == CalendarSelectionMode.Single) ? _baseCellH : 0;

            UpdateMinimumSize();
        }

        /// <summary>Recalcula las métricas realmente usadas para pintar,
        /// a partir del tamaño ACTUAL del control (que nunca baja del
        /// mínimo, porque MinimumSize ya lo garantiza).</summary>
        private void ApplyLayout()
        {
            if (_view == CalView.Days)
            {
                int rows = GetVisibleRowsForMonth(_viewDate);
                _cellW = Width / Cols;
                int gridAvailH = Height - _baseHeaderH - _baseDayNameH - _baseFooterH;
                _cellH = gridAvailH / Math.Max(1, rows);
            }
            else
            {
                _cellW = _baseCellW;
                _cellH = _baseCellH;
            }

            _bigCellW = (_cellW * Cols) / GridCols;
            _bigCellH = _cellH + 6;

            _headerH = _baseHeaderH;
            _dayNameH = _baseDayNameH;
            _footerH = (_view == CalView.Days) ? _baseFooterH : 0;
            _gridTop = _headerH + _dayNameH;

            int btnSize = Math.Max(10, _headerH - 10);
            int btnY = (_headerH - btnSize) / 2;
            _prevBtn = new Rectangle(6, btnY, btnSize, btnSize);
            _nextBtn = new Rectangle(Math.Max(_prevBtn.Right, Width - btnSize - 6), btnY, btnSize, btnSize);
            _titleBtn = new Rectangle(_prevBtn.Right + 4, 0, Math.Max(0, _nextBtn.Left - _prevBtn.Right - 8), _headerH);
        }

        /// <summary>Cantidad de filas que hacen falta para mostrar el mes
        /// completo (4, 5 o 6) — evita la fila sobrante de puros días del
        /// mes siguiente.</summary>
        private int GetVisibleRowsForMonth(DateTime viewDate)
        {
            DateTime firstOfMonth = new DateTime(viewDate.Year, viewDate.Month, 1);
            int offset = ((int)firstOfMonth.DayOfWeek - (int)_firstDayOfWeek + 7) % 7;
            int daysInMonth = DateTime.DaysInMonth(viewDate.Year, viewDate.Month);
            return (int)Math.Ceiling((offset + daysInMonth) / (double)Cols);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            RecalcBaseMetrics();
            ApplyLayout();
            Invalidate();
        }

        // ══════════════════════════════════════════════════════════════════
        // ── PINTURA ───────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            DrawHeader(g);

            using (var pen = new Pen(_separatorColor))
                g.DrawLine(pen, 0, _headerH, Width, _headerH);

            switch (_view)
            {
                case CalView.Days:
                    DrawDayNames(g);
                    using (var pen = new Pen(_separatorColor))
                        g.DrawLine(pen, 0, _headerH + _dayNameH, Width, _headerH + _dayNameH);
                    DrawDays(g);
                    if (_showToday && _mode == CalendarSelectionMode.Single) DrawFooter(g);
                    break;

                case CalView.Months:
                    DrawMonthGrid(g);
                    break;

                case CalView.Years:
                    DrawYearGrid(g);
                    break;
            }
        }

        private void DrawHeader(Graphics g)
        {
            g.FillRectangle(new SolidBrush(_titleBackColor), new Rectangle(0, 0, Width, _headerH));

            if (_allowNavigation && _titleHover && _view != CalView.Years)
            {
                using (var path = RoundedRect(Inset(_titleBtn, 2), 4))
                using (var br = new SolidBrush(_navButtonHoverColor))
                    g.FillPath(br, path);
            }

            string title = GetHeaderTitle();
            using (var boldFont = new Font(Font, FontStyle.Bold))
                TextRenderer.DrawText(g, title, boldFont, _titleBtn, _titleForeColor,
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            if (_allowNavigation)
            {
                DrawNavButton(g, _prevBtn, "◄", _prevNavHover, CanGoPrev());
                DrawNavButton(g, _nextBtn, "►", _nextNavHover, CanGoNext());
            }
        }

        private string GetHeaderTitle()
        {
            switch (_view)
            {
                case CalView.Days:
                    return _culture.DateTimeFormat.GetMonthName(_viewDate.Month) + " " + _viewDate.Year;
                case CalView.Months:
                    return _viewDate.Year.ToString();
                case CalView.Years:
                    return _yearRangeStart + " – " + (_yearRangeStart + 11);
                default:
                    return string.Empty;
            }
        }

        private void DrawNavButton(Graphics g, Rectangle rect, string text, bool hover, bool enabled)
        {
            if (hover && enabled)
            {
                using (var path = RoundedRect(rect, 3))
                using (var br = new SolidBrush(_navButtonHoverColor))
                    g.FillPath(br, path);
            }
            Color fore = enabled ? _titleForeColor : Color.FromArgb(80, _titleForeColor);
            TextRenderer.DrawText(g, text, Font, rect, fore,
                                  TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void DrawDayNames(Graphics g)
        {
            int y = _headerH;
            for (int col = 0; col < Cols; col++)
            {
                DayOfWeek dow = (DayOfWeek)(((int)_firstDayOfWeek + col) % 7);
                string name = _culture.DateTimeFormat.GetShortestDayName(dow);
                var rect = new Rectangle(col * _cellW, y, _cellW, _dayNameH);
                TextRenderer.DrawText(g, name, Font, rect, _dayNameForeColor,
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private struct DayCellInfo
        {
            public bool IsCurrentMonth, IsToday, IsHover, IsDisabled, IsBlocked;
            public bool IsSelected;
            public bool IsRangeStart, IsRangeEnd, IsInRange;
            public bool IsPreviewEnd, IsInPreview;
        }

        private void DrawDays(Graphics g)
        {
            DateTime firstCell = GetFirstCellDate();
            DateTime today = DateTime.Today;
            int rows = GetVisibleRowsForMonth(_viewDate);

            DateTime? rangeStart = null, rangeEnd = null, hoverPreviewTo = null;
            bool previewBlocked = false;

            if (_mode == CalendarSelectionMode.Range && _rangeSelector != null)
            {
                rangeStart = _rangeSelector.Start;
                rangeEnd = _rangeSelector.End;

                if (rangeStart.HasValue && !rangeEnd.HasValue && _hoverCell >= 0)
                {
                    DateTime hoverDate = firstCell.AddDays(_hoverCell);
                    if (hoverDate.Date > rangeStart.Value.Date && hoverDate >= _minDate && hoverDate <= _maxDate)
                    {
                        hoverPreviewTo = hoverDate;
                        previewBlocked = _rangeSelector.IsBlocked(hoverDate)
                                          || _rangeSelector.HasBlockedBetween(rangeStart.Value, hoverDate);
                    }
                }
            }

            for (int i = 0; i < Cols * rows; i++)
            {
                DateTime date = firstCell.AddDays(i);
                int col = i % Cols;
                int row = i / Cols;
                var rect = new Rectangle(col * _cellW, _gridTop + row * _cellH, _cellW, _cellH);

                bool isBlocked = _mode == CalendarSelectionMode.Range &&
                                  _rangeSelector != null && _rangeSelector.IsBlocked(date);
                bool isPastDisabled = IsPastDisabledFor(date);

                var info = new DayCellInfo
                {
                    IsCurrentMonth = date.Month == _viewDate.Month,
                    IsToday = date.Date == today.Date,
                    IsHover = i == _hoverCell,
                    IsBlocked = isBlocked,
                    IsDisabled = date < _minDate || date > _maxDate || isBlocked || isPastDisabled
                };

                if (_mode == CalendarSelectionMode.Single)
                {
                    info.IsSelected = _selectedDate.HasValue && date.Date == _selectedDate.Value.Date;
                }
                else
                {
                    info.IsRangeStart = rangeStart.HasValue && date.Date == rangeStart.Value.Date;
                    info.IsRangeEnd = rangeEnd.HasValue && date.Date == rangeEnd.Value.Date;
                    info.IsInRange = rangeStart.HasValue && rangeEnd.HasValue &&
                                      date.Date > rangeStart.Value.Date && date.Date < rangeEnd.Value.Date;

                    if (!previewBlocked && hoverPreviewTo.HasValue && rangeStart.HasValue)
                    {
                        info.IsPreviewEnd = date.Date == hoverPreviewTo.Value.Date;
                        info.IsInPreview = date.Date > rangeStart.Value.Date && date.Date < hoverPreviewTo.Value.Date;
                    }
                }

                DrawDayCell(g, rect, date, info);
            }
        }

        private void DrawDayCell(Graphics g, Rectangle rect, DateTime date, DayCellInfo info)
        {
            bool isBand = info.IsRangeStart || info.IsRangeEnd || info.IsInRange;
            bool isPreview = info.IsPreviewEnd || info.IsInPreview;

            if (info.IsSelected)
            {
                using (var path = RoundedRect(Inset(rect, 2), 4))
                using (var br = new SolidBrush(_selectedBackColor))
                    g.FillPath(br, path);
            }
            else if (isBand)
            {
                var bandRect = new Rectangle(rect.X, rect.Y + 3, rect.Width, rect.Height - 6);
                Color fill = (info.IsRangeStart || info.IsRangeEnd) ? _selectedBackColor : _inRangeBackColor;
                using (var path = BandPath(bandRect, info.IsRangeStart, info.IsRangeEnd, 6))
                using (var br = new SolidBrush(fill))
                    g.FillPath(br, path);
            }
            else if (isPreview)
            {
                var bandRect = new Rectangle(rect.X, rect.Y + 3, rect.Width, rect.Height - 6);
                using (var path = BandPath(bandRect, false, info.IsPreviewEnd, 6))
                using (var br = new SolidBrush(_previewBackColor))
                    g.FillPath(br, path);
            }
            else if (info.IsBlocked)
            {
                using (var br = new SolidBrush(_blockedBackColor))
                    g.FillRectangle(br, rect);
            }
            else if (info.IsHover && !info.IsDisabled)
            {
                using (var path = RoundedRect(Inset(rect, 2), 4))
                using (var br = new SolidBrush(_hoverBackColor))
                    g.FillPath(br, path);
            }

            if (info.IsToday && _showTodayHighlight && !info.IsSelected && !info.IsRangeStart && !info.IsRangeEnd)
            {
                using (var path = RoundedRect(Inset(rect, 2), 4))
                using (var pen = new Pen(_todayHighlightColor, 1.5f))
                    g.DrawPath(pen, path);
            }

            Color fore;
            if (info.IsSelected || info.IsRangeStart || info.IsRangeEnd) fore = _selectedForeColor;
            else if (info.IsBlocked) fore = _blockedForeColor;
            else if (info.IsDisabled) fore = _disabledForeColor;
            else if (!info.IsCurrentMonth) fore = _trailingForeColor;
            else if (info.IsHover) fore = _hoverForeColor;
            else fore = _dayForeColor;

            TextRenderer.DrawText(g, date.Day.ToString(), Font, rect, fore,
                                  TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            if (info.IsBlocked)
            {
                using (var pen = new Pen(_blockedForeColor, 1f))
                    g.DrawLine(pen, rect.X + 6, rect.Bottom - 6, rect.Right - 6, rect.Y + 6);
            }
        }

        private void DrawFooter(Graphics g)
        {
            int rows = GetVisibleRowsForMonth(_viewDate);
            int y = _gridTop + rows * _cellH;
            var rect = new Rectangle(0, y, Width, _footerH);
            string txt = "Hoy: " + DateTime.Today.ToString("dd/MM/yyyy");

            using (var pen = new Pen(_separatorColor))
                g.DrawLine(pen, 0, y, Width, y);

            TextRenderer.DrawText(g, txt, Font, rect, _footerForeColor,
                                  TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void DrawMonthGrid(Graphics g)
        {
            int gridTop = _headerH + 4;
            for (int i = 0; i < 12; i++)
            {
                int col = i % GridCols;
                int row = i / GridCols;
                var rect = new Rectangle(col * _bigCellW, gridTop + row * _bigCellH, _bigCellW, _bigCellH);

                bool isSelected = _selectedDate.HasValue &&
                                  _selectedDate.Value.Year == _viewDate.Year &&
                                  _selectedDate.Value.Month == i + 1;
                bool isHover = i == _hoverCell;
                bool isDisabled = !MonthInRange(i + 1, _viewDate.Year);
                bool isCurrent = DateTime.Today.Month == i + 1 && DateTime.Today.Year == _viewDate.Year;

                DrawBigCell(g, rect, _culture.DateTimeFormat.GetAbbreviatedMonthName(i + 1),
                            isSelected, isHover, isDisabled, isCurrent);
            }
        }

        private void DrawYearGrid(Graphics g)
        {
            int gridTop = _headerH + 4;
            for (int i = 0; i < 12; i++)
            {
                int year = _yearRangeStart + i;
                int col = i % GridCols;
                int row = i / GridCols;
                var rect = new Rectangle(col * _bigCellW, gridTop + row * _bigCellH, _bigCellW, _bigCellH);

                bool isSelected = _selectedDate.HasValue && _selectedDate.Value.Year == year;
                bool isHover = i == _hoverCell;
                bool isDisabled = year < _minDate.Year || year > _maxDate.Year;
                bool isCurrent = year == DateTime.Today.Year;

                DrawBigCell(g, rect, year.ToString(), isSelected, isHover, isDisabled, isCurrent);
            }
        }

        private void DrawBigCell(Graphics g, Rectangle rect, string text,
                                 bool selected, bool hover, bool disabled, bool isCurrent)
        {
            var inner = Inset(rect, 3);

            if (selected)
            {
                using (var path = RoundedRect(inner, 5))
                using (var br = new SolidBrush(_selectedBackColor))
                    g.FillPath(br, path);
            }
            else if (hover && !disabled)
            {
                using (var path = RoundedRect(inner, 5))
                using (var br = new SolidBrush(_hoverBackColor))
                    g.FillPath(br, path);
            }

            if (isCurrent && !selected)
            {
                using (var path = RoundedRect(inner, 5))
                using (var pen = new Pen(_todayHighlightColor, 1.5f))
                    g.DrawPath(pen, path);
            }

            Color fore;
            if (selected) fore = _selectedForeColor;
            else if (disabled) fore = _disabledForeColor;
            else if (hover) fore = _hoverForeColor;
            else fore = _dayForeColor;

            TextRenderer.DrawText(g, text, Font, rect, fore,
                                  TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // ══════════════════════════════════════════════════════════════════
        // ── INTERACCIÓN ───────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool prevP = _prevNavHover, nextP = _nextNavHover, titleP = _titleHover;
            _prevNavHover = _allowNavigation && _prevBtn.Contains(e.Location);
            _nextNavHover = _allowNavigation && _nextBtn.Contains(e.Location);
            _titleHover = _allowNavigation && _titleBtn.Contains(e.Location) && _view != CalView.Years;

            int newHover = HitTestCell(e.Location);

            if (newHover != _hoverCell || prevP != _prevNavHover || nextP != _nextNavHover || titleP != _titleHover)
            {
                _hoverCell = newHover;

                bool hoverSelectable = _hoverCell >= 0;
                if (hoverSelectable && _view == CalView.Days)
                {
                    DateTime hd = GetFirstCellDate().AddDays(_hoverCell);
                    bool blocked = _mode == CalendarSelectionMode.Range &&
                                   _rangeSelector != null && _rangeSelector.IsBlocked(hd);
                    hoverSelectable = hd >= _minDate && hd <= _maxDate && !blocked && !IsPastDisabledFor(hd);
                }

                Cursor = (hoverSelectable || _titleHover) ? Cursors.Hand : Cursors.Default;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hoverCell = -1;
            _prevNavHover = false;
            _nextNavHover = false;
            _titleHover = false;
            Cursor = Cursors.Default;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left) return;

            if (_allowNavigation)
            {
                if (_prevBtn.Contains(e.Location) && CanGoPrev()) { NavigatePrev(); return; }
                if (_nextBtn.Contains(e.Location) && CanGoNext()) { NavigateNext(); return; }

                if (_titleBtn.Contains(e.Location))
                {
                    if (_view == CalView.Days)
                    {
                        _view = CalView.Months;
                        _hoverCell = -1;
                        UpdateMinimumSize();
                        Size = PreferredSize;
                        ApplyLayout();
                        SizeChanged2?.Invoke(this, EventArgs.Empty);
                        Invalidate();
                    }
                    else if (_view == CalView.Months)
                    {
                        _yearRangeStart = _viewDate.Year - 5;
                        _view = CalView.Years;
                        _hoverCell = -1;
                        UpdateMinimumSize();
                        Size = PreferredSize;
                        ApplyLayout();
                        SizeChanged2?.Invoke(this, EventArgs.Empty);
                        Invalidate();
                    }
                    return;
                }
            }

            int idx = HitTestCell(e.Location);
            if (idx < 0) return;

            switch (_view)
            {
                case CalView.Days:
                    DateTime date = GetFirstCellDate().AddDays(idx);
                    if (_mode == CalendarSelectionMode.Range)
                        _rangeSelector?.Select(date);
                    else if (date >= _minDate && date <= _maxDate && !IsPastDisabledFor(date))
                        SelectDate(date);
                    break;

                case CalView.Months:
                    int month = idx + 1;
                    if (MonthInRange(month, _viewDate.Year))
                    {
                        _viewDate = new DateTime(_viewDate.Year, month, 1);
                        _view = CalView.Days;
                        _hoverCell = -1;
                        UpdateMinimumSize();
                        Size = PreferredSize;
                        ApplyLayout();
                        ViewMonthChanged?.Invoke(this, EventArgs.Empty);
                        SizeChanged2?.Invoke(this, EventArgs.Empty);
                        Invalidate();
                    }
                    break;

                case CalView.Years:
                    int year = _yearRangeStart + idx;
                    if (year >= _minDate.Year && year <= _maxDate.Year)
                    {
                        _viewDate = new DateTime(year, _viewDate.Month, 1);
                        _view = CalView.Months;
                        _hoverCell = -1;
                        UpdateMinimumSize();
                        Size = PreferredSize;
                        ApplyLayout();
                        ViewMonthChanged?.Invoke(this, EventArgs.Empty);
                        SizeChanged2?.Invoke(this, EventArgs.Empty);
                        Invalidate();
                    }
                    break;
            }

            if (_view == CalView.Days && _showToday && _mode == CalendarSelectionMode.Single)
            {
                int rows = GetVisibleRowsForMonth(_viewDate);
                int footerY = _gridTop + rows * _cellH;
                var footerRect = new Rectangle(0, footerY, Width, _footerH);
                if (footerRect.Contains(e.Location))
                    SelectDate(DateTime.Today);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (_view != CalView.Days) { e.Handled = true; return; }

            if (_mode == CalendarSelectionMode.Range)
                return; // selección por teclado no implementada en modo Rango

            DateTime current = _selectedDate ?? DateTime.Today;
            switch (e.KeyCode)
            {
                case Keys.Left: TrySelect(current.AddDays(-1)); e.Handled = true; break;
                case Keys.Right: TrySelect(current.AddDays(+1)); e.Handled = true; break;
                case Keys.Up: TrySelect(current.AddDays(-7)); e.Handled = true; break;
                case Keys.Down: TrySelect(current.AddDays(+7)); e.Handled = true; break;
                case Keys.PageUp: NavigatePrev(); e.Handled = true; break;
                case Keys.PageDown: NavigateNext(); e.Handled = true; break;
                case Keys.Home:
                    TrySelect(new DateTime(current.Year, current.Month, 1));
                    e.Handled = true; break;
                case Keys.End:
                    TrySelect(new DateTime(current.Year, current.Month,
                        DateTime.DaysInMonth(current.Year, current.Month)));
                    e.Handled = true; break;
                case Keys.Return:
                case Keys.Space:
                    if (_selectedDate.HasValue)
                        FireDateSelected(_selectedDate.Value);
                    e.Handled = true; break;
                case Keys.Escape:
                    _view = CalView.Days; Invalidate();
                    e.Handled = true; break;
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── HELPERS ───────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        private void NavigatePrev()
        {
            switch (_view)
            {
                case CalView.Days: UpdateViewDate(_viewDate.AddMonths(-1)); break;
                case CalView.Months: UpdateViewDate(_viewDate.AddYears(-1)); break;
                case CalView.Years: _yearRangeStart -= 12; break;
            }
            _hoverCell = -1;
            Invalidate();
        }

        private void NavigateNext()
        {
            switch (_view)
            {
                case CalView.Days: UpdateViewDate(_viewDate.AddMonths(+1)); break;
                case CalView.Months: UpdateViewDate(_viewDate.AddYears(+1)); break;
                case CalView.Years: _yearRangeStart += 12; break;
            }
            _hoverCell = -1;
            Invalidate();
        }

        private bool CanGoPrev()
        {
            switch (_view)
            {
                case CalView.Days: return CanGoToPreviousMonth;
                case CalView.Months: return _viewDate.Year - 1 >= _minDate.Year;
                case CalView.Years: return _yearRangeStart - 1 >= _minDate.Year;
                default: return false;
            }
        }

        private bool CanGoNext()
        {
            switch (_view)
            {
                case CalView.Days: return CanGoToNextMonth;
                case CalView.Months: return _viewDate.Year + 1 <= _maxDate.Year;
                case CalView.Years: return _yearRangeStart + 12 <= _maxDate.Year;
                default: return false;
            }
        }

        private int HitTestCell(Point pt)
        {
            switch (_view)
            {
                case CalView.Days:
                    {
                        int x = pt.X / _cellW;
                        int y = (pt.Y - _gridTop) / _cellH;
                        int rows = GetVisibleRowsForMonth(_viewDate);
                        if (x < 0 || x >= Cols || y < 0 || y >= rows) return -1;
                        return y * Cols + x;
                    }
                case CalView.Months:
                case CalView.Years:
                    {
                        int gridTop = _headerH + 4;
                        int x = pt.X / _bigCellW;
                        int y = (pt.Y - gridTop) / _bigCellH;
                        if (x < 0 || x >= GridCols || y < 0 || y >= GridRows) return -1;
                        return y * GridCols + x;
                    }
                default: return -1;
            }
        }

        private DateTime GetFirstCellDate()
        {
            DateTime first = new DateTime(_viewDate.Year, _viewDate.Month, 1);
            int offset = ((int)first.DayOfWeek - (int)_firstDayOfWeek + 7) % 7;
            return first.AddDays(-offset);
        }

        private bool MonthInRange(int month, int year)
        {
            var first = new DateTime(year, month, 1);
            var last = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return last >= _minDate && first <= _maxDate;
        }

        private void SelectDate(DateTime date)
        {
            _selectedDate = date;
            UpdateViewDate(date);
            Invalidate();
            FireDateSelected(date);
        }

        private void TrySelect(DateTime date)
        {
            if (date < _minDate || date > _maxDate || IsPastDisabledFor(date)) return;
            _selectedDate = date;
            UpdateViewDate(date);
            Invalidate();
        }

        private void FireDateSelected(DateTime date)
            => DateSelected?.Invoke(this, new DateRangeEventArgs(date, date));

        private void UpdateViewDate(DateTime newDate)
        {
            newDate = new DateTime(newDate.Year, newDate.Month, 1);
            if (newDate == _viewDate) return;
            _viewDate = newDate;
            UpdateMinimumSize();
            ApplyLayout();
            Invalidate();
            ViewMonthChanged?.Invoke(this, EventArgs.Empty);
            if (_view == CalView.Days)
                SizeChanged2?.Invoke(this, EventArgs.Empty);
        }

        private static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            float d = radius * 2f;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static GraphicsPath BandPath(Rectangle rect, bool roundLeft, bool roundRight, int radius)
        {
            var path = new GraphicsPath();
            float d = radius * 2f;
            float x = rect.X, y = rect.Y, w = rect.Width, h = rect.Height;
            float rL = roundLeft ? radius : 0;
            float rR = roundRight ? radius : 0;

            path.AddLine(x + rL, y, x + w - rR, y);
            if (roundRight) path.AddArc(x + w - d, y, d, d, 270, 90);
            path.AddLine(x + w, y + rR, x + w, y + h - rR);
            if (roundRight) path.AddArc(x + w - d, y + h - d, d, d, 0, 90);
            path.AddLine(x + w - rR, y + h, x + rL, y + h);
            if (roundLeft) path.AddArc(x, y + h - d, d, d, 90, 90);
            path.AddLine(x, y + h - rL, x, y + rL);
            if (roundLeft) path.AddArc(x, y, d, d, 180, 90);
            path.CloseFigure();
            return path;
        }

        private static Rectangle Inset(Rectangle rect, int margin) =>
            new Rectangle(rect.X + margin, rect.Y + margin, rect.Width - margin * 2, rect.Height - margin * 2);
    }
}