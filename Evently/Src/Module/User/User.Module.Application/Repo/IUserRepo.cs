using User.Module.Domain.Models;

namespace User.Module.Application.Repo;
public interface IUserRepo
{
    Task<IEnumerable<UserModel>> GetAllUser(CancellationToken cancellationToken = default);
    Task<UserModel> GetAllUser(Guid Id, CancellationToken cancellationToken = default);

}

