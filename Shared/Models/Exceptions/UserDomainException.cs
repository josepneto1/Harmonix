namespace Harmonix.Shared.Models.Exceptions;

public static class UserDomainException
{
    public static DomainException InvalidPassword() => new("user.password.invalid", "Senha inválida");
    public static DomainException InvalidEmail() => new("user.email.invalid", "Email inválido");

}
