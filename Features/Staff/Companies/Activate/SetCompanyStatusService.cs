using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.Activate;

public sealed class SetCompanyStatusService
{
    private readonly HarmonixDbContext _context;

    public SetCompanyStatusService(HarmonixDbContext context)
    {
        _context = context;
    }

    public async Task<Result> ExecuteAsync(Guid companyId, SetCompanyStatusRequest request, CancellationToken ct)
    {
        var company = await _context.Companies
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == companyId && !c.Removed, ct);

        if (company is null)
            return Result.Fail(CommonError.NotFound);

        if (request.IsActive) company.Activate();
        else company.Deactivate();

        await _context.SaveChangesAsync(ct);

        return Result.Success();
    }
}
