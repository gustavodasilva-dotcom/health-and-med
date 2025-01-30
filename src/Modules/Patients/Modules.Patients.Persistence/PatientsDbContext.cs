using Common.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Patients.Persistence.Constants;

namespace Modules.Patients.Persistence;

internal sealed class PatientsDbContext(
    IPublisher publisher,
    DbContextOptions<PatientsDbContext> options
) : BaseDbContext<PatientsDbContext>(publisher, options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(PersistenceConstants.DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
