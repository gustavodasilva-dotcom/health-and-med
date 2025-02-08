using Common.Shared.Abstractions;

namespace Modules.Doctors.Domain.DomainEvents;

public sealed record AppointmentDeniedDomainEvent(Guid PatientId, Guid DoctorShiftId)
    : IDomainEvent;
