namespace Harmonix.Features.Staff.Users.Create;

public record CreateUserResponse(
    Guid Id,
    Guid CompanyId,
    string Name,
    string Email,
    string Role,
    DateTimeOffset CreatedAt
);
