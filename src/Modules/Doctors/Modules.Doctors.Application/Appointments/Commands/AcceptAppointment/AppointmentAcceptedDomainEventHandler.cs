using MassTransit;
using MediatR;
using Modules.Doctors.Domain.DomainEvents;
using Modules.Doctors.IntegrationEvents;

namespace Modules.Doctors.Application.Appointments.Commands.AcceptAppointment;

internal sealed class AppointmentAcceptedDomainEventHandler(IPublishEndpoint publishEndpoint)
    : INotificationHandler<AppointmentAcceptedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task Handle(
        AppointmentAcceptedDomainEvent notification,
        CancellationToken cancellationToken)
        => _publishEndpoint.Publish(new AppointmentAcceptedByDoctorIntegrationEvent
        {
            PatientId = notification.PatientId,
            DoctorShiftId = notification.DoctorShiftId,
            AppointmentPrice = notification.AppointmentPrice
        }, cancellationToken);
}
