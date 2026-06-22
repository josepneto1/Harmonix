using Harmonix.Domain.Common;
using Harmonix.Domain.Common.Validations;

namespace Harmonix.Domain.Companies.ValueObjects;

public sealed record Alias
{
    private const byte MinLength = 3;
    private const byte MaxLength = 30;
    public string Value { get; }

    private Alias(string value) => Value = value;

    public static Result<Alias> Create(string alias)
    {
        if (string.IsNullOrWhiteSpace(alias))
            return Result<Alias>.Fail(CompanyErrors.InvalidAlias);

        var normalized = NormalizeAlias(alias);

        if (!Validate.IsValidText(normalized, MinLength, MaxLength))
            return Result<Alias>.Fail(CompanyErrors.InvalidAlias);

        return Result<Alias>.Success(new Alias(normalized));
    }

    private static string NormalizeAlias(string alias) => alias.Trim().Replace(" ", string.Empty).ToLower();
}
