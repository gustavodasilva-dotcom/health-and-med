using Common.Shared.Repositories;
using MassTransit;
using Modules.Doctors.IntegrationEvents;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;
using Modules.Patients.Domain.Enums;

namespace Modules.Patients.Application.AppointmentsRegistrations.Events;

public class AppointmentDeniedBySystemIntegrationEventHandler(
    IRepository<PatientAppointment> patientAppointmentRepository,
    IPatientsUnitOfWork unitOfWork) :
    IConsumer<AppointmentDeniedBySystemIntegrationEvent>
{
    private readonly IRepository<PatientAppointment> _patientAppointmentRepository
        = patientAppointmentRepository;
    private readonly IPatientsUnitOfWork _unitOfWork = unitOfWork;

    public Task Consume(
        ConsumeContext<AppointmentDeniedBySystemIntegrationEvent> context)
    {
        AppointmentDeniedBySystemIntegrationEvent message = context.Message;

        var appointment = _patientAppointmentRepository.GetById(message.PatientAppointmentId);

#pragma warning disable IDE0270 // Use coalesce expression
        if (appointment is null)
        {
            throw new InvalidOperationException("No appointment was found.");
        }
#pragma warning restore IDE0270 // Use coalesce expression

        if (appointment.Status != AppointmentStatus.Pending)
        {
            throw new InvalidOperationException(
                "The appointment should be pending analysis, but was found in a different state.");
        }

        appointment.SetAnalysisResult(
            status: AppointmentStatus.AppointmentDeniedBySystem,
            message: message.Motive);

        return _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
