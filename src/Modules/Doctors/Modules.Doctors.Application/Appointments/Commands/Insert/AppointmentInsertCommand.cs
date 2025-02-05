using Common.Shared;
using MediatR;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Commands.Insert
{
    public sealed record class AppointmentInsertCommand(Guid IdDoctor, Guid IdPatient, DateTime DateFrom, DateTime DateUntil) 
        : IRequest<Result<Appointment>>;
}
