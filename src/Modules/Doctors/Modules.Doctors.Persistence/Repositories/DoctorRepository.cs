using System.Linq.Expressions;
using Common.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Persistence.Repositories;

internal sealed class DoctorRepository(DoctorsDbContext dbContext) :
    Repository<DoctorsDbContext, Doctor>(dbContext),
    IDoctorRepository
{
    public override IEnumerable<Doctor> Get(Expression<Func<Doctor, bool>> filter)
        => DbContext.Doctors
            .Include(doctor => doctor.Registrations)
            .Where(filter);

    public override Doctor? GetById(Guid id)
        => DbContext.Doctors
            .Include(doctor => doctor.Registrations)
            .FirstOrDefault(doctor => doctor.Id == id);

    public bool ExistsWithEmail(string email)
        => DbContext.Doctors
            .Any(doctor => doctor.Email == email);

    public bool ExistsWithSsn(string ssn)
        => DbContext.Doctors
            .Any(doctor => doctor.Ssn == ssn);

    public bool IsRegisteredInState(int number, UFs state)
        => DbContext.Doctors
            .Include(doctor => doctor.Registrations)
            .Any(doctor => doctor.Registrations
                .Any(reg => reg.Number == number && reg.State == state));

    public Doctor? GetWithEmail(string email)
        => DbContext.Doctors
            .SingleOrDefault(doctor => doctor.Email == email);
}
