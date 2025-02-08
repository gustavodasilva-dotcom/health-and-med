using Common.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Patients.Domain.Entities;
using Modules.Patients.Persistence.Constants;

namespace Modules.Patients.Persistence;

internal sealed class PatientsDbContext(
    IPublisher publisher,
    DbContextOptions<PatientsDbContext> options
) : BaseDbContext<PatientsDbContext>(publisher, options)
{
    public DbSet<Patient> Patients { get; set; }

    public DbSet<PatientAppointment> PatientsAppointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(PersistenceConstants.DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
