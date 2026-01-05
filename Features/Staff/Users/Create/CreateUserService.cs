using FluentValidation;
using Harmonix.Features.Staff.Companies.Create;
using Harmonix.Features.Staff.Companies.Update;
using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Errors.DomainErrors;
using Harmonix.Shared.Extensions;
using Harmonix.Shared.Models;
using Harmonix.Shared.Results;
using Harmonix.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Users.Create;

public class CreateUserService
{
    private readonly HarmonixDbContext _context;
    private readonly PasswordHasher _passwordHasher;
    private readonly IValidator<CreateUserRequest> _validator;

    public CreateUserService(
        HarmonixDbContext context,
        PasswordHasher passwordHasher,
        IValidator<CreateUserRequest> validator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    public async Task<Result<CreateUserResponse>> ExecuteAsync(CreateUserRequest request, CancellationToken ct)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            return Result<CreateUserResponse>.Fail(validationResult.ToValidationError());

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId && !c.Removed, ct);
        if (company is null)
            return Result<CreateUserResponse>.Fail(CommonError.NotFound);

        var emailExists = await _context.Users.AnyAsync(u => u.Email == request.Email, ct);
        if (emailExists)
            return Result<CreateUserResponse>.Fail(CommonError.EmailAlreadyExists);

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
