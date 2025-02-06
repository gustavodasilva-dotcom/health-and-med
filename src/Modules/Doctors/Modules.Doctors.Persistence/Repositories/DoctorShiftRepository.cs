using Common.Shared.Repositories;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Persistence.Repositories;

internal sealed class DoctorShiftRepository(DoctorsDbContext dbContext) :
    Repository<DoctorsDbContext, DoctorShift>(dbContext),
    IDoctorShiftRepository
{
    public bool IsShiftAvailable(DateTime startAt, DateTime endAt)
        => !DbContext.DoctorsShifts
            .Any(shift => startAt >= shift.StartAt && endAt <= shift.EndAt);
}
