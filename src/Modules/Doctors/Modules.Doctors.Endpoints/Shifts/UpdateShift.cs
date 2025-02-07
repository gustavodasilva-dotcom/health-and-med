using Carter;
using Common.Shared.Constants;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Shifts.Commands.UpdateShift;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Shifts;

public sealed class UpdateShift : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ShiftsRoutes.UpdateShift, async (
            ISender sender,
            IMapper mapper,
            [FromRoute] Guid id,
            [FromBody] UpdateShiftRequest request) =>
        {
            var command = mapper.Map<UpdateShiftCommand>((id, request));
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            else
            {
                var response = result.Value.Adapt<UpdateShiftResponse>();
                return Results.Ok(response);
            }
        })
        .WithTags(ShiftsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
