using BuildingBlock.Common.Domain;
using BuildingBlock.Common.InfraStructure.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace BuildingBlock.Common.InfraStructure.outbox;
public sealed class InsertOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
                             DbContextEventData eventData,
                             InterceptionResult<int> result,
                            CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            InsertOutboxMessages(eventData.Context);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InsertOutboxMessages(DbContext context)
    {
        #region originalcode

#pragma warning disable S125 // Sections of code should not be commented out

        //var outboxMessages = context
        //    .ChangeTracker
        //    .Entries<Entity>()
        //    .Select(entry => entry.Entity)
        //    .SelectMany(entity =>

        //    {
        //        IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;

        //        entity.ClearDomainEvents();

        //        return domainEvents;
        //    })
        //    .Select(domainEvent => new OutboxMessage
        //    {
        //        Id = domainEvent.Id,
        //        Type = domainEvent.GetType().Name,
        //        Content = JsonConvert.SerializeObject(domainEvent, SerializerSettings.Instance),
        //        OccurredOnUtc = domainEvent.OccuredOn
        //    })
        //    .ToList();

        //context.Set<OutboxMessage>().AddRange(outboxMessages);
#pragma warning restore S125 // Sections of code should not be commented out

        #endregion

        IEnumerable<Entity> entities = context.ChangeTracker.Entries<Entity>().Select(e => e.Entity);

        IEnumerable<IDomainEvent> domainEvents = entities
            .SelectMany(e =>
            {
                var events = e.DomainEvents.ToList();
                e.ClearDomainEvents();
                return events;
            });

        var outboxMessages = domainEvents.Select(e => new OutboxMessage
        {
            Id = e.Id,
            Type = e.GetType().Name,
            Content = JsonConvert.SerializeObject(e, SerializerSettings.Instance),
            OccurredOnUtc = e.OccuredOn
        }).ToList();

        context.AddRange(outboxMessages);

    }
}

