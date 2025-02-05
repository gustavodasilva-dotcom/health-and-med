using MediatR;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Queries.GetIDAppointment;

internal sealed class GetIDAppointmentQueryHandler(
    IAppointmentRepository _appointmentRepository
    )
    : IRequestHandler<GetIDAppointmentQuery, Appointment>
{
    public async Task<Appointment> Handle(
        GetIDAppointmentQuery request,
        CancellationToken cancellationToken)
    {
        return _appointmentRepository.GetById(request.Id);
    }
}
