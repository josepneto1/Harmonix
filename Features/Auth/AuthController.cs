using Harmonix.Features.Auth.Login;
using Harmonix.Features.Auth.Refresh;
using Harmonix.Features.Auth.Logout;
using Microsoft.AspNetCore.Mvc;
using Harmonix.Shared.Extensions;

namespace Harmonix.Features.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(request, ct);
        
        return this.GetResult(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshTokenRequest request,
        [FromServices] RefreshTokenService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(request, ct);
        
        return this.GetResult(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutRequest request,
        [FromServices] LogoutService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(request, ct);
        
        return this.GetResult(result);
    }
}