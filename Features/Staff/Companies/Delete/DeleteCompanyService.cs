using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.Delete;

public class DeleteCompanyService
{
    private readonly HarmonixDbContext _context;

    public DeleteCompanyService(HarmonixDbContext context)
    {
        _context = context;
    }
    public async Task<Result> ExecuteAsync(Guid id, CancellationToken ct)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == id && !c.Removed, ct);

        if (company is null)
            return Result.Fail(CommonError.NotFound);

        company.Remove();
        company.Deactivate();

        var users = await _context.Users
            .Where(u => u.CompanyId == company.Id && !u.Removed)
            .ToListAsync(ct);

        foreach (var user in users)
        {
            user.Remove();
        }

        await _context.SaveChangesAsync(ct);

        return Result.Success();
    }
}
