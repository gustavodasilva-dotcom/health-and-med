using Common.Shared.Repositories;
using MassTransit;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;
using Modules.Doctors.IntegrationEvents;
using Modules.Patients.IntegrationEvents;

namespace Modules.Doctors.Application.ShiftsRegistrations.Events;

public class AppointmentCreatedIntegrationEventHandler(
    IDoctorShiftRepository doctorShiftRepository,
    IRepository<DoctorShiftAppointment> doctorShiftAppointmentRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IConsumer<AppointmentCreatedIntegrationEvent>
{
    private readonly IDoctorShiftRepository _doctorShiftRepository = doctorShiftRepository;
    private readonly IRepository<DoctorShiftAppointment> _doctorShiftAppointmentRepository
        = doctorShiftAppointmentRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public Task Consume(
        ConsumeContext<AppointmentCreatedIntegrationEvent> context)
    {
        AppointmentCreatedIntegrationEvent message = context.Message;

        var doctorShift = _doctorShiftRepository.GetById(message.DoctorShiftId);
        if (doctorShift is null)
        {
            return context.Publish(new AppointmentDeniedBySystemIntegrationEvent
            {
                PatientAppointmentId = message.AppointmentId,
                Motive = "The required appointment was not found."
            });
        }

        if (doctorShift.Appointment is not null)
        {
            return context.Publish(new AppointmentDeniedBySystemIntegrationEvent
            {
                PatientAppointmentId = message.AppointmentId,
                Motive = "There's already a scheduled appointment at this time."
            });
        }

        var shiftAppointment = new DoctorShiftAppointment
        {
            PatientId = message.PatientId,
            Status = AppointmentStatus.PendingDoctorAnalysis
        };
        doctorShift.AddAppointment(shiftAppointment);

        _doctorShiftAppointmentRepository.Add(shiftAppointment);

        return _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
