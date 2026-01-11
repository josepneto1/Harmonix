using Harmonix.Shared.Models.Common.Services;
using Harmonix.Shared.Models.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Shared.Data.Services;

public sealed class EmailUniqueChecker : IEmailUniqueChecker
{
    private readonly HarmonixDbContext _context;
    public EmailUniqueChecker(HarmonixDbContext context)
    {
        _context = context;
    }
    public async Task<bool> IsUniqueAsync(Email email, CancellationToken ct)
    {
        return !await _context.Users
            .AsNoTracking()
            .AnyAsync(u =>
                u.Email == email,
                ct);
    }
}
