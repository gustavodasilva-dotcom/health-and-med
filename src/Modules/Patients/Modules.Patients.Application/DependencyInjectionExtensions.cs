using Microsoft.Extensions.DependencyInjection;

namespace Modules.Patients.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly));
}
