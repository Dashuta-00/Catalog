using Carter;
using Checkout.Infrastructure.Data.Extensions;

namespace Checkout.API;

public static class DependencyInjection
{
	public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddCarter();
		
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		
		return services;
	}

	public static async Task<WebApplication> UseApiServices(this WebApplication app)
	{
		app.MapCarter();
		
		app.UseSwagger();
		app.UseSwaggerUI();

		await app.Services.MigrateAndSeedDatabaseAsync();
		
		app.MapGet("/", () => "Hello World!");

		return app;
	}
}