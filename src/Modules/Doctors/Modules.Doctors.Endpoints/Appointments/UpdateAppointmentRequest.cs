namespace Modules.Doctors.Endpoints.Appointments
{
    internal sealed class UpdateAppointmentRequest
    {
        public Guid IdDoctor { get; set; }
        public Guid IdPatient { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
    }
}
