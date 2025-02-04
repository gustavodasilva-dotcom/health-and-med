using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Security;
using MediatR;
using Modules.Patients.Domain.Abstractions;

namespace Modules.Patients.Application.Accesses.Commands.LoginPatient;

internal sealed class LoginPatientCommandHandler(
    IPasswordHasher passwordHasher,
    TokenProvider tokenProvider,
    IPatientRepository patientRepository) :
    IRequestHandler<LoginPatientCommand, Result<string>>
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly TokenProvider _tokenProvider = tokenProvider;
    private readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<Result<string>> Handle(
        LoginPatientCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = _patientRepository.GetWithEmail(request.Email);
        if (doctor is null)
        {
            return new Error(
                ErrorConstants.NotFoundTitle,
                "No patient was found with the given email.");
        }

        if (!_passwordHasher.Verify(request.Password.Trim(), doctor.Password))
        {
            return new Error(
                ErrorConstants.InvalidOperationTitle,
                "The given password is incorrect.");
        }

        var token = _tokenProvider.Create(doctor, UserRoles.Doctor);

        return await Task.FromResult(token);
    }
}
