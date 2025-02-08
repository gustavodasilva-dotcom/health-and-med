using Carter;
using Common.Shared.Constants;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Patients.Application.Appointments.Queries.GetAppointmentsByPatientId;
using Modules.Patients.Endpoints.Routes;

namespace Modules.Patients.Endpoints.Appointments
{
    public class GetAppointmentsByPatientId : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(AppointmentsRoutes.GetAppointmentsByPatientId, async (
            ISender sender,
            IMapper mapper,
            [FromRoute] Guid id) =>
            {
                var result = await sender.Send(new GetAppointmentsByPatientIdQuery(id));
                if (result is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    var response = mapper.Map<GetAppointmentsByPatientIdResponse>(result);
                    return Results.Ok(response);
                }
            })
            .WithTags(AppointmentsRoutes.Tags)
            .WithMetadata(new AuthorizeAttribute(SecurityPolices.PatientsOnly))
            .RequireAuthorization();
        }
    }
}
