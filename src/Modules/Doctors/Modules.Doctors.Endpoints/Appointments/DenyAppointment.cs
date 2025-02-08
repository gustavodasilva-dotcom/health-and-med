using Carter;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Appointments.Commands.DenyAppointment;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Appointments;

public sealed class DenyAppointment : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch(AppointmentsRoutes.DenyAppointment, async (
            ISender sender,
            [FromRoute] Guid appointmentId) =>
        {
            var result = await sender.Send(new DenyAppointmentCommand(appointmentId));
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            else
            {
                return Results.Ok();
            }
        })
        .WithTags(AppointmentsRoutes.DenyAppointment)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
