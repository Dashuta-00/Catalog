using Catalog.Domain.Repositories;

namespace Catalog.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		return services;
	}
}