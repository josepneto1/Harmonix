namespace Harmonix.Shared.Services;

public class PasswordHasher
{
    public string HashPassword(string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        return passwordHash;
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
