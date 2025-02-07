using Common.Shared.Repositories;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Domain.Abstractions;

public interface IDoctorRepository : IRepository<Doctor>
{
    bool IsRegistrationNumberInUse(int number);

    Doctor? GetByRegistrationNumber(int number);
}
