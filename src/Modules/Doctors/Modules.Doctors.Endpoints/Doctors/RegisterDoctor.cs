using Common.Shared.Extensions;
using FastEndpoints;
using MediatR;
using Modules.Doctors.Application.Doctors.Commands.RegisterDoctor;
using Modules.Doctors.Endpoints.Routes;

namespace Modules.Doctors.Endpoints.Doctors;

public sealed class RegisterDoctor(ISender sender) : Endpoint<RegisterDoctorCommand>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Post(DoctorRoutes.RegisterDoctor);
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        RegisterDoctorCommand req,
        CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);
        if (result.IsFailure)
        {
            await this.SendResultAsync(result, ct);
        }
        else
        {
            await this.SendCreatedAsync(ct);
        }
    }
}
