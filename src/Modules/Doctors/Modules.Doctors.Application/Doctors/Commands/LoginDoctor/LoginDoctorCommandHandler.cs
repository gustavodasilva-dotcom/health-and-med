using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Security;
using MediatR;
using Modules.Doctors.Domain.Abstractions;

namespace Modules.Doctors.Application.Doctors.Commands.LoginDoctor;

internal sealed class LoginDoctorCommandHandler(
    IPasswordHasher passwordHasher,
    TokenProvider tokenProvider,
    IDoctorRepository doctorRepository) :
    IRequestHandler<LoginDoctorCommand, Result<string>>
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly TokenProvider _tokenProvider = tokenProvider;
    private readonly IDoctorRepository _doctorRepository = doctorRepository;

    public async Task<Result<string>> Handle(
        LoginDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = _doctorRepository.GetWithEmail(request.Email);
        if (doctor is null)
        {
            return new Error(
                ErrorConstants.NotFoundTitle,
                "No doctor was found with the given email.");
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
