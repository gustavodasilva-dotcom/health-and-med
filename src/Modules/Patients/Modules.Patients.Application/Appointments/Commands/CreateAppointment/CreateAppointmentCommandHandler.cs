using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.Patients.Application.Constants;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Application.Appointments.Commands.CreateAppointment;

internal sealed class CreateAppointmentCommandHandler(
    IPatientRepository patientRepository,
    IRepository<PatientAppointment> patientAppointmentRepository,
    IPatientsUnitOfWork unitOfWork) :
    IRequestHandler<CreateAppointmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly IRepository<PatientAppointment> _patientAppointmentRepository
        = patientAppointmentRepository;

    public async Task<Result> Handle(
        CreateAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var patient = _patientRepository.GetById(request.PatientId);
        if (patient is null)
        {
            return new Error(
                SharedErrorConstants.NotFoundTitle,
                ErrorConstants.PatientNotFound,
                StatusCodes.Status404NotFound);
        }

        // TODO: Adicionar validação de agenda do médico.

        var appointment = new PatientAppointment
        {
            DoctorShiftId = request.DoctorShiftId
        };
        patient.AddAppointment(appointment);

        _patientAppointmentRepository.Add(appointment);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
