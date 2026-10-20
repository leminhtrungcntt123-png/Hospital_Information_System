using Microsoft.EntityFrameworkCore;
using HIS.Domain;

namespace HIS.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
    public DbSet<AppointmentStatus> AppointmentStatuses => Set<AppointmentStatus>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<ClinicRoom> ClinicRooms => Set<ClinicRoom>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Nurse> Nurses => Set<Nurse>();
    public DbSet<DepartmentHead> DepartmentHeads => Set<DepartmentHead>();
    public DbSet<Medication> Medications => Set<Medication>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<InsurancePlan> InsurancePlans => Set<InsurancePlan>();
    public DbSet<DoctorSchedule> DoctorSchedules => Set<DoctorSchedule>();
    public DbSet<ScheduleException> ScheduleExceptions => Set<ScheduleException>();
    public DbSet<AppointmentSlot> AppointmentSlots => Set<AppointmentSlot>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentStatusHistory> AppointmentStatusHistories => Set<AppointmentStatusHistory>();
    public DbSet<VitalSigns> VitalSigns => Set<VitalSigns>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<LabOrder> LabOrders => Set<LabOrder>();
    public DbSet<LabResult> LabResults => Set<LabResult>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionItem> PrescriptionItems => Set<PrescriptionItem>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<AccessLog> AccessLogs => Set<AccessLog>();

    // CSDL thật dùng 'timestamp without time zone' cho mọi cột datetime
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>()
            .HaveColumnType("timestamp without time zone");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 0a. Role (lookup table — id do người dùng seed, không phải identity)
        modelBuilder.Entity<Role>(b =>
        {
            b.ToTable("roles");
            b.HasKey(r => r.Id);
            b.Property(r => r.Id).ValueGeneratedNever();
            b.Property(r => r.Code).HasMaxLength(20).IsRequired();
            b.HasIndex(r => r.Code).IsUnique();
            b.Property(r => r.Name).HasMaxLength(50).IsRequired();
            b.Property(r => r.Description).HasMaxLength(255);
        });

        // 0b. ServiceType (lookup table)
        modelBuilder.Entity<ServiceType>(b =>
        {
            b.ToTable("service_types");
            b.HasKey(st => st.Id);
            b.Property(st => st.Id).ValueGeneratedNever();
            b.Property(st => st.Code).HasMaxLength(20).IsRequired();
            b.HasIndex(st => st.Code).IsUnique();
            b.Property(st => st.Name).HasMaxLength(50).IsRequired();
        });

        // 0c. AppointmentStatus (lookup table)
        modelBuilder.Entity<AppointmentStatus>(b =>
        {
            b.ToTable("appointment_statuses");
            b.HasKey(s => s.Id);
            b.Property(s => s.Id).ValueGeneratedNever();
            b.Property(s => s.Code).HasMaxLength(20).IsRequired();
            b.HasIndex(s => s.Code).IsUnique();
            b.Property(s => s.Name).HasMaxLength(50).IsRequired();
            b.Property(s => s.SortOrder).IsRequired();
            b.Property(s => s.IsTerminal).HasDefaultValue(false);
        });

        // 1. Account
        modelBuilder.Entity<Account>(b =>
        {
            b.ToTable("accounts");

            b.HasKey(a => a.Id);
            b.Property(a => a.Username).HasMaxLength(50).IsRequired();
            b.HasIndex(a => a.Username).IsUnique();

            b.Property(a => a.PasswordHash).HasMaxLength(255).IsRequired();
            b.Property(a => a.RoleId).IsRequired();
            b.Property(a => a.IsActive).HasDefaultValue(true);
            b.Property(a => a.FailedLoginCount).HasDefaultValue(0);
            b.Property(a => a.CreatedAt).HasDefaultValueSql("now()");
            b.Property(a => a.UpdatedAt).HasDefaultValueSql("now()");

            b.HasOne(a => a.Role)
                .WithMany(r => r.Accounts)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 2. Department
        modelBuilder.Entity<Department>(b =>
        {
            b.ToTable("departments");
            b.HasKey(d => d.Id);
            b.Property(d => d.Name).HasMaxLength(100).IsRequired();
            b.Property(d => d.Description).HasMaxLength(255);
        });

        // 3. ClinicRoom
        modelBuilder.Entity<ClinicRoom>(b =>
        {
            b.ToTable("clinic_rooms");
            b.HasKey(r => r.Id);
            b.Property(r => r.RoomName).HasMaxLength(50).IsRequired();
            b.Property(r => r.RoomNumber).HasMaxLength(20).IsRequired();

            b.HasOne(r => r.Department)
                .WithMany(d => d.ClinicRooms)
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 4. Patient
        modelBuilder.Entity<Patient>(b =>
        {
            b.ToTable("patients", t =>
            {
                t.HasCheckConstraint("ck_patients_gender", "gender IS NULL OR gender IN ('MALE','FEMALE','OTHER')");
            });

            b.HasKey(p => p.Id);
            b.Property(p => p.FullName).HasMaxLength(100).IsRequired();
            b.Property(p => p.Gender).HasMaxLength(10);
            b.Property(p => p.NationalId).HasMaxLength(20);
            b.HasIndex(p => p.NationalId).IsUnique();
            b.Property(p => p.PhoneNumber).HasMaxLength(15).IsRequired();
            b.Property(p => p.Address).HasMaxLength(255);
            b.Property(p => p.InsuranceCardNumber).HasMaxLength(20);
            b.Property(p => p.CreatedAt).HasDefaultValueSql("now()");

            b.HasOne(p => p.Account)
                .WithOne(a => a.Patient)
                .HasForeignKey<Patient>(p => p.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(p => p.AccountId).IsUnique();
        });

        // 5. Doctor
        modelBuilder.Entity<Doctor>(b =>
        {
            b.ToTable("doctors");
            b.HasKey(d => d.Id);
            b.Property(d => d.FullName).HasMaxLength(100).IsRequired();
            b.Property(d => d.Specialty).HasMaxLength(100);
            b.Property(d => d.LicenseNumber).HasMaxLength(30);
            b.Property(d => d.AvgConsultationMinutes).HasDefaultValue(15);

            b.HasOne(d => d.Account)
                .WithOne(a => a.Doctor)
                .HasForeignKey<Doctor>(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(d => d.AccountId).IsUnique();

            b.HasOne(d => d.Department)
                .WithMany(dep => dep.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 6. Nurse
        modelBuilder.Entity<Nurse>(b =>
        {
            b.ToTable("nurses");
            b.HasKey(n => n.Id);
            b.Property(n => n.FullName).HasMaxLength(100).IsRequired();

            b.HasOne(n => n.Account)
                .WithOne(a => a.Nurse)
                .HasForeignKey<Nurse>(n => n.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(n => n.AccountId).IsUnique();

            b.HasOne(n => n.Department)
                .WithMany(d => d.Nurses)
                .HasForeignKey(n => n.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // 7. DepartmentHead
        modelBuilder.Entity<DepartmentHead>(b =>
        {
            b.ToTable("department_heads");
            b.HasKey(dh => dh.Id);
            b.Property(dh => dh.FullName).HasMaxLength(100).IsRequired();

            b.HasOne(dh => dh.Account)
                .WithOne(a => a.DepartmentHead)
                .HasForeignKey<DepartmentHead>(dh => dh.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(dh => dh.AccountId).IsUnique();

            b.HasOne(dh => dh.Department)
                .WithOne(d => d.DepartmentHead)
                .HasForeignKey<DepartmentHead>(dh => dh.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(dh => dh.DepartmentId).IsUnique();
        });

        // 8. Medication
        modelBuilder.Entity<Medication>(b =>
        {
            b.ToTable("medications");
            b.HasKey(m => m.Id);
            b.Property(m => m.Name).HasMaxLength(150).IsRequired();
            b.Property(m => m.ActiveIngredient).HasMaxLength(150);
            b.Property(m => m.Strength).HasMaxLength(50);
            b.Property(m => m.Unit).HasMaxLength(20).IsRequired();
            b.Property(m => m.Price).HasPrecision(12, 2).HasDefaultValue(0m);
            b.Property(m => m.IsActive).HasDefaultValue(true);
        });

        // 9. Service
        modelBuilder.Entity<Service>(b =>
        {
            b.ToTable("services");

            b.HasKey(s => s.Id);
            b.Property(s => s.Name).HasMaxLength(150).IsRequired();
            b.Property(s => s.ServiceTypeId).IsRequired();
            b.Property(s => s.Price).HasPrecision(12, 2).HasDefaultValue(0m);
            b.Property(s => s.IsActive).HasDefaultValue(true);

            b.HasOne(s => s.ServiceType)
                .WithMany(st => st.Services)
                .HasForeignKey(s => s.ServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 10. InsurancePlan
        modelBuilder.Entity<InsurancePlan>(b =>
        {
            b.ToTable("insurance_plans", t =>
            {
                t.HasCheckConstraint("ck_insurance_plans_referral_type", "referral_type IS NULL OR referral_type IN ('IN_NETWORK','OUT_OF_NETWORK')");
            });

            b.HasKey(ip => ip.Id);
            b.Property(ip => ip.Name).HasMaxLength(100).IsRequired();
            b.Property(ip => ip.CoveragePct).HasPrecision(5, 2).IsRequired();
            b.Property(ip => ip.ReferralType).HasMaxLength(20);
        });

        // 11. DoctorSchedule
        modelBuilder.Entity<DoctorSchedule>(b =>
        {
            b.ToTable("doctor_schedules", t =>
            {
                t.HasCheckConstraint("ck_doctor_schedules_day_of_week", "day_of_week IS NULL OR (day_of_week BETWEEN 1 AND 7)");
                t.HasCheckConstraint("ck_doctor_schedules_shift", "shift IN ('MORNING','AFTERNOON')");
            });

            b.HasKey(ds => ds.Id);
            b.Property(ds => ds.Shift).HasMaxLength(10).IsRequired();
            b.Property(ds => ds.SlotDurationMinutes).HasDefaultValue(15);
            b.Property(ds => ds.IsActive).HasDefaultValue(true);

            b.HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorSchedules)
                .HasForeignKey(ds => ds.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(ds => ds.Room)
                .WithMany(r => r.DoctorSchedules)
                .HasForeignKey(ds => ds.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 12. ScheduleException
        modelBuilder.Entity<ScheduleException>(b =>
        {
            b.ToTable("schedule_exceptions", t =>
            {
                t.HasCheckConstraint("ck_schedule_exceptions_shift", "shift IS NULL OR shift IN ('MORNING','AFTERNOON')");
                t.HasCheckConstraint("ck_schedule_exceptions_type", "type IN ('LEAVE','TRAINING','SURGERY','SHIFT_SWAP')");
            });

            b.HasKey(se => se.Id);
            b.Property(se => se.Shift).HasMaxLength(10);
            b.Property(se => se.Type).HasMaxLength(20).IsRequired();
            b.Property(se => se.Reason).HasMaxLength(255);
            b.Property(se => se.CreatedAt).HasDefaultValueSql("now()");

            b.HasOne(se => se.Doctor)
                .WithMany(d => d.ScheduleExceptions)
                .HasForeignKey(se => se.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(se => se.ReplacementDoctor)
                .WithMany(d => d.ReplacementScheduleExceptions)
                .HasForeignKey(se => se.ReplacementDoctorId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(se => se.Approver)
                .WithMany(a => a.ApprovedScheduleExceptions)
                .HasForeignKey(se => se.ApprovedBy)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // 13. AppointmentSlot
        modelBuilder.Entity<AppointmentSlot>(b =>
        {
            b.ToTable("appointment_slots", t =>
            {
                t.HasCheckConstraint("ck_appointment_slots_status", "status IN ('AVAILABLE','FULL','BLOCKED')");
                t.HasCheckConstraint("ck_capacity", "booked_count <= capacity");
            });

            b.HasKey(s => s.Id);
            b.Property(s => s.Capacity).HasDefaultValue(1);
            b.Property(s => s.BookedCount).HasDefaultValue(0);
            b.Property(s => s.Status).HasMaxLength(15).HasDefaultValue("AVAILABLE").IsRequired();
            b.Property(s => s.Version).HasDefaultValue(0).IsConcurrencyToken();

            b.HasOne(s => s.Doctor)
                .WithMany(d => d.AppointmentSlots)
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(s => s.Room)
                .WithMany(r => r.AppointmentSlots)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(s => new { s.DoctorId, s.StartTime })
                .IsUnique()
                .HasDatabaseName("uq_slot");
        });

        // 14. Appointment
        modelBuilder.Entity<Appointment>(b =>
        {
            b.ToTable("appointments", t =>
            {
                t.HasCheckConstraint("ck_appointments_booking_channel", "booking_channel IN ('WEB','ON_SITE')");
                t.HasCheckConstraint("ck_appointments_visit_type", "visit_type IN ('SCHEDULED','WALK_IN')");
                t.HasCheckConstraint("ck_appointments_priority_level", "priority_level BETWEEN 0 AND 3");
            });

            b.HasKey(a => a.Id);
            b.Property(a => a.AppointmentCode).HasMaxLength(30).IsRequired();
            b.HasIndex(a => a.AppointmentCode).IsUnique();

            b.Property(a => a.StatusId).IsRequired();
            b.Property(a => a.BookingChannel).HasMaxLength(15).HasDefaultValue("WEB").IsRequired();
            b.Property(a => a.VisitType).HasMaxLength(15).HasDefaultValue("SCHEDULED").IsRequired();
            b.Property(a => a.PriorityLevel).HasDefaultValue(0);
            b.Property(a => a.QueueNumber).HasMaxLength(10);
            b.Property(a => a.Reason).HasMaxLength(255);
            b.Property(a => a.CreatedAt).HasDefaultValueSql("now()");
            b.Property(a => a.UpdatedAt).HasDefaultValueSql("now()");

            b.HasOne(a => a.Status)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(a => a.Slot)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.SlotId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(a => a.Room)
                .WithMany(r => r.Appointments)
                .HasForeignKey(a => a.RoomId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(a => a.ParentAppointment)
                .WithMany(pa => pa.ChildAppointments)
                .HasForeignKey(a => a.ParentAppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(a => a.PatientId).HasDatabaseName("idx_appointments_patient");
            b.HasIndex(a => a.StatusId).HasDatabaseName("idx_appointments_status_id");
        });

        // 15. AppointmentStatusHistory
        modelBuilder.Entity<AppointmentStatusHistory>(b =>
        {
            b.ToTable("appointment_status_history");
            b.HasKey(h => h.Id);
            b.Property(h => h.OldStatusId);
            b.Property(h => h.NewStatusId).IsRequired();
            b.Property(h => h.ChangedAt).HasDefaultValueSql("now()");

            b.HasOne(h => h.OldStatus)
                .WithMany(s => s.OldStatusHistories)
                .HasForeignKey(h => h.OldStatusId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(h => h.NewStatus)
                .WithMany(s => s.NewStatusHistories)
                .HasForeignKey(h => h.NewStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(h => h.Appointment)
                .WithMany(a => a.StatusHistories)
                .HasForeignKey(h => h.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(h => h.Performer)
                .WithMany(a => a.PerformedAppointmentHistories)
                .HasForeignKey(h => h.PerformedBy)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // 16. VitalSigns
        modelBuilder.Entity<VitalSigns>(b =>
        {
            b.ToTable("vital_signs");
            b.HasKey(v => v.Id);
            b.Property(v => v.Temperature).HasPrecision(4, 1);
            b.Property(v => v.Weight).HasPrecision(5, 2);
            b.Property(v => v.Height).HasPrecision(5, 2);
            b.Property(v => v.RecordedAt).HasDefaultValueSql("now()");

            b.HasOne(v => v.Appointment)
                .WithOne(a => a.VitalSigns)
                .HasForeignKey<VitalSigns>(v => v.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(v => v.AppointmentId).IsUnique();

            b.HasOne(v => v.Recorder)
                .WithMany(n => n.RecordedVitalSigns)
                .HasForeignKey(v => v.RecordedBy)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 17. MedicalRecord
        modelBuilder.Entity<MedicalRecord>(b =>
        {
            b.ToTable("medical_records");
            b.HasKey(m => m.Id);
            b.Property(m => m.Symptoms).HasColumnType("text");
            b.Property(m => m.ClinicalExamination).HasColumnType("text");
            b.Property(m => m.PrimaryDiagnosisCode).HasMaxLength(20);
            b.Property(m => m.PrimaryDiagnosisDesc).HasMaxLength(255);
            b.Property(m => m.SecondaryDiagnosis).HasColumnType("text");
            b.Property(m => m.Conclusion).HasColumnType("text");
            b.Property(m => m.CreatedAt).HasDefaultValueSql("now()");
            b.Property(m => m.UpdatedAt).HasDefaultValueSql("now()");

            b.HasOne(m => m.Appointment)
                .WithOne(a => a.MedicalRecord)
                .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(m => m.AppointmentId).IsUnique().HasDatabaseName("idx_records_appointment");

            b.HasOne(m => m.Doctor)
                .WithMany(d => d.MedicalRecords)
                .HasForeignKey(m => m.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 18. LabOrder
        modelBuilder.Entity<LabOrder>(b =>
        {
            b.ToTable("lab_orders", t =>
            {
                t.HasCheckConstraint("ck_lab_orders_status", "status IN ('PENDING','COMPLETED')");
            });

            b.HasKey(o => o.Id);
            b.Property(o => o.Note).HasMaxLength(255);
            b.Property(o => o.Status).HasMaxLength(20).HasDefaultValue("PENDING").IsRequired();
            b.Property(o => o.CreatedAt).HasDefaultValueSql("now()");

            b.HasOne(o => o.MedicalRecord)
                .WithMany(m => m.LabOrders)
                .HasForeignKey(o => o.MedicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(o => o.Service)
                .WithMany(s => s.LabOrders)
                .HasForeignKey(o => o.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 19. LabResult
        modelBuilder.Entity<LabResult>(b =>
        {
            b.ToTable("lab_results");
            b.HasKey(r => r.Id);
            b.Property(r => r.Result).HasColumnType("text");
            b.Property(r => r.RecordedAt).HasDefaultValueSql("now()");

            b.HasOne(r => r.Order)
                .WithOne(o => o.Result)
                .HasForeignKey<LabResult>(r => r.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(r => r.OrderId).IsUnique();

            b.HasOne(r => r.Performer)
                .WithMany(a => a.PerformedLabResults)
                .HasForeignKey(r => r.PerformedBy)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // 20. Prescription
        modelBuilder.Entity<Prescription>(b =>
        {
            b.ToTable("prescriptions");
            b.HasKey(p => p.Id);
            b.Property(p => p.PrescribedDate).HasDefaultValueSql("CURRENT_DATE");
            b.Property(p => p.Note).HasMaxLength(255);

            b.HasOne(p => p.MedicalRecord)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(p => p.MedicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // 21. PrescriptionItem
        modelBuilder.Entity<PrescriptionItem>(b =>
        {
            b.ToTable("prescription_items");
            b.HasKey(pi => pi.Id);
            b.Property(pi => pi.Dosage).HasMaxLength(100);
            b.Property(pi => pi.UsageInstructions).HasMaxLength(255);

            b.HasOne(pi => pi.Prescription)
                .WithMany(p => p.PrescriptionItems)
                .HasForeignKey(pi => pi.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(pi => pi.Medication)
                .WithMany(m => m.PrescriptionItems)
                .HasForeignKey(pi => pi.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 22. Invoice
        modelBuilder.Entity<Invoice>(b =>
        {
            b.ToTable("invoices", t =>
            {
                t.HasCheckConstraint("ck_invoices_status", "status IN ('UNPAID','PAID')");
                t.HasCheckConstraint("ck_invoices_payment_method", "payment_method IS NULL OR payment_method IN ('CASH','BANK_TRANSFER')");
            });

            b.HasKey(i => i.Id);
            b.Property(i => i.TotalAmount).HasPrecision(12, 2).HasDefaultValue(0m);
            b.Property(i => i.InsuranceCoveredAmount).HasPrecision(12, 2).HasDefaultValue(0m);
            b.Property(i => i.PatientPaidAmount).HasPrecision(12, 2).HasDefaultValue(0m);
            b.Property(i => i.Status).HasMaxLength(20).HasDefaultValue("UNPAID").IsRequired();
            b.Property(i => i.PaymentMethod).HasMaxLength(20);
            b.Property(i => i.CreatedAt).HasDefaultValueSql("now()");

            b.HasOne(i => i.Appointment)
                .WithOne(a => a.Invoice)
                .HasForeignKey<Invoice>(i => i.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(i => i.AppointmentId).IsUnique();

            b.HasOne(i => i.Patient)
                .WithMany(p => p.Invoices)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(i => i.InsurancePlan)
                .WithMany(ip => ip.Invoices)
                .HasForeignKey(i => i.InsurancePlanId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(i => i.Cashier)
                .WithMany(a => a.CashierInvoices)
                .HasForeignKey(i => i.CashierId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // 23. InvoiceItem
        modelBuilder.Entity<InvoiceItem>(b =>
        {
            b.ToTable("invoice_items", t =>
            {
                t.HasCheckConstraint("ck_invoice_items_category", "category IN ('EXAMINATION','MEDICATION','SERVICE')");
            });

            b.HasKey(ii => ii.Id);
            b.Property(ii => ii.Category).HasMaxLength(20).IsRequired();
            b.Property(ii => ii.Description).HasMaxLength(255).IsRequired();
            b.Property(ii => ii.Amount).HasPrecision(12, 2).IsRequired();

            b.HasOne(ii => ii.Invoice)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // 24. AccessLog
        modelBuilder.Entity<AccessLog>(b =>
        {
            b.ToTable("access_logs");
            b.HasKey(al => al.Id);
            b.Property(al => al.Action).HasMaxLength(50).IsRequired();
            b.Property(al => al.OccurredAt).HasDefaultValueSql("now()");
            b.Property(al => al.IpAddress).HasMaxLength(45);

            b.HasOne(al => al.Account)
                .WithMany(a => a.AccessLogs)
                .HasForeignKey(al => al.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(al => al.MedicalRecord)
                .WithMany(m => m.AccessLogs)
                .HasForeignKey(al => al.MedicalRecordId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasIndex(al => al.AccountId).HasDatabaseName("idx_access_logs_account");
        });
    }
}
