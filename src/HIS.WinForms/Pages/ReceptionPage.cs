using System.Drawing;
using System.Text;
using HIS.BLL;
using HIS.BLL.DTOs;
using HIS.Domain;
using HIS.WinForms.Dialogs;

namespace HIS.WinForms.Pages;

public partial class ReceptionPage : UserControl
{
    private const int PageSize = 10;

    private static readonly Font TabActiveFont = new("Segoe UI", 9F, FontStyle.Bold);
    private static readonly Font TabInactiveFont = new("Segoe UI", 9F, FontStyle.Regular);
    private static readonly Font PagerFont = new("Segoe UI", 8.5F, FontStyle.Regular);
    private static readonly Font PagerActiveFont = new("Segoe UI", 8.5F, FontStyle.Bold);

    private readonly IReceptionService? _receptionService;
    private readonly SemaphoreSlim _loadLock = new(1, 1);

    private List<ReceptionItemDto> _items = new();
    private ReceptionTab _activeTab = ReceptionTab.Waiting;
    private int _page = 1;
    private int _loadVersion;

    public event Action<ReceptionCountsDto>? CountsChanged;

    public ReceptionPage() : this(null)
    {
    }

    public ReceptionPage(IReceptionService? receptionService)
    {
        _receptionService = receptionService;

        InitializeComponent();
        ApplyTabVisual();
        WireEvents();

        Load += async (_, _) => await ReloadAsync();
    }

    public void OpenNewReception()
    {
        BtnNewReception_Click(this, EventArgs.Empty);
    }

    public async Task RefreshAsync()
    {
        _page = 1;
        await ReloadAsync();
    }

    private void WireEvents()
    {
        btnTabWaiting.Click += TabButton_Click;
        btnTabReceived.Click += TabButton_Click;
        btnTabScheduled.Click += TabButton_Click;
        btnTabCancelled.Click += TabButton_Click;

        btnNewReception.Click += BtnNewReception_Click;
        btnRefresh.Click += async (_, _) =>
        {
            _page = 1;
            await ReloadAsync();
        };

        btnSearch.Click += async (_, _) =>
        {
            _page = 1;
            await ReloadAsync();
        };

        txtSearch.KeyDown += async (_, e) =>
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            _page = 1;
            await ReloadAsync();
        };

        btnExport.Click += BtnExport_Click;

        gridReceptions.CellFormatting += GridReceptions_CellFormatting;
        gridReceptions.CellContentClick += GridReceptions_CellContentClick;
        gridReceptions.CellMouseEnter += GridReceptions_CellMouseEnter;
        gridReceptions.CellMouseLeave += (_, _) => gridReceptions.Cursor = Cursors.Default;
    }

    private async void TabButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.Tag is not string tag)
        {
            return;
        }

        if (!Enum.TryParse(tag, out ReceptionTab tab) || tab == _activeTab)
        {
            return;
        }

        _activeTab = tab;
        _page = 1;
        ApplyTabVisual();
        await ReloadAsync(loadCounts: false);
    }

    private async void BtnNewReception_Click(object? sender, EventArgs e)
    {
        if (_receptionService == null)
        {
            return;
        }

        using var dialog = new NewReceptionDialog(_receptionService);

        if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        _activeTab = ReceptionTab.Received;
        _page = 1;
        ApplyTabVisual();
        await ReloadAsync();
        SelectAppointment(dialog.CreatedAppointmentId);
    }

    private async void GridReceptions_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (_receptionService == null
            || e.RowIndex < 0
            || e.ColumnIndex != colAction.Index
            || _activeTab != ReceptionTab.Waiting
            || gridReceptions.Rows[e.RowIndex].Tag is not ReceptionItemDto item)
        {
            return;
        }

        DialogResult confirm = MessageBox.Show(
            FindForm(),
            $"Tiếp nhận bệnh nhân {item.PatientName}?\nKhoa: {item.DepartmentName}\nMã lượt khám: {item.AppointmentCode}",
            "Xác nhận tiếp nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
        {
            return;
        }

        gridReceptions.Enabled = false;
        UseWaitCursor = true;

        try
        {
            ReceptionActionResult result = await _receptionService.CheckInAsync(item.AppointmentId);

            if (!result.Success)
            {
                MessageBox.Show(FindForm(), result.Message, "Không thể tiếp nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await ReloadAsync();
                return;
            }

            MessageBox.Show(
                FindForm(),
                $"{result.Message}\nSố thứ tự: {result.QueueNumber}",
                "Tiếp nhận thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _activeTab = ReceptionTab.Received;
            _page = 1;
            ApplyTabVisual();
            await ReloadAsync();
            SelectAppointment(item.AppointmentId);
        }
        catch (Exception ex)
        {
            MessageBox.Show(FindForm(), ex.Message, "Lỗi tiếp nhận", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            gridReceptions.Enabled = true;
            UseWaitCursor = false;
        }
    }

    private async Task ReloadAsync(bool loadCounts = true)
    {
        if (_receptionService == null)
        {
            return;
        }

        int version = ++_loadVersion;
        await _loadLock.WaitAsync();

        try
        {
            if (version != _loadVersion)
            {
                return;
            }

            if (loadCounts)
            {
                ReceptionCountsDto counts = await _receptionService.GetCountsAsync();
                if (version != _loadVersion)
                {
                    return;
                }

                UpdateTabCounts(counts);
                CountsChanged?.Invoke(counts);
            }

            IReadOnlyList<ReceptionItemDto> items = await _receptionService.GetReceptionListAsync(_activeTab, txtSearch.Text);
            if (version != _loadVersion)
            {
                return;
            }

            _items = items.ToList();
            _page = Math.Clamp(_page, 1, Math.Max(1, TotalPages));
            RenderPage();
        }
        catch (Exception ex)
        {
            if (version == _loadVersion)
            {
                lblListSubtitle.Text = "Không thể tải dữ liệu tiếp đón.";
                MessageBox.Show(FindForm(), ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        finally
        {
            _loadLock.Release();
        }
    }

    private int TotalPages => Math.Max(1, (int)Math.Ceiling(_items.Count / (double)PageSize));

    private void RenderPage()
    {
        gridReceptions.Rows.Clear();

        IEnumerable<ReceptionItemDto> pageItems = _items
            .Skip((_page - 1) * PageSize)
            .Take(PageSize);

        foreach (ReceptionItemDto item in pageItems)
        {
            int rowIndex = gridReceptions.Rows.Add(
                item.AppointmentCode,
                item.PatientCode,
                item.PatientName,
                item.Gender,
                item.DateOfBirth.ToString("dd/MM/yyyy"),
                DisplayOrDash(item.InsuranceCardNumber),
                item.DepartmentName,
                DisplayOrDash(item.DoctorName),
                FormatAppointmentTime(item.AppointmentTime),
                DisplayOrDash(item.QueueNumber),
                item.StatusName,
                _activeTab == ReceptionTab.Waiting ? "Tiếp nhận" : string.Empty);

            gridReceptions.Rows[rowIndex].Tag = item;
        }

        colAction.Visible = _activeTab == ReceptionTab.Waiting;
        gridReceptions.ClearSelection();

        int first = _items.Count == 0 ? 0 : ((_page - 1) * PageSize) + 1;
        int last = Math.Min(_page * PageSize, _items.Count);
        lblPageInfo.Text = $"Hiển thị {first}–{last} / {_items.Count} hồ sơ";

        string tabName = _activeTab switch
        {
            ReceptionTab.Waiting => "chờ tiếp nhận",
            ReceptionTab.Received => "đã tiếp nhận",
            ReceptionTab.Scheduled => "lịch hẹn",
            _ => "đã hủy"
        };

        lblListSubtitle.Text = $"Ngày {DateTime.Today:dd/MM/yyyy} · {_items.Count} hồ sơ {tabName}";
        lblListTitle.Text = _activeTab == ReceptionTab.Scheduled ? "Danh sách lịch hẹn" : "Danh sách tiếp đón";

        BuildPager();
    }

    private void BuildPager()
    {
        pagerPanel.SuspendLayout();

        Control[] oldButtons = pagerPanel.Controls.Cast<Control>().ToArray();
        pagerPanel.Controls.Clear();

        foreach (Control control in oldButtons)
        {
            control.Dispose();
        }

        pagerPanel.Controls.Add(CreatePagerButton("‹", _page - 1, _page > 1, false));

        int start = Math.Max(1, _page - 2);
        int end = Math.Min(TotalPages, start + 4);
        start = Math.Max(1, end - 4);

        for (int pageNumber = start; pageNumber <= end; pageNumber++)
        {
            pagerPanel.Controls.Add(CreatePagerButton(pageNumber.ToString(), pageNumber, true, pageNumber == _page));
        }

        pagerPanel.Controls.Add(CreatePagerButton("›", _page + 1, _page < TotalPages, false));

        pagerPanel.ResumeLayout();
    }

    private Button CreatePagerButton(string text, int targetPage, bool enabled, bool active)
    {
        var button = new Button
        {
            Text = text,
            Size = new Size(32, 28),
            Margin = new Padding(0, 0, 4, 0),
            FlatStyle = FlatStyle.Flat,
            Font = active ? PagerActiveFont : PagerFont,
            BackColor = active ? Theme.Blue : Color.White,
            ForeColor = active ? Color.White : enabled ? Color.FromArgb(83, 97, 116) : Color.FromArgb(189, 197, 206),
            Enabled = enabled,
            Cursor = enabled ? Cursors.Hand : Cursors.Default,
            TabStop = false
        };

        button.FlatAppearance.BorderColor = active ? Theme.Blue : Theme.Line;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.MouseOverBackColor = active ? Theme.Blue : Theme.BlueSoft;
        button.FlatAppearance.MouseDownBackColor = active ? Theme.BlueDark : Theme.BlueSoft;

        if (enabled && !active)
        {
            button.Click += (_, _) =>
            {
                _page = Math.Clamp(targetPage, 1, TotalPages);
                RenderPage();
            };
        }

        return button;
    }

    private void GridReceptions_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != colStatus.Index)
        {
            return;
        }

        string status = e.Value?.ToString() ?? string.Empty;

        e.CellStyle.ForeColor = status switch
        {
            "Chờ xác nhận" => Theme.Amber,
            "Đã xác nhận" => Theme.Blue,
            "Đã tiếp nhận" => Theme.Green,
            "Đang khám" => Theme.Purple,
            "Đã hoàn tất" => Color.FromArgb(71, 90, 111),
            "Đã hủy" => Theme.Red,
            _ => Theme.Muted
        };
    }

    private void GridReceptions_CellMouseEnter(object? sender, DataGridViewCellEventArgs e)
    {
        bool isAction = e.RowIndex >= 0
            && e.ColumnIndex == colAction.Index
            && _activeTab == ReceptionTab.Waiting;

        gridReceptions.Cursor = isAction ? Cursors.Hand : Cursors.Default;
    }

    private void BtnExport_Click(object? sender, EventArgs e)
    {
        if (_items.Count == 0)
        {
            MessageBox.Show(FindForm(), "Không có dữ liệu để xuất.", "Xuất CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Filter = "Tệp CSV (*.csv)|*.csv",
            FileName = $"tiep-don-{DateTime.Now:yyyyMMdd-HHmm}.csv",
            Title = "Xuất danh sách tiếp đón"
        };

        if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        try
        {
            var content = new StringBuilder();
            content.AppendLine("Mã lượt khám,Mã BN,Họ và tên,Giới tính,Ngày sinh,Số BHYT,Chuyên khoa,Bác sĩ,Thời gian,Số thứ tự,Trạng thái,Đặt qua");

            foreach (ReceptionItemDto item in _items)
            {
                content.AppendLine(string.Join(",",
                    Csv(item.AppointmentCode),
                    Csv(item.PatientCode),
                    Csv(item.PatientName),
                    Csv(item.Gender),
                    Csv(item.DateOfBirth.ToString("dd/MM/yyyy")),
                    Csv(item.InsuranceCardNumber),
                    Csv(item.DepartmentName),
                    Csv(item.DoctorName),
                    Csv(item.AppointmentTime.ToString("dd/MM/yyyy HH:mm")),
                    Csv(item.QueueNumber),
                    Csv(item.StatusName),
                    Csv(item.BookingChannel)));
            }

            File.WriteAllText(dialog.FileName, content.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));

            MessageBox.Show(FindForm(), $"Đã xuất {_items.Count} hồ sơ ra tệp:\n{dialog.FileName}", "Xuất CSV thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(FindForm(), ex.Message, "Lỗi xuất CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplyTabVisual()
    {
        (Button button, ReceptionTab tab)[] tabs =
        {
            (btnTabWaiting, ReceptionTab.Waiting),
            (btnTabReceived, ReceptionTab.Received),
            (btnTabScheduled, ReceptionTab.Scheduled),
            (btnTabCancelled, ReceptionTab.Cancelled)
        };

        foreach ((Button button, ReceptionTab tab) in tabs)
        {
            bool active = tab == _activeTab;
            button.BackColor = active ? Color.White : Theme.Canvas;
            button.ForeColor = active ? Theme.Blue : Theme.Muted;
            button.Font = active ? TabActiveFont : TabInactiveFont;
        }

        Button activeButton = _activeTab switch
        {
            ReceptionTab.Waiting => btnTabWaiting,
            ReceptionTab.Received => btnTabReceived,
            ReceptionTab.Scheduled => btnTabScheduled,
            _ => btnTabCancelled
        };

        tabIndicator.Left = activeButton.Left;
        tabIndicator.Width = activeButton.Width;
    }

    private void UpdateTabCounts(ReceptionCountsDto counts)
    {
        btnTabWaiting.Text = $"Chờ tiếp nhận ({counts.Waiting})";
        btnTabReceived.Text = $"Đã tiếp nhận ({counts.Received})";
        btnTabScheduled.Text = $"Lịch hẹn ({counts.Scheduled})";
        btnTabCancelled.Text = $"Đã hủy ({counts.Cancelled})";
    }

    private void SelectAppointment(int appointmentId)
    {
        foreach (DataGridViewRow row in gridReceptions.Rows)
        {
            if (row.Tag is ReceptionItemDto item && item.AppointmentId == appointmentId)
            {
                row.Selected = true;
                gridReceptions.CurrentCell = row.Cells[colPatientName.Index];
                return;
            }
        }
    }

    private static string FormatAppointmentTime(DateTime time)
    {
        return time.Date == DateTime.Today ? time.ToString("HH:mm") : time.ToString("dd/MM HH:mm");
    }

    private static string DisplayOrDash(string value) => string.IsNullOrWhiteSpace(value) ? "—" : value;

    private static string Csv(string value)
    {
        return $"\"{value.Replace("\"", "\"\"")}\"";
    }
}
