using Common.Shared;
using MediatR;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Commands.Update
{
    public sealed record class AppointmentUpdateCommand(Guid Id,Guid IdDoctor, Guid IdPatient, DateTime DateFrom, DateTime DateUntil) 
        : IRequest<Result<Appointment>>;
}
