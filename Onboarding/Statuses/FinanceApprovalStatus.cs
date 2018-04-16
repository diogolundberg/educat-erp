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
            if (!string.IsNullOrEmpty(_entity.SentAt) && !string.IsNullOrEmpty(_entity.FinanceApproval) && _entity.FinancePendencies.Count() == 0)
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
