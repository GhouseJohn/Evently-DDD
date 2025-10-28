using Evently.Modules.Users.UnitTests.Abstractions;
using FluentAssertions;
using User.Module.Domain.Models;

namespace Evently.Modules.Users.UnitTests.Users;
public abstract class UserTests : BaseTest
{
    [Fact]
    public void Create_ShouldReturnUser()
    {
        // Act

        var users = UserModel.Create(
            Guid.NewGuid(),
            Faker.Internet.UserName(),
            Faker.Internet.Email(),
            Faker.Address.FullAddress());
        // Assert
        users.Should().NotBeNull();
    }


}
