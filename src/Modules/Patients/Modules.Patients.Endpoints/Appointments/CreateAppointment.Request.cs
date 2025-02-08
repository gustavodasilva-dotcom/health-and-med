using System.Text.Json.Serialization;

namespace Modules.Patients.Endpoints.Appointments;

internal sealed class CreateAppointmentRequest
{
    [JsonPropertyName("patient_id")]
    public required Guid PatientId { get; init; }

    [JsonPropertyName("doctor_shift_id")]
    public required Guid DoctorShiftId { get; init; }
}
