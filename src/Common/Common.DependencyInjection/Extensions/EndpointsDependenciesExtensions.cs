using System.Reflection;
using Carter;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Common.DependencyInjection.Extensions;

internal static class EndpointsDependenciesExtensions
{
    internal static IServiceCollection AddEndpointsDependencies(
        this IServiceCollection services)
    {
        services.AddMapster();
        services.AddScoped<IMapper, ServiceMapper>();

        Assembly[] endpointsAssemblies =
        [
            Modules.Doctors.Endpoints.AssemblyReference.Assembly,
            Modules.Patients.Endpoints.AssemblyReference.Assembly
        ];

        services.AddCarter(
            new DependencyContextAssemblyCatalog(endpointsAssemblies));

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Modules.Doctors.Endpoints.AssemblyReference.Assembly);

        services.AddSingleton(config);

        return services;
    }
}
