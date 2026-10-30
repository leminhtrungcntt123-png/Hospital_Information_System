using System.ComponentModel;
using System.Runtime.InteropServices;
using HIS.BLL;
using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using HIS.BLL.Security;
using HIS.WinForms.Controls;
using HIS.WinForms.Pages;

namespace HIS.WinForms;

public partial class MainForm : Form
{
    private const int WM_NCHITTEST = 0x0084;
    private const int HTCLIENT = 1;
    private const int HTCAPTION = 2;
    private const int HTLEFT = 10;
    private const int HTRIGHT = 11;
    private const int HTTOP = 12;
    private const int HTTOPLEFT = 13;
    private const int HTTOPRIGHT = 14;
    private const int HTBOTTOM = 15;
    private const int HTBOTTOMLEFT = 16;
    private const int HTBOTTOMRIGHT = 17;
    private const int ResizeBorder = 6;

    private readonly IBookingService _bookingService;
    private readonly IAdminService _adminService;
    private readonly IReceptionService _receptionService;
    private readonly IStaffService? _staffService;
    private readonly IDepartmentService? _departmentService;
    private readonly IAuthService? _authService;
    private readonly UserSession? _session;
    private UserControl? _currentPage;

    public bool LogoutRequested { get; private set; }

    private readonly ToolStripMenuItem _menuItemStaff = new("Quản lý nhân sự và tài khoản");
    private readonly ToolStripMenuItem _menuItemDepartment = new("Quản lý khoa và phòng");

    [DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    // Constructor không tham số để Visual Studio Designer có thể mở form
    public MainForm() : this(null!, null!, null!, null!, null!, null!, null!)
    {
    }

    // Constructor dùng Dependency Injection khi chạy thật
    public MainForm(
        IBookingService bookingService,
        IAdminService adminService,
        IReceptionService receptionService,
        IStaffService staffService,
        IDepartmentService departmentService,
        IAuthService authService,
        UserSession session)
    {
        _bookingService = bookingService;
        _adminService = adminService;
        _receptionService = receptionService;
        _staffService = staffService;
        _departmentService = departmentService;
        _authService = authService;
        _session = session;

        InitializeComponent();
        DoubleBuffered = true;
        KeyPreview = true;

        BuildSessionMenu();

        WireNavigation();
        WireToolbar();
        WireWindowChrome();

        lblBadgeReception.BringToFront();
        lblBadgeExamination.BringToFront();

        ApplySession();
        ApplyPermissions();

        ShowDashboard();

        if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
        {
            UpdateClock();
            clockTimer.Start();
            lblBadgeReception.Visible = false;
            Shown += async (_, _) => await UpdateReceptionBadgeAsync();
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyPermissions();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        // Form viền mặc định bị tắt nên phải giới hạn vùng maximized theo vùng làm việc
        // để cửa sổ không che mất taskbar
        if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        {
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
        }
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg != WM_NCHITTEST || WindowState != FormWindowState.Normal)
        {
            return;
        }

        if (m.Result != (IntPtr)HTCLIENT)
        {
            return;
        }

        long lParam = m.LParam.ToInt64();
        int screenX = (short)(lParam & 0xFFFF);
        int screenY = (short)((lParam >> 16) & 0xFFFF);
        Point point = PointToClient(new Point(screenX, screenY));

        bool left = point.X <= ResizeBorder;
        bool right = point.X >= ClientSize.Width - ResizeBorder;
        bool top = point.Y <= ResizeBorder;
        bool bottom = point.Y >= ClientSize.Height - ResizeBorder;

        if (top && left)
            m.Result = (IntPtr)HTTOPLEFT;
        else if (top && right)
            m.Result = (IntPtr)HTTOPRIGHT;
        else if (bottom && left)
            m.Result = (IntPtr)HTBOTTOMLEFT;
        else if (bottom && right)
            m.Result = (IntPtr)HTBOTTOMRIGHT;
        else if (left)
            m.Result = (IntPtr)HTLEFT;
        else if (right)
            m.Result = (IntPtr)HTRIGHT;
        else if (top)
            m.Result = (IntPtr)HTTOP;
        else if (bottom)
            m.Result = (IntPtr)HTBOTTOM;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.KeyCode == Keys.F3)
        {
            txtGlobalSearch.Focus();
            e.Handled = true;
        }
    }

    private void WireWindowChrome()
    {
        btnMinimize.Click += (_, _) => WindowState = FormWindowState.Minimized;
        btnMaximize.Click += (_, _) => ToggleMaximize();
        btnClose.Click += (_, _) => Close();

        btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 79, 126);
        btnMaximize.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 79, 126);
        btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(217, 72, 79);

        foreach (Control control in new Control[] { titleBarPanel, logoMark, lblAppTitle, windowControlsPanel })
        {
            control.MouseDown += TitleBarMouseDown;
            control.DoubleClick += (_, _) => ToggleMaximize();
        }
    }

    private void TitleBarMouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;

        ReleaseCapture();
        SendMessage(Handle, 0x00A1, HTCAPTION, 0);
    }

    private void ToggleMaximize()
    {
        if (WindowState == FormWindowState.Maximized)
        {
            WindowState = FormWindowState.Normal;
            return;
        }

        MaximizedBounds = Screen.FromControl(this).WorkingArea;
        WindowState = FormWindowState.Maximized;
    }

    private void BuildSessionMenu()
    {
        menuSystem.Items.Add(_menuItemStaff);
        menuSystem.Items.Add(_menuItemDepartment);
    }

    private void WireNavigation()
    {
        btnNavDashboard.Click += (_, _) => ShowDashboard();

        btnNavReception.Click += (_, _) => ShowReceptionPage();

        btnNavPatients.Click += (_, _) => ShowPlaceholder(
            "patients",
            "Quản lý bệnh nhân",
            "Tra cứu và quản lý hồ sơ hành chính, lịch sử khám chữa bệnh.");

        btnNavExamination.Click += (_, _) => ShowPlaceholder(
            "examination",
            "Khám ngoại trú",
            "Tiếp nhận thông tin lâm sàng, chỉ định cận lâm sàng và kê đơn thuốc.");

        btnNavInpatient.Click += (_, _) => ShowPlaceholder(
            "inpatient",
            "Quản lý nội trú",
            "Theo dõi giường bệnh, hồ sơ điều trị và tình trạng khoa phòng.");

        btnNavPharmacy.Click += (_, _) => ShowPlaceholder(
            "pharmacy",
            "Dược và kho thuốc",
            "Quản lý cấp phát, tồn kho và cảnh báo dược.");

        btnNavReports.Click += (_, _) => ShowPlaceholder(
            "reports",
            "Báo cáo thống kê",
            "Tổng hợp số liệu hoạt động khám chữa bệnh và vận hành bệnh viện.");

        lblBadgeReception.Click += (_, _) => btnNavReception.PerformClick();
        lblBadgeExamination.Click += (_, _) => btnNavExamination.PerformClick();

        btnMenuSystem.Click += (_, _) => menuSystem.Show(btnMenuSystem, new Point(0, btnMenuSystem.Height));
        menuItemAdmin.Click += (_, _) => ShowAdminPage();
        _menuItemStaff.Click += (_, _) => ShowStaffPage();
        _menuItemDepartment.Click += (_, _) => ShowDepartmentPage();
        btnSettings.Click += (_, _) => ShowAdminPage();

        btnHelp.Click += (_, _) => MessageBox.Show(
            this,
            "Hệ thống quản lý bệnh viện HIS phiên bản 2.5.1.\nLiên hệ phòng Công nghệ thông tin khi cần hỗ trợ.",
            "Trợ giúp sử dụng",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        btnLogout.Click += async (_, _) =>
        {
            DialogResult result = MessageBox.Show(
                this,
                "Bạn có muốn đăng xuất khỏi hệ thống?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (_authService != null && _session?.User is { } user)
                {
                    await _authService.LogoutAsync(user.AccountId);
                }
            }
            catch
            {
                // Ghi nhật ký lỗi không được chặn thao tác đăng xuất
            }

            _session?.SignOut();
            LogoutRequested = true;
            Close();
        };
    }

    private void WireToolbar()
    {
        btnNewReception.Click += (_, _) =>
        {
            ShowReceptionPage();

            if (_currentPage is ReceptionPage page)
            {
                page.OpenNewReception();
            }
        };

        btnRefresh.Click += async (_, _) =>
        {
            if (_currentPage is DashboardPage)
            {
                ShowDashboard();
            }
            else if (_currentPage is ReceptionPage receptionPage)
            {
                await receptionPage.RefreshAsync();
            }
        };

        btnNotifications.Click += (_, _) => MessageBox.Show(
            this,
            "Bạn có 3 thông báo mới từ hệ thống.",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        txtGlobalSearch.KeyDown += (_, e) =>
        {
            if (e.KeyCode != Keys.Enter) return;

            e.SuppressKeyPress = true;
            btnNavPatients.PerformClick();
        };

        toolTip.SetToolTip(btnRefresh, "Làm mới");
        toolTip.SetToolTip(btnNotifications, "Thông báo");
        toolTip.SetToolTip(btnLogout, "Đăng xuất");
    }

    private void ShowDashboard()
    {
        var page = new DashboardPage();
        page.NavigateRequested += OpenView;

        SetActiveNav(btnNavDashboard);
        lblBreadcrumb.Text = "HIS   ›   Bàn làm việc";
        ShowPage(page);
    }

    private void ShowReceptionPage()
    {
        if (!EnsureAccess(RolePermissions.Reception))
        {
            return;
        }

        if (_receptionService == null)
        {
            MessageBox.Show(
                this,
                "Trang tiếp đón cần kết nối cơ sở dữ liệu.",
                "Tiếp đón bệnh nhân",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        if (_currentPage is ReceptionPage)
        {
            return;
        }

        SetActiveNav(btnNavReception);
        lblBreadcrumb.Text = "HIS   ›   Tiếp đón bệnh nhân";

        var page = new ReceptionPage(_receptionService);
        page.CountsChanged += counts => UpdateReceptionBadge(counts.Waiting);
        ShowPage(page);
    }

    private async Task UpdateReceptionBadgeAsync()
    {
        if (_receptionService == null || !CanAccess(RolePermissions.Reception))
        {
            return;
        }

        try
        {
            ReceptionCountsDto counts = await _receptionService.GetCountsAsync();
            UpdateReceptionBadge(counts.Waiting);
        }
        catch
        {
            lblBadgeReception.Visible = false;
        }
    }

    private void UpdateReceptionBadge(int waitingCount)
    {
        if (!CanAccess(RolePermissions.Reception))
        {
            lblBadgeReception.Visible = false;
            return;
        }

        lblBadgeReception.Text = Math.Min(waitingCount, 99).ToString();
        lblBadgeReception.Visible = waitingCount > 0;
    }

    private void ShowPlaceholder(string key, string title, string subtitle)
    {
        string? module = ModuleForKey(key);
        if (module != null && !EnsureAccess(module))
        {
            return;
        }

        SetActiveNav(GetNavButton(key));
        lblBreadcrumb.Text = $"HIS   ›   {title}";
        ShowPage(new PlaceholderPage(title, subtitle));
    }

    private void ShowAdminPage()
    {
        if (!EnsureAccess(RolePermissions.Admin))
        {
            return;
        }

        if (_adminService == null)
        {
            MessageBox.Show(
                this,
                "Trang quản lý hệ thống cần kết nối cơ sở dữ liệu.",
                "Quản lý hệ thống",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        SetActiveNav(null);
        lblBreadcrumb.Text = "HIS   ›   Quản lý hệ thống";
        ShowPage(new AdminPage(_adminService));
    }

    private void ShowStaffPage()
    {
        if (!EnsureAccess(RolePermissions.Admin))
        {
            return;
        }

        if (_staffService == null)
        {
            MessageBox.Show(
                this,
                "Trang nhân sự cần kết nối cơ sở dữ liệu.",
                "Nhân sự",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        SetActiveNav(null);
        lblBreadcrumb.Text = "HIS   ›   Nhân sự";
        ShowPage(new StaffPage(_staffService));
    }

    private void ShowDepartmentPage()
    {
        if (!EnsureAccess(RolePermissions.Admin))
        {
            return;
        }

        if (_departmentService == null)
        {
            MessageBox.Show(
                this,
                "Trang quản lý khoa và phòng cần kết nối cơ sở dữ liệu.",
                "Quản lý khoa và phòng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        SetActiveNav(null);
        lblBreadcrumb.Text = "HIS   ›   Quản lý khoa và phòng";
        ShowPage(new DepartmentPage(_departmentService));
    }

    private void ShowPage(UserControl page)
    {
        contentHostPanel.SuspendLayout();

        if (_currentPage != null)
        {
            contentHostPanel.Controls.Remove(_currentPage);
            _currentPage.Dispose();
        }

        _currentPage = page;
        page.Dock = DockStyle.Fill;
        contentHostPanel.Controls.Add(page);

        contentHostPanel.ResumeLayout();
    }

    private void OpenView(string view)
    {
        switch (view)
        {
            case "dashboard":
                ShowDashboard();
                break;
            case "patients":
                btnNavPatients.PerformClick();
                break;
            case "examination":
                btnNavExamination.PerformClick();
                break;
            case "inpatient":
                btnNavInpatient.PerformClick();
                break;
            case "pharmacy":
                btnNavPharmacy.PerformClick();
                break;
            case "reports":
                btnNavReports.PerformClick();
                break;
            default:
                btnNavReception.PerformClick();
                break;
        }
    }

    private void SetActiveNav(NavButton? active)
    {
        NavButton[] buttons =
        {
            btnNavDashboard,
            btnNavReception,
            btnNavPatients,
            btnNavExamination,
            btnNavInpatient,
            btnNavPharmacy,
            btnNavReports
        };

        foreach (NavButton button in buttons)
        {
            button.Active = ReferenceEquals(button, active);
        }
    }

    private NavButton? GetNavButton(string key) => key switch
    {
        "dashboard" => btnNavDashboard,
        "reception" => btnNavReception,
        "patients" => btnNavPatients,
        "examination" => btnNavExamination,
        "inpatient" => btnNavInpatient,
        "pharmacy" => btnNavPharmacy,
        "reports" => btnNavReports,
        _ => null
    };

    private void ApplySession()
    {
        if (_session?.User is not { } user)
        {
            return;
        }

        accountName.Text = user.DisplayName;
        accountRole.Text = user.RoleName;
        accountAvatar.Text = GetInitials(user.DisplayName);
    }

    private void ApplyPermissions()
    {
        bool isAdmin = _session?.IsAdmin ?? false;

        bool canDashboard = CanAccess(RolePermissions.Dashboard);
        bool canReception = CanAccess(RolePermissions.Reception);
        bool canPatients = CanAccess(RolePermissions.Patients);
        bool canExamination = CanAccess(RolePermissions.Examination);
        bool canInpatient = CanAccess(RolePermissions.Inpatient);
        bool canPharmacy = CanAccess(RolePermissions.Pharmacy);
        bool canReports = CanAccess(RolePermissions.Reports);

        btnMenuSystem.Visible = isAdmin;
        menuItemAdmin.Visible = isAdmin;
        _menuItemStaff.Visible = isAdmin;
        _menuItemDepartment.Visible = isAdmin;
        btnSettings.Visible = isAdmin;
        btnHelp.Top = isAdmin ? 51 : 9;

        btnNewReception.Visible = canReception;

        txtGlobalSearch.Enabled = canPatients;
        if (!txtGlobalSearch.Enabled)
        {
            txtGlobalSearch.PlaceholderText = "Không có quyền tra cứu bệnh nhân";
        }

        LayoutMenuBar(isAdmin);
        LayoutNavigation(canDashboard, canReception, canPatients, canExamination, canInpatient, canPharmacy, canReports);
    }

    private void LayoutMenuBar(bool isAdmin)
    {
        int x = 7;

        foreach (Button button in new[] { btnMenuSystem, btnMenuBusiness, btnMenuCatalog, btnMenuReports, btnMenuHelp })
        {
            if (button == btnMenuSystem && !isAdmin)
            {
                continue;
            }

            button.Left = x;
            x += button.Width;
        }
    }

    private void LayoutNavigation(
        bool canDashboard,
        bool canReception,
        bool canPatients,
        bool canExamination,
        bool canInpatient,
        bool canPharmacy,
        bool canReports)
    {
        int y = 5;

        y = PlaceNavLabel(lblNavGroupOverview, y, canDashboard);
        y = PlaceNavButton(btnNavDashboard, null, y, canDashboard);

        bool clinicalVisible = canReception || canPatients || canExamination || canInpatient;

        if (clinicalVisible)
        {
            y += 12;
            y = PlaceNavLabel(lblNavGroupClinical, y, true);
            y = PlaceNavButton(btnNavReception, lblBadgeReception, y, canReception);
            y = PlaceNavButton(btnNavPatients, null, y, canPatients);
            y = PlaceNavButton(btnNavExamination, lblBadgeExamination, y, canExamination);
            y = PlaceNavButton(btnNavInpatient, null, y, canInpatient);
        }
        else
        {
            lblNavGroupClinical.Visible = false;
            btnNavReception.Visible = false;
            btnNavPatients.Visible = false;
            btnNavExamination.Visible = false;
            btnNavInpatient.Visible = false;
            lblBadgeReception.Visible = false;
            lblBadgeExamination.Visible = false;
        }

        bool paraclinicalVisible = canPharmacy || canReports;

        if (paraclinicalVisible)
        {
            y += 12;
            y = PlaceNavLabel(lblNavGroupParaclinical, y, true);
            y = PlaceNavButton(btnNavPharmacy, null, y, canPharmacy);
            y = PlaceNavButton(btnNavReports, null, y, canReports);
        }
        else
        {
            lblNavGroupParaclinical.Visible = false;
            btnNavPharmacy.Visible = false;
            btnNavReports.Visible = false;
        }
    }

    private static int PlaceNavLabel(Label label, int y, bool visible)
    {
        label.Visible = visible;
        if (!visible)
        {
            return y;
        }

        label.Top = y;
        label.BringToFront();
        return y + 24;
    }

    private static int PlaceNavButton(Button button, Label? badge, int y, bool visible)
    {
        button.Visible = visible;

        if (!visible)
        {
            if (badge != null)
            {
                badge.Visible = false;
            }

            return y;
        }

        button.Top = y;
        button.BringToFront();
        if (badge != null)
        {
            badge.Top = y + 16;
            badge.BringToFront();
        }

        return y + 55;
    }

    private bool CanAccess(string module) => _session?.Can(module) ?? true;

    private bool EnsureAccess(string module)
    {
        if (CanAccess(module))
        {
            return true;
        }

        MessageBox.Show(
            this,
            "Bạn không có quyền truy cập chức năng này.",
            "Phân quyền",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return false;
    }

    private static string? ModuleForKey(string key) => key switch
    {
        "dashboard" => RolePermissions.Dashboard,
        "reception" => RolePermissions.Reception,
        "patients" => RolePermissions.Patients,
        "examination" => RolePermissions.Examination,
        "inpatient" => RolePermissions.Inpatient,
        "pharmacy" => RolePermissions.Pharmacy,
        "reports" => RolePermissions.Reports,
        "admin" => RolePermissions.Admin,
        _ => null
    };

    private static string GetInitials(string name)
    {
        string[] parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
        {
            return "?";
        }

        if (parts.Length == 1)
        {
            return parts[0].Length >= 2
                ? parts[0][..2].ToUpperInvariant()
                : parts[0].ToUpperInvariant();
        }

        return $"{parts[^2][0]}{parts[^1][0]}".ToUpperInvariant();
    }

    private void ClockTimer_Tick(object? sender, EventArgs e) => UpdateClock();

    private void UpdateClock()
    {
        DateTime now = DateTime.Now;
        lblStatusRight.Text = $"Trạm TIEPDON-03          {now:HH:mm:ss}";
        lblSessionDate.Text = now.ToString("dd/MM/yyyy HH:mm");
    }
}
