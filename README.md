# HIS - Hospital Information System

Hệ thống hỗ trợ đặt lịch khám và quản lý hồ sơ bệnh án (Đồ án môn học: Lập trình trực quan & Lập trình Web).

## Cấu trúc Solution

- **HIS.sln**: Solution chứa tất cả các dự án.
- **src/HIS.Domain**: Định nghĩa các Entity nghiệp vụ (PascalCase, độc lập).
- **src/HIS.DAL**: Data Access Layer, Entity Framework Core 8, `AppDbContext`, Npgsql PostgreSQL (snake_case convention).
- **src/HIS.BLL**: Business Logic Layer, triển khai các Service dùng chung giữa WinForms và WebApi/Web.
- **src/HIS.WinForms**: Ứng dụng Desktop (Windows Forms App, .NET 8, Windows-only).
- **src/HIS.WebApi**: RESTful API (ASP.NET Core Web API, .NET 8, Swagger).
- **src/HIS.Web**: Ứng dụng Web dành cho bệnh nhân (ASP.NET Core Razor Pages, .NET 8).
- **tests/HIS.Tests**: Unit Test dự án (xUnit, Moq, InMemory EF Core).

## Yêu cầu môi trường
- .NET 10 SDK
- PostgreSQL (hoặc Docker chạy Postgres)

## Hướng dẫn cài đặt & nạp database

1. Cập nhật chuỗi kết nối trong `appsettings.json` của `HIS.WinForms` (hoặc `HIS.WebApi`) theo PostgreSQL trên máy bạn.
2. Nạp database và dữ liệu mẫu bằng một lệnh (tự tạo database nếu chưa có, chạy lặp lại an toàn):
```bash
psql -h localhost -p 5432 -U postgres -d postgres -f src/HIS.DAL/Seed/setup_database.sql
```

`setup_database.sql` lần lượt nạp `schema.sql` (sinh từ migration), `reception_seed.sql` và `auth_seed.sql`. Muốn dùng tên database khác `hospital_his` thì thêm `-v db_name=ten_database` và sửa `appsettings.json` tương ứng. Khi có migration mới, tạo lại `schema.sql`:

```bash
dotnet ef migrations script --idempotent --project src/HIS.DAL --startup-project src/HIS.DAL --context AppDbContext -o src/HIS.DAL/Seed/schema.sql
```

Có thể dùng EF migration thay thế, nhưng lệnh dưới hiện có thể báo `PendingModelChangesWarning` do chênh kiểu timestamp giữa model và snapshot:

```bash
dotnet ef database update --project src/HIS.DAL --startup-project src/HIS.WinForms
```

> Lỗi "Không kết nối được cơ sở dữ liệu" ở màn hình đăng nhập thường do database chưa tồn tại hoặc sai mật khẩu trong `appsettings.json`.

## Đăng nhập & phân quyền (HIS-19)

Ứng dụng `HIS.WinForms` mở form đăng nhập trước khi vào màn hình chính. Mật khẩu lưu dạng BCrypt; sai 5 lần liên tiếp thì tài khoản bị khóa 5 phút. Mỗi lần đăng nhập/đăng xuất được ghi vào bảng `access_logs`.

### Tài khoản demo

Nếu database đã có schema và chỉ cần nạp lại dữ liệu mẫu:

```bash
psql -h localhost -p 5432 -U postgres -d hospital_his -f src/HIS.DAL/Seed/reception_seed.sql
psql -h localhost -p 5432 -U postgres -d hospital_his -f src/HIS.DAL/Seed/auth_seed.sql
```

| Vai trò | Tên đăng nhập | Mật khẩu |
|---|---|---|
| Quản trị viên (ADMIN) | `admin` | `Admin@123` |
| Tiếp đón (RECEPTIONIST) | `td.tranthivan` | `TiepDon@123` |
| Bác sĩ (DOCTOR) | `bs.tranthutrang`, `bs.nguyenvanan`, `bs.leminhchau`, `bs.phamthuyduong`, `bs.dovanhung`, `bs.nguyenthilan` | `BacSi@123` |
| Bệnh nhân (PATIENT) | `bn0001` … `bn0024` | `BenhNhan@123` |

`auth_seed.sql` đặt lại mật khẩu, mở khóa và bật lại các tài khoản demo nên có thể chạy lặp lại khi cần. Tài khoản bệnh nhân chỉ dành cho cổng web, ứng dụng quản trị sẽ từ chối đăng nhập.

### Ma trận phân quyền

| Chức năng | ADMIN | RECEPTIONIST | DOCTOR | PATIENT |
|---|:-:|:-:|:-:|:-:|
| Bàn làm việc | ✓ | ✓ | ✓ | Đăng nhập bị chặn |
| Tiếp đón | ✓ | ✓ | – | – |
| Quản lý bệnh nhân | ✓ | ✓ | ✓ | – |
| Khám ngoại trú | ✓ | – | ✓ | – |
| Quản lý nội trú | ✓ | – | – | – |
| Dược & kho thuốc | ✓ | – | – | – |
| Báo cáo thống kê | ✓ | – | – | – |
| Quản lý hệ thống | ✓ | – | – | – |

Menu không có quyền sẽ bị ẩn. Nếu chức năng được gọi bằng đường khác (phím tắt, liên kết nhanh), ứng dụng vẫn chặn và báo "Bạn không có quyền truy cập chức năng này.". Quy tắc nằm ở `RolePermissions` và `AuthService` trong `HIS.BLL`.
