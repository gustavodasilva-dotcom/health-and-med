namespace Modules.Doctors.IntegrationEvents;

public sealed record AppointmentDeniedByDoctorIntegrationEvent
{
    public required Guid PatientId { get; init; }

    public required Guid DoctorShiftId { get; init; }
}
