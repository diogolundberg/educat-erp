using FluentValidation;

namespace Onboarding.Validations.Onboarding
{
    public class EnrollmentFormValidator : AbstractValidator<ViewModels.Onboarding.EnrollmentForm>
    {
        public EnrollmentFormValidator() 
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.CPF).Must(x => Cpf.ValidCPF(x));
        }
    }
}
