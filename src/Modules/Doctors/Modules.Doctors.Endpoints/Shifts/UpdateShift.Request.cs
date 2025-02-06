using System.Text.Json.Serialization;

namespace Modules.Doctors.Endpoints.Shifts;

internal sealed class UpdateShiftRequest
{
    [JsonPropertyName("start_at")]
    public required DateTime StartAt { get; init; }

    [JsonPropertyName("end_at")]
    public required DateTime EndAt { get; init; }
}
