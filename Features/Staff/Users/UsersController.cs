using Harmonix.Features.Staff.Users.Create;
using Harmonix.Features.Staff.Users.Get;
using Harmonix.Features.Staff.Users.List;
using Harmonix.Features.Staff.Users.Update;
using Harmonix.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmonix.Features.Staff.Users;

[ApiController]
[Route("api/staff/[controller]")]
[Authorize(Roles = "SysAdmin")]
public class UsersController : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserRequest request,
        [FromServices] CreateUserService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(request, ct);
        return this.GetCreatedResult(result);
    }

    [HttpGet("user/{id:guid}")]
    public async Task<IActionResult> GetUserById(
        Guid id,
        [FromServices] GetUserByIdService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(id, ct);

        if (result == null)
            return NotFound();

        return this.GetResult(result);
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListUsers(
        [FromServices] ListUsersService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(ct);
        return this.GetResult(result);
    }

    [HttpPut("update/{id:guid}")]
    public async Task<IActionResult> UpdateUser(
        Guid id,
        [FromBody] UpdateUserRequest request,
        [FromServices] UpdateUserService service,
        CancellationToken ct)
    {
        var result = await service.ExecuteAsync(id, request, ct);

        if (result == null)
            return NotFound();

        return this.GetResult(result);
    }
}