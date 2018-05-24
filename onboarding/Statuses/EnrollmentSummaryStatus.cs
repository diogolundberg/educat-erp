using FluentValidation;
using onboarding.Models;

namespace onboarding.Statuses
{
    public class EnrollmentSummaryStatus : BaseStatus<Enrollment>
    {
        public EnrollmentSummaryStatus(AbstractValidator<Enrollment> validator, Enrollment entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            if (_entity.EnrollmentSummary.HasValue)
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
