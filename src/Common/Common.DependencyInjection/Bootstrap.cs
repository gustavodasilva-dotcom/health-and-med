using Carter;
using Common.Shared.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Doctors.CrossCutting.DependencyInjection;
using Modules.Patients.CrossCutting.DependencyInjection;

namespace Common.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddProblemDetails();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<TokenProvider>();

        services
            .AddDoctors(configuration)
            .AddPatients(configuration);

        services.AddCarter(
            new DependencyContextAssemblyCatalog(
                [Modules.Doctors.Endpoints.AssemblyReference.Assembly,
                Modules.Patients.Endpoints.AssemblyReference.Assembly]));

        return services;
    }

    public static void UseApplicationServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseStatusCodePages();

        app.UseDoctors();
        app.UsePatients();
    }
}
