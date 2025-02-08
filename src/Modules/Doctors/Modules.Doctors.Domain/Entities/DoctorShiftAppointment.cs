using Common.Shared.Abstractions;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Domain.Entities;

public sealed class DoctorShiftAppointment : BaseEntity
{
    public Guid DoctorShiftId { get; private set; }

    public required Guid PatientId { get; set; }

    public required AppointmentStatus Status { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [DoctorShiftId, PatientId];
}
