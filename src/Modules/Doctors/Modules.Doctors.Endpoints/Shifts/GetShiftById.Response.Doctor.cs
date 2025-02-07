using System.Text.Json.Serialization;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Endpoints.Shifts;

internal sealed class GetShiftByIdResponseDoctor
{
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("registration_number")]
    public required int RegistrationNumber { get; init; }

    [JsonPropertyName("registration_state")]
    public required UFs RegistrationState { get; init; }

    public required string Ssn { get; init; }
}
