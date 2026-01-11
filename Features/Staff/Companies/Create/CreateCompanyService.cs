using FluentValidation;
using Harmonix.Shared.Application;
using Harmonix.Shared.Data;
using Harmonix.Shared.Errors.DomainErrors;
using Harmonix.Shared.Models.Companies;
using Harmonix.Shared.Models.Companies.Services;
using Harmonix.Shared.Models.Companies.ValueObjects;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.Create;

public class CreateCompanyService : BaseService<CreateCompanyRequest, CreateCompanyResponse>
{
    private readonly HarmonixDbContext _context;
    private readonly IAliasUniqueChecker _aliasChecker;

    public CreateCompanyService(
        HarmonixDbContext context,
        IAliasUniqueChecker aliasChecker,
        IValidator<CreateCompanyRequest> validator)
        : base (validator)
    {
        _context = context;
        _aliasChecker = aliasChecker;
    }

    protected override async Task<Result<CreateCompanyResponse>> HandleAsync(CreateCompanyRequest request, CancellationToken ct)
    {
        var company = new Company(
            request.Name,
            request.Alias,
            request.ExpirationDate
        );

        var alias = Alias.Create(request.Alias);

        var isUnique = await _aliasChecker.IsUniqueAsync(alias, ct);

        if (!isUnique)
            return Result<CreateCompanyResponse>.Fail(CompanyError.AliasAlreadyExists);

        _context.Companies.Add(company);

        await _context.SaveChangesAsync(ct);

        var response = new CreateCompanyResponse(
            company.Id,
            company.Name,
            company.Alias.Value,
            company.CreatedAt,
            company.ExpirationDate
        );

        return Result<CreateCompanyResponse>.Success(response);
    }
}
