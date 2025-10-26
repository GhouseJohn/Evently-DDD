using System.Collections.Concurrent;
using System.Reflection;
using BuildingBlock.Common.Application.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Common.InfraStructure.outbox;
public static class DomainEventHandlersFactory
{
    private static readonly ConcurrentDictionary<string, Type[]> HandlersDictionary = new();

    public static IEnumerable<IDomainEventHandler> GetHandlers(
        Type type,
        IServiceProvider serviceProvider,
        Assembly assembly)
    {
        // The key line is here. It will return the cached empty array if found.
        Type[] domainEventHandlerTypes = HandlersDictionary.GetOrAdd(
            $"{assembly.GetName().Name}{type.Name}",
            _ =>
            {
                Type handlerInterfaceType = typeof(IDomainEventHandler<>).MakeGenericType(type);

                Type[] foundTypes = assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(handlerInterfaceType) &&
                               !t.IsAbstract &&
                               !t.IsInterface)
                    .ToArray();

                // Check: If foundTypes is empty, that empty array is being cached!
                return foundTypes;
            });

        List<IDomainEventHandler> handlers = [];
        foreach (Type domainEventHandlerType in domainEventHandlerTypes)
        {
            object domainEventHandler = serviceProvider.GetRequiredService(domainEventHandlerType);

            handlers.Add((domainEventHandler as IDomainEventHandler)!);
        }

        return handlers;
    }
}
