using Harmonix.Shared.Models.Exceptions;

namespace Harmonix.Shared.Models.Companies.ValueObjects;

public sealed record Alias
{
    public const int MaxLength = 30;
    public const int MinLength = 3;
    public string Value { get; }

    private Alias(string value)
    {
        Value = value;
    }

    public static Alias Create(string alias)
    {
        if (string.IsNullOrEmpty(alias))
            throw CompanyDomainException.InvalidAlias();

        var normalized = NormalizeAlias(alias);

        if (normalized.Length is < MinLength or > MaxLength)
            throw CompanyDomainException.InvalidAlias();

        return new Alias(normalized);
    }

    private static string NormalizeAlias(string alias)
    {
        return alias
            .Trim()
            .Replace(" ", "")
            .ToLower();
    }
}
