using BuildingBlock.Common.Application.Messaging;
using Evently.Common.Application.EventBus;
using User.Module.Domain;
using User.Module.IntegrationEvents;

namespace User.Module.Application.Repo.CreateUser;
internal sealed class UserRegisteredDomainEventHandler(IEventBus bus)
                    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
#pragma warning disable S125 // Sections of code should not be commented out

        //Result<UserResponse> result = await sender.Send(new GetUserQuery(domainEvent.UserId),

        //    cancellationToken);
        //if (result.IsFailure)
        //{
        //    throw new EventlyException(nameof(GetUserQuery), result.Error);
        //}
#pragma warning restore S125 // Sections of code should not be commented out


        await bus.PublishAsync(
          new UserRegisteredIntegrationEvent(
              domainEvent.Id,
              domainEvent.OccuredOn,
              domainEvent.UserId,
              "domainEvent.Email",
              "domainEvent.UserName"),
          cancellationToken);
    }
}
