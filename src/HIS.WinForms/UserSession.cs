namespace HIS.WinForms;

public static class UserSession
{
    public const string AdminRoleCode = "ADMIN";

    public static string Username { get; private set; } = "admin";
    public static string FullName { get; private set; } = "Quản trị viên";
    public static string RoleCode { get; private set; } = AdminRoleCode;
    public static string RoleName { get; private set; } = "Quản trị hệ thống";

    public static bool IsAdmin => string.Equals(RoleCode, AdminRoleCode, StringComparison.OrdinalIgnoreCase);

    public static void SignIn(string username, string fullName, string roleCode, string roleName)
    {
        Username = username;
        FullName = fullName;
        RoleCode = roleCode;
        RoleName = roleName;
    }

    // Khởi tạo phiên làm việc mặc định quyền Quản trị viên (Admin)
    public static void UseMockAdmin() => SignIn("admin", "Quản trị viên", AdminRoleCode, "Quản trị hệ thống");
}
