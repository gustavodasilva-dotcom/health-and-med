using Carter;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Appointments.Commands.Delete;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Appointments
{
    public sealed class DeleteAppointment : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete(AppointmentsRoutes.DeleteAppointments, async (
                ISender sender,
                [FromRoute] Guid Id) =>
            {
                var result = await sender.Send(new AppointmentDeleteCommand(Id));

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
}
