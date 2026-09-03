using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Common.Logging.Extensions;

public static class LoggingServiceCollectionExtensions
{
    public static IServiceCollection AddCommonLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var elasticUrl = configuration.GetSection("ElasticConfiguration:Uri").Value!;
        
        var serviceName = Assembly.GetExecutingAssembly()?.GetName().Name ?? "Unknown";
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl))
            {
                IndexFormat = "marketplace-logs-{0:yyyy.MM.dd}",
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                TemplateName = "marketplace-template",
                NumberOfShards = 1,
                NumberOfReplicas = 0
            })
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ServiceName", serviceName)
            .CreateLogger();

        services.AddSingleton(Log.Logger);

        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog();
        });
        return services;
    }
}