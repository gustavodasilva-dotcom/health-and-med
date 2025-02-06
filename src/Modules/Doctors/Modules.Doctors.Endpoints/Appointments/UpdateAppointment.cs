using Carter;
using Common.Shared.Constants;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Appointments.Commands.UpdateAppointment;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Appointments;

public sealed class UpdateAppointment : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(AppointmentsRoutes.UpdateAppointments, async (
            ISender sender,
            IMapper mapper,
            [FromRoute] Guid id,
            [FromBody] UpdateAppointmentRequest request) =>
        {
            var command = mapper.Map<UpdateAppointmentCommand>((id, request));
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            else
            {
                return Results.Ok(result);
            }
        })
        .WithTags(AppointmentsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
