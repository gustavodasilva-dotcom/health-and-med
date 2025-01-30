using Common.Shared.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.DependencyInjection.Extensions;

internal static class MessagingExtensions
{
    internal static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumers(
                Modules.Doctors.Application.AssemblyReference.Assembly,
                Modules.Patients.Application.AssemblyReference.Assembly);

            busConfigurator.SetKebabCaseEndpointNameFormatter();

            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var messagingSettings = configuration
                .GetSection(MessageBrokerOptions.Position)
                .Get<MessageBrokerOptions>() ??
                throw new InvalidOperationException($"Missing configuration for {MessageBrokerOptions.Position}.");

            busConfigurator.AddConfigureEndpointsCallback((context, name, configurator) =>
            {
                configurator.UseMessageRetry(retryFilter => retryFilter
                    .Immediate(messagingSettings.NumberOfRetries));

                KillSwitchOptions killSwitchSettings = messagingSettings.KillSwitch;

                configurator.UseKillSwitch(config => config
                    .SetActivationThreshold(killSwitchSettings.ActivationThreshold)
                    .SetTripThreshold(killSwitchSettings.TripThreshold)
                    .SetRestartTimeout(m: killSwitchSettings.RestartMinutesTimeout));
            });

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(messagingSettings.Host, host =>
                {
                    host.Username(messagingSettings.Username);
                    host.Password(messagingSettings.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
