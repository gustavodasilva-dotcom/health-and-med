using MassTransit;
using MediatR;
using Modules.Patients.Domain.DomainEvents;
using Modules.Patients.IntegrationEvents;

namespace Modules.Patients.Application.Appointments.Commands.CancelAppointment;

internal sealed class AppointmentCancelledDomainEventHandler(
    IPublishEndpoint publishEndpoint) :
    INotificationHandler<AppointmentCancelledDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task Handle(
        AppointmentCancelledDomainEvent notification,
        CancellationToken cancellationToken)
        => _publishEndpoint.Publish(
            new AppointmentCancelledIntegrationEvent(notification.DoctorShiftId),
            cancellationToken);
}
