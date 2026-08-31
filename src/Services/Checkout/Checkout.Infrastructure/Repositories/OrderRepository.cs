using Checkout.Domain.Orders;
using Checkout.Domain.Repositories;
using Checkout.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure.Repositories;

public class OrderRepository(OrderContext context): 
    BaseRepository<Order>(context), IOrderRepository
{
    public override async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await DbContext.Orders
            .Include(x => x.Items)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        return result;
    }

    public override async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await DbContext.Orders
            .Include(x => x.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        return result;
    }

    public async Task<IEnumerable<Order>> GetOrdersByAccountName(string accountName, CancellationToken cancellationToken)
    {
        var result = await DbContext.Orders
            .Include(x => x.Items)
            .AsNoTracking()
            .Where(o => o.AccountName == accountName)
            .ToListAsync(cancellationToken);
        
        return result;
    }

    public async Task<bool> DeleteOrderById(Guid id, CancellationToken cancellationToken)
    {
        var result = await DbContext.Orders
            .Include(x => x.Items)
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        
        await DbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}