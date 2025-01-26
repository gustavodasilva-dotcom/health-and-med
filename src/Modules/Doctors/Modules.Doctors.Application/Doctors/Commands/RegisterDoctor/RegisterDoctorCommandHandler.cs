using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Security;
using MediatR;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Doctors.Commands.RegisterDoctor;

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
                ErrorConstants.InvalidOperationTitle,
                "The email is already in use.");
        }

        if (_doctorRepository.ExistsWithCrmInUf(request.CrmUf, request.Crm))
        {
            return new Error(
                ErrorConstants.InvalidOperationTitle,
                "The CRM is already in use.");
        }

        var doctor = new Doctor
        {
            Name = request.Name.Trim(),
            Cpf = request.Cpf.Trim(),
            CrmUf = request.CrmUf,
            Crm = request.Crm,
            Email = request.Email.Trim(),
            Password = _passwordHasher.Hash(request.Password.Trim())
        };

        _doctorRepository.Add(doctor);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
