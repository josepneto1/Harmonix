using FluentValidation;
using Harmonix.Shared.Application;
using Harmonix.Shared.Data;
using Harmonix.Shared.Models;
using Harmonix.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Features.Staff.Companies.Create;

public class CreateCompanyService : BaseService<CreateCompanyRequest, CreateCompanyResponse>
{
    private readonly HarmonixDbContext _context;

    public CreateCompanyService(
        HarmonixDbContext context, 
        IValidator<CreateCompanyRequest> validator)
        : base (validator)
    {
        _context = context;
    }

    protected override async Task<Result<CreateCompanyResponse>> HandleAsync(CreateCompanyRequest request, CancellationToken ct)
    {
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
