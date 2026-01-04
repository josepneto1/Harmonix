using Harmonix.Features.Staff.Companies.Create;
using Harmonix.Features.Staff.Companies.Get;
using Harmonix.Features.Staff.Companies.List;
using Harmonix.Features.Staff.Companies.Update;
using Harmonix.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmonix.Features.Staff.Companies;

[ApiController]
[Route("api/staff/[controller]")]
[Authorize(Roles = "SysAdmin")]
public class CompaniesController : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateCompany(
        [FromBody] CreateCompanyRequest request,
        [FromServices] CreateCompanyService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(request, ct);
        return this.GetCreatedResult(result);
    }

    [HttpGet("company/{id:guid}")]
    public async Task<IActionResult> GetCompanyById(
        Guid id,
        [FromServices] GetCompanyByIdService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(id, ct);

        if (result == null)
            return NotFound();

        return this.GetResult(result);
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListCompanies(
        [FromServices] ListCompaniesService service,
        CancellationToken cancellationToken)
    {
        var result = await service.ExecuteAsync(cancellationToken);
        return this.GetResult(result);
    }

    [HttpPut("update/{id:guid}")]
    public async Task<IActionResult> UpdateCompany(
        Guid id, 
        [FromBody] UpdateCompanyRequest request,
        [FromServices] UpdateCompanyService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(id, request, ct);

        if (result == null)
            return NotFound();

        return this.GetResult(result);
    }
}
