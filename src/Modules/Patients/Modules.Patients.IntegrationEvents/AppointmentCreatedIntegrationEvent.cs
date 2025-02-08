namespace Modules.Patients.IntegrationEvents;

public sealed record AppointmentCreatedIntegrationEvent
{
    public required Guid AppointmentId { get; init; }

    public required Guid DoctorShiftId { get; init; }

    public required Guid PatientId { get; init; }
}
