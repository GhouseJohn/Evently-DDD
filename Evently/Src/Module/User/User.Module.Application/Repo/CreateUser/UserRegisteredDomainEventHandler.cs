using BuildingBlock.Common.Application.Messaging;
using BuildingBlock.Common.Domain;
using Evently.Common.Application.EventBus;
using MediatR;
using User.Module.Application.Users;
using User.Module.Domain;
using User.Module.IntegrationEvents;

namespace User.Module.Application.Repo.CreateUser;
internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus bus)
                    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<UserResponse> result = await sender.Send(
            new GetUserQuery(domainEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetUserQuery), result.Error);
        }
        await bus.PublishAsync(
          new UserRegisteredIntegrationEvent(
              domainEvent.Id,
              domainEvent.OccuredOn,
              result.Value.UserId,
              result.Value.Email,
              result.Value.UserName),
          cancellationToken);
    }
}
