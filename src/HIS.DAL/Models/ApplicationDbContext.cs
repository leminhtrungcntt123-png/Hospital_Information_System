using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HIS.DAL.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessLog> AccessLogs { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentSlot> AppointmentSlots { get; set; }

    public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

    public virtual DbSet<AppointmentStatusHistory> AppointmentStatusHistories { get; set; }

    public virtual DbSet<ClinicRoom> ClinicRooms { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentHead> DepartmentHeads { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }

    public virtual DbSet<InsurancePlan> InsurancePlans { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<LabOrder> LabOrders { get; set; }

    public virtual DbSet<LabResult> LabResults { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<Nurse> Nurses { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionItem> PrescriptionItems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ScheduleException> ScheduleExceptions { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<VitalSign> VitalSigns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("access_logs_pkey");

            entity.ToTable("access_logs");

            entity.HasIndex(e => e.AccountId, "idx_access_logs_account");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.OccurredAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("occurred_at");

            entity.HasOne(d => d.Account).WithMany(p => p.AccessLogs)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("access_logs_account_id_fkey");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.AccessLogs)
                .HasForeignKey(d => d.MedicalRecordId)
                .HasConstraintName("access_logs_medical_record_id_fkey");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accounts_pkey");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.Username, "accounts_username_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FailedLoginCount).HasColumnName("failed_login_count");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LockedUntil)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("locked_until");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accounts_role_id_fkey");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("appointments_pkey");

            entity.ToTable("appointments");

            entity.HasIndex(e => e.AppointmentCode, "appointments_appointment_code_key").IsUnique();

            entity.HasIndex(e => e.PatientId, "idx_appointments_patient");

            entity.HasIndex(e => e.StatusId, "idx_appointments_status_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AppointmentCode)
                .HasMaxLength(30)
                .HasColumnName("appointment_code");
            entity.Property(e => e.BookingChannel)
                .HasMaxLength(15)
                .HasDefaultValueSql("'WEB'::character varying")
                .HasColumnName("booking_channel");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.HoldUntil)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("hold_until");
            entity.Property(e => e.ParentAppointmentId).HasColumnName("parent_appointment_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PriorityLevel).HasColumnName("priority_level");
            entity.Property(e => e.QueueNumber)
                .HasMaxLength(10)
                .HasColumnName("queue_number");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("reason");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.VisitType)
                .HasMaxLength(15)
                .HasDefaultValueSql("'SCHEDULED'::character varying")
                .HasColumnName("visit_type");

            entity.HasOne(d => d.ParentAppointment).WithMany(p => p.InverseParentAppointment)
                .HasForeignKey(d => d.ParentAppointmentId)
                .HasConstraintName("appointments_parent_appointment_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_patient_id_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("appointments_room_id_fkey");

            entity.HasOne(d => d.Slot).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.SlotId)
                .HasConstraintName("appointments_slot_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_status_id_fkey");
        });

        modelBuilder.Entity<AppointmentSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("appointment_slots_pkey");

            entity.ToTable("appointment_slots");

            entity.HasIndex(e => new { e.DoctorId, e.StartTime }, "idx_slots_doctor_date");

            entity.HasIndex(e => new { e.DoctorId, e.StartTime }, "uq_slot").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BookedCount).HasColumnName("booked_count");
            entity.Property(e => e.Capacity)
                .HasDefaultValue(1)
                .HasColumnName("capacity");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_time");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValueSql("'AVAILABLE'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Version).HasColumnName("version");

            entity.HasOne(d => d.Doctor).WithMany(p => p.AppointmentSlots)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointment_slots_doctor_id_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.AppointmentSlots)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointment_slots_room_id_fkey");
        });

        modelBuilder.Entity<AppointmentStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("appointment_statuses_pkey");

            entity.ToTable("appointment_statuses");

            entity.HasIndex(e => e.Code, "appointment_statuses_code_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.IsTerminal).HasColumnName("is_terminal");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
        });

        modelBuilder.Entity<AppointmentStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("appointment_status_history_pkey");

            entity.ToTable("appointment_status_history");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("changed_at");
            entity.Property(e => e.NewStatusId).HasColumnName("new_status_id");
            entity.Property(e => e.OldStatusId).HasColumnName("old_status_id");
            entity.Property(e => e.PerformedBy).HasColumnName("performed_by");

            entity.HasOne(d => d.Appointment).WithMany(p => p.AppointmentStatusHistories)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointment_status_history_appointment_id_fkey");

            entity.HasOne(d => d.NewStatus).WithMany(p => p.AppointmentStatusHistoryNewStatuses)
                .HasForeignKey(d => d.NewStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointment_status_history_new_status_id_fkey");

            entity.HasOne(d => d.OldStatus).WithMany(p => p.AppointmentStatusHistoryOldStatuses)
                .HasForeignKey(d => d.OldStatusId)
                .HasConstraintName("appointment_status_history_old_status_id_fkey");

            entity.HasOne(d => d.PerformedByNavigation).WithMany(p => p.AppointmentStatusHistories)
                .HasForeignKey(d => d.PerformedBy)
                .HasConstraintName("appointment_status_history_performed_by_fkey");
        });

        modelBuilder.Entity<ClinicRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clinic_rooms_pkey");

            entity.ToTable("clinic_rooms");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.RoomName)
                .HasMaxLength(50)
                .HasColumnName("room_name");
            entity.Property(e => e.RoomNumber)
                .HasMaxLength(20)
                .HasColumnName("room_number");

            entity.HasOne(d => d.Department).WithMany(p => p.ClinicRooms)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clinic_rooms_department_id_fkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("departments_pkey");

            entity.ToTable("departments");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DepartmentHead>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("department_heads_pkey");

            entity.ToTable("department_heads");

            entity.HasIndex(e => e.AccountId, "department_heads_account_id_key").IsUnique();

            entity.HasIndex(e => e.DepartmentId, "department_heads_department_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");

            entity.HasOne(d => d.Account).WithOne(p => p.DepartmentHead)
                .HasForeignKey<DepartmentHead>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("department_heads_account_id_fkey");

            entity.HasOne(d => d.Department).WithOne(p => p.DepartmentHead)
                .HasForeignKey<DepartmentHead>(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("department_heads_department_id_fkey");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doctors_pkey");

            entity.ToTable("doctors");

            entity.HasIndex(e => e.AccountId, "doctors_account_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AvgConsultationMinutes)
                .HasDefaultValue(15)
                .HasColumnName("avg_consultation_minutes");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(30)
                .HasColumnName("license_number");
            entity.Property(e => e.Specialty)
                .HasMaxLength(100)
                .HasColumnName("specialty");

            entity.HasOne(d => d.Account).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctors_account_id_fkey");

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctors_department_id_fkey");
        });

        modelBuilder.Entity<DoctorSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doctor_schedules_pkey");

            entity.ToTable("doctor_schedules");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.EffectiveFrom).HasColumnName("effective_from");
            entity.Property(e => e.EffectiveTo).HasColumnName("effective_to");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Shift)
                .HasMaxLength(10)
                .HasColumnName("shift");
            entity.Property(e => e.SlotDurationMinutes)
                .HasDefaultValue(15)
                .HasColumnName("slot_duration_minutes");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSchedules)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_schedules_doctor_id_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.DoctorSchedules)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_schedules_room_id_fkey");
        });

        modelBuilder.Entity<InsurancePlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("insurance_plans_pkey");

            entity.ToTable("insurance_plans");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CoveragePct)
                .HasPrecision(5, 2)
                .HasColumnName("coverage_pct");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ReferralType)
                .HasMaxLength(20)
                .HasColumnName("referral_type");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoices_pkey");

            entity.ToTable("invoices");

            entity.HasIndex(e => e.AppointmentId, "invoices_appointment_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.CashierId).HasColumnName("cashier_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.InsuranceCoveredAmount)
                .HasPrecision(12, 2)
                .HasColumnName("insurance_covered_amount");
            entity.Property(e => e.InsurancePlanId).HasColumnName("insurance_plan_id");
            entity.Property(e => e.PaidAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("paid_at");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PatientPaidAmount)
                .HasPrecision(12, 2)
                .HasColumnName("patient_paid_amount");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .HasColumnName("payment_method");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'UNPAID'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(12, 2)
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Appointment).WithOne(p => p.Invoice)
                .HasForeignKey<Invoice>(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoices_appointment_id_fkey");

            entity.HasOne(d => d.Cashier).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CashierId)
                .HasConstraintName("invoices_cashier_id_fkey");

            entity.HasOne(d => d.InsurancePlan).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.InsurancePlanId)
                .HasConstraintName("invoices_insurance_plan_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoices_patient_id_fkey");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoice_items_pkey");

            entity.ToTable("invoice_items");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(12, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Category)
                .HasMaxLength(20)
                .HasColumnName("category");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_items_invoice_id_fkey");
        });

        modelBuilder.Entity<LabOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lab_orders_pkey");

            entity.ToTable("lab_orders");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDING'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.LabOrders)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lab_orders_medical_record_id_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.LabOrders)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lab_orders_service_id_fkey");
        });

        modelBuilder.Entity<LabResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lab_results_pkey");

            entity.ToTable("lab_results");

            entity.HasIndex(e => e.OrderId, "lab_results_order_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PerformedBy).HasColumnName("performed_by");
            entity.Property(e => e.RecordedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("recorded_at");
            entity.Property(e => e.Result).HasColumnName("result");

            entity.HasOne(d => d.Order).WithOne(p => p.LabResult)
                .HasForeignKey<LabResult>(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lab_results_order_id_fkey");

            entity.HasOne(d => d.PerformedByNavigation).WithMany(p => p.LabResults)
                .HasForeignKey(d => d.PerformedBy)
                .HasConstraintName("lab_results_performed_by_fkey");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("medical_records_pkey");

            entity.ToTable("medical_records");

            entity.HasIndex(e => e.AppointmentId, "idx_records_appointment");

            entity.HasIndex(e => e.AppointmentId, "medical_records_appointment_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.ClinicalExamination).HasColumnName("clinical_examination");
            entity.Property(e => e.Conclusion).HasColumnName("conclusion");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.PrimaryDiagnosisCode)
                .HasMaxLength(20)
                .HasColumnName("primary_diagnosis_code");
            entity.Property(e => e.PrimaryDiagnosisDesc)
                .HasMaxLength(255)
                .HasColumnName("primary_diagnosis_desc");
            entity.Property(e => e.SecondaryDiagnosis).HasColumnName("secondary_diagnosis");
            entity.Property(e => e.Symptoms).HasColumnName("symptoms");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Appointment).WithOne(p => p.MedicalRecord)
                .HasForeignKey<MedicalRecord>(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_records_appointment_id_fkey");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_records_doctor_id_fkey");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("medications_pkey");

            entity.ToTable("medications");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ActiveIngredient)
                .HasMaxLength(150)
                .HasColumnName("active_ingredient");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.Strength)
                .HasMaxLength(50)
                .HasColumnName("strength");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<Nurse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nurses_pkey");

            entity.ToTable("nurses");

            entity.HasIndex(e => e.AccountId, "nurses_account_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");

            entity.HasOne(d => d.Account).WithOne(p => p.Nurse)
                .HasForeignKey<Nurse>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nurses_account_id_fkey");

            entity.HasOne(d => d.Department).WithMany(p => p.Nurses)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("nurses_department_id_fkey");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patients_pkey");

            entity.ToTable("patients");

            entity.HasIndex(e => e.AccountId, "patients_account_id_key").IsUnique();

            entity.HasIndex(e => e.NationalId, "patients_national_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.InsuranceCardNumber)
                .HasMaxLength(20)
                .HasColumnName("insurance_card_number");
            entity.Property(e => e.NationalId)
                .HasMaxLength(20)
                .HasColumnName("national_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Account).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patients_account_id_fkey");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prescriptions_pkey");

            entity.ToTable("prescriptions");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.PrescribedDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("prescribed_date");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescriptions_medical_record_id_fkey");
        });

        modelBuilder.Entity<PrescriptionItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prescription_items_pkey");

            entity.ToTable("prescription_items");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Dosage)
                .HasMaxLength(100)
                .HasColumnName("dosage");
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
            entity.Property(e => e.MedicationId).HasColumnName("medication_id");
            entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UsageInstructions)
                .HasMaxLength(255)
                .HasColumnName("usage_instructions");

            entity.HasOne(d => d.Medication).WithMany(p => p.PrescriptionItems)
                .HasForeignKey(d => d.MedicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescription_items_medication_id_fkey");

            entity.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionItems)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescription_items_prescription_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Code, "roles_code_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ScheduleException>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("schedule_exceptions_pkey");

            entity.ToTable("schedule_exceptions");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ApprovedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("approved_at");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("reason");
            entity.Property(e => e.ReplacementDoctorId).HasColumnName("replacement_doctor_id");
            entity.Property(e => e.Shift)
                .HasMaxLength(10)
                .HasColumnName("shift");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.ScheduleExceptions)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("schedule_exceptions_approved_by_fkey");

            entity.HasOne(d => d.Doctor).WithMany(p => p.ScheduleExceptionDoctors)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("schedule_exceptions_doctor_id_fkey");

            entity.HasOne(d => d.ReplacementDoctor).WithMany(p => p.ScheduleExceptionReplacementDoctors)
                .HasForeignKey(d => d.ReplacementDoctorId)
                .HasConstraintName("schedule_exceptions_replacement_doctor_id_fkey");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("services_pkey");

            entity.ToTable("services");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
        });

        modelBuilder.Entity<VitalSign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vital_signs_pkey");

            entity.ToTable("vital_signs");

            entity.HasIndex(e => e.AppointmentId, "vital_signs_appointment_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.DiastolicBp).HasColumnName("diastolic_bp");
            entity.Property(e => e.Height)
                .HasPrecision(5, 2)
                .HasColumnName("height");
            entity.Property(e => e.Pulse).HasColumnName("pulse");
            entity.Property(e => e.RecordedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("recorded_at");
            entity.Property(e => e.RecordedBy).HasColumnName("recorded_by");
            entity.Property(e => e.RespiratoryRate).HasColumnName("respiratory_rate");
            entity.Property(e => e.Spo2).HasColumnName("spo2");
            entity.Property(e => e.SystolicBp).HasColumnName("systolic_bp");
            entity.Property(e => e.Temperature)
                .HasPrecision(4, 1)
                .HasColumnName("temperature");
            entity.Property(e => e.Weight)
                .HasPrecision(5, 2)
                .HasColumnName("weight");

            entity.HasOne(d => d.Appointment).WithOne(p => p.VitalSign)
                .HasForeignKey<VitalSign>(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vital_signs_appointment_id_fkey");

            entity.HasOne(d => d.RecordedByNavigation).WithMany(p => p.VitalSigns)
                .HasForeignKey(d => d.RecordedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vital_signs_recorded_by_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
