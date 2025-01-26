using Common.Shared.Security;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Doctors.CrossCutting.DependencyInjection;

namespace Common.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddProblemDetails();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddDoctors(configuration);

        return services;
    }

    public static void UseApplicationServices(this WebApplication app)
    {
        app.UseStatusCodePages();

        app.UseDoctors();

        app.UseFastEndpoints();
    }
}
