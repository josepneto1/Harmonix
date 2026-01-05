using Harmonix.Shared.Models.Enums;

namespace Harmonix.Features.Staff.Users.Update;

public record UpdateUserRequest(
    string Name,
    string Email,
    Role Role
);
