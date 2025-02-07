using Common.Shared;
using MediatR;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Shifts.Commands.UpdateShift;

public sealed record class UpdateShiftCommand(Guid Id, DateTime StartAt, DateTime EndAt)
    : IRequest<Result<DoctorShift>>;
