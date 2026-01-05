using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.Get;

public class GetCompanyByIdService
{
    private readonly HarmonixDbContext _context;

    public GetCompanyByIdService(HarmonixDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetCompanyByIdResponse>> ExecuteAsync(Guid id, CancellationToken ct)
    {
        var company = await _context.Companies
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new GetCompanyByIdResponse(
                c.Id,
                c.Name,
                c.Alias,
                c.CreatedAt,
                c.ExpirationDate
            ))
            .FirstOrDefaultAsync(ct);

        if (company is null)
            return Result<GetCompanyByIdResponse>.Fail(CommonError.NotFound);

        return Result<GetCompanyByIdResponse>.Success(company);
    }
}
