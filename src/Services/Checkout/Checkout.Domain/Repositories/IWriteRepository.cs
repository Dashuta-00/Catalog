using Checkout.Domain.Common;

namespace Checkout.Domain.Repositories;

public interface IWriteRepository<T>
    where T : IAggregateRoot
{
    Task AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}