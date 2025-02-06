using Common.Shared.Repositories;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Domain.Abstractions;

public interface IDoctorShiftRepository : IRepository<DoctorShift>
{
    bool IsShiftAvailable(DateTime startAt, DateTime endAt);
}
