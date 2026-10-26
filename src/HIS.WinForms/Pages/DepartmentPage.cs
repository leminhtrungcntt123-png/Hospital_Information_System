using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using HIS.WinForms.Dialogs;
using System.Drawing;
using System.Windows.Forms;

namespace HIS.WinForms.Pages;

public partial class DepartmentPage : UserControl
{
    private readonly IDepartmentService _departmentService = null!;

    private readonly Color _muted = Color.FromArgb(102, 117, 138);
    private readonly Color _error = Color.FromArgb(190, 40, 40);

    public DepartmentPage() : this(null!) { }

    public DepartmentPage(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
        InitializeComponent();
    }

    private async void DepartmentPage_Load(object? sender, EventArgs e)
    {
        if (!DesignMode && _departmentService != null)
        {
            await LoadDepartmentsAsync();
        }
    }

    private async void AddButton_Click(object? sender, EventArgs e) => await OnAddDepartmentAsync();
    private async void EditButton_Click(object? sender, EventArgs e) => await OnEditDepartmentAsync();
    private async void DeleteButton_Click(object? sender, EventArgs e) => await OnDeleteDepartmentAsync();
    private async void RefreshButton_Click(object? sender, EventArgs e) => await LoadDepartmentsAsync();

    private DepartmentListItemDto? GetSelectedDepartment()
    {
        if (_departmentsGrid.CurrentRow?.DataBoundItem is DepartmentListItemDto dept)
            return dept;
        return null;
    }

    private async Task OnAddDepartmentAsync()
    {
        using var dialog = new DepartmentEditDialog();
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                await _departmentService.CreateDepartmentAsync(dialog.DepartmentName, dialog.Description);
                _statusLabel.Text = $"Đã thêm khoa/phòng '{dialog.DepartmentName}' thành công.";
                _statusLabel.ForeColor = Color.FromArgb(22, 128, 92);
                await LoadDepartmentsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi thêm khoa/phòng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async Task OnEditDepartmentAsync()
    {
        var selected = GetSelectedDepartment();
        if (selected == null)
        {
            MessageBox.Show("Vui lòng chọn một khoa/phòng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new DepartmentEditDialog(selected.Name, selected.Description, isEdit: true);
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                await _departmentService.UpdateDepartmentAsync(selected.Id, dialog.DepartmentName, dialog.Description);
                _statusLabel.Text = $"Đã cập nhật khoa/phòng '{dialog.DepartmentName}' thành công.";
                _statusLabel.ForeColor = Color.FromArgb(22, 128, 92);
                await LoadDepartmentsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async Task OnDeleteDepartmentAsync()
    {
        var selected = GetSelectedDepartment();
        if (selected == null)
        {
            MessageBox.Show("Vui lòng chọn một khoa/phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var confirm = MessageBox.Show(
            $"Bạn có chắc chắn muốn xóa khoa/phòng '{selected.Name}' (Mã: {selected.Id}) không?",
            "Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm == DialogResult.Yes)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(selected.Id);
                _statusLabel.Text = $"Đã xóa khoa/phòng '{selected.Name}' thành công.";
                _statusLabel.ForeColor = Color.FromArgb(22, 128, 92);
                await LoadDepartmentsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Không thể xóa khoa/phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    private async Task LoadDepartmentsAsync()
    {
        SetButtonsEnabled(false);
        Cursor = Cursors.WaitCursor;
        _statusLabel.ForeColor = _muted;
        _statusLabel.Text = "Đang tải dữ liệu...";

        try
        {
            var departments = await _departmentService.GetDepartmentsAsync();
            _departmentsGrid.DataSource = departments;
            _statusLabel.Text = $"Đã tải {departments.Count} khoa/phòng lúc {DateTime.Now:HH:mm:ss}.";
        }
        catch (Exception ex)
        {
            _statusLabel.Text = $"Lỗi tải danh sách khoa/phòng: {ex.Message}";
            _statusLabel.ForeColor = _error;

            MessageBox.Show(
                $"Không thể tải danh sách khoa/phòng từ cơ sở dữ liệu.\n\nChi tiết: {ex.Message}",
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
