using FluentValidation;
using onboarding.Models;

namespace onboarding.Validations.Scheduling
{
    public class SchedulingValidator : AbstractValidator<Models.Scheduling>
    {
        public SchedulingValidator(DatabaseContext databaseContext)
        {
            RuleFor(scheduling => scheduling.OnboardingId).NotNull().WithName(Resources.Models.Scheduling.OnboardingId);
            RuleFor(scheduling => scheduling.StartAt).NotNull().WithName(Resources.Models.Scheduling.StartAt);
            RuleFor(scheduling => scheduling.EndAt).NotNull().WithName(Resources.Models.Scheduling.EndAt);
            RuleFor(scheduling => scheduling.Intervals).NotNull().WithName(Resources.Models.Scheduling.Intervals);
            RuleFor(scheduling => scheduling.Intervals).Custom((intervals, context) =>
            {
                if (!string.IsNullOrEmpty(intervals))
                {
                    int convertedValue = 0;
                    int.TryParse(intervals, out convertedValue);

                    if (convertedValue == 0)
                    {
                        context.AddFailure("Intervals", string.Format(Resources.Shared.NotValidField, Resources.Models.Scheduling.Intervals));
                    }
                }
            });
            RuleFor(scheduling => scheduling).Must(x => x.EndAt >= x.StartAt).WithMessage(Resources.Models.Scheduling.StartAtGreaterThanEndAt);
        }
    }
}
