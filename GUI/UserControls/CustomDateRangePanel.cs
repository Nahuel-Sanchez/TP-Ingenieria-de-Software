using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    // ══════════════════════════════════════════════════════════════════════
    //  CustomDateRangePanel
    //
    //  Calendario grande para elegir un período (check-in / check-out).
    //  Arma uno o dos CustomCalendarPanel (según MonthsVisible) en modo
    //  Range, compartiendo un DateRangeSelector. La navegación (◄ ►) es
    //  propia del control, en gutters a los costados — los calendarios
    //  internos van con AllowNavigation = false. Debajo hay un link para
    //  limpiar la selección.
    // ══════════════════════════════════════════════════════════════════════
    [ToolboxItem(true)]
    [DefaultProperty(nameof(RangeStart))]
    [DefaultEvent(nameof(RangeSelected))]
    public class CustomDateRangePanel : UserControl
    {
        private const int PanelGap = 24;
        private const int ArrowGutterWidth = 32;
        private const int ClearBarHeight = 30;

        private readonly DateRangeSelector _selector = new DateRangeSelector();
        private CustomCalendarPanel _left;
        private CustomCalendarPanel _right;
        private LinkLabel _clearLink;
        private int _monthsVisible = 2;
        private bool _showClearButton = true;

        private Rectangle _prevArrowRect;
        private Rectangle _nextArrowRect;
        private bool _prevHover;
        private bool _nextHover;

        public event DateRangeEventHandler RangeSelected;
        public event EventHandler RangeChanged;

        public CustomDateRangePanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            _selector.RangeChanged += (s, e) =>
            {
                _clearLink.Enabled = _selector.Start.HasValue;
                RangeChanged?.Invoke(this, EventArgs.Empty);
            };
            _selector.RangeCompleted += (s, e) => RangeSelected?.Invoke(this, e);

            BackColor = SystemColors.Window;

            _clearLink = new LinkLabel
            {
                Text = "Limpiar selección",
                TextAlign = ContentAlignment.MiddleCenter,
                LinkBehavior = LinkBehavior.HoverUnderline,
                BackColor = Color.Transparent,
                Enabled = false
            };
            _clearLink.Click += (s, e) => _selector.Clear();
            Controls.Add(_clearLink);

            BuildPanels();
        }

        // ══════════════════════════════════════════════════════════════════
        // ── DATOS ─────────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Browsable(false)]
        public DateTime? RangeStart => _selector.Start;

        [Browsable(false)]
        public DateTime? RangeEnd => _selector.End;

        [Browsable(false)]
        public int? Nights => (_selector.Start.HasValue && _selector.End.HasValue)
            ? (int?)(_selector.End.Value - _selector.Start.Value).Days
            : null;

        [Category("Data")]
        public DateTime MinDate
        {
            get => _selector.MinDate;
            set { _selector.MinDate = value; PushBoundsToChildren(); }
        }

        [Category("Data")]
        public DateTime MaxDate
        {
            get => _selector.MaxDate;
            set { _selector.MaxDate = value; PushBoundsToChildren(); }
        }

        [Category("Data")]
        [DefaultValue(1)]
        public int MinNights
        {
            get => _selector.MinNights;
            set => _selector.MinNights = Math.Max(1, value);
        }

        [Category("Data")]
        public int? MaxNights
        {
            get => _selector.MaxNights;
            set => _selector.MaxNights = value;
        }

        [Category("Data")]
        [DefaultValue(false)]
        public bool DisablePastDates
        {
            get => _selector.DisablePastDates;
            set
            {
                _selector.DisablePastDates = value;
                _left?.Invalidate();
                _right?.Invalidate();
            }
        }

        public void SetBlockedDates(IEnumerable<DateTime> dates)
        {
            _selector.BlockedDates.Clear();
            if (dates != null)
                foreach (var d in dates)
                    _selector.BlockedDates.Add(d.Date);
            _left?.Invalidate();
            _right?.Invalidate();
        }

        public void SetRange(DateTime start, DateTime? end)
        {
            _selector.SetRange(start, end);
            _left.SetDate(start);
        }

        public void ClearSelection() => _selector.Clear();

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool ShowClearButton
        {
            get => _showClearButton;
            set { _showClearButton = value; PositionPanels(); }
        }

        [Category("Data")]
        public string ClearButtonText
        {
            get => _clearLink.Text;
            set => _clearLink.Text = value;
        }

        [Category("Calendar")]
        public Color ClearButtonColor
        {
            get => _clearLink.LinkColor;
            set
            {
                _clearLink.LinkColor = value;
                _clearLink.VisitedLinkColor = value;
                _clearLink.ActiveLinkColor = ControlPaint.Light(value, 0.3f);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── LAYOUT ────────────────────────────────────────────────────────
        // ══════════════════════════════════════════════════════════════════

        [Category("Layout")]
        [DefaultValue(2)]
        public int MonthsVisible
        {
            get => _monthsVisible;
            set
            {
                int v = value <= 1 ? 1 : 2;
                if (v == _monthsVisible) return;
                _monthsVisible = v;
                BuildPanels();
            }
        }

        private void BuildPanels()
        {
            SuspendLayout();

            if (_left != null) { Controls.Remove(_left); _left.ViewMonthChanged -= Left_ViewMonthChanged; }
            if (_right != null) { Controls.Remove(_right); }

            _left = new CustomCalendarPanel
            {
                SelectionMode = CalendarSelectionMode.Range,
                RangeSelector = _selector,
                AllowNavigation = false
            };
            _left.ViewMonthChanged += Left_ViewMonthChanged;
            Controls.Add(_left);
            _left.BringToFront();

            if (_monthsVisible == 2)
            {
                _right = new CustomCalendarPanel
                {
                    SelectionMode = CalendarSelectionMode.Range,
                    RangeSelector = _selector,
                    AllowNavigation = false
                };
                Controls.Add(_right);
                _right.BringToFront();
                _right.SetDate(_left.ViewDate.AddMonths(1));
            }
            else
            {
                _right = null;
            }

            _clearLink.BringToFront();

            PushBoundsToChildren();
            ApplyStyleToChildren();
            PositionPanels();
            ResumeLayout();
        }

        private void Left_ViewMonthChanged(object sender, EventArgs e)
        {
            _right?.SetDate(_left.ViewDate.AddMonths(1));
            PositionPanels();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionPanels();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            PositionPanels();
        }

        private void PositionPanels()
        {
            if (_left == null) return;

            Size minLeft = _left.PreferredSize;
            Size minRight = _right?.PreferredSize ?? Size.Empty;

            int gap = _right != null ? PanelGap : 0;
            int clearH = _showClearButton ? ClearBarHeight : 0;

            int minCalendarsW = minLeft.Width + (_right != null ? gap + minRight.Width : 0);
            int minTotalW = ArrowGutterWidth * 2 + minCalendarsW;
            int minTotalH = Math.Max(minLeft.Height, minRight.Height) + clearH;
            MinimumSize = new Size(minTotalW, minTotalH);

            int availW = Math.Max(Width, minTotalW);
            int availH = Math.Max(Height, minTotalH);
            int calendarsW = availW - ArrowGutterWidth * 2;
            int calendarsH = availH - clearH;

            if (_right != null)
            {
                int eachW = (calendarsW - gap) / 2;
                _left.Location = new Point(ArrowGutterWidth, 0);
                _left.Size = new Size(eachW, calendarsH);
                _right.Location = new Point(ArrowGutterWidth + eachW + gap, 0);
                _right.Size = new Size(calendarsW - eachW - gap, calendarsH);
            }
            else
            {
                _left.Location = new Point(ArrowGutterWidth, 0);
                _left.Size = new Size(calendarsW, calendarsH);
            }

            _prevArrowRect = new Rectangle(0, 0, ArrowGutterWidth, calendarsH);
            _nextArrowRect = new Rectangle(availW - ArrowGutterWidth, 0, ArrowGutterWidth, calendarsH);

            if (_showClearButton)
            {
                _clearLink.Visible = true;
                _clearLink.Location = new Point(0, calendarsH);
                _clearLink.Size = new Size(availW, clearH);
            }
            else
            {
                _clearLink.Visible = false;
            }

            Invalidate();
        }

        private void PushBoundsToChildren()
        {
            if (_left != null) { _left.MinDate = _selector.MinDate; _left.MaxDate = _selector.MaxDate; }
            if (_right != null) { _right.MinDate = _selector.MinDate; _right.MaxDate = _selector.MaxDate; }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── FLECHAS (a los costados del control, no dentro de un calendario)
        // ══════════════════════════════════════════════════════════════════

        private CustomCalendarPanel RightmostCalendar() => _right ?? _left;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawArrow(g, _prevArrowRect, "◄", _prevHover, _left != null && _left.CanGoToPreviousMonth);
            DrawArrow(g, _nextArrowRect, "►", _nextHover, RightmostCalendar()?.CanGoToNextMonth ?? false);
        }

        private void DrawArrow(Graphics g, Rectangle rect, string glyph, bool hover, bool enabled)
        {
            if (rect.Width <= 0 || rect.Height <= 0) return;

            if (hover && enabled)
            {
                using (var br = new SolidBrush(_arrowHoverBackColor))
                    g.FillRectangle(br, rect);
            }
            Color fore = enabled ? _arrowForeColor : Color.FromArgb(120, _arrowForeColor);
            using (var f = new Font(Font, FontStyle.Bold))
                TextRenderer.DrawText(g, glyph, f, rect, fore,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool p = _prevArrowRect.Contains(e.Location);
            bool n = _nextArrowRect.Contains(e.Location);
            if (p != _prevHover || n != _nextHover)
            {
                _prevHover = p;
                _nextHover = n;
                Cursor = (p || n) ? Cursors.Hand : Cursors.Default;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _prevHover = _nextHover = false;
            Cursor = Cursors.Default;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left || _left == null) return;

            if (_prevArrowRect.Contains(e.Location) && _left.CanGoToPreviousMonth)
            {
                _left.SetDate(_left.ViewDate.AddMonths(-1));
            }
            else if (_nextArrowRect.Contains(e.Location) && (RightmostCalendar()?.CanGoToNextMonth ?? false))
            {
                _left.SetDate(_left.ViewDate.AddMonths(1));
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // ── COLORES Y ESTILO (pass-through a los calendarios internos) ───
        // ══════════════════════════════════════════════════════════════════

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
        private Color _separatorColor = Color.FromArgb(200, 200, 200);
        private Color _footerForeColor = SystemColors.WindowText;
        private Color _inRangeBackColor = Color.FromArgb(210, 230, 250);
        private Color _previewBackColor = Color.FromArgb(235, 242, 250);
        private Color _blockedBackColor = Color.FromArgb(246, 236, 236);
        private Color _blockedForeColor = Color.FromArgb(190, 120, 120);
        private bool _showTodayHighlight = true;
        private Color _arrowForeColor = Color.FromArgb(90, 90, 90);
        private Color _arrowHoverBackColor = Color.FromArgb(230, 230, 230);

        [Category("Calendar")] public Color TitleBackColor { get => _titleBackColor; set { _titleBackColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color TitleForeColor { get => _titleForeColor; set { _titleForeColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color DayNameForeColor { get => _dayNameForeColor; set { _dayNameForeColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color DayForeColor { get => _dayForeColor; set { _dayForeColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color TrailingForeColor { get => _trailingForeColor; set { _trailingForeColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color TodayHighlightColor { get => _todayHighlightColor; set { _todayHighlightColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color SelectedBackColor { get => _selectedBackColor; set { _selectedBackColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color SelectedForeColor { get => _selectedForeColor; set { _selectedForeColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color HoverBackColor { get => _hoverBackColor; set { _hoverBackColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color HoverForeColor { get => _hoverForeColor; set { _hoverForeColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color DisabledForeColor { get => _disabledForeColor; set { _disabledForeColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color SeparatorColor { get => _separatorColor; set { _separatorColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color FooterForeColor { get => _footerForeColor; set { _footerForeColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color InRangeBackColor { get => _inRangeBackColor; set { _inRangeBackColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color PreviewBackColor { get => _previewBackColor; set { _previewBackColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color BlockedBackColor { get => _blockedBackColor; set { _blockedBackColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public Color BlockedForeColor { get => _blockedForeColor; set { _blockedForeColor = value; ApplyStyleToChildren(); } }
        [Category("Calendar")] public bool ShowTodayHighlight { get => _showTodayHighlight; set { _showTodayHighlight = value; ApplyStyleToChildren(); } }
        [Category("Arrows")] public Color ArrowForeColor { get => _arrowForeColor; set { _arrowForeColor = value; Invalidate(); } }
        [Category("Arrows")] public Color ArrowHoverBackColor { get => _arrowHoverBackColor; set { _arrowHoverBackColor = value; Invalidate(); } }

        private void ApplyStyleToChildren()
        {
            foreach (var panel in new[] { _left, _right })
            {
                if (panel == null) continue;
                panel.Font = Font;
                panel.BackColor = BackColor;
                panel.TitleBackColor = _titleBackColor;
                panel.TitleForeColor = _titleForeColor;
                panel.DayNameForeColor = _dayNameForeColor;
                panel.DayForeColor = _dayForeColor;
                panel.TrailingForeColor = _trailingForeColor;
                panel.TodayHighlightColor = _todayHighlightColor;
                panel.SelectedBackColor = _selectedBackColor;
                panel.SelectedForeColor = _selectedForeColor;
                panel.HoverBackColor = _hoverBackColor;
                panel.HoverForeColor = _hoverForeColor;
                panel.DisabledForeColor = _disabledForeColor;
                panel.SeparatorColor = _separatorColor;
                panel.FooterForeColor = _footerForeColor;
                panel.InRangeBackColor = _inRangeBackColor;
                panel.PreviewBackColor = _previewBackColor;
                panel.BlockedBackColor = _blockedBackColor;
                panel.BlockedForeColor = _blockedForeColor;
                panel.ShowTodayHighlight = _showTodayHighlight;
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            ApplyStyleToChildren();
            PositionPanels();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            ApplyStyleToChildren();
        }
    }
}