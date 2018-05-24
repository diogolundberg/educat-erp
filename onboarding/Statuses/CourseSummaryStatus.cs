using FluentValidation;
using onboarding.Models;

namespace onboarding.Statuses
{
    public class CourseSummaryStatus : BaseStatus<Enrollment>
    {
        public CourseSummaryStatus(AbstractValidator<Enrollment> validator, Enrollment entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            if (_entity.CourseSummary.HasValue)
            {
                return "valid";
            }
            else
            {
                return "invalid";
            }
        }
    }
}
