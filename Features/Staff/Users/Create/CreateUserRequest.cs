namespace Harmonix.Features.Staff.Users.Create;

public record CreateUserRequest(
    Guid CompanyId,
    string Name,
    string Email,
    string Password,
    string Role
);
