namespace Common.Shared.Abstractions;

public abstract class UserEntity : BaseEntity
{
    public required string Email { get; set; }
}
