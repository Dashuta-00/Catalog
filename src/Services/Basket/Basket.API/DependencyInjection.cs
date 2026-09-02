using Basket.API.Infrastructure;
using Basket.API.Models.ShoppingCart;
using Carter;
using Common.Kernel.Behaviors;
using Common.Kernel.Exceptions.Handler;
using Common.Kernel.OpenApi.Filters;
using Common.Messaging.Extensions;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Marten;
using Promotion.Grpc.Protos;
using Scalar.AspNetCore;

namespace Basket.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddEndpointsApiExplorer();

        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<SnakeCaseSchemaTransformer>();
        });
        
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

        var promotionService = configuration.GetSection("GrpcServices:PromotionService").Value!;
        services.AddGrpcClient<PromoService.PromoServiceClient>(options =>
        {
            options.Address =  new Uri(promotionService);
        });

        services.AddBroker(configuration);

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
        
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseExceptionHandler(options => { });
        app.MapCarter();

        app.MapOpenApi();                  
        app.MapScalarApiReference();  
        
        return app;
    }
}