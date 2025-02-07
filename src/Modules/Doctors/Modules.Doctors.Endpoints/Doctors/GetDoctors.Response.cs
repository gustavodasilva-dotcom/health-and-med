using System.Text.Json.Serialization;
using Common.Shared.Contracts;

namespace Modules.Doctors.Endpoints.Doctors;

internal sealed class GetDoctorsResponse
{
    public required string Name { get; init; }

    public required EnumResponse Specialty { get; init; }

    public required string Email { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    public required IEnumerable<GetDoctorsResponseRegistrations> Registrations { get; init; }
}
