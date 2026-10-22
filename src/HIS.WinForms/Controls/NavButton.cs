using System.ComponentModel;
using System.Drawing;

namespace HIS.WinForms.Controls;

public class NavButton : Button
{
    private bool _active;

    public NavButton()
    {
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 70, 111);
        FlatAppearance.MouseDownBackColor = Color.FromArgb(28, 82, 128);
        TextAlign = ContentAlignment.MiddleLeft;
        Padding = new Padding(14, 0, 0, 0);
        Font = new Font("Segoe UI", 8.5F);
        ForeColor = Color.FromArgb(184, 200, 216);
        BackColor = Color.FromArgb(11, 39, 70);
        Cursor = Cursors.Hand;
        UseVisualStyleBackColor = false;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Active
    {
        get => _active;
        set
        {
            if (_active == value) return;
            _active = value;
            BackColor = value ? Color.FromArgb(25, 99, 160) : Color.FromArgb(11, 39, 70);
            ForeColor = value ? Color.White : Color.FromArgb(184, 200, 216);
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        if (!_active) return;

        using var brush = new SolidBrush(Color.FromArgb(101, 189, 255));
        pevent.Graphics.FillRectangle(brush, 0, 0, 3, Height);
    }
}
