using Carter;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Shifts.Queries.GetShifts;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class GetShifts : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ShiftsRoutes.GetShifts, async (
            ISender sender,
            [FromRoute] DateTime from,
            [FromRoute] DateTime to) =>
        {
            var result = await sender.Send(new GetShiftsQuery(from, to));
            if (!result.Any())
            {
                return Results.NoContent();
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
