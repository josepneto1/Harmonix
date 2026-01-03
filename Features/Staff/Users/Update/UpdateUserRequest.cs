namespace Harmonix.Features.Staff.Users.Update;

public record UpdateUserRequest(
    string Name,
    string Email,
    string Role
);
