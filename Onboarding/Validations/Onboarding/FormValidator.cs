using FluentValidation;

namespace Onboarding.Validations.Onboarding
{
    public class FormValidator : AbstractValidator<ViewModels.Onboarding.Form>
    {
        public FormValidator()
        {
            RuleFor(x => x.Items).SetCollectionValidator(new EnrollmentFormValidator());
            RuleFor(x => x.Deadline).NotNull();
            RuleFor(x => x.Year).NotNull();
            RuleFor(x => x.Semester).NotNull().Must(x=> x == "1" || x == "2").WithMessage("Semestre inválido.");
        }
    }
}
