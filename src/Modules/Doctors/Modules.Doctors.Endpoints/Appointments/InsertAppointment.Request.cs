using System.Text.Json.Serialization;

namespace Modules.Doctors.Endpoints.Appointments;

internal sealed class InsertAppointmentRequest
{
    [JsonPropertyName("id_doctor")]
    public required Guid IdDoctor { get; init; }

    [JsonPropertyName("id_patient")]
    public required Guid IdPatient { get; init; }

    [JsonPropertyName("date_from")]
    public required DateTime DateFrom { get; init; }

    [JsonPropertyName("date_until")]
    public required DateTime DateUntil { get; init; }
}
