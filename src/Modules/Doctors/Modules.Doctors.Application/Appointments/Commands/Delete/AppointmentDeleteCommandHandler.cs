using Common.Shared;
using Common.Shared.Constants;
using MediatR;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;

namespace Modules.Doctors.Application.Appointments.Commands.Delete
{
    internal sealed class AppointmentDeleteCommandHandler (
        IAppointmentRepository _appointmentRepository,
        IAppoinmentUnitOfWork _appoinmentUnitOfWork
        )
        : IRequestHandler<AppointmentDeleteCommand, Result>
    {
        public async Task<Result> Handle(AppointmentDeleteCommand request, CancellationToken cancellationToken)
        {

            var appointment = _appointmentRepository.GetById(request.Id);

            if(appointment == null)
            {
                return new Error(
                    ErrorConstants.NotFoundTitle,
                    "Appointment not found.");
            }

            appointment.Delete();

            await _appoinmentUnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
