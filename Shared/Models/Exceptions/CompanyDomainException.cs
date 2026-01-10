namespace Harmonix.Shared.Models.Exceptions;

public class CompanyDomainException
{
    public static DomainException InvalidName() => new("company.name.invalid", "Nome inválido");
    public static DomainException InvalidAlias() => new("company.alias.invalid", "Alias inválido");
    public static DomainException InvalidExpirationDate() => new("company.expiration-date.invalid", "Data de expiração inválida");
}
