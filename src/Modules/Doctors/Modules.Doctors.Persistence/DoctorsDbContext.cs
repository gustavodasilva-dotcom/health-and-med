using Common.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Persistence.Constants;

namespace Modules.Doctors.Persistence;

internal sealed class DoctorsDbContext(
    IPublisher publisher,
    DbContextOptions<DoctorsDbContext> options
) : BaseDbContext<DoctorsDbContext>(publisher, options)
{
    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<DoctorShift> DoctorsShifts { get; set; }

    public DbSet<DoctorShiftAppointment> DoctorsShiftsAppointments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(PersistenceConstants.DefaultSchema);

        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
