using MediatR;

namespace Modules.Doctors.Application.Appointments.Queries.GetAppointments;

public sealed record GetAppointmentsQuery(DateTime From, DateTime To) : IRequest;
