using FluentValidation;
using onboarding.Models;

namespace onboarding.Validations.Scheduling
{
    public class SchedulingValidator : AbstractValidator<Models.Scheduling>
    {
        public SchedulingValidator(DatabaseContext context)
        {
            RuleFor(scheduling => scheduling.OnboardingId).NotNull().WithName(Resources.Models.Scheduling.OnboardingId);
            RuleFor(scheduling => scheduling.StartAt).NotNull().WithName(Resources.Models.Scheduling.StartAt);
            RuleFor(scheduling => scheduling.EndAt).NotNull().WithName(Resources.Models.Scheduling.EndAt);
            RuleFor(scheduling => scheduling.Intervals).NotNull().WithName(Resources.Models.Scheduling.Intervals);
            RuleFor(scheduling => scheduling).Must(x => x.EndAt >= x.StartAt).WithPropertyName("StartAt").WithMessage(Resources.Models.Scheduling.StartAtGreaterThanEndAt);
        }
    }
}
