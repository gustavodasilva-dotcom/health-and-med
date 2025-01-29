using Microsoft.EntityFrameworkCore;
using Modules.Patients.Persistence.Constants;

namespace Modules.Patients.Persistence;

internal sealed class PatientsDbContext(DbContextOptions<PatientsDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(PersistenceConstants.DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
