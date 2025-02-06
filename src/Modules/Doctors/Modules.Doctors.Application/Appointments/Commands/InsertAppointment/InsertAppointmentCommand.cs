using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Appointments.Commands.InsertAppointment;

public sealed record InsertAppointmentCommand(
    Guid IdDoctor,
    Guid IdPatient,
    DateTime DateFrom,
    DateTime DateUntil) : IRequest<Result>;
