using Common.Shared.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Patients.Domain.Entities;

namespace Modules.Patients.Persistence.Configurations
{
    internal sealed class PatientAppointmentConfiguration : BaseEntityTypeConfiguration<PatientAppointment>
    {
        public override void Configure(EntityTypeBuilder<PatientAppointment> builder)
        {
            base.Configure(builder);
        }
    }
}
