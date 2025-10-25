namespace User.Module.IntegrationEvents;
public sealed class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public UserRegisteredIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid UserId,
        string UserName,
        string Email)
        : base(id, occurredOnUtc)
    {
        this.UserId = UserId;
        this.Email = Email;
        this.UserName = UserName;
    }
    public Guid UserId { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
}
