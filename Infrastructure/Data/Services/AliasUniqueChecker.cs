using Harmonix.Domain.Companies.Services;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Infrastructure.Data.Services;

public sealed class AliasUniqueChecker : IAliasUniqueChecker
{
    private readonly HarmonixDbContext _context;

    public AliasUniqueChecker(HarmonixDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsUniqueAsync(string alias)
    {
        return !await _context.Companies
            .IgnoreQueryFilters()
            .AsNoTracking()
            .AnyAsync(c => c.Alias.Value == alias);
    }
}
