
using MediatR;

namespace BuildingBlock.Common.Application.Messaging;

public interface ICommand : ICommand<Unit>
{
}
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
