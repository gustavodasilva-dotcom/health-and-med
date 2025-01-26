using Common.Shared.Repositories;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Persistence.Repositories;

internal sealed class DoctorRepository(DoctorsDbContext dbContext) :
    Repository<DoctorsDbContext, Doctor>(dbContext),
    IDoctorRepository
{
    public bool ExistsWithCrmInUf(UFs uf, int crm)
        => DbContext.Doctors
            .Any(doctor => doctor.CrmUf == uf && doctor.Crm == crm);

    public bool ExistsWithEmail(string email)
        => DbContext.Doctors
            .Any(doctor => doctor.Email == email);
}
