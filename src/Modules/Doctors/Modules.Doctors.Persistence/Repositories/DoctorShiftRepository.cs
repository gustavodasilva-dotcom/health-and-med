using System.Linq.Expressions;
using Common.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
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

    public override DoctorShift? GetById(Guid id)
        => DbContext.DoctorsShifts
            .Include(shift => shift.Registration)
                .ThenInclude(registration => registration.Doctor)
            .SingleOrDefault(shift => shift.Id == id);

    public override IEnumerable<DoctorShift> Get(Expression<Func<DoctorShift, bool>> filter)
        => DbContext.DoctorsShifts
            .Include(shift => shift.Registration)
                .ThenInclude(registration => registration.Doctor)
            .AsSplitQuery()
            .Where(filter);
}
