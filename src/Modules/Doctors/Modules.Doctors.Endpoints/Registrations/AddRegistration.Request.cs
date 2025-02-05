using System.Text.Json.Serialization;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Endpoints.Registrations;

internal sealed class AddRegistrationRequest
{
    [JsonPropertyName("registration_state")]
    public UFs RegistrationState { get; init; }
    
    [JsonPropertyName("registration_number")]
    public int RegistrationNumber { get; init; }
}
