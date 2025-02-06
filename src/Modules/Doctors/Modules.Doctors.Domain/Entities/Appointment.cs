using Common.Shared.Abstractions;

namespace Modules.Doctors.Domain.Entities;

public sealed class Appointment : BaseEntity
{
    public required Guid IdDoctor { get; set; }
    public required Guid IdPatient { get; set; }
    public required DateTime DateFrom { get; set; }
    public required DateTime DateUntil { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [IdDoctor, IdPatient, DateFrom, DateUntil];

    public void Update(Guid idPatient, DateTime dateFrom, DateTime dateUntil)
    {
        IdPatient = idPatient;
        DateFrom = dateFrom;
        DateUntil = dateUntil;
        UpdatedAt = DateTime.UtcNow;
    }
}
