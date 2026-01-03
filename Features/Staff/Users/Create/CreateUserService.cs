using Harmonix.Shared.Data;
using Harmonix.Shared.Models;
using Harmonix.Shared.Results;
using Harmonix.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Users.Create;

public class CreateUserService
{
    private readonly HarmonixDbContext _context;
    private readonly PasswordHasher _passwordHasher;

    public CreateUserService(
        HarmonixDbContext context,
        PasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<CreateUserResponse>> ExecuteAsync(CreateUserRequest request, CancellationToken ct)
    {
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        var user = new User(
            request.CompanyId,
            request.Name,
            request.Email,
            passwordHash,
            request.Role
        );

        _context.Users.Add(user);
        await _context.SaveChangesAsync(ct);

        var response = new CreateUserResponse(
            user.Id,
            user.CompanyId,
            user.Name,
            user.Email,
            user.Role,
            user.CreatedAt
        );

        return Result<CreateUserResponse>.Success(response);
    }
}
