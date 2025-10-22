using Microsoft.EntityFrameworkCore;
using User.Module.Application;
using User.Module.Domain.Models;

namespace User.Module.Infrastructure.Database;
public sealed class UserDbContext : DbContext, IUnitOfWork
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    internal DbSet<UserModel> EventUser { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);
    }
}

internal static class Schemas
{
    internal const string Events = "user";
}
