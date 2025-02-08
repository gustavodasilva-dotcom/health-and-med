using Common.Shared;
using Common.Shared.Constants;
using Common.Shared.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.Patients.Application.Constants;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Application.Appointments.Commands.CancelAppointment;

internal sealed class CancelAppointmentCommandHandler(
    IRepository<PatientAppointment> patientAppointmentRepository,
    IPatientsUnitOfWork unitOfWork) :
    IRequestHandler<CancelAppointmentCommand, Result>
{
    private readonly IRepository<PatientAppointment> _patientAppointmentRepository
        = patientAppointmentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        CancelAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var appointment = _patientAppointmentRepository.GetById(request.AppointmentId);
        if (appointment is null)
        {
            return new Error(
                SharedErrorConstants.NotFoundTitle,
                ErrorConstants.PatientAppointmentNotFound,
                StatusCodes.Status404NotFound);
        }

        var result = appointment.CancelAppointment(request.Motive);
        if (result.IsFailure)
        {
            return result.Error!;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
