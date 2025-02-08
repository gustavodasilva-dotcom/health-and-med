namespace Modules.Doctors.IntegrationEvents;

public sealed class AppointmentDeniedBySystemIntegrationEvent
{
    public required Guid PatientAppointmentId { get; init; }

    public required string Motive { get; init; }
}
