using Common.Shared.Abstractions;

namespace Modules.Doctors.Domain.Entities;

public sealed class DoctorShift : BaseEntity
{
    private readonly DoctorRegistration? _registration = null;

    public Guid DoctorRegistrationId { get; private set; }

    public required DateTime StartAt { get; set; }

    public required DateTime EndAt { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [DoctorRegistrationId, StartAt, EndAt];

    public DoctorRegistration Registration
    {
        get
        {
            ArgumentNullException.ThrowIfNull(_registration);
            return _registration;
        }
    }

    public void Update(DateTime startAt, DateTime endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
        UpdatedAt = DateTime.UtcNow;
    }
}
