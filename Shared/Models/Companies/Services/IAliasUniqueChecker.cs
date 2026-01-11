using Harmonix.Shared.Models.Companies.ValueObjects;

namespace Harmonix.Shared.Models.Companies.Services;

public interface IAliasUniqueChecker
{
    Task<bool> IsUniqueAsync(Alias alias, CancellationToken ct);
}
