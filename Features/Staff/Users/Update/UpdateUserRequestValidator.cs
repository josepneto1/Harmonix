using FluentValidation;

namespace Harmonix.Features.Staff.Users.Update;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório")
            .EmailAddress().WithMessage("O email informado é inválido")
            .MaximumLength(255).WithMessage("O email não pode ter mais de 255 caracteres");
        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("O papel informado é inválido");
    }
}