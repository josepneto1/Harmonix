using FluentValidation;
using Harmonix.Common;
using Harmonix.Domain.Common;
using Harmonix.Domain.Common.Errors;
using Harmonix.Domain.Users.ValueObjects;
using Harmonix.Infrastructure.Auth;
using Harmonix.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Api.Features.Staff.Users.ChangePassword;

public class ChangePasswordHandler : BaseHandler<ChangePasswordRequest, bool>
{
    private readonly HarmonixDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public ChangePasswordHandler(
        HarmonixDbContext context,
        IPasswordHasher passwordHasher,
        IValidator<ChangePasswordRequest> validator)
        : base(validator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    protected override async Task<Result<bool>> HandleAsync(ChangePasswordRequest request, CancellationToken ct)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.Id && !u.Removed);

        if (user is null)
            return Result<bool>.Fail(CommonErrors.NotFound);

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
            return Result<bool>.Fail(passwordResult.Error);

        var passwordHash = _passwordHasher.HashPassword(passwordResult.Data.Value);
        user.SetPasswordHash(passwordHash);

        var activeRefreshTokens = await _context.RefreshTokens
            .IgnoreQueryFilters()
            .Where(rt =>
                rt.UserId == user.Id &&
                rt.RevokedAt == null)
            .ToListAsync();

        foreach (var refreshToken in activeRefreshTokens)
            refreshToken.Revoke();

        await _context.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}

public record ChangePasswordRequest(Guid Id, string Password);
