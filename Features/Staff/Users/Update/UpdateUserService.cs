using FluentValidation;
using Harmonix.Shared.Application;
using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Users.Update;

public class UpdateUserService : BaseService<UpdateUserRequest, UpdateUserResponse>
{
    private readonly HarmonixDbContext _context;

    public UpdateUserService(
        HarmonixDbContext context,
        IValidator<UpdateUserRequest> validator)
        : base(validator)
    {
        _context = context;
    }

    protected override async Task<Result<UpdateUserResponse>> HandleAsync(UpdateUserRequest request, CancellationToken ct)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.Id && !u.Removed, ct);

        if (user is null)
            return Result<UpdateUserResponse>.Fail(CommonError.NotFound);

        user.Update(request.Name, request.Email, request.Role);

        await _context.SaveChangesAsync(ct);

        var response = new UpdateUserResponse(user.Id, user.Name);

        return Result<UpdateUserResponse>.Success(response);
    }
}
