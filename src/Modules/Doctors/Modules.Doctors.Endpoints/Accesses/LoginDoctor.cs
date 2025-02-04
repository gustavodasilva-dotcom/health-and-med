using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Accesses.Commands.LoginDoctor;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Accesses;

public sealed class LoginDoctor : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AccessesRoutes.LoginDoctor, async (
            ISender sender,
            [FromBody] LoginDoctorRequest request) =>
        {
            var command = request.Adapt<LoginDoctorCommand>();
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
