using Common.Shared.Repositories;
using Modules.Patients.Domain.Abstractions;

namespace Modules.Patients.Persistence.Repositories;

internal sealed class PatientsUnitOfWork(PatientsDbContext dbContext) :
    UnitOfWork<PatientsDbContext>(dbContext),
    IPatientsUnitOfWork;
