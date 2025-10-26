using BuildingBlock.Common.Domain;
using MediatR;

namespace BuildingBlock.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
