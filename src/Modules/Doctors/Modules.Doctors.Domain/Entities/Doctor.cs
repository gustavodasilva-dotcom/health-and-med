using Common.Shared.Abstractions;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Domain.Entities;

public sealed class Doctor : UserEntity
{
    private readonly HashSet<DoctorRegistration> _registrations = [];

    public required string Name { get; set; }

    public required string Ssn { get; set; }

    public required string Password { get; set; }

    public required MedicalSpecialties Specialty { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [Ssn];

    public IReadOnlySet<DoctorRegistration> Registrations
        => _registrations;

    public void Update(string name, string ssn, string email, MedicalSpecialties specialty)
    {
        Name = name.Trim();
        Ssn = ssn.Trim();
        Email = email.Trim();
        Specialty = specialty;
        UpdatedAt = DateTime.UtcNow;
    }

    public DoctorRegistration AddRegistration(int registrationNumber, UFs registrationState)
    {
        var registration = new DoctorRegistration
        {
            Number = registrationNumber,
            State = registrationState
        };

        _registrations.Add(registration);

        return registration;
    }
}
