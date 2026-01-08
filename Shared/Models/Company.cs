namespace Harmonix.Shared.Models;

public class Company : BaseEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Alias { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset ExpirationDate { get; private set; }

    public List<User> Users { get; private set; } = new List<User>();

    protected Company() { }

    public Company(string name, string alias, DateTimeOffset expirationDate)
    {
        Id = Guid.NewGuid();
        Name = name;
        Alias = alias;
        CreatedAt = DateTimeOffset.UtcNow;
        ExpirationDate = expirationDate;
    }

    public void Update(string? name, string? alias, DateTimeOffset? expirationDate)
    {
        if (name is not null)
            Name = name.Trim();
        
        if (alias is not null)
            Alias = NormalizeAlias(alias);

        if (expirationDate is not null)
            ExpirationDate = expirationDate.Value;

        UpdatedAt = DateTimeOffset.UtcNow;
    }

    private static string NormalizeAlias(string alias)
    {
        return alias
            .Trim()
            .Replace(" ", "")
            .ToLower();
    }
}
