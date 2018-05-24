using FluentValidation;
using onboarding.Models;

namespace onboarding.Statuses
{
    public class ContractStatus : BaseStatus<Enrollment>
    {
        public ContractStatus(AbstractValidator<Enrollment> validator, Enrollment entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            return "invalid";
        }
    }
}
