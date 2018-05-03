using FluentValidation;
using onboarding.Models;
using onboarding.ViewModels.Scheduling;
using System;

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
            RuleFor(scheduling => scheduling.ScheduleStartTime).NotNull().WithName(Resources.Models.Scheduling.ScheduleStartTime);
            RuleFor(scheduling => scheduling.ScheduleEndTime).NotNull().WithName(Resources.Models.Scheduling.ScheduleEndTime);
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
            RuleFor(scheduling => scheduling.FormBreaks).Custom((formBreaks, context) =>
            {
                if (formBreaks.Count > 0)
                {
                    for (int i = 0; i < formBreaks.Count; i++)
                    {
                        FormBreak formBreak = formBreaks[i];

                        try
                        {
                            DateTime startDate = DateTime.Now.AddHours(int.Parse(formBreak.Start.Split(":")[0]))
                                                             .AddMinutes(int.Parse(formBreak.Start.Split(":")[1]));
                        }
                        catch (Exception)
                        {
                            context.AddFailure(string.Format("Breaks[{0}].Start", i), string.Format(Resources.Shared.NotValidField, Resources.Models.Scheduling.BreakStart));
                        }

                        try
                        {
                            DateTime endDate = DateTime.Now.AddHours(int.Parse(formBreak.End.Split(":")[0]))
                                                           .AddMinutes(int.Parse(formBreak.End.Split(":")[1]));
                        }
                        catch (Exception)
                        {
                            context.AddFailure(string.Format("Breaks[{0}].End", i), string.Format(Resources.Shared.NotValidField, Resources.Models.Scheduling.BreakEnd));
                        }
                    }
                }
            });
            RuleFor(scheduling => scheduling).Must(x => x.EndAt >= x.StartAt).WithMessage(Resources.Models.Scheduling.StartAtGreaterThanEndAt);
        }
    }
}
