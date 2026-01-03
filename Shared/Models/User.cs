namespace Harmonix.Shared.Models;

public class User : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string Role { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Company Company { get; private set; } = null!;

    protected User() { }

    public User(
        Guid companyId,
        string name,
        string email,
        string passwordHash,
        string role)
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
}
