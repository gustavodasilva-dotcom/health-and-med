using Carter;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Appointments.Queries.GetAppointments;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Appointments;

public sealed class GetAppointments : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(AppointmentRoutes.GetAppointments, async (
            ISender sender,
            [FromRoute] DateTime from,
            [FromRoute] DateTime to) =>
        {
            await sender.Send(new GetAppointmentsQuery(from, to));

            return Results.NoContent();

            // TODO: implement handling where, if there's no appointments, return a 204 NoContent:
            //
            // if (!result.Any())
            // {
            //     return Results.NoContent();
            // }
            // else
            // {
            //     return Results.Ok(result);
            // }
        })
        .WithTags(AppointmentRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
