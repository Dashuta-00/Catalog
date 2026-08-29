using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure.Data.Seed;

public class OrderContextSeed
{
    public static async void SeedAsync(OrderContext dbContext)
    {
        if (!await dbContext.Orders.AnyAsync())
        {
            var orders = InitialData.Orders;
            foreach (var order in orders)
            {
                order.SetCreatedAudit("System");
            }
            
            dbContext.Orders.AddRange(orders);
            await dbContext.SaveChangesAsync();
        }
    }
}