using Checkout.Domain.Repositories;
using Checkout.Infrastructure.Data;
using Checkout.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services, 
		IConfiguration configuration)
	{
		var pgConnection = configuration.GetConnectionString("PgConnection");
		services.AddDbContext<OrderContext>(options =>
		{
			options.UseNpgsql(pgConnection);
		});

		services.AddScoped<IOrderRepository, OrderRepository>();
		
		return services;
	}
}