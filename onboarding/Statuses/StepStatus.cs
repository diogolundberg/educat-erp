using FluentValidation;
using onboarding.Models;

namespace onboarding.Statuses
{
    public class StepStatus : BaseStatus<Step>
    {
        private readonly EnrollmentStep _enrollmentStep;
        public StepStatus(AbstractValidator<Step> validator, Step entity, EnrollmentStep enrollmentStep) : base(validator, entity)
        {
            _enrollmentStep = enrollmentStep;
        }

        public override string GetStatus()
        {
            if(_enrollmentStep == null)
            {
                return "empty";
            }
            else if(_enrollmentStep != null && _enrollmentStep.Pendencies.Count > 0)
            {
                return "invalid";
            }
            else if (!_enrollmentStep.ApprovedAt.HasValue && _entity.HasApproval)
            {
                return "pending";
            }
            else
            {
                return "valid";
            }
        }
    }
}
