using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.Patients.Application.Constants;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Application.Appointments.Commands.RegisterAppointment
{
    internal sealed class CreateAppointmentCommandHandler(
        IPatientRepository patientRepository,
        IPatientAppointmentRepository patientAppointmentRepository,
        IPatientsUnitOfWork unitOfWork) :
        IRequestHandler<CreateAppointmentCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPatientRepository _patientRepository = patientRepository;
        private readonly IPatientAppointmentRepository _patientAppointmentRepository = patientAppointmentRepository;

        public async Task<Result> Handle(
            CreateAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var patient = _patientRepository.GetById(request.PatientId);
            if (patient is null)
            {
                return new Error(
                    SharedErrorConstants.NotFoundTitle,
                    "No patient was found with the given id.",
                    StatusCodes.Status404NotFound);
            }

            if (!_patientAppointmentRepository.IsPatientAvailable(request.PatientId, request.StartAt, request.EndAt))
            {
                return new Error(
                    SharedErrorConstants.InvalidOperationTitle,
                    ErrorConstants.PatientUnavailableMessage);
            }

            if (!_patientAppointmentRepository.IsDoctorAvailable(request.DoctorId, request.StartAt, request.EndAt))
            {
                return new Error(
                    SharedErrorConstants.InvalidOperationTitle,
                    ErrorConstants.DoctorUnavailableMessage);
            }

            //Adicionar validação de agenda do médico

            var appointment = new PatientAppointment
            {
                StartAt = request.StartAt,
                EndAt = request.EndAt
            };

            patient.AddAppointment(appointment);

            _patientAppointmentRepository.Add(appointment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
