namespace Harmonix.Features.Auth.Refresh;

public record RefreshTokenResponse(
    string AccessToken, 
    DateTime ExpiresAt
);
