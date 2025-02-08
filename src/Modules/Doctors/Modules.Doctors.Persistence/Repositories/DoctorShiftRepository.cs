using Common.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using System.Linq.Expressions;

namespace Modules.Doctors.Persistence.Repositories;

internal sealed class DoctorShiftRepository(DoctorsDbContext dbContext) :
    Repository<DoctorsDbContext, DoctorShift>(dbContext),
    IDoctorShiftRepository
{
    public override DoctorShift? GetById(Guid id)
        => DbContext.DoctorsShifts
            .Include(shift => shift.Doctor)
            .Include(shift => shift.Appointment)
            .SingleOrDefault(shift => shift.Id == id);

    public override IEnumerable<DoctorShift> Get(
        Expression<Func<DoctorShift, bool>> filter)
        => DbContext.DoctorsShifts
            .Include(shift => shift.Doctor)
            .AsSplitQuery()
            .Where(filter);

    public bool IsShiftAvailableForDoctor(Guid doctorId, DateTime startAt, DateTime endAt)
        => !DbContext.DoctorsShifts.Any(shift =>
            shift.DoctorId == doctorId &&
            startAt >= shift.StartAt && endAt <= shift.EndAt);
}
