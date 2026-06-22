using System.Security.Claims;
using Harmonix.Domain.Users.Enums;

namespace Harmonix.Common.CurrentRequest;

public sealed class CurrentRequestMiddleware : IMiddleware
{
    public const string ItemKey = "CurrentRequest";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var user = context.User;

        var currentRequest = new CurrentRequestData(
            userId: GetGuidClaim(user, "id"),
            companyId: GetGuidClaim(user, "company_id"),
            email: GetClaim(user, "email", ClaimTypes.Email),
            role: GetRoleClaim(user),
            companyAlias: GetClaim(user, "co"));

        context.Items[ItemKey] = currentRequest;

        await next(context);
    }

    private static Guid? GetGuidClaim(ClaimsPrincipal user, params string[] claimTypes)
    {
        var value = GetClaim(user, claimTypes);

        return Guid.TryParse(value, out var parsedValue)
            ? parsedValue
            : null;
    }

    private static Role? GetRoleClaim(ClaimsPrincipal user)
    {
        var value = GetClaim(user, "role", ClaimTypes.Role);

        return Enum.TryParse<Role>(value, ignoreCase: true, out var role)
            ? role
            : null;
    }

    private static string? GetClaim(ClaimsPrincipal user, params string[] claimTypes)
    {
        foreach (var claimType in claimTypes)
        {
            var value = user.FindFirstValue(claimType);

            if (!string.IsNullOrWhiteSpace(value))
                return value;
        }

        return null;
    }
}
