using Checkout.Domain.Orders;
using Checkout.Domain.Repositories;
using Checkout.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure.Repositories;

public class OrderRepository(OrderContext context): 
    BaseRepository<Order>(context), IOrderRepository
{
    public override async Task<IReadOnlyList<Order>> GetAllAsync()
    {
        var result = await DbContext.Orders
            .Include(x => x.Items)
            .AsNoTracking()
            .ToListAsync();
        
        return result;
    }

    public override async Task<Order?> GetByIdAsync(Guid id)
    {
        var result = await DbContext.Orders
            .Include(x => x.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return result;
    }

    public async Task<IEnumerable<Order>> GetOrdersByAccountName(string accountName)
    {
        var result = await DbContext.Orders
            .Include(x => x.Items)
            .AsNoTracking()
            .Where(o => o.AccountName == accountName)
            .ToListAsync();
        
        return result;
    }
}