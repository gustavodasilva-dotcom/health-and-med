using Common.Shared.Repositories;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Domain.Abstractions;

public interface IDoctorRepository : IRepository<Doctor>
{
    bool ExistsWithEmail(string email);

    bool ExistsWithSsn(string ssn);

    bool IsRegisteredInState(int number, UFs state);

    Doctor? GetWithEmail(string email);
}
