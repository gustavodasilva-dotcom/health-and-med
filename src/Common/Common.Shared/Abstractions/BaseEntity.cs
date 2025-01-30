namespace Common.Shared.Abstractions;

public abstract class BaseEntity : DomainEvent, IEquatable<BaseEntity>
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

    public abstract IEnumerable<object> GetAtomicValues();

    private bool ValuesAreEqual(BaseEntity other)
        => GetAtomicValues().SequenceEqual(other.GetAtomicValues());

    public override bool Equals(object? obj)
        => obj is BaseEntity other && ValuesAreEqual(other);

    public override int GetHashCode()
        => GetAtomicValues()
            .Aggregate(
                default(int),
                HashCode.Combine);

    public bool Equals(BaseEntity? other)
        => other is not null && ValuesAreEqual(other);
}
