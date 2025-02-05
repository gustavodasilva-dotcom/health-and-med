using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Patients.Application.Accesses.Commands.LoginPatient;
using Modules.Patients.Endpoints.Routes;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Patients.Endpoints.Accesses;

public sealed class LoginDoctor : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(AccessesRoutes.LoginPatient,
            [SwaggerOperation(
                Summary = "PT: autenticação do paciente. EN: patient's authentication.",
                Description = @"
                    PT: autentique-se com as credenciais de um paciente para acessar às funcionalides exclusivas para pacientes.
                    EN: authenticate with a patients's credentials to access patient-only features.")]
            [SwaggerResponse(StatusCodes.Status200OK)]
            [SwaggerResponse(StatusCodes.Status400BadRequest)]
            [SwaggerResponse(StatusCodes.Status500InternalServerError)]
            async (
                ISender sender,
                [FromBody] LoginPatientRequest request
            ) =>
        {
            var command = request.Adapt<LoginPatientCommand>();
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            else
            {
                return Results.Ok(result.Value);
            }
        })
        .WithTags(AccessesRoutes.Tags)
        .AllowAnonymous();
    }
}
