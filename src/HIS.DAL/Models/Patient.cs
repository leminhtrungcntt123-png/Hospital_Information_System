using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class Patient
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? NationalId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    public string? InsuranceCardNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
