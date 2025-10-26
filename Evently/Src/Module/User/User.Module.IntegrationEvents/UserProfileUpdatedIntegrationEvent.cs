using Evently.Common.Application.EventBus;

namespace User.Module.IntegrationEvents;

public sealed class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public UserRegisteredIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId,
        string email,
        string firstName)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
        Email = email;
        FirstName = firstName;
    }

    public Guid UserId { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

}
