using Common.Shared.Repositories;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Persistence.Repositories;

internal sealed class PatientRepository(PatientsDbContext dbContext) :
    Repository<PatientsDbContext, Patient>(dbContext),
    IPatientRepository
{
    public bool ExistsWithEmail(string email)
        => DbContext.Patients
            .Any(patient => patient.Email == email);

    public bool ExistsWithCpf(string cpf)
        => DbContext.Patients
            .Any(patient => patient.Cpf == cpf);

    public Patient? GetWithEmail(string email)
        => DbContext.Patients
            .SingleOrDefault(patient => patient.Email == email);
}
