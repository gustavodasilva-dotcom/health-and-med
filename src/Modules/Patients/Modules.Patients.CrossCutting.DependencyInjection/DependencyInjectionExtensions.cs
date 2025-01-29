using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Patients.Persistence;

namespace Modules.Patients.CrossCutting.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPatients(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddPersistence(configuration);

    public static void UsePatients(this IApplicationBuilder app)
        => app.UsePersistence();
}
