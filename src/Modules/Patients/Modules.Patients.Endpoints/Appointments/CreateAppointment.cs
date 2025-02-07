using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Patients.Application.Appointments.Commands.RegisterAppointment;
using Modules.Patients.Endpoints.Routes;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Patients.Endpoints.Appointments
{
    public sealed class CreateAppointment : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(AppointmentsRoutes.CreateAppointment,
            [SwaggerOperation(
                Summary = "PT: Agendamento de consulta do paciente. EN: Patient's  appointment registration.",
                Description = @"
                    PT: Agende uma nova consulta ao paciente, informando dados do paciente, médico, data e hora.
                    EN: Schedule a new appointment for the patient, informing date, time and both patient and doctor data.")]
            [SwaggerResponse(StatusCodes.Status201Created)]
            [SwaggerResponse(StatusCodes.Status400BadRequest)]
            [SwaggerResponse(StatusCodes.Status500InternalServerError)]
            async (
                ISender sender,
                [FromBody] CreateAppointmentRequest request
            ) =>
            {
                var command = request.Adapt<CreateAppointmentCommand>();
                var result = await sender.Send(command);
                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                else
                {
                    return Results.Created();
                }
            })
            .WithTags(AppointmentsRoutes.Tags)
            .RequireAuthorization();
        }
    }
}
