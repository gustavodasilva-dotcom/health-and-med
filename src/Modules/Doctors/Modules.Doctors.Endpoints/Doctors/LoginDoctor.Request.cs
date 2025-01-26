namespace Modules.Doctors.Endpoints.Doctors;

internal sealed class LoginDoctorRequest
{
    public required string Email { get; init; }

    public required string Password { get; init; }
}
