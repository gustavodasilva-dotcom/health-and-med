namespace Modules.Doctors.Endpoints.Routes;

internal static class AppointmentsRoutes
{
    public const string Tags = nameof(Appointments);

    public const string GetAppointments = "api/doctors/appointments/{from:datetime}/{to:datetime}";
    public const string GetIDAppointment = "api/doctors/appointments/{Id:Guid}";
    public const string InsertAppointments = "api/doctors/appointments";
    public const string UpdateAppointments = "api/doctors/appointments/{Id:Guid}";
    public const string DeleteAppointments = "api/doctors/appointments/{Id:Guid}";
}
