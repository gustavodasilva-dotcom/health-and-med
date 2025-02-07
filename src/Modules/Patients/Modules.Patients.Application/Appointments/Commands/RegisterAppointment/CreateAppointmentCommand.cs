using Common.Shared;
using MediatR;

namespace Modules.Patients.Application.Appointments.Commands.RegisterAppointment
{
    public sealed record CreateAppointmentCommand(Guid PatientId, Guid DoctorId, DateTime StartAt, DateTime EndAt)
        : IRequest<Result>;
}
