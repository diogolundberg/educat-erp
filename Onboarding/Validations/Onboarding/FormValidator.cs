using FluentValidation;

namespace Onboarding.Validations.Onboarding
{
    public class FormValidator : AbstractValidator<ViewModels.Onboarding.Form>
    {
        public FormValidator()
        {
            RuleFor(x => x.Enrollments).SetCollectionValidator(new EnrollmentFormValidator());

            RuleFor(x => x.StartAt).NotNull().WithName(onboarding.Resources.Models.Onboarding.StartAt);
            RuleFor(x => x.EndAt).NotNull().WithName(onboarding.Resources.Models.Onboarding.EndAt);
            RuleFor(x => x.Year).NotNull().WithName(onboarding.Resources.Models.Onboarding.Year);

            RuleFor(x => x.Semester).NotNull().Must(x=> x == "1" || x == "2").WithMessage(string.Format(onboarding.Resources.Shared.NotValidField,onboarding.Resources.Models.Onboarding.Semester));
        }
    }
}
