using System.Linq.Expressions;
using Checkout.Domain.Common;
using Checkout.Domain.Repositories;
using Checkout.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure.Repositories;

public abstract class BaseRepository<T>(OrderContext dbContext)
    : IRepository<T>
    where T: class, IAggregateRoot 
{
    public virtual async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var result = await dbContext.Set<T>().ToListAsync();
        return result;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        var result = await dbContext
            .Set<T>()
            .FirstOrDefaultAsync(e => 
                EF.Property<Guid>(e, "Id") == id);

        return result;
    }

    public virtual async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await dbContext.Set<T>()
            .Where(predicate)
            .ToListAsync();
        return result;
    }

    public virtual async Task AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        return await dbContext.SaveChangesAsync() > 0;
    }
}