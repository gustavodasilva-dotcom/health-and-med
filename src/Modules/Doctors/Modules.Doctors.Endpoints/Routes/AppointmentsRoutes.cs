namespace Modules.Doctors.Endpoints.Routes;

internal static class AppointmentsRoutes
{
    public const string Tags = "Doctor's Appointments";

    public const string DenyAppointment = "api/doctors/appointments/{appointmentId:guid}/deny";

    public const string AcceptAppointment = "api/doctors/appointments/{appointmentId:guid}/accept";
}
