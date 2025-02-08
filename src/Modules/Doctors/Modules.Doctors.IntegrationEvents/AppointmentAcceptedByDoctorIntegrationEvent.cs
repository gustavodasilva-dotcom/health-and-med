namespace Modules.Doctors.IntegrationEvents;

public sealed record AppointmentAcceptedByDoctorIntegrationEvent
{
    public required Guid PatientId { get; init; }

    public required Guid DoctorShiftId { get; init; }
}
