using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Repositories;
using Common.Shared.Security;
using MediatR;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Application.Accesses.Commands.RegisterPatient;

internal sealed class RegisterPatientCommandHandler(
    IPasswordHasher passwordHasher,
    IPatientRepository patientRepository,
    IPatientsUnitOfWork unitOfWork) :
    IRequestHandler<RegisterPatientCommand, Result>
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        RegisterPatientCommand request,
        CancellationToken cancellationToken)
    {
        if (_patientRepository.ExistsWithEmail(request.Email))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "The email is already in use.");
        }

        if (_patientRepository.ExistsWithCpf(request.Cpf))
        {
            return new Error(
                SharedErrorConstants.InvalidOperationTitle,
                "The CPF is already in use.");
        }

        var patient = new Patient
        {
            Name = request.Name.Trim(),
            Cpf = request.Cpf.Trim(),
            Email = request.Email.Trim(),
            Password = _passwordHasher.Hash(request.Password.Trim())
        };

        _patientRepository.Add(patient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
