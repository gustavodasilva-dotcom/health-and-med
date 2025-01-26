namespace Common.Shared.Abstractions;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;

    public bool IsDeleted { get; private set; } = false;

    public void Delete()
    {
        UpdatedAt = DateTime.UtcNow;
        IsDeleted = true;
    }
}
