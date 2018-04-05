using FluentValidation;

namespace Onboarding.Validations.Onboarding
{
    public class FormValidator : AbstractValidator<ViewModels.Onboarding.Form>
    {
        public FormValidator()
        {
            RuleFor(x => x.Items).SetCollectionValidator(new ItemValidator());
            RuleFor(x => x.Deadline).NotNull();
        }
    }
}
