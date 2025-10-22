using User.Module.Domain.Models;

namespace User.Module.Domain;
public interface IEventRepository
{
    void Insert(UserModel @event);
}
