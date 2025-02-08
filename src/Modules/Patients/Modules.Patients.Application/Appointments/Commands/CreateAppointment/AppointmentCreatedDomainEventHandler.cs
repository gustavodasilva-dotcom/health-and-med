using Common.Shared.Repositories;
using MassTransit;
using MediatR;
using Modules.Patients.Domain.DomainEvents;
using Modules.Patients.Domain.Entities;
using Modules.Patients.IntegrationEvents;

namespace Modules.Patients.Application.Appointments.Commands.CreateAppointment;

internal sealed class AppointmentCreatedDomainEventHandler(
    IRepository<PatientAppointment> patientAppointmentRepository,
    IPublishEndpoint publishEndpoint) :
    INotificationHandler<AppointmentCreatedDomainEvent>
{
    private readonly IRepository<PatientAppointment> _patientAppointmentRepository
        = patientAppointmentRepository;

    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task Handle(
        AppointmentCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var appointments = _patientAppointmentRepository.Get(p =>
            p.PatientId == notification.PatientId &&
            p.DoctorShiftId == notification.DoctorShiftId);

        var lastAppointment = appointments.Last();

        return _publishEndpoint.Publish(new AppointmentCreatedIntegrationEvent
        {
            AppointmentId = lastAppointment.Id,
            DoctorShiftId = notification.DoctorShiftId,
            PatientId = notification.PatientId
        }, cancellationToken);
    }
}
