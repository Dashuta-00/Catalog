using Asp.Versioning;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Repositories;
using Scalar.AspNetCore;

namespace Catalog.API;

public static class DependencyInjection
{
	public static IServiceCollection AddApiServices(
		this IServiceCollection services, 
		IConfiguration configuration)
	{
		services.AddApiVersioning(options =>
		{
			options.ReportApiVersions = true;
			options.AssumeDefaultVersionWhenUnspecified = true;
			options.DefaultApiVersion = new ApiVersion(1, 0);
		})
		.AddApiExplorer(options =>
		{
			options.GroupNameFormat = "'v'VVV";
			options.SubstituteApiVersionInUrl = true;
		});
		
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddOpenApi(options =>
		{
			options.AddDocumentTransformer((document, context, cancellationToken) =>
			{
				switch (context.DocumentName)
				{
					case "v1":
						document.Info.Title = "Catalog API";
						document.Info.Version = "v1";
						document.Info.Description = "Catalog API Version 1";
						break;
					case "v2":
						document.Info.Title = "Catalog API";
						document.Info.Version = "v2";
						document.Info.Description = "Catalog API Version 2";
						break;
				}

				return Task.CompletedTask;
			});
		});
		services.AddTransient<IBrandRepository, CatalogRepository>();
		services.AddTransient<ICategoryRepository, CatalogRepository>();
		services.AddTransient<ICatalogItemRepository, CatalogRepository>();

		var assembly = typeof(Application.DependencyInjection).Assembly;
		var licenseKey = configuration.GetSection("MediatR:LicenseKey").Value;
		services.AddMediatR(config =>
		{
			config.LicenseKey = licenseKey;
			config.RegisterServicesFromAssembly(assembly);
		});
		
		return services;
	}

	public static WebApplication UseApiServices(this WebApplication app)
	{
		app.MapControllers();
		
		app.MapOpenApi();
		app.MapScalarApiReference();

		return app;
	}
}