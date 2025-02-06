using MediatR;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Shifts.Queries.GetShiftById;

public sealed record GetShiftByIdQuery(Guid Id) : IRequest<DoctorShift?>;
