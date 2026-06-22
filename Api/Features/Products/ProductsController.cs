using Harmonix.Api.Features.Products.List;
using Harmonix.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmonix.Api.Features.Products;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    [HttpGet("list")]
    public async Task<IActionResult> ListProducts(
        [FromQuery] ListProductsRequest request,
        ListProductsHandler handler,
        CancellationToken ct)
    {
        var result = await handler.ExecuteAsync(request, ct);
        return this.GetResult(result);
    }
}
