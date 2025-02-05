using MediatR;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Queries.GetIDAppointment;

public sealed record GetIDAppointmentQuery(Guid Id) : IRequest<Appointment>;
