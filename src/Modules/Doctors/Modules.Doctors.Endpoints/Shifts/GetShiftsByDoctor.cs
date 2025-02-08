using Carter;
using Common.Shared.Constants;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Shifts.Queries.GetShiftsByDoctor;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class GetShiftsByDoctor : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ShiftsRoutes.GetShiftsByDoctor, async (
            ISender sender,
            IMapper mapper,
            [FromRoute] Guid doctorId) =>
        {
            var result = await sender.Send(new GetShiftsByDoctorQuery(doctorId));
            if (!result.Any())
            {
                return Results.NoContent();
            }
            else
            {
                var response = mapper
                    .Map<IEnumerable<GetShiftsByDoctorResponse>>(result);
                return Results.Ok(response);
            }
        })
        .WithTags(ShiftsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
