namespace Modules.Patients.Endpoints.Accesses;

internal sealed class LoginPatientRequest
{
    public required string Email { get; init; }

    public required string Password { get; init; }
}
