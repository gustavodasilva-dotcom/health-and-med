using Common.Shared.Abstractions;

namespace Modules.Patients.Domain.DomainEvents;

public sealed record AppointmentCreatedDomainEvent(Guid PatientId, Guid DoctorShiftId)
    : IDomainEvent;
