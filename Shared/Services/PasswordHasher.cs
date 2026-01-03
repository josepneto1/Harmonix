namespace Harmonix.Shared.Services;

public class PasswordHasher
{
    public string HashPassword(string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        return passwordHash;
    }
    
    //dps fazer o verify
}
