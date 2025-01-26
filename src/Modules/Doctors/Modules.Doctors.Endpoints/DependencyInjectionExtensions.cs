using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Doctors.Endpoints;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
        => services.AddFastEndpoints(options =>
            options.Assemblies = [AssemblyReference.Assembly]);
}
