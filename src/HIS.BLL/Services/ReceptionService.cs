using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL.Services;

public class ReceptionService : IReceptionService
{
    private static readonly string[] WaitingCodes = { AppointmentStatus.Codes.Pending, AppointmentStatus.Codes.Confirmed };
    private static readonly string[] ReceivedCodes = { AppointmentStatus.Codes.CheckedIn, AppointmentStatus.Codes.InProgress, AppointmentStatus.Codes.Completed };
    private static readonly string[] CancelledCodes = { AppointmentStatus.Codes.Cancelled };

    private readonly AppDbContext _context;

    public ReceptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReceptionCountsDto> GetCountsAsync(CancellationToken cancellationToken = default)
    {
        var statusMap = await LoadStatusMapAsync(cancellationToken);
        var waitingIds = ResolveIds(statusMap, WaitingCodes);
        var receivedIds = ResolveIds(statusMap, ReceivedCodes);
        var cancelledIds = ResolveIds(statusMap, CancelledCodes);

        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
        IQueryable<Appointment> todayAppointments = InDay(_context.Appointments.AsNoTracking(), today);

        int waiting = waitingIds.Count == 0
            ? 0
            : await todayAppointments.CountAsync(a => waitingIds.Contains(a.StatusId), cancellationToken);

        int received = receivedIds.Count == 0
            ? 0
            : await todayAppointments.CountAsync(a => receivedIds.Contains(a.StatusId), cancellationToken);

        int scheduled = waitingIds.Count == 0
            ? 0
            : await _context.Appointments.AsNoTracking()
                .CountAsync(a => a.Slot != null
                                 && a.Slot.StartTime >= tomorrow
                                 && waitingIds.Contains(a.StatusId), cancellationToken);

        int cancelled = cancelledIds.Count == 0
            ? 0
            : await todayAppointments.CountAsync(a => cancelledIds.Contains(a.StatusId), cancellationToken);

        return new ReceptionCountsDto(waiting, received, scheduled, cancelled);
    }

    public async Task<IReadOnlyList<ReceptionItemDto>> GetReceptionListAsync(
        ReceptionTab tab,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        var statusMap = await LoadStatusMapAsync(cancellationToken);

        List<int> statusIds = tab switch
        {
            ReceptionTab.Waiting => ResolveIds(statusMap, WaitingCodes),
            ReceptionTab.Received => ResolveIds(statusMap, ReceivedCodes),
            ReceptionTab.Scheduled => ResolveIds(statusMap, WaitingCodes),
            ReceptionTab.Cancelled => ResolveIds(statusMap, CancelledCodes),
            _ => new List<int>()
        };

        if (statusIds.Count == 0)
        {
            return Array.Empty<ReceptionItemDto>();
        }

        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);

        IQueryable<Appointment> query = _context.Appointments
            .AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Status)
            .Include(a => a.Room)
                .ThenInclude(r => r!.Department)
            .Include(a => a.Slot)
                .ThenInclude(s => s!.Doctor)
            .Include(a => a.Slot)
                .ThenInclude(s => s!.Room)
                    .ThenInclude(r => r!.Department);

        query = tab == ReceptionTab.Scheduled
            ? query.Where(a => a.Slot != null && a.Slot.StartTime >= tomorrow)
            : InDay(query, today);

        query = query.Where(a => statusIds.Contains(a.StatusId));

        if (!string.IsNullOrWhiteSpace(search))
        {
            string keyword = search.Trim();
            string pattern = $"%{keyword}%";
            int? patientIdFromCode = TryParsePatientCode(keyword);

            query = query.Where(a =>
                EF.Functions.ILike(a.AppointmentCode, pattern) ||
                EF.Functions.ILike(a.Patient.FullName, pattern) ||
                EF.Functions.ILike(a.Patient.PhoneNumber, pattern) ||
                (a.Patient.NationalId != null && EF.Functions.ILike(a.Patient.NationalId, pattern)) ||
                (a.Patient.InsuranceCardNumber != null && EF.Functions.ILike(a.Patient.InsuranceCardNumber, pattern)) ||
                (patientIdFromCode != null && a.PatientId == patientIdFromCode));
        }

        List<Appointment> rows = await query.ToListAsync(cancellationToken);

        List<ReceptionItemDto> items = rows.Select(a => MapItem(a)).ToList();

        IEnumerable<ReceptionItemDto> ordered = tab switch
        {
            ReceptionTab.Waiting => items
                .OrderByDescending(i => i.PriorityLevel)
                .ThenBy(i => i.AppointmentTime),
            ReceptionTab.Received => items.OrderByDescending(i => i.AppointmentTime),
            ReceptionTab.Scheduled => items.OrderBy(i => i.AppointmentTime),
            ReceptionTab.Cancelled => items.OrderByDescending(i => i.AppointmentTime),
            _ => items
        };

        return ordered.ToList();
    }

    public async Task<ReceptionPatientDto?> FindPatientAsync(string keyword, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return null;
        }

        string normalized = keyword.Trim();
        int? patientIdFromCode = TryParsePatientCode(normalized);

        Patient? patient = null;

        if (patientIdFromCode is int patientId)
        {
            patient = await _context.Patients.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken);
        }

        patient ??= await _context.Patients.AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.PhoneNumber == normalized ||
                p.NationalId == normalized ||
                p.InsuranceCardNumber == normalized, cancellationToken);

        patient ??= await _context.Patients.AsNoTracking()
            .FirstOrDefaultAsync(p => EF.Functions.ILike(p.FullName, normalized), cancellationToken);

        return patient == null ? null : MapPatient(patient);
    }

    public async Task<IReadOnlyList<DepartmentOptionDto>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .OrderBy(d => d.Name)
            .Select(d => new DepartmentOptionDto(d.Id, d.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<DoctorOptionDto>> GetDoctorsAsync(int? departmentId = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Doctor> query = _context.Doctors.AsNoTracking();

        if (departmentId is int id && id > 0)
        {
            query = query.Where(d => d.DepartmentId == id);
        }

        return await query
            .OrderBy(d => d.FullName)
            .Select(d => new DoctorOptionDto(d.Id, d.DepartmentId, d.FullName, d.Specialty ?? string.Empty))
            .ToListAsync(cancellationToken);
    }

    public async Task<CreateReceptionResult> CreateReceptionAsync(
        CreateReceptionRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateRequest(request);

        DateTime now = DateTime.Now;
        string fullName = request.FullName.Trim();
        string phoneNumber = NormalizePhone(request.PhoneNumber);
        string? nationalId = NullIfWhiteSpace(request.NationalId);
        string? address = NullIfWhiteSpace(request.Address);
        string? insuranceCard = NullIfWhiteSpace(request.InsuranceCardNumber);

        Department department = await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == request.DepartmentId, cancellationToken)
            ?? throw new ArgumentException("Khoa/phòng đã chọn không tồn tại.");

        Doctor? doctor = null;
        if (request.DoctorId is int doctorId)
        {
            doctor = await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == doctorId && d.DepartmentId == department.Id, cancellationToken)
                ?? throw new ArgumentException("Bác sĩ đã chọn không thuộc khoa/phòng này.");
        }

        int? roomId = null;
        if (doctor != null)
        {
            roomId = await _context.DoctorSchedules
                .AsNoTracking()
                .Where(s => s.DoctorId == doctor.Id && s.IsActive)
                .OrderBy(s => s.Id)
                .Select(s => (int?)s.RoomId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        roomId ??= await _context.ClinicRooms
            .AsNoTracking()
            .Where(r => r.DepartmentId == department.Id)
            .OrderBy(r => r.Id)
            .Select(r => (int?)r.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (roomId == null)
        {
            throw new InvalidOperationException("Khoa đã chọn chưa được cấu hình phòng khám.");
        }

        var statusMap = await LoadStatusMapAsync(cancellationToken);
        if (!statusMap.TryGetValue(AppointmentStatus.Codes.CheckedIn, out int checkedInStatusId))
        {
            throw new InvalidOperationException("Cơ sở dữ liệu chưa có trạng thái CHECKED_IN. Vui lòng nạp dữ liệu danh mục trạng thái.");
        }

        Role patientRole = await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Code == "PATIENT", cancellationToken)
            ?? throw new InvalidOperationException("Cơ sở dữ liệu chưa có vai trò PATIENT. Vui lòng nạp dữ liệu danh mục vai trò.");

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            Patient patient;
            bool isNewPatient = false;

            if (request.PatientId is int existingPatientId)
            {
                patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Id == existingPatientId, cancellationToken)
                    ?? throw new ArgumentException("Không tìm thấy hồ sơ bệnh nhân đã chọn.");

                patient.PhoneNumber = phoneNumber;
                patient.Address = address;
                patient.InsuranceCardNumber = insuranceCard;
            }
            else
            {
                Patient? duplicate = null;
                if (nationalId != null)
                {
                    duplicate = await _context.Patients
                        .FirstOrDefaultAsync(p => p.NationalId == nationalId, cancellationToken);
                }

                if (duplicate != null)
                {
                    patient = duplicate;
                    patient.PhoneNumber = phoneNumber;
                    patient.Address = address;
                    patient.InsuranceCardNumber = insuranceCard;
                }
                else
                {
                    var account = new Account
                    {
                        Username = await GenerateUsernameAsync(cancellationToken),
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString("N")),
                        RoleId = patientRole.Id,
                        IsActive = true,
                        CreatedAt = now,
                        UpdatedAt = now
                    };

                    patient = new Patient
                    {
                        Account = account,
                        FullName = fullName,
                        DateOfBirth = request.DateOfBirth,
                        Gender = request.Gender,
                        NationalId = nationalId,
                        PhoneNumber = phoneNumber,
                        Address = address,
                        InsuranceCardNumber = insuranceCard,
                        CreatedAt = now
                    };

                    _context.Patients.Add(patient);
                    isNewPatient = true;
                }
            }

            string queueNumber = await GenerateQueueNumberAsync(DateTime.Today, cancellationToken);

            var appointment = new Appointment
            {
                AppointmentCode = await GenerateAppointmentCodeAsync(cancellationToken),
                Patient = patient,
                StatusId = checkedInStatusId,
                BookingChannel = BookingChannelCodes.OnSite,
                VisitType = "WALK_IN",
                PriorityLevel = Math.Clamp(request.PriorityLevel, 0, 3),
                QueueNumber = queueNumber,
                Reason = Truncate(NullIfWhiteSpace(request.Reason), 255),
                RoomId = roomId,
                CreatedAt = now,
                UpdatedAt = now
            };

            appointment.StatusHistories.Add(new AppointmentStatusHistory
            {
                NewStatusId = checkedInStatusId,
                ChangedAt = now
            });

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new CreateReceptionResult(
                appointment.Id,
                appointment.AppointmentCode,
                patient.Id,
                patient.Id.ToPatientCode(),
                queueNumber,
                isNewPatient);
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw new InvalidOperationException(
                "Không thể lưu hồ sơ tiếp nhận. Số CCCD hoặc thông tin bệnh nhân có thể đã tồn tại.",
                ex);
        }
    }

    public async Task<ReceptionActionResult> CheckInAsync(int appointmentId, CancellationToken cancellationToken = default)
    {
        var statusMap = await LoadStatusMapAsync(cancellationToken);
        if (!statusMap.TryGetValue(AppointmentStatus.Codes.CheckedIn, out int checkedInStatusId))
        {
            throw new InvalidOperationException("Cơ sở dữ liệu chưa có trạng thái CHECKED_IN.");
        }

        Appointment? appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Status)
            .Include(a => a.Slot)
            .FirstOrDefaultAsync(a => a.Id == appointmentId, cancellationToken);

        if (appointment == null)
        {
            return new ReceptionActionResult(false, "Không tìm thấy lượt khám cần tiếp nhận.");
        }

        if (appointment.Status.Code is not (AppointmentStatus.Codes.Pending or AppointmentStatus.Codes.Confirmed))
        {
            return new ReceptionActionResult(
                false,
                $"Lượt khám {appointment.AppointmentCode} đang ở trạng thái \"{appointment.Status.Name}\", không thể tiếp nhận.");
        }

        DateTime now = DateTime.Now;
        string queueNumber = appointment.QueueNumber ?? string.Empty;

        if (string.IsNullOrWhiteSpace(queueNumber))
        {
            queueNumber = await GenerateQueueNumberAsync(DateTime.Today, cancellationToken);
            appointment.QueueNumber = queueNumber;
        }

        int oldStatusId = appointment.StatusId;
        appointment.StatusId = checkedInStatusId;
        appointment.UpdatedAt = now;

        if (appointment.RoomId == null && appointment.Slot?.RoomId is int slotRoomId)
        {
            appointment.RoomId = slotRoomId;
        }

        _context.AppointmentStatusHistories.Add(new AppointmentStatusHistory
        {
            AppointmentId = appointment.Id,
            OldStatusId = oldStatusId,
            NewStatusId = checkedInStatusId,
            ChangedAt = now
        });

        await _context.SaveChangesAsync(cancellationToken);

        return new ReceptionActionResult(
            true,
            $"Đã tiếp nhận bệnh nhân {appointment.Patient.FullName}.",
            queueNumber);
    }

    private static void ValidateRequest(CreateReceptionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            throw new ArgumentException("Vui lòng nhập họ tên bệnh nhân.");
        }

        if (request.FullName.Trim().Length > 100)
        {
            throw new ArgumentException("Họ tên bệnh nhân không được vượt quá 100 ký tự.");
        }

        if (request.DateOfBirth == default || request.DateOfBirth > DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException("Ngày sinh không hợp lệ.");
        }

        if (request.Gender is not (GenderCodes.Male or GenderCodes.Female or GenderCodes.Other))
        {
            throw new ArgumentException("Vui lòng chọn giới tính.");
        }

        string phone = NormalizePhone(request.PhoneNumber);
        if (phone.Length is < 9 or > 15 || !phone.All(char.IsDigit))
        {
            throw new ArgumentException("Số điện thoại phải gồm 9 đến 15 chữ số.");
        }

        if (request.DepartmentId <= 0)
        {
            throw new ArgumentException("Vui lòng chọn khoa/phòng khám.");
        }

        if (!string.IsNullOrWhiteSpace(request.NationalId) && request.NationalId.Trim().Length > 20)
        {
            throw new ArgumentException("Số CCCD/hộ chiếu không được vượt quá 20 ký tự.");
        }

        if (!string.IsNullOrWhiteSpace(request.InsuranceCardNumber) && request.InsuranceCardNumber.Trim().Length > 20)
        {
            throw new ArgumentException("Số BHYT không được vượt quá 20 ký tự.");
        }
    }

    private async Task<Dictionary<string, int>> LoadStatusMapAsync(CancellationToken cancellationToken)
    {
        List<AppointmentStatus> statuses = await _context.AppointmentStatuses
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return statuses.ToDictionary(s => s.Code, s => s.Id, StringComparer.OrdinalIgnoreCase);
    }

    private async Task<string> GenerateAppointmentCodeAsync(CancellationToken cancellationToken)
    {
        for (int attempt = 0; attempt < 10; attempt++)
        {
            string code = $"LK{DateTime.Now:yyMMddHHmm}{Random.Shared.Next(10, 100)}";
            bool exists = await _context.Appointments
                .AsNoTracking()
                .AnyAsync(a => a.AppointmentCode == code, cancellationToken);

            if (!exists)
            {
                return code;
            }

            await Task.Delay(5, cancellationToken);
        }

        throw new InvalidOperationException("Không thể sinh mã lượt khám mới. Vui lòng thử lại.");
    }

    private async Task<string> GenerateQueueNumberAsync(DateTime day, CancellationToken cancellationToken)
    {
        List<string> queueNumbers = await InDay(_context.Appointments.AsNoTracking(), day)
            .Where(a => a.QueueNumber != null)
            .Select(a => a.QueueNumber!)
            .ToListAsync(cancellationToken);

        int maxNumber = 0;
        foreach (string queueNumber in queueNumbers)
        {
            if (queueNumber.Length > 1 && int.TryParse(queueNumber.AsSpan(1), out int number))
            {
                maxNumber = Math.Max(maxNumber, number);
            }
        }

        return $"A{maxNumber + 1:D3}";
    }

    private async Task<string> GenerateUsernameAsync(CancellationToken cancellationToken)
    {
        for (int attempt = 0; attempt < 10; attempt++)
        {
            string username = $"bn{DateTime.Now:yyMMddHHmmss}{Random.Shared.Next(100, 1000)}";
            bool exists = await _context.Accounts
                .AsNoTracking()
                .AnyAsync(a => a.Username == username, cancellationToken);

            if (!exists)
            {
                return username;
            }

            await Task.Delay(5, cancellationToken);
        }

        throw new InvalidOperationException("Không thể sinh tài khoản bệnh nhân mới. Vui lòng thử lại.");
    }

    private static IQueryable<Appointment> InDay(IQueryable<Appointment> query, DateTime day)
    {
        DateTime end = day.AddDays(1);

        return query.Where(a =>
            (a.Slot != null && a.Slot.StartTime >= day && a.Slot.StartTime < end) ||
            (a.SlotId == null && a.CreatedAt >= day && a.CreatedAt < end));
    }

    private static List<int> ResolveIds(IReadOnlyDictionary<string, int> statusMap, IEnumerable<string> codes)
    {
        return codes
            .Where(code => statusMap.ContainsKey(code))
            .Select(code => statusMap[code])
            .ToList();
    }

    private static ReceptionItemDto MapItem(Appointment appointment)
    {
        string departmentName = appointment.Room?.Department.Name
            ?? appointment.Slot?.Room?.Department?.Name
            ?? "Chưa phân khoa";

        string doctorName = appointment.Slot?.Doctor.FullName ?? string.Empty;
        DateTime appointmentTime = appointment.Slot?.StartTime ?? appointment.CreatedAt;

        return new ReceptionItemDto(
            appointment.Id,
            appointment.AppointmentCode,
            appointment.Patient.Id,
            appointment.Patient.Id.ToPatientCode(),
            appointment.Patient.FullName,
            appointment.Patient.Gender.ToGenderVietnamese(),
            appointment.Patient.DateOfBirth,
            appointment.Patient.InsuranceCardNumber ?? string.Empty,
            departmentName,
            doctorName,
            appointment.Status.Code,
            appointment.Status.Name,
            appointmentTime,
            appointment.BookingChannel.ToChannelVietnamese(),
            appointment.VisitType,
            appointment.PriorityLevel,
            appointment.PriorityLevel.ToPriorityVietnamese(),
            appointment.QueueNumber ?? string.Empty,
            appointment.Reason ?? string.Empty);
    }

    private static ReceptionPatientDto MapPatient(Patient patient)
    {
        return new ReceptionPatientDto(
            patient.Id,
            patient.Id.ToPatientCode(),
            patient.FullName,
            patient.DateOfBirth,
            patient.Gender ?? string.Empty,
            patient.PhoneNumber,
            patient.NationalId ?? string.Empty,
            patient.Address ?? string.Empty,
            patient.InsuranceCardNumber ?? string.Empty);
    }

    private static int? TryParsePatientCode(string keyword)
    {
        if (keyword.StartsWith("BN", StringComparison.OrdinalIgnoreCase)
            && int.TryParse(keyword.AsSpan(2), out int patientId))
        {
            return patientId;
        }

        return null;
    }

    private static string NormalizePhone(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return string.Empty;
        }

        return new string(phoneNumber.Where(c => !char.IsWhiteSpace(c) && c != '.' && c != '-').ToArray());
    }

    private static string? NullIfWhiteSpace(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    private static string Truncate(string? value, int maxLength)
    {
        return value == null || value.Length <= maxLength ? value ?? string.Empty : value[..maxLength];
    }
}