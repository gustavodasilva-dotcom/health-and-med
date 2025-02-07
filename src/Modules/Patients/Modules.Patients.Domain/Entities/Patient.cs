using Common.Shared.Abstractions;

namespace Modules.Patients.Domain.Entities;

public sealed class Patient : UserEntity
{
    private readonly HashSet<PatientAppointment> _appointments = [];

    public required string Name { get; set; }

    public required string Ssn { get; set; }

    public required string Password { get; set; }

    public override IEnumerable<object> GetAtomicValues() => [Ssn];

    public IReadOnlySet<PatientAppointment> Appointments => _appointments;

    public void Update(string name, string cpf, string email)
    {
        Name = name.Trim();
        Ssn = cpf.Trim();
        Email = email.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAppointment(PatientAppointment appointment)
        => _appointments.Add(appointment);
}
