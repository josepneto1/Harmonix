using Harmonix.Domain.Users.Enums;

namespace Harmonix.Common.CurrentRequest;

public class CurrentRequestData
{
    public static CurrentRequestData Empty { get; } = new(
        userId: null,
        companyId: null,
        email: null,
        role: null,
        companyAlias: null);

    public Guid? UserId { get; }
    public Guid? CompanyId { get; }
    public string? Email { get; }
    public Role? Role { get; }
    public string? CompanyAlias { get; }


    public CurrentRequestData(
        Guid? userId,
        Guid? companyId,
        string? email,
        Role? role,
        string? companyAlias)
    {
        UserId = userId;
        CompanyId = companyId;
        Email = email;
        Role = role;
        CompanyAlias = companyAlias;
    }
}
