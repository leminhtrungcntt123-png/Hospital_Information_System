using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using HIS.BLL;
using HIS.BLL.DTOs;

namespace HIS.WinForms;

public partial class LoginForm : Form
{
    private const int WM_NCLBUTTONDOWN = 0x00A1;
    private const int HTCAPTION = 2;
    private const int EM_SETCUEBANNER = 0x1501;

    private static readonly Color InputBackColor = Color.FromArgb(247, 249, 252);
    private static readonly Color InputFocusColor = Color.FromArgb(232, 242, 253);

    private readonly IAuthService? _authService;
    private bool _busy;

    [DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

    public AuthenticatedUserDto? AuthenticatedUser { get; private set; }

    public LoginForm() : this(null)
    {
    }

    public LoginForm(IAuthService? authService)
    {
        _authService = authService;

        InitializeComponent();

        DoubleBuffered = true;
        AcceptButton = btnLogin;

        WireEvents();
    }

    private void WireEvents()
    {
        btnCloseLogin.Click += (_, _) => Close();
        btnMinimizeLogin.Click += (_, _) => WindowState = FormWindowState.Minimized;
        btnLogin.Click += async (_, _) => await SignInAsync();
        btnTogglePassword.Click += (_, _) => TogglePasswordVisibility();

        titleBarPanel.MouseDown += TitleBar_MouseDown;
        logoMark.MouseDown += TitleBar_MouseDown;
        lblAppTitle.MouseDown += TitleBar_MouseDown;

        brandPanel.Paint += BrandPanel_Paint;

        txtUsername.Enter += (_, _) => SetInputFocus(pnlUsername, txtUsername, true);
        txtUsername.Leave += (_, _) => SetInputFocus(pnlUsername, txtUsername, false);
        txtPassword.Enter += (_, _) => SetInputFocus(pnlPassword, txtPassword, true);
        txtPassword.Leave += (_, _) => SetInputFocus(pnlPassword, txtPassword, false);

        txtUsername.TextChanged += (_, _) => ClearError();
        txtPassword.TextChanged += (_, _) => ClearError();

        txtUsername.KeyDown += (_, e) =>
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            txtPassword.Focus();
        };

        loginHost.Resize += (_, _) => CenterCard();
        titleBarPanel.Resize += (_, _) => LayoutTitleBarButtons();

        Shown += (_, _) =>
        {
            LayoutTitleBarButtons();
            CenterCard();
            txtUsername.Focus();
        };

        KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        };
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        SendMessage(txtUsername.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập tên đăng nhập");
        SendMessage(txtPassword.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập mật khẩu");
    }

    private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        ReleaseCapture();
        SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);
    }

    private void BrandPanel_Paint(object? sender, PaintEventArgs e)
    {
        Rectangle bounds = brandPanel.ClientRectangle;
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        using (var gradient = new LinearGradientBrush(
                   bounds,
                   Color.FromArgb(11, 39, 70),
                   Color.FromArgb(21, 88, 150),
                   LinearGradientMode.ForwardDiagonal))
        {
            e.Graphics.FillRectangle(gradient, bounds);
        }

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        using var ring = new Pen(Color.FromArgb(34, 255, 255, 255), 1.5f);
        e.Graphics.DrawEllipse(ring, bounds.Width - 190, -110, 330, 330);
        e.Graphics.DrawEllipse(ring, bounds.Width - 120, -40, 200, 200);

        using var glow = new SolidBrush(Color.FromArgb(20, 255, 255, 255));
        e.Graphics.FillEllipse(glow, -90, bounds.Height - 220, 300, 300);
    }

    private void CenterCard()
    {
        int x = Math.Max(24, (loginHost.ClientSize.Width - card.Width) / 2);
        int y = Math.Max(24, (loginHost.ClientSize.Height - card.Height) / 2);

        card.Location = new Point(x, y);
    }

    private void LayoutTitleBarButtons()
    {
        int height = titleBarPanel.ClientSize.Height;
        if (height <= 0)
        {
            return;
        }

        int closeWidth = btnCloseLogin.Width;
        int minimizeWidth = btnMinimizeLogin.Width;

        btnCloseLogin.Size = new Size(closeWidth, height);
        btnMinimizeLogin.Size = new Size(minimizeWidth, height);
        btnCloseLogin.Location = new Point(titleBarPanel.ClientSize.Width - closeWidth, 0);
        btnMinimizeLogin.Location = new Point(btnCloseLogin.Left - minimizeWidth, 0);
    }

    private static void SetInputFocus(Panel container, TextBox textBox, bool focused)
    {
        Color color = focused ? InputFocusColor : InputBackColor;

        container.BackColor = color;
        textBox.BackColor = color;
    }

    private void TogglePasswordVisibility()
    {
        txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        btnTogglePassword.Text = txtPassword.UseSystemPasswordChar ? "Hiện" : "Ẩn";
        txtPassword.Focus();
    }

    private async Task SignInAsync()
    {
        if (_busy)
        {
            return;
        }

        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;

        if (username.Length == 0)
        {
            ShowError("Vui lòng nhập tên đăng nhập.");
            txtUsername.Focus();
            return;
        }

        if (password.Length == 0)
        {
            ShowError("Vui lòng nhập mật khẩu.");
            txtPassword.Focus();
            return;
        }

        if (_authService == null)
        {
            ShowError("Chưa kết nối được dịch vụ xác thực.");
            return;
        }

        SetBusy(true);

        try
        {
            LoginResultDto result = await _authService.LoginAsync(username, password);

            if (!result.Success || result.User == null)
            {
                ShowError(result.Message);
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }

            if (string.Equals(result.User.RoleCode, "PATIENT", StringComparison.OrdinalIgnoreCase))
            {
                ShowError("Tài khoản bệnh nhân vui lòng đăng nhập tại cổng web.");
                return;
            }

            AuthenticatedUser = result.User;
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception)
        {
            ShowError("Không thể kết nối cơ sở dữ liệu. Vui lòng kiểm tra PostgreSQL và chuỗi kết nối.");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void SetBusy(bool busy)
    {
        _busy = busy;
        btnLogin.Enabled = !busy;
        btnLogin.Text = busy ? "Đang đăng nhập..." : "Đăng nhập";
        txtUsername.Enabled = !busy;
        txtPassword.Enabled = !busy;
        btnTogglePassword.Enabled = !busy;
    }

    private void ShowError(string message)
    {
        lblError.Text = message;
    }

    private void ClearError()
    {
        if (lblError.Text.Length > 0)
        {
            lblError.Text = string.Empty;
        }
    }
}
