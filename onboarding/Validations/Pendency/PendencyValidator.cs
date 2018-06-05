using FluentValidation;

namespace onboarding.Validations.Pendency
{
    public class PendencyValidator : AbstractValidator<Models.Pendency>
    {
        public PendencyValidator()
        {
            RuleFor(pendency => pendency.SectionId).NotNull().WithName(Resources.Models.Pendency.SectionId);
            RuleFor(pendency => pendency.Description).NotNull().WithName(Resources.Models.Pendency.Description);
        }
    }
}
