using Common.Shared;
using Common.Shared.Constants;
using MediatR;
using Modules.Doctors.Application.Constants;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Commands.Insert
{
    internal sealed class AppointmentInsertCommandHandler(
        IAppointmentRepository _appointmentRepository,
        IAppoinmentUnitOfWork _appoinmentUnitOfWork
        )
        : IRequestHandler<AppointmentInsertCommand, Result<Appointment>>
    {
        public async Task<Result<Appointment>> Handle(AppointmentInsertCommand request, CancellationToken cancellationToken)
        {
            var isFree = _appointmentRepository.ExistsDateFree(request.IdDoctor, request.DateFrom, request.DateUntil);

            if (!isFree) 
            {
                return new Error(
                    ErrorConstants.InvalidOperationTitle,
                    "Busy date for scheduling.");
            }

            var appointment = new Appointment(
                    request.IdDoctor,
                    request.IdPatient,
                    request.DateFrom,
                    request.DateUntil
                );

            _appointmentRepository.Add(appointment);

           await _appoinmentUnitOfWork.SaveChangesAsync(cancellationToken);

            return appointment;
        }
    }
}
