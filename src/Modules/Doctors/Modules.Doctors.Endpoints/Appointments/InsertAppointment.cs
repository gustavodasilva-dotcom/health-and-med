using Carter;
using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Appointments.Commands.Insert;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Appointments
{
    public sealed class InsertAppointment : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(AppointmentsRoutes.InsertAppointments, async (
                ISender sender,
                [FromBody] InsertAppointmentRequest request) =>
            {
                var result = await sender.Send(new AppointmentInsertCommand(
                    request.IdDoctor,
                    request.IdPatient,
                    request.DateFrom,
                    request.DateUntil
                    ));

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
