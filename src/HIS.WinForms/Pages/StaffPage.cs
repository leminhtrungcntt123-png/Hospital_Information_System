using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using HIS.WinForms.Dialogs;
using System.Drawing;
using System.Windows.Forms;

namespace HIS.WinForms.Pages;

public partial class StaffPage : UserControl
{
    private readonly IStaffService _staffService = null!;

    private readonly Color _muted = Color.FromArgb(102, 117, 138);
    private readonly Color _error = Color.FromArgb(190, 40, 40);

    public StaffPage() : this(null!) { }

    public StaffPage(IStaffService staffService)
    {
        _staffService = staffService;
        InitializeComponent();
    }

    private async void StaffPage_Load(object? sender, EventArgs e)
    {
        if (!DesignMode && _staffService != null)
        {
            await LoadStaffAsync();
        }
    }

    private async void AddButton_Click(object? sender, EventArgs e) => await OnAddStaffAsync();
    private async void EditButton_Click(object? sender, EventArgs e) => await OnEditStaffAsync();
    private async void DeleteButton_Click(object? sender, EventArgs e) => await OnDeleteStaffAsync();
    private async void RefreshButton_Click(object? sender, EventArgs e) => await LoadStaffAsync();

    private StaffListItemDto? GetSelectedStaff()
    {
        if (_staffGrid.CurrentRow?.DataBoundItem is StaffListItemDto staff)
            return staff;
        return null;
    }

    private async Task OnAddStaffAsync()
    {
        try
        {
            var roles = await _staffService.GetRolesAsync();
            using var dialog = new StaffEditDialog(roles);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await _staffService.CreateStaffAsync(
                    dialog.StaffUsername,
                    dialog.StaffPassword,
                    dialog.StaffFullName,
                    dialog.SelectedRoleId,
                    dialog.IsStaffActive);

                _statusLabel.Text = $"Đã thêm tài khoản '{dialog.StaffUsername}' thành công.";
                _statusLabel.ForeColor = Color.FromArgb(22, 128, 92);
                await LoadStaffAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi thêm nhân sự", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task OnEditStaffAsync()
    {
        var selected = GetSelectedStaff();
        if (selected == null)
        {
            MessageBox.Show("Vui lòng chọn một tài khoản để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            var roles = await _staffService.GetRolesAsync();
            using var dialog = new StaffEditDialog(roles, selected);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await _staffService.UpdateStaffAsync(
                    selected.Id,
                    dialog.StaffPassword,
                    dialog.StaffFullName,
                    dialog.SelectedRoleId,
                    dialog.IsStaffActive);

                _statusLabel.Text = $"Đã cập nhật tài khoản '{selected.Username}' thành công.";
                _statusLabel.ForeColor = Color.FromArgb(22, 128, 92);
                await LoadStaffAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task OnDeleteStaffAsync()
    {
        var selected = GetSelectedStaff();
        if (selected == null)
        {
            MessageBox.Show("Vui lòng chọn một tài khoản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var confirm = MessageBox.Show(
            $"Bạn có chắc chắn muốn xóa tài khoản '{selected.Username}' ({selected.FullName}) không?",
            "Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm == DialogResult.Yes)
        {
            try
            {
                await _staffService.DeleteStaffAsync(selected.Id);
                _statusLabel.Text = $"Đã xóa tài khoản '{selected.Username}' thành công.";
                _statusLabel.ForeColor = Color.FromArgb(22, 128, 92);
                await LoadStaffAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Không thể xóa nhân sự", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    private async Task LoadStaffAsync()
    {
        SetButtonsEnabled(false);
        Cursor = Cursors.WaitCursor;
        _statusLabel.ForeColor = _muted;
        _statusLabel.Text = "Đang tải dữ liệu...";

        try
        {
            var staff = await _staffService.GetStaffAsync();
            _staffGrid.DataSource = staff;
            _statusLabel.Text = $"Đã tải {staff.Count} tài khoản lúc {DateTime.Now:HH:mm:ss}.";
        }
        catch (Exception ex)
        {
            _statusLabel.Text = $"Lỗi tải danh sách nhân sự: {ex.Message}";
            _statusLabel.ForeColor = _error;

            MessageBox.Show(
                $"Không thể tải danh sách nhân sự từ cơ sở dữ liệu.\n\nChi tiết: {ex.Message}",
                "Lỗi kết nối dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            SetButtonsEnabled(true);
            Cursor = Cursors.Default;
        }
    }

    private void SetButtonsEnabled(bool enabled)
    {
        _addButton.Enabled = enabled;
        _editButton.Enabled = enabled;
        _deleteButton.Enabled = enabled;
        _refreshButton.Enabled = enabled;
    }
}
