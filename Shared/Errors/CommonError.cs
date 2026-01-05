using Harmonix.Shared.Errors.Enums;

namespace Harmonix.Shared.Errors;

public static class CommonError
{
    public static Error NotFound => new("common.not-found", "Não encontrado", ErrorStatus.NotFound);
    public static Error InternalError => new("internal.error", "Erro interno", ErrorStatus.InternalError);
    public static Error BadRequest(string message) => new("bad.request", message, ErrorStatus.BadRequest);
    public static Error EmailAlreadyExists => new("common.email-exists", "Este email já existe", ErrorStatus.Conflict);
}
