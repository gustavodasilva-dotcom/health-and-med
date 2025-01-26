using Common.Shared.Repositories;
using Modules.Doctors.Domain.Abstractions;

namespace Modules.Doctors.Persistence.Repositories;

internal sealed class DoctorsUnitOfWork(DoctorsDbContext dbContext) :
    UnitOfWork<DoctorsDbContext>(dbContext),
    IDoctorsUnitOfWork;
