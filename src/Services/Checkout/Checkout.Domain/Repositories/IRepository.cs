using Checkout.Domain.Common;

namespace Checkout.Domain.Repositories;

public interface IRepository<T>
    : IReadRepository<T>, IWriteRepository<T>
    where T : IAggregateRoot
{
    
}