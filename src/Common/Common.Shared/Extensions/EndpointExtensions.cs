using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Common.Shared.Extensions;

public static class EndpointExtensions
{
    public static Task SendResultAsync(
        this IEndpoint endpoint,
        Result result,
        CancellationToken cancellationToken = default)
    {
        result.Error!.Deconstruct(out _, out string? message, out int statusCode);

        endpoint.HttpContext.Response.StatusCode = statusCode;

        endpoint.HttpContext.Response.WriteAsJsonAsync(
            new ProblemDetails
            {
                Instance = endpoint.HttpContext.Request.Path,
                Status = statusCode,
                Detail = message
            }, cancellationToken);

        return endpoint.HttpContext.Response.StartAsync(cancellationToken);
    }

    public static Task SendCreatedAsync(
        this IEndpoint endpoint,
        CancellationToken cancellationToken = default)
    {
        endpoint.HttpContext.Response.StatusCode = StatusCodes.Status201Created;

        return endpoint.HttpContext.Response.StartAsync(cancellationToken);
    }
}
