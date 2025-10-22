using BuildingBlock.Common.Domain;

namespace User.Module.Domain.Models;
public sealed class UserModel : Entity
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }


    public static UserModel Create(Guid userId, string userName, string email, string address)
    {
        var @event = new UserModel
        {
            UserId = userId,
            UserName = userName,
            Email = email,
            Address = address
        };
        @event.AddDomainEvent(new UserEventCreatedDomainEvent(@event.UserId));
        return @event;
    }
}

