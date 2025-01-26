using MediatR;

namespace Modules.Doctors.Application.Appointments.Queries.GetAppointments;

internal sealed class GetAppointmentsQueryHandler
    : IRequestHandler<GetAppointmentsQuery>
{
    public Task Handle(
        GetAppointmentsQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
