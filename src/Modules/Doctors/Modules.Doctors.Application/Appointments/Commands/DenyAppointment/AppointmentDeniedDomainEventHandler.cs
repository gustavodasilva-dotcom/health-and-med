using MassTransit;
using MediatR;
using Modules.Doctors.Domain.DomainEvents;
using Modules.Doctors.IntegrationEvents;

namespace Modules.Doctors.Application.Appointments.Commands.DenyAppointment;

internal sealed class AppointmentDeniedDomainEventHandler(IPublishEndpoint publishEndpoint)
    : INotificationHandler<AppointmentDeniedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task Handle(
        AppointmentDeniedDomainEvent notification,
        CancellationToken cancellationToken)
        => _publishEndpoint.Publish(new AppointmentDeniedByDoctorIntegrationEvent
        {
            PatientId = notification.PatientId,
            DoctorShiftId = notification.DoctorShiftId
        }, cancellationToken);
}
