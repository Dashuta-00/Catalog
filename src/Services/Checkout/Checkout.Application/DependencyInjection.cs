using Common.Kernel.Behaviors;

namespace Checkout.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		var assembly = typeof(Program).Assembly;
		var licenseKey = configuration.GetSection("MediatR:LicenseKey").Value;

		services.AddMediatR(config =>
		{
			config.LicenseKey = licenseKey;
			config.RegisterServicesFromAssembly(assembly);
		});
		
		return services;
	}
}