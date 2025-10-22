using User.Module.Domain;
using User.Module.Domain.Models;
using User.Module.Infrastructure.Database;

namespace User.Module.Infrastructure.Event;
internal sealed class EventRepository(UserDbContext userDbContext) : IEventRepository
{
    public void Insert(UserModel @event)
    {
        userDbContext.EventUser.Add(@event);
    }
}
