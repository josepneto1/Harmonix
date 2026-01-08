using FluentValidation;
using Harmonix.Shared.Application;
using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.Update;

public class UpdateCompanyService : BaseService<UpdateCompanyRequest, UpdateCompanyResponse>
{
    private readonly HarmonixDbContext _context;

    public UpdateCompanyService(
        HarmonixDbContext context,
        IValidator<UpdateCompanyRequest> validator) 
        : base(validator)
    {
        _context = context;
    }

    protected override async Task<Result<UpdateCompanyResponse>> HandleAsync(UpdateCompanyRequest request,CancellationToken ct)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.Id && !c.Removed, ct);

        if (company is null)
            return Result<UpdateCompanyResponse>.Fail(CommonError.NotFound);

        company.Update(request.Name, request.Alias, request.ExpirationDate);

        await _context.SaveChangesAsync(ct);

        var response = new UpdateCompanyResponse(company.Id, company.Name);

        return Result<UpdateCompanyResponse>.Success(response);
    }
}
