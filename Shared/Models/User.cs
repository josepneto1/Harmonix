using Harmonix.Shared.Models.Enums;

namespace Harmonix.Shared.Models;

public class User : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public Role Role { get; private set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Company Company { get; private set; } = null!;
    public List<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    protected User() { }

    public User(
        Guid companyId,
        string name,
        string email,
        string passwordHash,
        Role role)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTimeOffset.UtcNow;
        Activate();
    }

    public void Update(string? name, string? email, Role? role)
    {
        if (name is not null)
            Name = name.Trim();

        if (email is not null)
            Email = email;

        if (role is Role r)
            Role = r;

        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
