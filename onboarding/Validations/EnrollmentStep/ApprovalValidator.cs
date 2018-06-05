using FluentValidation;
using onboarding.Validations.Pendency;

namespace onboarding.Validations.Approval
{
    public class EnrollmentStepValidator : AbstractValidator<Models.EnrollmentStep>
    {
        public EnrollmentStepValidator()
        {
            RuleFor(enrollmentStep => enrollmentStep.Pendencies).SetCollectionValidator(new PendencyValidator());
        }
    }
}
