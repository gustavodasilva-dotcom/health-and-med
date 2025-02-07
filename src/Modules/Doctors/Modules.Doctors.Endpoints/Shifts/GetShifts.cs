using AutoMapper;
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
            IMapper mapper,
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
                var response = mapper.Map<IEnumerable<GetShiftsResponse>>(result);                
                return Results.Ok(response);
            }
        })
        .WithTags(ShiftsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
