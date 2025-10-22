using MediatR;

namespace BuildingBlock.Common.Domain;
public interface IDomainEvent : INotification
{
    Guid Id { get; set; }
    DateTime OccuredOn { get; set; }
}

