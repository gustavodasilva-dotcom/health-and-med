using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Patients.Application.Accesses.Commands.RegisterPatient;
using Modules.Patients.Endpoints.Routes;

namespace Modules.Patients.Endpoints.Accesses;

public sealed class RegisterPatient : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AccessesRoutes.RegisterPatient, async (
            ISender sender,
            [FromBody] RegisterPatientRequest request) =>
        {
            var command = request.Adapt<RegisterPatientCommand>();
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            else
            {
                return Results.Created();
            }
        })
        .WithTags(AccessesRoutes.Tags)
        .AllowAnonymous();
    }
}
