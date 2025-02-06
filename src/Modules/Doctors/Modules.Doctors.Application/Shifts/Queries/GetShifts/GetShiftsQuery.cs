using MediatR;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Application.Shifts.Queries.GetShifts;

public sealed record GetShiftsQuery(DateTime FromDate, DateTime ToDate)
    : IRequest<IEnumerable<DoctorShift>>;
