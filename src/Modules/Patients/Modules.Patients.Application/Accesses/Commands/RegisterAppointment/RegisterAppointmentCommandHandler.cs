using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Repositories;
using MediatR;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Application.Accesses.Commands.RegisterAppointment
{
    internal sealed class RegisterAppointmentCommandHandler(
        IPatientsUnitOfWork unitOfWork) : IRequestHandler<RegisterAppointmentCommand, Result>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(
            RegisterAppointmentCommand request, 
            CancellationToken cancellationToken)
        {
            //if (_patientRepository.ExistsWithEmail(request.Email))
            //{
            //    return new Error(
            //        SharedErrorConstants.InvalidOperationTitle,
            //        "The email is already in use.");
            //}

            //if (_patientRepository.ExistsWithCpf(request.Ssn))
            //{
            //    return new Error(
            //        SharedErrorConstants.InvalidOperationTitle,
            //        "The CPF is already in use.");
            //}

            //var patient = new Patient
            //{
            //    Name = request.Name.Trim(),
            //    Ssn = request.Ssn.Trim(),
            //    Email = request.Email.Trim(),
            //    Password = _passwordHasher.Hash(request.Password.Trim())
            //};

            //_patientRepository.Add(patient);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
