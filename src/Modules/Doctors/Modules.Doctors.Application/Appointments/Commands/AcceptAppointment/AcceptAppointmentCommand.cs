using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Appointments.Commands.AcceptAppointment;

public sealed record AcceptAppointmentCommand(Guid AppointmentId) : IRequest<Result>;
