using Common.Shared.Repositories;
using Modules.Doctors.Domain.Abstractions;

namespace Modules.Doctors.Persistence.Repositories
{
    internal sealed class AppoinmentUnitOfWork(DoctorsDbContext dbContext) :
    UnitOfWork<DoctorsDbContext>(dbContext),
    IAppoinmentUnitOfWork;
}
