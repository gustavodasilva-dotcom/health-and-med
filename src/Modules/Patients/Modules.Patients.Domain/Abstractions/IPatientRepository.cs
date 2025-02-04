using Common.Shared.Repositories;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Domain.Abstractions;

public interface IPatientRepository : IRepository<Patient>
{
    bool ExistsWithEmail(string email);

    bool ExistsWithCpf(string cpf);

    Patient? GetWithEmail(string email);
}
