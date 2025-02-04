namespace Modules.Patients.Endpoints.Accesses;

internal sealed class RegisterPatientRequest
{
    public required string Name { get; init; }

    public required string Cpf { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}
