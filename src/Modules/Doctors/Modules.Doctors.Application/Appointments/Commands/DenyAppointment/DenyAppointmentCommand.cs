using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Appointments.Commands.DenyAppointment;

public sealed record DenyAppointmentCommand(Guid AppointmentId) : IRequest<Result>;
