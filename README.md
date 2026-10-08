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
- .NET 8 SDK
- PostgreSQL (hoặc Docker chạy Postgres)

## Hướng dẫn cài đặt & chạy Migration

1. Cập nhật chuỗi kết nối trong `appsettings.json` của `HIS.WinForms` hoặc `HIS.WebApi` (hoặc cấu hình User Secrets).
2. Chạy Migration cập nhật Database:
```bash
dotnet ef database update --project src/HIS.DAL --startup-project src/HIS.WinForms
```
