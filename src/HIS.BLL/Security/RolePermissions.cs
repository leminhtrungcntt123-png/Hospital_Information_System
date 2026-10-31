namespace HIS.BLL.Security;

public static class RolePermissions
{
    public const string Dashboard = "dashboard";
    public const string Reception = "reception";
    public const string Patients = "patients";
    public const string Examination = "examination";
    public const string Inpatient = "inpatient";
    public const string Pharmacy = "pharmacy";
    public const string Reports = "reports";
    public const string Admin = "admin";
    public const string Staff = "staff";
    public const string Department = "department";

    private static readonly string[] AdminModules =
    {
        Dashboard, Reception, Patients, Examination, Inpatient, Pharmacy, Reports, Admin, Staff, Department
    };

    private static readonly string[] ReceptionistModules =
    {
        Dashboard, Reception, Patients
    };

    private static readonly string[] DoctorModules =
    {
        Dashboard, Patients, Examination
    };

    private static readonly string[] NurseModules =
    {
        Dashboard, Examination, Inpatient
    };

    // Tài khoản bệnh nhân chỉ dùng cho cổng web, không có quyền trong ứng dụng quản trị máy bàn.
    private static readonly string[] NoModules = Array.Empty<string>();

    public static IReadOnlyList<string> For(string? roleCode) => roleCode?.Trim().ToUpperInvariant() switch
    {
        "ADMIN" => AdminModules,
        "RECEPTIONIST" => ReceptionistModules,
        "DOCTOR" => DoctorModules,
        "NURSE" => NurseModules,
        _ => NoModules
    };
}
