using Harmonix.Shared.Models.Companies.ValueObjects;
using Harmonix.Shared.Models.Exceptions;

namespace Harmonix.Shared.Models.Companies;

public class Company : BaseEntity
{
    public string Name { get; private set; } = null!;
    public Alias Alias { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset ExpirationDate { get; private set; }
    public bool IsActive { get; private set; }

    public List<User> Users { get; private set; } = new List<User>();

    protected Company() { }

    public Company(string name, string alias, DateTimeOffset expirationDate)
    {
        name = name.Trim();
        ValidateCreate(name, expirationDate);

        Id = Guid.NewGuid();
        Name = name;
        Alias = Alias.Create(alias);
        CreatedAt = DateTimeOffset.UtcNow;
        ExpirationDate = expirationDate;
        IsActive = true;
    }

    public void Update(string? name, string? alias, DateTimeOffset? expirationDate)
    {
        if (name is not null)
        {
            name = name.Trim();
            ValidateName(name);
            Name = name;
        }
        
        if (alias is not null)
            Alias = Alias.Create(alias);

        if (expirationDate is not null)
        {
            ValidateExpirationDate(expirationDate);
            ExpirationDate = expirationDate.Value;
        }

        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    private static void ValidateCreate(string name, DateTimeOffset expirationDate)
    {
        ValidateName(name);
        ValidateExpirationDate(expirationDate);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw CompanyDomainException.InvalidName();
        
        if (name.Length is < 3 or > 100)
            throw CompanyDomainException.InvalidName();
    }

    private static void ValidateExpirationDate(DateTimeOffset? expirationDate)
    {
        if (expirationDate is null)
            throw CompanyDomainException.InvalidExpirationDate();

        if (expirationDate <= DateTimeOffset.UtcNow)
            throw CompanyDomainException.InvalidExpirationDate();
    }
}
