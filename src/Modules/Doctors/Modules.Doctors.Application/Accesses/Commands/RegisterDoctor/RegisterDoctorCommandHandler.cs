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
        if (_doctorRepository.IsRegistrationNumberInUse(request.RegistrationNumber))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                ErrorConstants.RegistrationNumberIsAlreadyInUseMessage);
        }

        var doctor = new Doctor
        {
            Name = request.Name.Trim(),
            Ssn = request.Ssn.Trim(),
            RegistrationNumber = request.RegistrationNumber,
            Specialty = request.Specialty,
            Email = request.Email.Trim(),
            Password = _passwordHasher.Hash(request.Password.Trim()),
        };

        _doctorRepository.Add(doctor);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
