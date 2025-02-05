using System.Text.Json.Serialization;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Endpoints.Accesses;

internal sealed class RegisterDoctorRequest
{
    public required string Name { get; init; }

    public required string Ssn { get; init; }

    [JsonPropertyName("registration_state")]
    public UFs RegistrationState { get; init; }
    
    [JsonPropertyName("registration_number")]
    public int RegistrationNumber { get; init; }

    public required string Email { get; init; }
    
    public required string Password { get; init; }
}
