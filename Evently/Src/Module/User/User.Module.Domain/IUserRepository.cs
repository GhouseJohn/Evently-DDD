using User.Module.Domain.Models;

namespace User.Module.Domain;
public interface IUserRepository
{
    Task<UserModel?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(UserModel user);
}

