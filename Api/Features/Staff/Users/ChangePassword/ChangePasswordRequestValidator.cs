using FluentValidation;

namespace Harmonix.Api.Features.Staff.Users.ChangePassword;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O ID é obrigatório");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória")
            .MinimumLength(8).WithMessage("A senha não pode ter menos de 8 caracteres")
            .MaximumLength(20).WithMessage("A senha não pode ter mais de 20 caracteres");
    }
}
