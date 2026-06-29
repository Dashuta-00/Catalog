using MediatR;

namespace Common.Kernel.CQRS.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}