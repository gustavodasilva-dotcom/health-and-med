using Common.Shared.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Persistence.Configurations;

internal sealed class DoctorRegistrationConfigurations
    : BaseEntityTypeConfiguration<DoctorRegistration>
{
    public override void Configure(EntityTypeBuilder<DoctorRegistration> builder)
    {
        base.Configure(builder);

        builder
            .HasMany(registration => registration.Schedules)
            .WithOne(schedule => schedule.Registration)
            .HasForeignKey(schedule => schedule.DoctorRegistrationId);

        builder
            .HasIndex(p => new
            {
                p.State,
                p.Number
            })
            .IsUnique();
    }
}
