using Harmonix.Shared.Errors.Enums;

namespace Harmonix.Shared.Errors.DomainErrors;

public static class CompanyError
{
    public static Error Expired => new("company.expired", "Company subscription has expired", ErrorStatus.Forbidden);
    public static Error AliasAlreadyExists => new("company.alias-exists", "Company alias already exists", ErrorStatus.Conflict);

}
