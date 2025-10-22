using MediatR;

namespace BuildingBlock.Common.Application.Messaging;
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
