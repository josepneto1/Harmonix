using Harmonix.Shared.Data;
using Harmonix.Shared.Errors.DomainErrors;
using Harmonix.Shared.Results;
using Harmonix.Shared.Security;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Auth.Refresh;

public class RefreshTokenService
{
    private readonly HarmonixDbContext _context;
    private readonly JwtTokenProvider _jwtTokenProvider;

    public RefreshTokenService(
        HarmonixDbContext context,
        JwtTokenProvider jwtTokenProvider)
    {
        _context = context;
        _jwtTokenProvider = jwtTokenProvider;
    }

    public async Task<Result<RefreshTokenResponse>> ExecuteAsync(RefreshTokenRequest request, CancellationToken ct)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(rt => rt.User)
                .ThenInclude(u => u.Company)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, ct);

        if (refreshToken is null || !refreshToken.IsValid)
            return Result<RefreshTokenResponse>.Fail(AuthError.InvalidRefreshToken);

        var user = refreshToken.User;
        var newAccessToken = _jwtTokenProvider.GenerateToken(user.Id, user.Email, user.Role.ToString(), user.Company.Alias);
        var expiresAt = DateTime.UtcNow.AddMinutes(60);

        var response = new RefreshTokenResponse(newAccessToken, expiresAt);
        return Result<RefreshTokenResponse>.Success(response);
    }
}
