using Microsoft.EntityFrameworkCore;
using User.Module.Domain;
using User.Module.Domain.Models;
using User.Module.Infrastructure.Database;

namespace User.Module.Infrastructure.User;
internal sealed class UserRepository : IUserRepository
{
    private readonly UserDbContext context;
    public UserRepository(UserDbContext dbContext)
    {
        context = dbContext;
    }

    public async Task<UserModel?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.EventUser.SingleOrDefaultAsync(u => u.UserId == id, cancellationToken);
    }

    public void Insert(UserModel user)
    {
        context.EventUser.Add(user);
    }
}
