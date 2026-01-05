using Harmonix.Shared.Errors.Enums;

namespace Harmonix.Shared.Errors;

public sealed record Error(string Code, string Message, ErrorStatus Status, object? Details = null)
{
    public static readonly Error None = new("", "", ErrorStatus.None);
}
