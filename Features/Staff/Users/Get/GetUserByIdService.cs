using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;
    
namespace Harmonix.Features.Staff.Users.Get;

public class GetUserByIdService
{
    private readonly HarmonixDbContext _context;

    public GetUserByIdService(HarmonixDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetUserByIdResponse>> ExecuteAsync(Guid id, CancellationToken ct)
    {
        var user = await _context.Users
            .Where(u => u.Id == id)
            .Select(u => new GetUserByIdResponse(
                u.Id,
                u.Company.Name,
                u.Name,
                u.Email.Value,
                u.Role,
                u.CreatedAt
            ))
            .FirstOrDefaultAsync(ct);

        if (user is null)
            return Result<GetUserByIdResponse>.Fail(CommonError.NotFound);

        return Result<GetUserByIdResponse>.Success(user);
    }
}
