using System.Text.Json.Serialization;
using Common.Shared.Contracts;

namespace Modules.Doctors.Endpoints.Shifts;

internal sealed class GetShiftByIdResponseDoctor
{
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("registration_number")]
    public required int RegistrationNumber { get; init; }

    public required EnumResponse Specialty { get; init; }
}
