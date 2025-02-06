using Common.Shared;
using MediatR;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;

namespace Modules.Doctors.Application.Appointments.Commands.UpdateAppointment;

internal sealed class UpdateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IRequestHandler<UpdateAppointmentCommand, Result>
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var isFree = _appointmentRepository.ExistsDateFree(
            idDoctor: request.IdDoctor,
            dateFrom: request.DateFrom,
            dateUntil: request.DateUntil);

        if (!isFree)
        {
            return new Error(
                ErrorConstants.InvalidOperationTitle,
                "Busy date for scheduling.");
        }

        var appointment = _appointmentRepository.GetById(request.Id);
        if (appointment is null)
        {
            return new Error(
                ErrorConstants.NotFoundTitle,
                "Appointment not found.");
        }

        appointment.Update(request.IdPatient, request.DateFrom, request.DateUntil);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
