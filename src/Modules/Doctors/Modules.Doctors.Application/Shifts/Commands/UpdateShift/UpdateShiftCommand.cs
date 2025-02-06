using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Shifts.Commands.UpdateShift;

public sealed record class UpdateShiftCommand(Guid Id, DateTime StartAt, DateTime EndAt)
    : IRequest<Result>;
