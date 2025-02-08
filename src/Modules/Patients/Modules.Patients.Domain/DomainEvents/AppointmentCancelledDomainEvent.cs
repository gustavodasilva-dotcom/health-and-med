using Common.Shared.Abstractions;

namespace Modules.Patients.Domain.DomainEvents;

public sealed record AppointmentCancelledDomainEvent(Guid DoctorShiftId) : IDomainEvent;
