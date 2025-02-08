using Common.Shared.Repositories;
using MassTransit;
using Modules.Doctors.IntegrationEvents;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;
using Modules.Patients.Domain.Enums;

namespace Modules.Patients.Application.AppointmentsRegistrations.Events;

public sealed class AppointmentDeniedByDoctorIntegrationEventHandler(
    IRepository<PatientAppointment> patientAppointmentRepository,
    IPatientsUnitOfWork unitOfWork) :
    IConsumer<AppointmentDeniedByDoctorIntegrationEvent>
{
    private readonly IRepository<PatientAppointment> _patientAppointmentRepository
        = patientAppointmentRepository;
    private readonly IPatientsUnitOfWork _unitOfWork = unitOfWork;

    public Task Consume(
        ConsumeContext<AppointmentDeniedByDoctorIntegrationEvent> context)
    {
        AppointmentDeniedByDoctorIntegrationEvent message = context.Message;

        var appointments = _patientAppointmentRepository.Get(app =>
            app.PatientId == message.PatientId && app.DoctorShiftId == message.DoctorShiftId);

        if (!appointments.Any())
        {
            throw new InvalidOperationException(
                "No appointment was found with the given request information.");
        }

        var pendingAppointment = appointments.FirstOrDefault(app =>
            app.Status == AppointmentStatus.Pending);

#pragma warning disable IDE0270 // Use coalesce expression
        if (pendingAppointment is null)
        {
            throw new InvalidOperationException(
                "No pending appointment was found with the given request information.");
        }
#pragma warning restore IDE0270 // Use coalesce expression

        pendingAppointment.DenyByDoctor();

        return _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
