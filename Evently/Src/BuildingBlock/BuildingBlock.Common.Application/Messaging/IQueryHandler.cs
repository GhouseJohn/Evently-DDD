using BuildingBlock.Common.Domain;
using MediatR;

namespace BuildingBlock.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
