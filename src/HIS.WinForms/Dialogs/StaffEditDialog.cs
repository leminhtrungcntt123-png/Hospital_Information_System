using HIS.BLL.DTOs;
using System.Drawing;
using System.Windows.Forms;

namespace HIS.WinForms.Dialogs;

public class StaffEditDialog : Form
{
    private readonly TextBox _txtUsername;
    private readonly TextBox _txtPassword;
    private readonly TextBox _txtFullName;
    private readonly ComboBox _cboRole;
    private readonly CheckBox _chkActive;
    private readonly Button _btnSave;
    private readonly Button _btnCancel;
    private readonly bool _isEdit;

    public string StaffUsername => _txtUsername.Text.Trim();
    public string StaffPassword => _txtPassword.Text;
    public string StaffFullName => _txtFullName.Text.Trim();
    public int SelectedRoleId => (int)_cboRole.SelectedValue!;
    public bool IsStaffActive => _chkActive.Checked;

    public StaffEditDialog(
        IReadOnlyList<RoleOptionDto> roles,
        StaffListItemDto? staff = null)
    {
        _isEdit = staff != null;
        Text = _isEdit ? "Chỉnh sửa nhân sự" : "Thêm nhân sự mới";
        Size = new Size(480, 410);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        BackColor = Color.White;
        Font = new Font("Segoe UI", 9.5F);

        int y = 18;

        // Tên đăng nhập
        var lblUsername = new Label
        {
            Text = "Tên đăng nhập *",
            Location = new Point(24, y),
            AutoSize = true,
            ForeColor = Color.FromArgb(23, 38, 59),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
        };
        y += 24;

        _txtUsername = new TextBox
        {
            Location = new Point(24, y),
            Size = new Size(415, 28),
            Text = staff?.Username ?? string.Empty,
            ReadOnly = _isEdit
        };
        if (_isEdit)
        {
            _txtUsername.BackColor = Color.FromArgb(245, 247, 250);
        }
        y += 38;

        // Mật khẩu
        var lblPassword = new Label
        {
            Text = _isEdit ? "Mật khẩu mới (bỏ trống nếu không đổi)" : "Mật khẩu *",
            Location = new Point(24, y),
            AutoSize = true,
            ForeColor = Color.FromArgb(23, 38, 59),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
        };
        y += 24;

        _txtPassword = new TextBox
        {
            Location = new Point(24, y),
            Size = new Size(415, 28),
            UseSystemPasswordChar = true
        };
        y += 38;

        // Họ và tên
        var lblFullName = new Label
        {
            Text = "Họ và tên *",
            Location = new Point(24, y),
            AutoSize = true,
            ForeColor = Color.FromArgb(23, 38, 59),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
        };
        y += 24;

        _txtFullName = new TextBox
        {
            Location = new Point(24, y),
            Size = new Size(415, 28),
            Text = staff?.FullName ?? string.Empty
        };
        y += 38;

        // Vai trò
        var lblRole = new Label
        {
            Text = "Vai trò *",
            Location = new Point(24, y),
            AutoSize = true,
            ForeColor = Color.FromArgb(23, 38, 59),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
        };
        y += 24;

        _cboRole = new ComboBox
        {
            Location = new Point(24, y),
            Size = new Size(415, 28),
            DropDownStyle = ComboBoxStyle.DropDownList,
            DisplayMember = nameof(RoleOptionDto.Name),
            ValueMember = nameof(RoleOptionDto.Id),
            DataSource = roles.ToList()
        };

        if (staff != null)
        {
            var matchRole = roles.FirstOrDefault(r => r.Code == staff.RoleCode);
            if (matchRole != null)
                _cboRole.SelectedValue = matchRole.Id;
        }
        y += 38;

        // Trạng thái hoạt động
        _chkActive = new CheckBox
        {
            Text = "Tài khoản đang hoạt động",
            Location = new Point(24, y),
            AutoSize = true,
            Checked = staff?.IsActive ?? true
        };

        // Panel nút
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
            Location = new Point(240, 13),
            BackColor = Color.FromArgb(22, 102, 177),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        _btnSave.FlatAppearance.BorderSize = 0;
        _btnSave.Click += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(_txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                _txtUsername.Focus();
                return;
            }

            if (!_isEdit && string.IsNullOrWhiteSpace(_txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cho tài khoản mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                _txtPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(_txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                _txtFullName.Focus();
                return;
            }

            if (_cboRole.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vai trò cho tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                _cboRole.Focus();
                return;
            }
        };

        _btnCancel = new Button
        {
            Text = "Hủy",
            DialogResult = DialogResult.Cancel,
            Size = new Size(95, 34),
            Location = new Point(345, 13),
            BackColor = Color.FromArgb(234, 242, 255),
            ForeColor = Color.FromArgb(16, 59, 128),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        _btnCancel.FlatAppearance.BorderSize = 0;

        buttonPanel.Controls.Add(_btnSave);
        buttonPanel.Controls.Add(_btnCancel);

        Controls.Add(lblUsername);
        Controls.Add(_txtUsername);
        Controls.Add(lblPassword);
        Controls.Add(_txtPassword);
        Controls.Add(lblFullName);
        Controls.Add(_txtFullName);
        Controls.Add(lblRole);
        Controls.Add(_cboRole);
        Controls.Add(_chkActive);
        Controls.Add(buttonPanel);

        AcceptButton = _btnSave;
        CancelButton = _btnCancel;
    }
}
