using MediatR;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Queries.GetAppointments;

internal sealed class GetAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
    : IRequestHandler<GetAppointmentsQuery, IEnumerable<Appointment>>
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

    public Task<IEnumerable<Appointment>> Handle(
        GetAppointmentsQuery request,
        CancellationToken cancellationToken)
    {
        var appointments = _appointmentRepository.GetByFilter(app =>
            app.DateFrom >= request.From &&
            app.DateFrom <= request.To);

        return Task.FromResult(appointments);
    }
}
