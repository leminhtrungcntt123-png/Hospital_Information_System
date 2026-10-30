using System.Drawing;
using System.Windows.Forms;
using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;

namespace HIS.WinForms.Pages;

public partial class ScheduleSlotPage : UserControl
{
    private readonly ISchedulingService _schedulingService = null!;
    private readonly IDepartmentService? _departmentService;

    public ScheduleSlotPage() : this(null!, null) { }

    public ScheduleSlotPage(ISchedulingService schedulingService, IDepartmentService? departmentService = null)
    {
        _schedulingService = schedulingService;
        _departmentService = departmentService;

        InitializeComponent();
        WireEvents();
    }

    private void WireEvents()
    {
        Load += ScheduleSlotPage_Load;

        _btnGenerate.Click += async (s, e) => await PerformGenerateSlotsAsync();
        _btnSearch.Click += async (s, e) => await PerformSearchAsync();
        _btnRefresh.Click += async (s, e) => await PerformSearchAsync();

        _dgvSlots.AutoGenerateColumns = false;
        _dgvSlots.CellFormatting += DgvSlots_CellFormatting;
    }

    private async void ScheduleSlotPage_Load(object? sender, EventArgs e)
    {
        if (!DesignMode)
        {
            await LoadDepartmentsAsync();
            await PerformSearchAsync();
        }
    }

    private void DgvSlots_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

        var col = _dgvSlots.Columns[e.ColumnIndex];
        if (col.DataPropertyName == "Status")
        {
            var val = e.Value?.ToString() ?? "";
            if (val == "AVAILABLE")
            {
                e.Value = "Còn trống";
                e.CellStyle.ForeColor = Theme.Green;
                e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
            else if (val == "FULL")
            {
                e.Value = "Đã kín chỗ";
                e.CellStyle.ForeColor = Theme.Muted;
            }
            else if (val == "BLOCKED")
            {
                e.Value = "Đã khóa";
                e.CellStyle.ForeColor = Theme.Red;
            }
        }
        else if (col.DataPropertyName == "AvailableCount")
        {
            if (e.Value is int count && count > 0)
            {
                e.CellStyle.ForeColor = Theme.Green;
                e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
        }
    }

    private async Task LoadDepartmentsAsync()
    {
        if (_departmentService == null) return;

        try
        {
            var depts = await _departmentService.GetDepartmentsAsync();
            var listGen = depts.ToList();
            var listSearch = depts.ToList();

            _cboGenDept.Items.Clear();
            _cboGenDept.Items.Add(new DepartmentComboBoxItem(0, "-- Tất cả chuyên khoa --"));
            foreach (var d in listGen)
            {
                _cboGenDept.Items.Add(new DepartmentComboBoxItem(d.Id, d.Name));
            }
            _cboGenDept.SelectedIndex = 0;

            _cboSearchDept.Items.Clear();
            _cboSearchDept.Items.Add(new DepartmentComboBoxItem(0, "-- Tất cả chuyên khoa --"));
            foreach (var d in listSearch)
            {
                _cboSearchDept.Items.Add(new DepartmentComboBoxItem(d.Id, d.Name));
            }
            _cboSearchDept.SelectedIndex = 0;
        }
        catch
        {
        }
    }

    private async Task PerformGenerateSlotsAsync()
    {
        if (_schedulingService == null) return;

        var from = DateOnly.FromDateTime(_dtpFromGen.Value);
        var to = DateOnly.FromDateTime(_dtpToGen.Value);

        if (from > to)
        {
            MessageBox.Show(this, "Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int? deptId = null;
        if (_cboGenDept.SelectedItem is DepartmentComboBoxItem item && item.Id > 0)
        {
            deptId = item.Id;
        }

        _btnGenerate.Enabled = false;
        _lblGenResult.Text = "Đang xử lý sinh slot...";
        _lblGenResult.ForeColor = Theme.Navy;

        try
        {
            var request = new GenerateSlotsRequestDto
            {
                FromDate = from,
                ToDate = to,
                DepartmentId = deptId
            };

            var result = await _schedulingService.GenerateSlotsAsync(request);
            if (result.IsSuccess)
            {
                _lblGenResult.Text = $"✔ {result.Message}";
                _lblGenResult.ForeColor = Theme.Green;

                _dtpSearchDate.Value = _dtpFromGen.Value;
                await PerformSearchAsync();
            }
            else
            {
                _lblGenResult.Text = $"✖ {result.Message}";
                _lblGenResult.ForeColor = Theme.Red;
            }
        }
        catch (Exception ex)
        {
            _lblGenResult.Text = "Lỗi khi sinh slot.";
            _lblGenResult.ForeColor = Theme.Red;
            MessageBox.Show(this, ex.Message, "Lỗi sinh slot", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _btnGenerate.Enabled = true;
        }
    }

    private async Task PerformSearchAsync()
    {
        if (_schedulingService == null) return;

        var date = DateOnly.FromDateTime(_dtpSearchDate.Value);
        int? deptId = null;
        if (_cboSearchDept.SelectedItem is DepartmentComboBoxItem item && item.Id > 0)
        {
            deptId = item.Id;
        }

        string? shift = null;
        if (_cboSearchShift.SelectedIndex == 1) shift = "MORNING";
        else if (_cboSearchShift.SelectedIndex == 2) shift = "AFTERNOON";
        else if (_cboSearchShift.SelectedIndex == 3) shift = "EVENING";

        _btnSearch.Enabled = false;

        try
        {
            List<AppointmentSlotDto> list;

            if (_chkOnlyAvailable.Checked)
            {
                list = await _schedulingService.GetAvailableSlotsAsync(new SlotSearchFilterDto
                {
                    Date = date,
                    DepartmentId = deptId,
                    Shift = shift
                });
            }
            else if (deptId.HasValue)
            {
                list = await _schedulingService.GetSlotsByDepartmentAndDateAsync(deptId.Value, date);
            }
            else
            {
                list = await _schedulingService.GetAvailableSlotsAsync(new SlotSearchFilterDto
                {
                    Date = date,
                    Shift = shift
                });
            }

            _dgvSlots.DataSource = list;

            int total = list.Count;
            int available = list.Count(s => s.IsAvailable);
            int booked = list.Sum(s => s.BookedCount);

            _lblGridSummary.Text = $"Ngày {date:dd/MM/yyyy}: Tìm thấy {total:N0} khung giờ | Còn trống: {available:N0} slot | Đã đặt: {booked:N0} lượt";
        }
        catch (Exception ex)
        {
            _lblGridSummary.Text = "Lỗi khi tải danh sách slot: " + ex.Message;
        }
        finally
        {
            _btnSearch.Enabled = true;
        }
    }

    private void grpGenerate_Enter(object sender, EventArgs e)
    {

    }

    private record DepartmentComboBoxItem(int Id, string Name)
    {
        public override string ToString() => Name;
    }
}
