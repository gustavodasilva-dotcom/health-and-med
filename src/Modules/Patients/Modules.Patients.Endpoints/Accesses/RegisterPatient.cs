using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Patients.Application.Accesses.Commands.RegisterPatient;
using Modules.Patients.Endpoints.Routes;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Patients.Endpoints.Accesses;

public sealed class RegisterPatient : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AccessesRoutes.RegisterPatient,
            [SwaggerOperation(
                Summary = "PT: cadastro de paciente. EN: patient's registration.",
                Description = @"
                    PT: cadastre um novo paciente, informando tanto credenciais de acesso quanto informações pessoais.
                    EN: register a new patient, informing both access credentials and personal information.")]
            [SwaggerResponse(StatusCodes.Status201Created)]
            [SwaggerResponse(StatusCodes.Status400BadRequest)]
            [SwaggerResponse(StatusCodes.Status500InternalServerError)]
            async (
                ISender sender,
                [FromBody] RegisterPatientRequest request
            ) =>
        {
            var command = request.Adapt<RegisterPatientCommand>();
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
        .WithTags(AccessesRoutes.Tags)
        .AllowAnonymous();
    }
}
