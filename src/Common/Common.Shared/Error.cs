using Microsoft.AspNetCore.Http;

namespace Common.Shared;

public record Error(string Title, string? Message = null, int StatusCode = StatusCodes.Status400BadRequest)
{
    public static readonly Error None = new(string.Empty);
}
