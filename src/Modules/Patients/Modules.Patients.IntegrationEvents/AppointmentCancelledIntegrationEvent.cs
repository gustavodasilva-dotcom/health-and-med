namespace Modules.Patients.IntegrationEvents;

public sealed record AppointmentCancelledIntegrationEvent(Guid DoctorShiftId);
