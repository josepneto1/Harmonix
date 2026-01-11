using Harmonix.Shared.Models.Common.ValueObjects;

namespace Harmonix.Shared.Models.Common.Services;

public interface IEmailUniqueChecker
{
    Task<bool> IsUniqueAsync(Email email, CancellationToken ct);
}