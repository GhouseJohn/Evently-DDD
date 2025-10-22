using Microsoft.EntityFrameworkCore;
using User.Module.Application.Repo;
using User.Module.Domain.Models;
using User.Module.Infrastructure.Database;

namespace User.Module.Infrastructure.User;
internal sealed class UserRepo : IUserRepo
{
    private readonly UserDbContext _dbContext;
    public UserRepo(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<UserModel>> GetAllUser(CancellationToken cancellationToken = default)
    {
        try
        {
            List<UserModel> x = await (from n in _dbContext.EventUser
                                       select n).ToListAsync(cancellationToken);
            return x;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserModel> GetAllUser(Guid Id, CancellationToken cancellationToken = default)
    {
        UserModel? result = await (from n in _dbContext.EventUser
                                   where n.UserId == Id
                                   select n).FirstOrDefaultAsync(cancellationToken);
        return result;
    }
}
