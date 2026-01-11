using FluentValidation;

namespace Harmonix.Features.Staff.Companies.Update;

public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome da empresa é obrigatório")
            .MinimumLength(3).WithMessage("O nome não pode ter menos de 3 caracteres")
            .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres")
            .When(x => x.Name is not null);
        RuleFor(x => x.Alias)
            .NotEmpty().WithMessage("O alias é obrigatório")
            .MinimumLength(3).WithMessage("O alias não pode ter menos de 3 caracteres")
            .MaximumLength(30).WithMessage("O alias não pode ter mais de 30 caracteres")
            .When(x => x.Alias is not null);
        RuleFor(x => x.ExpirationDate)
            .GreaterThan(DateTimeOffset.UtcNow).WithMessage("A data de expiração deve futura")
            .When(x => x.ExpirationDate is not null);
    }
}