namespace HIS.BLL.DTOs;

public record PatientListItemDto(
    int Id,
    string FullName,
    DateOnly DateOfBirth,
    string Gender,
    string PhoneNumber,
    string NationalId,
    string Address,
    string InsuranceCardNumber);