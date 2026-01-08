using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Users.Delete;

public class DeleteUserService
{
    private readonly HarmonixDbContext _context;

    public DeleteUserService(HarmonixDbContext context)
    {
        _context = context;
    }
    public async Task<Result> ExecuteAsync(Guid id, CancellationToken ct)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(c => c.Id == id && !c.Removed, ct);

        if (user is null)
            return Result.Fail(CommonError.NotFound);

        user.Remove();

        await _context.SaveChangesAsync(ct);

        return Result.Success();
    }
}
