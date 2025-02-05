using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Appointments.Commands.Delete
{
    public sealed record class AppointmentDeleteCommand(Guid Id) : IRequest<Result>;
}
