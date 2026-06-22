namespace Harmonix.Common.CurrentRequest;

public static class CurrentRequestExtensions
{
    public static WebApplication UseCurrentRequest(this WebApplication app)
    {
        var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();

        HandlerBase.GetCurrentRequest = () =>
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext is null)
                return CurrentRequestData.Empty;

            return httpContext.Items.TryGetValue(CurrentRequestMiddleware.ItemKey, out var currentRequest)
                ? currentRequest as CurrentRequestData ?? CurrentRequestData.Empty
                : CurrentRequestData.Empty;
        };

        app.UseMiddleware<CurrentRequestMiddleware>();

        return app;
    }
}
