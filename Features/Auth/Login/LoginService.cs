using Harmonix.Shared.Data;
using Harmonix.Shared.Errors.DomainErrors;
using Harmonix.Shared.Models;
using Harmonix.Shared.Models.Common.ValueObjects;
using Harmonix.Shared.Results;
using Harmonix.Shared.Security;
using Harmonix.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Auth.Login;

public class LoginService
{
    private readonly HarmonixDbContext _context;
    private readonly JwtTokenProvider _jwtTokenProvider;
    private readonly PasswordHasher _passwordHasher;

    public LoginService(
        HarmonixDbContext context,
        JwtTokenProvider jwtTokenProvider,
        PasswordHasher passwordHasher)
    {
        _context = context;
        _jwtTokenProvider = jwtTokenProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<LoginResponse>> ExecuteAsync(LoginRequest request, CancellationToken ct)
    {
        var userEmail = Email.Create(request.Email);
        var user = await _context.Users
            .IgnoreQueryFilters()
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.Email == userEmail, ct);

        if (user is null)
            return Result<LoginResponse>.Fail(AuthError.InvalidCredentials);
        
        var companyIsActive = user.Company.IsActive;

        if (!companyIsActive)
            return Result<LoginResponse>.Fail(CompanyError.Inactive);

        if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            return Result<LoginResponse>.Fail(AuthError.InvalidCredentials);

        var activeRefreshTokens = await _context.RefreshTokens
            .Where(rt => 
                rt.UserId == user.Id && 
                rt.RevokedAt == null && 
                rt.ExpiresAt > DateTime.UtcNow)
            .ToListAsync(ct);

        foreach (var token in activeRefreshTokens)
        {
            token.Revoke();
        }

        var companyAlias = user.Company.Alias.Value;
        var accessToken = _jwtTokenProvider.GenerateToken(user.Id, user.Email.Value, user.Role.ToString(), companyAlias);
        var refreshTokenString = _jwtTokenProvider.GenerateRefreshToken();
        var expiresAt = DateTime.UtcNow.AddDays(7);

        var refreshToken = new RefreshToken(user.Id, refreshTokenString, expiresAt);
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync(ct);

        var response = new LoginResponse(
            user.Id,
            user.Email.Value,
            user.Role.ToString(),
            companyAlias,
            accessToken,
            refreshTokenString,
            DateTime.UtcNow.AddMinutes(60)
        );

        return Result<LoginResponse>.Success(response);
    }
}
