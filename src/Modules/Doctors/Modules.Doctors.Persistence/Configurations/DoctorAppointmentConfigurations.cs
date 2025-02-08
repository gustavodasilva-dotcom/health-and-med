using Common.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Doctors.Domain.Entities;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Persistence.Configurations;

internal sealed class DoctorShiftAppointmentConfigurations
    : BaseEntityTypeConfiguration<DoctorShiftAppointment>
{
    public override void Configure(
        EntityTypeBuilder<DoctorShiftAppointment> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.Status)
            .HasDefaultValue(AppointmentStatus.PendingDoctorAnalysis);

        builder
            .HasIndex(p => new
            {
                p.DoctorShiftId,
                p.PatientId
            })
            .IsUnique();
    }
}
