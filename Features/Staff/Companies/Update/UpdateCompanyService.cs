using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.Update;

public class UpdateCompanyService
{
    private readonly HarmonixDbContext _context;

    public UpdateCompanyService(HarmonixDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UpdateCompanyResponse>> ExecuteAsync(
        Guid id,
        UpdateCompanyRequest request,
        CancellationToken ct)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == id, ct);

        //fazer metodos de update na model

        await _context.SaveChangesAsync(ct);

        var response = new UpdateCompanyResponse(
            company.Id,
            company.Name
        );

        return Result<UpdateCompanyResponse>.Success(response);
    }
}
