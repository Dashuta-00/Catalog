using System.Reflection;
using Checkout.Domain.Repositories;
using Checkout.Infrastructure.Data;
using Checkout.Infrastructure.Repositories;
using Common.Messaging.Extensions;
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

		services.AddBroker(configuration, Assembly.GetExecutingAssembly());
		
		return services;
	}
}