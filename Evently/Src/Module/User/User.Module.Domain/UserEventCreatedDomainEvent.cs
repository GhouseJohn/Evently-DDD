using BuildingBlock.Common.Domain;

namespace User.Module.Domain;
public sealed class UserEventCreatedDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}

