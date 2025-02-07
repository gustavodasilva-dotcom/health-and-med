using Common.Shared.Abstractions;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Domain.Entities;

public sealed class Doctor : UserEntity
{
    private readonly HashSet<DoctorShift> _shifts = [];

    public required string Name { get; set; }

    public required string Ssn { get; set; }

    public required int RegistrationNumber { get; set; }

    public required MedicalSpecialties Specialty { get; set; }

    public required string Password { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [RegistrationNumber];

    public IReadOnlySet<DoctorShift> Shifts
        => _shifts;

    public void Update(
        string name,
        string ssn,
        int registrationNumber,
        MedicalSpecialties specialty,
        string email)
    {
        Name = name.Trim();
        Ssn = ssn.Trim();
        RegistrationNumber = registrationNumber;
        Specialty = specialty;
        Email = email.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddShift(DoctorShift shift)
        => _shifts.Add(shift);
}
