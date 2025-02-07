using System.Text.Json.Serialization;

namespace Modules.Doctors.Endpoints.Accesses;

internal sealed class LoginDoctorRequest
{
    [JsonPropertyName("registration_number")]
    public required int RegistrationNumber { get; init; }

    public required string Password { get; init; }
}
