using Harmonix.Shared.Models.Users;

namespace Harmonix.Shared.Models;

public class RefreshToken : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public User User { get; private set; } = null!;

    protected RefreshToken() { }

    public RefreshToken(Guid userId, string token, DateTime expiresAt, string? deviceInfo = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public bool IsExpired => DateTime.UtcNow > ExpiresAt;
    public bool IsRevoked => RevokedAt.HasValue;
    public bool IsValid => !IsExpired && !IsRevoked;

    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
        Remove();
    }
}