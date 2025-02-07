using System.Text.Json.Serialization;

namespace Modules.Doctors.Endpoints.Shifts;

internal sealed class GetShiftsResponseDoctor
{
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("registration_id")]
    public required int RegistrationId { get; init; }

    public required string Ssn { get; init; }
}
