using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Common.InfraStructure.Authentication;
internal static class AuthenticationExtension
{
    internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddAuthentication().AddJwtBearer();

        services.AddHttpContextAccessor();

        services.ConfigureOptions<JwtBearerConfigureOptions>();

        return services;
    }
}
