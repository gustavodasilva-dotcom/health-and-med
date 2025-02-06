using MediatR;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Queries.GetAppointments;

public sealed record GetAppointmentsQuery(DateTime From, DateTime To)
    : IRequest<IEnumerable<Appointment>>;
