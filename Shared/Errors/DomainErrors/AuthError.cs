using Harmonix.Shared.Errors.Enums;

namespace Harmonix.Shared.Errors.DomainErrors;

public static class AuthError
{
    public static Error InvalidCredentials => new("auth.invalid-credentials", "Email or password is invalid", ErrorStatus.Unauthorized);
    public static Error InvalidRefreshToken => new("auth.invalid-refresh-token", "Refresh token is invalid or expired", ErrorStatus.Unauthorized);
}
