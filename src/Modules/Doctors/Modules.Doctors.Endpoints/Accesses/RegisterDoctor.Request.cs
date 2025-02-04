using System.Text.Json.Serialization;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Endpoints.Accesses;

internal sealed class RegisterDoctorRequest
{
    public required string Name { get; init; }

    public required string Cpf { get; init; }

    [JsonPropertyName("crm_uf")]
    public UFs CrmUf { get; init; }
    
    public int Crm { get; init; }

    public required string Email { get; init; }
    
    public required string Password { get; init; }
}
