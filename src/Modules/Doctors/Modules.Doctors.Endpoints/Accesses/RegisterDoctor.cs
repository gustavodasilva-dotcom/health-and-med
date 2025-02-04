using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Accesses.Commands.RegisterDoctor;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Accesses;

public sealed class RegisterDoctor : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AccessesRoutes.RegisterDoctor, async (
            ISender sender,
            [FromBody] RegisterDoctorRequest request) =>
        {
            var command = request.Adapt<RegisterDoctorCommand>();
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
