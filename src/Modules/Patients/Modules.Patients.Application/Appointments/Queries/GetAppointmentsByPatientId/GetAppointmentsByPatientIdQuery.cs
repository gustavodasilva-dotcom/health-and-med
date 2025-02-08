using MediatR;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Application.Appointments.Queries.GetAppointmentsByPatientId;

public sealed record GetAppointmentsByPatientIdQuery(Guid PatientId)
    : IRequest<IEnumerable<PatientAppointment>>;
