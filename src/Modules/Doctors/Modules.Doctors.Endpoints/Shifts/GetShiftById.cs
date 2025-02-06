using Carter;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Shifts.Queries.GetShiftById;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class GetShiftById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ShiftsRoutes.GetShiftById, async (
            ISender sender,
            [FromRoute] Guid id) =>
        {
            var result = await sender.Send(new GetShiftByIdQuery(id));
            if (result is null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(result);
            }
        })
        .WithTags(ShiftsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
