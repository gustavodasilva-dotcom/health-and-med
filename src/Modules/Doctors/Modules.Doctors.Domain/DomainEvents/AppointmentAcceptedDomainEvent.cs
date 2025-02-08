using Common.Shared.Abstractions;

namespace Modules.Doctors.Domain.DomainEvents;

public sealed record AppointmentAcceptedDomainEvent(Guid PatientId, Guid DoctorShiftId)
    : IDomainEvent;
