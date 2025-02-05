using Carter;
using Common.Shared.Constants;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Registrations.Commands.AddRegistration;
using Modules.Doctors.Endpoints.Routes;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Doctors.Endpoints.Registrations;

public sealed class AddRegistration : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(RegistrationsRoutes.AddRegistration,
            [SwaggerOperation(
                Summary = "PT: cadastrar CRM. EN: register doctor's registration.",
                Description = @"
                    PT: cadastre um novo CRM a um cadastro de médico já existente no sistema.
                    EN: register a new doctor's registration to an existing doctor in the system.")]
            [SwaggerResponse(StatusCodes.Status201Created)]
            [SwaggerResponse(StatusCodes.Status400BadRequest)]
            [SwaggerResponse(StatusCodes.Status500InternalServerError)]
            async (
                ISender sender,
                IMapper mapper,
                [FromRoute] Guid doctorId,
                [FromBody] AddRegistrationRequest request
            ) =>
        {
            var command = mapper.Map<AddRegistrationCommand>((doctorId, request));
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
        .WithTags(RegistrationsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.DoctorsOnly))
        .RequireAuthorization();
    }
}
