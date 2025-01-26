namespace Common.Shared;

public class Result
{
    protected readonly bool _isSuccess;

    public readonly Error? Error;

    protected Result()
    {
        _isSuccess = true;
        Error = default;
    }

    protected Result(Error error)
    {
        _isSuccess = false;
        Error = error;
    }

    public bool IsSuccess => _isSuccess;

    public bool IsFailure => !_isSuccess;

    public static implicit operator Result(Error error)
        => new(error);

    public static Result Success() => new();

    public static Result Failure(Error error)
        => new(error);
}
