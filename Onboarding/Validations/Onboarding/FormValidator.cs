using FluentValidation;

namespace Onboarding.Validations.Onboarding
{
    public class FormValidator : AbstractValidator<ViewModels.Onboarding.Form>
    {
        public FormValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Cpf).Must(x => Cpf.ValidCPF(x));
        }
    }
}
