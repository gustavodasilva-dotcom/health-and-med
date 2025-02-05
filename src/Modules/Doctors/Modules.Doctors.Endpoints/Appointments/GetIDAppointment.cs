using Carter;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Appointments.Queries.GetIDAppointment;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Appointments
{
    public sealed class GetIDAppointment : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(AppointmentsRoutes.GetIDAppointment, async (
                ISender sender,
                [FromRoute] Guid Id) =>
            {
                var result = await sender.Send(new GetIDAppointmentQuery(Id));

                if (result is null)
                {
                    return Results.NoContent();
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
}
