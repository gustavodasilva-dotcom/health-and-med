using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Queries.GetAppointments;

internal sealed class GetAppointmentsQueryHandler(
    IAppointmentRepository _appointmentRepository
    )
    : IRequestHandler<GetAppointmentsQuery, IEnumerable<Appointment>>
{
    public async Task<IEnumerable<Appointment>> Handle(
        GetAppointmentsQuery request,
        CancellationToken cancellationToken)
    {
        return _appointmentRepository.GetByFilter(app => app.DateFrom >= request.From && app.DateFrom <= request.To);
    }
}
