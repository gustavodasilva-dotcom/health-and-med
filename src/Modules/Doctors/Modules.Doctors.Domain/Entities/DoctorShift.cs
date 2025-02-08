using Common.Shared.Abstractions;

namespace Modules.Doctors.Domain.Entities;

public sealed class DoctorShift : BaseEntity
{
    private readonly Doctor? _doctor = null;

    private DoctorShiftAppointment? _appointment = null;

    public Guid DoctorId { get; private set; }

    public required DateTime StartAt { get; set; }

    public required DateTime EndAt { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [DoctorId, StartAt, EndAt];

    public Doctor Doctor
    {
        get
        {
            ArgumentNullException.ThrowIfNull(_doctor);
            return _doctor;
        }
    }

    public DoctorShiftAppointment? Appointment
        => _appointment;

    public void Update(DateTime startAt, DateTime endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAppointment(DoctorShiftAppointment appointment)
        => _appointment = appointment;
}
