using Common.Shared;
using MediatR;

namespace Modules.Doctors.Application.Shifts.Commands.DeleteShift;

public sealed record class DeleteShiftCommand(Guid Id) : IRequest<Result>;
