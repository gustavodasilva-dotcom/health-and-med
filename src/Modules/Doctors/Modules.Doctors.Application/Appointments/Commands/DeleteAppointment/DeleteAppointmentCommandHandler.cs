using Common.Shared;
using MediatR;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;

namespace Modules.Doctors.Application.Appointments.Commands.DeleteAppointment;

internal sealed class DeleteAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IRequestHandler<DeleteAppointmentCommand, Result>
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        DeleteAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var appointment = _appointmentRepository.GetById(request.Id);
        if (appointment is null)
        {
            return new Error(
                ErrorConstants.NotFoundTitle,
                "Appointment not found.");
        }

        appointment.Delete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
