using Carter;
using Checkout.Infrastructure.Data.Extensions;
using Scalar.AspNetCore;

namespace Checkout.API;

public static class DependencyInjection
{
	public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
	{
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
		
		return services;
	}

	public static async Task<WebApplication> UseApiServices(this WebApplication app)
	{
		app.MapCarter();
		
		app.MapScalarApiReference(); 
		app.MapOpenApi();
		
		await app.Services.MigrateAndSeedDatabaseAsync();
		
		return app;
	}
}