using System.Text.Json.Serialization;

namespace Modules.Doctors.Endpoints.Shifts;

internal sealed class UpdateShiftResponse
{
    public required Guid Id { get; init; }

    [JsonPropertyName("start_at")]
    public required DateTime StartAt { get; init; }

    [JsonPropertyName("end_at")]
    public required DateTime EndAt { get; init; }
}
