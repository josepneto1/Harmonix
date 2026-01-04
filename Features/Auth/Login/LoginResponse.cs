namespace Harmonix.Features.Auth.Login;

public record LoginResponse(
    Guid UserId,
    string Email,
    string Role,
    string CompanyAlias,
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);
