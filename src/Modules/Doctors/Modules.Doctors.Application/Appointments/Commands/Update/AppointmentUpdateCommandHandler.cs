using Common.Shared;
using Common.Shared.Constants;
using MediatR;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Commands.Update
{
    internal sealed class AppointmentUpdateCommandHandler(
        IAppointmentRepository _appointmentRepository,
        IAppoinmentUnitOfWork _appoinmentUnitOfWork
        )
        : IRequestHandler<AppointmentUpdateCommand, Result<Appointment>>
    {
        public async Task<Result<Appointment>> Handle(AppointmentUpdateCommand request, CancellationToken cancellationToken)
        {
            var isFree = _appointmentRepository.ExistsDateFree(request.IdDoctor, request.DateFrom, request.DateUntil);

            if (!isFree)
            {
                return new Error(
                    ErrorConstants.InvalidOperationTitle,
                    "Busy date for scheduling.");
            }

            var appointment = _appointmentRepository.GetById(request.Id);

            if(appointment == null)
            {
                return new Error(
                    ErrorConstants.NotFoundTitle,
                    "Appointment not found.");
            }

            appointment.Update(request.IdPatient,request.DateFrom,request.DateUntil);

            await _appoinmentUnitOfWork.SaveChangesAsync(cancellationToken);

            return appointment;
        }
    }
}
