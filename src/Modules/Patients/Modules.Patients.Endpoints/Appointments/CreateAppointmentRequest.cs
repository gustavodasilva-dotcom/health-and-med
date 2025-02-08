using System.Text.Json.Serialization;

namespace Modules.Patients.Endpoints.Appointments
{
    internal sealed class CreateAppointmentRequest
    {
        [JsonPropertyName("patient_id")]
        public required Guid PatientId { get; init; }

        [JsonPropertyName("doctor_id")]
        public required Guid DoctorId { get; init; }

        [JsonPropertyName("start_at")]
        public required DateTime StartAt { get; init; }
    }
}
