using FluentValidation;

namespace Onboarding.Validations.Onboarding
{
    public class ItemValidator : AbstractValidator<ViewModels.Onboarding.Item>
    {
        public ItemValidator() 
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Cpf).Must(x => Cpf.ValidCPF(x));
        }
    }
}
