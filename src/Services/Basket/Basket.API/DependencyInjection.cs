using Basket.API.Infrastructure;
using Basket.API.Models.ShoppingCart;
using Basket.API.Validation;
using Carter;
using Common.Kernel.Behaviors;
using Common.Kernel.Exceptions.Handler;
using FluentValidation;
using Marten;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        //автоматическое описание эндпоинтов для swagger
        services.AddEndpointsApiExplorer();
        //генерация swagger документации
        
        services.AddSwaggerGen();
        
        services.AddValidatorsFromAssemblyContaining(typeof(Program));
        var redisConnectionString = configuration.GetConnectionString("RedisConnection");
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "Cart";
        });
        // services.AddScoped<CartRepository>();
        // services.AddScoped<ICartRepository>(provider => 
        //     new RedisCartCacheRepository(
        //     provider.GetRequiredService<CartRepository>(),
        //     provider.GetRequiredService<IDistributedCache>()
        //     ));
        
        services.AddScoped<ICartRepository, CartRepository>();
        services.Decorate<ICartRepository, RedisCartCacheRepository>();
        
        services.AddCarter();
        
        var assembly = typeof(Program).Assembly;
        var licenseKey = configuration.GetSection("MediatR:LicenseKey").Value;

        services.AddMediatR(config =>
        {
             config.LicenseKey = licenseKey;
             config.RegisterServicesFromAssembly(assembly);
             config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(assembly);
        
        var connectionString = configuration.GetConnectionString("PgConnection")!;

        services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.Schema.For<ShoppingCart>().Identity(x => x.AccountName);
            }).UseLightweightSessions();
        
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseExceptionHandler(options => { });
        app.MapCarter();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API v1");
        });
        
        return app;
    }
}