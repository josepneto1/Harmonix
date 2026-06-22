using Harmonix.Common;
using Harmonix.Domain.Common;
using Harmonix.Domain.Common.Errors;
using Harmonix.Domain.Products;
using Harmonix.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Api.Features.Products.List;

public class ListProductsHandler : BaseHandler<ListProductsRequest, ListProductsResponse>
{
    private readonly HarmonixDbContext _context;

    public ListProductsHandler(HarmonixDbContext context)
    {
        _context = context;
    }

    protected override async Task<Result<ListProductsResponse>> HandleAsync(ListProductsRequest request, CancellationToken ct)
    {
        if (CurrentRequest.CompanyId is not Guid companyId)
            return Result<ListProductsResponse>.Fail(AuthErrors.Unauthorized);

        var page = request.NormalizedPage;
        var pageSize = request.NormalizedPageSize;

        var query = _context.Products.AsNoTracking().Where(p => !p.Removed && p.CompanyId == companyId);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLowerInvariant();

            query = query.Where(p =>
                p.Code.ToLower().Contains(search) ||
                p.Name.ToLower().Contains(search));
        }

        query = ApplySorting(query, request.SortBy, request.SortDirection);

        var totalCount = await query.CountAsync(ct);

        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductView(
                p.Id,
                p.Company.Name,
                p.Code,
                p.Name,
                p.Description == null ? null : p.Description.Value,
                p.CreatedAt
            ))
            .ToListAsync(ct);

        var response = new ListProductsResponse(
            products,
            page,
            pageSize,
            totalCount);

        return Result<ListProductsResponse>.Success(response);
    }

    private static IQueryable<Product> ApplySorting(IQueryable<Product> query, string? sortBy, string? sortDirection)
    {
        var isDesc = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);

        sortBy = (sortBy ?? string.Empty).Trim().ToLowerInvariant();

        return sortBy switch
        {
            "companyname" => isDesc
                ? query.OrderByDescending(p => p.Company.Name)
                : query.OrderBy(p => p.Company.Name),

            "code" => isDesc
                ? query.OrderByDescending(p => p.Code)
                : query.OrderBy(p => p.Code),

            "name" => isDesc
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),

            "createdat" => isDesc
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt),

            _ => isDesc
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt)
        };
    }
}

public record ListProductsRequest(
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    string? Search = null)
{
    public int NormalizedPage => Page <= 0 ? 1 : Page;
    public int NormalizedPageSize => Math.Clamp(PageSize <= 0 ? 25 : PageSize, 1, 100);
}

public record ListProductsResponse(
    List<ProductView> Data,
    int Page,
    int PageSize,
    int TotalCount
);

public record ProductView(
    Guid Id,
    string CompanyName,
    string Code,
    string Name,
    string? Description,
    DateTimeOffset CreatedAt
);
