using Common.Shared.Abstractions;
using Common.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Patients.Domain.Entities;
using Modules.Patients.Domain.Enums;

namespace Modules.Patients.Persistence.Configurations;

internal sealed class PatientAppointmentConfiguration
    : BaseEntityTypeConfiguration<PatientAppointment>
{
    public override void Configure(
        EntityTypeBuilder<PatientAppointment> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.Status)
            .HasDefaultValue(AppointmentStatus.Pending);

        builder
            .Property(p => p.AnalysisMessage)
            .HasMaxLength(DatabaseConstants.MaxLength255)
            .IsRequired(false);

        builder
            .Property(p => p.AcceptedAt)
            .IsRequired(false);

        builder
            .Property(p => p.AppointmentPrice)
            .HasPrecision(18, 2);

        builder
            .Property(p => p.CancelledAt)
            .IsRequired(false);

        builder
            .Property(p => p.CancellationMotive)
            .HasMaxLength(DatabaseConstants.MaxLength255)
            .IsRequired(false);
    }
}
