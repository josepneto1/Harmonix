using Harmonix.Shared.Data;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Users.List;

public class ListUsersService
{
    private readonly HarmonixDbContext _context;

    public ListUsersService(HarmonixDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<ListUsersResponse>>> ExecuteAsync(CancellationToken ct)
    {
        var data = await _context.Users
            .Select(u => new ListUsersResponse(
                u.Id,
                u.Company.Name,
                u.Name,
                u.Email.Value
            ))
            .ToListAsync(ct);

        return Result<List<ListUsersResponse>>.Success(data);
    }
}