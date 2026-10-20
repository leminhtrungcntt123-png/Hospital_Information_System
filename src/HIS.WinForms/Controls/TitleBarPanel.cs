using System.Drawing;
using System.Drawing.Drawing2D;

namespace HIS.WinForms.Controls;

public class TitleBarPanel : Panel
{
    public TitleBarPanel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
        {
            base.OnPaintBackground(e);
            return;
        }

        using var brush = new LinearGradientBrush(
            ClientRectangle,
            Color.FromArgb(13, 49, 86),
            Color.FromArgb(23, 79, 130),
            LinearGradientMode.Horizontal);

        e.Graphics.FillRectangle(brush, ClientRectangle);
    }
}
