using Harmonix.Shared.Models.Exceptions;

namespace Harmonix.Shared.Middlewares;

public class DomainExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public DomainExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var error = new { code = ex.Code, message = ex.Message };

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
