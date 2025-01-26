namespace Common.Shared;

public sealed class Result<TValue> : Result
{
    public readonly TValue? Value;

    private Result(TValue value) : base()
        => Value = value;

    private Result(Error error) : base(error)
        => Value = default;

    public static implicit operator Result<TValue>(TValue value)
        => new(value);

    public static implicit operator Result<TValue>(Error error)
        => new(error);

    public Result<TValue> Match(
        Func<TValue, Result<TValue>> success,
        Func<Error, Result<TValue>> failure)
    {
        if (_isSuccess)
        {
            return success(Value!);
        }

        return failure(Error!);
    }
}
