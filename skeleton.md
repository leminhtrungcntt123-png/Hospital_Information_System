HIS/                              ← 1 repo Git duy nhất, 1 folder gốc
│
├─ HIS.sln                        ← file Solution, mở bằng Visual Studio, thấy hết mọi project
├─ .gitignore
├─ README.md
│
├─ .github/
│   └─ workflows/
│       └─ ci.yml                 ← file CI đã nói ở câu trước
│
├─ src/                           ← chứa toàn bộ code
│   │
│   ├─ HIS.Domain/                ← Entity (Patient, Doctor, Appointment...)
│   │   ├─ HIS.Domain.csproj
│   │   ├─ Patient.cs
│   │   ├─ Doctor.cs
│   │   └─ ...
│   │
│   ├─ HIS.DAL/                   ← EF Core, DbContext, Migration
│   │   ├─ HIS.DAL.csproj
│   │   ├─ AppDbContext.cs
│   │   └─ Migrations/
│   │
│   ├─ HIS.BLL/                   ← Service, Business Rule (dùng CHUNG cho Web + WinForms)
│   │   ├─ HIS.BLL.csproj
│   │   ├─ IBookingService.cs
│   │   ├─ BookingService.cs
│   │   └─ ...
│   │
│   ├─ HIS.WinForms/              ← Ứng dụng desktop (môn Lập trình trực quan)
│   │   ├─ HIS.WinForms.csproj
│   │   ├─ Program.cs
│   │   ├─ Forms/
│   │   │   ├─ Admin/
│   │   │   ├─ BacSi/
│   │   │   └─ YTaLeTan/
│   │   └─ ...
│   │
│   ├─ HIS.WebApi/                ← API mỏng, gọi xuống HIS.BLL
│   │   ├─ HIS.WebApi.csproj
│   │   ├─ Program.cs
│   │   └─ Controllers/
│   │       └─ AppointmentsController.cs
│   │
│   └─ HIS.Web/                   ← Trang web cho bệnh nhân (môn Lập trình web)
│       ├─ HIS.Web.csproj
│       ├─ Program.cs
│       └─ Pages/
│
└─ tests/
    └─ HIS.Tests/
        ├─ HIS.Tests.csproj
        └─ BookingServiceTests.cs