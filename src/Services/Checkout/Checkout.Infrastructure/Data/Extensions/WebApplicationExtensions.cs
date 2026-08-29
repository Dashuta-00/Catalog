using Checkout.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure.Data.Extensions;

public static class WebApplicationExtensions
{
    public static async Task MigrateAndSeedDatabaseAsync(
        this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();

        await dbContext.Database.MigrateAsync();
        
        await OrderContextSeed.SeedAsync(dbContext);
    }
}