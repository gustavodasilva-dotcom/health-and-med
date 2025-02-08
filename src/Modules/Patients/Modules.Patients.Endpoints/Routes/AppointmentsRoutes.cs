namespace Modules.Patients.Endpoints.Routes;

internal static class AppointmentsRoutes
{
    public const string Tags = "Patient's Appointments";

    public const string CreateAppointment = "api/patients/appointments";

    public const string GetAppointmentsByPatientId = "api/patients/{id:guid}/appointments";
}
