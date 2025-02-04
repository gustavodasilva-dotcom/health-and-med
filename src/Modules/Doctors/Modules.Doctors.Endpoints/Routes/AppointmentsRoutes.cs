namespace Modules.Doctors.Endpoints.Routes;

internal static class AppointmentsRoutes
{
    public const string Tags = nameof(Appointments);

    public const string GetAppointments = "api/doctors/appointments/{from:datetime}/{to:datetime}";
}
