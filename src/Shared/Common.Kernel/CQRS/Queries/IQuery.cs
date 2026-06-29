using MediatR;

namespace Common.Kernel.CQRS.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
