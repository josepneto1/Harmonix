using FluentValidation;
using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Extensions;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Users.Update;

public class UpdateUserService
{
    private readonly HarmonixDbContext _context;
    private readonly IValidator<UpdateUserRequest> _validator;

    public UpdateUserService(
        HarmonixDbContext context,
        IValidator<UpdateUserRequest> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<UpdateUserResponse>> ExecuteAsync(
        Guid id,
        UpdateUserRequest request,
        CancellationToken ct)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            return Result<UpdateUserResponse>.Fail(validationResult.ToValidationError());

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id && !u.Removed, ct);
        if (user is null)
            return Result<UpdateUserResponse>.Fail(CommonError.NotFound);

        //fazer metodos de update

        await _context.SaveChangesAsync(ct);

        var response = new UpdateUserResponse(
            user.Id,
            user.Name
        );

        return Result<UpdateUserResponse>.Success(response);
    }
}