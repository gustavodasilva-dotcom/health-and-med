namespace Modules.Doctors.Endpoints.Routes;

internal static class AppointmentsRoutes
{
    public const string Tags = nameof(Appointments);

    public const string GetAppointments = "api/doctors/appointments/{from:datetime}/{to:datetime}";
    
    public const string GetIdAppointment = "api/doctors/appointments/{id:guid}";
    
    public const string InsertAppointments = "api/doctors/appointments";
    
    public const string UpdateAppointments = "api/doctors/appointments/{id:guid}";
    
    public const string DeleteAppointments = "api/doctors/appointments/{id:guid}";
}
