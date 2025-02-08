using Common.Shared.Abstractions;

namespace Modules.Doctors.Domain.DomainEvents;

public sealed record AppointmentAcceptedDomainEvent : IDomainEvent
{
    public required Guid PatientId { get; init; }

    public required Guid DoctorShiftId { get; init; }

    public required decimal AppointmentPrice { get; init; }
}
