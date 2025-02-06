using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Appointments.Commands.UpdateAppointment;

public sealed record class UpdateAppointmentCommand(
    Guid Id,
    Guid IdDoctor,
    Guid IdPatient,
    DateTime DateFrom,
    DateTime DateUntil) : IRequest<Result>;
