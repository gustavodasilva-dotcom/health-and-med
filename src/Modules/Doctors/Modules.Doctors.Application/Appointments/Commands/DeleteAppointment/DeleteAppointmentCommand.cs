using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Appointments.Commands.DeleteAppointment;

public sealed record class DeleteAppointmentCommand(Guid Id) : IRequest<Result>;
