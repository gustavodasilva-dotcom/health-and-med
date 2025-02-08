using Common.Shared;
using MediatR;

namespace Modules.Patients.Application.Appointments.Commands.CreateAppointment;

public sealed record CreateAppointmentCommand(Guid PatientId, Guid DoctorShiftId)
    : IRequest<Result>;
