using FluentValidation;
using Harmonix.Shared.Data;
using Harmonix.Shared.Errors.DomainErrors;
using Harmonix.Shared.Extensions;
using Harmonix.Shared.Models;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.Create;

public class CreateCompanyService
{
    private readonly HarmonixDbContext _context;
    private readonly IValidator<CreateCompanyRequest> _validator;

    public CreateCompanyService(
        HarmonixDbContext context, 
        IValidator<CreateCompanyRequest> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<CreateCompanyResponse>> ExecuteAsync(CreateCompanyRequest request, CancellationToken ct)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            return Result<CreateCompanyResponse>.Fail(validationResult.ToValidationError());

        var aliasExists = await _context.Companies.AnyAsync(c => c.Alias == request.Alias, ct);
        if (aliasExists)
            return Result<CreateCompanyResponse>.Fail(CompanyError.AliasAlreadyExists);

        var company = new Company(
            request.Name,
            request.Alias,
            request.ExpirationDate
        );

        _context.Companies.Add(company);
        await _context.SaveChangesAsync(ct);

        var response = new CreateCompanyResponse(
            company.Id,
            company.Name,
            company.Alias,
            company.CreatedAt,
            company.ExpirationDate
        );

        return Result<CreateCompanyResponse>.Success(response);
    }
}
