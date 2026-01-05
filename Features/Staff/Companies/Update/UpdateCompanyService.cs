using FluentValidation;
using Harmonix.Shared.Data;
using Harmonix.Shared.Errors;
using Harmonix.Shared.Extensions;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.Update;

public class UpdateCompanyService
{
    private readonly HarmonixDbContext _context;
    private readonly IValidator<UpdateCompanyRequest> _validator;

    public UpdateCompanyService(
        HarmonixDbContext context,
        IValidator<UpdateCompanyRequest> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<UpdateCompanyResponse>> ExecuteAsync(
        Guid id,
        UpdateCompanyRequest request,
        CancellationToken ct)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            return Result<UpdateCompanyResponse>.Fail(validationResult.ToValidationError());

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == id, ct);
        if (company is null)
            return Result<UpdateCompanyResponse>.Fail(CommonError.NotFound);

        //fazer metodos de update na model

        await _context.SaveChangesAsync(ct);

        var response = new UpdateCompanyResponse(
            company.Id,
            company.Name
        );

        return Result<UpdateCompanyResponse>.Success(response);
    }
}
