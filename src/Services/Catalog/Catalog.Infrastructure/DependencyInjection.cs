using Catalog.Infrastructure.Data.Seed;
using Marten;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("PgConnection")!;

		services.AddMarten(options =>
			{
				options.Connection(connectionString);

				options.Schema.For<CatalogItem>()
					.Index(x => x.Brand!.Id)
					.Index(x => x.Category!.Id);
			})
			.UseLightweightSessions()
			.InitializeWith<InitializeDatabaseAsync>();
		
		return services;
	}
}