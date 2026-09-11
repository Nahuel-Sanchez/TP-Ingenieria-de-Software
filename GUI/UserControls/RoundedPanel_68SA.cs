using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    public class RoundedPanel_68SA : Panel
    {
        private int _cornerRadius = 14;

        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; AplicarRegion(); }
        }

        public RoundedPanel_68SA()
        {
            AplicarRegion();
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            AplicarRegion();
        }

        private void AplicarRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            var path = new GraphicsPath();
            int d = _cornerRadius * 2;

            path.AddArc(0, 0, d, d, 180, 90);
            path.AddArc(Width - d, 0, d, d, 270, 90);
            path.AddArc(Width - d, Height - d, d, d, 0, 90);
            path.AddArc(0, Height - d, d, d, 90, 90);
            path.CloseFigure();

            Region = new Region(path);
        }
    }
}
