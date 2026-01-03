namespace Harmonix.Features.Staff.Users.Get;

public record GetUserByIdResponse(
    Guid Id,
    string CompanyName,
    string Name,
    string Email,
    string Role,
    DateTimeOffset CreatedAt
);
