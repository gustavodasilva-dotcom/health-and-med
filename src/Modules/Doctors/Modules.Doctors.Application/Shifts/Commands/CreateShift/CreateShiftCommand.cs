using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Shifts.Commands.CreateShift;

public sealed record CreateShiftCommand(Guid DoctorId, DateTime StartAt, DateTime EndAt)
    : IRequest<Result>;
