using Harmonix.Shared.Models.Enums;

namespace Harmonix.Features.Staff.Users.Create;

public record CreateUserResponse(
    Guid Id,
    Guid CompanyId,
    string Name,
    string Email,
    Role Role,
    DateTimeOffset CreatedAt
);
