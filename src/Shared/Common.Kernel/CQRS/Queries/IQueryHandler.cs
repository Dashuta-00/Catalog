using MediatR;

namespace Common.Kernel.CQRS.Queries;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse: notnull
{
}

public interface IQueryHandler<in TQuery>
    : IRequestHandler<TQuery, Unit>
    where TQuery : IQuery<Unit>
{
}