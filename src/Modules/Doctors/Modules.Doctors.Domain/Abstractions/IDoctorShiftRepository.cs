using Common.Shared.Repositories;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Domain.Abstractions;

public interface IDoctorShiftRepository : IRepository<DoctorShift>
{
    bool IsShiftAvailableForDoctor(Guid doctorId, DateTime startAt, DateTime endAt);
}
