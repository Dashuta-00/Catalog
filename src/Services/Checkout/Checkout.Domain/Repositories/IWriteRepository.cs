using Checkout.Domain.Common;

namespace Checkout.Domain.Repositories;

public interface IWriteRepository<T>
    where T : IAggregateRoot
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
}