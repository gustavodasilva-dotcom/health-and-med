using System.Text.Json.Serialization;

namespace Modules.Patients.Endpoints.Appointments;

internal sealed class CancelAppointmentRequest
{
    [JsonPropertyName("cancellation_motive")]
    public required string Motive { get; init; }
}
