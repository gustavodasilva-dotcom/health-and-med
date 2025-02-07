using AutoMapper;
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
            IMapper mapper,
            [FromRoute] Guid id) =>
        {
            var result = await sender.Send(new GetShiftByIdQuery(id));
            if (result is null)
            {
                return Results.NotFound();
            }
            else
            {
                var response = mapper.Map<GetShiftByIdResponse>(result);
                return Results.Ok(response);
            }
        })
        .WithTags(ShiftsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
