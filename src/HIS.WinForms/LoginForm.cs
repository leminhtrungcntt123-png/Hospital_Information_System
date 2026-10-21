using HIS.BLL;

namespace HIS.WinForms;

public partial class LoginForm : Form
{
    private readonly IAuthService _authService;
    private readonly UserSession _session;
    private bool _busy;

    // Constructor không tham số để Visual Studio Designer có thể mở form
    public LoginForm() : this(null!, null!)
    {
    }

    public LoginForm(IAuthService authService, UserSession session)
    {
        _authService = authService;
        _session = session;

        InitializeComponent();

        btnLogin.Click += async (_, _) => await SignInAsync();
        chkShowPassword.CheckedChanged += (_, _) => txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        txtUsername.TextChanged += (_, _) => ClearError();
        txtPassword.TextChanged += (_, _) => ClearError();
        KeyDown += LoginForm_KeyDown;
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        txtUsername.Focus();
    }

    private void LoginForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape && !_busy)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
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

        SetBusy(true);

        try
        {
            LoginResult result = await _authService.LoginAsync(username, password);

            if (!result.Succeeded || result.User is null)
            {
                ShowError(result.ErrorMessage ?? "Đăng nhập thất bại.");
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }

            _session.SignIn(result.User);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            ShowError("Không kết nối được cơ sở dữ liệu. Kiểm tra PostgreSQL và chuỗi kết nối.\n" + ex.Message);
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
        UseWaitCursor = busy;
    }

    private void ShowError(string message) => lblError.Text = message;

    private void ClearError()
    {
        if (lblError.Text.Length > 0)
        {
            lblError.Text = string.Empty;
        }
    }
}
