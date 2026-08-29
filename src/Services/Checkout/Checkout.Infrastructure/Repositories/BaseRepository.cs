using System.Linq.Expressions;
using Checkout.Domain.Common;
using Checkout.Domain.Repositories;
using Checkout.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure.Repositories;

public abstract class BaseRepository<T>(OrderContext context)
    : IRepository<T>
    where T: class, IAggregateRoot
{
    protected readonly OrderContext DbContext = context;
    
    public virtual async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var result = await DbContext.Set<T>().ToListAsync();
        return result;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        var result = await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(e => 
                EF.Property<Guid>(e, "Id") == id);

        return result;
    }

    public virtual async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await DbContext.Set<T>()
            .Where(predicate)
            .ToListAsync();
        return result;
    }

    public virtual async Task AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        DbContext.Set<T>().Update(entity);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        return await DbContext.SaveChangesAsync() > 0;
    }
}