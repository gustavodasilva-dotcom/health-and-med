using Common.Shared.Abstractions;
using Common.Shared.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Doctors.Domain.Entities;

namespace Modules.Doctors.Persistence.Configurations;

internal sealed class DoctorConfigurations : BaseEntityTypeConfiguration<Doctor>
{
    public override void Configure(EntityTypeBuilder<Doctor> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.Name)
            .HasMaxLength(DatabaseConstants.MaxLength70);

        builder
            .Property(x => x.Cpf)
            .HasMaxLength(DatabaseConstants.MaxLength11);

        builder
            .Property(x => x.Email)
            .HasMaxLength(DatabaseConstants.MaxLength320);

        builder
            .Property(x => x.Password)
            .HasMaxLength(DatabaseConstants.MaxLength97);

        builder
            .HasIndex(p => new
            {
                p.CrmUf,
                p.Crm
            })
            .IsUnique();
    }
}
