using System.Linq.Expressions;
using Checkout.Domain.Common;

namespace Checkout.Domain.Repositories;

public interface IReadRepository<T>
    where T: IAggregateRoot
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
}