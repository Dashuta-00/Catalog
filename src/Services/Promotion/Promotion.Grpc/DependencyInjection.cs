using System.Data;
using MySqlConnector;
using Promotion.Grpc.Configuration;
using Promotion.Grpc.Persistence.Extensions;
using Promotion.Grpc.Persistence.Interfaces;
using Promotion.Grpc.Persistence.Repositories;
using Promotion.Grpc.Services;

namespace Promotion.Grpc;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mySqlConnectionString = configuration.GetConnectionString("MySqlConnection");
        
        services.AddScoped<IDbConnection>(_ =>
            new MySqlConnection(mySqlConnectionString));
        
        services.AddGrpc();
        services.AddGrpcReflection();
        
        var assembly = typeof(Program).Assembly;
        var licenseKey = configuration.GetSection("MediatR:LicenseKey").Value;

        services.AddMediatR(config =>
        {
            config.LicenseKey = licenseKey;
            config.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped<IPromoRepository, PromoRepository>();
        
        MappingConfig.Configure();
        
        return services;
    }
    
    public static async Task<WebApplication> UseApiServicesAsync(this WebApplication app)
    {
        using var scope  = app.Services.CreateScope();
        var connection = scope.ServiceProvider.GetService<IDbConnection>()!;
        
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        await DatabaseExtensions.SeedAsync(connection, logger);
        app.MapGrpcReflectionService();
        app.MapGrpcService<PromoGrpcService>();
        
        return app;
    }
}