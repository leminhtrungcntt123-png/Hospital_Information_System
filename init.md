Tôi cần bạn dựng khung (scaffold) cho một dự án .NET 10 tên là HIS 
(Hospital Information System - Hệ thống hỗ trợ đặt lịch khám và quản lý hồ sơ bệnh án).
Đây là đồ án môn học, dùng cho 2 môn: Lập trình trực quan (WinForms) và Lập trình Web,
dùng chung 1 tầng nghiệp vụ. Làm theo ĐÚNG cấu trúc và yêu cầu dưới đây, không tự ý đổi.

## 1. CẤU TRÚC SOLUTION

Tạo cấu trúc thư mục và file .csproj như sau (dùng `dotnet new sln` và `dotnet new <template>`):

HIS/
├─ HIS.sln
├─ .gitignore
├─ README.md
├─ .github/workflows/ci.yml
├─ src/
│ ├─ HIS.Domain/ (Class Library, .NET 10)
│ ├─ HIS.DAL/ (Class Library, .NET 10)
│ ├─ HIS.BLL/ (Class Library, .NET 10)
│ ├─ HIS.WinForms/ (Windows Forms App, .NET 10, chỉ chạy trên Windows)
│ ├─ HIS.WebApi/ (ASP.NET Core Web API, .NET 10)
│ └─ HIS.Web/ (ASP.NET Core Web App - Razor Pages, .NET 10)
└─ tests/
└─ HIS.Tests/ (xUnit Test Project, .NET 10)


Thiết lập Project Reference (dùng `dotnet add reference`):
- HIS.DAL        → tham chiếu HIS.Domain
- HIS.BLL        → tham chiếu HIS.DAL, HIS.Domain
- HIS.WinForms   → tham chiếu HIS.BLL
- HIS.WebApi     → tham chiếu HIS.BLL
- HIS.Tests      → tham chiếu HIS.BLL, HIS.DAL, HIS.Domain

QUAN TRỌNG: HIS.BLL và HIS.Domain tuyệt đối KHÔNG được tham chiếu ngược lại 
HIS.WinForms, HIS.WebApi hay HIS.Web. HIS.Web chỉ gọi HIS.WebApi qua HTTP 
(HttpClient), không thêm Project Reference giữa HIS.Web và HIS.WebApi.

## 2. NUGET PACKAGE CẦN CÀI

- HIS.DAL: Microsoft.EntityFrameworkCore, Npgsql.EntityFrameworkCore.PostgreSQL, 
  Microsoft.EntityFrameworkCore.Design, EFCore.NamingConventions
- HIS.BLL: Microsoft.Extensions.DependencyInjection.Abstractions, 
  FluentValidation, BCrypt.Net-Next
- HIS.WinForms: Microsoft.Extensions.DependencyInjection, 
  Microsoft.Extensions.Configuration.Json
- HIS.WebApi: Npgsql.EntityFrameworkCore.PostgreSQL, Swashbuckle.AspNetCore
- HIS.Tests: xunit, xunit.runner.visualstudio, Moq, 
  Microsoft.EntityFrameworkCore.InMemory

## 3. DATABASE: PostgreSQL, snake_case

Dùng PostgreSQL (Npgsql). DbContext đặt tên `AppDbContext` trong HIS.DAL.
Cấu hình để EF Core tự map tên bảng/cột sang snake_case bằng 
EFCore.NamingConventions, vì class C# dùng PascalCase nhưng bảng Postgres 
phải là snake_case:

```csharp
optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
```

## 4. TẠO CÁC ENTITY SAU TRONG HIS.Domain (PascalCase, tiếng Anh)

Tạo class C# tương ứng cho từng bảng dưới đây (mỗi bảng 1 file .cs riêng, 
namespace HIS.Domain). Property kiểu dữ liệu hợp lý với PostgreSQL 
(DateTime cho TIMESTAMP, decimal cho NUMERIC, string cho VARCHAR/TEXT, 
int cho INTEGER, bool cho BOOLEAN). Thêm navigation property (quan hệ) hợp lý 
giữa các entity dựa theo khóa ngoại.
- roles(Id,code name,decsription),
- service_types(Id,code,name),
- appointment_statuses(id, code ,name,sort_order,is_terminal),
- Account (Id, Username, PasswordHash, roleID, IsActive, FailedLoginCount, 
  LockedUntil, CreatedAt, UpdatedAt)
- Department (Id, Name, Description)
- ClinicRoom (Id, DepartmentId, RoomName, RoomNumber)
- Patient (Id, AccountId, FullName, DateOfBirth, Gender, NationalId, 
  PhoneNumber, Address, InsuranceCardNumber, CreatedAt)
- Doctor (Id, AccountId, DepartmentId, FullName, Specialty, LicenseNumber, 
  AvgConsultationMinutes)
- Nurse (Id, AccountId, DepartmentId, FullName)
- DepartmentHead (Id, AccountId, DepartmentId, FullName)
- Medication (Id, Name, ActiveIngredient, Strength, Unit, Price, IsActive)
- Service (Id, Name, service_typeId, Price, IsActive)
- InsurancePlan (Id, Name, CoveragePct, ReferralType)
- DoctorSchedule (Id, DoctorId, RoomId, DayOfWeek, Shift, StartTime, 
  EndTime, SlotDurationMinutes, EffectiveFrom, EffectiveTo, IsActive)
- ScheduleException (Id, DoctorId, Date, Shift, Type, ReplacementDoctorId, 
  Reason, ApprovedBy, ApprovedAt, CreatedAt)
- AppointmentSlot (Id, DoctorId, RoomId, StartTime, EndTime, Capacity, 
  BookedCount, Status, Version)
- Appointment (Id, AppointmentCode, PatientId, SlotId, RoomId, StatusID, 
  BookingChannel, VisitType, PriorityLevel, QueueNumber, Reason, 
  HoldUntil, ParentAppointmentId, CreatedAt, UpdatedAt)
- AppointmentStatusHistory (Id, AppointmentId, OldAppointment_StatusId, NewApointment_StatusId, 
  PerformedBy, ChangedAt)
- VitalSigns (Id, AppointmentId, Pulse, SystolicBp, DiastolicBp, 
  Temperature, RespiratoryRate, Spo2, Weight, Height, RecordedBy, RecordedAt)
- MedicalRecord (Id, AppointmentId, DoctorId, Symptoms, ClinicalExamination, 
  PrimaryDiagnosisCode, PrimaryDiagnosisDesc, SecondaryDiagnosis, 
  Conclusion, CreatedAt, UpdatedAt)
- LabOrder (Id, MedicalRecordId, ServiceId, Note, Status, CreatedAt)
- LabResult (Id, OrderId, Result, PerformedBy, RecordedAt)
- Prescription (Id, MedicalRecordId, PrescribedDate, Note)
- PrescriptionItem (Id, PrescriptionId, MedicationId, Dosage, Quantity, 
  DurationDays, UsageInstructions)
- Invoice (Id, AppointmentId, PatientId, InsurancePlanId, TotalAmount, 
  InsuranceCoveredAmount, PatientPaidAmount, Status, PaymentMethod, 
  CashierId, CreatedAt, PaidAt)
- InvoiceItem (Id, InvoiceId, Category, Description, Amount)
- AccessLog (Id, AccountId, MedicalRecordId, Action, OccurredAt, IpAddress)

## 5. CẤU HÌNH TRONG AppDbContext (HIS.DAL)

Viết AppDbContext với DbSet cho tất cả entity trên. Trong OnModelCreating,
cấu hình Fluent API (KHÔNG dùng Data Annotation) cho các ràng buộc sau:

- Unique constraint: Account.Username, Patient.AccountId, Doctor.AccountId, 
  Nurse.AccountId, DepartmentHead.AccountId, DepartmentHead.DepartmentId, 
  Appointment.AppointmentCode, AppointmentSlot(DoctorId, StartTime) composite unique
- Check constraint cho các cột enum-string (Account.Role, Appointment.Status, 
  AppointmentSlot.Status, v.v. — dùng HasCheckConstraint với đúng danh sách 
  giá trị đã liệt kê ở schema SQL bên dưới)
- Default value: CreatedAt = now(), IsActive = true, Status mặc định 
  tương ứng từng bảng
- Concurrency token: AppointmentSlot.Version dùng IsConcurrencyToken()

Tham khảo đúng schema SQL đầy đủ sau để lấy chính xác kiểu dữ liệu, 
độ dài VARCHAR, và danh sách giá trị CHECK constraint:
-- =====================================================
-- HIS DATABASE SCHEMA — PostgreSQL
-- Hospital Information System
-- =====================================================
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
-- ─── LOOKUP TABLES (ĐẶT ĐẦU FILE) ────────────────────

CREATE TABLE roles (
    id          INTEGER PRIMARY KEY,
    code        VARCHAR(20) NOT NULL UNIQUE,    -- 'ADMIN', 'DOCTOR'...
    name        VARCHAR(50) NOT NULL,           -- 'Quản trị viên'
    description VARCHAR(255)
);

CREATE TABLE service_types (
    id          INTEGER PRIMARY KEY,
    code        VARCHAR(20) NOT NULL UNIQUE,    -- 'LAB_TEST', 'EXAMINATION', 'IMAGING'
    name        VARCHAR(50) NOT NULL            -- 'Xét nghiệm'
);

CREATE TABLE appointment_statuses (
    id          INTEGER PRIMARY KEY,
    code        VARCHAR(20) NOT NULL UNIQUE,    -- 'CONFIRMED'
    name        VARCHAR(50) NOT NULL,           -- 'Đã xác nhận'
    sort_order  INTEGER NOT NULL,
    is_terminal BOOLEAN NOT NULL DEFAULT FALSE
);

-- ─── ACCOUNTS & STAFF ────────────────────────────────

CREATE TABLE accounts (
    id                  INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    username            VARCHAR(50) NOT NULL UNIQUE,
    password_hash       VARCHAR(255) NOT NULL,
    role_id             INTEGER NOT NULL REFERENCES roles(id),
    is_active           BOOLEAN NOT NULL DEFAULT TRUE,
    failed_login_count  INTEGER NOT NULL DEFAULT 0,
    locked_until        TIMESTAMP NULL,
    created_at          TIMESTAMP NOT NULL DEFAULT now(),
    updated_at          TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE departments (
    id          INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name        VARCHAR(100) NOT NULL,
    description VARCHAR(255)
);

CREATE TABLE clinic_rooms (
    id              INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    department_id   INTEGER NOT NULL REFERENCES departments(id),
    room_name       VARCHAR(50) NOT NULL,
    room_number     VARCHAR(20) NOT NULL
);

CREATE TABLE patients (
    id                        INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    account_id                INTEGER NOT NULL UNIQUE REFERENCES accounts(id),
    full_name                 VARCHAR(100) NOT NULL,
    date_of_birth             DATE NOT NULL,
    gender                    VARCHAR(10) CHECK (gender IN ('MALE','FEMALE','OTHER')),
    national_id               VARCHAR(20) UNIQUE,
    phone_number              VARCHAR(15) NOT NULL,
    address                   VARCHAR(255),
    insurance_card_number     VARCHAR(20),
    created_at                TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE doctors (
    id                          INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    account_id                  INTEGER NOT NULL UNIQUE REFERENCES accounts(id),
    department_id               INTEGER NOT NULL REFERENCES departments(id),
    full_name                   VARCHAR(100) NOT NULL,
    specialty                   VARCHAR(100),
    license_number              VARCHAR(30),          -- Số chứng chỉ hành nghề
    avg_consultation_minutes    INTEGER NOT NULL DEFAULT 15  -- Dùng tính ETA hàng đợi
);

CREATE TABLE nurses (
    id              INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    account_id      INTEGER NOT NULL UNIQUE REFERENCES accounts(id),
    department_id   INTEGER REFERENCES departments(id),
    full_name       VARCHAR(100) NOT NULL
);

CREATE TABLE department_heads (
    id              INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    account_id      INTEGER NOT NULL UNIQUE REFERENCES accounts(id),
    department_id   INTEGER NOT NULL UNIQUE REFERENCES departments(id),
    full_name       VARCHAR(100) NOT NULL
);

-- ─── CATALOGS (ADMIN-MANAGED) ────────────────────────

CREATE TABLE medications (
    id                  INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name                VARCHAR(150) NOT NULL,
    active_ingredient   VARCHAR(150),
    strength            VARCHAR(50),
    unit                VARCHAR(20) NOT NULL,       -- viên, chai, ống...
    price               NUMERIC(12,2) NOT NULL DEFAULT 0,
    is_active           BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE services (
    id              INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name            VARCHAR(150) NOT NULL,
    service_type_id INTEGER NOT NULL REFERENCES service_types(id),
    price           NUMERIC(12,2) NOT NULL DEFAULT 0,
    is_active       BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE insurance_plans (
    id              INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name            VARCHAR(100) NOT NULL,       -- VD: "Đúng tuyến - 80%"
    coverage_pct    NUMERIC(5,2) NOT NULL,        -- 80.00 / 95.00 / 100.00
    referral_type   VARCHAR(20) CHECK (referral_type IN ('IN_NETWORK','OUT_OF_NETWORK'))
);

-- ─── SCHEDULING ENGINE ────────────────────────────────

CREATE TABLE doctor_schedules (
    id                      INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    doctor_id               INTEGER NOT NULL REFERENCES doctors(id),
    room_id                 INTEGER NOT NULL REFERENCES clinic_rooms(id),
    day_of_week             INTEGER CHECK (day_of_week BETWEEN 1 AND 7), -- 1=CN..7=T7
    shift                   VARCHAR(10) NOT NULL CHECK (shift IN ('MORNING','AFTERNOON')),
    start_time              TIME NOT NULL,
    end_time                TIME NOT NULL,
    slot_duration_minutes   INTEGER NOT NULL DEFAULT 15,
    effective_from          DATE NOT NULL,
    effective_to            DATE,
    is_active               BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE schedule_exceptions (
    id                      INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    doctor_id               INTEGER NOT NULL REFERENCES doctors(id),
    date                    DATE NOT NULL,
    shift                   VARCHAR(10) CHECK (shift IN ('MORNING','AFTERNOON', NULL)),
    type                    VARCHAR(20) NOT NULL
                            CHECK (type IN ('LEAVE','TRAINING','SURGERY','SHIFT_SWAP')),
    replacement_doctor_id   INTEGER REFERENCES doctors(id),
    reason                  VARCHAR(255),
    approved_by             INTEGER REFERENCES accounts(id),
    approved_at             TIMESTAMP,
    created_at              TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE appointment_slots (
    id              INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    doctor_id       INTEGER NOT NULL REFERENCES doctors(id),
    room_id         INTEGER NOT NULL REFERENCES clinic_rooms(id),
    start_time      TIMESTAMP NOT NULL,
    end_time        TIMESTAMP NOT NULL,
    capacity        INTEGER NOT NULL DEFAULT 1,
    booked_count    INTEGER NOT NULL DEFAULT 0,
    status          VARCHAR(15) NOT NULL DEFAULT 'AVAILABLE'
                    CHECK (status IN ('AVAILABLE','FULL','BLOCKED')),
    version         INTEGER NOT NULL DEFAULT 0,    -- Optimistic locking
    CONSTRAINT uq_slot UNIQUE (doctor_id, start_time),
    CONSTRAINT ck_capacity CHECK (booked_count <= capacity)
);

CREATE TABLE appointments (
    id                      INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    appointment_code        VARCHAR(30) NOT NULL UNIQUE,      -- HIS-20261015-0042
    patient_id              INTEGER NOT NULL REFERENCES patients(id),
    slot_id                 INTEGER REFERENCES appointment_slots(id),   -- NULL nếu walk-in chưa gán slot
    room_id                 INTEGER REFERENCES clinic_rooms(id),
    status_id               INTEGER NOT NULL REFERENCES appointment_statuses(id),
    booking_channel         VARCHAR(15) NOT NULL DEFAULT 'WEB'
                            CHECK (booking_channel IN ('WEB','ON_SITE')),
    visit_type              VARCHAR(15) NOT NULL DEFAULT 'SCHEDULED'
                            CHECK (visit_type IN ('SCHEDULED','WALK_IN')),
    priority_level          INTEGER NOT NULL DEFAULT 0 CHECK (priority_level BETWEEN 0 AND 3),
    queue_number            VARCHAR(10),          -- "A01" / "B03"
    reason                  VARCHAR(255),
    hold_until              TIMESTAMP,             -- Hạn giữ chỗ PENDING (BR-05)
    parent_appointment_id   INTEGER REFERENCES appointments(id),  -- Dùng khi dời lịch
    created_at              TIMESTAMP NOT NULL DEFAULT now(),
    updated_at              TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE appointment_status_history (
    id              INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    appointment_id  INTEGER NOT NULL REFERENCES appointments(id),
    old_status_id   INTEGER REFERENCES appointment_statuses(id),
    new_status_id   INTEGER NOT NULL REFERENCES appointment_statuses(id),
    performed_by    INTEGER REFERENCES accounts(id),
    changed_at      TIMESTAMP NOT NULL DEFAULT now()
);

-- ─── CLINICAL & MEDICAL RECORDS ───────────────────────

CREATE TABLE vital_signs (
    id                  INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    appointment_id      INTEGER NOT NULL UNIQUE REFERENCES appointments(id),
    pulse               INTEGER,
    systolic_bp         INTEGER,
    diastolic_bp        INTEGER,
    temperature         NUMERIC(4,1),
    respiratory_rate    INTEGER,
    spo2                INTEGER,
    weight              NUMERIC(5,2),
    height              NUMERIC(5,2),
    recorded_by         INTEGER NOT NULL REFERENCES nurses(id),
    recorded_at         TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE medical_records (
    id                          INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    appointment_id              INTEGER NOT NULL UNIQUE REFERENCES appointments(id),
    doctor_id                   INTEGER NOT NULL REFERENCES doctors(id),
    symptoms                    TEXT,
    clinical_examination        TEXT,
    primary_diagnosis_code      VARCHAR(20),   -- Mã ICD-10
    primary_diagnosis_desc      VARCHAR(255),
    secondary_diagnosis         TEXT,
    conclusion                  TEXT,
    created_at                  TIMESTAMP NOT NULL DEFAULT now(),
    updated_at                  TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE lab_orders (
    id                  INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    medical_record_id   INTEGER NOT NULL REFERENCES medical_records(id),
    service_id          INTEGER NOT NULL REFERENCES services(id),
    note                VARCHAR(255),
    status              VARCHAR(20) NOT NULL DEFAULT 'PENDING'
                        CHECK (status IN ('PENDING','COMPLETED')),
    created_at          TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE lab_results (
    id              INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    order_id        INTEGER NOT NULL UNIQUE REFERENCES lab_orders(id),
    result          TEXT,
    performed_by    INTEGER REFERENCES accounts(id),
    recorded_at     TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE prescriptions (
    id                  INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    medical_record_id   INTEGER NOT NULL REFERENCES medical_records(id),
    prescribed_date     DATE NOT NULL DEFAULT CURRENT_DATE,
    note                VARCHAR(255)
);

CREATE TABLE prescription_items (
    id                  INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    prescription_id     INTEGER NOT NULL REFERENCES prescriptions(id),
    medication_id       INTEGER NOT NULL REFERENCES medications(id),
    dosage              VARCHAR(100),        -- "2 viên/lần, 2 lần/ngày"
    quantity            INTEGER NOT NULL,
    duration_days       INTEGER,
    usage_instructions  VARCHAR(255)         -- "uống sau ăn"
);

-- ─── BILLING ───────────────────────────────────────────

CREATE TABLE invoices (
    id                          INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    appointment_id              INTEGER NOT NULL UNIQUE REFERENCES appointments(id),
    patient_id                  INTEGER NOT NULL REFERENCES patients(id),
    insurance_plan_id           INTEGER REFERENCES insurance_plans(id),
    total_amount                NUMERIC(12,2) NOT NULL DEFAULT 0,
    insurance_covered_amount    NUMERIC(12,2) NOT NULL DEFAULT 0,
    patient_paid_amount         NUMERIC(12,2) NOT NULL DEFAULT 0,
    status                      VARCHAR(20) NOT NULL DEFAULT 'UNPAID'
                                CHECK (status IN ('UNPAID','PAID')),
    payment_method              VARCHAR(20) CHECK (payment_method IN ('CASH','BANK_TRANSFER', NULL)),
    cashier_id                  INTEGER REFERENCES accounts(id),
    created_at                  TIMESTAMP NOT NULL DEFAULT now(),
    paid_at                     TIMESTAMP
);

CREATE TABLE invoice_items (
    id          INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    invoice_id  INTEGER NOT NULL REFERENCES invoices(id),
    category    VARCHAR(20) NOT NULL CHECK (category IN ('EXAMINATION','MEDICATION','SERVICE')),
    description VARCHAR(255) NOT NULL,
    amount      NUMERIC(12,2) NOT NULL
);

-- ─── SYSTEM ────────────────────────────────────────────

CREATE TABLE access_logs (
    id                  INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    account_id          INTEGER NOT NULL REFERENCES accounts(id),
    medical_record_id   INTEGER REFERENCES medical_records(id),
    action              VARCHAR(50) NOT NULL,   -- VD: "VIEW_RECORD", "EDIT_RECORD"
    occurred_at         TIMESTAMP NOT NULL DEFAULT now(),
    ip_address          VARCHAR(45)
);

-- ─── INDEXES ───────────────────────────────────────────

CREATE INDEX idx_slots_doctor_date ON appointment_slots (doctor_id, start_time);
CREATE INDEX idx_appointments_patient ON appointments (patient_id);
CREATE INDEX idx_appointments_status_id ON appointments (status_id);
CREATE INDEX idx_records_appointment ON medical_records (appointment_id);
CREATE INDEX idx_access_logs_account ON access_logs (account_id);
## 6. DEPENDENCY INJECTION

Trong HIS.WinForms/Program.cs: dựng Host (Generic Host hoặc 
ServiceCollection thủ công), đọc connection string từ appsettings.json, 
đăng ký AppDbContext (AddDbContext) và tạo sẵn 1 interface rỗng mẫu 
IBookingService + class BookingService rỗng trong HIS.BLL để minh họa 
cách đăng ký AddScoped<IBookingService, BookingService>(). Form chính 
(MainForm) nhận IBookingService qua constructor injection.

Trong HIS.WebApi/Program.cs: làm tương tự theo kiểu ASP.NET Core 
(builder.Services.AddDbContext, AddScoped), thêm Swagger.

appsettings.json ở cả WinForms và WebApi có connection string PostgreSQL dạng:
"Host=localhost;Port=5432;Database=his_db;Username=postgres;Password=..."
Đặt placeholder, không hardcode password thật — ghi chú dùng User Secrets 
cho local dev.

## 7. CI: .github/workflows/ci.yml

Tạo GitHub Actions workflow: chạy trên push và pull_request, dùng 
runs-on: windows-latest (vì có WinForms), setup .NET 10, restore, 
build toàn bộ solution, chạy dotnet test cho HIS.Tests.

## 8. .gitignore

Dùng .gitignore chuẩn cho Visual Studio/.NET (bin/, obj/, .vs/, 
*.user, appsettings.Development.json nếu chứa secret).

## 9. SAU KHI SCAFFOLD XONG

Chạy `dotnet ef migrations add InitialCreate --project src/HIS.DAL 
--startup-project src/HIS.WinForms` để tạo migration đầu tiên dựa trên 
các entity vừa tạo, rồi báo cho tôi kết quả, KHÔNG tự ý chạy 
`dotnet ef database update` (tôi sẽ tự chạy khi đã cấu hình xong 
PostgreSQL local).

Làm từng bước, báo tôi biết sau mỗi bước lớn (cấu trúc solution xong, 
entity xong, DbContext xong, DI xong, CI xong) trước khi sang bước tiếp theo.