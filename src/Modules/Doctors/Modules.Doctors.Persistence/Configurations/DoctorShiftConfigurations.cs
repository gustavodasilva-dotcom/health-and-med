using Common.Shared.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Persistence.Configurations;

internal sealed class DoctorShiftConfigurations
    : BaseEntityTypeConfiguration<DoctorShift>
{
    public override void Configure(EntityTypeBuilder<DoctorShift> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(p => p.Appointment)
            .WithOne()
            .HasForeignKey<DoctorShiftAppointment>(
                appointment => appointment.DoctorShiftId)
            .IsRequired(false);
    }
}
