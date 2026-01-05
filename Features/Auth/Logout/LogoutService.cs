using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Auth.Logout;

public class LogoutService
{
    private readonly HarmonixDbContext _context;

    public LogoutService(HarmonixDbContext context)
    {
        _context = context;
    }

    public async Task<Result<LogoutResponse>> ExecuteAsync(LogoutRequest request, CancellationToken ct)
    {
        var refreshToken = await _context.RefreshTokens
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, ct);

        if (refreshToken is null)
            return Result<LogoutResponse>.Fail(CommonError.InternalError);

        if (refreshToken.IsRevoked)
            return Result<LogoutResponse>.Success(new LogoutResponse(true, "Token already revoked"));

        refreshToken.Revoke();
        await _context.SaveChangesAsync(ct);

        return Result<LogoutResponse>.Success(new LogoutResponse(true, "Logout successful"));
    }
}
