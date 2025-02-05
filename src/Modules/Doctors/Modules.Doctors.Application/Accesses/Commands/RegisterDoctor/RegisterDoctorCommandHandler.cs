using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Security;
using MediatR;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Accesses.Commands.RegisterDoctor;

internal sealed class RegisterDoctorCommandHandler(
    IPasswordHasher passwordHasher,
    IDoctorRepository doctorRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IRequestHandler<RegisterDoctorCommand, Result>
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IDoctorRepository _doctorRepository = doctorRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        RegisterDoctorCommand request,
        CancellationToken cancellationToken)
    {
        if (_doctorRepository.ExistsWithEmail(request.Email))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "The informed email is already in use.");
        }

        if (_doctorRepository.ExistsWithSsn(request.Ssn))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "The informed Social Security Number is already in use.");
        }

        if (_doctorRepository.IsRegisteredInState(request.RegistrationNumber, request.RegistrationState))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                ErrorConstants.RegistrationNumberIsRegisteredInStateMessage);
        }

        var doctor = new Doctor
        {
            Name = request.Name.Trim(),
            Ssn = request.Ssn.Trim(),
            Email = request.Email.Trim(),
            Password = _passwordHasher.Hash(request.Password.Trim())
        };
        doctor.AddRegistration(request.RegistrationNumber, request.RegistrationState);

        _doctorRepository.Add(doctor);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
