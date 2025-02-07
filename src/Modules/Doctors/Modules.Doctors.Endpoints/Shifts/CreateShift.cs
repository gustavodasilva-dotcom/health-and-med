using Carter;
using Common.Shared.Constants;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Shifts.Commands.CreateShift;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class CreateShift : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ShiftsRoutes.CreateShift, async (
            ISender sender,
            [FromBody] CreateShiftRequest request) =>
        {
            var command = request.Adapt<CreateShiftCommand>();
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
        .WithTags(ShiftsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
