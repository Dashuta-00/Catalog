using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Messaging.Extensions;

public static class MassTransitConfigurationExtension
{
    public static IServiceCollection AddBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly is not null)
            {
                config.AddConsumers(assembly);
            }
            
            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["Broker:Host"]!), host =>
                {
                    host.Username(configuration["Broker:UserName"]!);
                    host.Password(configuration["Broker:Password"]!);
                });
                
                configurator.UseMessageRetry(retry =>
                {
                    retry.Interval(5, TimeSpan.FromSeconds(5));
                });
                
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}