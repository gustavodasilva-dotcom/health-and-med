using Common.Shared.Abstractions;

namespace Modules.Patients.Domain.Entities;

public sealed class Patient : UserEntity
{
    public required string Name { get; set; }

    public required string Ssn { get; set; }

    public required string Password { get; set; }

    public override IEnumerable<object> GetAtomicValues() => [Ssn];

    public void Update(string name, string cpf, string email)
    {
        Name = name.Trim();
        Ssn = cpf.Trim();
        Email = email.Trim();
        UpdatedAt = DateTime.UtcNow;
    }
}
