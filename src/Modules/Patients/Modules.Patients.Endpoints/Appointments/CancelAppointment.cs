using Carter;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Patients.Application.Appointments.Commands.CancelAppointment;
using Modules.Patients.Endpoints.Routes;

namespace Modules.Patients.Endpoints.Appointments;

public sealed class CancelAppointment : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch(AppointmentsRoutes.CancelAppointment, async (
            ISender sender,
            [FromRoute] Guid id,
            [FromBody] CancelAppointmentRequest request) =>
        {
            var command = new CancelAppointmentCommand(id, request.Motive);
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            else
            {
                return Results.Ok();
            }
        })
        .WithTags(AppointmentsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.PatientsOnly))
        .RequireAuthorization();
    }
}
