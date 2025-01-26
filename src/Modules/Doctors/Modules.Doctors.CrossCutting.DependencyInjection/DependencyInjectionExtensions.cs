using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Modules.Doctors.Application;
using Modules.Doctors.Endpoints;
using Modules.Doctors.Persistence;

namespace Modules.Doctors.CrossCutting.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDoctors(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddPersistence(configuration)
            .AddApplication()
            .AddEndpoints();

    public static void UseDoctors(this IApplicationBuilder app)
        => app.UsePersistence();
}
