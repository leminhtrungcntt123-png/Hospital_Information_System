using System.Globalization;

namespace HIS.WinForms.Pages;

public partial class DashboardPage : UserControl
{
    public event Action<string>? NavigateRequested;

    public DashboardPage()
    {
        InitializeComponent();
        WireActions();
        FillSampleRows();
        Load += (_, _) => ApplyDateTime();
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
        lnkRoomsRefresh.Click += (_, _) => lblRoomsSubtitle.Text = $"Cập nhật gần nhất {DateTime.Now:HH:mm}";

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

        if (status.Contains("Chờ"))
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
