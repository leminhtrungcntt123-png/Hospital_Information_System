using HIS.BLL;
using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using HIS.WinForms.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace HIS.WinForms.Pages;

public class AdminPage : UserControl
{
    private readonly IAdminService _adminService;

    private readonly Color _blue = Color.FromArgb(23, 78, 166);
    private readonly Color _blueDark = Color.FromArgb(16, 59, 128);
    private readonly Color _background = Color.FromArgb(245, 247, 250);
    private readonly Color _muted = Color.FromArgb(102, 117, 138);
    private readonly Color _text = Color.FromArgb(23, 38, 59);
    private readonly Color _error = Color.FromArgb(190, 40, 40);

    private StatCard _patientsCard = null!;
    private StatCard _doctorsCard = null!;
    private StatCard _todayCard = null!;
    private StatCard _departmentsCard = null!;

    private DataGridView _patientsGrid = null!;
    private DataGridView _doctorsGrid = null!;
    private DataGridView _appointmentsGrid = null!;

    private TextBox _searchBox = null!;
    private Label _statusLabel = null!;
    private Button _searchButton = null!;
    private Button _refreshButton = null!;

    public AdminPage(IAdminService adminService)
    {
        _adminService = adminService;

        BuildLayout();
        Load += async (_, _) => await LoadAllAsync();
    }

    private void BuildLayout()
    {
        Dock = DockStyle.Fill;
        BackColor = _background;
        Padding = new Padding(28);

        // ===== Header =====
        var headerPanel = new Panel { Dock = DockStyle.Top, Height = 72, BackColor = _background };

        var title = new Label
        {
            Text = "Quản lý hệ thống",
            AutoSize = true,
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = _text,
            Location = new Point(0, 4)
        };

        var subtitle = new Label
        {
            Text = "Tổng quan dữ liệu và danh sách bệnh nhân, bác sĩ, lịch hẹn của bệnh viện.",
            AutoSize = true,
            Font = new Font("Segoe UI", 10F),
            ForeColor = _muted,
            Location = new Point(2, 44)
        };

        headerPanel.Controls.Add(title);
        headerPanel.Controls.Add(subtitle);

        // ===== Stats row =====
        var statsPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 118,
            BackColor = _background,
            ColumnCount = 4
        };
        for (int i = 0; i < 4; i++)
        {
            statsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        }
        statsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        _patientsCard = new StatCard("Bệnh nhân", _blue) { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
        _doctorsCard = new StatCard("Bác sĩ", Color.FromArgb(46, 125, 50)) { Dock = DockStyle.Fill, Margin = new Padding(6, 0, 6, 0) };
        _departmentsCard = new StatCard("Khoa", Color.FromArgb(245, 124, 0)) { Dock = DockStyle.Fill, Margin = new Padding(6, 0, 6, 0) };
        _todayCard = new StatCard("Lịch hẹn hôm nay", Color.FromArgb(106, 27, 154)) { Dock = DockStyle.Fill, Margin = new Padding(12, 0, 0, 0) };

        statsPanel.Controls.Add(_patientsCard, 0, 0);
        statsPanel.Controls.Add(_doctorsCard, 1, 0);
        statsPanel.Controls.Add(_departmentsCard, 2, 0);
        statsPanel.Controls.Add(_todayCard, 3, 0);

        // ===== Tabs =====
        var tabControl = new TabControl
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F)
        };

        tabControl.TabPages.Add(BuildPatientsTab());
        tabControl.TabPages.Add(BuildDoctorsTab());
        tabControl.TabPages.Add(BuildAppointmentsTab());

        // ===== Status bar =====
        var statusPanel = new Panel { Dock = DockStyle.Bottom, Height = 30, BackColor = _background };

        _statusLabel = new Label
        {
            Text = "Sẵn sàng.",
            AutoSize = true,
            Font = new Font("Segoe UI", 9F),
            ForeColor = _muted,
            Location = new Point(0, 6)
        };

        statusPanel.Controls.Add(_statusLabel);

        // Thứ tự add: Fill trước, Bottom, rồi các Top cuối (dock xếp từ cuối danh sách)
        Controls.Add(tabControl);
        Controls.Add(statusPanel);
        Controls.Add(statsPanel);
        Controls.Add(headerPanel);
    }

    private TabPage BuildPatientsTab()
    {
        var tab = new TabPage("Bệnh nhân");
        tab.BackColor = Color.White;

        var toolbar = new Panel { Dock = DockStyle.Top, Height = 56, BackColor = Color.White, Padding = new Padding(0, 10, 0, 10) };

        _searchBox = new TextBox
        {
            Location = new Point(0, 12),
            Size = new Size(280, 30),
            Font = new Font("Segoe UI", 10F),
            ForeColor = _text
        };
        _searchBox.PlaceholderText = "Tìm theo tên, SĐT, CCCD/CMND...";

        _searchButton = CreateButton("Tìm kiếm", _blue, Color.White, new Point(292, 10), true);
        _searchButton.Click += async (_, _) => await SafeLoadAsync("danh sách bệnh nhân", async () =>
        {
            var patients = await _adminService.GetPatientsAsync(_searchBox.Text);
            _patientsGrid.DataSource = patients;
        });

        _refreshButton = CreateButton("Làm mới", Color.FromArgb(234, 242, 255), _blueDark, new Point(404, 10), false);
        _refreshButton.Click += async (_, _) => await LoadAllAsync();

        toolbar.Controls.Add(_searchBox);
        toolbar.Controls.Add(_searchButton);
        toolbar.Controls.Add(_refreshButton);

        _patientsGrid = CreateGrid();
        _patientsGrid.Columns.AddRange(new DataGridViewColumn[]
        {
            TextColumn(nameof(PatientListItemDto.Id), "Mã BN", 8),
            TextColumn(nameof(PatientListItemDto.FullName), "Họ và tên", 22),
            TextColumn(nameof(PatientListItemDto.DateOfBirth), "Ngày sinh", 12, "dd/MM/yyyy"),
            TextColumn(nameof(PatientListItemDto.Gender), "Giới tính", 10),
            TextColumn(nameof(PatientListItemDto.PhoneNumber), "Số điện thoại", 14),
            TextColumn(nameof(PatientListItemDto.NationalId), "CCCD/CMND", 14),
            TextColumn(nameof(PatientListItemDto.Address), "Địa chỉ", 24),
            TextColumn(nameof(PatientListItemDto.InsuranceCardNumber), "Số BHYT", 14)
        });

        tab.Controls.Add(_patientsGrid);
        tab.Controls.Add(toolbar);
        return tab;
    }

    private TabPage BuildDoctorsTab()
    {
        var tab = new TabPage("Bác sĩ");
        tab.BackColor = Color.White;

        _doctorsGrid = CreateGrid();
        _doctorsGrid.Columns.AddRange(new DataGridViewColumn[]
        {
            TextColumn(nameof(DoctorListItemDto.Id), "Mã BS", 10),
            TextColumn(nameof(DoctorListItemDto.FullName), "Họ và tên", 30),
            TextColumn(nameof(DoctorListItemDto.Specialty), "Chuyên môn", 25),
            TextColumn(nameof(DoctorListItemDto.DepartmentName), "Khoa", 25),
            TextColumn(nameof(DoctorListItemDto.LicenseNumber), "Số giấy phép", 18)
        });

        tab.Controls.Add(_doctorsGrid);
        return tab;
    }

    private TabPage BuildAppointmentsTab()
    {
        var tab = new TabPage("Lịch hẹn hôm nay");
        tab.BackColor = Color.White;

        _appointmentsGrid = CreateGrid();
        _appointmentsGrid.Columns.AddRange(new DataGridViewColumn[]
        {
            TextColumn(nameof(TodayAppointmentItemDto.AppointmentCode), "Mã lịch hẹn", 18),
            TextColumn(nameof(TodayAppointmentItemDto.PatientName), "Bệnh nhân", 30),
            TextColumn(nameof(TodayAppointmentItemDto.StartTime), "Giờ khám", 14, "HH:mm"),
            TextColumn(nameof(TodayAppointmentItemDto.StatusName), "Trạng thái", 20),
            TextColumn(nameof(TodayAppointmentItemDto.BookingChannel), "Kênh đặt", 18)
        });

        tab.Controls.Add(_appointmentsGrid);
        return tab;
    }

    private async Task LoadAllAsync()
    {
        SetBusy(true);
        _statusLabel.ForeColor = _muted;

        await SafeLoadAsync("thống kê", async () =>
        {
            var stats = await _adminService.GetDashboardStatsAsync();
            _patientsCard.Value = stats.TotalPatients.ToString("N0");
            _doctorsCard.Value = stats.TotalDoctors.ToString("N0");
            _departmentsCard.Value = stats.TotalDepartments.ToString("N0");
            _todayCard.Value = stats.TodayAppointments.ToString("N0");
        });

        await SafeLoadAsync("danh sách bệnh nhân", async () =>
        {
            var patients = await _adminService.GetPatientsAsync(_searchBox?.Text);
            _patientsGrid.DataSource = patients;
        });

        await SafeLoadAsync("danh sách bác sĩ", async () =>
        {
            var doctors = await _adminService.GetDoctorsAsync();
            _doctorsGrid.DataSource = doctors;
        });

        await SafeLoadAsync("lịch hẹn hôm nay", async () =>
        {
            var appointments = await _adminService.GetTodayAppointmentsAsync();
            _appointmentsGrid.DataSource = appointments;
        });

        _statusLabel.Text = $"Đã tải dữ liệu lúc {DateTime.Now:HH:mm:ss}.";
        SetBusy(false);
    }

    private async Task SafeLoadAsync(string what, Func<Task> loadAction)
    {
        try
        {
            await loadAction();
        }
        catch (Exception ex)
        {
            _statusLabel.Text = $"Lỗi tải {what}: {ex.Message}";
            _statusLabel.ForeColor = _error;

            MessageBox.Show(
                $"Không thể tải {what} từ cơ sở dữ liệu.\n\nChi tiết: {ex.Message}",
                "Lỗi kết nối dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void SetBusy(bool busy)
    {
        if (_searchButton != null) _searchButton.Enabled = !busy;
        if (_refreshButton != null) _refreshButton.Enabled = !busy;
        Cursor = busy ? Cursors.WaitCursor : Cursors.Default;

        if (busy)
        {
            _statusLabel.Text = "Đang tải dữ liệu...";
            _statusLabel.ForeColor = _muted;
        }
    }

    private Button CreateButton(string text, Color backColor, Color foreColor, Point location, bool primary)
    {
        var button = new Button
        {
            Text = text,
            Location = location,
            Size = new Size(104, 34),
            BackColor = backColor,
            ForeColor = foreColor,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9.5F, primary ? FontStyle.Bold : FontStyle.Regular),
            Cursor = Cursors.Hand
        };
        button.FlatAppearance.BorderSize = 0;
        return button;
    }

    private static DataGridView CreateGrid()
    {
        var grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = false,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            GridColor = Color.FromArgb(229, 234, 242),
            EnableHeadersVisualStyles = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ColumnHeadersHeight = 38,
            RowTemplate = { Height = 34 },
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        };

        grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(240, 244, 250),
            ForeColor = Color.FromArgb(23, 38, 59),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
            SelectionBackColor = Color.FromArgb(240, 244, 250),
            SelectionForeColor = Color.FromArgb(23, 38, 59),
            Padding = new Padding(6, 0, 0, 0),
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            WrapMode = DataGridViewTriState.False
        };

        grid.DefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.White,
            ForeColor = Color.FromArgb(51, 65, 85),
            Font = new Font("Segoe UI", 9.5F),
            SelectionBackColor = Color.FromArgb(234, 242, 255),
            SelectionForeColor = Color.FromArgb(16, 59, 128),
            Padding = new Padding(6, 0, 0, 0)
        };

        grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(248, 250, 253),
            ForeColor = Color.FromArgb(51, 65, 85),
            SelectionBackColor = Color.FromArgb(234, 242, 255),
            SelectionForeColor = Color.FromArgb(16, 59, 128)
        };

        return grid;
    }

    private static DataGridViewTextBoxColumn TextColumn(string property, string header, int fillWeight, string? format = null)
    {
        var column = new DataGridViewTextBoxColumn
        {
            Name = property,
            DataPropertyName = property,
            HeaderText = header,
            FillWeight = fillWeight,
            ReadOnly = true,
            SortMode = DataGridViewColumnSortMode.Automatic
        };

        if (format != null)
        {
            column.DefaultCellStyle.Format = format;
        }

        return column;
    }
}
