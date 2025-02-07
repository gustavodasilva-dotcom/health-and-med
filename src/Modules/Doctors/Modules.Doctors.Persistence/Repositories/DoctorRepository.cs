using Common.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Persistence.Repositories;

internal sealed class DoctorRepository(DoctorsDbContext dbContext) :
    Repository<DoctorsDbContext, Doctor>(dbContext),
    IDoctorRepository
{
    public override Doctor? GetById(Guid id)
        => DbContext.Doctors
            .Include(doctor => doctor.Shifts)
            .FirstOrDefault(doctor => doctor.Id == id);

    public bool IsRegistrationNumberInUse(int number)
        => DbContext.Doctors.Any(reg => reg.RegistrationNumber == number);

    public Doctor? GetByRegistrationNumber(int number)
        => DbContext.Doctors
            .SingleOrDefault(doctor => doctor.RegistrationNumber == number);
}
