using Common.Shared.Abstractions;
using Common.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Persistence.Configurations;

internal sealed class DoctorConfigurations : BaseEntityTypeConfiguration<Doctor>
{
    public override void Configure(EntityTypeBuilder<Doctor> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.Name)
            .HasMaxLength(DatabaseConstants.MaxLength70);

        builder
            .Property(p => p.Ssn)
            .HasMaxLength(DatabaseConstants.MaxLength11);

        builder
            .Property(p => p.Email)
            .HasMaxLength(DatabaseConstants.MaxLength320);

        builder
            .Property(p => p.Password)
            .HasMaxLength(DatabaseConstants.MaxLength97);

        builder
            .HasIndex(p => p.RegistrationNumber)
            .IsUnique();

        builder
            .HasMany(doctor => doctor.Shifts)
            .WithOne(shift => shift.Doctor)
            .HasForeignKey(shift => shift.DoctorId);
    }
}
