using Common.Shared.Abstractions;
using Common.Shared.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Persistence.Configurations;

internal sealed class PatientConfigurations : BaseEntityTypeConfiguration<Patient>
{
    public override void Configure(EntityTypeBuilder<Patient> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.Name)
            .HasMaxLength(DatabaseConstants.MaxLength70);

        builder
            .Property(x => x.Ssn)
            .HasMaxLength(DatabaseConstants.MaxLength11);

        builder
            .Property(x => x.Email)
            .HasMaxLength(DatabaseConstants.MaxLength320);

        builder
            .Property(x => x.Password)
            .HasMaxLength(DatabaseConstants.MaxLength97);

        builder
            .HasIndex(p => p.Ssn)
            .IsUnique();

        builder
            .HasIndex(p => p.Email)
            .IsUnique();

        builder
            .HasMany(p => p.Appointments)
            .WithOne(appointment => appointment.Patient)
            .HasForeignKey(appointment => appointment.PatientId);
    }
}
