using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Patients.Application.Accesses.Commands.LoginPatient;
using Modules.Patients.Endpoints.Routes;

namespace Modules.Patients.Endpoints.Accesses;

public sealed class LoginDoctor : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AccessesRoutes.LoginPatient, async (
            ISender sender,
            [FromBody] LoginPatientRequest request) =>
        {
            var command = request.Adapt<LoginPatientCommand>();
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            else
            {
                return Results.Ok(result.Value);
            }
        })
        .WithTags(AccessesRoutes.Tags)
        .AllowAnonymous();
    }
}
