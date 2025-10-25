using BuildingBlock.Common.Domain;

namespace User.Module.Domain;
public sealed class UserRegisteredDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
