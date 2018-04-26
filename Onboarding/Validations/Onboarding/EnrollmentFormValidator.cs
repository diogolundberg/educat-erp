using FluentValidation;

namespace onboarding.Validations.Onboarding
{
    public class EnrollmentFormValidator : AbstractValidator<ViewModels.Onboarding.EnrollmentForm>
    {
        public EnrollmentFormValidator() 
        {
            RuleFor(x => x.Name).NotNull().WithName(onboarding.Resources.Models.Enrollment.Name);

            RuleFor(x => x.CPF).Must(x => Cpf.ValidCPF(x));

            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}
