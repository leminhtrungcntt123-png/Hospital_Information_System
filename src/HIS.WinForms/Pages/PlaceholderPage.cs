using HIS.WinForms.Controls;

namespace HIS.WinForms.Pages;

public class PlaceholderPage : UserControl
{
    public PlaceholderPage(string title, string subtitle)
    {
        Dock = DockStyle.Fill;
        BackColor = Theme.Canvas;
        AutoScroll = true;

        var header = new Panel
        {
            Dock = DockStyle.Top,
            Height = 90,
            BackColor = Theme.Canvas
        };

        var lblTitle = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 14.25F, FontStyle.Bold),
            ForeColor = Color.FromArgb(26, 42, 62),
            Location = new Point(24, 20),
            Text = title
        };

        var lblSubtitle = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 8.25F),
            ForeColor = Theme.Muted,
            Location = new Point(25, 49),
            Text = subtitle
        };

        header.Controls.Add(lblSubtitle);
        header.Controls.Add(lblTitle);

        var body = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Theme.Canvas,
            Padding = new Padding(24, 0, 24, 28)
        };

        var card = new CardPanel { Dock = DockStyle.Fill };

        var message = new Label
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F),
            ForeColor = Theme.Muted,
            Text = "Trang này đang được xây dựng theo thiết kế Figma và sẽ hoàn thiện ở bước tiếp theo.",
            TextAlign = ContentAlignment.MiddleCenter
        };

        card.Controls.Add(message);
        body.Controls.Add(card);

        Controls.Add(body);
        Controls.Add(header);
    }
}
