using Harmonix.Shared.Errors.Enums;

namespace Harmonix.Shared.Errors.DomainErrors;

public static class CompanyError
{
    public static Error Expired => new("company.expired", "Acesso expirado", ErrorStatus.Forbidden);
    public static Error AliasAlreadyExists => new("company.alias-exists", "Empresa já existe", ErrorStatus.Conflict);
    public static Error Inactive => new("company.is-inactive", "Empresa inativa", ErrorStatus.Unauthorized);

}
