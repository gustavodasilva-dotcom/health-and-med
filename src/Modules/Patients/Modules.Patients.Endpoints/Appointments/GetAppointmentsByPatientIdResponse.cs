using System.Text.Json.Serialization;

namespace Modules.Patients.Endpoints.Appointments
{
    internal sealed class GetAppointmentsByPatientIdResponse
    {
        public required Guid Id { get; set; }

        [JsonPropertyName("start_at")]
        public required DateTime StartAt { get; init; }

        [JsonPropertyName("doctor_id")]
        public Guid DoctorId { get; init; }
    }
}
