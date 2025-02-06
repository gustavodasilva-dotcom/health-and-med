using MediatR;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Appointments.Queries.GetAppointmentById;

internal sealed class GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository)
    : IRequestHandler<GetAppointmentByIdQuery, Appointment?>
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

    public Task<Appointment?> Handle(
        GetAppointmentByIdQuery request,
        CancellationToken cancellationToken)
        => Task.FromResult(_appointmentRepository.GetById(request.Id));
}
