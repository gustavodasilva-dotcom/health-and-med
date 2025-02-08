using Common.Shared;
using MediatR;

namespace Modules.Patients.Application.Appointments.Commands.CancelAppointment;

public sealed record CancelAppointmentCommand(Guid AppointmentId, string Motive)
    : IRequest<Result>;
