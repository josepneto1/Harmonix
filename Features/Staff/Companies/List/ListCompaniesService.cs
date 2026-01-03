using Harmonix.Shared.Data;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.List;

public class ListCompaniesService
{
    private readonly HarmonixDbContext _context;

    public ListCompaniesService(HarmonixDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<ListCompaniesResponse>>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var companies = await _context.Companies
            .AsNoTracking()
            .Where(c => !c.Removed)
            .Select(c => new ListCompaniesResponse(
                c.Id,
                c.Name
            ))
            .ToListAsync(cancellationToken);

        return Result<List<ListCompaniesResponse>>.Success(companies);
    }
}
