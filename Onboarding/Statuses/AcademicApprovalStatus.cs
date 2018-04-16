using FluentValidation;
using Onboarding.Models;
using System.Linq;

namespace Onboarding.Statuses
{
    public class AcademicApprovalStatus : BaseStatus<Enrollment>
    {
        public AcademicApprovalStatus(AbstractValidator<Enrollment> validator, Enrollment entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            if (!string.IsNullOrEmpty(_entity.SentAt) && !string.IsNullOrEmpty(_entity.AcademicApproval) && _entity.AcademicPendencies.Count() == 0)
            {
                return "approved";
            }
            else
            {
                return "pending";
            }
        }
    }
}
