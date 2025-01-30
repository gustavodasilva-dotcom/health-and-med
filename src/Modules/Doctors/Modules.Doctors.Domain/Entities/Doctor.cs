using Common.Shared.Abstractions;
using Modules.Doctors.Domain.Enums;

namespace Modules.Doctors.Domain.Entities;

public sealed class Doctor : UserEntity
{
    public required string Name { get; set; }

    public required string Cpf { get; set; }

    public required UFs CrmUf { get; set; }

    public required int Crm { get; set; }

    public required string Password { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [CrmUf, Crm];

    public void Update(string name, string cpf, UFs crmUf, int crm, string email)
    {
        Name = name.Trim();
        Cpf = cpf.Trim();
        CrmUf = crmUf;
        Crm = crm;
        Email = email.Trim();
        UpdatedAt = DateTime.UtcNow;
    }
}
