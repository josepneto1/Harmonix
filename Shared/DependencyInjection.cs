using Harmonix.Shared.Data.Services;
using Harmonix.Shared.Models.Common.Services;
using Harmonix.Shared.Models.Companies.Services;
using Harmonix.Shared.Security;
using Harmonix.Shared.Services;

namespace Harmonix.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddScoped<IEmailUniqueChecker, EmailUniqueChecker>();
        services.AddScoped<IAliasUniqueChecker, AliasUniqueChecker>();
        
        services.AddScoped<PasswordHasher>();
        services.AddScoped<JwtTokenProvider>();

        return services;
    }
}
