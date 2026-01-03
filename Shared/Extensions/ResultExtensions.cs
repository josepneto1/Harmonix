using Harmonix.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace Harmonix.Shared.Extensions;

public static class ResultExtensions
{
    public static IActionResult GetResult<T>(this ControllerBase controller, Result<T> result)
    {
        if (result.IsSuccess)
            return controller.Ok(result.Data);

        var error = result.Error;
        return controller.StatusCode(
            (int)error.Status,
            new
            {
                error = error.Code,
                message = error.Message
            });
    }

    public static IActionResult GetResult(this ControllerBase controller, Result result)
    {
        if (result.IsSuccess)
            return controller.NoContent();

        var error = result.Error;
        return controller.StatusCode(
            (int)error.Status,
            new
            {
                error = error.Code,
                message = error.Message
            });
    }

    public static IActionResult GetCreatedResult<T>(this ControllerBase controller, Result<T> result)
    {
        if (result.IsSuccess)
            return controller.StatusCode(StatusCodes.Status201Created, result.Data);

        var error = result.Error;
        return controller.StatusCode(
            (int)error.Status,
            new
            {
                error = error.Code,
                message = error.Message
            });
    }
}
