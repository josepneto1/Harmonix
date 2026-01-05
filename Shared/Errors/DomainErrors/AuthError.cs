using Harmonix.Shared.Errors.Enums;

namespace Harmonix.Shared.Errors.DomainErrors;

public static class AuthError
{
    public static Error InvalidCredentials => new("auth.invalid-credentials", "Email ou senha inválido", ErrorStatus.Unauthorized);
    public static Error InvalidRefreshToken => new("auth.invalid-refresh-token", "Refresh token inválido ou expirado", ErrorStatus.Unauthorized);
}
