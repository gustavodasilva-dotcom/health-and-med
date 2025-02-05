namespace Modules.Doctors.Endpoints.Appointments
{
    internal sealed class InsertAppointmentRequest
    {
        public Guid IdDoctor { get; set; }
        public Guid IdPatient { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
    }
}
