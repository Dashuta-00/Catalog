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
    
    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await DbContext.Set<T>().ToListAsync(cancellationToken);
        return result;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(e => 
                EF.Property<Guid>(e, "Id") == id,
                cancellationToken);

        return result;
    }

    public virtual async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var result = await DbContext.Set<T>()
            .Where(predicate)
            .ToListAsync(cancellationToken);
        return result;
    }

    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await DbContext.Set<T>().AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        DbContext.Set<T>().Update(entity);
        return await DbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public virtual async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        DbContext.Set<T>().Remove(entity);
        return await DbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}