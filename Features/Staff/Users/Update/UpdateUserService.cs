using Harmonix.Shared.Data;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Users.Update;

public class UpdateUserService
{
    private readonly HarmonixDbContext _context;

    public UpdateUserService(HarmonixDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UpdateUserResponse>> ExecuteAsync(
        Guid id,
        UpdateUserRequest request,
        CancellationToken ct)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id, ct);

        await _context.SaveChangesAsync(ct);

        var response = new UpdateUserResponse(
            user.Id,
            user.Name
        );

        return Result<UpdateUserResponse>.Success(response);
    }
}