using FluentValidation;

namespace Onboarding.Validations.Onboarding
{
    public class FormValidator : AbstractValidator<ViewModels.Onboarding.Form>
    {
        public FormValidator()
        {
            RuleFor(x => x.Enrollments).SetCollectionValidator(new EnrollmentFormValidator());
            RuleFor(x => x.StartAt).NotNull();
            RuleFor(x => x.EndAt).NotNull();
            RuleFor(x => x.Year).NotNull();
            RuleFor(x => x.Semester).NotNull().Must(x=> x == "1" || x == "2").WithMessage("Semestre inválido.");
        }
    }
}
