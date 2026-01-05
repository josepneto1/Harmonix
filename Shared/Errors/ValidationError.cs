using Harmonix.Shared.Errors.Enums;

namespace Harmonix.Shared.Errors;

public sealed record ValidationError(string Field, string Message)
{
    public static Error Validation(List<ValidationError> details) =>
        new("validation.failed", "Ocorreram erros de validação", ErrorStatus.BadRequest, details);
}
