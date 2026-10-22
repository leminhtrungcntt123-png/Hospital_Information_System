using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HIS.WinForms.Controls;

public class SimpleBarChart : Control
{
    private int[] _values = { 28, 52, 44, 61, 49, 36, 25, 18 };
    private string[] _labels = { "7:00", "9:00", "11:00", "13:00", "15:00", "17:00", "19:00", "21:00" };
    private int _maximum = 60;

    public SimpleBarChart()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true);
        BackColor = Color.White;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int[] Values
    {
        get => _values;
        set { _values = value ?? Array.Empty<int>(); Invalidate(); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] Labels
    {
        get => _labels;
        set { _labels = value ?? Array.Empty<string>(); Invalidate(); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Maximum
    {
        get => _maximum;
        set { _maximum = Math.Max(1, value); Invalidate(); }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(BackColor);

        const int left = 28;
        const int right = 6;
        const int top = 4;
        const int bottom = 22;

        int plotWidth = Width - left - right;
        int plotHeight = Height - top - bottom;
        if (plotWidth <= 0 || plotHeight <= 0 || _values.Length == 0) return;

        using var gridPen = new Pen(Color.FromArgb(233, 237, 242));
        using var axisPen = new Pen(Color.FromArgb(212, 219, 228));
        using var labelBrush = new SolidBrush(Color.FromArgb(147, 158, 172));
        using var font = new Font("Segoe UI", 7F);

        for (int i = 0; i <= 3; i++)
        {
            int y = top + plotHeight * i / 3;
            g.DrawLine(i == 3 ? axisPen : gridPen, left, y, left + plotWidth, y);
            string text = (_maximum * (3 - i) / 3).ToString();
            g.DrawString(text, font, labelBrush, 2, y - 7);
        }

        int slotWidth = plotWidth / _values.Length;
        int barWidth = Math.Min(25, Math.Max(10, (int)(slotWidth * 0.55)));

        using var barBrush = new LinearGradientBrush(
            new Point(0, top),
            new Point(0, top + plotHeight),
            Color.FromArgb(61, 145, 213),
            Color.FromArgb(23, 103, 171));

        for (int i = 0; i < _values.Length; i++)
        {
            int barHeight = Math.Max(4, (int)Math.Round((double)_values[i] / _maximum * plotHeight));
            int x = left + slotWidth * i + (slotWidth - barWidth) / 2;
            int y = top + plotHeight - barHeight;
            g.FillRectangle(barBrush, x, y, barWidth, barHeight);

            if (i < _labels.Length)
            {
                var size = g.MeasureString(_labels[i], font);
                g.DrawString(
                    _labels[i],
                    font,
                    labelBrush,
                    x + barWidth / 2f - size.Width / 2f,
                    top + plotHeight + 5);
            }
        }
    }
}
