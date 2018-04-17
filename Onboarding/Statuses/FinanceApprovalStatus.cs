using FluentValidation;
using Onboarding.Models;
using System.Linq;

namespace Onboarding.Statuses
{
    public class FinanceApprovalStatus : BaseStatus<Enrollment>
    {
        public FinanceApprovalStatus(AbstractValidator<Enrollment> validator, Enrollment entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            if (_entity.FinanceApproval.HasValue)
            {
                return "approved";
            }
            else if (_entity.FinancePendencies.Count() > 0)
            {
                return "pending";
            }
            else
            {
                return "inReview";
            }
        }
    }
}
