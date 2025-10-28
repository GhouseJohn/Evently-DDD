using System.Reflection;
using User.Module.Domain.Models;
using User.Module.Infrastructure;

namespace Evently.Modules.Users.ArchitectureTests.Abstractions;
#pragma warning disable CA1515 // Because an application's API isn't typically referenced from outside the assembly, types can be made internal
public abstract class BaseTest2
{
    protected static readonly Assembly ApplicationAssembly = typeof(User.Module.Application.AssemblyReference).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(UserModel).Assembly;
    protected static readonly Assembly Infrastructure = typeof(UserModuleInfrastructure).Assembly;
    protected static readonly Assembly presentation = typeof(User.Module.Presentation.AssemblyReference).Assembly;
}

