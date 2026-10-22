using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HIS.WinForms.Controls;

public class StatCard : Panel
{
    private readonly Label _valueLabel;

    public StatCard(string title, Color accentColor)
    {
        BackColor = Color.White;

        var accentBar = new Panel
        {
            Dock = DockStyle.Left,
            Width = 5,
            BackColor = accentColor
        };

        var titleLabel = new Label
        {
            Text = title,
            AutoSize = false,
            Location = new Point(20, 16),
            Size = new Size(190, 22),
            Font = new Font("Segoe UI", 9.5F),
            ForeColor = Color.FromArgb(102, 117, 138)
        };

        _valueLabel = new Label
        {
            Text = "—",
            AutoSize = false,
            Location = new Point(20, 42),
            Size = new Size(190, 42),
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            ForeColor = Color.FromArgb(23, 38, 59)
        };

        Controls.Add(titleLabel);
        Controls.Add(_valueLabel);
        Controls.Add(accentBar);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Value
    {
        set => _valueLabel.Text = value;
    }
}
