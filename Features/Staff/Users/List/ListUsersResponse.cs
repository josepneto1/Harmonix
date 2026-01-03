namespace Harmonix.Features.Staff.Users.List;

public record ListUsersResponse (
    Guid Id,
    string CompanyName,
    string Name,
    string Email
);
