using Carter;
using Common.Shared.Constants;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Doctors.Application.Doctors.Queries.GetDoctors;
using Modules.Doctors.Domain.Enums;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Doctors;

public sealed class GetDoctors : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(DoctorsRoutes.GetDoctors, async (
            ISender sender,
            IMapper mapper,
            [FromQuery] string? name,
            [FromQuery] MedicalSpecialties? specialty) =>
        {
            var query = mapper.Map<GetDoctorsQuery>((name, specialty));
            var result = await sender.Send(query);
            if (!result.Any())
            {
                return Results.NoContent();
            }
            else
            {
                var response = mapper.Map<IEnumerable<GetDoctorsResponse>>(result);
                return Results.Ok(response);
            }
        })
        .WithTags(DoctorsRoutes.Tags)
        .WithMetadata(new AuthorizeAttribute(SecurityPolices.PatientsOnly))
        .RequireAuthorization();
    }
}
