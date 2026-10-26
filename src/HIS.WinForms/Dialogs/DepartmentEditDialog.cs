using System.Drawing;
using System.Windows.Forms;

namespace HIS.WinForms.Dialogs;

public class DepartmentEditDialog : Form
{
    private readonly TextBox _txtName;
    private readonly TextBox _txtDescription;
    private readonly Button _btnSave;
    private readonly Button _btnCancel;

    public string DepartmentName => _txtName.Text.Trim();
    public string Description => _txtDescription.Text.Trim();

    public DepartmentEditDialog(string? name = null, string? description = null, bool isEdit = false)
    {
        Text = isEdit ? "Chỉnh sửa khoa/phòng" : "Thêm khoa/phòng mới";
        Size = new Size(460, 310);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        BackColor = Color.White;
        Font = new Font("Segoe UI", 9.5F);

        var lblName = new Label
        {
            Text = "Tên khoa/phòng *",
            Location = new Point(24, 20),
            AutoSize = true,
            ForeColor = Color.FromArgb(23, 38, 59),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
        };

        _txtName = new TextBox
        {
            Location = new Point(24, 46),
            Size = new Size(396, 28),
            Text = name ?? string.Empty
        };

        var lblDescription = new Label
        {
            Text = "Mô tả",
            Location = new Point(24, 88),
            AutoSize = true,
            ForeColor = Color.FromArgb(23, 38, 59),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
        };

        _txtDescription = new TextBox
        {
            Location = new Point(24, 114),
            Size = new Size(396, 75),
            Multiline = true,
            Text = description ?? string.Empty
        };

        var buttonPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            BackColor = Color.FromArgb(245, 247, 250)
        };

        _btnSave = new Button
        {
            Text = "Lưu",
            DialogResult = DialogResult.OK,
            Size = new Size(95, 34),
            Location = new Point(220, 13),
            BackColor = Color.FromArgb(22, 102, 177),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        _btnSave.FlatAppearance.BorderSize = 0;
        _btnSave.Click += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khoa/phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                _txtName.Focus();
            }
        };

        _btnCancel = new Button
        {
            Text = "Hủy",
            DialogResult = DialogResult.Cancel,
            Size = new Size(95, 34),
            Location = new Point(325, 13),
            BackColor = Color.FromArgb(234, 242, 255),
            ForeColor = Color.FromArgb(16, 59, 128),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        _btnCancel.FlatAppearance.BorderSize = 0;

        buttonPanel.Controls.Add(_btnSave);
        buttonPanel.Controls.Add(_btnCancel);

        Controls.Add(lblName);
        Controls.Add(_txtName);
        Controls.Add(lblDescription);
        Controls.Add(_txtDescription);
        Controls.Add(buttonPanel);

        AcceptButton = _btnSave;
        CancelButton = _btnCancel;
    }
}
