using System.ComponentModel;
using System.Globalization;
using HIS.BLL;
using HIS.BLL.DTOs;
using HIS.Domain;

namespace HIS.WinForms.Pages;

public partial class DashboardPage : UserControl
{
    private static readonly string[] BucketLabels = { "7:00", "9:00", "11:00", "13:00", "15:00", "17:00", "19:00", "21:00" };

    private readonly IDashboardService? _dashboardService;

    public event Action<string>? NavigateRequested;

    public DashboardPage() : this(null)
    {
    }

    public DashboardPage(IDashboardService? dashboardService)
    {
        _dashboardService = dashboardService;

        InitializeComponent();
        WireActions();

        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        {
            FillSampleRows();
        }

        Load += async (_, _) =>
        {
            ApplyDateTime();
            await LoadDashboardAsync();
        };
    }

    private void WireActions()
    {
        btnPeriod.Click += (_, _) => TogglePeriod();

        btnQuickReception.Click += (_, _) => NavigateRequested?.Invoke("reception");
        btnQuickPatients.Click += (_, _) => NavigateRequested?.Invoke("patients");
        btnQuickPharmacy.Click += (_, _) => NavigateRequested?.Invoke("pharmacy");
        btnQuickReports.Click += (_, _) => NavigateRequested?.Invoke("reports");

        lnkChartReport.Click += (_, _) => NavigateRequested?.Invoke("reports");
        lnkPriorityViewAll.Click += (_, _) => NavigateRequested?.Invoke("reception");
        lnkRoomsRefresh.Click += async (_, _) => await LoadDashboardAsync();

        gridPriority.CellFormatting += GridPriority_CellFormatting;
    }

    private void ApplyDateTime()
    {
        DateTime now = DateTime.Now;
        CultureInfo culture = CultureInfo.GetCultureInfo("vi-VN");
        string day = culture.TextInfo.ToTitleCase(now.ToString("dddd", culture));
        lblSubtitle.Text = $"Tổng quan hoạt động bệnh viện hôm nay, {day} {now:dd/MM/yyyy}";
    }

    private void TogglePeriod()
    {
        btnPeriod.Text = btnPeriod.Text.StartsWith("Hôm nay") ? "Tuần này   ▾" : "Hôm nay   ▾";
    }

    private async Task LoadDashboardAsync()
    {
        if (_dashboardService == null)
        {
            return;
        }

        try
        {
            DashboardOverviewDto overview = await _dashboardService.GetOverviewAsync();
            IReadOnlyList<DashboardScheduleItemDto> schedule = await _dashboardService.GetTodayScheduleAsync();
            IReadOnlyList<DashboardRoomStatusDto> rooms = await _dashboardService.GetRoomStatusAsync();

            lblPatientsValue.Text = overview.TodayPatients.ToString("N0");
            lblWaitingValue.Text = overview.WaitingPatients.ToString("N0");
            lblInpatientValue.Text = overview.TotalPatients.ToString("N0");
            lblRecordsValue.Text = overview.CompletedToday.ToString("N0");

            FillPriorityRows(schedule);
            FillRoomRows(rooms);
            UpdateChart(schedule, overview);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"Không thể tải dữ liệu bàn làm việc từ cơ sở dữ liệu.\n\nChi tiết: {ex.Message}",
                "Lỗi kết nối dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void FillPriorityRows(IReadOnlyList<DashboardScheduleItemDto> schedule)
    {
        gridPriority.Rows.Clear();

        IEnumerable<DashboardScheduleItemDto> waiting = schedule
            .Where(i => i.StatusCode == AppointmentStatus.Codes.Pending
                        || i.StatusCode == AppointmentStatus.Codes.Confirmed)
            .Take(5);

        foreach (DashboardScheduleItemDto item in waiting)
        {
            gridPriority.Rows.Add(item.AppointmentCode, item.PatientName, item.DepartmentName, item.StatusName);
        }

        gridPriority.ClearSelection();
    }

    private void FillRoomRows(IReadOnlyList<DashboardRoomStatusDto> rooms)
    {
        (Label Code, Label Info, Label Count, Label Status)[] rows =
        {
            (lblRoomCode1, lblRoomInfo1, lblRoomCount1, lblRoomStatus1),
            (lblRoomCode2, lblRoomInfo2, lblRoomCount2, lblRoomStatus2),
            (lblRoomCode3, lblRoomInfo3, lblRoomCount3, lblRoomStatus3),
            (lblRoomCode4, lblRoomInfo4, lblRoomCount4, lblRoomStatus4)
        };

        for (int i = 0; i < rows.Length; i++)
        {
            if (i < rooms.Count)
            {
                DashboardRoomStatusDto room = rooms[i];
                bool full = room.Capacity > 0 && room.BookedCount >= room.Capacity;
                bool active = room.BookedCount > 0;

                rows[i].Code.Text = room.RoomNumber;
                rows[i].Info.Text = $"{room.Specialty}\r\nBS. {room.DoctorName}";
                rows[i].Count.Text = $"{room.BookedCount} / {room.Capacity}";
                rows[i].Status.Text = full ? "Đầy" : active ? "Đang khám" : "Trống";
                rows[i].Status.BackColor = full
                    ? Color.FromArgb(253, 236, 236)
                    : active ? Color.FromArgb(231, 242, 252) : Color.FromArgb(236, 245, 236);
                rows[i].Status.ForeColor = full
                    ? Color.FromArgb(198, 60, 60)
                    : active ? Color.FromArgb(18, 96, 158) : Color.FromArgb(46, 125, 50);
            }
            else
            {
                rows[i].Code.Text = string.Empty;
                rows[i].Info.Text = string.Empty;
                rows[i].Count.Text = string.Empty;
                rows[i].Status.Text = string.Empty;
                rows[i].Status.BackColor = Color.Transparent;
            }
        }

        lblRoomsSubtitle.Text = rooms.Count == 0
            ? "Chưa có lịch khám hôm nay"
            : $"Cập nhật gần nhất {DateTime.Now:HH:mm}";
    }

    private void UpdateChart(IReadOnlyList<DashboardScheduleItemDto> schedule, DashboardOverviewDto overview)    {
        int[] buckets = new int[BucketLabels.Length];

        foreach (DashboardScheduleItemDto item in schedule)
        {
            if (item.StatusCode == AppointmentStatus.Codes.Cancelled)
            {
                continue;
            }

            int index = Math.Clamp((item.StartTime.Hour - 7) / 2, 0, buckets.Length - 1);
            buckets[index]++;
        }

        barChart.Labels = BucketLabels;
        barChart.Values = buckets;
        barChart.Maximum = Math.Max(5, buckets.Max());

        lblLegendDone.Text = $"Đã khám: {overview.CompletedToday}";
        lblLegendReceived.Text = $"Đã tiếp nhận: {overview.TodayPatients}";

        int peak = Array.IndexOf(buckets, buckets.Max());
        lblLegendPeak.Text = buckets.Max() == 0
            ? "Chưa có dữ liệu hôm nay"
            : $"Cao điểm {7 + peak * 2:00}:00 – {9 + peak * 2:00}:00";
    }

    private void FillSampleRows()
    {
        gridPriority.Rows.Add("BN25060182", "Nguyễn Minh Anh", "Tim mạch", "Đang chờ khám");
        gridPriority.Rows.Add("BN25060181", "Trần Thu Hà", "Nội tổng quát", "Đang khám");
        gridPriority.Rows.Add("BN25060179", "Lê Hoàng Nam", "Cơ xương khớp", "Đã hoàn tất");
        gridPriority.ClearSelection();
    }

    private void GridPriority_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.ColumnIndex != colPriorityStatus.Index || e.Value is not string status)
        {
            return;
        }

        if (status.Contains("Chờ") || status.Contains("xác nhận"))
        {
            e.CellStyle.ForeColor = Theme.Amber;
        }
        else if (status.Contains("Đang khám"))
        {
            e.CellStyle.ForeColor = Theme.Blue;
        }
        else
        {
            e.CellStyle.ForeColor = Theme.Green;
        }
    }
}
