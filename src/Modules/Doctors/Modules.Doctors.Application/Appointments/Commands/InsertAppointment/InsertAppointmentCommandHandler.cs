using Common.Shared;
using MediatR;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Commands.InsertAppointment;

internal sealed class InsertAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IRequestHandler<InsertAppointmentCommand, Result>
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        InsertAppointmentCommand request,
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

        var appointment = new Appointment
        {
            IdDoctor = request.IdDoctor,
            IdPatient = request.IdPatient,
            DateFrom = request.DateFrom,
            DateUntil = request.DateUntil
        };

        _appointmentRepository.Add(appointment);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
