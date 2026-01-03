using Harmonix.Shared.Errors.Enums;

namespace Harmonix.Shared.Errors;

public static class CommonError
{
    public static Error NotFound => new("common.not-found", "Not found", ErrorStatus.NotFound);
    public static Error InternalError => new("internal.error", "Internal server error", ErrorStatus.InternalError);
    public static Error BadRequest(string message) => new("bad.request", message, ErrorStatus.BadRequest);
    public static Error EmailAlreadyExists => new("common.email-exists", "Email already in use", ErrorStatus.Conflict);
}
