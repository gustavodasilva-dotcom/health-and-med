using Common.Shared.Repositories;
using MassTransit;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Modules.Patients.IntegrationEvents;

namespace Modules.Doctors.Application.ShiftsRegistrations.Events;

public class AppointmentCancelledIntegrationEventHandler(
    IDoctorShiftRepository doctorShiftRepository,
    IRepository<DoctorShiftAppointment> doctorShiftAppointmentRepository,
    IDoctorsUnitOfWork unitOfWork) :
    IConsumer<AppointmentCancelledIntegrationEvent>
{
    private readonly IDoctorShiftRepository _doctorShiftRepository = doctorShiftRepository;
    private readonly IRepository<DoctorShiftAppointment> _doctorShiftAppointmentRepository
        = doctorShiftAppointmentRepository;
    private readonly IDoctorsUnitOfWork _unitOfWork = unitOfWork;

    public Task Consume(
        ConsumeContext<AppointmentCancelledIntegrationEvent> context)
    {
        AppointmentCancelledIntegrationEvent message = context.Message;

        var doctorShift = _doctorShiftRepository.GetById(message.DoctorShiftId)
            ?? throw new InvalidOperationException("The required appointment was not found.");

        if (doctorShift.Appointment is null)
        {
            throw new InvalidOperationException(
                "There isn't a scheduled appointment for this shift.");
        }

        _doctorShiftAppointmentRepository.Remove(doctorShift.Appointment);

        return _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
