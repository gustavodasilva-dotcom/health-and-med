using MediatR;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Shifts.Queries.GetShiftsByDoctor;

public sealed record GetShiftsByDoctorQuery(Guid DoctorId)
    : IRequest<IEnumerable<DoctorShift>>;
