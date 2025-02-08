using Common.Shared;
using Common.Shared.Abstractions;
using Common.Shared.Constants;
using Modules.Doctors.Domain.DomainEvents;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Domain.Entities;

public sealed class DoctorShiftAppointment : BaseEntity
{
    public Guid DoctorShiftId { get; private set; }

    public required Guid PatientId { get; set; }

    public required AppointmentStatus Status { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [DoctorShiftId, PatientId];

    public Result DenyAppointment()
    {
        if (Status != AppointmentStatus.PendingDoctorAnalysis)
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "To be denied, an appointment needs to be pending the doctor analysis.");
        }

        Status = AppointmentStatus.Denied;

        RaiseDomainEvent(new AppointmentDeniedDomainEvent(PatientId, DoctorShiftId));

        return Result.Success();
    }

    public Result AcceptAppointment()
    {
        if (Status != AppointmentStatus.PendingDoctorAnalysis)
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "To be accepted, an appointment needs to be pending the doctor analysis.");
        }

        Status = AppointmentStatus.Accepted;

        RaiseDomainEvent(new AppointmentAcceptedDomainEvent(PatientId, DoctorShiftId));

        return Result.Success();
    }
}
