using Carter;
using Checkout.Infrastructure.Data.Extensions;
using Common.Kernel.Exceptions.Handler;
using Common.Logging.Extensions;
using Common.Logging.Middleware;
using Scalar.AspNetCore;

namespace Checkout.API;

public static class DependencyInjection
{
	public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddExceptionHandler<CustomExceptionHandler>();
		
		services.AddCarter();
		
		services.AddOpenApi(options =>
		{
			options.AddDocumentTransformer((document, _, _) =>
			{
				document.Info.Title = "Checkout API";
				document.Info.Version = "v1";
				document.Info.Description = "Checkout Service API";
				return Task.CompletedTask;
			});
		});
		
		services.AddCommonLogging(configuration);
		
		return services;
	}

	public static async Task<WebApplication> UseApiServices(this WebApplication app)
	{
		app.UseMiddleware<RequestLoggingMiddleware>();

		app.UseExceptionHandler(_ => { });
		
		app.MapCarter();
		
		app.MapScalarApiReference(); 
		app.MapOpenApi();
		
		await app.Services.MigrateAndSeedDatabaseAsync();
		
		return app;
	}
}