using Harmonix.Shared.Models.Enums;

namespace Harmonix.Features.Staff.Users.Get;

public record GetUserByIdResponse(
    Guid Id,
    string CompanyName,
    string Name,
    string Email,
    Role Role,
    DateTimeOffset CreatedAt
);
